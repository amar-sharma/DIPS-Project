using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Facebook;
using System.Net;
using Microsoft.VisualBasic;

namespace FacebookingTest
{
    public partial class Authorize : Form
    {
        public Authorize()
        {
            InitializeComponent();

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

        public static IWebProxy DefaultWebProxy { get; set; }

        public string AppSecret
        {
            get
            {
                return ConfigurationManager.AppSettings["ApplicationSecret"];
            }
        }

        public string AccessToken { get; set; }

        private void LoadAuthorize(object sender, EventArgs e)
        {
            var destinationURL = String.Format(
                @"https://www.facebook.com/dialog/oauth?client_id={0}&scope={1}&redirect_uri=http://www.facebook.com/connect/login_success.html&response_type=token",
                this.ApplicationId,
                this.ExtendedPermissions);
            webBrowser.Navigated += WebBrowserNavigated;
            webBrowser.Navigate(destinationURL);
        }

         private void WebBrowserNavigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            // get token
            
            String username = "mukul.me11";
            String password = "kumarmukul";
           
            IWebProxy proxyObject = new WebProxy("http://172.16.1.10:3128",true);

           // MessageBox.Show(username+" pass "+password);
            proxyObject.Credentials = new NetworkCredential(username, password);
            FacebookClient.SetDefaultHttpWebRequestFactory(uri =>
            {
                var request = new HttpWebRequestWrapper((HttpWebRequest)WebRequest.Create(uri));
                request.Proxy = proxyObject;
                return request;
            });
            var url = e.Url.Fragment;
            if (url.Contains("access_token") && url.Contains("#"))
            {
                this.Hide();
                url = (new Regex("#")).Replace(url, "?", 1);
                this.AccessToken = System.Web.HttpUtility.ParseQueryString(url).Get("access_token");
                MessageBox.Show(this.AccessToken);
                //MessageBox.Show(ConfigurationManager.AppSettings["ExtendedPermissions"]);
                try
                {
                    //var facebooking = new FacebookingTest(facebookCore.AccessToken);
                    //facebooking.UpdateStatus
                    var fb = new FacebookClient(this.AccessToken);
                    dynamic t = fb.Get("me");
                    MessageBox.Show(t.bio);
                    dynamic result = fb.Post("me/feed", new { message = "David guetta + Nicki minaj ... kyoki pagalpanti bhi jaruri he :P" });
                    var newPostId = result.id;
                    MessageBox.Show(result.id);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString());
                    this.Dispose();
                }
                this.Dispose();
            }

        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //this.Dispose();
        }
    }
}
