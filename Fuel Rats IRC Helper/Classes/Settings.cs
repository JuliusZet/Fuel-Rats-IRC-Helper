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
    class Settings
    {
        private ExeConfigurationFileMap _SettingsExeConfigurationFileMap;
        private Configuration _SettingsConfiguration;
        private KeyValueConfigurationCollection _SettingsKeyValueConfigurationCollection;

        private Configuration _AppConfiguration;
        private KeyValueConfigurationCollection _AppKeyValueConfigurationCollection;

        public Settings()
        {
            _SettingsExeConfigurationFileMap = new ExeConfigurationFileMap();
            _SettingsExeConfigurationFileMap.ExeConfigFilename = "../Settings.config";
            _SettingsConfiguration = ConfigurationManager.OpenMappedExeConfiguration(_SettingsExeConfigurationFileMap, ConfigurationUserLevel.None);
            _SettingsKeyValueConfigurationCollection = _SettingsConfiguration.AppSettings.Settings;

            _AppConfiguration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            _AppKeyValueConfigurationCollection = _AppConfiguration.AppSettings.Settings;
        }

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
        public string Get(string key)
        {
            if (key != "showChangelogOnStartup")
            {
                _SettingsConfiguration = ConfigurationManager.OpenMappedExeConfiguration(_SettingsExeConfigurationFileMap, ConfigurationUserLevel.None);
                _SettingsKeyValueConfigurationCollection = _SettingsConfiguration.AppSettings.Settings;

                if (_SettingsKeyValueConfigurationCollection[key] != null)
                {
                    return _SettingsKeyValueConfigurationCollection[key].Value;
                }

                else
                {
                    return "";
                }
            }

            else
            {
                _AppConfiguration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                _AppKeyValueConfigurationCollection = _AppConfiguration.AppSettings.Settings;

                if (_AppKeyValueConfigurationCollection[key] != null)
                {
                    return _AppKeyValueConfigurationCollection[key].Value;
                }

                else
                {
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
        public int Set(string key, string value)
        {
            try
            {
                if (key != "showChangelogOnStartup")
                {
                    if (_SettingsKeyValueConfigurationCollection[key] == null)
                    {
                        _SettingsKeyValueConfigurationCollection.Add(key, value);
                    }

                    else
                    {
                        _SettingsKeyValueConfigurationCollection[key].Value = value;
                    }

                    _SettingsConfiguration.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection(_SettingsConfiguration.AppSettings.SectionInformation.Name);
                    return 0;
                }

                else
                {
                    if (_AppKeyValueConfigurationCollection[key] == null)
                    {
                        _AppKeyValueConfigurationCollection.Add(key, value);
                    }

                    else
                    {
                        _AppKeyValueConfigurationCollection[key].Value = value;
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
    }
}
