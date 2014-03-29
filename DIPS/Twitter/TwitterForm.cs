using System;
using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Web;
using System.Runtime.InteropServices;

namespace DIPS.Twitter
{
    public partial class Form1 : Form
    {
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


        #region Glass Aero Effect
        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int Left;
            public int Right;
            public int Top;
            public int Bottom;
        }
        /// <summary>
        /// The API used to extend the GLass margins into the client area
        /// </summary>
        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);

        /// <summary>
        /// Determins whether the Desktop Windows Manager is enabled
        /// and can therefore display Aero 
        /// </summary>
        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern bool DwmIsCompositionEnabled();
        MARGINS margins;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DwmIsCompositionEnabled())
            {
                MessageBox.Show("This demo requires Vista, with Aero enabled.");
                Application.Exit();
            }
            SetGlassRegion();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (DwmIsCompositionEnabled())
            {
                e.Graphics.Clear(Color.FromArgb(161, 160, 160));
                // put back the original form background for non-glass area
                Rectangle clientArea = new Rectangle(
                margins.Left,
                margins.Top,
                this.ClientRectangle.Width /*- margins.Left - margins.Right*/,
                this.ClientRectangle.Height /*- margins.Top - margins.Bottom*/);
                Brush b = new SolidBrush(this.BackColor);
                e.Graphics.FillRectangle(b, clientArea);

            }
            else
            {
                base.OnPaintBackground(e);
            }
        }

        public void SetGlassRegion()
        {
            // Set up the glass effect using padding as the defining glass region
            if (DwmIsCompositionEnabled())
            {
                margins = new MARGINS();
                margins.Top = this.Height;
                margins.Left = this.Width;
                margins.Bottom = this.Height;
                margins.Right = this.Width;
                DwmExtendFrameIntoClientArea(this.Handle, ref margins);
            }
        }
        #endregion


        public Form1()
        {
            InitializeComponent();
        }

        private void SendTweet()
        {
            this.Proxify();
            if (txtTweet.Text.Length == 0)
            {
                MessageBox.Show("Your tweet must be at least 1 character long!");
                return;
            }

            if (txtTweet.Text.Length > 140)
            {
                MessageBox.Show("Your tweet exceeds 140 characters! Please edit your tweet.");
                return;
            }

            try
            {
                // URL-encode the tweet...
                string tweet = HttpUtility.UrlEncode(txtTweet.Text);

                // And send it off...
                string xml = _oAuth.oAuthWebRequest(
                    oAuthTwitter.Method.POST,
                    "https://api.twitter.com/1/statuses/update.xml",
                    "status="+tweet);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred! Your tweet could not be posted.\n\n" + ex.Message);
                return;
            }
        }

        private void btwTweet_Click(object sender, EventArgs e)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.DoWork += new DoWorkEventHandler(
            delegate(object o, DoWorkEventArgs args)
            {
                BackgroundWorker b = o as BackgroundWorker;
                SendTweet();
                //Do
            });
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
            delegate(object o, RunWorkerCompletedEventArgs args)
            {
                txtTweet.Clear();
                //Done
            });

            bw.RunWorkerAsync();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtTweet.Enabled = btwTweet.Enabled = IsConfigured;

            // Set up our credentials...
            _oAuth.Token = Settings1.Default.token;
            _oAuth.TokenSecret = Settings1.Default.secretToken;
            _oAuth.ConsumerKey = Settings1.Default.consumerKey;
            _oAuth.ConsumerSecret = Settings1.Default.consumerSecret;
            _oAuth.PIN = Settings1.Default.pin;
        }
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x80;  // Turn on WS_EX_TOOLWINDOW
                return cp;
            }
        }
        private void txtTweet_TextChanged(object sender, EventArgs e)
        {
            lblCount.Text = (140 - txtTweet.Text.Length).ToString();
        }

        private void txtTweet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                SendTweet();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormSettings dlgSettings = new FormSettings(_oAuth);
            if (DialogResult.OK == dlgSettings.ShowDialog())
            {
                txtTweet.Enabled = btwTweet.Enabled = IsConfigured;
            }
        }

        public bool IsConfigured
        {
            get
            {
                return !String.IsNullOrEmpty(Settings1.Default.token) &&
                    !String.IsNullOrEmpty(Settings1.Default.secretToken) &&
                    !String.IsNullOrEmpty(Settings1.Default.consumerKey) &&
                    !String.IsNullOrEmpty(Settings1.Default.consumerSecret) &&
                    !String.IsNullOrEmpty(Settings1.Default.pin);
                 }
        }

        private oAuthTwitter _oAuth = new oAuthTwitter();

        private void Form1_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                DIPS.Properties.Locations.Default.TwitterL = this.DesktopLocation;
                DIPS.Properties.Locations.Default.Save();
            }
        }

    }
}
