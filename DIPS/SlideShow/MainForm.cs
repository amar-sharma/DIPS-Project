
using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Drawing;


namespace DIPS.SlideShow
{
    public partial class MainForm : Form
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


        private string[] imageFiles = null;

        // Image index
        private int selected = 0;
        private int begin = 0;
        private int end = 0;

        private FullScreen fullScreen = new FullScreen();

        public MainForm()
        {
            InitializeComponent();
            this.pBPrev.Enabled = false;
            this.pBNext.Enabled = false;
            Slideit();
        }

        private void Slideit()
        {
            try
            {
                if (Properties.Settings.Default.ImagesFolder.Length != 0)
                {
                    this.imageFiles = GetFiles(Properties.Settings.Default.ImagesFolder,
                        "*.jpg;*.jpeg;*.png;*.bmp;*.tif;*.tiff;*.gif");

                    this.selected = 0;
                    this.begin = 0;
                    this.end = imageFiles.Length;

                    if (this.imageFiles.Length == 0)
                    {
                        MessageBox.Show("No image can be found");

                        this.pBPrev.Enabled = false;
                        this.pBNext.Enabled = false;
                    }
                    else
                    {
                        ShowImage(imageFiles[selected], pictureBox);
                        this.pBPrev.Enabled = true;
                        this.pBNext.Enabled = true;
                        if (this.imageFiles == null || this.imageFiles.Length == 0)
                        {
                            MessageBox.Show("Please select the images to slideshow!");
                            return;
                        }

                        if (timer.Enabled == true)
                        {
                            this.timer.Enabled = false;
                            this.btnOpenFolder.Enabled = true;
                        }
                        else
                        {
                            this.timer.Enabled = true;
                            this.btnOpenFolder.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Some Error occurred!!");
            }
        }

        /// <summary>
        /// Select the image folder.
        /// </summary>
        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.imageFolderBrowserDlg.ShowDialog() == DialogResult.OK)
                {
                    this.imageFiles = GetFiles(this.imageFolderBrowserDlg.SelectedPath,
                        "*.jpg;*.jpeg;*.png;*.bmp;*.tif;*.tiff;*.gif");
                    Properties.Settings.Default.ImagesFolder = this.imageFolderBrowserDlg.SelectedPath;
                    Properties.Settings.Default.Save();
                    this.selected = 0;
                    this.begin = 0;
                    this.end = imageFiles.Length;

                    if (this.imageFiles.Length == 0)
                    {
                        MessageBox.Show("No image can be found");

                        this.pBPrev.Enabled = false;
                        this.pBNext.Enabled = false;
                    }
                    else
                    {
                        ShowImage(imageFiles[selected], pictureBox);
                        this.pBPrev.Enabled = true;
                        this.pBNext.Enabled = true;
                        if (this.imageFiles == null || this.imageFiles.Length == 0)
                        {
                            MessageBox.Show("Please select the images to slideshow!");
                            return;
                        }

                        if (timer.Enabled == true)
                        {
                            this.timer.Enabled = false;
                            this.btnOpenFolder.Enabled = true;
                        }
                        else
                        {
                            this.timer.Enabled = true;
                            this.btnOpenFolder.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Some Error occurred!!");
            }
        }

        public static string[] GetFiles(string path, string searchPattern)
        {
            string[] patterns = searchPattern.Split(';');
            List<string> files = new List<string>();
            foreach (string filter in patterns)
            {

                Stack<string> dirs = new Stack<string>(20);

                if (!Directory.Exists(path))
                {
                    throw new ArgumentException();
                }
                dirs.Push(path);

                while (dirs.Count > 0)
                {
                    string currentDir = dirs.Pop();
                    string[] subDirs;
                    try
                    {
                        subDirs = Directory.GetDirectories(currentDir);
                    }

                    catch (UnauthorizedAccessException)
                    {
                        continue;
                    }
                    catch (DirectoryNotFoundException)
                    {
                        continue;
                    }

                    try
                    {
                        files.AddRange(Directory.GetFiles(currentDir, filter));
                    }
                    catch (UnauthorizedAccessException)
                    {
                        continue;
                    }
                    catch (DirectoryNotFoundException)
                    {
                        continue;
                    }

                    // Push the subdirectories onto the stack for traversal.
                    // This could also be done before handing the files.
                    foreach (string str in subDirs)
                    {
                        dirs.Push(str);
                    }
                }
            }

            return files.ToArray();
        }

        /// <summary>
        /// Click the "Previous" button to navigate to the previous image.
        /// </summary>
        private void btnPrevious_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Click the "Next" button to navigate to the next image.
        /// </summary>
        private void btnNext_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Image slideshow.
        /// </summary>
        private void btnImageSlideShow_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Show the next image at every regular intervals.
        /// </summary>
        private void timer_Tick(object sender, EventArgs e)
        {
            ShowNextImage();
        }

        /// <summary>
        /// Show child windows to alternate the settings about Timer control.
        /// </summary>
        private void btnSetting_Click(object sender, EventArgs e)
        {
            Settings frmSettings = new Settings(ref timer);
            frmSettings.ShowDialog();
        }

        /// <summary>
        /// Enter or leave the full screen mode.
        /// </summary>
        private void btnFullScreen_Click(object sender, EventArgs e)
        {
            if (!this.fullScreen.IsFullScreen)
            {
                // Hide the buttons and max the slideshow panel.
                this.gbButtons.Visible = false;
                this.pnlSlideShow.Dock = DockStyle.Fill;

                fullScreen.EnterFullScreen(this);
            }
        }

        /// <summary>
        /// Respond to the keystroke "ESC".
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                if (this.fullScreen.IsFullScreen)
                {
                    // Unhide the buttons and restore the slideshow panel.
                    this.gbButtons.Visible = true;
                    this.pnlSlideShow.Dock = DockStyle.Top;

                    fullScreen.LeaveFullScreen(this);
                    pBNext.Show();
                    pBPrev.Show();
                }
                return true;
            }
            else
                return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Show the image in the PictureBox.
        /// </summary>
        public static void ShowImage(string path, PictureBox pct)
        {
            pct.ImageLocation = path;
        }

        /// <summary>
        /// Show the previous image.
        /// </summary>
        private void ShowPrevImage()
        {
            ShowImage(this.imageFiles[(--this.selected) % this.imageFiles.Length], this.pictureBox);
        }

        /// <summary>
        /// Show the next image.
        /// </summary>
        private void ShowNextImage()
        {
            ShowImage(this.imageFiles[(++this.selected) % this.imageFiles.Length], this.pictureBox);
        }

        private void MainForm_VisibleChanged(object sender, EventArgs e)
        {

            if (!this.Visible)
            {
                Properties.Locations.Default.SlideShowL = this.DesktopLocation;
                Properties.Locations.Default.Save();
            }

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

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox_DoubleClick(object sender, EventArgs e)
        {
            int x = Screen.PrimaryScreen.WorkingArea.Width;
            int y = Screen.PrimaryScreen.WorkingArea.Height;
            if (!this.fullScreen.IsFullScreen)
            {
                // Hide the buttons and max the slideshow panel.
                this.gbButtons.Visible = false;
                this.pnlSlideShow.Dock = DockStyle.Fill;
                this.pBNext.Hide();
                this.pBPrev.Hide();
                fullScreen.EnterFullScreen(this);
            }
            else
            {
                if (this.fullScreen.IsFullScreen)
                {
                    // Unhide the buttons and restore the slideshow panel.
                    this.gbButtons.Visible = true;
                    this.pnlSlideShow.Dock = DockStyle.Top;
                    fullScreen.LeaveFullScreen(this);
                    pBNext.Show();
                    pBPrev.Show();

                }
            }
        }
        bool clicked = false;

        private void pictureBox_Click(object sender, EventArgs e)
        {
            if (!clicked)
            {
                timer.Enabled = false;
                clicked = true;
            }
            else
            {
                timer.Enabled = true;
                clicked = false;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (this.imageFiles == null || this.imageFiles.Length == 0)
            {
                MessageBox.Show("Please select the images to slideshow!");
                return;
            }
            ShowNextImage();
        }

        private void pBPrev_Click(object sender, EventArgs e)
        {
            if (this.imageFiles == null || this.imageFiles.Length == 0)
            {
                MessageBox.Show("Please select the images to slideshow!");
                return;
            }
            ShowPrevImage();
        }
    }
}
