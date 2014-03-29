using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Facebook;
using Microsoft.Win32;

namespace DIPS
{
    public partial class MainDIPS : Form
    {
        public static int Screenwidth = Screen.PrimaryScreen.Bounds.Width;
        public static int Screenheight = Screen.PrimaryScreen.Bounds.Height;
        public static DIPSPanel Paneldips = new DIPSPanel(Screenheight);
        public static DIPS.Help.Helpme help;
        public MainDIPS()
        {
            InitializeComponent();
            startWithWindowsToolStripMenuItem.Checked = Properties.Locations.Default.Startup;
            SetStartup();
            Paneldips.SafeLoadOffline();
            Paneldips.SafeLoadOnline();
            this.SetDesktopLocation(Screenwidth - 162, 10);
            if (Properties.Locations.Default.FirstTime)
                 InitLocations();
            if (Properties.Settings.Default.FirstTime)
            {
                Prefrences prefrences = new Prefrences();
                prefrences.Show();
            }


        }
        private void InitLocations()
        {
            Properties.Locations.Default.FBL = new Point(741, 98);
            Properties.Locations.Default.CalcL = new Point(432, 580);
            Properties.Locations.Default.SudokuL = new Point(1190, 212);
            Properties.Locations.Default.VideoPlay = new Point(1193, 400);
            Properties.Locations.Default.GoogleSearchL = new Point(744, 25);
            Properties.Locations.Default.TwitterL = new Point(432, 580);
            Properties.Locations.Default.NewsL = new Point(572, 653);
            Properties.Locations.Default.WeatherL = new Point(390, 327);
            Properties.Locations.Default.StocksL = new Point(432, 580);
            Properties.Locations.Default.CcL = new Point(432, 580);
            Properties.Locations.Default.YoutubeL = new Point(431, 444);
            Properties.Locations.Default.CalenderL = new Point(432, 580);
            Properties.Locations.Default.UnitConvL = new Point(432, 580);
            Properties.Locations.Default.StickyNotes = new Point(388, 23);
            Properties.Locations.Default.SlideShowL = new Point(712,270);
            Properties.Locations.Default.PanelS = new System.Drawing.Size(1080, Screenheight);
            Properties.Locations.Default.Save();
        }
        private void MainDisc_Load(object sender, EventArgs e)
        {
            mainbmp = Properties.Resources.main;
            if (Properties.Locations.Default.FirstTime)
            {
                this.Tray.BalloonTipText = "Please right-click on this icon to set prefrences";
                this.Tray.BalloonTipTitle = "DIPS";
                this.Tray.Visible = true;
                this.Tray.ShowBalloonTip(5);
                this.Tray.BalloonTipClicked += new EventHandler(Tray_BalloonTipClicked);
            }
        }

        void Tray_BalloonTipClicked(object sender, EventArgs e)
        {
            helpToolStripMenuItem.PerformClick();
        }

        private void MainDIPS_Paint(object sender, PaintEventArgs e)
        {
            ImageAttributes mainimgattr = new ImageAttributes();

            mainimgattr.SetColorKey(mainbmp.GetPixel(0, 0), mainbmp.GetPixel(0, 0));

            e.Graphics.FillRectangle(Brushes.Transparent, this.DisplayRectangle); //This also makes the form invisible to the mouse

            GraphicsUnit maingu = GraphicsUnit.Pixel;
            if (Properties.Settings.Default.Rotate)
            {
                while (true)
                {
                    e.Graphics.TranslateTransform((float)mainbmp.Width / 2, (float)mainbmp.Height / 2);
                    //rotate
                    e.Graphics.RotateTransform(1.0F);
                    //move image back
                    e.Graphics.TranslateTransform(-(float)mainbmp.Width / 2, -(float)mainbmp.Height / 2);
                    e.Graphics.DrawImage((Image)mainbmp, Rectangle.Truncate( //need to convert from RectangleF
                         mainbmp.GetBounds(ref maingu)), 0F, 0F, (float)mainbmp.Width, (float)mainbmp.Height,
                         GraphicsUnit.Pixel, mainimgattr);
                    Thread.Sleep(20);
                    Application.DoEvents();
                }
            }
            else
            {
                e.Graphics.DrawImage((Image)mainbmp, Rectangle.Truncate( //need to convert from RectangleF
                         mainbmp.GetBounds(ref maingu)), 0F, 0F, (float)mainbmp.Width, (float)mainbmp.Height,
                         GraphicsUnit.Pixel, mainimgattr);
            }
        }

        private void Tray_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Paneldips.Hider();
            this.Dispose();
            Application.Exit();
        }


        private void prefrencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Paneldips.Dispose();
            // Paneldips.DIPSPanel_Disposed();
            Paneldips.Hider();
            Prefrences pf = new Prefrences();
            pf.Show();
        }

        private void MainDIPS_MouseHover(object sender, EventArgs e)
        {
            if (!Paneldips.IsDisposed && !Paneldips.Visible)
            {
                Paneldips.Visible = true;
                Paneldips.SetDesktopLocation(Screenwidth - Properties.Locations.Default.PanelS.Width - 3, 0);
                Paneldips.SendToBack();
                this.BringToFront();
            }
        }

        private void MainDIPS_FormClosed(object sender, FormClosedEventArgs e)
        {
            Tray.Icon.Dispose();
        }

        void MainDIPS_MouseLeave(object sender, System.EventArgs e)
        {
            //this.BringToFront();
        }

        private void sendFeedBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FeedBack.FeedBk Fback = new FeedBack.FeedBk();
            Fback.Show();
        }
        private void SetStartup()
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (startWithWindowsToolStripMenuItem.Checked)
                rk.SetValue("DIPS", Application.ExecutablePath.ToString());
            else
                rk.DeleteValue("DIPS", false);

        }

        private void startWithWindowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!startWithWindowsToolStripMenuItem.Checked)
            {
                startWithWindowsToolStripMenuItem.Checked = true;
                Properties.Locations.Default.Startup = true;
                Properties.Locations.Default.Save();
                SetStartup();
            }
            else
            {
                startWithWindowsToolStripMenuItem.Checked = false;
                Properties.Locations.Default.Startup = false;
                Properties.Locations.Default.Save();
                SetStartup();
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (help == null)
            {
                help = new Help.Helpme();
                help.Show();
            }
        }

        private void MainDIPS_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Locations.Default.Save();

        }

    }
}