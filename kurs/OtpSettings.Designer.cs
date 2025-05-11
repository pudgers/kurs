namespace kurs
{
    partial class OtpSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OtpSettings));
            this.nameLbl = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.surnameLbl = new System.Windows.Forms.Label();
            this.loginLbl = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.qrCodePb = new System.Windows.Forms.PictureBox();
            this.fa2Cb = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qrCodePb)).BeginInit();
            this.SuspendLayout();
            // 
            // nameLbl
            // 
            this.nameLbl.AutoSize = true;
            this.nameLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nameLbl.Location = new System.Drawing.Point(151, 12);
            this.nameLbl.Name = "nameLbl";
            this.nameLbl.Size = new System.Drawing.Size(40, 20);
            this.nameLbl.TabIndex = 0;
            this.nameLbl.Text = "N/A";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(133, 128);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // surnameLbl
            // 
            this.surnameLbl.AutoSize = true;
            this.surnameLbl.Location = new System.Drawing.Point(151, 41);
            this.surnameLbl.Name = "surnameLbl";
            this.surnameLbl.Size = new System.Drawing.Size(30, 16);
            this.surnameLbl.TabIndex = 2;
            this.surnameLbl.Text = "N/A";
            // 
            // loginLbl
            // 
            this.loginLbl.AutoSize = true;
            this.loginLbl.Location = new System.Drawing.Point(60, 143);
            this.loginLbl.Name = "loginLbl";
            this.loginLbl.Size = new System.Drawing.Size(30, 16);
            this.loginLbl.TabIndex = 3;
            this.loginLbl.Text = "N/A";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(362, 306);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Готово";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // qrCodePb
            // 
            this.qrCodePb.Location = new System.Drawing.Point(236, 12);
            this.qrCodePb.Name = "qrCodePb";
            this.qrCodePb.Size = new System.Drawing.Size(356, 288);
            this.qrCodePb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.qrCodePb.TabIndex = 10;
            this.qrCodePb.TabStop = false;
            // 
            // fa2Cb
            // 
            this.fa2Cb.AutoSize = true;
            this.fa2Cb.Location = new System.Drawing.Point(24, 175);
            this.fa2Cb.Name = "fa2Cb";
            this.fa2Cb.Size = new System.Drawing.Size(120, 20);
            this.fa2Cb.TabIndex = 11;
            this.fa2Cb.Text = "Включить 2FA";
            this.fa2Cb.UseVisualStyleBackColor = true;
            // 
            // OtpSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 345);
            this.Controls.Add(this.fa2Cb);
            this.Controls.Add(this.qrCodePb);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.loginLbl);
            this.Controls.Add(this.surnameLbl);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.nameLbl);
            this.Name = "OtpSettings";
            this.Text = "OtpSettings";
            this.Load += new System.EventHandler(this.ChangePasswordForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qrCodePb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nameLbl;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label surnameLbl;
        private System.Windows.Forms.Label loginLbl;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox qrCodePb;
        private System.Windows.Forms.CheckBox fa2Cb;
    }
}