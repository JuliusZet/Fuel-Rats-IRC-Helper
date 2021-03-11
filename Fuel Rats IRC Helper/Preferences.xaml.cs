/*
 *   Preferences.xaml.cs
 *   Fuel Rats IRC Helper
 *
 *   Created by Julius Zitzmann on 2020-05-08.
 *   Copyright © 2021 Julius Zitzmann. All rights reserved.
 *
 *   Feature requests, bug reports or questions?
 *   info@julius-zitzmann.de
 *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */


using System.Windows;
using System.Windows.Controls;

namespace Fuel_Rats_IRC_Helper
{
    /// <summary>
    /// Interaction logic for Preferences.xaml
    /// </summary>
    public partial class Preferences : Window
    {
        private Settings _Settings;

        public Preferences()
        {
            _Settings = new Settings();
            InitializeComponent();
        }

        private void checkboxCheckForUpdatesOnStartup_Loaded(object sender, RoutedEventArgs e)
        {
            if (_Settings.Get("checkForUpdatesOnStartup") == "yes")
            {
                checkboxCheckForUpdatesOnStartup.IsChecked = true;
            }
        }

        private void checkboxCheckForUpdatesOnStartup_Checked(object sender, RoutedEventArgs e)
        {
            _Settings.Set("checkForUpdatesOnStartup", "yes");
        }

        private void checkboxCheckForUpdatesOnStartup_Unchecked(object sender, RoutedEventArgs e)
        {
            _Settings.Set("checkForUpdatesOnStartup", "no");
        }

        private void textboxWindowTitleIrcClient_Loaded(object sender, RoutedEventArgs e)
        {
            textboxWindowTitleIrcClient.Text = _Settings.Get("windowTitleIrcClient");
        }

        private void textboxWindowTitleIrcClient_TextChanged(object sender, TextChangedEventArgs e)
        {
            _Settings.Set("windowTitleIrcClient", textboxWindowTitleIrcClient.Text);
        }

        private void buttonWindowTitleIrcClientResetToDefault_Click(object sender, RoutedEventArgs e)
        {
            _Settings.ResetToDefault("windowTitleIrcClient");
            textboxWindowTitleIrcClient.Text = _Settings.Get("windowTitleIrcClient");
        }

        private void textboxWindowTitleEliteDangerous_Loaded(object sender, RoutedEventArgs e)
        {
            textboxWindowTitleEliteDangerous.Text = _Settings.Get("windowTitleEliteDangerous");
        }

        private void textboxWindowTitleEliteDangerous_TextChanged(object sender, TextChangedEventArgs e)
        {
            _Settings.Set("windowTitleEliteDangerous", textboxWindowTitleEliteDangerous.Text);
        }

        private void buttonWindowTitleEliteDangerousResetToDefault_Click(object sender, RoutedEventArgs e)
        {
            _Settings.ResetToDefault("windowTitleEliteDangerous");
            textboxWindowTitleEliteDangerous.Text = _Settings.Get("windowTitleEliteDangerous");
        }

        private void radiobuttonCopyPaste_Loaded(object sender, RoutedEventArgs e)
        {
            if (_Settings.Get("messageInsertionMode") == "copyPaste")
            {
                radiobuttonCopyPaste.IsChecked = true;
            }
        }

        private void radiobuttonCopyPaste_Checked(object sender, RoutedEventArgs e)
        {
            _Settings.Set("messageInsertionMode", "copyPaste");
        }

        private void radiobuttonSendkeys_Loaded(object sender, RoutedEventArgs e)
        {
            if (_Settings.Get("messageInsertionMode") == "sendKeys")
            {
                radiobuttonSendkeys.IsChecked = true;
            }
        }

        private void radiobuttonSendkeys_Checked(object sender, RoutedEventArgs e)
        {
            _Settings.Set("messageInsertionMode", "sendKeys");
        }
    }
}
