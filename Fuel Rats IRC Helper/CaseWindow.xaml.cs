/*
 *   CaseWindow.xaml.cs
 *   Fuel Rats IRC Helper
 *
 *   Created by Julius Zitzmann on 2020-05-08.
 *   Copyright © 2021 Julius Zitzmann. All rights reserved.
 *
 *   Feature requests, bug reports or questions?
 *   info@julius-zitzmann.de
 *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */


using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Fuel_Rats_IRC_Helper
{
    /// <summary>
    /// Interaction logic for CaseWindow.xaml
    /// </summary>
    public partial class CaseWindow : Window
    {
        private string _CaseNumber;
        private string _ClientIrcNick;
        private string _ClientCmdrName;
        private string _ClientSystem;
        private string _ClientPlatform;
        private string _ClientO2;
        private string _ClientLanguage;
        private List<IrcMessage> _IrcMessage;
        private List<Rat> _AssignedRat;

        public CaseWindow(string caseNumber, string clientIrcNick, string clientCmdrName, string clientSystem, string clientPlatform, string clientO2, string clientLanguage, List<IrcMessage> ircMessage, List<Rat> assignedRat)
        {
            _CaseNumber = caseNumber;
            _ClientIrcNick = clientIrcNick;
            _ClientCmdrName = clientCmdrName;
            _ClientSystem = clientSystem;
            _ClientPlatform = clientPlatform;
            _ClientO2 = clientO2;
            _ClientLanguage = clientLanguage;
            _IrcMessage = ircMessage;
            _AssignedRat = assignedRat;

            InitializeComponent();
        }

        public string ClientIrcNick
        {
            set
            {
                _ClientIrcNick = value;
                
                if (textboxClientIrcNick.IsLoaded)
                {
                    textboxClientIrcNick.Text = value;
                }
            }
        }

        public string ClientCmdrName
        {
            set
            {
                _ClientCmdrName = value;

                if (textboxClientCmdrName.IsLoaded)
                {
                    textboxClientCmdrName.Text = value;
                }
            }
        }

        public string ClientSystem
        {
            set
            {
                _ClientSystem = value;

                if (textboxClientSystem.IsLoaded)
                {
                    textboxClientSystem.Text = value;
                }
            }
        }

        public string ClientPlatform
        {
            set
            {
                _ClientPlatform = value;

                if (textboxClientPlatform.IsLoaded)
                {
                    textboxClientPlatform.Text = value;
                }
            }
        }

        public string ClientO2
        {
            set
            {
                _ClientO2 = value;

                if (textboxClientO2.IsLoaded)
                {
                    textboxClientO2.Text = value;
                }
            }
        }

        public string ClientLanguage
        {
            set
            {
                _ClientLanguage = value;

                if (textboxClientLanguage.IsLoaded)
                {
                    textboxClientLanguage.Text = value;
                }
            }
        }

        public void RefreshCaseChat()
        {
            if (datagridCaseChat.IsLoaded)
            {
                datagridCaseChat.Items.Refresh();

                if (_IrcMessage.Count != 0)
                {
                    datagridCaseChat.ScrollIntoView(datagridCaseChat.Items[datagridCaseChat.Items.Count - 1]);
                }
            }
        }

        public void RefreshAssignedRat()
        {
            if (datagridAssignedRats.IsLoaded)
            {
                datagridAssignedRats.Items.Refresh();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title = "Case #" + _CaseNumber;
        }

        private void buttonPrep_Click(object sender, RoutedEventArgs e)
        {
            textboxMessage.Text = "!prep-auto " + _ClientIrcNick;
            buttonSend_Click(null, null);
        }

        private void buttonModules_Click(object sender, RoutedEventArgs e)
        {
            textboxMessage.Text = "!modules-auto " + _ClientIrcNick;
            buttonSend_Click(null, null);
        }

        private void buttonGoFr_Click(object sender, RoutedEventArgs e)
        {
            textboxMessage.Text = "!gofr-auto " + _ClientIrcNick;
            textboxMessage.Focus();
            textboxMessage.CaretIndex = textboxMessage.Text.Length;
        }

        private void buttonTeam_Click(object sender, RoutedEventArgs e)
        {
            textboxMessage.Text = "!team-auto " + _ClientIrcNick;
            buttonSend_Click(null, null);
        }

        private void buttonBeacon_Click(object sender, RoutedEventArgs e)
        {
            textboxMessage.Text = "!beacon-auto " + _ClientIrcNick;
            buttonSend_Click(null, null);
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            textboxMessage.Text = "!close " + _ClientIrcNick;
            textboxMessage.Focus();
            textboxMessage.CaretIndex = textboxMessage.Text.Length;
        }

        private void buttonQuit_Click(object sender, RoutedEventArgs e)
        {
            textboxMessage.Text = "!quit-auto " + _ClientIrcNick;
            buttonSend_Click(null, null);
        }

        private void buttonCrinst_Click(object sender, RoutedEventArgs e)
        {
            textboxMessage.Text = "!crinst-auto " + _ClientIrcNick;
            buttonSend_Click(null, null);
        }

        private void buttonNick_Click(object sender, RoutedEventArgs e)
        {
            textboxMessage.Text = "!nick " + _CaseNumber;
            textboxMessage.Focus();
            textboxMessage.CaretIndex = textboxMessage.Text.Length;
        }

        private void buttonCmdr_Click(object sender, RoutedEventArgs e)
        {
            textboxMessage.Text = "!cmdr " + _CaseNumber;
            textboxMessage.Focus();
            textboxMessage.CaretIndex = textboxMessage.Text.Length;
        }

        private void buttonSys_Click(object sender, RoutedEventArgs e)
        {
            textboxMessage.Text = "!sys " + _CaseNumber;
            textboxMessage.Focus();
            textboxMessage.CaretIndex = textboxMessage.Text.Length;
        }

        private void buttonPc_Click(object sender, RoutedEventArgs e)
        {
            textboxMessage.Text = "!pc " + _CaseNumber;
            buttonSend_Click(null, null);
        }

        private void buttonOdyssey_Click(object sender, RoutedEventArgs e)
        {
            textboxMessage.Text = "!odyssey  " + _CaseNumber;
            buttonSend_Click(null, null);
        }

        private void buttonXb_Click(object sender, RoutedEventArgs e)
        {
            textboxMessage.Text = "!xb " + _CaseNumber;
            buttonSend_Click(null, null);
        }

        private void buttonPs_Click(object sender, RoutedEventArgs e)
        {
            textboxMessage.Text = "!ps " + _CaseNumber;
            buttonSend_Click(null, null);
        }

        private void buttonCr_Click(object sender, RoutedEventArgs e)
        {
            textboxMessage.Text = "!cr " + _CaseNumber;
            buttonSend_Click(null, null);
        }

        private void buttonLang_Click(object sender, RoutedEventArgs e)
        {
            textboxMessage.Text = "!lang " + _CaseNumber;
            textboxMessage.Focus();
            textboxMessage.CaretIndex = textboxMessage.Text.Length;
        }

        private void buttonGrab_Click(object sender, RoutedEventArgs e)
        {
            textboxMessage.Text = "!grab " + _CaseNumber;
            buttonSend_Click(null, null);
        }

        private void buttonInject_Click(object sender, RoutedEventArgs e)
        {
            textboxMessage.Text = "!inject " + _CaseNumber;
            textboxMessage.Focus();
            textboxMessage.CaretIndex = textboxMessage.Text.Length;
        }

        private void datagridCaseChat_Loaded(object sender, RoutedEventArgs e)
        {
            datagridCaseChat.ItemsSource = _IrcMessage;

            if (_IrcMessage.Count != 0)
            {
                datagridCaseChat.ScrollIntoView(datagridCaseChat.Items[datagridCaseChat.Items.Count - 1]);
            }
        }

        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridCell dataGridCell = sender as DataGridCell;
            TextBlock textBlock = dataGridCell.Content as TextBlock;
            
            if (dataGridCell.Column.DisplayIndex == 2)
            {
                IrcMessage IrcMessage = dataGridCell.DataContext as IrcMessage;
                FormTranslateMessage.TranslateStatic(IrcMessage.SenderNickname, IrcMessage.Text);
                return;
            }

            if (textboxMessage.Text != "")
            {
                textboxMessage.Text += ' ';
            }
            textboxMessage.Text += textBlock.Text;
            textboxMessage.Focus();
            textboxMessage.CaretIndex = textboxMessage.Text.Length;
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

            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                int selectionStart = textboxMessage.SelectionStart;
                int selectionLength = textboxMessage.SelectionLength;
                textboxMessage.Text = textboxMessage.Text.Remove(selectionStart, selectionLength).Insert(selectionStart, _ClientIrcNick);
                textboxMessage.CaretIndex = selectionStart + _ClientIrcNick.Length;
            }
        }

        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            textboxMessage.Clear();
            textboxMessage.Focus();
        }

        private void buttonSend_Click(object sender, RoutedEventArgs e)
        {
            if (Message.Send(textboxMessage.Text, false) == 0)
            {
                buttonClear_Click(null, null);
            }
        }

        private void textboxCasenumber_Loaded(object sender, RoutedEventArgs e)
        {
            textboxCasenumber.Text = _CaseNumber;
        }

        private void textboxClientIrcNick_Loaded(object sender, RoutedEventArgs e)
        {
            textboxClientIrcNick.Text = _ClientIrcNick;
        }

        private void textboxClientCmdrName_Loaded(object sender, RoutedEventArgs e)
        {
            textboxClientCmdrName.Text = _ClientCmdrName;
        }

        private void textboxClientSystem_Loaded(object sender, RoutedEventArgs e)
        {
            textboxClientSystem.Text = _ClientSystem;
        }

        private void textboxClientPlatform_Loaded(object sender, RoutedEventArgs e)
        {
            textboxClientPlatform.Text = _ClientPlatform;
        }

        private void textboxClientO2_Loaded(object sender, RoutedEventArgs e)
        {
            textboxClientO2.Text = _ClientO2;
        }

        private void textboxClientLanguage_Loaded(object sender, RoutedEventArgs e)
        {
            textboxClientLanguage.Text = _ClientLanguage;
        }

        private void datagridAssignedRats_Loaded(object sender, RoutedEventArgs e)
        {
            datagridAssignedRats.ItemsSource = _AssignedRat;
        }
    }
}
