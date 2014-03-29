using System.Windows.Forms;
namespace DIPS
{
    partial class DIPSPanel
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
        private void InitializeComponent(int Screenheight)
        {
            this.SuspendLayout();
            // 
            // DIPSPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = Properties.Locations.Default.PanelS;
            this.ControlBox = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DIPSPanel";
            this.ShowInTaskbar = false;
            this.DesktopLocation = new System.Drawing.Point(Screen.PrimaryScreen.WorkingArea.Right - Properties.Locations.Default.PanelS.Width, 0);
            this.Text = "DIPS Panel";
            this.Load += new System.EventHandler(this.DIPSPanel_Load);
            this.Shown += new System.EventHandler(this.DIPSPanel_Shown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DIPSPanel_MouseUp);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DIPSPanel_MouseUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DIPSPanel_MouseUp);
            this.MouseLeave += new System.EventHandler(DIPSPanel_MouseLeave);
            this.MouseEnter += new System.EventHandler(DIPSPanel_MouseEnter);
            this.LostFocus += new System.EventHandler(DIPSPanel_MouseLeave);
            this.GotFocus += new System.EventHandler(DIPSPanel_GotFocus);
            this.VisibleChanged += new System.EventHandler(DIPSPanel_VisibleChanged);
            this.Resize += new System.EventHandler(DIPSPanel_Resize);
            this.Scroll += new System.Windows.Forms.ScrollEventHandler(DIPSPanel_Scroll);
            this.ResizeEnd += new System.EventHandler(DIPSPanel_ResizeEnd);
            this.ResizeBegin += new System.EventHandler(DIPSPanel_ResizeEnd);
            this.LocationChanged += new System.EventHandler(DIPSPanel_LocationChanged);
            this.MouseDoubleClick += new MouseEventHandler(DIPSPanel_MouseDoubleClick);
            this.ResumeLayout(false);

        }






        void DIPSPanel_GotFocus(object sender, System.EventArgs e)
        {
            this.SendToBack();
        }

        #endregion


    }
}