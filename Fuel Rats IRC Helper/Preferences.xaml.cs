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


using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        private Process[] _Process;
        private List<string> _ForegroundProcessName;

        public Preferences()
        {
            _Settings = new Settings();
            refreshForegroundProcessNameList();
            InitializeComponent();
        }

        private void refreshForegroundProcessNameList()
        {
            _Process = Process.GetProcesses();
            _ForegroundProcessName = new List<string>();
            _ForegroundProcessName.Add("");

            for (int i = 0; i < _Process.Length; ++i)
            {
                if (_Process.ElementAt(i).MainWindowTitle != "")
                {
                    _ForegroundProcessName.Add(_Process.ElementAt(i).ProcessName);
                }
            }

            if (_Settings.Get("processNameIrcClient") != "" && !_ForegroundProcessName.Contains(_Settings.Get("processNameIrcClient")))
            {
                _ForegroundProcessName.Add(_Settings.Get("processNameIrcClient"));
            }

            if (_Settings.Get("processNameEliteDangerous") != "" && !_ForegroundProcessName.Contains(_Settings.Get("processNameEliteDangerous")))
            {
                _ForegroundProcessName.Add(_Settings.Get("processNameEliteDangerous"));
            }
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

        private void comboboxProcessNameIrcClient_Loaded(object sender, RoutedEventArgs e)
        {
            comboboxProcessNameIrcClient.ItemsSource = _ForegroundProcessName;
            comboboxProcessNameIrcClient.SelectedItem = _Settings.Get("processNameIrcClient");
        }

        private void comboboxProcessNameIrcClient_DropDownOpened(object sender, System.EventArgs e)
        {
            refreshForegroundProcessNameList();

            comboboxProcessNameIrcClient.ItemsSource = _ForegroundProcessName;
            comboboxProcessNameIrcClient.SelectedItem = _Settings.Get("processNameIrcClient");
        }

        private void comboboxProcessNameIrcClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _Settings.Set("processNameIrcClient", _ForegroundProcessName.ElementAt(comboboxProcessNameIrcClient.SelectedIndex));
        }

        private void comboboxProcessNameEliteDangerous_Loaded(object sender, RoutedEventArgs e)
        {
            comboboxProcessNameEliteDangerous.ItemsSource = _ForegroundProcessName;
            comboboxProcessNameEliteDangerous.SelectedItem = _Settings.Get("processNameEliteDangerous");
        }

        private void comboboxProcessNameEliteDangerous_DropDownOpened(object sender, System.EventArgs e)
        {
            refreshForegroundProcessNameList();

            comboboxProcessNameEliteDangerous.ItemsSource = _ForegroundProcessName;
            comboboxProcessNameEliteDangerous.SelectedItem = _Settings.Get("processNameEliteDangerous");
        }

        private void comboboxProcessNameEliteDangerous_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _Settings.Set("processNameEliteDangerous", _ForegroundProcessName.ElementAt(comboboxProcessNameEliteDangerous.SelectedIndex));
        }

        private void radiobuttonCopyPaste_Loaded(object sender, RoutedEventArgs e)
        {
            if (_Settings.Get("messageInsertionMode") == "copyPaste" || _Settings.Get("messageInsertionMode") == "")
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
