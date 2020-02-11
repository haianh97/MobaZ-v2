namespace DotA_Allstars.mainview
{
    partial class statusW
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(statusW));
            this.connectstt = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.pTop = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Pbot = new System.Windows.Forms.Panel();
            this.Pright = new System.Windows.Forms.Panel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.clBt = new Bunifu.UI.WinForms.BunifuPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.clBt)).BeginInit();
            this.SuspendLayout();
            // 
            // connectstt
            // 
            this.connectstt.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.connectstt.ForeColor = System.Drawing.Color.Coral;
            this.connectstt.Location = new System.Drawing.Point(-1, 71);
            this.connectstt.Name = "connectstt";
            this.connectstt.Size = new System.Drawing.Size(265, 35);
            this.connectstt.TabIndex = 0;
            this.connectstt.Text = "Kết nối vào mạng...";
            this.connectstt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pTop
            // 
            this.pTop.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pTop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pTop.BackgroundImage")));
            this.pTop.Location = new System.Drawing.Point(12, -2);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(231, 16);
            this.pTop.TabIndex = 16;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Location = new System.Drawing.Point(-1, -2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(10, 178);
            this.panel1.TabIndex = 17;
            // 
            // Pbot
            // 
            this.Pbot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Pbot.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Pbot.BackgroundImage")));
            this.Pbot.Location = new System.Drawing.Point(13, 160);
            this.Pbot.Name = "Pbot";
            this.Pbot.Size = new System.Drawing.Size(230, 16);
            this.Pbot.TabIndex = 18;
            // 
            // Pright
            // 
            this.Pright.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Pright.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Pright.BackgroundImage")));
            this.Pright.Location = new System.Drawing.Point(245, -2);
            this.Pright.Name = "Pright";
            this.Pright.Size = new System.Drawing.Size(10, 178);
            this.Pright.TabIndex = 19;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker1_DoWork);
            // 
            // clBt
            // 
            this.clBt.AllowFocused = false;
            this.clBt.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.clBt.BackColor = System.Drawing.Color.Transparent;
            this.clBt.BorderRadius = 100;
            this.clBt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.clBt.Image = global::DotA_Allstars.Properties.Resources.close_window_96px;
            this.clBt.IsCircle = false;
            this.clBt.Location = new System.Drawing.Point(224, 0);
            this.clBt.Name = "clBt";
            this.clBt.Size = new System.Drawing.Size(31, 31);
            this.clBt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.clBt.TabIndex = 20;
            this.clBt.TabStop = false;
            this.clBt.Type = Bunifu.UI.WinForms.BunifuPictureBox.Types.Custom;
            this.clBt.Visible = false;
            this.clBt.Click += new System.EventHandler(this.ClBt_Click);
            // 
            // statusW
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(255, 173);
            this.ControlBox = false;
            this.Controls.Add(this.clBt);
            this.Controls.Add(this.Pright);
            this.Controls.Add(this.Pbot);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pTop);
            this.Controls.Add(this.connectstt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "statusW";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.StatusW_Load);
            ((System.ComponentModel.ISupportInitialize)(this.clBt)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.Framework.UI.BunifuCustomLabel connectstt;
        private System.Windows.Forms.Panel pTop;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel Pbot;
        private System.Windows.Forms.Panel Pright;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Bunifu.UI.WinForms.BunifuPictureBox clBt;
    }
}