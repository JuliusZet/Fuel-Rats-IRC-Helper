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


using System.Windows;
using System.Windows.Controls;

namespace Fuel_Rats_IRC_Helper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Message message = new Message();

        public MainWindow()
        {
            InitializeComponent();
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

        private void menuitemClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void menuitemAbout_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void checkboxCasenumber_Checked(object sender, RoutedEventArgs e)
        {
            message.Prepend("Casenumber", "");
            textboxCasenumber.Focus();
        }

        private void checkboxCasenumber_Unchecked(object sender, RoutedEventArgs e)
        {
            message.Remove("Casenumber");
            textboxMessage.Text = message.Generate();
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
                message.Replace("Casenumber", '#' + textboxCasenumber.Text);
                textboxMessage.Text = message.Generate();
            }

            else
            {
                checkboxCasenumber.IsChecked = false;
            }
        }

        private void checkboxRgr_Checked(object sender, RoutedEventArgs e)
        {
            message.Append("Rgr", "rgr");
            textboxMessage.Text = message.Generate();
        }

        private void checkboxRgr_Unchecked(object sender, RoutedEventArgs e)
        {
            message.Remove("Rgr");
            textboxMessage.Text = message.Generate();
        }

        private void checkboxRdy_Checked(object sender, RoutedEventArgs e)
        {
            message.Append("Rdy", "rdy");
            textboxMessage.Text = message.Generate();
        }

        private void checkboxRdy_Unchecked(object sender, RoutedEventArgs e)
        {
            message.Remove("Rdy");
            textboxMessage.Text = message.Generate();
        }

        private void checkboxJumpcallout_Checked(object sender, RoutedEventArgs e)
        {
            message.Append("Jumpcallout", "");
            textboxJumpcallout.Focus();
        }

        private void checkboxJumpcallout_Unchecked(object sender, RoutedEventArgs e)
        {
            message.Remove("Jumpcallout");
            textboxMessage.Text = message.Generate();
            textboxJumpcallout.Clear();
        }

        private void textboxJumpcallout_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textboxJumpcallout.Text != "")
            {
                checkboxJumpcallout.IsChecked = true;
                message.Replace("Jumpcallout", textboxJumpcallout.Text + 'j');
                textboxMessage.Text = message.Generate();
            }

            else
            {
                checkboxJumpcallout.IsChecked = false;
            }
        }

        private void checkboxOwnPos_Checked(object sender, RoutedEventArgs e)
        {
            message.Append("OwnPos", "");

            if (radiobuttonPosPlus.IsChecked != true)
            {
                radiobuttonSysPlus.IsChecked = true;
            }
        }

        private void checkboxOwnPos_Unchecked(object sender, RoutedEventArgs e)
        {
            message.Remove("OwnPos");
            textboxMessage.Text = message.Generate();
            radiobuttonSysPlus.IsChecked = false;
            radiobuttonPosPlus.IsChecked = false;
        }

        private void radiobuttonSysPlus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxOwnPos.IsChecked = true;
            message.Replace("OwnPos", "sys+");
            textboxMessage.Text = message.Generate();
        }

        private void radiobuttonSysPlus_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void radiobuttonPosPlus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxOwnPos.IsChecked = true;
            message.Replace("OwnPos", "pos+");
            textboxMessage.Text = message.Generate();
        }

        private void radiobuttonPosPlus_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void checkboxFr_Checked(object sender, RoutedEventArgs e)
        {
            message.Append("Fr", "");

            if (radiobuttonFrMinus.IsChecked != true)
            {
                radiobuttonFrPlus.IsChecked = true;
            }
        }

        private void checkboxFr_Unchecked(object sender, RoutedEventArgs e)
        {
            message.Remove("Fr");
            textboxMessage.Text = message.Generate();
            radiobuttonFrPlus.IsChecked = false;
            radiobuttonFrMinus.IsChecked = false;
        }

        private void radiobuttonFrPlus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxFr.IsChecked = true;
            message.Replace("Fr", "fr+");
            textboxMessage.Text = message.Generate();
        }

        private void radiobuttonFrPlus_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void radiobuttonFrMinus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxFr.IsChecked = true;
            message.Replace("Fr", "fr-");
            textboxMessage.Text = message.Generate();
        }

        private void radiobuttonFrMinus_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void checkboxOnlineStatus_Checked(object sender, RoutedEventArgs e)
        {
            message.Append("OnlineStatus", "");

            if (radiobuttonOnlinestatusPg.IsChecked != true && radiobuttonOnlinestatusSolo.IsChecked != true && radiobuttonOnlinestatusMm.IsChecked != true)
            {
                radiobuttonOnlinestatusOpen.IsChecked = true;
            }
        }

        private void checkboxOnlineStatus_Unchecked(object sender, RoutedEventArgs e)
        {
            message.Remove("OnlineStatus");
            textboxMessage.Text = message.Generate();
            radiobuttonOnlinestatusOpen.IsChecked = false;
            radiobuttonOnlinestatusPg.IsChecked = false;
            radiobuttonOnlinestatusSolo.IsChecked = false;
            radiobuttonOnlinestatusMm.IsChecked = false;
        }

        private void radiobuttonOnlinestatusOpen_Checked(object sender, RoutedEventArgs e)
        {
            checkboxOnlineStatus.IsChecked = true;
            message.Replace("OnlineStatus", "in open");
            textboxMessage.Text = message.Generate();
        }

        private void radiobuttonOnlinestatusOpen_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void radiobuttonOnlinestatusPg_Checked(object sender, RoutedEventArgs e)
        {
            checkboxOnlineStatus.IsChecked = true;
            message.Replace("OnlineStatus", "in pg");
            textboxMessage.Text = message.Generate();
        }

        private void radiobuttonOnlinestatusPg_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void radiobuttonOnlinestatusSolo_Checked(object sender, RoutedEventArgs e)
        {
            checkboxOnlineStatus.IsChecked = true;
            message.Replace("OnlineStatus", "in solo");
            textboxMessage.Text = message.Generate();
        }

        private void radiobuttonOnlinestatusSolo_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void radiobuttonOnlinestatusMm_Checked(object sender, RoutedEventArgs e)
        {
            checkboxOnlineStatus.IsChecked = true;
            message.Replace("OnlineStatus", "in mm");
            textboxMessage.Text = message.Generate();
        }

        private void radiobuttonOnlinestatusMm_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void checkboxSysconf_Checked(object sender, RoutedEventArgs e)
        {
            message.Append("Sysconf", "");

            if (radiobuttonSysconfMinus.IsChecked != true)
            {
                radiobuttonSysconfPlus.IsChecked = true;
            }
        }

        private void checkboxSysconf_Unchecked(object sender, RoutedEventArgs e)
        {
            message.Remove("Sysconf");
            textboxMessage.Text = message.Generate();
            radiobuttonSysconfPlus.IsChecked = false;
            radiobuttonSysconfMinus.IsChecked = false;
            textboxSysconfMinus.Clear();
        }

        private void radiobuttonSysconfPlus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxSysconf.IsChecked = true;
            message.Replace("Sysconf", "sysconf");
            textboxMessage.Text = message.Generate();
        }

        private void radiobuttonSysconfPlus_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void radiobuttonSysconfMinus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxSysconf.IsChecked = true;
            textboxSysconfMinus.Focus();
            message.Replace("Sysconf", "in " + textboxSysconfMinus.Text);
            textboxMessage.Text = message.Generate();
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
                message.Replace("Sysconf", "in " + textboxSysconfMinus.Text);
                textboxMessage.Text = message.Generate();
            }

            else
            {
                radiobuttonSysconfMinus.IsChecked = false;
            }
        }

        private void checkboxWr_Checked(object sender, RoutedEventArgs e)
        {
            message.Append("Wr", "");

            if (radiobuttonWrMinus.IsChecked != true)
            {
                radiobuttonWrPlus.IsChecked = true;
            }
        }

        private void checkboxWr_Unchecked(object sender, RoutedEventArgs e)
        {
            message.Remove("Wr");
            textboxMessage.Text = message.Generate();
            radiobuttonWrPlus.IsChecked = false;
            radiobuttonWrMinus.IsChecked = false;
        }

        private void radiobuttonWrPlus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxWr.IsChecked = true;
            message.Replace("Wr", "wr+");
            textboxMessage.Text = message.Generate();
        }

        private void radiobuttonWrPlus_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void radiobuttonWrMinus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxWr.IsChecked = true;
            message.Replace("Wr", "wr-");
            textboxMessage.Text = message.Generate();
        }

        private void radiobuttonWrMinus_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void checkboxPrep_Checked(object sender, RoutedEventArgs e)
        {
            message.Append("Prep", "");

            if (radiobuttonPrepMinus.IsChecked != true)
            {
                radiobuttonPrepPlus.IsChecked = true;
            }
        }

        private void checkboxPrep_Unchecked(object sender, RoutedEventArgs e)
        {
            message.Remove("Prep");
            textboxMessage.Text = message.Generate();
            radiobuttonPrepPlus.IsChecked = false;
            radiobuttonPrepMinus.IsChecked = false;
        }

        private void radiobuttonPrepPlus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxPrep.IsChecked = true;
            message.Replace("Prep", "prep+");
            textboxMessage.Text = message.Generate();
        }

        private void radiobuttonPrepPlus_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void radiobuttonPrepMinus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxPrep.IsChecked = true;
            message.Replace("Prep", "prep-");
            textboxMessage.Text = message.Generate();
        }

        private void radiobuttonPrepMinus_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void checkboxNavcheck_Checked(object sender, RoutedEventArgs e)
        {
            message.Append("Navcheck", "");

            if (radiobuttonNavcheckHasJumps.IsChecked != true)
            {
                radiobuttonNavcheckNoJumps.IsChecked = true;
            }
        }

        private void checkboxNavcheck_Unchecked(object sender, RoutedEventArgs e)
        {
            message.Remove("Navcheck");
            textboxMessage.Text = message.Generate();
            radiobuttonNavcheckNoJumps.IsChecked = false;
            radiobuttonNavcheckHasJumps.IsChecked = false;
            textboxNavcheckHasJumps.Clear();
        }

        private void radiobuttonNavcheckNoJumps_Checked(object sender, RoutedEventArgs e)
        {
            checkboxNavcheck.IsChecked = true;
            message.Replace("Navcheck", "navcheck: can not jump");
            textboxMessage.Text = message.Generate();
        }

        private void radiobuttonNavcheckNoJumps_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void radiobuttonNavcheckHasJumps_Checked(object sender, RoutedEventArgs e)
        {
            checkboxNavcheck.IsChecked = true;
            textboxNavcheckHasJumps.Focus();
            message.Replace("Navcheck", "navcheck: can jump " + textboxNavcheckHasJumps.Text + " ly");
            textboxMessage.Text = message.Generate();
        }

        private void radiobuttonNavcheckHasJumps_Unchecked(object sender, RoutedEventArgs e)
        {
            textboxNavcheckHasJumps.Clear();
        }

        private void textboxNavcheckHasJumps_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textboxNavcheckHasJumps.Text != "")
            {
                radiobuttonNavcheckHasJumps.IsChecked = true;
                message.Replace("Navcheck", "navcheck: can jump " + textboxNavcheckHasJumps.Text + " ly");
                textboxMessage.Text = message.Generate();
            }

            else
            {
                radiobuttonNavcheckHasJumps.IsChecked = false;
            }
        }

        private void checkboxBc_Checked(object sender, RoutedEventArgs e)
        {
            message.Append("Bc", "");

            if (radiobuttonBcMinus.IsChecked != true)
            {
                radiobuttonBcPlus.IsChecked = true;
            }
        }

        private void checkboxBc_Unchecked(object sender, RoutedEventArgs e)
        {
            message.Remove("Bc");
            textboxMessage.Text = message.Generate();
            radiobuttonBcPlus.IsChecked = false;
            radiobuttonBcMinus.IsChecked = false;
        }

        private void radiobuttonBcPlus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxBc.IsChecked = true;
            textboxDistance.Focus();
            message.Replace("Bc", "bc+");
            textboxMessage.Text = message.Generate();
        }

        private void radiobuttonBcPlus_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void radiobuttonBcMinus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxBc.IsChecked = true;
            message.Replace("Bc", "bc-");
            textboxMessage.Text = message.Generate();
        }

        private void radiobuttonBcMinus_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void checkboxInst_Checked(object sender, RoutedEventArgs e)
        {
            message.Append("Inst", "");

            if (radiobuttonInstMinus.IsChecked != true)
            {
                radiobuttonInstPlus.IsChecked = true;
            }
        }

        private void checkboxInst_Unchecked(object sender, RoutedEventArgs e)
        {
            message.Remove("Inst");
            textboxMessage.Text = message.Generate();
            radiobuttonInstPlus.IsChecked = false;
            radiobuttonInstMinus.IsChecked = false;
        }

        private void radiobuttonInstPlus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxInst.IsChecked = true;
            message.Replace("Inst", "inst+");
            textboxMessage.Text = message.Generate();
        }

        private void radiobuttonInstPlus_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void radiobuttonInstMinus_Checked(object sender, RoutedEventArgs e)
        {
            checkboxInst.IsChecked = true;
            textboxDistance.Focus();
            message.Replace("Inst", "inst-");
            textboxMessage.Text = message.Generate();
        }

        private void radiobuttonInstMinus_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void checkboxClientPos_Checked(object sender, RoutedEventArgs e)
        {
            message.Append("ClientPos", "");

            if (radiobuttonInSc.IsChecked != true)
            {
                radiobuttonInEz.IsChecked = true;
            }
        }

        private void checkboxClientPos_Unchecked(object sender, RoutedEventArgs e)
        {
            message.Remove("ClientPos");
            textboxMessage.Text = message.Generate();
            radiobuttonInEz.IsChecked = false;
            radiobuttonInSc.IsChecked = false;
        }

        private void radiobuttonInEz_Checked(object sender, RoutedEventArgs e)
        {
            checkboxClientPos.IsChecked = true;
            textboxDistance.Focus();
            message.Replace("ClientPos", "in ez");
            textboxMessage.Text = message.Generate();
        }

        private void radiobuttonInEz_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void radiobuttonInSc_Checked(object sender, RoutedEventArgs e)
        {
            checkboxClientPos.IsChecked = true;
            message.Replace("ClientPos", "in sc");
            textboxMessage.Text = message.Generate();
        }

        private void radiobuttonInSc_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void checkboxDistance_Checked(object sender, RoutedEventArgs e)
        {
            message.Append("Distance", "");
            textboxDistance.Focus();
        }

        private void checkboxDistance_Unchecked(object sender, RoutedEventArgs e)
        {
            message.Remove("Distance");
            textboxMessage.Text = message.Generate();
            textboxDistance.Clear();
        }

        private void textboxDistance_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textboxDistance.Text != "")
            {
                checkboxDistance.IsChecked = true;
                message.Replace("Distance", textboxDistance.Text + " ls");
                textboxMessage.Text = message.Generate();
            }

            else
            {
                checkboxDistance.IsChecked = false;
            }
        }

        private void checkboxFuel_Checked(object sender, RoutedEventArgs e)
        {
            message.Append("Fuel", "fuel+");
            textboxMessage.Text = message.Generate();
        }

        private void checkboxFuel_Unchecked(object sender, RoutedEventArgs e)
        {
            message.Remove("Fuel");
            textboxMessage.Text = message.Generate();
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
            message.Clear();
            textboxMessage.Text = message.Generate();
        }

        private void buttonSend_Click(object sender, RoutedEventArgs e)
        {
            message.Send(textboxMessage.Text);
            UncheckAllCheckboxesExceptCasenumber();
        }
    }
}
