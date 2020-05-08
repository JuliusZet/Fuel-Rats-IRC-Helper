/*
 *   About.xaml.cs
 *   Fuel Rats IRC Helper
 *
 *   Created by Julius Zitzmann on 2020-05-08.
 *   Copyright © 2020 Julius Zitzmann. All rights reserved.
 *
 *   Feature requests, bug reports or questions?
 *   info@julius-zitzmann.de
 *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */


using System.Deployment.Application;
using System.Windows;

namespace Fuel_Rats_IRC_Helper
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
        }

        private void labelVersion_Initialized(object sender, System.EventArgs e)
        {
            try
            {
                labelVersion.Content = "Version " + ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }

            catch (InvalidDeploymentException)
            {
                labelVersion.Content = "Version 0.0.0.0 (not installed)";
            }
        }
    }
}
