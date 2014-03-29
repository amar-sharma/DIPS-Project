using System.Drawing;
using System.IO;
using System.Windows.Forms;
namespace DIPS
{
    partial class MainDIPS
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainDIPS));
            this.Tray = new System.Windows.Forms.NotifyIcon(this.components);
            this.MenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.startWithWindowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendFeedBackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prefrencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tray
            // 
            this.Tray.ContextMenuStrip = this.MenuStrip;
            this.Tray.Icon = ((System.Drawing.Icon)(resources.GetObject("Tray.Icon")));
            this.Tray.Text = "DIPS";
            this.Tray.Visible = true;
            this.Tray.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Tray_MouseDoubleClick);
            // 
            // MenuStrip
            // 
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startWithWindowsToolStripMenuItem,
            this.sendFeedBackToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.prefrencesToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(177, 136);
            // 
            // startWithWindowsToolStripMenuItem
            // 
            this.startWithWindowsToolStripMenuItem.Name = "startWithWindowsToolStripMenuItem";
            this.startWithWindowsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.startWithWindowsToolStripMenuItem.Text = "Start with Windows";
            this.startWithWindowsToolStripMenuItem.Click += new System.EventHandler(this.startWithWindowsToolStripMenuItem_Click);
            // 
            // sendFeedBackToolStripMenuItem
            // 
            this.sendFeedBackToolStripMenuItem.Image = global::DIPS.Properties.Resources.feedback;
            this.sendFeedBackToolStripMenuItem.Name = "sendFeedBackToolStripMenuItem";
            this.sendFeedBackToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.sendFeedBackToolStripMenuItem.Text = "Send Feed-Back";
            this.sendFeedBackToolStripMenuItem.Click += new System.EventHandler(this.sendFeedBackToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Image = global::DIPS.Properties.Resources.help;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // prefrencesToolStripMenuItem
            // 
            this.prefrencesToolStripMenuItem.Image = global::DIPS.Properties.Resources.Prefrences;
            this.prefrencesToolStripMenuItem.Name = "prefrencesToolStripMenuItem";
            this.prefrencesToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.prefrencesToolStripMenuItem.Text = "Prefrences";
            this.prefrencesToolStripMenuItem.Click += new System.EventHandler(this.prefrencesToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::DIPS.Properties.Resources.Closeit;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // MainDIPS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(155, 155);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainDIPS";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "DIPS";
            this.TransparencyKey = System.Drawing.Color.White;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainDIPS_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainDIPS_FormClosed);
            this.Load += new System.EventHandler(this.MainDisc_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainDIPS_Paint);
            this.MouseLeave += new System.EventHandler(this.MainDIPS_MouseLeave);
            this.MouseHover += new System.EventHandler(this.MainDIPS_MouseHover);
            this.MenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }



        #endregion
        private Bitmap mainbmp;
        private NotifyIcon Tray;
        private ContextMenuStrip MenuStrip;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem prefrencesToolStripMenuItem;
        private ToolStripMenuItem startWithWindowsToolStripMenuItem;
        private ToolStripMenuItem sendFeedBackToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;

    }
}

