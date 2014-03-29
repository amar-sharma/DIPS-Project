namespace DIPS.Twitter
{
    partial class FormSettings
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
            this.grpSettings = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.grpAuthorize = new System.Windows.Forms.GroupBox();
            this.btnAuthorize = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPIN = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpSettings.SuspendLayout();
            this.grpAuthorize.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSettings
            // 
            this.grpSettings.Controls.Add(this.btnSave);
            this.grpSettings.Controls.Add(this.label3);
            this.grpSettings.Location = new System.Drawing.Point(11, 12);
            this.grpSettings.Name = "grpSettings";
            this.grpSettings.Size = new System.Drawing.Size(442, 109);
            this.grpSettings.TabIndex = 4;
            this.grpSettings.TabStop = false;
            this.grpSettings.Text = " Application Settings ";
            this.grpSettings.Enter += new System.EventHandler(this.grpSettings_Enter);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(18, 74);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Get PIN!";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(15, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(405, 35);
            this.label3.TabIndex = 4;
            this.label3.Text = "Clicking the \'Get PIN!\' button will launch Twitter on your default browser. Enter" +
                " your Twitter Login details in it and authorize this app to get a 7-digit PIN.";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(11, 243);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(104, 23);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // grpAuthorize
            // 
            this.grpAuthorize.Controls.Add(this.btnAuthorize);
            this.grpAuthorize.Controls.Add(this.label5);
            this.grpAuthorize.Controls.Add(this.txtPIN);
            this.grpAuthorize.Controls.Add(this.label1);
            this.grpAuthorize.Enabled = false;
            this.grpAuthorize.Location = new System.Drawing.Point(11, 127);
            this.grpAuthorize.Name = "grpAuthorize";
            this.grpAuthorize.Size = new System.Drawing.Size(440, 110);
            this.grpAuthorize.TabIndex = 5;
            this.grpAuthorize.TabStop = false;
            this.grpAuthorize.Text = " Enter The 7-digit PIN ";
            // 
            // btnAuthorize
            // 
            this.btnAuthorize.Enabled = false;
            this.btnAuthorize.Location = new System.Drawing.Point(173, 69);
            this.btnAuthorize.Name = "btnAuthorize";
            this.btnAuthorize.Size = new System.Drawing.Size(104, 23);
            this.btnAuthorize.TabIndex = 5;
            this.btnAuthorize.Text = "Authorize!";
            this.btnAuthorize.UseVisualStyleBackColor = true;
            this.btnAuthorize.Click += new System.EventHandler(this.btnAuthorize_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "PIN:";
            // 
            // txtPIN
            // 
            this.txtPIN.Location = new System.Drawing.Point(54, 71);
            this.txtPIN.Name = "txtPIN";
            this.txtPIN.Size = new System.Drawing.Size(104, 20);
            this.txtPIN.TabIndex = 5;
            this.txtPIN.TextChanged += new System.EventHandler(this.txtPIN_TextChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(15, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(405, 35);
            this.label1.TabIndex = 5;
            this.label1.Text = "Like any other Twitter application, DIPS has to be authorized. Please specify the" +
                " PIN you were provided and click \'Authorize!\' ";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(296, 243);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(376, 243);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 270);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.grpAuthorize);
            this.Controls.Add(this.grpSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormSettings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.FormSettings_Load);
            this.grpSettings.ResumeLayout(false);
            this.grpAuthorize.ResumeLayout(false);
            this.grpAuthorize.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpSettings;  
        private System.Windows.Forms.Button btnSave;    //The Get Pin button
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label label3;
    
        private System.Windows.Forms.GroupBox grpAuthorize; 
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPIN;    
        private System.Windows.Forms.Button btnAuthorize;   
        private System.Windows.Forms.Button btnOK;  
        private System.Windows.Forms.Button btnCancel;  
    }
}