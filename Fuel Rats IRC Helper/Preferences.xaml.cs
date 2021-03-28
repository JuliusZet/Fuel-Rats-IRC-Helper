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


using System;
using System.Diagnostics;
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

        private void textboxIrcAddress_Loaded(object sender, RoutedEventArgs e)
        {
            textboxIrcAddress.Text = _Settings.Get("ircAddress");
        }

        private void textboxIrcAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            _Settings.Set("ircAddress", textboxIrcAddress.Text);
        }

        private void buttonIrcAddressResetToDefault_Click(object sender, RoutedEventArgs e)
        {
            _Settings.ResetToDefault("ircAddress");
            textboxIrcAddress.Text = _Settings.Get("ircAddress");
        }

        private void textboxIrcPort_Loaded(object sender, RoutedEventArgs e)
        {
            textboxIrcPort.Text = _Settings.Get("ircPort");
        }

        private void textboxIrcPort_TextChanged(object sender, TextChangedEventArgs e)
        {
            _Settings.Set("ircPort", textboxIrcPort.Text);
        }

        private void buttonIrcPortResetToDefault_Click(object sender, RoutedEventArgs e)
        {
            _Settings.ResetToDefault("ircPort");
            textboxIrcPort.Text = _Settings.Get("ircPort");
        }

        private void textboxIrcNick_Loaded(object sender, RoutedEventArgs e)
        {
            textboxIrcNick.Text = _Settings.Get("ircNick");
        }

        private void textboxIrcNick_TextChanged(object sender, TextChangedEventArgs e)
        {
            _Settings.Set("ircNick", textboxIrcNick.Text);
        }

        private void buttonIrcNickResetToDefault_Click(object sender, RoutedEventArgs e)
        {
            _Settings.ResetToDefault("ircNick");
            textboxIrcNick.Text = _Settings.Get("ircNick");
        }

        private void textboxIrcRealname_Loaded(object sender, RoutedEventArgs e)
        {
            textboxIrcRealname.Text = _Settings.Get("ircRealname");
        }

        private void textboxIrcRealname_TextChanged(object sender, TextChangedEventArgs e)
        {
            _Settings.Set("ircRealname", textboxIrcRealname.Text);
        }

        private void buttonIrcRealnameResetToDefault_Click(object sender, RoutedEventArgs e)
        {
            _Settings.ResetToDefault("ircRealname");
            textboxIrcRealname.Text = _Settings.Get("ircRealname");
        }

        private void passwordboxIrcPassword_Loaded(object sender, RoutedEventArgs e)
        {
            passwordboxIrcPassword.Password = _Settings.Get("ircPassword");
        }

        private void passwordboxIrcPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _Settings.Set("ircPassword", passwordboxIrcPassword.Password);
        }

        private void buttonIrcPasswordResetToDefault_Click(object sender, RoutedEventArgs e)
        {
            _Settings.ResetToDefault("ircPassword");
            passwordboxIrcPassword.Password = _Settings.Get("ircPassword");
        }

        private void textboxIrcChannel_Loaded(object sender, RoutedEventArgs e)
        {
            textboxIrcChannel.Text = _Settings.Get("ircChannel");
        }

        private void textboxIrcChannel_TextChanged(object sender, TextChangedEventArgs e)
        {
            _Settings.Set("ircChannel", textboxIrcChannel.Text);
        }

        private void buttonIrcChannelResetToDefault_Click(object sender, RoutedEventArgs e)
        {
            _Settings.ResetToDefault("ircChannel");
            textboxIrcChannel.Text = _Settings.Get("ircChannel");
        }

        private void textboxTimestampFormat_Loaded(object sender, RoutedEventArgs e)
        {
            textboxTimestampFormat.Text = _Settings.Get("timestampFormat");
        }

        private void textboxTimestampFormat_TextChanged(object sender, TextChangedEventArgs e)
        {
            _Settings.Set("timestampFormat", textboxTimestampFormat.Text);

            if (labelTimestampFormatExample.IsLoaded)
            {
                try
                {
                    labelTimestampFormatExample.Content = "Example: 2009-06-01T13:45:30Z will be displayed as " + DateTimeOffset.FromUnixTimeSeconds(1243863930).ToString(textboxTimestampFormat.Text);
                }
                catch (Exception)
                {
                    labelTimestampFormatExample.Content = "Invalid timestamp format";
                }
            }
        }

        private void buttonTimestampFormatHelp_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://docs.microsoft.com/de-de/dotnet/standard/base-types/custom-date-and-time-format-strings#code-try-1:~:text=%22d%22");
        }

        private void buttonTimestampFormatResetToDefault_Click(object sender, RoutedEventArgs e)
        {
            _Settings.ResetToDefault("timestampFormat");
            textboxTimestampFormat.Text = _Settings.Get("timestampFormat");
        }
    }
}
