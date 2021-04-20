/*
 *   Settings.cs
 *   Fuel Rats IRC Helper
 *
 *   Created by Julius Zitzmann on 2020-05-08.
 *   Copyright © 2021 Julius Zitzmann. All rights reserved.
 *
 *   Feature requests, bug reports or questions?
 *   info@julius-zitzmann.de
 *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */


using System.Configuration;
using System.Windows;

namespace Fuel_Rats_IRC_Helper
{
    static class Settings
    {
        private static ExeConfigurationFileMap _SettingsExeConfigurationFileMap = new ExeConfigurationFileMap()
        {
            ExeConfigFilename = "../Settings.config"
        };
        private static Configuration _SettingsConfiguration = ConfigurationManager.OpenMappedExeConfiguration(_SettingsExeConfigurationFileMap, ConfigurationUserLevel.None);
        private static KeyValueConfigurationCollection _SettingsKeyValueConfigurationCollection = _SettingsConfiguration.AppSettings.Settings;

        private static Configuration _AppConfiguration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        private static KeyValueConfigurationCollection _AppKeyValueConfigurationCollection = _AppConfiguration.AppSettings.Settings;

        //
        // Summary:
        //   Gets the value of the specified key
        //
        // Parameters:
        //   key:
        //     key to the value that should be returned
        //   
        // Returns:
        //   Value of the specified key
        public static string Get(string key)
        {
            if (key != "showChangelogOnStartup")
            {
                if (_SettingsKeyValueConfigurationCollection[key] == null)
                {
                    ResetToDefault(key);
                }

                if (_SettingsKeyValueConfigurationCollection[key] != null)
                {
                    return _SettingsKeyValueConfigurationCollection[key].Value;
                }

                else
                {
                    MessageBox.Show("Could not find \"" + key + "\" in the configuration file and there is also no default value available.", "Error");
                    return "";
                }
            }

            else
            {
                if (_AppKeyValueConfigurationCollection[key] == null)
                {
                    ResetToDefault(key);
                }

                if (_AppKeyValueConfigurationCollection[key] != null)
                {
                    return _AppKeyValueConfigurationCollection[key].Value;
                }

                else
                {
                    MessageBox.Show("Could not find \"" + key + "\" in the configuration file and there is also no default value available.", "Error");
                    return "";
                }
            }
        }

        //
        // Summary:
        //   Adds a new key-value combination to the settings or replaces the value of an already existing key
        //
        // Parameters:
        //   key:
        //     key that should be added or key to the value that should be replaced
        //
        //   value:
        //     value of the specified key
        //
        // Returns:
        //   Error code:
        //     0: Everything went ok
        //     1: There was an error writing to the configuration file
        public static int Set(string key, string value)
        {
            try
            {
                if (key != "showChangelogOnStartup")
                {
                    if (_SettingsKeyValueConfigurationCollection[key] != null)
                    {
                        _SettingsKeyValueConfigurationCollection[key].Value = value;
                    }

                    else
                    {
                        _SettingsKeyValueConfigurationCollection.Add(key, value);
                    }

                    _SettingsConfiguration.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection(_SettingsConfiguration.AppSettings.SectionInformation.Name);
                    return 0;
                }

                else
                {
                    if (_AppKeyValueConfigurationCollection[key] != null)
                    {
                        _AppKeyValueConfigurationCollection[key].Value = value;
                    }

                    else
                    {
                        _AppKeyValueConfigurationCollection.Add(key, value);
                    }

                    _AppConfiguration.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection(_AppConfiguration.AppSettings.SectionInformation.Name);
                    return 0;
                }
            }

            catch (ConfigurationErrorsException)
            {
                MessageBox.Show("Could not write to the configuration file.", "Error");
                return 1;
            }
        }

        //
        // Summary:
        //   Resets the value of an existing or non-existing key to the default value
        //
        // Parameters:
        //   key:
        //     key that should be added or key to the value that should be reset to its default
        //
        // Returns:
        //   Error code:
        //     0: Everything went ok
        //     1: There was an error writing to the configuration file
        //     2: There is no default value for this key
        public static int ResetToDefault(string key)
        {
            switch (key)
            {
                case "autoCopySystemOnPcSignal":
                    return Set("autoCopySystemOnPcSignal", "no");
                case "checkForUpdatesOnStartup":
                    return Set("checkForUpdatesOnStartup", "yes");
                case "ircAddress":
                    return Set("ircAddress", "localhost");
                case "ircAutoconnect":
                    return Set("ircAutoconnect", "no");
                case "ircChannel":
                    return Set("ircChannel", "#fuelrats");
                case "ircNick":
                    return Set("ircNick", "myNickname");
                case "ircPassword":
                    return Set("ircPassword", "myPassword");
                case "ircPort":
                    return Set("ircPort", "6667");
                case "ircRealname":
                    return Set("ircRealname", "myRealname");
                case "ircUseSsl":
                    return Set("ircUseSsl", "no");
                case "messageInsertionMode":
                    return Set("messageInsertionMode", "copyPaste");
                case "showChangelogOnStartup":
                    return Set("showChangelogOnStartup", "yes");
                case "timestampFormat":
                    return Set("timestampFormat", "HH:mm:ss");
                case "windowTitleEliteDangerous":
                    return Set("windowTitleEliteDangerous", "Elite - Dangerous (CLIENT)");
                case "windowTitleIrcClient":
                    return Set("windowTitleIrcClient", "#fuelrats");
                default:
                    return 2;
            }
        }
    }
}
