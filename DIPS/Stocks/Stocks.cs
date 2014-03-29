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
using System.Threading;

namespace DIPS.Stocks
{
    public partial class Stocks : Form
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
        private delegate void UpdateUiCallback(string news);
        public void proxify()
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

        public Stocks()
        {
            InitializeComponent();
        }

        private static String GetQuoteFromWebSite(String strsymbol)
        {
            string fullpath =
                @"http://quote.yahoo.com/d/quotes.csv?s=" + strsymbol + "&f=sl1d1t1c1ohgvj1pp2owern&e=.csv";

            HttpWebRequest req;
            HttpWebResponse res;
            StreamReader sr;

            string strResult;
            try
            {
                req = (HttpWebRequest)WebRequest.Create(fullpath);
                res = (HttpWebResponse)req.GetResponse();
                sr = new StreamReader(res.GetResponseStream(), Encoding.ASCII);
                strResult = sr.ReadLine();
                sr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                strResult = "Some Error";
            }
            return strResult;
        }
        String str;

        private void getQuote_Click(object sender, EventArgs e)
        {
            GetResult();
        }

        private void GetResult()
        {
            Thread getQuotes = new Thread(new ThreadStart(FetchQuotes));
            getQuotes.IsBackground = true;
            getQuotes.Start();
        }

        void FetchQuotes()
        {
            this.proxify();
            str = company.Text.ToUpper();
            string strcurindex;
            string strcurdate;
            //string strQuotes;
            char[] separator = { ',' };
            //strQuotes = "";
            string strResult = GetQuoteFromWebSite(str);
            string[] temp = strResult.Split(separator);
            //Check if the string array returned has more than one elements
            //since if there are less than one elements then a exception must have been returned 
            if (temp.Length > 1)
            {
                //The WebService returns a lot of information about the stock,
                //We only show the relevant portions .
                strcurindex = "Current Index for " + str + " = " + temp[1]; //Set the label to current Index
                strcurdate = temp[2] + " at " + temp[3];  //Set the label to current Date Time
                str = strcurindex + System.Environment.NewLine + "Last updated: " + strcurdate + System.Environment.NewLine;
            }
            else
            {
                //strerror = "Error :" + strResult; //set the error label
                //strQuotes = strQuotes + strerror + System.Environment.NewLine;
            }
            output.Invoke(new UpdateUiCallback(this.UpdateTb), new string[] { str });
        }

        private void Form1_Load(object sender, EventArgs e)
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
        void UpdateTb(string quots)
        {
            output.Text = quots;
            company.Text = null;
        }

        private void Stocks_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                Properties.Locations.Default.StocksL = this.DesktopLocation;
                Properties.Locations.Default.Save();
            }
        }

        private void company_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetResult();
            }
        }

    }
}
