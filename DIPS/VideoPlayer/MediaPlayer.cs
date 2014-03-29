using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace DIPS.VideoPlayer
{
    
    public partial class MediaPlayer : Form
    {
        public MediaPlayer()
        {
            InitializeComponent();
        }

        public bool Outb = false;

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

        private void Player_Enter(object sender, EventArgs e)
        {
           //Player.URL = @"E:\d.mp4.mp4";
           //Player.currentPlaylist = Player.mediaCollection.getByName("mediafile");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd=new OpenFileDialog();
            FolderBrowserDialog fbd=new FolderBrowserDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string sourceFile = ofd.FileName;
                Player.URL = sourceFile;

            }
         }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void MediaPlayer_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                Properties.Locations.Default.MediaPlayer = this.DesktopLocation;
                Properties.Locations.Default.Save();
            }
        }

        private void MediaPlayer_LocationChanged(object sender, EventArgs e)
        {
            if (MainDIPS.Paneldips.DesktopLocation.X > this.DesktopLocation.X)
            {
                Outb = true;
            }
            else
            {
                Outb = false;
               
            }
        }

        private void MediaPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            PlayerInit.my = null;
        }
     }
}
