/*
 *   Case.cs
 *   Fuel Rats IRC Helper
 *
 *   Created by Julius Zitzmann on 2020-05-08.
 *   Copyright © 2021 Julius Zitzmann. All rights reserved.
 *
 *   Feature requests, bug reports or questions?
 *   info@julius-zitzmann.de
 *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Fuel_Rats_IRC_Helper
{
    public class Case
    {
        private static int _NextId;
        private int _Id;
        private string _CaseNumber;
        private string _ClientIrcNick;
        private string _ClientCmdrName;
        private string _ClientSystem;
        private string _ClientPlatform;
        private string _ClientO2;
        private string _ClientLanguage;
        private string _Status;
        private List<IrcMessage> _IrcMessage;
        private List<Rat> _AssignedRat;
        private DateTimeOffset _StartTime;
        private DateTimeOffset _EndTime;
        private CaseWindow _CaseWindow;

        public Case(string caseNumber, string clientIrcNick, string clientCmdrName, string clientSystem, string clientPlatform, string clientO2, string clientLanguage, IrcMessage ircMessage)
        {
            _Id = _NextId++;
            _CaseNumber = caseNumber;
            _ClientIrcNick = clientIrcNick;
            _ClientCmdrName = clientCmdrName;
            _ClientSystem = clientSystem;
            _ClientPlatform = clientPlatform;
            _ClientO2 = clientO2;
            _ClientLanguage = clientLanguage;
            _Status = "Open";
            _IrcMessage = new List<IrcMessage>();
            _AssignedRat = new List<Rat>();
            _StartTime = DateTimeOffset.FromUnixTimeSeconds(ircMessage.UnixTimestamp);
            _EndTime = DateTimeOffset.MaxValue;
            _CaseWindow = null;

            _IrcMessage.Add(ircMessage);

            if (Settings.Get("autoCopySystemOnPcSignal") == "yes" && (clientPlatform == "PC" || clientPlatform == "PC (Odyssey)"))
            {
                try
                {
                    Clipboard.SetText(clientSystem);
                }
                catch (Exception)
                {

                }
            }
        }

        public Case(string caseNumber, string clientIrcNick, string clientSystem, string clientPlatform, IrcMessage ircMessage) : this(caseNumber, clientIrcNick, "unknown", clientSystem, clientPlatform, "unknown", "unknown", ircMessage)
        {

        }

        public Case() : this("unknown", "unknown", "unknown", "unknown", "unknown", "unknown", "unknown", new IrcMessage())
        {

        }

        public string CaseNumber
        {
            get { return _CaseNumber; }
        }

        public string ClientIrcNick
        {
            get { return _ClientIrcNick; }
            set
            {
                _ClientIrcNick = value;
                if (_CaseWindow != null && _CaseWindow.IsLoaded)
                {
                    _CaseWindow.ClientIrcNick = value;
                }
            }
        }

        public string ClientCmdrName
        {
            get { return _ClientCmdrName; }
            set
            {
                _ClientCmdrName = value;
                if (_CaseWindow != null && _CaseWindow.IsLoaded)
                {
                    _CaseWindow.ClientCmdrName = value;
                }

                Irc.RefreshCaseList();
            }
        }

        public string ClientSystem
        {
            get { return _ClientSystem; }
            set
            {
                _ClientSystem = value;
                if (_CaseWindow != null && _CaseWindow.IsLoaded)
                {
                    _CaseWindow.ClientSystem = value;
                }

                Irc.RefreshCaseList();
            }
        }

        public string ClientPlatform
        {
            get { return _ClientPlatform; }
            set
            {
                _ClientPlatform = value;
                if (_CaseWindow != null && _CaseWindow.IsLoaded)
                {
                    _CaseWindow.ClientPlatform = value;
                }

                Irc.RefreshCaseList();
            }
        }

        public string ClientO2
        {
            get { return _ClientO2; }
            set
            {
                _ClientO2 = value;
                if (_CaseWindow != null && _CaseWindow.IsLoaded)
                {
                    _CaseWindow.ClientO2 = value;
                }

                Irc.RefreshCaseList();
            }
        }

        public string ClientLanguage
        {
            get { return _ClientLanguage; }
            set
            {
                _ClientLanguage = value;
                if (_CaseWindow != null && _CaseWindow.IsLoaded)
                {
                    _CaseWindow.ClientLanguage = value;
                }

                Irc.RefreshCaseList();
            }
        }

        public string Status
        {
            get { return _Status; }
            set
            {
                _Status = value;

                Irc.RefreshCaseList();
            }
        }

        public List<Rat> AssignedRat
        {
            get { return _AssignedRat; }
        }

        public string AssignedRats
        {
            get
            {
                string rats = "";

                for (int i = 0; i != _AssignedRat.Count; ++i)
                {
                    rats += _AssignedRat.ElementAt(i).CmdrName;

                    if (i + 1 != _AssignedRat.Count)
                    {
                        rats += ", ";
                    }
                }

                return rats;
            }
        }

        public bool IsClosed
        {
            get
            {
                if (_EndTime < DateTimeOffset.UtcNow)
                {
                    return true;
                }

                else
                {
                    return false;
                }
            }
        }

        public void ShowCaseWindow()
        {
            if (_CaseWindow == null || !_CaseWindow.IsLoaded)
            {
                _CaseWindow = new CaseWindow(_CaseNumber, _ClientIrcNick, _ClientCmdrName, _ClientSystem, _ClientPlatform, _ClientO2, _ClientLanguage, _IrcMessage, _AssignedRat);
                _CaseWindow.Show();
            }

            else
            {
                _CaseWindow.WindowState = System.Windows.WindowState.Normal;
                _CaseWindow.Activate();
            }
        }

        public void RefreshCaseChat()
        {
            if (_CaseWindow != null && _CaseWindow.IsLoaded)
            {
                _CaseWindow.RefreshCaseChat();
            }
        }

        public void RefreshAssignedRat()
        {
            if (_CaseWindow != null && _CaseWindow.IsLoaded)
            {
                _CaseWindow.RefreshAssignedRat();
            }

            Irc.RefreshCaseList();
        }

        public void CloseCaseWindow()
        {
            if (_CaseWindow != null)
            {
                if (_CaseWindow.IsLoaded)
                {
                    _CaseWindow.Close();
                }

                _CaseWindow = null;
            }
        }

        private void CloseCase(long unixTimestamp)
        {
            _EndTime = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp);
            Irc.RefreshCaseList();
        }

        private void UncloseCase()
        {
            _EndTime = DateTimeOffset.MaxValue;
            Irc.RefreshCaseList();
        }

        public void AddMessage(IrcMessage ircMessage)
        {
            _IrcMessage.Add(ircMessage);

            RefreshCaseChat();

            if (ircMessage.Text.Contains("!go") && (ircMessage.Text.Contains(' ' + CaseNumber + ' ') || ircMessage.Text.Contains(' ' + ClientIrcNick + ' ')))
            {
                List<string> go = new List<string>(ircMessage.Text.Split(new string[] { "!go" }, StringSplitOptions.None).ElementAt(1).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

                int i = 2;

                if (go.ElementAt(1) != CaseNumber && go.ElementAt(1) != ClientIrcNick)
                {
                    i = 1;
                }

                for (; i != go.Count; ++i)
                {
                    _AssignedRat.Add(new Rat(go.ElementAt(i)));

                    RefreshAssignedRat();
                }
            }

            if (ircMessage.SenderNickname == Settings.Get("ircNickBot"))
            {
                if (ircMessage.Text.Contains("Caution: Client of case #") && ircMessage.Text.Contains(" has changed IRC nick to "))
                {
                    ClientIrcNick = ircMessage.Text.Split(new string[] { " has changed IRC nick to " }, StringSplitOptions.None).ElementAt(1);
                }

                else if (ircMessage.Text.Contains("Client nick for case #") && ircMessage.Text.Contains(" has been changed to "))
                {
                    string clientIrcNick = ircMessage.Text.Split(new string[] { " has been changed to " }, StringSplitOptions.None).ElementAt(1);
                    ClientIrcNick = clientIrcNick.Remove(clientIrcNick.Length - 1);
                }

                else if (ircMessage.Text.Contains("Client name for case #") && ircMessage.Text.Contains(" has been changed to: "))
                {
                    string clientCmdrName = ircMessage.Text.Split(new string[] { " has been changed to: " }, StringSplitOptions.None).ElementAt(1);
                    if (clientCmdrName.Contains(" "))
                    {
                        ClientCmdrName = clientCmdrName.Split(new string[] { " " }, StringSplitOptions.None).ElementAt(0);
                    }
                    else
                    {
                        ClientCmdrName = clientCmdrName.Remove(clientCmdrName.Length - 1);
                    }
                }

                else if (ircMessage.Text.Contains("System for case #") && ircMessage.Text.Contains(" has been changed to "))
                {
                    string clientSystem = ircMessage.Text.Split(new string[] { " has been changed to " }, StringSplitOptions.None).ElementAt(1);
                    if (clientSystem.StartsWith("\""))
                    {
                        ClientSystem = clientSystem.Split('\"').ElementAt(1);
                    }
                    else
                    {
                        ClientSystem = clientSystem;
                    }
                }

                else if (ircMessage.Text.Contains("ATTENTION: System for case #") && ircMessage.Text.Contains(" has been automatically corrected to "))
                {
                    string clientSystem = ircMessage.Text.Split(new string[] { " has been automatically corrected to " }, StringSplitOptions.None).ElementAt(1);
                    if (clientSystem.StartsWith("\""))
                    {
                        ClientSystem = clientSystem.Split('\"').ElementAt(1);
                    }
                    else
                    {
                        ClientSystem = clientSystem;
                    }
                }

                else if (ircMessage.Text.Contains("Platform for case #") && ircMessage.Text.Contains(" set to: "))
                {
                    string clientPlatform = ircMessage.Text.Split(new string[] { " set to: " }, StringSplitOptions.None).ElementAt(1);

                    if (clientPlatform.Contains("PC"))
                    {
                        ClientPlatform = "PC";
                    }
                    else if (clientPlatform.Contains("Xbox"))
                    {
                        ClientPlatform = "Xbox";
                    }
                    else if (clientPlatform.Contains("Playstation"))
                    {
                        ClientPlatform = "Playstation";
                    }
                    else
                    {
                        ClientPlatform = clientPlatform;
                    }
                }

                else if (ircMessage.Text.Contains("Case #") && ircMessage.Text.Contains(" is marked as using Odyssey"))
                {
                    ClientPlatform = "PC (Odyssey)";
                }

                else if (ircMessage.Text.Contains("Case #") && ircMessage.Text.Contains(" is marked as not using Odyssey"))
                {
                    ClientPlatform = "PC";
                }

                else if (ircMessage.Text.Contains("CODE RED! ") && ircMessage.Text.Contains(" is on emergency oxygen!"))
                {
                    ClientO2 = "NOT OK";
                }

                else if (ircMessage.Text.Contains("Case #") && ircMessage.Text.Contains(" is no longer CR."))
                {
                    ClientO2 = "OK";
                }

                else if (ircMessage.Text.Contains("Language for case #") && ircMessage.Text.Contains(" has now been changed to "))
                {
                    ClientLanguage = ircMessage.Text.Split(new string[] { " has now been changed to " }, StringSplitOptions.None).ElementAt(1).Split(' ').ElementAt(0);
                }

                else if (ircMessage.Text.Contains("Status of case #") && ircMessage.Text.Contains(" set to: "))
                {
                    string status = ircMessage.Text.Split(new string[] { " set to: " }, StringSplitOptions.None).ElementAt(1);
                    Status = status.Remove(status.Length - 1);
                }

                else if (ircMessage.Text.Contains("Successfully closed case #"))
                {
                    CloseCase(ircMessage.UnixTimestamp);
                }

                else if (ircMessage.Text.Contains("Successfully added case #") && ircMessage.Text.Contains(" to the deletion list."))
                {
                    CloseCase(ircMessage.UnixTimestamp);
                }

                else if (ircMessage.Text.Contains("Client of case #") && ircMessage.Text.Contains(" has been banned by the moderator team and the case has been trashed."))
                {
                    CloseCase(ircMessage.UnixTimestamp);
                }

                else if (ircMessage.Text.Contains("Rescue \"") && ircMessage.Text.Contains("\" has been re-opened and added to the board as case #"))
                {
                    UncloseCase();
                }

                else if (ircMessage.Text.Contains(": \"") && _IrcMessage.ElementAt(_IrcMessage.Count - 2).Text.Contains("!go"))
                {
                    List<string> go = new List<string>(ircMessage.Text.Split(new string[] { ": \"", "\", \"" }, StringSplitOptions.None));

                    for (int i = _AssignedRat.Count - 1, j = go.Count - 1; i != -1 && j != 0; --i, --j)
                    {
                        _AssignedRat.ElementAt(i).CmdrName = go.ElementAt(j).Trim('\"');
                        RefreshAssignedRat();
                    }
                }

                else if (ircMessage.Text.Contains("Unassigned ") && ircMessage.Text.Contains(" from case #"))
                {
                    string ratToUnassign = ircMessage.Text.Split(new string[] { "Unassigned " }, StringSplitOptions.None).ElementAt(1).Split(new string[] { " from case #" }, StringSplitOptions.None).ElementAt(0);

                    for (int i = 0; i != _AssignedRat.Count; ++i)
                    {
                        if (_AssignedRat.ElementAt(i).CmdrName == ratToUnassign)
                        {
                            _AssignedRat.RemoveAt(i);
                            RefreshAssignedRat();
                            break;
                        }
                    }
                }
            }
        }
    }
}
