/*
 *   ChangelogWindow.xaml.cs
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

namespace Fuel_Rats_IRC_Helper
{
    /// <summary>
    /// Interaction logic for ChangelogWindow.xaml
    /// </summary>
    public partial class ChangelogWindow : Window
    {
        public ChangelogWindow()
        {
            InitializeComponent();
        }

        private void checkboxDontShowAgain_Loaded(object sender, RoutedEventArgs e)
        {
            if (Settings.Get("showChangelogOnStartup") == "no")
            {
                checkboxDontShowAgain.IsChecked = true;
            }
        }

        private void checkboxDontShowAgain_Checked(object sender, RoutedEventArgs e)
        {
            Settings.Set("showChangelogOnStartup", "no");
        }

        private void checkboxDontShowAgain_Unchecked(object sender, RoutedEventArgs e)
        {
            Settings.Set("showChangelogOnStartup", "yes");
        }

        private void butonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
