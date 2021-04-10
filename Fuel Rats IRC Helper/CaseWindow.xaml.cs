/*
 *   CaseWindow.xaml.cs
 *   Fuel Rats IRC Helper
 *
 *   Created by Julius Zitzmann on 2020-05-08.
 *   Copyright © 2021 Julius Zitzmann. All rights reserved.
 *
 *   Feature requests, bug reports or questions?
 *   info@julius-zitzmann.de
 *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */


using System.Collections.Generic;
using System.Windows;

namespace Fuel_Rats_IRC_Helper
{
    /// <summary>
    /// Interaction logic for CaseWindow.xaml
    /// </summary>
    public partial class CaseWindow : Window
    {
        private string _CaseNumber;
        private string _IrcNick;
        private string _CmdrName;
        private string _System;
        private string _Platform;
        private string _O2;
        private string _Language;
        private List<IrcMessage> _IrcMessage;

        public CaseWindow(ref string caseNumber, ref string ircNick, ref string cmdrName, ref string system, ref string platform, ref string o2, ref string language, ref List<IrcMessage> ircMessage)
        {
            _CaseNumber = caseNumber;
            _IrcNick = ircNick;
            _CmdrName = cmdrName;
            _System = system;
            _Platform = platform;
            _O2 = o2;
            _Language = language;
            _IrcMessage = ircMessage;

            InitializeComponent();
        }

        public void RefreshCaseChat()
        {
            if (datagridCaseChat.IsLoaded)
            {
                datagridCaseChat.Items.Refresh();

                if (_IrcMessage.Count != 0)
                {
                    datagridCaseChat.ScrollIntoView(datagridCaseChat.Items[_IrcMessage.Count - 1]);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title = "Case #" + _CaseNumber;
        }

        private void datagridCaseChat_Loaded(object sender, RoutedEventArgs e)
        {
            datagridCaseChat.ItemsSource = _IrcMessage;

            if (_IrcMessage.Count != 0)
            {
                datagridCaseChat.ScrollIntoView(datagridCaseChat.Items[_IrcMessage.Count - 1]);
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
