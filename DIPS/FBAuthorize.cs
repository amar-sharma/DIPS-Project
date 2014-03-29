using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Net;
namespace DIPS
{
    public partial class FBAuthorize : Form
    {
        public void Proxify()
        {
            IWebProxy proxyObject;
            String username = DIPS.Properties.Settings.Default.ProxyUsername;
            String password = DIPS.Properties.Settings.Default.ProxyPassword;
            password = DIPS.DIPSEncryption.ToInsecureString(DIPS.DIPSEncryption.DecryptString(password));
            Uri domain = new Uri(DIPS.Properties.Settings.Default.ProxyAddress);
            proxyObject = new WebProxy(domain, true);
            proxyObject.Credentials = new NetworkCredential(username, password);
            HttpWebRequest.DefaultWebProxy = proxyObject;
        }

        #region Dragging
        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        ///
        /// Handling the window messages
        ///
        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == WM_NCHITTEST && (int)message.Result == HTCLIENT)
                message.Result = (IntPtr)HTCAPTION;
        }
        #endregion

        public FBAuthorize()
        {
            InitializeComponent();
            ShowInTaskbar = false;
            if (Properties.Settings.Default.hasPermission)
            {
                this.Visible = true;
            }
            else
            {
                this.Visible = false;
            }
            if (Properties.Settings.Default.IsProxy)
            {
                Proxify();
            }
            }

        public string ApplicationId
        {
            get
            {
                return ConfigurationManager.AppSettings["ApplicationId"];
            }
        }

        public string ExtendedPermissions
        {
            get
            {
                return ConfigurationManager.AppSettings["ExtendedPermissions"];
            }
        }


        public string AppSecret
        {
            get
            {
                return ConfigurationManager.AppSettings["ApplicationSecret"];
            }
        }

        public string AccessToken { get; set; }

        private void FBAuthorize_Load(object sender, EventArgs e)
        {

            if (Properties.Settings.Default.hasPermission)
            {
                this.SetVisibleCore(false);
            }
            else
            {
                this.SetVisibleCore(true);
            }
            if (Properties.Settings.Default.IsProxy)
            {
                Proxify();
            }
            try
            {
                var destinationURL = String.Format(
                     @"https://www.facebook.com/dialog/oauth?client_id={0}&scope={1}&redirect_uri=http://www.facebook.com/connect/login_success.html&response_type=token",
                this.ApplicationId,
                this.ExtendedPermissions);
                webBrowser.Navigated += WebBrowserNavigated;
                webBrowser.Navigate(destinationURL);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

         }

        private void WebBrowserNavigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            var url = e.Url.Fragment;
            if (url.Contains("access_token") && url.Contains("#"))
            {
                url = (new Regex("#")).Replace(url, "?", 1);
                this.AccessToken = System.Web.HttpUtility.ParseQueryString(url).Get("access_token");
                Properties.Settings.Default.AccesToken = this.AccessToken;
                MessageBox.Show("Your Access Token has been Verified ! ");
                Properties.Settings.Default.Save();
                Properties.Settings.Default.hasPermission = true;
                Properties.Settings.Default.hasAuthorized = true;
                Properties.Settings.Default.Save();
                this.Dispose();
            }
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void FBAuthorize_FormClosing(object sender, FormClosingEventArgs e)
        {
            DIPSPanel.fbauth = null;
        }
    }
}
