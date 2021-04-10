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
            if (e.Data.Type == ReceiveType.ChannelMessage && e.Data.Channel == "#fuelrats")
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

                        string cmdrName = ratsignal.Find(delegate (string ratsignalPart) { return ratsignalPart.StartsWith("CMDR "); });
                        if (cmdrName != null && cmdrName.Length > 5)
                        {
                            cmdrName = cmdrName.Substring(5);
                        }

                        string system = ratsignal.Find(delegate (string ratsignalPart) { return ratsignalPart.StartsWith("Reported System: "); });
                        if (system != null && system.Length > 17)
                        {
                            system = system.Substring(17);
                        }

                        string platform = ratsignal.Find(delegate (string ratsignalPart) { return ratsignalPart.StartsWith("Platform: "); });
                        if (platform != null && platform.Length > 10)
                        {
                            platform = platform.Substring(10);

                            if (platform.Contains("PC"))
                            {
                                platform = "PC";
                            }
                            else if (platform.Contains("Xbox"))
                            {
                                platform = "Xbox";
                            }
                            else if (platform.Contains("Playstation"))
                            {
                                platform = "Playstation";
                            }
                        }

                        string o2 = ratsignal.Find(delegate (string ratsignalPart) { return ratsignalPart.StartsWith("O2: "); });
                        if (o2 != null && o2.Length > 4)
                        {
                            o2 = o2.Substring(4);

                            if (o2.Contains("NOT OK"))
                            {
                                o2 = "NOT OK";
                            }
                        }

                        string language = ratsignal.Find(delegate (string ratsignalPart) { return ratsignalPart.StartsWith("Language: "); });
                        if (language != null && language.Length > 10)
                        {
                            language = language.Substring(10).Trim();
                        }

                        string ircNick = ratsignal.Find(delegate (string ratsignalPart) { return ratsignalPart.StartsWith("IRC Nick "); });
                        if (ircNick != null && ircNick.Length > 9)
                        {
                            ircNick = ircNick.Substring(9).Trim();
                        }
                        else
                        {
                            ircNick = cmdrName;
                        }

                        string caseNumber = ratsignal.Find(delegate (string ratsignalPart) { return ratsignalPart.StartsWith("Case #"); });
                        if (caseNumber != null && caseNumber.Length > 7)
                        {
                            caseNumber = caseNumber.Substring(7, caseNumber.Length - 10);
                        }

                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            _Case.Insert(0, new Case(caseNumber, ircNick, cmdrName, system, platform, o2, language, new IrcMessage(unixTimestamp, timestamp, e.Data.Nick, e.Data.Message)));
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
                                || e.Data.Message.Contains(_Case.ElementAt(i).IrcNick)
                                || e.Data.Nick == _Case.ElementAt(i).IrcNick)
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
