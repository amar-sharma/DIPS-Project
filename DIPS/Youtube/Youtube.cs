//Copy rights are reserved for Akram kamal qassas
//Email me, Akramnet4u@hotmail.com
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading;

namespace DIPS.Youtube
{
    public partial class youtubeForm : Form
    {
        public youtubeForm()
        {
            InitializeComponent();
            timer1.Enabled = true;
            this.Width = 48;
            this.Height = 137;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.Disposed += new EventHandler(backgroundWorker1_Disposed);
        }

        void backgroundWorker1_Disposed(object sender, EventArgs e)
        {
            MessageBox.Show("OK");
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
                if(DIPS.Properties.Settings.Default.ProxyAuth)
                proxyObject.Credentials = new NetworkCredential(username, password);
                HttpWebRequest.DefaultWebProxy = proxyObject;
            }
            else
            {
                HttpWebRequest.DefaultWebProxy = null;
            }
            
        }
        string[] videoUrls;
        //List<string> downVideoUrls = new List<string>();
        List<YouTubeVideoQuality> downVideoUrls = new List<YouTubeVideoQuality>();

        private void down_Button_Click(object sender, EventArgs e)
        {
            try
            {
                proxify();
                YouTubeVideoQuality tempItem = downVideoUrls[quality_ComboBox.SelectedIndex];

                saveFileDialog1.Filter = String.Format("{0} Files|*.{1}", tempItem.Extention.ToUpper(), tempItem.Extention.ToLower());
                //saveFileDialog1.FileName = String.Format("{0}.{1}", tempItem.VideoTitle, tempItem.Extention.ToLower());
                saveFileDialog1.FileName = FormatTitle(tempItem.VideoTitle);

                if (DialogResult.OK != saveFileDialog1.ShowDialog(this)) return;
                new frmFileDownloader(tempItem.DownloadUrl, saveFileDialog1.FileName).Show(this);
            }
            catch (Exception ex) { MessageBox.Show(this, ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = YouTubeDownloader.GetYouTubeVideoUrls(videoUrls);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                UseWaitCursor = false;
                if (e.Error != null)
                    throw e.Error;

                List<YouTubeVideoQuality> urls = e.Result as List<YouTubeVideoQuality>;

                TimeSpan videoLength = TimeSpan.FromSeconds(urls[0].Length);
                if (videoLength.Hours > 0)
                { drawVideoLenght(String.Format("{0}:{1}:{2}", videoLength.Hours, videoLength.Minutes, videoLength.Seconds)); }
                else
                { drawVideoLenght(String.Format("{0}:{1}", videoLength.Minutes, videoLength.Seconds)); };

                foreach (var item in urls)
                {
                    string videoExtention = item.Extention;
                    string videoDimension = formatSize(item.Dimension);
                    string videoSize = formatSizeBinary(item.VideoSize);
                    //string videoTitle = item.VideoTitle.Replace(@"\", "").Replace("&#39;", "'").Replace("&quot;", "'").Replace("&lt;", "(").Replace("&gt;", ")").Replace("+", " ");

                    quality_ComboBox.Items.Add(String.Format("{0} ({1}) - {2}", videoExtention.ToUpper(), videoDimension, videoSize));
                    quality_ComboBox.Text = quality_ComboBox.Items[0].ToString();
                    quality_ComboBox.Enabled = true;
                    downVideoUrls.Add(item);
                    name_Label2.Text = FormatTitle(item.VideoTitle);
                    url_TextBox.Clear();
                    url_TextBox.Enabled = true;
                    timer1.Enabled = true;
                    progressBar1.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Check your Internet Connection or "+ex.Message);
                paste_Button.Enabled = true;
                url_TextBox.Enabled = true;
                timer1.Enabled = true;
                progressBar1.Hide();
            }
        }

        private string formatSize(object value)
        {
            string s = ((Size)value).Height >= 720 ? " HD" : "";
            if (value is Size) return ((Size)value).Width + " x " + ((Size)value).Height + s;
            return "";
        }

        private string formatSizeBinary(Int64 size, Int32 decimals = 2)
        {
            string[] sizes = { "Bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
            double formattedSize = size;
            Int32 sizeIndex = 0;
            while (formattedSize >= 1024 & sizeIndex < sizes.Length)
            {
                formattedSize /= 1024;
                sizeIndex += 1;
            }
            return string.Format("{0} {1}", Math.Round(formattedSize, decimals).ToString(), sizes[sizeIndex]);
        }

        public static string FormatTitle(string title)
        {
            return title.Replace(@"\", "").Replace("&#39;", "'").Replace("&quot;", "'").Replace("&lt;", "(").Replace("&gt;", ")").Replace("+", " ").Replace(":", "-");
        }

        private void get_Button_Click(object sender, EventArgs e)
        {
            this.Height = 288;
            
            
                try
                {
                    this.proxify();

                    if (!Helper.isValidUrl(url_TextBox.Text) || !url_TextBox.Text.ToLower().Contains("www.youtube.com/watch?"))
                        MessageBox.Show(this, "You enter invalid YouTube URL, Please correct it.\r\n\nNote: URL should start with:\r\nhttp://www.youtube.com/watch?",
                            "Invalid URL", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                    {
                        downVideoUrls.Clear();
                        url_TextBox.Enabled = false;
                        get_Button.Enabled = false;
                        timer1.Enabled = false;
                        paste_Button.Enabled = false;
                        progressBar1.Show();

                        this.videoUrls = new string[] { url_TextBox.Text };
                        video_PictureBox.ImageLocation = string.Format("http://i3.ytimg.com/vi/{0}/default.jpg", Helper.GetVideoIDFromUrl(url_TextBox.Text));
                        backgroundWorker1.RunWorkerAsync();
                        //UseWaitCursor = true;
                        //ShowPanel(1);
                    }
                }
                catch (Exception ex)
                { 
                    MessageBox.Show(ex.ToString());
                }
         
        }

        private void copy_Button_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(downVideoUrls[quality_ComboBox.SelectedIndex].DownloadUrl);
                MessageBox.Show(this, "URL copied to clipboard", "YouTube Downloader", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(this, ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void textBoxUrl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                get_Button_Click(null, null);
            }
        }

        private void url_TextBox_TextChanged(object sender, EventArgs e)
        {
            get_Button.Enabled = !String.IsNullOrEmpty(url_TextBox.Text);

            video_PictureBox.Image = null;
            downVideoUrls.Clear();
            quality_ComboBox.Items.Clear();
            quality_ComboBox.Enabled = false;
            name_Label2.Text = "--";
        }

        private void quality_ComboBox_EnabledChanged(object sender, EventArgs e)
        {
            down_Button.Enabled = quality_ComboBox.Enabled;
            copy_Button.Enabled = quality_ComboBox.Enabled;
        }

        private void paste_Button_Click(object sender, EventArgs e)
        {
            url_TextBox.Clear();
            url_TextBox.Text = Clipboard.GetText().Trim();
        }

       

        private void timer1_Tick(object sender, EventArgs e)
        {
            paste_Button.Enabled = !String.IsNullOrEmpty(Clipboard.GetText());
        }

        private void grop_Panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Gainsboro), new Rectangle(0, 0, grop_Panel1.Width - 1, grop_Panel1.Height - 1));
            e.Graphics.DrawRectangle(new Pen(Color.White), new Rectangle(1, 1, grop_Panel1.Width - 3, grop_Panel1.Height - 3));
        }

        private void grop_Panel2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Gainsboro), new Rectangle(0, 0, grop_Panel2.Width - 1, grop_Panel2.Height - 1));
            e.Graphics.DrawRectangle(new Pen(Color.White), new Rectangle(1, 1, grop_Panel2.Width - 3, grop_Panel2.Height - 3));
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

