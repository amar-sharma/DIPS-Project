using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Facebook;
using System.Web;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Net;
using Microsoft.VisualBasic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace DIPS
{
    public partial class FacebookDIPS : Form
    {
        public Boolean NotificatioSeen;
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

        public FacebookDIPS()
        {
            InitializeComponent();
            ShowInTaskbar = false;

        }

        public dynamic result;
        public dynamic pics;

        private void GetNotifications()
        {
            try
            {
                var fb = new FacebookClient(Properties.Settings.Default.AccesToken);

                dynamic t = fb.Get("me");
                result = fb.Get("fql", new { q = "SELECT title_text,title_html,is_unread FROM notification WHERE recipient_id = " + t.id });
                // pics = fb.Get("fql", new { q = "SELECT pic FROM profile WHERE id IN (SELECT sender_id FROM notification WHERE recipient_id = " + t.id + ")" });

            }
            catch (Exception exception)
            {
                if (exception.ToString().Contains("Proxy") || exception.ToString().Contains("sever") || exception.ToString().Contains("407"))
                    MessageBox.Show("Please check your internet connection or proxy settings !");
            }
        }

        private void tbPost_Click(object sender, EventArgs e)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.DoWork += new DoWorkEventHandler(
            delegate(object o, DoWorkEventArgs args)
            {
                BackgroundWorker b = o as BackgroundWorker;

                if (Properties.Settings.Default.IsProxy)
                {

                    Proxify();
                    PostStatus();
                }
                else
                {
                    PostStatus();
                }
            });
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
            delegate(object o, RunWorkerCompletedEventArgs args)
            {
                tBStatus.Clear();
            });

            bw.RunWorkerAsync();

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

        private void PostStatus()
        {
            try
            {
                var fb = new FacebookClient(Properties.Settings.Default.AccesToken);
                fb.Post("me/feed", new { message = tBStatus.Text });

            }
            catch (Exception exception)
            {
                if (exception.ToString().Contains("Proxy") || exception.ToString().Contains("sever") || exception.ToString().Contains("407"))
                    MessageBox.Show("Please check your internet connection or proxy settings !");
            }
        }

        private void FacebookDIPS_Load(object sender, EventArgs e)
        {
            this.Height = 145;
        }
        Boolean Clicked = false;

        private void Notify_Click(object sender, EventArgs e)
        {
            if (!Properties.Settings.Default.hasAuthorized)
            {
                MessageBox.Show(" You need to authorize first !!");
                FBAuthorize fbauth;
                fbauth = new FBAuthorize();
                fbauth.Show();
                return;
            }
            if (!Clicked)
            {
                NotificationList.Visible = true;
            }
            else
            {
                NotificationList.Visible = false;
                this.Height = 145;
                Clicked = false;
            }
            BackgroundWorker bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.DoWork += new DoWorkEventHandler(
            delegate(object o, DoWorkEventArgs args)
            {
                BackgroundWorker b = o as BackgroundWorker;
                if (Properties.Settings.Default.IsProxy)
                {
                    Proxify();
                    GetNotifications();
                }
                else
                {
                    GetNotifications();
                }
                Clicked = true;
            });

            // what to do when progress changed (update the progress bar for example)
            bw.ProgressChanged += new ProgressChangedEventHandler(
            delegate(object o, ProgressChangedEventArgs args)
            {
            });

            // what to do when worker completes its task (notify the user)
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
            delegate(object o, RunWorkerCompletedEventArgs args)
            {

                /* using (WebClient webClient = new WebClient())
                 {
                     for (int i = 0; i < 10; i++)
                     {
                         // MessageBox.Show(pics.data[i].pic_square);
                         ImageList il = new ImageList();
                         var url = new Uri(pics.data[i].pic);
                         byte[] bitmapData;
                         bitmapData = webClient.DownloadData(url);

                         // Bitmap data => bitmap => resized bitmap.            
                         using (MemoryStream memoryStream = new MemoryStream(bitmapData))
                         using (Bitmap bitmap = new Bitmap(memoryStream))
                         using (Bitmap resizedBitmap = new Bitmap(bitmap, 50, 50))
                         {
                             il.Images.Add(resizedBitmap);
                         }
                         NotificationList.LargeImageList = il;
                     }
                 }*/
                NotificationList.View = View.List;
                NotificationList.Clear();
                NotificationList.MultiSelect = false;
                NotificationList.ShowItemToolTips = true;
                try
                {
                    for (int i = 0; i < 20; i++)
                    {
                        ListViewItem lvi = new ListViewItem();
                        //lvi.ImageIndex = i;
                        lvi.BackColor = result.data[i].is_unread == 1 ? Color.FromKnownColor(KnownColor.ActiveCaption) : Color.White;
                        lvi.Text = result.data[i].title_text;
                        lvi.Tag= result.data[i].title_html;
                        lvi.ToolTipText = result.data[i].title_text;
                        NotificationList.Items.Add(lvi);
                    }
                }
                catch
                {
                    this.Height = 145;
                }

            });

            bw.RunWorkerAsync();

        }


        private void FacebookDIPS_MouseLeave(object sender, EventArgs e)
        {
            int x = this.PointToClient(Control.MousePosition).X;
            int y = this.PointToClient(Control.MousePosition).Y;
            if (y >= 530 || x < -2 || y < -2 || x > 370)
            {
                NotificationList.Visible = false;
                this.Height = 145;
            }
        }

        private void FacebookDIPS_Shown(object sender, EventArgs e)
        {
            /*WebClient webClient = new WebClient();
            var url = new Uri(pic.data[0].pic);
            byte[] bitmapData;
            bitmapData = webClient.DownloadData(url);
            MemoryStream memoryStream = new MemoryStream(bitmapData);
            Bitmap bitmap = new Bitmap(memoryStream);
            Bitmap resizedBitmap = new Bitmap(bitmap, 84, 84);
            pics = fb.Get("fql", new { q = "SELECT pic FROM profile WHERE id IN (SELECT sender_id FROM notification WHERE recipient_id = " + t.id + ")" });
    
           */
        }

        private void FacebookDIPS_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                Properties.Locations.Default.FBL = this.DesktopLocation;
                Properties.Locations.Default.Save();
            }

        }

    }
}
