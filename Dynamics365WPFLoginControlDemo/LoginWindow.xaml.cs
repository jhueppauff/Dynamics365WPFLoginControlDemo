//-----------------------------------------------------------------------
// <copyright file="LoginWindow.xaml.cs" company="https://github.com/jhueppauff/Dynamics365WPFLoginControlDemo">
// Copyright 2018 Jhueppauff
// MIT License 
// For licence details visit https://github.com/jhueppauff/Dynamics365WPFLoginControlDemo/blob/master/LICENSE
//-----------------------------------------------------------------------

namespace Dynamics365WPFLoginControlDemo
{
    using Microsoft.Xrm.Tooling.Connector;
    using Microsoft.Xrm.Tooling.CrmConnectControl;
    using System;
    using System.Windows;

    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        #region variables

        /// <summary>
        /// Crm Service Client
        /// </summary>
        private CrmServiceClient CrmServiceClient = null;

        /// <summary>
        /// flag to determine if ther is a connection
        /// </summary>
        private bool connectionState = false;

        /// <summary>
        /// Crm Connection Manager
        /// </summary>
        private CrmConnectionManager crmConnectionManager = null;

        /// <summary>
        /// Used to reset the ui without reopening
        /// </summary>
        private bool resetUiFlag = false;
        #endregion

        #region properties

        /// <summary>
        /// Gets the CrmConnectionManager
        /// </summary>
        public CrmConnectionManager CrmConnectionManager { get { return crmConnectionManager; } }
        #endregion

        #region event

        /// <summary>
        /// Raised when the connection was established
        /// </summary>
        public event EventHandler ConnectionToCrmCompleted;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginWindow"/> class.
        /// </summary>
        public LoginWindow()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Raised when the window loads 
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.connectionState = false;

            // Initialize the connection Manager
            this.crmConnectionManager = new CrmConnectionManager
            {
                ParentControl = this.CrmLoginControl,
                UseUserLocalDirectoryForConfigStore = true
            };

            // Configure the Crm Control
            this.CrmLoginControl.SetGlobalStoreAccess(crmConnectionManager);
            this.CrmLoginControl.SetControlMode(ServerLoginConfigCtrlMode.FullLoginPanel);

            // Event registration
            this.CrmLoginControl.ConnectionCheckBegining += new EventHandler(CrmLoginControl_ConnectionCheckStarted);
            this.CrmLoginControl.ConnectErrorEvent += new EventHandler<ConnectErrorEventArgs>(CrmLoginControl_ConnectionErrorRaised);
            this.CrmLoginControl.ConnectionStatusEvent += new EventHandler<ConnectStatusEventArgs>(CrmLoginControl_ConnectionStatusEventRaised);
            this.CrmLoginControl.UserCancelClicked += new EventHandler(CrmLoginControl_UserCancelClicked);

            // Check if an auto login is possible
            if (!this.crmConnectionManager.RequireUserLogin())
            {
#pragma warning disable S1066 // Collapsible "if" statements should be merged
                if (MessageBox.Show("There are already Credentials saved in your configuration\nDo you want to load those?\nYes to Auto Login or No to Reset Credentials", "Auto Login possible", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
#pragma warning restore S1066 // Collapsible "if" statements should be merged
                {
                    // Credentials are cached
                    this.CrmLoginControl.IsEnabled = false;

                    this.crmConnectionManager.ServerConnectionStatusUpdate += new EventHandler<ServerConnectStatusEventArgs>(CrmConnectionManager_ServerConnectionStatusUpdateRaised);
                    this.crmConnectionManager.ConnectionCheckComplete += new EventHandler<ServerConnectStatusEventArgs>(CrmConnectionManager_ConnectionCheckCompleted);

                    this.crmConnectionManager.ConnectToServerCheck();

                    // Show message
                    this.CrmLoginControl.ShowMessageGrid();
                }
            }

        }

        #region events

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">The <see cref="ServerConnectStatusEventArgs"/> instance containing the event data.</param>
        private void CrmConnectionManager_ConnectionCheckCompleted(object sender, ServerConnectStatusEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CrmConnectionManager_ServerConnectionStatusUpdateRaised(object sender, ServerConnectStatusEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CrmLoginControl_UserCancelClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CrmLoginControl_ConnectionStatusEventRaised(object sender, ConnectStatusEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CrmLoginControl_ConnectionErrorRaised(object sender, ConnectErrorEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CrmLoginControl_ConnectionCheckStarted(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
