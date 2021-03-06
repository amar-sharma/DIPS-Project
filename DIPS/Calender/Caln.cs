﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DIPS.Calender
{
    public partial class Caln : Form
    {
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
        public static String filename = "Cal.txt";
        public int c = 0;
        public Caln()
        {
            InitializeComponent();

        }


        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            this.Width = 429;
            ReadEvents();
        }

        private void WriteEvents()
        {
            String date = monthCalendar1.SelectionRange.Start.ToShortDateString();
            using (StreamWriter file = File.AppendText(@filename))
            {
                file.WriteLine(date + ":" + tb2.Text);
                //MessageBox.Show("Saved to " + folderBrowserDialog1.SelectedPath + "\\_file.txt", "Saved Log File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // MessageBox.Show("Cal.txt");
            }

        }

        private void ReadEvents()
        {
            String date = monthCalendar1.SelectionRange.Start.ToShortDateString();
            string line;
            if (!File.Exists(filename))
                File.Create(filename);
            try
            {
                tb2.Text = null;
                StreamReader txt = new StreamReader(filename);
                bool flag = false;
                while ((line = txt.ReadLine()) != null)
                {

                    string[] data = line.Split(':');
                    if (date == data[0])
                    {
                        flag = true;
                        tb2.Text = data[1] + "\r\n";
                    }
                    if (data.Length == 1 && flag)
                    {
                        tb2.Text += data[0] + "\r\n";
                    }
                }
                txt.Close();
            }
            catch
            {
            }
        }


        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void Caln_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                Properties.Locations.Default.CalenderL = this.DesktopLocation;
                Properties.Locations.Default.Save();
            }
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            WriteEvents();
        }

        private void Caln_Load(object sender, EventArgs e)
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

    }
}
                                                                                                                                                                                              