            e.Graphics.DrawLine(new Pen(Color.Silver, 2), new Point(0, 1), new Point(panel1.Width, 1));

        }

        private void drawVideoLenght(string lenght)
        {

            video_PictureBox.Refresh();

            Graphics e = video_PictureBox.CreateGraphics();
            Font mFont = new Font(this.Font.Name, 10.0F, FontStyle.Bold, GraphicsUnit.Point);
            SizeF mSize = e.MeasureString(lenght, mFont);
            Rectangle mRec = new Rectangle((int)(video_PictureBox.Width - mSize.Width - 6),
                                           (int)(video_PictureBox.Height - mSize.Height - 6),
                                           (int)(mSize.Width + 2),
                                           (int)(mSize.Height + 2));

            e.FillRectangle(new SolidBrush(Color.FromArgb(200, Color.Black)), mRec);
            e.DrawString(lenght, mFont, new SolidBrush(Color.Gainsboro), new PointF((video_PictureBox.Width - mSize.Width - 5),
                                                                                (video_PictureBox.Height - mSize.Height - 5)));
            e.Dispose();
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
        private void mainForm_Load(object sender, EventArgs e)
        {

        }
        bool ignore = false;
        public void HideFull()
        {
            if (backgroundWorker1.IsBusy)
                return;
            while ((this.Width > 132 || this.Height > 137))
            {
                if (this.Width != 132)
                    this.Width -= 1;
                if (this.Height != 137)
                    this.Height -= 1;
                Application.DoEvents();
                
            }
                   
        }
        public void Animateresize(int width, int height,int stepx,int stepy)
        {
            if (!ignore)
                return;
            else
            {
                while (this.Width < width || this.Height < height)
                {
                    if (this.Width != width)
                        this.Width += stepx;
                    if (this.Height != height)
                        this.Height += stepy;
                    Application.DoEvents();
                }
            }
        }
        void showme()
        {
          ignore = true;
          Animateresize(450, 137, 2, 2);
          ignore = false;
        }


        private void bCancel_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.WorkerSupportsCancellation = true;
            timer1.Enabled = true;
            url_TextBox.Enabled = true;
            url_TextBox.Clear();
            this.Width = 450;
            this.Height = 137;

        }

        private void youtubeForm_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                Properties.Locations.Default.YoutubeL = this.DesktopLocation;
                Properties.Locations.Default.Save();
            }
        }



        private void logo_PictureBox_Click(object sender, EventArgs e)
        {
            showme();
        }

    }
}
