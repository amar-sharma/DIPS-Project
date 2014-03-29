using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Collections.Specialized;

namespace DIPS.FeedBack
{
    public partial class FeedBk : Form
    {
       static bool ok = true;
        public FeedBk()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void bSend_Click(object sender, EventArgs e)
        {
            if (tBfeedB.Text.Length != 0)
                SubmitGoogleDoc();
            else
                MessageBox.Show("Atleast Write Something!!");
        }

        void wc_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
        {
                //Submit Complete
            try
            {
                e.Result.ToString();
                if (ok)
                    MessageBox.Show("       Thanks For feedback!! \r\n Your feedback is valuable to us");
            }
            catch
            {
                MessageBox.Show("     Sending failed !!\r\n  Check your connection settings !");
            }
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

        void SubmitGoogleDoc(){
            //Use WebClient Class to submit a new entry
            try
            {
                this.Proxify();
                WebClient wc = new WebClient();
                var keyval = new NameValueCollection();
                keyval.Add("entry.157740679", tBfeedB.Text);

                keyval.Add("submit", "Submit");

                //Create an event for Submit Complete
                wc.UploadValuesCompleted +=
                new UploadValuesCompletedEventHandler(wc_UploadValuesCompleted);
                //Create Additional Headers  

                wc.Headers.Add("Origin", "https://docs.google.com");
                wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US) AppleWebKit/534.10 (KHTML, like Gecko) Chrome/8.0.552.224 Safari/534.10");

                //Finally Submit the Form:
                wc.UploadValuesAsync(new Uri
                ("https://docs.google.com/forms/d/1zTC-ukSV5iZ3Ao3opye7GqVMaZWl16j43MbPnyzfWOg/formResponse"),
                "POST", keyval, Guid.NewGuid().ToString());
                //wc.ResponseHeaders.ToString();

            }
            catch
            {
                
                ok = false;
            }
            
        }

        private void FeedBk_Load(object sender, EventArgs e)
        {

        } 
    }
}
