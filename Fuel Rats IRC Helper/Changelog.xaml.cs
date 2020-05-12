/*
 *   Changelog.xaml.cs
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
    /// Interaction logic for Changelog.xaml
    /// </summary>
    public partial class Changelog : Window
    {
        private Settings _Settings;

        public Changelog()
        {
            _Settings = new Settings();
            InitializeComponent();
        }

        private void checkboxDontShowAgain_Loaded(object sender, RoutedEventArgs e)
        {
            if (_Settings.Get("showChangelogOnStartup") == "no")
            {
                checkboxDontShowAgain.IsChecked = true;
            }
        }

        private void checkboxDontShowAgain_Checked(object sender, RoutedEventArgs e)
        {
            _Settings.Set("showChangelogOnStartup", "no");
        }

        private void checkboxDontShowAgain_Unchecked(object sender, RoutedEventArgs e)
        {
            _Settings.Set("showChangelogOnStartup", "yes");
        }

        private void butonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
