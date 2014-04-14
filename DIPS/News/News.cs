/*
 Name: RSSToolbar 
 Author: Neal T Bailey (nealbailey@hotmail.com)
 Purpose: Fetch Rss feeds
 License: Creative Commons (must reference original author)
 
 Notes: This was a .NET v1.1 project that was upgraded to .NET v2.0 
 
*/

using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Rss;
using System.Net;
using System.Runtime.InteropServices;

namespace DIPS.News
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{

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


        //Threaded Ui
        private delegate void UpdateUiCallback(string news);
        private IniDataReader _iniReader;
        private string _htmlFile;
        private int _maxHeadlines = 0;
        private int _maxStories = 0;
        
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel1;
        private Button button1;
        private PictureBox pictureBox1;
		private System.ComponentModel.IContainer components;
        bool FLAG = false;
		public Form1()
		{
			InitializeComponent();
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


		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 19);
            this.label1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(8, 64);
            this.textBox1.MaxLength = 0;
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(296, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "textBox1";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(737, 28);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DIPS.Properties.Resources.refresh;
            this.pictureBox1.Location = new System.Drawing.Point(711, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(26, 27);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Left;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(25, 28);
            this.button1.TabIndex = 2;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(737, 28);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "News";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.Load += new System.EventHandler(this.Form1_Load);
            this.VisibleChanged += new System.EventHandler(this.Form1_VisibleChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
		#endregion

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x80;  // Turn on WS_EX_TOOLWINDOW
                return cp;
            }
        }
	
		private void Form1_Load(object sender, System.EventArgs e)
		{

            this.button1.Height = panel1.Height;
            this.label1.Height = panel1.Height;		
			_iniReader = new IniDataReader(Application.StartupPath + "\\settings.ini");
            _maxHeadlines = _iniReader.GetIntValue("maxheadlines");
            _maxStories = _iniReader.GetIntValue("maxstories");
            _htmlFile = String.Format("{0}\\{1}.htm", 
                Path.GetTempPath(), Path.GetFileName(Path.GetTempFileName()));

            foreach (string fil in Directory.GetFiles(Path.GetTempPath(), "*.htm", SearchOption.TopDirectoryOnly))
            {
                File.Delete(fil);
            }

            label1.Font = new Font(_iniReader.GetStringValue("fontface"), _iniReader.GetIntValue("fontsize"), FontStyle.Regular);
			this.timer1.Enabled = true;
            this.timer1.Interval = _iniReader.GetIntValue("speed");
            InitiateThread();			
		}

        private void InitiateThread()
        {
            this.textBox1.Text = "DIPS Panel is Loading latest News Please wait ....";
            Thread rssStart = new Thread(new ThreadStart(StartRssThread));
            rssStart.IsBackground = true;
            rssStart.Start();
        }

        private void UpdateUi(string news)
        {
            textBox1.Text = news;
        }
        List<RssChannel> channels;
		//Start Rss Parsing
		private void StartRssThread()
		{
            this.Proxify();
            channels = new List<RssChannel>();
			StringBuilder mergedFeed =  new StringBuilder();
            int mh = 0;
            
			foreach (string rssUrl in PopulateUrls())
            {
                int ms = 0;
                if (mh < _maxHeadlines)
                {
                    try
                    {
                        RssFeed DaFeed = RssFeed.Read(rssUrl);
                        RssChannel DaChannel = (RssChannel)DaFeed.Channels[0];
                        channels.Add(DaChannel);
                        mergedFeed.AppendFormat(" {0}: ", DaChannel.Title);

                        foreach (RssItem sTrm in DaChannel.Items)
                        {
                            if (ms < _maxStories)
                            {
                                mergedFeed.AppendFormat(" {0} |", sTrm.Title);
                                ms++;
                                mh++;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // MessageBox.Show(ex.Message);
                    }
                }
               FLAG = true;
			}
            string dafeed = mergedFeed.ToString();
            mergedFeed = null;
            textBox1.Invoke(new UpdateUiCallback(this.UpdateUi), new string[] { dafeed });
            
		}

        private void MakeHtml(List<RssChannel> feeds)
        {
            StringBuilder sb = new StringBuilder();
            String htmlheader = String.Format("<html><head></head><body style=\"margin-left: 150px;font-family: Arial, sans-serif;font-size: 10pt;\">");
            sb.Append(htmlheader);

            foreach (RssChannel feed in feeds)
            {
                sb.Append("<p></p>");
                sb.AppendFormat("<b>{0}:</b><p></p>", feed.Title);
                foreach (RssItem sTrm in feed.Items)
                {
                    sb.AppendFormat("<a style=\"text-decoration: none;\" href=\"{0}\">{1}</a><br />", sTrm.Link, sTrm.Title);
                }
            }
            sb.Append("</body></html>");
            
            StreamWriter sw = File.CreateText(_htmlFile);
            sw.Write(sb.ToString());
            sw.Flush();
            sw.Close();
        }

		private List<string> PopulateUrls()
		{
           this.Proxify();
            List<string> alUrls = new List<string>();

			for (int i = 1; i <= 5; i++)
			{
				string pVal = _iniReader.GetStringValue(Convert.ToString(i));
				if (!String.IsNullOrEmpty(pVal))
				{
					alUrls.Add(pVal);
				}				
			}
			return alUrls;
		}

		//Regular Expression to Remove HTML/XML tags
		private string StripTags(string HTML)
		{
			return Regex.Replace(HTML, "<[^>]*>", "");
		}

		private void textBox1_TextChanged(object sender, System.EventArgs e)
		{
			//this.label1.
			this.label1.Text = this.textBox1.Text;
		}

		private void timer1_Tick(object sender, System.EventArgs e)
		{
				this.label1.Left = this.label1.Left - 2;
				if (this.label1.Left + this.label1.Width < 0)
				{
				this.label1.Left = this.panel1.Width;
				} 
		}

        private void button1_Click(object sender, EventArgs e)
        {
            if (FLAG)
            {
                try
                {
                    MakeHtml(channels);
                    System.Diagnostics.Process.Start(_htmlFile);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Fetching all news please wait ...");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                Properties.Locations.Default.NewsL = this.DesktopLocation;
                Properties.Locations.Default.Save();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FLAG = false;
            InitiateThread();
        }
	}
}
