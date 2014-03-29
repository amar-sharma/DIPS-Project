using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace DIPS.Weather
{
    public partial class WeatherPanel : Form
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

        public WeatherPanel()
        {
            InitializeComponent();
        }

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

        private void button1_Click(object sender, EventArgs e)
        {
            GetWeather();
        }
        Conditions conditions;
        private void GetWeather()
        {
            try
            {
                BackgroundWorker bw = new BackgroundWorker();
                bw.WorkerReportsProgress = true;
                bw.DoWork += new DoWorkEventHandler(
                delegate(object o, DoWorkEventArgs args)
                {
                    BackgroundWorker b = o as BackgroundWorker;

                    this.Proxify();
                    try
                    {
                        conditions = new Conditions();
                        conditions = Weather.GetCurrentConditions(this.city.Text);//Do
                    }
                    catch
                    {
                        MessageBox.Show("Please check your internet connection !!");
                    }
                });
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                delegate(object o, RunWorkerCompletedEventArgs args)
                {
                    if (conditions != null)
                    {
                        this.label6.Text = "Condition: " + conditions.Condition;
                        this.label7.Text = "Temperature(C): " + conditions.TempC;
                        this.label8.Text = conditions.Humidity;
                        this.label9.Text = conditions.Wind;
                    }
                    else
                    {
                        MessageBox.Show("There was an error processing the request in weather.");
                        this.label6.Text = "Error Occured";
                    }//Done
                });

                bw.RunWorkerAsync();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Weather_Load(object sender, EventArgs e)
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
        private void WeatherPanel_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                Properties.Locations.Default.WeatherL = this.DesktopLocation;
                Properties.Locations.Default.Save();
            }
        }

        private void city_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetWeather();
            }
        }

    }
}
