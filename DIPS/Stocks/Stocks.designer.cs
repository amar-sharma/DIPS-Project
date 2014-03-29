namespace DIPS.Stocks
{
    partial class Stocks
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Stocks));
            this.label1 = new System.Windows.Forms.Label();
            this.company = new System.Windows.Forms.TextBox();
            this.getQuote = new System.Windows.Forms.Button();
            this.output = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(1, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Company Code";
            // 
            // company
            // 
            this.company.Location = new System.Drawing.Point(107, 7);
            this.company.Multiline = true;
            this.company.Name = "company";
            this.company.Size = new System.Drawing.Size(167, 22);
            this.company.TabIndex = 1;
            this.company.KeyDown += new System.Windows.Forms.KeyEventHandler(this.company_KeyDown);
            // 
            // getQuote
            // 
            this.getQuote.Location = new System.Drawing.Point(280, 6);
            this.getQuote.Name = "getQuote";
            this.getQuote.Size = new System.Drawing.Size(69, 26);
            this.getQuote.TabIndex = 2;
            this.getQuote.Text = "Get Quote";
            this.getQuote.UseVisualStyleBackColor = true;
            this.getQuote.Click += new System.EventHandler(this.getQuote_Click);
            // 
            // output
            // 
            this.output.AutoSize = true;
            this.output.BackColor = System.Drawing.Color.Transparent;
            this.output.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.output.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.output.Location = new System.Drawing.Point(6, 37);
            this.output.Name = "output";
            this.output.Size = new System.Drawing.Size(0, 17);
            this.output.TabIndex = 3;
            // 
            // Stocks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(365, 121);
            this.Controls.Add(this.output);
            this.Controls.Add(this.getQuote);
            this.Controls.Add(this.company);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Stocks";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.VisibleChanged += new System.EventHandler(this.Stocks_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox company;
        private System.Windows.Forms.Button getQuote;
        private System.Windows.Forms.Label output;
    }
}

