using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Threading;

namespace DIPS
{
    public partial class CurrencyConv : Form
    {
        public CurrencyConv()
        {
            InitializeComponent();
        }

        double rate;
        bool gotit = false;
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
        private void CurrencyConv_Load(object sender, EventArgs e)
        {

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
        private void bConv_Click(object sender, EventArgs e)
        {
            if (cBFrom.Text.Equals("Currency"))
                MessageBox.Show("Please Select From Currency !");
            else if (CBTo.Text.Equals("Currency"))
                MessageBox.Show("Please Select To Currency !");
            else if (tbValue.Text.Length == 0)
                MessageBox.Show("Please Specify Value to be Converted!");
            else
            {
                tBresult.Text = " Calculating Please Wait ..... "; 
                String url = "http://www.google.com/ig/calculator?hl=en&q=" + "1" + cBFrom.Text.Substring(0, 3).ToUpper() + "%3D%3F" + CBTo.Text.Substring(0, 3).ToUpper();
                BackgroundWorker bw = new BackgroundWorker();
                bw.WorkerReportsProgress = true;
                bw.DoWork += new DoWorkEventHandler(
                delegate(object o, DoWorkEventArgs args)
                {
                    BackgroundWorker b = o as BackgroundWorker;
                    GetRatio(url);
                    //Do
                });

               bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                delegate(object o, RunWorkerCompletedEventArgs args)
                {
                    if (gotit)
                    {
                        double Result = System.Convert.ToDouble(tbValue.Text) * rate;
                        tBresult.Text = tbValue.Text + " " + cBFrom.Text.Substring(0, 3).ToUpper() + " = " + Result.ToString() + " " + CBTo.Text.Substring(0, 3).ToUpper();
                    }
                    else
                    {
                        tBresult.Text = " Some error Occured!";
                    }
                    //Done
                });

                bw.RunWorkerAsync();
            }
        }

        private void GetRatio(String url)
        {
            try
            {
                this.Proxify();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader resStream = new StreamReader(response.GetResponseStream());
                String s = resStream.ReadToEnd();
                Regex regex = new Regex("rhs: \\\"(\\d*.\\d*)");
                rate = System.Convert.ToDouble(regex.Match(s).Groups[1].Value);
                gotit = true;
            }
            catch (Exception ex)
            {
                gotit = false;
                MessageBox.Show("An Error has occured \n Please Check your Internet Connection"+  ex.ToString());
            }
        }

        private void tbValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void CurrencyConv_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                Properties.Locations.Default.CcL = this.DesktopLocation;
                Properties.Locations.Default.Save();
            }
        }

        private void CurrencyConv_DoubleClick(object sender, EventArgs e)
        {
            //MessageBox.Show(this.DesktopLocation.ToString());
        }

    }
}
