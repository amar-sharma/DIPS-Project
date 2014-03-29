using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace DIPS
{
    public partial class DIPSPanel : Form
    {

        FacebookDIPS FBDIPS;
        public static FBAuthorize fbauth = null;
        CurrencyConv cc;
        Youtube.youtubeForm youTube;
        SudokuGame.NewGame sudoku;
        StickNotes.FormNotes Sticky;
        UnitConv.MainForm Uc;
        VideoPlayer.PlayerInit VideoP;
        Calculator.Calci Calc;
        Stocks.Stocks stockpanel;
        SlideShow.MainForm sShow;
        Weather.WeatherPanel WEATHER;
        Twitter.Form1 TwitterP;
        News.Form1 NEWSP;
        GoogleSearch.GoogleSearch GSearch;
        Calender.Caln Calen;

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
                e.Graphics.Clear(Color.Black);
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

        /*  #region ClickTrough
        [DllImport("user32.dll", SetLastError = true)]

private static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);

[DllImport("user32.dll")]

static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

[DllImport("user32.dll")]

static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

public const int GWL_EXSTYLE = -20;

public const int WS_EX_LAYERED = 0x80000;

public const int WS_EX_TRANSPARENT = 0x20;

public const int LWA_ALPHA = 0x2;

public const int LWA_COLORKEY = 0x1;
#endregion*/


        public DIPSPanel(int Screenheight)
        {
            InitializeComponent(Screenheight);
            /* SetWindowLong(this.Handle, GWL_EXSTYLE,
             (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) ^ WS_EX_LAYERED ^ WS_EX_TRANSPARENT));
             SetLayeredWindowAttributes(this.Handle, 0, 255, LWA_ALPHA);*/

            this.SendToBack();

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

        private void DIPSPanel_Load(object sender, EventArgs e)
        {
            this.SendToBack();
            LoadOnline();
            LoadOffline();

        }


        private void LoadOffline()
        {
            if (Properties.Settings.Default.Sudoku)
            {
                sudoku.Show();
                sudoku.DesktopLocation = Properties.Locations.Default.SudokuL;
            }
            if (Properties.Settings.Default.Sticky)
            {
                Sticky.Show();
                Sticky.DesktopLocation = Properties.Locations.Default.StickyNotes;
            }
            if (Properties.Settings.Default.UConv)
            {
                Uc.Show();
                Uc.DesktopLocation = Properties.Locations.Default.UnitConvL;
            }
            if (Properties.Settings.Default.VideoP)
            {
                VideoP.Show();
                VideoP.DesktopLocation = Properties.Locations.Default.VideoPlay;
                VideoP.SendToBack();
                this.SendToBack();
            }
            if (Properties.Settings.Default.Calc)
            {
                Calc.Show();
                Calc.DesktopLocation = Properties.Locations.Default.CalcL;
            }

            if (Properties.Settings.Default.Slide)
            {
                sShow.Show();
                sShow.DesktopLocation = Properties.Locations.Default.SlideShowL;
            }
            if (Properties.Settings.Default.Calender)
            {
                Calen.Show();
                Calen.DesktopLocation = Properties.Locations.Default.CalenderL;

            }
        }

        public void SafeLoadOnline()
        {
            if (Properties.Settings.Default.GoogleS)
            {
                GSearch = new GoogleSearch.GoogleSearch();
            }

            if (Properties.Settings.Default.Weather)
            {
                WEATHER = new Weather.WeatherPanel();
            }

            if (Properties.Settings.Default.News)
            {
                NEWSP = new News.Form1();
            }

            if (Properties.Settings.Default.Stock)
            {
                stockpanel = new Stocks.Stocks();
            }
            if (Properties.Settings.Default.CConv)
            {
                cc = new CurrencyConv();
            }
            if (Properties.Settings.Default.Youtube)
            {
                youTube = new Youtube.youtubeForm();

            }
            if (DIPS.Properties.Settings.Default.hasAuthorized && Properties.Settings.Default.FacebookPanel)
            {
                FBDIPS = new FacebookDIPS();
            }
            else
            {
                FBDIPS = new FacebookDIPS();
            }
            if (Properties.Settings.Default.TwitterPanel)
            {
                TwitterP = new Twitter.Form1();
            }
        }

        public void SafeLoadOffline()
        {
            if (Properties.Settings.Default.Sudoku)
            {
                sudoku = new SudokuGame.NewGame();
            }
            if (Properties.Settings.Default.Sticky)
            {
                Sticky = new StickNotes.FormNotes();
            }
            if (Properties.Settings.Default.UConv)
            {
                Uc = new UnitConv.MainForm();
            }
            if (Properties.Settings.Default.VideoP)
            {
                VideoP = new VideoPlayer.PlayerInit();
                VideoP.SendToBack();
                this.SendToBack();
            }
            if (Properties.Settings.Default.Calc)
            {
                Calc = new Calculator.Calci();
            }

            if (Properties.Settings.Default.Slide)
            {
                sShow = new SlideShow.MainForm();
            }
            if (Properties.Settings.Default.Calender)
            {
                Calen = new Calender.Caln();
            }
        }

        private void LoadOnline()
        {
            if (Properties.Settings.Default.GoogleS)
            {
                GSearch.Show();
                GSearch.DesktopLocation = Properties.Locations.Default.GoogleSearchL;
            }

            if (Properties.Settings.Default.Weather)
            {
                WEATHER.Show();
                WEATHER.DesktopLocation = Properties.Locations.Default.WeatherL;
            }

            if (Properties.Settings.Default.News)
            {
                NEWSP.Show();
                NEWSP.DesktopLocation = Properties.Locations.Default.NewsL;
            }

            if (Properties.Settings.Default.Stock)
            {
                stockpanel.Show();
                stockpanel.DesktopLocation = Properties.Locations.Default.StocksL;
            }
            if (Properties.Settings.Default.CConv)
            {
                cc.Show();
                cc.DesktopLocation = Properties.Locations.Default.CcL;

            }
            if (Properties.Settings.Default.Youtube)
            {
                youTube.Show();
                youTube.DesktopLocation = Properties.Locations.Default.YoutubeL;

            }
            if (DIPS.Properties.Settings.Default.hasAuthorized && Properties.Settings.Default.FacebookPanel)
            {
                FBDIPS.Show();
                FBDIPS.DesktopLocation = Properties.Locations.Default.FBL;
            }
            else if (Properties.Settings.Default.FacebookPanel)
            {
                if (fbauth == null)
                {
                    MessageBox.Show("You Have not Authorized Yet !!");
                    fbauth = new FBAuthorize();
                    fbauth.Show();
                    FBDIPS.Show();
                    FBDIPS.DesktopLocation = Properties.Locations.Default.FBL;
                }
            }
            if (Properties.Settings.Default.TwitterPanel)
            {
                TwitterP.Show();
                TwitterP.DesktopLocation = Properties.Locations.Default.TwitterL;
            }
        }

        private void DIPSPanel_Shown(object sender, EventArgs e)
        {
            //ShowOnline();

        }
        void DIPSPanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //MessageBox.Show(this.Width.ToString());
        }

        private void ShowOnline()
        {

            if (Properties.Settings.Default.FacebookPanel && FBDIPS != null)
            {
                FBDIPS.Show();
                if(Properties.Settings.Default.AccesToken.Length == 0 && this.Visible && fbauth == null)
                {
                    MessageBox.Show("You Have not Authorized Yet !!");
                    fbauth = new FBAuthorize();
                    fbauth.Show();
                }
                //fbauth.SetDesktopLocation(MainDIPS.Screenwidth - 900, 50);
            }
            if (Properties.Settings.Default.CConv && cc != null)
                cc.Show();
            if (Properties.Settings.Default.Youtube && youTube != null)
                youTube.Show();
            if (Properties.Settings.Default.Stock && stockpanel != null)
                stockpanel.Show();
            if (Properties.Settings.Default.Weather && WEATHER != null)
                WEATHER.Show();
            if (Properties.Settings.Default.TwitterPanel && TwitterP != null)
                TwitterP.Show();
            if (Properties.Settings.Default.News && NEWSP != null)
                NEWSP.Show();
            if (Properties.Settings.Default.GoogleS && GSearch != null)
                GSearch.Show();
            if (Properties.Settings.Default.GoogleS && GSearch == null)
            {
                GSearch = new GoogleSearch.GoogleSearch();
                GSearch.Show();
                GSearch.DesktopLocation = Properties.Locations.Default.GoogleSearchL;
            }

            if (Properties.Settings.Default.Weather && WEATHER == null)
            {
                WEATHER = new Weather.WeatherPanel();
                WEATHER.Show();
                WEATHER.DesktopLocation = Properties.Locations.Default.WeatherL;
            }

            if (Properties.Settings.Default.News && NEWSP == null)
            {
                NEWSP = new News.Form1();
                NEWSP.Show();
                NEWSP.DesktopLocation = Properties.Locations.Default.NewsL;
            }

            if (Properties.Settings.Default.Stock && stockpanel == null)
            {
                stockpanel = new Stocks.Stocks();
                stockpanel.Show();
                stockpanel.DesktopLocation = Properties.Locations.Default.StocksL;
            }
            if (Properties.Settings.Default.CConv && cc == null)
            {
                cc = new CurrencyConv();
                cc.Show();
                cc.DesktopLocation = Properties.Locations.Default.CcL;

            }
            if (Properties.Settings.Default.Youtube && youTube == null)
            {
                youTube = new Youtube.youtubeForm();
                youTube.Show();
                youTube.DesktopLocation = Properties.Locations.Default.YoutubeL;

            }
            if (DIPS.Properties.Settings.Default.hasAuthorized && Properties.Settings.Default.FacebookPanel && FBDIPS == null)
            {
                FBDIPS = new FacebookDIPS();
                FBDIPS.Show();
                FBDIPS.DesktopLocation = Properties.Locations.Default.FBL;
            }
            if (Properties.Settings.Default.TwitterPanel && TwitterP == null)
            {
                TwitterP = new Twitter.Form1();
                TwitterP.Show();
                TwitterP.DesktopLocation = Properties.Locations.Default.TwitterL;
            }

        }

        private void DIPSPanel_VisibleChanged(object sender, EventArgs e)
        {
            this.SendToBack();
            Properties.Locations.Default.FirstTime = false;
            Properties.Locations.Default.Save();
            ShowOnline();
            ShowOffline();
        }

        private void ShowOffline()
        {
            if (Properties.Settings.Default.Sudoku && sudoku == null)
            {
                sudoku = new SudokuGame.NewGame();
                sudoku.Show();
                sudoku.DesktopLocation = Properties.Locations.Default.SudokuL;
            }
            if (Properties.Settings.Default.Sticky && Sticky == null)
            {
                Sticky = new StickNotes.FormNotes();
                Sticky.Show();
                Sticky.DesktopLocation = Properties.Locations.Default.StickyNotes;
            }
            if (Properties.Settings.Default.UConv && Uc == null)
            {
                Uc = new UnitConv.MainForm();
                Uc.Show();
                Uc.DesktopLocation = Properties.Locations.Default.UnitConvL;
            }
            if (Properties.Settings.Default.VideoP && VideoP == null)
            {
                VideoP = new VideoPlayer.PlayerInit();
                VideoP.Show();
                VideoP.DesktopLocation = Properties.Locations.Default.VideoPlay;
                VideoP.SendToBack();
                this.SendToBack();
            }
            if (Properties.Settings.Default.Calc && Calc == null)
            {
                Calc = new Calculator.Calci();
                Calc.Show();
                Calc.DesktopLocation = Properties.Locations.Default.CalcL;
            }

            if (Properties.Settings.Default.Slide && sShow == null)
            {
                sShow = new SlideShow.MainForm();
                sShow.Show();
                sShow.DesktopLocation = Properties.Locations.Default.SlideShowL;
            }
            if (Properties.Settings.Default.Calender && Calen == null)
            {
                Calen = new Calender.Caln();
                Calen.Show();
                Calen.DesktopLocation = Properties.Locations.Default.CalenderL;
            }

            if (Properties.Settings.Default.Sudoku)
                sudoku.Show();
            if (Properties.Settings.Default.Sticky)
                Sticky.Show();
            if (Properties.Settings.Default.UConv)
                Uc.Show();
            if (Properties.Settings.Default.VideoP)
                VideoP.Show();
            if (Properties.Settings.Default.Calc)
                Calc.Show();
            if (Properties.Settings.Default.Slide)
                sShow.Show();
            if (Properties.Settings.Default.Calender)
                Calen.Show();
        }

        private void HideOffline()
        {

            if (Properties.Settings.Default.Sudoku && sudoku != null)
                sudoku.Hide();
            if (Properties.Settings.Default.Sticky && Sticky != null)
                Sticky.Hide();
            if (Properties.Settings.Default.UConv && Uc != null)
                Uc.Hide();
            if (Properties.Settings.Default.VideoP && VideoP != null)
                VideoP.Hide();
            if (Properties.Settings.Default.Calc && Calc != null)
                Calc.Hide();
            if (Properties.Settings.Default.Slide && sShow != null)
                sShow.Hide();
            if (Properties.Settings.Default.GoogleS && GSearch != null)
                GSearch.Hide();
            if (Properties.Settings.Default.Calender && Calen != null)
                Calen.Hide();

        }

        private void DIPSPanel_MouseUp(object sender, MouseEventArgs e)
        {
            this.SendToBack();
        }

        private void DIPSPanel_Leave(object sender, EventArgs e)
        {

            /*  if(this.PointToClient(Control.MousePosition).X < -2)
              {
                  this.Hide();
                  fbdips.Hide();
              }*/
        }

        void DIPSPanel_MouseEnter(object sender, EventArgs e)
        {
            //this.SendToBack();
            if (Properties.Settings.Default.Youtube && youTube != null)
                youTube.HideFull();
            if (Properties.Settings.Default.Calender && Calen != null)
                Calen.Width = 246;
        }
        public void Hider()
        {
            this.Hide();
            HideOnline();
            HideOffline();
        }

        void DIPSPanel_MouseLeave(object sender, System.EventArgs e)
        {
            if (this.PointToClient(Control.MousePosition).X < -2)
            {
                this.Hide();
                HideOnline();
                HideOffline();
                if (Properties.Settings.Default.Calender && Calen != null)
                    Calen.Width = 246;
            }
        }

        void DIPSPanel_LocationChanged(object sender, System.EventArgs e)
        {

            if (this.DesktopLocation.Y > 10)
            {
                this.Hide();
                HideOnline();
                HideOffline();
            }
            
        }

        private void HideOnline()
        {

            try
            {
                if (Properties.Settings.Default.FacebookPanel)
                    FBDIPS.Hide();
                if (Properties.Settings.Default.CConv)
                    cc.Hide();
                if (Properties.Settings.Default.Youtube)
                    youTube.Hide();
                if (Properties.Settings.Default.Stock)
                    stockpanel.Hide();
                if (Properties.Settings.Default.Weather)
                    WEATHER.Hide();
                if (Properties.Settings.Default.TwitterPanel)
                    TwitterP.Hide();
                if (Properties.Settings.Default.News)
                    NEWSP.Hide();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

        }
        void DIPSPanel_ResizeEnd(object sender, System.EventArgs e)
        {
            SetGlassRegion();
            Properties.Locations.Default.PanelS = this.Size;
            Properties.Locations.Default.Save();
            this.Invalidate();
        }

        void DIPSPanel_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
        {
            SetGlassRegion();
            this.Invalidate();
        }

        void DIPSPanel_Resize(object sender, System.EventArgs e)
        {
            SetGlassRegion();
            this.Invalidate();
        }
    }
}
