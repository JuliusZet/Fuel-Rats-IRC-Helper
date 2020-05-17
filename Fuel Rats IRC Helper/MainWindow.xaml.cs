/*
 *   MainWindow.xaml.cs
 *   Fuel Rats IRC Helper
 *
 *   Created by Julius Zitzmann on 2020-05-08.
 *   Copyright © 2020 Julius Zitzmann. All rights reserved.
 *
 *   Feature requests, bug reports or questions?
 *   info@julius-zitzmann.de
 *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */


using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Fuel_Rats_IRC_Helper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Message _Message;
        private Settings _Settings;
        private List<string> _DistanceUnit;

        public MainWindow()
        {
            _Message = new Message();
            _Settings = new Settings();
            _DistanceUnit = new List<string>(new string[]{ "m", "km", "Mm", "ls", "kls", "ly" });
            InitializeComponent();
        }

        private void CheckForUpdates(bool silentCheck)
        {
            UpdateCheckInfo updateCheckInfo = null;

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ApplicationDeployment applicationDeployment = ApplicationDeployment.CurrentDeployment;

                try
                {
                    updateCheckInfo = applicationDeployment.CheckForDetailedUpdate();
                }

                catch (DeploymentDownloadException deploymentDownloadException)
                {
                    if (silentCheck == false)
                    {
                        MessageBox.Show("The update can not be downloaded at this time. Please check your network connection or try again later. Error: " + deploymentDownloadException.Message, "Error");
                    }
                }

                catch (InvalidDeploymentException invalidDeploymentException)
                {
                    if (silentCheck == false)
                    {
                        MessageBox.Show("Could not check for a new version of the application. The ClickOnce deployment is corrupt. Please reinstall the application and try again. Error: " + invalidDeploymentException.Message, "Error");
                    }
                }

                catch (InvalidOperationException invalidOperationException)
                {
                    if (silentCheck == false)
                    {
                        MessageBox.Show("This application can not be updated. It is likely not a ClickOnce application. Error: " + invalidOperationException.Message, "Error");
                    }
                }

                if (updateCheckInfo.UpdateAvailable)
                {
                    bool doUpdate = true;

                    if (!updateCheckInfo.IsUpdateRequired)
                    {
                        System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("An update to version " + updateCheckInfo.AvailableVersion.ToString() + " is available. Do you want to update the application now?", "Update available", System.Windows.Forms.MessageBoxButtons.YesNo);

                        if (dialogResult != System.Windows.Forms.DialogResult.Yes)
                        {
                            doUpdate = false;
                        }
                    }

                    else
                    {
                        MessageBox.Show("A mandatory update to version " + updateCheckInfo.AvailableVersion.ToString() + " is available. The application will now update and restart.", "Mandatory update available");
                    }

                    if (doUpdate == true)
                    {
                        try
                        {
                            applicationDeployment.Update();
                            MessageBox.Show("The update to version " + updateCheckInfo.AvailableVersion.ToString() + " was installed successfully. Please restart the application to see what has changed.", "Update successful");
                            Close();
                        }

                        catch (DeploymentDownloadException deploymentDownloadException)
                        {
                            MessageBox.Show("Could not install the update to version " + updateCheckInfo.AvailableVersion.ToString() + ". Please check your network connection or try again later. Error: " + deploymentDownloadException.Message, "Error");
                        }
                    }
                }

                else
                {
                    if (silentCheck == false)
                    {
                        MessageBox.Show("There are no new updates. You are running the latest version.", "No update available");
                    }
                }
            }
        }

        private void UncheckAllCheckboxesExceptCasenumber()
        {
            checkboxRgr.IsChecked = false;
            checkboxRdy.IsChecked = false;
            checkboxJumpcallout.IsChecked = false;
            checkboxOwnPos.IsChecked = false;
            checkboxFr.IsChecked = false;
            checkboxOnlineStatus.IsChecked = false;
            checkboxSysconf.IsChecked = false;
            checkboxWr.IsChecked = false;
            checkboxPrep.IsChecked = false;
            checkboxNavcheck.IsChecked = false;
            checkboxBc.IsChecked = false;
            checkboxInst.IsChecked = false;
            checkboxClientPos.IsChecked = false;
            checkboxDistance.IsChecked = false;
            checkboxFuel.IsChecked = false;
        }

        private void UncheckAllCheckboxes()
        {
            checkboxCasenumber.IsChecked = false;
            UncheckAllCheckboxesExceptCasenumber();
        }

        private void UpdateUserInterfaceColors()
        {
            SolidColorBrush greenGroupboxBorderBrush = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
            SolidColorBrush redGroupboxBorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));

            if (textboxCasenumber.Text != "")
            {
                groupboxCasenumber.BorderBrush = greenGroupboxBorderBrush;
            }

            if (checkboxRgr.IsChecked == true)
            {
                groupboxRgr.BorderBrush = greenGroupboxBorderBrush;
            }

            if (checkboxRdy.IsChecked == true)
            {
                groupboxRdy.BorderBrush = greenGroupboxBorderBrush;
            }

            if (textboxJumpcallout.Text != "")
            {
                groupboxJumpcallout.BorderBrush = greenGroupboxBorderBrush;
            }

            if (checkboxOwnPos.IsChecked == true)
            {
                groupboxOwnPos.BorderBrush = greenGroupboxBorderBrush;
            }

            if (radiobuttonFrPlus.IsChecked == true)
            {
                groupboxFr.BorderBrush = greenGroupboxBorderBrush;
            }

            else if (radiobuttonFrMinus.IsChecked == true)
            {
                groupboxFr.BorderBrush = redGroupboxBorderBrush;
            }

            if (checkboxOnlineStatus.IsChecked == true)
            {
                groupboxOnlineStatus.BorderBrush = greenGroupboxBorderBrush;
            }

            if (checkboxSysconf.IsChecked == true)
            {
                groupboxSysconf.BorderBrush = greenGroupboxBorderBrush;
            }

            if (radiobuttonWrPlus.IsChecked == true)
            {
                groupboxWr.BorderBrush = greenGroupboxBorderBrush;
            }

            else if (radiobuttonWrMinus.IsChecked == true)
            {
                groupboxWr.BorderBrush = redGroupboxBorderBrush;
            }

            if (radiobuttonPrepPlus.IsChecked == true)
            {
                groupboxPrep.BorderBrush = greenGroupboxBorderBrush;
            }

            else if (radiobuttonPrepMinus.IsChecked == true)
            {
                groupboxPrep.BorderBrush = redGroupboxBorderBrush;
            }

            if (checkboxNavcheck.IsChecked == true)
            {
                groupboxNavcheck.BorderBrush = greenGroupboxBorderBrush;
            }

            if (radiobuttonBcPlus.IsChecked == true)
            {
                groupboxBc.BorderBrush = greenGroupboxBorderBrush;
            }

            else if (radiobuttonBcMinus.IsChecked == true)
            {
                groupboxBc.BorderBrush = redGroupboxBorderBrush;
            }

            if (radiobuttonInstPlus.IsChecked == true)
            {
                groupboxInst.BorderBrush = greenGroupboxBorderBrush;
            }

            else if (radiobuttonInstMinus.IsChecked == true)
            {
                groupboxInst.BorderBrush = redGroupboxBorderBrush;
            }

            if (checkboxClientPos.IsChecked == true)
            {
                groupboxClientPos.BorderBrush = greenGroupboxBorderBrush;
            }

            if (textboxDistance.Text != "")
            {
                groupboxDistance.BorderBrush = greenGroupboxBorderBrush;
            }

            if (checkboxFuel.IsChecked == true)
            {
                groupboxFuel.BorderBrush = greenGroupboxBorderBrush;
            }
        }

        private void ResetUserInterfaceColors()
        {
            SolidColorBrush defaultGroupboxBorderBrush = new SolidColorBrush(Color.FromArgb(255, 213, 223, 229));

            groupboxCasenumber.BorderBrush = defaultGroupboxBorderBrush;
            groupboxRgr.BorderBrush = defaultGroupboxBorderBrush;
            groupboxRdy.BorderBrush = defaultGroupboxBorderBrush;
            groupboxJumpcallout.BorderBrush = defaultGroupboxBorderBrush;
            groupboxOwnPos.BorderBrush = defaultGroupboxBorderBrush;
            groupboxFr.BorderBrush = defaultGroupboxBorderBrush;
            groupboxOnlineStatus.BorderBrush = defaultGroupboxBorderBrush;
            groupboxSysconf.BorderBrush = defaultGroupboxBorderBrush;
            groupboxWr.BorderBrush = defaultGroupboxBorderBrush;
            groupboxPrep.BorderBrush = defaultGroupboxBorderBrush;
            groupboxNavcheck.BorderBrush = defaultGroupboxBorderBrush;
            groupboxBc.BorderBrush = defaultGroupboxBorderBrush;
            groupboxInst.BorderBrush = defaultGroupboxBorderBrush;
            groupboxClientPos.BorderBrush = defaultGroupboxBorderBrush;
            groupboxDistance.BorderBrush = defaultGroupboxBorderBrush;
            groupboxFuel.BorderBrush = defaultGroupboxBorderBrush;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_Settings.Get("checkForUpdatesOnStartup") == "yes")
            {
                CheckForUpdates(true);
            }

            if (_Settings.Get("showChangelogOnStartup") == "yes")
            {
                Changelog changelog = new Changelog();
                changelog.ShowDialog();
            }
        }

        private void menuitemClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void menuitemPreferences_Click(object sender, RoutedEventArgs e)
        {
            Preferences preferences = new Preferences();
            preferences.ShowDialog();
        }

        private void menuitemAbout_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void menuitemChangelog_Click(object sender, RoutedEventArgs e)
        {
            Changelog changelog = new Changelog();
            changelog.ShowDialog();
        }

        private void menuitemCheckForUpdates_Click(object sender, RoutedEventArgs e)
        {
            CheckForUpdates(false);
        }

        private void checkboxCasenumber_Checked(object sender, RoutedEventArgs e)
        {
            _Message.Prepend("Casenumber", "");
            textboxCasenumber.Focus();
        }

        private void checkboxCasenumber_Unchecked(object sender, RoutedEventArgs e)
        {
            _Message.Remove("Casenumber");
            textboxMessage.Text = _Message.Generate();
            textboxCasenumber.Clear();
        }

        private void textboxCasenumber_GotFocus(object sender, RoutedEventArgs e)
        {
            textboxCasenumber.Clear();
        }

        private void textboxCasenumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textboxCasenumber.Text != "")
            {
                checkboxCasenumber.IsChecked = true;
                _Message.Replace("Casenumber", '#' + textboxCasenumber.Text);
                textboxMessage.Text = _Message.Generate();
            }

            else
            {
                checkboxCasenumber.IsChecked = false;
            }
        }

        private void checkboxRgr_Checked(object sender, RoutedEventArgs e)
        {
            _Message.Append("Rgr", "rgr");
            textboxMessage.Text = _Message.Generate();
        }

        private void checkboxRgr_Unchecked(object sender, RoutedEventArgs e)
        {
            _Message.Remove("Rgr");
            textboxMessage.Text = _Message.Generate();
        }

        private void checkboxRdy_Checked(object sender, RoutedEventArgs e)
        {
            _Message.Append("Rdy", "rdy");
            textboxMessage.Text = _Message.Generate();
        }

        private void checkboxRdy_Unchecked(object sender, RoutedEventArgs e)
        {
            _Message.Remove("Rdy");
            textboxMessage.Text = _Message.Generate();
        }

        private void checkboxJumpcallout_Checked(object sender, RoutedEventArgs e)
        {
            _Message.Append("Jumpcallout", "");
            textboxJumpcallout.Focus();
        }

        private void checkboxJumpcallout_Unchecked(object sender, RoutedEventArgs e)
        {
            _Message.Remove("Jumpcallout");
            textboxMessage.Text = _Message.Generate();
            textboxJumpcallout.Clear();
        }

        private void textboxJumpcallout_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textboxJumpcallout.Text != "")
            {
                checkboxJumpcallout.IsChecked = true;
                _Message.Replace("Jumpcallout", textboxJumpcallout.Text + 'j');
                textboxMessage.Text = _Message.Generate();
            }

            else
            {
                checkboxJumpcallout.IsChecked = false;
            }
        }

        private void checkboxOwnPos_Checked(object sender, RoutedEventArgs e)
        {
            _Message.Append("OwnPos", "");

            if (radiobuttonPosPlus.IsChecked != true)
            {
                radiobuttonSysPlus.IsChecked = true;
            }
        }

        private void checkboxOwnPos_Unchecked(object sender, RoutedEventArgs e)
        {
            _Message.Remove("OwnPos");
            textboxMessage.Text = _Message.Generate();
            radiobuttonSysPlus.IsChecked = false;
            radiobuttonPosPlus.IsChecked = false;
        }

        private void radiobuttonSysPlus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxOwnPos.IsChecked = true;
            _Message.Replace("OwnPos", "sys+");
            textboxMessage.Text = _Message.Generate();
        }

        private void radiobuttonSysPlus_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void radiobuttonPosPlus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxOwnPos.IsChecked = true;
            _Message.Replace("OwnPos", "pos+");
            textboxMessage.Text = _Message.Generate();
        }

        private void radiobuttonPosPlus_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void checkboxFr_Checked(object sender, RoutedEventArgs e)
        {
            _Message.Append("Fr", "");

            if (radiobuttonFrMinus.IsChecked != true)
            {
                radiobuttonFrPlus.IsChecked = true;
            }
        }

        private void checkboxFr_Unchecked(object sender, RoutedEventArgs e)
        {
            _Message.Remove("Fr");
            textboxMessage.Text = _Message.Generate();
            radiobuttonFrPlus.IsChecked = false;
            radiobuttonFrMinus.IsChecked = false;
        }

        private void radiobuttonFrPlus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxFr.IsChecked = true;
            _Message.Replace("Fr", "fr+");
            textboxMessage.Text = _Message.Generate();
        }

        private void radiobuttonFrPlus_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void radiobuttonFrMinus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxFr.IsChecked = true;
            _Message.Replace("Fr", "fr-");
            textboxMessage.Text = _Message.Generate();
        }

        private void radiobuttonFrMinus_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void checkboxOnlineStatus_Checked(object sender, RoutedEventArgs e)
        {
            _Message.Append("OnlineStatus", "");

            if (radiobuttonInPg.IsChecked != true && radiobuttonInSolo.IsChecked != true && radiobuttonInMm.IsChecked != true)
            {
                radiobuttonInOpen.IsChecked = true;
            }
        }

        private void checkboxOnlineStatus_Unchecked(object sender, RoutedEventArgs e)
        {
            _Message.Remove("OnlineStatus");
            textboxMessage.Text = _Message.Generate();
            radiobuttonInOpen.IsChecked = false;
            radiobuttonInPg.IsChecked = false;
            radiobuttonInSolo.IsChecked = false;
            radiobuttonInMm.IsChecked = false;
        }

        private void radiobuttonInOpen_Checked(object sender, RoutedEventArgs e)
        {
            checkboxOnlineStatus.IsChecked = true;
            _Message.Replace("OnlineStatus", "in open");
            textboxMessage.Text = _Message.Generate();
        }

        private void radiobuttonInOpen_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void radiobuttonInPg_Checked(object sender, RoutedEventArgs e)
        {
            checkboxOnlineStatus.IsChecked = true;
            _Message.Replace("OnlineStatus", "in pg");
            textboxMessage.Text = _Message.Generate();
        }

        private void radiobuttonInPg_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void radiobuttonInSolo_Checked(object sender, RoutedEventArgs e)
        {
            checkboxOnlineStatus.IsChecked = true;
            _Message.Replace("OnlineStatus", "in solo");
            textboxMessage.Text = _Message.Generate();
        }

        private void radiobuttonInSolo_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void radiobuttonInMm_Checked(object sender, RoutedEventArgs e)
        {
            checkboxOnlineStatus.IsChecked = true;
            _Message.Replace("OnlineStatus", "in mm");
            textboxMessage.Text = _Message.Generate();
        }

        private void radiobuttonInMm_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void checkboxSysconf_Checked(object sender, RoutedEventArgs e)
        {
            _Message.Append("Sysconf", "");

            if (radiobuttonSysconfMinus.IsChecked != true)
            {
                radiobuttonSysconfPlus.IsChecked = true;
            }
        }

        private void checkboxSysconf_Unchecked(object sender, RoutedEventArgs e)
        {
            _Message.Remove("Sysconf");
            textboxMessage.Text = _Message.Generate();
            radiobuttonSysconfPlus.IsChecked = false;
            radiobuttonSysconfMinus.IsChecked = false;
        }

        private void radiobuttonSysconfPlus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxSysconf.IsChecked = true;
            _Message.Replace("Sysconf", "sysconf");
            textboxMessage.Text = _Message.Generate();
        }

        private void radiobuttonSysconfPlus_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void radiobuttonSysconfMinus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxSysconf.IsChecked = true;
            textboxSysconfMinus.Focus();
            _Message.Replace("Sysconf", "in " + textboxSysconfMinus.Text);
            textboxMessage.Text = _Message.Generate();
        }

        private void radiobuttonSysconfMinus_Unchecked(object sender, RoutedEventArgs e)
        {
            textboxSysconfMinus.Clear();
        }

        private void textboxSysconfMinus_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textboxSysconfMinus.Text != "")
            {
                radiobuttonSysconfMinus.IsChecked = true;
                _Message.Replace("Sysconf", "in " + textboxSysconfMinus.Text);
                textboxMessage.Text = _Message.Generate();
            }

            else
            {
                radiobuttonSysconfMinus.IsChecked = false;
            }
        }

        private void checkboxWr_Checked(object sender, RoutedEventArgs e)
        {
            _Message.Append("Wr", "");

            if (radiobuttonWrMinus.IsChecked != true)
            {
                radiobuttonWrPlus.IsChecked = true;
            }
        }

        private void checkboxWr_Unchecked(object sender, RoutedEventArgs e)
        {
            _Message.Remove("Wr");
            textboxMessage.Text = _Message.Generate();
            radiobuttonWrPlus.IsChecked = false;
            radiobuttonWrMinus.IsChecked = false;
        }

        private void radiobuttonWrPlus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxWr.IsChecked = true;
            _Message.Replace("Wr", "wr+");
            textboxMessage.Text = _Message.Generate();
        }

        private void radiobuttonWrPlus_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void radiobuttonWrMinus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxWr.IsChecked = true;
            _Message.Replace("Wr", "wr-");
            textboxMessage.Text = _Message.Generate();
        }

        private void radiobuttonWrMinus_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void checkboxPrep_Checked(object sender, RoutedEventArgs e)
        {
            _Message.Append("Prep", "");

            if (radiobuttonPrepMinus.IsChecked != true)
            {
                radiobuttonPrepPlus.IsChecked = true;
            }
        }

        private void checkboxPrep_Unchecked(object sender, RoutedEventArgs e)
        {
            _Message.Remove("Prep");
            textboxMessage.Text = _Message.Generate();
            radiobuttonPrepPlus.IsChecked = false;
            radiobuttonPrepMinus.IsChecked = false;
        }

        private void radiobuttonPrepPlus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxPrep.IsChecked = true;
            _Message.Replace("Prep", "prep+");
            textboxMessage.Text = _Message.Generate();
        }

        private void radiobuttonPrepPlus_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void radiobuttonPrepMinus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxPrep.IsChecked = true;
            _Message.Replace("Prep", "prep-");
            textboxMessage.Text = _Message.Generate();
        }

        private void radiobuttonPrepMinus_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void checkboxNavcheck_Checked(object sender, RoutedEventArgs e)
        {
            _Message.Append("Navcheck", "");

            if (radiobuttonHasJumps.IsChecked != true)
            {
                radiobuttonNoJumps.IsChecked = true;
            }
        }

        private void checkboxNavcheck_Unchecked(object sender, RoutedEventArgs e)
        {
            _Message.Remove("Navcheck");
            textboxMessage.Text = _Message.Generate();
            radiobuttonNoJumps.IsChecked = false;
            radiobuttonHasJumps.IsChecked = false;
        }

        private void radiobuttonNoJumps_Checked(object sender, RoutedEventArgs e)
        {
            checkboxNavcheck.IsChecked = true;
            _Message.Replace("Navcheck", "navcheck: can not jump");
            textboxMessage.Text = _Message.Generate();
        }

        private void radiobuttonNoJumps_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void radiobuttonHasJumps_Checked(object sender, RoutedEventArgs e)
        {
            checkboxNavcheck.IsChecked = true;
            textboxHasJumps.Focus();
            _Message.Replace("Navcheck", "navcheck: can jump " + textboxHasJumps.Text + " ly");
            textboxMessage.Text = _Message.Generate();
        }

        private void radiobuttonHasJumps_Unchecked(object sender, RoutedEventArgs e)
        {
            textboxHasJumps.Clear();
        }

        private void textboxHasJumps_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textboxHasJumps.Text != "")
            {
                radiobuttonHasJumps.IsChecked = true;
                _Message.Replace("Navcheck", "navcheck: can jump " + textboxHasJumps.Text + " ly");
                textboxMessage.Text = _Message.Generate();
            }

            else
            {
                radiobuttonHasJumps.IsChecked = false;
            }
        }

        private void checkboxBc_Checked(object sender, RoutedEventArgs e)
        {
            _Message.Append("Bc", "");

            if (radiobuttonBcMinus.IsChecked != true)
            {
                radiobuttonBcPlus.IsChecked = true;
            }
        }

        private void checkboxBc_Unchecked(object sender, RoutedEventArgs e)
        {
            _Message.Remove("Bc");
            textboxMessage.Text = _Message.Generate();
            radiobuttonBcPlus.IsChecked = false;
            radiobuttonBcMinus.IsChecked = false;
        }

        private void radiobuttonBcPlus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxBc.IsChecked = true;
            textboxDistance.Focus();
            comboboxDistanceUnit.SelectedItem = "ls";
            _Message.Replace("Bc", "bc+");
            textboxMessage.Text = _Message.Generate();
        }

        private void radiobuttonBcPlus_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void radiobuttonBcMinus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxBc.IsChecked = true;
            _Message.Replace("Bc", "bc-");
            textboxMessage.Text = _Message.Generate();
        }

        private void radiobuttonBcMinus_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void checkboxInst_Checked(object sender, RoutedEventArgs e)
        {
            _Message.Append("Inst", "");

            if (radiobuttonInstMinus.IsChecked != true)
            {
                radiobuttonInstPlus.IsChecked = true;
            }
        }

        private void checkboxInst_Unchecked(object sender, RoutedEventArgs e)
        {
            _Message.Remove("Inst");
            textboxMessage.Text = _Message.Generate();
            radiobuttonInstPlus.IsChecked = false;
            radiobuttonInstMinus.IsChecked = false;
        }

        private void radiobuttonInstPlus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxInst.IsChecked = true;
            _Message.Replace("Inst", "inst+");
            textboxMessage.Text = _Message.Generate();
        }

        private void radiobuttonInstPlus_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void radiobuttonInstMinus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxInst.IsChecked = true;
            textboxDistance.Focus();
            comboboxDistanceUnit.SelectedItem = "km";
            _Message.Replace("Inst", "inst-");
            textboxMessage.Text = _Message.Generate();
        }

        private void radiobuttonInstMinus_Unchecked(object sender, RoutedEventArgs e)
        {
            comboboxDistanceUnit.SelectedItem = "ls";
        }

        private void checkboxClientPos_Checked(object sender, RoutedEventArgs e)
        {
            _Message.Append("ClientPos", "");

            if (radiobuttonInSc.IsChecked != true)
            {
                radiobuttonInEz.IsChecked = true;
            }
        }

        private void checkboxClientPos_Unchecked(object sender, RoutedEventArgs e)
        {
            _Message.Remove("ClientPos");
            textboxMessage.Text = _Message.Generate();
            radiobuttonInEz.IsChecked = false;
            radiobuttonInSc.IsChecked = false;
        }

        private void radiobuttonInEz_Checked(object sender, RoutedEventArgs e)
        {
            checkboxClientPos.IsChecked = true;
            textboxDistance.Focus();
            comboboxDistanceUnit.SelectedItem = "ls";
            _Message.Replace("ClientPos", "in ez");
            textboxMessage.Text = _Message.Generate();
        }

        private void radiobuttonInEz_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void radiobuttonInSc_Checked(object sender, RoutedEventArgs e)
        {
            checkboxClientPos.IsChecked = true;
            _Message.Replace("ClientPos", "in sc");
            textboxMessage.Text = _Message.Generate();
        }

        private void radiobuttonInSc_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void checkboxDistance_Checked(object sender, RoutedEventArgs e)
        {
            _Message.Append("Distance", "");
            textboxDistance.Focus();
        }

        private void checkboxDistance_Unchecked(object sender, RoutedEventArgs e)
        {
            _Message.Remove("Distance");
            textboxMessage.Text = _Message.Generate();
            textboxDistance.Clear();
            comboboxDistanceUnit.SelectedItem = "ls";
        }

        private void textboxDistance_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textboxDistance.Text != "")
            {
                checkboxDistance.IsChecked = true;
                _Message.Replace("Distance", textboxDistance.Text + ' ' + _DistanceUnit.ElementAt(comboboxDistanceUnit.SelectedIndex));
                textboxMessage.Text = _Message.Generate();
            }

            else
            {
                checkboxDistance.IsChecked = false;
            }
        }

        private void comboboxDistanceUnit_Loaded(object sender, RoutedEventArgs e)
        {
            comboboxDistanceUnit.ItemsSource = _DistanceUnit;
            comboboxDistanceUnit.SelectedItem = "ls";
        }

        private void comboboxDistanceUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (checkboxDistance.IsChecked == true)
            {
                _Message.Replace("Distance", textboxDistance.Text + ' ' + _DistanceUnit.ElementAt(comboboxDistanceUnit.SelectedIndex));
                textboxMessage.Text = _Message.Generate();
            }
        }

        private void checkboxFuel_Checked(object sender, RoutedEventArgs e)
        {
            _Message.Append("Fuel", "fuel+");
            textboxMessage.Text = _Message.Generate();
        }

        private void checkboxFuel_Unchecked(object sender, RoutedEventArgs e)
        {
            _Message.Remove("Fuel");
            textboxMessage.Text = _Message.Generate();
        }

        private void textboxMessage_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textboxMessage.Text != "" && !textboxMessage.Text.EndsWith(" "))
            {
                textboxMessage.Text += ' ';
            }
        }

        private void textboxMessage_LostFocus(object sender, RoutedEventArgs e)
        {
            while (textboxMessage.Text != "" && textboxMessage.Text.EndsWith(" "))
            {
                textboxMessage.Text = textboxMessage.Text.Remove(textboxMessage.Text.Length - 1);
            }
        }

        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            UncheckAllCheckboxes();
            _Message.Clear();
            textboxMessage.Clear();
            ResetUserInterfaceColors();
            comboboxDistanceUnit.SelectedItem = "ls";
        }

        private void buttonSend_Click(object sender, RoutedEventArgs e)
        {
            _Message.Send(textboxMessage.Text);
            UpdateUserInterfaceColors();
            UncheckAllCheckboxesExceptCasenumber();
            textboxMessage.Text = _Message.Generate();
        }
    }
}
