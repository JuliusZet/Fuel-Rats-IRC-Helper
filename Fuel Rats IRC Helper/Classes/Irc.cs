/*
 *   Irc.cs
 *   Fuel Rats IRC Helper
 *
 *   Created by Julius Zitzmann on 2020-05-08.
 *   Copyright © 2021 Julius Zitzmann. All rights reserved.
 *
 *   Feature requests, bug reports or questions?
 *   info@julius-zitzmann.de
 *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */


using Meebey.SmartIrc4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;

namespace Fuel_Rats_IRC_Helper
{
    static class Irc
    {
        private static IrcClient _IrcClient = new IrcClient();
        private static List<Case> _Case = new List<Case>();
        private static Thread _IrcListener;

        private static void OnRawMessage(object sender, IrcEventArgs e)
        {
            if (e.Data.Type == ReceiveType.ChannelMessage && e.Data.Channel == Settings.Get("ircChannel"))
            {
                long unixTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

                // If the message is a Ratsignal
                if (e.Data.Message.StartsWith("RATSIGNAL - ") && e.Data.Nick == "MechaSqueak[BOT]")
                {

                    // Try to add a new case to the case list
                    try
                    {
                        string timestamp = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).ToString(Settings.Get("timestampFormat"));

                        List<string> ratsignal = new List<string>(e.Data.Message.Split(new string[] { " - " }, StringSplitOptions.None));

                        int ratsignalPartToSplit = ratsignal.Count - 1;
                        ratsignal.AddRange(ratsignal.ElementAt(ratsignalPartToSplit).Split('('));
                        ratsignal.RemoveAt(ratsignalPartToSplit);

                        string clientCmdrName = ratsignal.Find(delegate (string ratsignalPart) { return ratsignalPart.StartsWith("CMDR "); });
                        if (clientCmdrName != null && clientCmdrName.Length > 5)
                        {
                            clientCmdrName = clientCmdrName.Substring(5);
                        }

                        string clientSystem = ratsignal.Find(delegate (string ratsignalPart) { return ratsignalPart.StartsWith("Reported System: "); });
                        if (clientSystem != null && clientSystem.Length > 17)
                        {
                            clientSystem = clientSystem.Substring(17);
                        }

                        string clientPlatform = ratsignal.Find(delegate (string ratsignalPart) { return ratsignalPart.StartsWith("Platform: "); });
                        if (clientPlatform != null && clientPlatform.Length > 10)
                        {
                            clientPlatform = clientPlatform.Substring(10);

                            if (clientPlatform.Contains("PC"))
                            {
                                clientPlatform = "PC";
                            }
                            else if (clientPlatform.Contains("Xbox"))
                            {
                                clientPlatform = "Xbox";
                            }
                            else if (clientPlatform.Contains("Playstation"))
                            {
                                clientPlatform = "Playstation";
                            }
                        }

                        string clientO2 = ratsignal.Find(delegate (string ratsignalPart) { return ratsignalPart.StartsWith("O2: "); });
                        if (clientO2 != null && clientO2.Length > 4)
                        {
                            clientO2 = clientO2.Substring(4);

                            if (clientO2.Contains("NOT OK"))
                            {
                                clientO2 = "NOT OK";
                            }
                        }

                        string clientLanguage = ratsignal.Find(delegate (string ratsignalPart) { return ratsignalPart.StartsWith("Language: "); });
                        if (clientLanguage != null && clientLanguage.Length > 10)
                        {
                            clientLanguage = clientLanguage.Substring(10).Trim();
                        }

                        string clientIrcNick = ratsignal.Find(delegate (string ratsignalPart) { return ratsignalPart.StartsWith("IRC Nick "); });
                        if (clientIrcNick != null && clientIrcNick.Length > 9)
                        {
                            clientIrcNick = clientIrcNick.Substring(9).Trim();
                        }
                        else
                        {
                            clientIrcNick = clientCmdrName;
                        }

                        string caseNumber = ratsignal.Find(delegate (string ratsignalPart) { return ratsignalPart.StartsWith("Case #"); });
                        if (caseNumber != null && caseNumber.Length > 7)
                        {
                            caseNumber = caseNumber.Substring(7, caseNumber.Length - 10);
                        }

                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            _Case.Insert(0, new Case(caseNumber, clientIrcNick, clientCmdrName, clientSystem, clientPlatform, clientO2, clientLanguage, new IrcMessage(unixTimestamp, timestamp, e.Data.Nick, e.Data.Message)));
                        });
                    }

