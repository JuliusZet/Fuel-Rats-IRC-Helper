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
        private string _ClientIrcNick;
        private string _ClientCmdrName;
        private string _ClientSystem;
        private string _ClientPlatform;
        private string _ClientO2;
        private string _ClientLanguage;
        private List<IrcMessage> _IrcMessage;

        public CaseWindow(string caseNumber, string clientIrcNick, string clientCmdrName, string clientSystem, string clientPlatform, string clientO2, string clientLanguage, List<IrcMessage> ircMessage)
        {
            _CaseNumber = caseNumber;
            _ClientIrcNick = clientIrcNick;
            _ClientCmdrName = clientCmdrName;
            _ClientSystem = clientSystem;
            _ClientPlatform = clientPlatform;
            _ClientO2 = clientO2;
            _ClientLanguage = clientLanguage;
            _IrcMessage = ircMessage;

            InitializeComponent();
        }

        public string ClientIrcNick
        {
            set
            {
                _ClientIrcNick = value;
                
                if (textboxClientIrcNick.IsLoaded)
                {
                    textboxClientIrcNick.Text = value;
                }
            }
        }

        public string ClientCmdrName
        {
            set
            {
                _ClientCmdrName = value;

                if (textboxClientCmdrName.IsLoaded)
                {
                    textboxClientCmdrName.Text = "CMDR " + value;
                }
            }
        }

        public string ClientSystem
        {
            set
            {
                _ClientSystem = value;

                if (textboxClientSystem.IsLoaded)
                {
                    textboxClientSystem.Text = value;
                }
            }
        }

        public string ClientPlatform
        {
            set
            {
                _ClientPlatform = value;

                if (textboxClientPlatform.IsLoaded)
                {
                    textboxClientPlatform.Text = value;
                }
            }
        }

        public string ClientO2
        {
            set
            {
                _ClientO2 = value;

                if (textboxClientO2.IsLoaded)
                {
                    textboxClientO2.Text = value;
                }
            }
        }

        public string ClientLanguage
        {
            set
            {
                _ClientLanguage = value;

                if (textboxClientLanguage.IsLoaded)
                {
                    textboxClientLanguage.Text = value;
                }
            }
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

        private void textboxClientIrcNick_Loaded(object sender, RoutedEventArgs e)
        {
            textboxClientIrcNick.Text = _ClientIrcNick;
        }

        private void textboxClientCmdrName_Loaded(object sender, RoutedEventArgs e)
        {
            textboxClientCmdrName.Text = "CMDR " + _ClientCmdrName;
        }

        private void textboxClientSystem_Loaded(object sender, RoutedEventArgs e)
        {
            textboxClientSystem.Text = _ClientSystem;
        }

        private void textboxClientPlatform_Loaded(object sender, RoutedEventArgs e)
        {
            textboxClientPlatform.Text = _ClientPlatform;
        }

        private void textboxClientO2_Loaded(object sender, RoutedEventArgs e)
        {
            textboxClientO2.Text = _ClientO2;
        }

        private void textboxClientLanguage_Loaded(object sender, RoutedEventArgs e)
        {
            textboxClientLanguage.Text = _ClientLanguage;
        }
    }
}
