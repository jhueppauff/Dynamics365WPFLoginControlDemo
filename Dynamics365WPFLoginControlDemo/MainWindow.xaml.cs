//-----------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="https://github.com/jhueppauff/Dynamics365WPFLoginControlDemo">
// Copyright 2018 Jhueppauff
// MIT License 
// For licence details visit https://github.com/jhueppauff/Dynamics365WPFLoginControlDemo/blob/master/LICENSE
//-----------------------------------------------------------------------

namespace Dynamics365WPFLoginControlDemo
{
    using System;
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes an new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Will open the Login Window
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.ConnectionToCrmCompleted += LoginWindow_ConnectionCompleted;

            loginWindow.ShowDialog();

            // Validate connection to crm
            if (loginWindow.CrmConnectionManager != null && loginWindow.CrmConnectionManager.CrmSvc != null && loginWindow.CrmConnectionManager.CrmSvc.IsReady)
            {
                UpdateStatus("Connected to CRM! (Version: " + loginWindow.CrmConnectionManager.CrmSvc.ConnectedOrgVersion.ToString() +
                   "; Org: " + loginWindow.CrmConnectionManager.CrmSvc.ConnectedOrgUniqueName.ToString() + ")");
                UpdateStatus("***************************************");

                BtnConnect.IsEnabled = false;
            }
        }

        /// <summary>
        /// Progressively displays the status messages about the actions
        /// performed during the running of the sample.
        /// <param name="updateText">Indicates the status string that needs to be 
        /// displayed to the user.</param>
        /// </summary>
        public void UpdateStatus(string updateText)
        {
            if (this.TbxStatus.Text.ToString() != String.Empty)
            {
                this.TbxStatus.Text = this.TbxStatus.Text + "\n" + updateText;
            }
            else
            {
                this.TbxStatus.Text = updateText;
            }
        }

        #region events

        /// <summary>
        /// Will be raised on connection completion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginWindow_ConnectionCompleted(object sender, EventArgs e)
        {
            if (sender is LoginWindow)
            {
                this.Dispatcher.Invoke(() =>
                {
                    ((LoginWindow)sender).Close();
                });
            }
        }

        #endregion
    }
}
