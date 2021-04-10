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

namespace Fuel_Rats_IRC_Helper
{
    class Case
    {
        private static int _NextId;
        private int _Id;
        private string _CaseNumber;
        private string _IrcNick;
        private string _CmdrName;
        private string _System;
        private string _Platform;
        private string _O2;
        private string _Language;
        private List<IrcMessage> _IrcMessage;
        private DateTimeOffset _StartTime;
        private DateTimeOffset _EndTime;
        private CaseWindow _CaseWindow;

        public Case(string caseNumber, string ircNick, string cmdrName, string system, string platform, string o2, string language, IrcMessage ircMessage)
        {
            _Id = _NextId++;
            _CaseNumber = caseNumber;
            _IrcNick = ircNick;
            _CmdrName = cmdrName;
            _System = system;
            _Platform = platform;
            _O2 = o2;
            _Language = language;
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

        public string IrcNick
        {
            get { return _IrcNick; }
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
                _CaseWindow = new CaseWindow(ref _CaseNumber, ref _IrcNick, ref _CmdrName, ref _System, ref _Platform, ref _O2, ref _Language, ref _IrcMessage);
                _CaseWindow.Show();
            }
        }

        public void CloseCaseWindow()
        {
            if (_CaseWindow != null && _CaseWindow.IsLoaded)
            {
                _CaseWindow.Close();
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
            }
        }
    }
}
