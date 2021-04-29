/*
 *   SettingsWindow.xaml.cs
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
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void checkboxCheckForUpdatesOnStartup_Loaded(object sender, RoutedEventArgs e)
        {
            if (Settings.Get("checkForUpdatesOnStartup") == "yes")
            {
                checkboxCheckForUpdatesOnStartup.IsChecked = true;
            }
        }

        private void checkboxCheckForUpdatesOnStartup_Checked(object sender, RoutedEventArgs e)
        {
            Settings.Set("checkForUpdatesOnStartup", "yes");
        }

        private void checkboxCheckForUpdatesOnStartup_Unchecked(object sender, RoutedEventArgs e)
        {
            Settings.Set("checkForUpdatesOnStartup", "no");
        }

        private void textboxWindowTitleIrcClient_Loaded(object sender, RoutedEventArgs e)
        {
            textboxWindowTitleIrcClient.Text = Settings.Get("windowTitleIrcClient");
        }

        private void textboxWindowTitleIrcClient_TextChanged(object sender, TextChangedEventArgs e)
        {
            Settings.Set("windowTitleIrcClient", textboxWindowTitleIrcClient.Text);
        }

        private void buttonWindowTitleIrcClientResetToDefault_Click(object sender, RoutedEventArgs e)
        {
            Settings.ResetToDefault("windowTitleIrcClient");
            textboxWindowTitleIrcClient.Text = Settings.Get("windowTitleIrcClient");
        }

        private void textboxWindowTitleEliteDangerous_Loaded(object sender, RoutedEventArgs e)
        {
            textboxWindowTitleEliteDangerous.Text = Settings.Get("windowTitleEliteDangerous");
        }

        private void textboxWindowTitleEliteDangerous_TextChanged(object sender, TextChangedEventArgs e)
        {
            Settings.Set("windowTitleEliteDangerous", textboxWindowTitleEliteDangerous.Text);
        }

        private void buttonWindowTitleEliteDangerousResetToDefault_Click(object sender, RoutedEventArgs e)
        {
            Settings.ResetToDefault("windowTitleEliteDangerous");
            textboxWindowTitleEliteDangerous.Text = Settings.Get("windowTitleEliteDangerous");
        }

        private void radiobuttonCopyPaste_Loaded(object sender, RoutedEventArgs e)
        {
            if (Settings.Get("messageInsertionMode") == "copyPaste")
            {
                radiobuttonCopyPaste.IsChecked = true;
            }
        }

        private void radiobuttonCopyPaste_Checked(object sender, RoutedEventArgs e)
        {
            Settings.Set("messageInsertionMode", "copyPaste");
        }

        private void radiobuttonSendkeys_Loaded(object sender, RoutedEventArgs e)
        {
            if (Settings.Get("messageInsertionMode") == "sendKeys")
            {
                radiobuttonSendkeys.IsChecked = true;
            }
        }

        private void radiobuttonSendkeys_Checked(object sender, RoutedEventArgs e)
        {
            Settings.Set("messageInsertionMode", "sendKeys");
        }

        private void checkboxIrcUseSsl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Settings.Get("ircUseSsl") == "yes")
            {
                checkboxIrcUseSsl.IsChecked = true;
            }
        }

        private void checkboxIrcUseSsl_Checked(object sender, RoutedEventArgs e)
        {
            Settings.Set("ircUseSsl", "yes");
        }

        private void checkboxIrcUseSsl_Unchecked(object sender, RoutedEventArgs e)
        {
            Settings.Set("ircUseSsl", "no");
        }

        private void textboxIrcAddress_Loaded(object sender, RoutedEventArgs e)
        {
            textboxIrcAddress.Text = Settings.Get("ircAddress");
        }

        private void textboxIrcAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            Settings.Set("ircAddress", textboxIrcAddress.Text);
        }

        private void buttonIrcAddressResetToDefault_Click(object sender, RoutedEventArgs e)
        {
            Settings.ResetToDefault("ircAddress");
            textboxIrcAddress.Text = Settings.Get("ircAddress");
        }

        private void textboxIrcPort_Loaded(object sender, RoutedEventArgs e)
        {
            textboxIrcPort.Text = Settings.Get("ircPort");
        }

        private void textboxIrcPort_TextChanged(object sender, TextChangedEventArgs e)
        {
            Settings.Set("ircPort", textboxIrcPort.Text);
        }

        private void buttonIrcPortResetToDefault_Click(object sender, RoutedEventArgs e)
        {
            Settings.ResetToDefault("ircPort");
            textboxIrcPort.Text = Settings.Get("ircPort");
        }

        private void textboxIrcNick_Loaded(object sender, RoutedEventArgs e)
        {
            textboxIrcNick.Text = Settings.Get("ircNick");
        }

        private void textboxIrcNick_TextChanged(object sender, TextChangedEventArgs e)
        {
            Settings.Set("ircNick", textboxIrcNick.Text);
        }

        private void buttonIrcNickResetToDefault_Click(object sender, RoutedEventArgs e)
        {
            Settings.ResetToDefault("ircNick");
            textboxIrcNick.Text = Settings.Get("ircNick");
        }

        private void textboxIrcRealname_Loaded(object sender, RoutedEventArgs e)
        {
            textboxIrcRealname.Text = Settings.Get("ircRealname");
        }

        private void textboxIrcRealname_TextChanged(object sender, TextChangedEventArgs e)
        {
            Settings.Set("ircRealname", textboxIrcRealname.Text);
        }

        private void buttonIrcRealnameResetToDefault_Click(object sender, RoutedEventArgs e)
        {
            Settings.ResetToDefault("ircRealname");
            textboxIrcRealname.Text = Settings.Get("ircRealname");
        }

        private void passwordboxIrcPassword_Loaded(object sender, RoutedEventArgs e)
        {
            passwordboxIrcPassword.Password = Settings.Get("ircPassword");
        }

        private void passwordboxIrcPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Settings.Set("ircPassword", passwordboxIrcPassword.Password);
        }

        private void buttonIrcPasswordResetToDefault_Click(object sender, RoutedEventArgs e)
        {
            Settings.ResetToDefault("ircPassword");
            passwordboxIrcPassword.Password = Settings.Get("ircPassword");
        }

        private void textboxTimestampFormat_Loaded(object sender, RoutedEventArgs e)
        {
            textboxTimestampFormat.Text = Settings.Get("timestampFormat");
        }

        private void textboxTimestampFormat_TextChanged(object sender, TextChangedEventArgs e)
        {
            Settings.Set("timestampFormat", textboxTimestampFormat.Text);

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
            Settings.ResetToDefault("timestampFormat");
            textboxTimestampFormat.Text = Settings.Get("timestampFormat");
        }

        private void checkboxIrcAutoconnect_Loaded(object sender, RoutedEventArgs e)
        {
            if (Settings.Get("ircAutoconnect") == "yes")
            {
                checkboxIrcAutoconnect.IsChecked = true;
            }
        }

        private void checkboxIrcAutoconnect_Checked(object sender, RoutedEventArgs e)
        {
            Settings.Set("ircAutoconnect", "yes");
        }

        private void checkboxIrcAutoconnect_Unchecked(object sender, RoutedEventArgs e)
        {
            Settings.Set("ircAutoconnect", "no");
        }

        private void checkboxAutoCopySystemOnPcSignal_Loaded(object sender, RoutedEventArgs e)
        {
            if (Settings.Get("autoCopySystemOnPcSignal") == "yes")
            {
                checkboxAutoCopySystemOnPcSignal.IsChecked = true;
            }
        }

        private void checkboxAutoCopySystemOnPcSignal_Checked(object sender, RoutedEventArgs e)
        {
            Settings.Set("autoCopySystemOnPcSignal", "yes");
        }

        private void checkboxAutoCopySystemOnPcSignal_Unchecked(object sender, RoutedEventArgs e)
        {
            Settings.Set("autoCopySystemOnPcSignal", "no");
        }

        private void textboxRescueChannel_Loaded(object sender, RoutedEventArgs e)
        {
            textboxRescueChannel.Text = Settings.Get("ircChannelRescue");
        }

        private void textboxRescueChannel_TextChanged(object sender, TextChangedEventArgs e)
        {
            Settings.Set("ircChannelRescue", textboxRescueChannel.Text);

            if (labelCaseTracker1.IsLoaded)
            {
                labelCaseTracker1.Content = "All messages in \"" + textboxRescueChannel.Text + "\" (rescue channel) are processed. Additionally,";
            }
        }

        private void buttonRescueChannelResetToDefault_Click(object sender, RoutedEventArgs e)
        {
            Settings.ResetToDefault("ircChannelRescue");
            textboxRescueChannel.Text = Settings.Get("ircChannelRescue");
        }

        private void textboxChatChannel_Loaded(object sender, RoutedEventArgs e)
        {
            textboxChatChannel.Text = Settings.Get("ircChannelChat");
        }

        private void textboxChatChannel_TextChanged(object sender, TextChangedEventArgs e)
        {
            Settings.Set("ircChannelChat", textboxChatChannel.Text);

            if (labelCaseTracker2.IsLoaded && textboxIrcNickBot.IsLoaded)
            {
                labelCaseTracker2.Content = "messages in \"" + textboxChatChannel.Text + "\" (chat channel) are processed, when they are sent by the bot named \"" + textboxIrcNickBot.Text + "\".";
            }
        }

        private void buttonChatChannelResetToDefault_Click(object sender, RoutedEventArgs e)
        {
            Settings.ResetToDefault("ircChannelChat");
            textboxChatChannel.Text = Settings.Get("ircChannelChat");
        }

        private void textboxIrcNickBot_Loaded(object sender, RoutedEventArgs e)
        {
            textboxIrcNickBot.Text = Settings.Get("ircNickBot");
        }

        private void textboxIrcNickBot_TextChanged(object sender, TextChangedEventArgs e)
        {
            Settings.Set("ircNickBot", textboxIrcNickBot.Text);

            if (labelCaseTracker2.IsLoaded && textboxChatChannel.IsLoaded)
            {
                labelCaseTracker2.Content = "messages in \"" + textboxChatChannel.Text + "\" (chat channel) are processed, when they are sent by the bot named \"" + textboxIrcNickBot.Text + "\".";
            }
        }

        private void buttonIrcNickBotResetToDefault_Click(object sender, RoutedEventArgs e)
        {
            Settings.ResetToDefault("ircNickBot");
            textboxIrcNickBot.Text = Settings.Get("ircNickBot");
        }

        private void textboxRatsignalStartsWith_Loaded(object sender, RoutedEventArgs e)
        {
            textboxRatsignalStartsWith.Text = Settings.Get("ratsignalStartsWith");
        }

        private void textboxRatsignalStartsWith_TextChanged(object sender, TextChangedEventArgs e)
        {
            Settings.Set("ratsignalStartsWith", textboxRatsignalStartsWith.Text);

            if (labelCaseTracker3.IsLoaded)
            {
                labelCaseTracker3.Content = "When a message comes in, that starts with  \"" + textboxRatsignalStartsWith.Text + "\", then a new case will be added to the case list.";
            }
        }

        private void textboxRatsignalStartsWithResetToDefault_Click(object sender, RoutedEventArgs e)
        {
            Settings.ResetToDefault("ratsignalStartsWith");
            textboxRatsignalStartsWith.Text = Settings.Get("ratsignalStartsWith");
        }
    }
}
