using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace DIPS.Twitter
{
    public partial class FormSettings : Form
    {

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

        public void Proxify()
        {
            if (DIPS.Properties.Settings.Default.IsProxy)
            {
                IWebProxy proxyObject;
                String username = DIPS.Properties.Settings.Default.ProxyUsername;
                String password = DIPS.Properties.Settings.Default.ProxyPassword;
                password = DIPS.DIPSEncryption.ToInsecureString(DIPS.DIPSEncryption.DecryptString(password));
                Uri domain = new Uri(DIPS.Properties.Settings.Default.ProxyAddress);
                proxyObject = new WebProxy(domain, true);
                if (DIPS.Properties.Settings.Default.ProxyAuth)
                    proxyObject.Credentials = new NetworkCredential(username, password);
                HttpWebRequest.DefaultWebProxy = proxyObject;
            }
            else
            {
                HttpWebRequest.DefaultWebProxy = null;
            }
        }
        public FormSettings(oAuthTwitter oAuth)
        {
            _oAuth = oAuth;

            InitializeComponent();
        }


        private void FormSettings_Load(object sender, EventArgs e)
        {
            // Load data
            //txtConsumerKey.Text = _oAuth.ConsumerKey;
            //txtConsumerSecret.Text = _oAuth.ConsumerSecret;
            txtPIN.Text = _oAuth.PIN;

            //bool hasConsumerKeyAndSecret = !String.IsNullOrEmpty(_oAuth.ConsumerKey) &&
            //                             !String.IsNullOrEmpty(_oAuth.ConsumerSecret);

            //grpSettings.Enabled = hasConsumerKeyAndSecret;

            //btnSave.Enabled = false;
            //btnSave.Enabled = true;

            if (!String.IsNullOrEmpty(Settings1.Default.pin))
            {
                btnSave.Enabled = false;
                grpSettings.Enabled = false;
            }
            else
            {
                btnSave.Enabled = true;
                grpSettings.Enabled = true;

            }

            //if (String.IsNullOrEmpty(Settings1.Default.pin) && !(btnSave.Enabled))
            if (String.IsNullOrEmpty(Settings1.Default.pin) && !(btnSave.Enabled))   //added !(btnSave.Enabled)
            {
                grpAuthorize.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Proxify();
            /* if (String.IsNullOrEmpty(txtConsumerKey.Text) || String.IsNullOrEmpty(txtConsumerSecret.Text))
            {
                MessageBox.Show("You must specify a consumer key and consumer secret.");
            }
            */
            string prevKey = _oAuth.ConsumerKey;
            string prevSecret = _oAuth.ConsumerSecret;

            try
            {
                //  _oAuth.ConsumerKey = txtConsumerKey.Text;
                //  _oAuth.ConsumerSecret = txtConsumerSecret.Text;

                // Each Twitter application has an authorization page where the user can specify
                // 'yes, allow this application' or 'no, deny this application'. The following
                // call obtains the URL to that page. Authorization link will look something like this:
                // http://twitter.com/oauth/authorize?oauth_token=c8GZ6vCDdgKO4gTx0ZZXzvjZ76auuvlD1hxoLeiWc
                string authLink = _oAuth.AuthorizationLinkGet();

                if (DialogResult.Cancel ==
                    MessageBox.Show(this, "DIPS will now direct you to the Twitter website where " +
                                          "you will be assigned a 7-digit PIN for this application.",
                                         "Twitter Client", MessageBoxButtons.OKCancel))
                    return;

                // Update the UI...
                grpSettings.Enabled = false;
                grpAuthorize.Enabled = true;

                // Launch the web browser...
                System.Diagnostics.Process.Start(authLink);
            }
            catch (System.Exception ex)
            {
                // Restore...
                _oAuth.ConsumerKey = prevKey;
                _oAuth.ConsumerSecret = prevSecret;
                MessageBox.Show(this, "An error occurred during authorization! Error:\n\n" + ex.Message, "Twitter Client", MessageBoxButtons.OK);
            }
        }

        /* private void txtConsumerKeyOrSecret_TextChanged(object sender, EventArgs e)
         {
             btnSave.Enabled = (!String.IsNullOrEmpty(txtConsumerKey.Text) && !String.IsNullOrEmpty(txtConsumerSecret.Text));
         }
         */
        private void btnReset_Click(object sender, EventArgs e)
        {
            _oAuth.Reset();
            //txtConsumerSecret.Enabled = txtConsumerKey.Enabled = true;
            //txtConsumerKey.Text = txtConsumerSecret.Text = txtPIN.Text = String.Empty;

            txtPIN.Text = String.Empty;
            /* txtProxy.Text = String.Empty;
             txtPort.Text = String.Empty;
             txtUsername.Text = String.Empty;
             txtPassword.Text = String.Empty;*/

            grpSettings.Enabled = true;
            grpAuthorize.Enabled = false;
        }

        private void txtPIN_TextChanged(object sender, EventArgs e)
        {
            btnAuthorize.Enabled = txtPIN.Text.Trim().Length == 7;
        }

        private void btnAuthorize_Click(object sender, EventArgs e)
        {
            try
            {
                this.Proxify();
                // Now that the application's been authenticated, let's get the (permanent)
                // token and secret token that we'll use to authenticate from now on.
                _oAuth.AccessTokenGet(_oAuth.OAuthToken, txtPIN.Text.Trim());

                //txtTweet.Enabled = btwTweet.Enabled = true;
                grpAuthorize.Enabled = false;

                MessageBox.Show(this, "Success! You're ready to start tweeting!", "Twitter Client", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "An error occurred during authorization:\n\n" + ex.Message, "Twitter Client", MessageBoxButtons.OK);
            }
        }


        private oAuthTwitter _oAuth;

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Store them...
            Settings1.Default.token = _oAuth.Token;
            Settings1.Default.authToken = _oAuth.OAuthToken;
            Settings1.Default.secretToken = _oAuth.TokenSecret;
            Settings1.Default.consumerKey = _oAuth.ConsumerKey;
            Settings1.Default.consumerSecret = _oAuth.ConsumerSecret;
            Settings1.Default.pin = _oAuth.PIN;

            /*Settings1.Default.Proxy = txtProxy.Text;
            Settings1.Default.Port = txtPort.Text;
            Settings1.Default.Username = txtUsername.Text;
            Settings1.Default.Password = txtPassword.Text;*/

            Settings1.Default.Save();
        }

        private void grpSettings_Enter(object sender, EventArgs e)
        {

        }

    }
}
