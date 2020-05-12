/*
 *   Preferences.xaml.cs
 *   Fuel Rats IRC Helper
 *
 *   Created by Julius Zitzmann on 2020-05-08.
 *   Copyright © 2020 Julius Zitzmann. All rights reserved.
 *
 *   Feature requests, bug reports or questions?
 *   info@julius-zitzmann.de
 *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */


using System.Windows;

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

        private void textboxProcessNameIrcClient_Loaded(object sender, RoutedEventArgs e)
        {
            textboxProcessNameIrcClient.Text = _Settings.Get("processNameIrcClient");
        }

        private void textboxProcessNameEliteDangerous_Loaded(object sender, RoutedEventArgs e)
        {
            textboxProcessNameEliteDangerous.Text = _Settings.Get("processNameEliteDangerous");
            if (textboxProcessNameEliteDangerous.Text == "")
            {
                textboxProcessNameEliteDangerous.Text = "EliteDangerous64";
            }
        }

        private void textboxProcessNameIrcClient_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            _Settings.Set("processNameIrcClient", textboxProcessNameIrcClient.Text);
        }

        private void textboxProcessNameEliteDangerous_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            _Settings.Set("processNameEliteDangerous", textboxProcessNameEliteDangerous.Text);
        }
    }
}