                    catch (Exception exception)
                    {
                        MessageBox.Show("An error occured while parsing information from the RATSIGNAL! " + exception.Message + " The case was not added to the list.", "Error");
                    }
                }

                // If the message is not a Ratsignal
                else
                {

                    // Try to associate the message to an open case
                    for (int i = 0; i < _Case.Count; ++i)
                    {
                        if (!_Case.ElementAt(i).IsClosed())
                        {
                            int indexOfCaseNumber = e.Data.Message.IndexOf("#" + _Case.ElementAt(i).CaseNumber);
                            int indexOfCharacterAfterCaseNumber = -1;
                            if (indexOfCaseNumber != -1 && indexOfCaseNumber + _Case.ElementAt(i).CaseNumber.Length + 1 != e.Data.Message.Length)
                            {
                                indexOfCharacterAfterCaseNumber = indexOfCaseNumber + _Case.ElementAt(i).CaseNumber.Length + 1;
                            }

                            // If the message contains "#1" but not #10, #11, etc. (= message from rat or bot) or
                            // if the message contains the client's IRC nick (= message from spatch or bot) or
                            // if the message was sent by the client
                            if ((indexOfCharacterAfterCaseNumber != -1 && (e.Data.Message.ElementAt(indexOfCharacterAfterCaseNumber) < '0' || e.Data.Message.ElementAt(indexOfCharacterAfterCaseNumber) > '9')) || (indexOfCaseNumber != -1 && indexOfCharacterAfterCaseNumber == -1)
                                || e.Data.Message.Contains(_Case.ElementAt(i).ClientIrcNick)
                                || e.Data.Nick == _Case.ElementAt(i).ClientIrcNick)
                            {
                                string timestamp = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).ToString(Settings.Get("timestampFormat"));
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    _Case.ElementAt(i).AddMessage(new IrcMessage(unixTimestamp, timestamp, e.Data.Nick, e.Data.Message));
                                });

                                //If the message was associated to an open case, there is no need to continue searching.
                                break;
                            }
                        }
                    }
                }
            }
        }

        public static void ShowCase(string caseNumber)
        {
            for (int i = 0; i < _Case.Count; ++i)
            {
                if (_Case.ElementAt(i).CaseNumber == caseNumber)
                {
                    _Case.ElementAt(i).ShowCaseWindow();
                    return;
                }
            }
        }

        public static void Connect()
        {
            if (!_IrcClient.IsConnected)
            {
                bool ircUseSsl = false;
                if (Settings.Get("ircUseSsl") == "yes")
                {
                    ircUseSsl = true;
                }
                string ircAddress = Settings.Get("ircAddress");
                int.TryParse(Settings.Get("ircPort"), out int ircPort);
                string ircNick = Settings.Get("ircNick");
                string ircRealname = Settings.Get("ircRealname");
                string ircPassword = Settings.Get("ircPassword");
                string ircChannel = Settings.Get("ircChannel");

                try
                {
                    if (ircUseSsl)
                    {
                        _IrcClient.UseSsl = true;
                    }
                    _IrcClient.OnRawMessage += new IrcEventHandler(OnRawMessage);
                    _IrcClient.ActiveChannelSyncing = true;
                    _IrcClient.Connect(ircAddress, ircPort);
                    _IrcClient.Login(ircNick, ircRealname, 4, "IRC-Helper", ircPassword);
                    _IrcClient.RfcJoin(ircChannel);
                    _IrcListener = new Thread(new ThreadStart(_IrcClient.Listen));
                    _IrcListener.SetApartmentState(ApartmentState.STA);
                    _IrcListener.Start();
                }

                catch (Exception exception)
                {
                    MessageBox.Show("An error occured while connecting to the IRC! " + exception.Message, "Error");
                }
            }

            else
            {
                MessageBox.Show("Already connected to the IRC.");
            }
        }

        public static void Disconnect()
        {
            if (_IrcClient.IsConnected)
            {
                _IrcClient.RfcQuit();
            }

            for (int i = 0; i < _Case.Count; ++i)
            {
                _Case.ElementAt(i).CloseCaseWindow();
            }
        }
    }
}
