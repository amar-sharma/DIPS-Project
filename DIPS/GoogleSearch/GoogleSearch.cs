using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace DIPS.GoogleSearch
{
    public partial class GoogleSearch : Form
    {
        public GoogleSearch()
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

        private void Form1_Load(object sender, EventArgs e)
        {

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
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            Searchit();
            label1.Show();
        }

        private void Searchit()
        {
            if (textSearch.TextLength == 0)
            {
                label1.Text = "Enter text to search";
                return;
            }
            else
            {

                //string search = "http://www.google.com/search?q=" + textSearch.Text;
                Process myProcess = new Process();

                try
                {
                    // true is the default, but it is important not to set it to false
                    myProcess.StartInfo.UseShellExecute = true;
                    myProcess.StartInfo.FileName = ("http://www.google.com/search?q=" + textSearch.Text);
                    myProcess.Start();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                // webBrowser1.Navigate(search);
                textSearch.Text = null;
            }
        }

        private void Form1_VisibleChanged(object sender, EventArgs e)
        {

            if (!this.Visible)
            {
                Properties.Locations.Default.GoogleSearchL = this.DesktopLocation;
                Properties.Locations.Default.Save();
            }

        }

        private void textSearch_TextChanged(object sender, EventArgs e)
        {
            if (textSearch.Text != null)
            {
                label1.Hide();
            }
            else if (!label1.Visible)
            {
                label1.Show();
            }
        }

        private void textSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Searchit();
                label1.Show();
            }
        }
    }
}
