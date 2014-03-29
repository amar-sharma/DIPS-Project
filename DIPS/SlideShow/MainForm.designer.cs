namespace DIPS.SlideShow
{
    partial class MainForm
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
            this.pnlSlideShow = new System.Windows.Forms.Panel();
            this.imageFolderBrowserDlg = new System.Windows.Forms.FolderBrowserDialog();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.gbButtons = new System.Windows.Forms.GroupBox();
            this.btnSetting = new System.Windows.Forms.Button();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.pBPrev = new System.Windows.Forms.PictureBox();
            this.pBNext = new System.Windows.Forms.PictureBox();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.pnlSlideShow.SuspendLayout();
            this.gbButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBPrev)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBNext)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSlideShow
            // 
            this.pnlSlideShow.AutoScroll = true;
            this.pnlSlideShow.BackColor = System.Drawing.Color.Black;
            this.pnlSlideShow.Controls.Add(this.pBPrev);
            this.pnlSlideShow.Controls.Add(this.pBNext);
            this.pnlSlideShow.Controls.Add(this.pictureBox);
            this.pnlSlideShow.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSlideShow.Location = new System.Drawing.Point(0, 0);
            this.pnlSlideShow.Name = "pnlSlideShow";
            this.pnlSlideShow.Size = new System.Drawing.Size(448, 248);
            this.pnlSlideShow.TabIndex = 1;
            // 
            // timer
            // 
            this.timer.Interval = 3000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // gbButtons
            // 
            this.gbButtons.Controls.Add(this.btnSetting);
            this.gbButtons.Controls.Add(this.btnOpenFolder);
            this.gbButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbButtons.Location = new System.Drawing.Point(0, 248);
            this.gbButtons.Name = "gbButtons";
            this.gbButtons.Size = new System.Drawing.Size(448, 50);
            this.gbButtons.TabIndex = 2;
            this.gbButtons.TabStop = false;
            // 
            // btnSetting
            // 
            this.btnSetting.Location = new System.Drawing.Point(306, 5);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(61, 34);
            this.btnSetting.TabIndex = 15;
            this.btnSetting.Text = "Settings";
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Location = new System.Drawing.Point(88, 6);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(83, 34);
            this.btnOpenFolder.TabIndex = 12;
            this.btnOpenFolder.Text = "Open Folder...";
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
            // 
            // pBPrev
            // 
            this.pBPrev.BackColor = System.Drawing.Color.Transparent;
            this.pBPrev.Image = global::DIPS.Properties.Resources.Prev;
            this.pBPrev.Location = new System.Drawing.Point(12, 103);
            this.pBPrev.Name = "pBPrev";
            this.pBPrev.Size = new System.Drawing.Size(40, 40);
            this.pBPrev.TabIndex = 16;
            this.pBPrev.TabStop = false;
            this.pBPrev.Click += new System.EventHandler(this.pBPrev_Click);
            // 
            // pBNext
            // 
            this.pBNext.BackColor = System.Drawing.Color.Transparent;
            this.pBNext.Image = global::DIPS.Properties.Resources.Next1;
            this.pBNext.Location = new System.Drawing.Point(396, 103);
            this.pBNext.Name = "pBNext";
            this.pBNext.Size = new System.Drawing.Size(40, 40);
            this.pBNext.TabIndex = 16;
            this.pBNext.TabStop = false;
            this.pBNext.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(448, 248);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Click += new System.EventHandler(this.pictureBox_Click);
            this.pictureBox.DoubleClick += new System.EventHandler(this.pictureBox_DoubleClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 298);
            this.ControlBox = false;
            this.Controls.Add(this.gbButtons);
            this.Controls.Add(this.pnlSlideShow);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Picture Slide Show";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.VisibleChanged += new System.EventHandler(this.MainForm_VisibleChanged);
            this.pnlSlideShow.ResumeLayout(false);
            this.gbButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pBPrev)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBNext)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSlideShow;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.FolderBrowserDialog imageFolderBrowserDlg;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.GroupBox gbButtons;
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.PictureBox pBNext;
        private System.Windows.Forms.PictureBox pBPrev;
    }
}

