namespace MyPhotoControls
{
    partial class AlbumEditDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAlbumFile = new System.Windows.Forms.TextBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtnFileName = new System.Windows.Forms.RadioButton();
            this.rbtnCaption = new System.Windows.Forms.RadioButton();
            this.rbtnDateTaken = new System.Windows.Forms.RadioButton();
            this.cbxPassword = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblConfirm = new System.Windows.Forms.Label();
            this.txtConfirm = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtTitle);
            this.panel1.Controls.Add(this.txtAlbumFile);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Size = new System.Drawing.Size(260, 64);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Album &File";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "&Title";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAlbumFile
            // 
            this.txtAlbumFile.Location = new System.Drawing.Point(80, 8);
            this.txtAlbumFile.Name = "txtAlbumFile";
            this.txtAlbumFile.ReadOnly = true;
            this.txtAlbumFile.Size = new System.Drawing.Size(175, 20);
            this.txtAlbumFile.TabIndex = 2;
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(80, 34);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(175, 20);
            this.txtTitle.TabIndex = 3;
            this.txtTitle.TextChanged += new System.EventHandler(this.txtTitle_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtnDateTaken);
            this.groupBox1.Controls.Add(this.rbtnCaption);
            this.groupBox1.Controls.Add(this.rbtnFileName);
            this.groupBox1.Location = new System.Drawing.Point(13, 83);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(259, 50);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Phot&o Description";
            // 
            // rbtnFileName
            // 
            this.rbtnFileName.AutoSize = true;
            this.rbtnFileName.Location = new System.Drawing.Point(16, 20);
            this.rbtnFileName.Name = "rbtnFileName";
            this.rbtnFileName.Size = new System.Drawing.Size(70, 17);
            this.rbtnFileName.TabIndex = 0;
            this.rbtnFileName.TabStop = true;
            this.rbtnFileName.Text = "File &name";
            this.rbtnFileName.UseVisualStyleBackColor = true;
            // 
            // rbtnCaption
            // 
            this.rbtnCaption.AutoSize = true;
            this.rbtnCaption.Location = new System.Drawing.Point(92, 20);
            this.rbtnCaption.Name = "rbtnCaption";
            this.rbtnCaption.Size = new System.Drawing.Size(61, 17);
            this.rbtnCaption.TabIndex = 1;
            this.rbtnCaption.TabStop = true;
            this.rbtnCaption.Text = "Ca&ption";
            this.rbtnCaption.UseVisualStyleBackColor = true;
            // 
            // rbtnDateTaken
            // 
            this.rbtnDateTaken.AutoSize = true;
            this.rbtnDateTaken.Location = new System.Drawing.Point(160, 20);
            this.rbtnDateTaken.Name = "rbtnDateTaken";
            this.rbtnDateTaken.Size = new System.Drawing.Size(82, 17);
            this.rbtnDateTaken.TabIndex = 2;
            this.rbtnDateTaken.TabStop = true;
            this.rbtnDateTaken.Text = "&Date Taken";
            this.rbtnDateTaken.UseVisualStyleBackColor = true;
            // 
            // cbxPassword
            // 
            this.cbxPassword.AutoSize = true;
            this.cbxPassword.Location = new System.Drawing.Point(13, 144);
            this.cbxPassword.Name = "cbxPassword";
            this.cbxPassword.Size = new System.Drawing.Size(124, 17);
            this.cbxPassword.TabIndex = 5;
            this.cbxPassword.Text = "Pass&word Protected:";
            this.cbxPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbxPassword.UseVisualStyleBackColor = true;
            this.cbxPassword.CheckedChanged += new System.EventHandler(this.cbxPassword_CheckedChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.Enabled = false;
            this.txtPassword.Location = new System.Drawing.Point(134, 142);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(134, 20);
            this.txtPassword.TabIndex = 6;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // lblConfirm
            // 
            this.lblConfirm.AutoSize = true;
            this.lblConfirm.Enabled = false;
            this.lblConfirm.Location = new System.Drawing.Point(34, 170);
            this.lblConfirm.Name = "lblConfirm";
            this.lblConfirm.Size = new System.Drawing.Size(94, 13);
            this.lblConfirm.TabIndex = 7;
            this.lblConfirm.Text = "Confir&m Password:";
            this.lblConfirm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblConfirm.Click += new System.EventHandler(this.lblConfirm_Click);
            // 
            // txtConfirm
            // 
            this.txtConfirm.Enabled = false;
            this.txtConfirm.Location = new System.Drawing.Point(134, 167);
            this.txtConfirm.Name = "txtConfirm";
            this.txtConfirm.Size = new System.Drawing.Size(134, 20);
            this.txtConfirm.TabIndex = 8;
            this.txtConfirm.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(40, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Confir&m Password:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.Click += new System.EventHandler(this.lblConfirm_Click);
            // 
            // AlbumEditDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.txtConfirm);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblConfirm);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.cbxPassword);
            this.Controls.Add(this.groupBox1);
            this.Name = "AlbumEditDialog";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.cbxPassword, 0);
            this.Controls.SetChildIndex(this.txtPassword, 0);
            this.Controls.SetChildIndex(this.lblConfirm, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.txtConfirm, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtAlbumFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtnDateTaken;
        private System.Windows.Forms.RadioButton rbtnCaption;
        private System.Windows.Forms.RadioButton rbtnFileName;
        private System.Windows.Forms.CheckBox cbxPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblConfirm;
        private System.Windows.Forms.TextBox txtConfirm;
        private System.Windows.Forms.Label label3;
    }
}
