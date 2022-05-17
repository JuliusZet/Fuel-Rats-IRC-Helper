/*
 *   CaseListWindow.xaml.cs
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
    /// Interaction logic for CaseListWindow.xaml
    /// </summary>
    public partial class CaseListWindow : Window
    {
        public CaseListWindow()
        {
            InitializeComponent();
        }

        public void RefreshCaseList()
        {
            if (datagridCaseList.IsLoaded)
            {
                datagridCaseList.Items.Refresh();
            }
        }

        private void datagridCaseList_Loaded(object sender, RoutedEventArgs e)
        {
            datagridCaseList.ItemsSource = Irc.Case;
        }

        private void DataGridRow_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            (datagridCaseList.CurrentItem as Case).ShowCaseWindow();
        }

        private void DataGridRow_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.MiddleButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                System.Windows.Controls.DataGridRow dataGridRow = sender as System.Windows.Controls.DataGridRow;
                Case c = dataGridRow.DataContext as Case;
                Irc._Case.Remove(c);
                Irc.RefreshCaseList();
            }
        }
    }
}
