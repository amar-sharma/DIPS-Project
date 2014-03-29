using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace DIPS.Help
{
    public partial class Helpme : Form
    {
        public Helpme()
        {
            InitializeComponent();
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = "The main aim of this project is to create a sleek and elegant, yet powerful and cheeky desktop integrated panel that keeps one connected to the social cloud in the world as well as heed to user’s day to day requirements, more like a desktop widget. It will be a user friendly interface that contains many simple utilities.\r\n\r\nDeveloper-\r\n Amar Sharma\r\nHelpers-\r\n\u2022 Ankish Gupta\r\n\u2022 Anupam Jena\r\n\u2022 Karthik Gopalkrishnan";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "\u2022Right Click on the DIPS logo in System Tray and select Preferences Option. \r\n\u2022Click on the check boxes to select the widgets which you want to be displayed on the panel.\r\n\u2022Click on Enable Proxy check box if you use proxy server. Then enter the proxy address and port number.\r\n\u2022Click on Enable Authentication check box if your server requires authentication. Then enter the username and password for proxy server.\r\n\u2022Click on Reset button to set the widgets and choices to default state.";
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "\u2022The logo of DIPS will be displayed on the top-right corner of your Desktop. \r\n\u2022Also a logo of DIPS will be displayed in the System Tray. Right Click on the logo to set the Preferences or Exit the application.\r\n\u2022Hover the mouse over the Logo of  DIPS on the top-right corner of your Desktop. On doing so, a panel will be opened which contains all the widgets you have selected in Preferences window. \r\n\u2022To close the panel, move the mouse outside the panel or drag the panel downwards.\r\n\u2022To use the different widgets, instructions are written below.";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "\u2022Click the Open Folder button and select the path to a folder which includes image files. \r\n\u2022Click Previous button and Next button to display the images in order.\r\n\u2022Click the Settings button and input the desired time interval between two images in the slideshow.\r\n\u2022Click on the image to Play/Pause the Slide Show.\r\n\u2022Double Click on the image to open full screen mode. Press the ESC key to leave the full screen mode."; 
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text = "\u2022Type the name of the city in the search box and click on Get Weather button. Conditon, Temperature, Humidity and Wind condition will be displayed.";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "\u2022First configure your client by clicking the Configure button.\r\n---Configuration---\r\n\u2022Click on the Get PIN button. This will launch your default web browser and you will have to enter your username and password on the displayed Twitter webpage.\r\n\u2022You will then be provided with a 7-digit PIN. Enter this PIN into the Twitter client and click on Authorize.\r\n\u2022If authorization is successful, you will get a message indicating success.\r\n\u2022Henceforth you will not have to login to your Twitter account to send tweets as your PIN will be stored by the client. All you'll have to do is enter your tweet and click on the Tweet It button.\r\n\u2022If you wish to reset all input information, click on the Reset button.\r\n---Tweeting---\r\nE\u2022nter your tweet in the text box and click on the Tweet It button to send the tweet.";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = "---First Time Users---\r\n\u2022For first time users, default browser is opened. You need to login with your Username and Password.\r\n\u2022You will then be redirected to application authorization page. Click on Allow Access to start using the widget.\r\n---Using the widget---\r\n\u2022Type the status in text-box and click on Share on Facebook button to post it on your Timeline.\r\n\u2022Click on the Facebook logo to see the drop down menu with the list of your notifications.";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text = "\u2022Type the search query in textbox and click on Search button.\r\n\u2022You will be redirected to the web page of results of your query in your default browser ";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text = "\u2022Type the text you want to save in the textbox.\r\n\u2022Click on the Save Note button to save the text.";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text = "\u2022The headlines(Google World, Google National, Google Entertainment, Google Sports and Google Technology News) are displayed in the ticker running at the bottom of the panel.\r\n\u2022Click on the RSS icon on the left side of Ticker to see the links of all the headlines.";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox1.Text = "\u2022Click on the required buttons to compute the equation.\r\n\u2022Click on the radio button with the label Scientific to open the buttons for scientific calculator. Use them similarly as above.";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBox1.Text = "\u2022Enter the company code in textbox. (Remember to enter the company code, and not the company name)\r\n\u2022Click on Get Quote button to fetch the Current Index for the company and the last update date and time.";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            textBox1.Text = "\u2022Click on Sudoku icon. The game window is opened.\r\n\u2022Choose Difficulty level and and click on New Game button. Click on Save Game button to save the game and Load Game to load the game again.\r\n\u2022Enter the numbers in the grid by clicking on the up-down button in the grid.";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            textBox1.Text = "\u2022Enter the number along with the standard name of the unit in Input Box. (Format: <number> <unit>)\r\n\u2022Enter the standard name of the unit to which you require the number fed in previous box to be converted to.(Format: <unit>)";
        }

        private void button15_Click(object sender, EventArgs e)
        {
            textBox1.Text = "\u2022Copy the link of the file to be downloaded.\r\n\u2022Hover the mouse over the You Tube logo. \r\n\u2022Click on the Paste button to paste the URL and click on Get Video to fetch the information about the video.\r\n\u2022Select among the various video qualities and then click on the Download button.\r\n\u2022Select the location where you want to save the video. Click on Save button to save the video.";
        }

        private void button16_Click(object sender, EventArgs e)
        {
            textBox1.Text = "\u2022Enter the amount to be converted along with the source and destination currency unit.\r\n\u2022Click on Convert button to get the result.";
        }

        private void button17_Click(object sender, EventArgs e)
        {
            textBox1.Text = "\u2022CLick on the OPEN MUSIC PLAYER button in the DIPS panel.\r\n\u2022Click the OPEN FILE folder.\r\n\u2022Select the media file which you want to play(maybe an MP3, MP4,avi etc).";
        }

        private void button18_Click(object sender, EventArgs e)
        {
            textBox1.Text = "\u2022Select the date on the given calender to which you want to store a reminder.\r\n\u2022Write all the details of the reminder.\r\n\u2022Click the save button to store the reminder.\r\n\u2022To view the stored reminders, just select the date on the calender and the corresponding reminder will be shown.";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Helpme_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainDIPS.help = null;
        }
    }
}
