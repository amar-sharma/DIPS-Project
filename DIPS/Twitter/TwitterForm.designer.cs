namespace DIPS.Twitter
{
    partial class Form1
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
            this.txtTweet = new System.Windows.Forms.TextBox();
            this.btwTweet = new System.Windows.Forms.Button();
            this.lblCount = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtTweet
            // 
            this.txtTweet.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtTweet.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTweet.Location = new System.Drawing.Point(6, 8);
            this.txtTweet.Multiline = true;
            this.txtTweet.Name = "txtTweet";
            this.txtTweet.Size = new System.Drawing.Size(253, 74);
            this.txtTweet.TabIndex = 0;
            this.txtTweet.TextChanged += new System.EventHandler(this.txtTweet_TextChanged);
            this.txtTweet.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTweet_KeyDown);
            // 
            // btwTweet
            // 
            this.btwTweet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btwTweet.Location = new System.Drawing.Point(262, 55);
            this.btwTweet.Name = "btwTweet";
            this.btwTweet.Size = new System.Drawing.Size(85, 23);
            this.btwTweet.TabIndex = 1;
            this.btwTweet.Text = "Tweet It";
            this.btwTweet.UseVisualStyleBackColor = true;
            this.btwTweet.Click += new System.EventHandler(this.btwTweet_Click);
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCount.BackColor = System.Drawing.Color.White;
            this.lblCount.Location = new System.Drawing.Point(228, 66);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(28, 12);
            this.lblCount.TabIndex = 4;
            this.lblCount.Text = "140";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(262, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Configure";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 88);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.btwTweet);
            this.Controls.Add(this.txtTweet);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Twitter";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.Load += new System.EventHandler(this.Form1_Load);
            this.VisibleChanged += new System.EventHandler(this.Form1_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTweet;
        private System.Windows.Forms.Button btwTweet;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Button button1;
    }
}

