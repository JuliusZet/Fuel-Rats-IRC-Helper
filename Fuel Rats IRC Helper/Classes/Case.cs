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

namespace Fuel_Rats_IRC_Helper
{
    class Case
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
        private List<IrcMessage> _IrcMessage;
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
            _IrcMessage = new List<IrcMessage>();
            _StartTime = DateTimeOffset.FromUnixTimeSeconds(ircMessage.UnixTimestamp);
            _EndTime = DateTimeOffset.MaxValue;
            _CaseWindow = null;

            _IrcMessage.Add(ircMessage);
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
        }

        public bool IsClosed()
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

        public void ShowCaseWindow()
        {
            if (_CaseWindow == null || !_CaseWindow.IsLoaded)
            {
                _CaseWindow = new CaseWindow(_CaseNumber, _ClientIrcNick, _ClientCmdrName, _ClientSystem, _ClientPlatform, _ClientO2, _ClientLanguage, _IrcMessage);
                _CaseWindow.Show();
            }
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

        public void CloseCase(long unixTimestamp)
        {
            _EndTime = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp);
        }

        public void AddMessage(IrcMessage ircMessage)
        {
            _IrcMessage.Add(ircMessage);

            if (_CaseWindow != null && _CaseWindow.IsLoaded)
            {
                _CaseWindow.RefreshCaseChat();
            }

            if (ircMessage.SenderNickname == "MechaSqueak[BOT]")
            {
                if (ircMessage.Text.StartsWith("Successfully closed case #"))
                {
                    CloseCase(ircMessage.UnixTimestamp);
                }

                else if (ircMessage.Text.StartsWith("Caution: Client of case #") && ircMessage.Text.Contains(" has changed IRC nick to "))
                {
                    _ClientIrcNick = ircMessage.Text.Split(new string[] { " has changed IRC nick to " }, StringSplitOptions.None).ElementAt(1);

                    if (_CaseWindow != null && _CaseWindow.IsLoaded)
                    {
                        _CaseWindow.ClientIrcNick = _ClientIrcNick;
                    }
                }

                else if (ircMessage.Text.StartsWith("Client name for case #") && ircMessage.Text.Contains(" has been changed to: "))
                {
                    _ClientCmdrName = ircMessage.Text.Split(new string[] { " has been changed to: " }, StringSplitOptions.None).ElementAt(1);
                    _ClientCmdrName = _ClientCmdrName.Remove(_ClientCmdrName.Length - 1);

                    if (_CaseWindow != null && _CaseWindow.IsLoaded)
                    {
                        _CaseWindow.ClientCmdrName = _ClientCmdrName;
                    }
                }

                else if (ircMessage.Text.StartsWith("System for case #") && ircMessage.Text.Contains(" has been changed to "))
                {
                    _ClientSystem = ircMessage.Text.Split(new string[] { " has been changed to " }, StringSplitOptions.None).ElementAt(1);

                    if (_CaseWindow != null && _CaseWindow.IsLoaded)
                    {
                        _CaseWindow.ClientSystem = _ClientSystem;
                    }
                }

                else if (ircMessage.Text.StartsWith("ATTENTION: System for case #") && ircMessage.Text.Contains(" has been automatically corrected to "))
                {
                    _ClientSystem = ircMessage.Text.Split(new string[] { " has been automatically corrected to " }, StringSplitOptions.None).ElementAt(1);
                    _ClientSystem = _ClientSystem.Remove(_ClientSystem.Length - 1);

                    if (_CaseWindow != null && _CaseWindow.IsLoaded)
                    {
                        _CaseWindow.ClientSystem = _ClientSystem;
                    }
                }

                else if (ircMessage.Text.StartsWith("Platform for case #") && ircMessage.Text.Contains(" set to: "))
                {
                    _ClientPlatform = ircMessage.Text.Split(new string[] { " set to: " }, StringSplitOptions.None).ElementAt(1);

                    if (_ClientPlatform.Contains("PC"))
                    {
                        _ClientPlatform = "PC";
                    }
                    else if (_ClientPlatform.Contains("Xbox"))
                    {
                        _ClientPlatform = "Xbox";
                    }
                    else if (_ClientPlatform.Contains("Playstation"))
                    {
                        _ClientPlatform = "Playstation";
                    }

                    if (_CaseWindow != null && _CaseWindow.IsLoaded)
                    {
                        _CaseWindow.ClientPlatform = _ClientPlatform;
                    }
                }

                else if (ircMessage.Text.StartsWith("CODE RED! ") && ircMessage.Text.EndsWith(" is on emergency oxygen!"))
                {
                    _ClientO2 = "NOT OK";

                    if (_CaseWindow != null && _CaseWindow.IsLoaded)
                    {
                        _CaseWindow.ClientO2 = _ClientO2;
                    }
                }

                else if (ircMessage.Text.StartsWith("Case #") && ircMessage.Text.EndsWith(" is no longer CR."))
                {
                    _ClientO2 = "OK";

                    if (_CaseWindow != null && _CaseWindow.IsLoaded)
                    {
                        _CaseWindow.ClientO2 = _ClientO2;
                    }
                }

                else if (ircMessage.Text.StartsWith("Language for case #") && ircMessage.Text.Contains(" has now been changed to "))
                {
                    _ClientLanguage = ircMessage.Text.Split(new string[] { " has now been changed to " }, StringSplitOptions.None).ElementAt(1);

                    if (_CaseWindow != null && _CaseWindow.IsLoaded)
                    {
                        _CaseWindow.ClientLanguage = _ClientLanguage;
                    }
                }
            }
        }
    }
}
