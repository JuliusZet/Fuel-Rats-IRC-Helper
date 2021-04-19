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
            Case selectedCase = datagridCaseList.CurrentItem as Case;
            Irc.ShowCaseWindow(selectedCase.Id);
        }
    }
}
