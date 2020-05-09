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


using System.Configuration;
using System.Windows;

namespace Fuel_Rats_IRC_Helper
{
    /// <summary>
    /// Interaction logic for Changelog.xaml
    /// </summary>
    public partial class Changelog : Window
    {
        public Changelog()
        {
            InitializeComponent();
        }

        private void checkboxDontShowAgain_Loaded(object sender, RoutedEventArgs e)
        {
            if (ConfigurationManager.AppSettings["showChangelogOnStartup"] == "no")
            {
                checkboxDontShowAgain.IsChecked = true;
            }
        }

        private void checkboxDontShowAgain_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                KeyValueConfigurationCollection settings = configuration.AppSettings.Settings;

                if (settings["showChangelogOnStartup"] == null)
                {
                    settings.Add("showChangelogOnStartup", "no");
                }

                else
                {
                    settings["showChangelogOnStartup"].Value = "no";
                }

                configuration.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configuration.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                MessageBox.Show("Could not write to the config file.", "Error");
            }
        }

        private void checkboxDontShowAgain_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                KeyValueConfigurationCollection settings = configuration.AppSettings.Settings;

                if (settings["showChangelogOnStartup"] == null)
                {
                    settings.Add("showChangelogOnStartup", "yes");
                }

                else
                {
                    settings["showChangelogOnStartup"].Value = "yes";
                }

                configuration.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configuration.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                MessageBox.Show("Could not write to the config file.", "Error");
            }
        }

        private void butonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
