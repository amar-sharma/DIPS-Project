using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DIPS.StickNotes
{
    public partial class FormNotes : Form
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
        public FormNotes()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

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

        private void Form1_Shown(object sender, EventArgs e)
        {
            String note = null;
            if (File.Exists(Application.CommonAppDataPath+"notes.txt"))
            {
                FileStream fs = new FileStream(Application.CommonAppDataPath+"notes.txt", FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                note = sr.ReadToEnd();
                sr.Close();
                fs.Close();
            }
            notes.Text = note; 
        }
        FileStream fp;
        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists(Application.CommonAppDataPath+"notes.txt"))
             fp = new FileStream(Application.CommonAppDataPath+"notes.txt", FileMode.Truncate, FileAccess.Write);
            else
             fp = new FileStream(Application.CommonAppDataPath+"notes.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fp);
            sw.WriteLine(notes.Text);
            sw.Flush();
            sw.Close();
            fp.Close();
        }

        private void FormNotes_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                Properties.Locations.Default.StickyNotes = this.DesktopLocation;
                Properties.Locations.Default.Save();
            }
        }
    }
}
