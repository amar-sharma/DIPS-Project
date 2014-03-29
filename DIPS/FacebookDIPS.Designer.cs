namespace DIPS
{
    partial class FacebookDIPS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FacebookDIPS));
            this.tbPost = new System.Windows.Forms.Button();
            this.Notify = new System.Windows.Forms.PictureBox();
            this.NotificationList = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tBStatus = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.Notify)).BeginInit();
            this.SuspendLayout();
            // 
            // tbPost
            // 
            this.tbPost.BackColor = System.Drawing.Color.Transparent;
            this.tbPost.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.tbPost, "tbPost");
            this.tbPost.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.tbPost.Name = "tbPost";
            this.tbPost.UseVisualStyleBackColor = false;
            this.tbPost.Click += new System.EventHandler(this.tbPost_Click);
            // 
            // Notify
            // 
            this.Notify.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.Notify, "Notify");
            this.Notify.Name = "Notify";
            this.Notify.TabStop = false;
            this.Notify.Click += new System.EventHandler(this.Notify_Click);
            // 
            // NotificationList
            // 
            this.NotificationList.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.NotificationList, "NotificationList");
            this.NotificationList.ForeColor = System.Drawing.SystemColors.ControlText;
            this.NotificationList.LargeImageList = this.imageList1;
            this.NotificationList.Name = "NotificationList";
            this.NotificationList.UseCompatibleStateImageBehavior = false;
            this.NotificationList.View = System.Windows.Forms.View.Details;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            resources.ApplyResources(this.imageList1, "imageList1");
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tBStatus
            // 
            this.tBStatus.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.tBStatus, "tBStatus");
            this.tBStatus.Name = "tBStatus";
            // 
            // FacebookDIPS
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.ControlBox = false;
            this.Controls.Add(this.NotificationList);
            this.Controls.Add(this.Notify);
            this.Controls.Add(this.tBStatus);
            this.Controls.Add(this.tbPost);
            this.ForeColor = System.Drawing.Color.MidnightBlue;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FacebookDIPS";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.Load += new System.EventHandler(this.FacebookDIPS_Load);
            this.Shown += new System.EventHandler(this.FacebookDIPS_Shown);
            this.VisibleChanged += new System.EventHandler(this.FacebookDIPS_VisibleChanged);
            this.MouseLeave += new System.EventHandler(this.FacebookDIPS_MouseLeave);
            ((System.ComponentModel.ISupportInitialize)(this.Notify)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button tbPost;
        private System.Windows.Forms.PictureBox Notify;
        private System.Windows.Forms.ListView NotificationList;
        private System.Windows.Forms.TextBox tBStatus;
        private System.Windows.Forms.ImageList imageList1;
    }
}