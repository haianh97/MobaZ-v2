namespace DotA_Allstars.mainview
{
    partial class notice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(notice));
            this.lbtxt = new System.Windows.Forms.Label();
            this.btnOK = new Bunifu.Framework.UI.BunifuThinButton2();
            this.dsag = new Bunifu.Framework.UI.BunifuCheckbox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbtxt
            // 
            this.lbtxt.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbtxt.Location = new System.Drawing.Point(36, 45);
            this.lbtxt.Name = "lbtxt";
            this.lbtxt.Size = new System.Drawing.Size(557, 106);
            this.lbtxt.TabIndex = 0;
            this.lbtxt.Text = resources.GetString("lbtxt.Text");
            this.lbtxt.UseCompatibleTextRendering = true;
            // 
            // btnOK
            // 
            this.btnOK.ActiveBorderThickness = 1;
            this.btnOK.ActiveCornerRadius = 20;
            this.btnOK.ActiveFillColor = System.Drawing.Color.SeaGreen;
            this.btnOK.ActiveForecolor = System.Drawing.Color.White;
            this.btnOK.ActiveLineColor = System.Drawing.Color.SeaGreen;
            this.btnOK.BackColor = System.Drawing.Color.White;
            this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
            this.btnOK.ButtonText = "Đồng ý";
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.ForeColor = System.Drawing.Color.SeaGreen;
            this.btnOK.IdleBorderThickness = 1;
            this.btnOK.IdleCornerRadius = 20;
            this.btnOK.IdleFillColor = System.Drawing.Color.White;
            this.btnOK.IdleForecolor = System.Drawing.Color.SeaGreen;
            this.btnOK.IdleLineColor = System.Drawing.Color.SeaGreen;
            this.btnOK.Location = new System.Drawing.Point(192, 182);
            this.btnOK.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(223, 39);
            this.btnOK.TabIndex = 1;
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // dsag
            // 
            this.dsag.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(135)))), ((int)(((byte)(140)))));
            this.dsag.ChechedOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(135)))), ((int)(((byte)(140)))));
            this.dsag.Checked = false;
            this.dsag.CheckedOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(205)))), ((int)(((byte)(117)))));
            this.dsag.ForeColor = System.Drawing.Color.White;
            this.dsag.Location = new System.Drawing.Point(168, 159);
            this.dsag.Name = "dsag";
            this.dsag.Size = new System.Drawing.Size(20, 20);
            this.dsag.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(192, 159);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Không hiển thị lại.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label2.Location = new System.Drawing.Point(168, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(280, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Thông báo quan trọng!";
            // 
            // notice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(625, 223);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dsag);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lbtxt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "notice";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "notice";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbtxt;
        private Bunifu.Framework.UI.BunifuThinButton2 btnOK;
        private Bunifu.Framework.UI.BunifuCheckbox dsag;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}