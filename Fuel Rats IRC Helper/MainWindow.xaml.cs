/*
 *   MainWindow.xaml.cs
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
using System.Collections.Generic;
using System.Deployment.Application;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Fuel_Rats_IRC_Helper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly List<string> _DistanceUnit;

        public MainWindow()
        {
            _DistanceUnit = new List<string>(new string[]{ "m", "km", "Mm", "ls", "kls", "Mls", "ly" });
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
            checkboxTm.IsChecked = false;
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

            if (radiobuttonTmPlus.IsChecked == true)
            {
                groupboxTm.BorderBrush = greenGroupboxBorderBrush;
            }

            else if (radiobuttonTmMinus.IsChecked == true)
            {
                groupboxTm.BorderBrush = redGroupboxBorderBrush;
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
            groupboxTm.BorderBrush = defaultGroupboxBorderBrush;
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
            if (Settings.Get("checkForUpdatesOnStartup") == "yes")
            {
                CheckForUpdates(silentCheck: true);
            }

            if (Settings.Get("showChangelogOnStartup") == "yes")
            {
                ChangelogWindow changelogWindow = new ChangelogWindow
                {
                    Owner = this
                };
                changelogWindow.ShowDialog();
            }

            // If this is the first time the app is launched after an update
            if (ApplicationDeployment.IsNetworkDeployed && Settings.Get("version") != ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString())
            {
                Settings.ResetToDefault("ratsignalStartsWith");
                Settings.Set("version", ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString());
            }

            if (Settings.Get("ircAutoconnect") == "yes")
            {
                Irc.Connect();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Irc.Disconnect();
            Irc.CloseCaseListWindow();
        }

        private void menuitemSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow setttingsWindow = new SettingsWindow
            {
                Owner = this
            };
            setttingsWindow.ShowDialog();
        }

        private void menuitemClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void menuitemConnectToIrc_Click(object sender, RoutedEventArgs e)
        {
            Irc.Connect();
        }

        private void menuitemDisconnectFromIrc_Click(object sender, RoutedEventArgs e)
        {
            Irc.Disconnect();
        }

        private void menuitemAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow
            {
                Owner = this
            };
            aboutWindow.ShowDialog();
        }

        private void menuitemChangelog_Click(object sender, RoutedEventArgs e)
        {
            ChangelogWindow changelogWindow = new ChangelogWindow
            {
                Owner = this
            };
            changelogWindow.ShowDialog();
        }

        private void menuitemCheckForUpdates_Click(object sender, RoutedEventArgs e)
        {
            CheckForUpdates(false);
        }

        private void checkboxCasenumber_Checked(object sender, RoutedEventArgs e)
        {
            Message.Prepend("Casenumber", "");
            textboxCasenumber.Focus();
        }

        private void checkboxCasenumber_Unchecked(object sender, RoutedEventArgs e)
        {
            Message.Remove("Casenumber");
            textboxMessage.Text = Message.Generate();
            textboxCasenumber.Clear();
            buttonShowCase.Content = "Show case";
            buttonShowCase.IsEnabled = false;
        }

        private void textboxCasenumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textboxCasenumber.Text != "")
            {
                checkboxCasenumber.IsChecked = true;
                Message.Replace("Casenumber", '#' + textboxCasenumber.Text);
                textboxMessage.Text = Message.Generate();
                buttonShowCase.Content = "Show case #" + textboxCasenumber.Text;
                buttonShowCase.IsEnabled = true;
            }

            else
            {
                checkboxCasenumber.IsChecked = false;
            }
        }

        private void textboxCasenumber_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                textboxJumpcallout.Focus();
            }
        }

        private void checkboxRgr_Checked(object sender, RoutedEventArgs e)
        {
            Message.Append("Rgr", "rgr");
            textboxMessage.Text = Message.Generate();
        }

        private void checkboxRgr_Unchecked(object sender, RoutedEventArgs e)
        {
            Message.Remove("Rgr");
            textboxMessage.Text = Message.Generate();
        }

        private void checkboxRdy_Checked(object sender, RoutedEventArgs e)
        {
            Message.Append("Rdy", "rdy");
            textboxMessage.Text = Message.Generate();
        }

        private void checkboxRdy_Unchecked(object sender, RoutedEventArgs e)
        {
            Message.Remove("Rdy");
            textboxMessage.Text = Message.Generate();
        }

        private void checkboxJumpcallout_Checked(object sender, RoutedEventArgs e)
        {
            Message.Append("Jumpcallout", "");
            textboxJumpcallout.Focus();
        }

        private void checkboxJumpcallout_Unchecked(object sender, RoutedEventArgs e)
        {
            Message.Remove("Jumpcallout");
            textboxMessage.Text = Message.Generate();
            textboxJumpcallout.Clear();
        }

        private void textboxJumpcallout_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textboxJumpcallout.Text != "")
            {
                checkboxJumpcallout.IsChecked = true;
                Message.Replace("Jumpcallout", textboxJumpcallout.Text + 'j');
                textboxMessage.Text = Message.Generate();
            }

            else
            {
                checkboxJumpcallout.IsChecked = false;
            }
        }

        private void textboxJumpcallout_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                buttonSend_Click(null, null);
            }
        }

        private void checkboxOwnPos_Checked(object sender, RoutedEventArgs e)
        {
            Message.Append("OwnPos", "");

            if (radiobuttonPosPlus.IsChecked != true)
            {
                radiobuttonSysPlus.IsChecked = true;
            }
        }

        private void checkboxOwnPos_Unchecked(object sender, RoutedEventArgs e)
        {
            Message.Remove("OwnPos");
            textboxMessage.Text = Message.Generate();
            radiobuttonSysPlus.IsChecked = false;
            radiobuttonPosPlus.IsChecked = false;
        }

        private void radiobuttonSysPlus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxOwnPos.IsChecked = true;
            Message.Replace("OwnPos", "sys+");
            textboxMessage.Text = Message.Generate();
        }

        private void radiobuttonPosPlus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxOwnPos.IsChecked = true;
            Message.Replace("OwnPos", "pos+");
            textboxMessage.Text = Message.Generate();
        }

        private void checkboxFr_Checked(object sender, RoutedEventArgs e)
        {
            Message.Append("Fr", "");

            if (radiobuttonFrMinus.IsChecked != true)
            {
                radiobuttonFrPlus.IsChecked = true;
            }
        }

        private void checkboxFr_Unchecked(object sender, RoutedEventArgs e)
        {
            Message.Remove("Fr");
            textboxMessage.Text = Message.Generate();
            radiobuttonFrPlus.IsChecked = false;
            radiobuttonFrMinus.IsChecked = false;
        }

        private void radiobuttonFrPlus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxFr.IsChecked = true;
            Message.Replace("Fr", "fr+");
            textboxMessage.Text = Message.Generate();
        }

        private void radiobuttonFrMinus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxFr.IsChecked = true;
            Message.Replace("Fr", "fr-");
            textboxMessage.Text = Message.Generate();
        }

        private void checkboxOnlineStatus_Checked(object sender, RoutedEventArgs e)
        {
            Message.Append("OnlineStatus", "");

            if (radiobuttonInPg.IsChecked != true && radiobuttonInSolo.IsChecked != true && radiobuttonInMm.IsChecked != true)
            {
                radiobuttonInOpen.IsChecked = true;
            }
        }

        private void checkboxOnlineStatus_Unchecked(object sender, RoutedEventArgs e)
        {
            Message.Remove("OnlineStatus");
            textboxMessage.Text = Message.Generate();
            radiobuttonInOpen.IsChecked = false;
            radiobuttonInPg.IsChecked = false;
            radiobuttonInSolo.IsChecked = false;
            radiobuttonInMm.IsChecked = false;
        }

        private void radiobuttonInOpen_Checked(object sender, RoutedEventArgs e)
        {
            checkboxOnlineStatus.IsChecked = true;
            Message.Replace("OnlineStatus", "client in open");
            textboxMessage.Text = Message.Generate();
        }

        private void radiobuttonInPg_Checked(object sender, RoutedEventArgs e)
        {
            checkboxOnlineStatus.IsChecked = true;
            Message.Replace("OnlineStatus", "client in pg");
            textboxMessage.Text = Message.Generate();
        }

        private void radiobuttonInSolo_Checked(object sender, RoutedEventArgs e)
        {
            checkboxOnlineStatus.IsChecked = true;
            Message.Replace("OnlineStatus", "client in solo");
            textboxMessage.Text = Message.Generate();
        }

        private void radiobuttonInMm_Checked(object sender, RoutedEventArgs e)
        {
            checkboxOnlineStatus.IsChecked = true;
            Message.Replace("OnlineStatus", "client in mm");
            textboxMessage.Text = Message.Generate();
        }

        private void checkboxSysconf_Checked(object sender, RoutedEventArgs e)
        {
            Message.Append("Sysconf", "");

            if (radiobuttonSysconfMinus.IsChecked != true)
            {
                radiobuttonSysconfPlus.IsChecked = true;
            }
        }

        private void checkboxSysconf_Unchecked(object sender, RoutedEventArgs e)
        {
            Message.Remove("Sysconf");
            textboxMessage.Text = Message.Generate();
            radiobuttonSysconfPlus.IsChecked = false;
            radiobuttonSysconfMinus.IsChecked = false;
            textboxSysconfMinus.Clear();
        }

        private void radiobuttonSysconfPlus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxSysconf.IsChecked = true;
            Message.Replace("Sysconf", "sysconf");
            textboxMessage.Text = Message.Generate();
        }

        private void radiobuttonSysconfMinus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxSysconf.IsChecked = true;
            textboxSysconfMinus.Focus();
            Message.Replace("Sysconf", "syscorr: " + textboxSysconfMinus.Text);
            textboxMessage.Text = Message.Generate();
        }

        private void textboxSysconfMinus_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textboxSysconfMinus.Text != "")
            {
                radiobuttonSysconfMinus.IsChecked = true;
                Message.Replace("Sysconf", "syscorr: " + textboxSysconfMinus.Text);
                textboxMessage.Text = Message.Generate();
            }

            else
            {
                checkboxSysconf.IsChecked = false;
            }
        }

        private void textboxSysconfMinus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                buttonSend_Click(null, null);
            }
        }

        private void checkboxTm_Checked(object sender, RoutedEventArgs e)
        {
            Message.Append("Tm", "");

            if (radiobuttonTmMinus.IsChecked != true)
            {
                radiobuttonTmPlus.IsChecked = true;
            }
        }

        private void checkboxTm_Unchecked(object sender, RoutedEventArgs e)
        {
            Message.Remove("Tm");
            textboxMessage.Text = Message.Generate();
            radiobuttonTmPlus.IsChecked = false;
            radiobuttonTmMinus.IsChecked = false;
        }

        private void radiobuttonTmPlus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxTm.IsChecked = true;
            Message.Replace("Tm", "tm+");
            textboxMessage.Text = Message.Generate();
        }

        private void radiobuttonTmMinus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxTm.IsChecked = true;
            Message.Replace("Tm", "tm-");
            textboxMessage.Text = Message.Generate();
        }

        private void checkboxPrep_Checked(object sender, RoutedEventArgs e)
        {
            Message.Append("Prep", "");

            if (radiobuttonPrepMinus.IsChecked != true)
            {
                radiobuttonPrepPlus.IsChecked = true;
            }
        }

        private void checkboxPrep_Unchecked(object sender, RoutedEventArgs e)
        {
            Message.Remove("Prep");
            textboxMessage.Text = Message.Generate();
            radiobuttonPrepPlus.IsChecked = false;
            radiobuttonPrepMinus.IsChecked = false;
        }

        private void radiobuttonPrepPlus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxPrep.IsChecked = true;
            Message.Replace("Prep", "prep+");
            textboxMessage.Text = Message.Generate();
        }

        private void radiobuttonPrepMinus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxPrep.IsChecked = true;
            Message.Replace("Prep", "prep-");
            textboxMessage.Text = Message.Generate();
        }

        private void checkboxNavcheck_Checked(object sender, RoutedEventArgs e)
        {
            Message.Append("Navcheck", "");

            if (radiobuttonHasJumps.IsChecked != true)
            {
                radiobuttonNoJumps.IsChecked = true;
            }
        }

        private void checkboxNavcheck_Unchecked(object sender, RoutedEventArgs e)
        {
            Message.Remove("Navcheck");
            textboxMessage.Text = Message.Generate();
            radiobuttonNoJumps.IsChecked = false;
            radiobuttonHasJumps.IsChecked = false;
            textboxHasJumps.Clear();
        }

        private void radiobuttonNoJumps_Checked(object sender, RoutedEventArgs e)
        {
            checkboxNavcheck.IsChecked = true;
            Message.Replace("Navcheck", "navcheck: client can not jump");
            textboxMessage.Text = Message.Generate();
        }

        private void radiobuttonHasJumps_Checked(object sender, RoutedEventArgs e)
        {
            checkboxNavcheck.IsChecked = true;
            textboxHasJumps.Focus();
            Message.Replace("Navcheck", "navcheck: client can jump " + textboxHasJumps.Text + " ly");
            textboxMessage.Text = Message.Generate();
        }

        private void textboxHasJumps_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textboxHasJumps.Text != "")
            {
                radiobuttonHasJumps.IsChecked = true;
                Message.Replace("Navcheck", "navcheck: client can jump " + textboxHasJumps.Text + " ly");
                textboxMessage.Text = Message.Generate();
            }

            else
            {
                checkboxNavcheck.IsChecked = false;
            }
        }

        private void textboxHasJumps_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                buttonSend_Click(null, null);
            }
        }

        private void checkboxBc_Checked(object sender, RoutedEventArgs e)
        {
            Message.Append("Bc", "");

            if (radiobuttonBcMinus.IsChecked != true)
            {
                radiobuttonBcPlus.IsChecked = true;
            }
        }

        private void checkboxBc_Unchecked(object sender, RoutedEventArgs e)
        {
            Message.Remove("Bc");
            textboxMessage.Text = Message.Generate();
            radiobuttonBcPlus.IsChecked = false;
            radiobuttonBcMinus.IsChecked = false;
        }

        private void radiobuttonBcPlus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxBc.IsChecked = true;
            textboxDistance.Focus();
            comboboxDistanceUnit.SelectedItem = "ls";
            Message.Replace("Bc", "bc+");
            textboxMessage.Text = Message.Generate();
        }

        private void radiobuttonBcMinus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxBc.IsChecked = true;
            Message.Replace("Bc", "bc-");
            textboxMessage.Text = Message.Generate();
        }

        private void checkboxInst_Checked(object sender, RoutedEventArgs e)
        {
            Message.Append("Inst", "");

            if (radiobuttonInstMinus.IsChecked != true)
            {
                radiobuttonInstPlus.IsChecked = true;
            }
        }

        private void checkboxInst_Unchecked(object sender, RoutedEventArgs e)
        {
            Message.Remove("Inst");
            textboxMessage.Text = Message.Generate();
            radiobuttonInstPlus.IsChecked = false;
            radiobuttonInstMinus.IsChecked = false;
            comboboxDistanceUnit.SelectedItem = "ls";
        }

        private void radiobuttonInstPlus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxInst.IsChecked = true;
            Message.Replace("Inst", "inst+");
            textboxMessage.Text = Message.Generate();
        }

        private void radiobuttonInstMinus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxInst.IsChecked = true;
            textboxDistance.Focus();
            comboboxDistanceUnit.SelectedItem = "km";
            Message.Replace("Inst", "inst-");
            textboxMessage.Text = Message.Generate();
        }

        private void checkboxClientPos_Checked(object sender, RoutedEventArgs e)
        {
            Message.Append("ClientPos", "");

            if (radiobuttonInSc.IsChecked != true)
            {
                radiobuttonInEz.IsChecked = true;
            }
        }

        private void checkboxClientPos_Unchecked(object sender, RoutedEventArgs e)
        {
            Message.Remove("ClientPos");
            textboxMessage.Text = Message.Generate();
            radiobuttonInEz.IsChecked = false;
            radiobuttonInSc.IsChecked = false;
        }

        private void radiobuttonInEz_Checked(object sender, RoutedEventArgs e)
        {
            checkboxClientPos.IsChecked = true;
            textboxDistance.Focus();
            comboboxDistanceUnit.SelectedItem = "ls";
            Message.Replace("ClientPos", "client in ez");
            textboxMessage.Text = Message.Generate();
        }

        private void radiobuttonInSc_Checked(object sender, RoutedEventArgs e)
        {
            checkboxClientPos.IsChecked = true;
            Message.Replace("ClientPos", "client in sc");
            textboxMessage.Text = Message.Generate();
        }

        private void checkboxDistance_Checked(object sender, RoutedEventArgs e)
        {
            Message.Append("Distance", "");
            textboxDistance.Focus();
        }

        private void checkboxDistance_Unchecked(object sender, RoutedEventArgs e)
        {
            Message.Remove("Distance");
            textboxMessage.Text = Message.Generate();
            textboxDistance.Clear();
            comboboxDistanceUnit.SelectedItem = "ls";
        }

        private void textboxDistance_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textboxDistance.Text != "")
            {
                checkboxDistance.IsChecked = true;
                Message.Replace("Distance", textboxDistance.Text + ' ' + _DistanceUnit.ElementAt(comboboxDistanceUnit.SelectedIndex));
                textboxMessage.Text = Message.Generate();
            }

            else
            {
                checkboxDistance.IsChecked = false;
            }
        }

        private void textboxDistance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                buttonSend_Click(null, null);
            }

            else if (e.Key == Key.Add)
            {
                e.Handled = true;
                if (comboboxDistanceUnit.SelectedIndex != _DistanceUnit.Count - 1)
                {
                    ++comboboxDistanceUnit.SelectedIndex;
                }
            }

            else if (e.Key == Key.Subtract)
            {
                e.Handled = true;
                if (comboboxDistanceUnit.SelectedIndex != 0)
                {
                    --comboboxDistanceUnit.SelectedIndex;
                }
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
                Message.Replace("Distance", textboxDistance.Text + ' ' + _DistanceUnit.ElementAt(comboboxDistanceUnit.SelectedIndex));
                textboxMessage.Text = Message.Generate();
            }
        }

        private void checkboxFuel_Checked(object sender, RoutedEventArgs e)
        {
            Message.Append("Fuel", "fuel+");
            textboxMessage.Text = Message.Generate();
        }

        private void checkboxFuel_Unchecked(object sender, RoutedEventArgs e)
        {
            Message.Remove("Fuel");
            textboxMessage.Text = Message.Generate();
        }

        private void buttonShowCase_Click(object sender, RoutedEventArgs e)
        {
            Irc.ShowCaseWindow(textboxCasenumber.Text);
        }

        private void buttonShowCaseList_Click(object sender, RoutedEventArgs e)
        {
            Irc.ShowCaseListWindow();
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
            textboxMessage.Text = textboxMessage.Text.Trim();
        }

        private void textboxMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                textboxMessage_LostFocus(null, null);
                buttonSend_Click(null, null);
            }
        }

        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            UncheckAllCheckboxes();
            Message.Clear();
            textboxMessage.Clear();
            ResetUserInterfaceColors();
            comboboxDistanceUnit.SelectedItem = "ls";
            textboxCasenumber.Focus();
        }

        private void buttonSend_Click(object sender, RoutedEventArgs e)
        {
            if (Message.Send(textboxMessage.Text) == 0)
            {
                UpdateUserInterfaceColors();
                UncheckAllCheckboxesExceptCasenumber();
                textboxMessage.Text = Message.Generate();
            }
        }
    }
}
