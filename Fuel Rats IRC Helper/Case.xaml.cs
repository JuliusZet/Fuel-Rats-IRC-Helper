/*
 *   Case.xaml.cs
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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Fuel_Rats_IRC_Helper
{
    /// <summary>
    /// Interaction logic for Case.xaml
    /// </summary>
    public partial class Case : Window
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
        private List<IrcMessage> _Message;
        private DateTimeOffset _StartTime;
        private DateTimeOffset _EndTime;
        private bool _CanBeDeleted;

        public Case(string caseNumber, string ircNick, string cmdrName, string system, string platform, string o2, string language, IrcMessage message)
        {
            _Id = _NextId++;
            _CaseNumber = caseNumber;
            _IrcNick = ircNick;
            _CmdrName = cmdrName;
            _System = system;
            _Platform = platform;
            _O2 = o2;
            _Language = language;
            _Message = new List<IrcMessage>();
            _StartTime = DateTimeOffset.FromUnixTimeSeconds(message.UnixTimestamp);
            _EndTime = DateTimeOffset.MaxValue;
            _CanBeDeleted = false;

            _Message.Add(message);

            InitializeComponent();
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

        public void CloseCase(long unixTimestamp)
        {
            _EndTime = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp);
        }

        public void AddMessage(IrcMessage message)
        {
            _Message.Add(message);

            if (datagridCaseChat.IsLoaded)
            {
                datagridCaseChat.Items.Refresh();

                if (_Message.Count != 0)
                {
                    datagridCaseChat.ScrollIntoView(datagridCaseChat.Items[_Message.Count - 1]);
                }
            }

            if (message.SenderNickname == "MechaSqueak[BOT]")
            {
                if (message.Text.StartsWith("Successfully closed case #"))
                {
                    CloseCase(message.UnixTimestamp);
                }
            }
        }

        public void DeleteCase()
        {
            _CanBeDeleted = true;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title = "Case #" + _CaseNumber;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!_CanBeDeleted)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void datagridCaseChat_Loaded(object sender, RoutedEventArgs e)
        {
            datagridCaseChat.ItemsSource = _Message;

            if (_Message.Count != 0)
            {
                datagridCaseChat.ScrollIntoView(datagridCaseChat.Items[_Message.Count - 1]);
            }
        }

        private void textboxCasenumber_Loaded(object sender, RoutedEventArgs e)
        {
            textboxCasenumber.Text = '#' + _CaseNumber;
        }

        private void textboxClientsIrcNickname_Loaded(object sender, RoutedEventArgs e)
        {
            textboxClientsIrcNickname.Text = _IrcNick;
        }

        private void textboxClientsCmdrName_Loaded(object sender, RoutedEventArgs e)
        {
            textboxClientsCmdrName.Text = "CMDR " + _CmdrName;
        }

        private void textboxSystem_Loaded(object sender, RoutedEventArgs e)
        {
            textboxSystem.Text = _System;
        }

        private void textboxPlatform_Loaded(object sender, RoutedEventArgs e)
        {
            textboxPlatform.Text = _Platform;
        }

        private void textboxO2_Loaded(object sender, RoutedEventArgs e)
        {
            textboxO2.Text = _O2;
        }

        private void textboxLanguage_Loaded(object sender, RoutedEventArgs e)
        {
            textboxLanguage.Text = _Language;
        }
    }
}
