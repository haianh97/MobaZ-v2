namespace DotA_Allstars
{
    partial class login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(login));
            Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox.StateProperties stateProperties1 = new Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox.StateProperties stateProperties2 = new Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox.StateProperties stateProperties3 = new Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox.StateProperties stateProperties4 = new Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox.StateProperties stateProperties5 = new Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox.StateProperties stateProperties6 = new Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox.StateProperties stateProperties7 = new Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox.StateProperties stateProperties8 = new Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox.StateProperties();
            this.remember = new Bunifu.Framework.UI.BunifuCheckbox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.reglink = new System.Windows.Forms.LinkLabel();
            this.sttLg = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.Pbot = new System.Windows.Forms.Panel();
            this.Pright = new System.Windows.Forms.Panel();
            this.pTop = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.loginBt = new Bunifu.Framework.UI.BunifuThinButton2();
            this.paswd = new Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox();
            this.usname = new Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.mmmBt = new Bunifu.UI.WinForms.BunifuPictureBox();
            this.clBt = new Bunifu.UI.WinForms.BunifuPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmmBt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clBt)).BeginInit();
            this.SuspendLayout();
            // 
            // remember
            // 
            this.remember.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(135)))), ((int)(((byte)(140)))));
            this.remember.ChechedOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(135)))), ((int)(((byte)(140)))));
            this.remember.Checked = false;
            this.remember.CheckedOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(205)))), ((int)(((byte)(117)))));
            this.remember.ForeColor = System.Drawing.Color.White;
            this.remember.Location = new System.Drawing.Point(322, 253);
            this.remember.Name = "remember";
            this.remember.Size = new System.Drawing.Size(20, 20);
            this.remember.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Coral;
            this.label1.Location = new System.Drawing.Point(91, 254);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Lưu thông tin đăng nhập";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Coral;
            this.label2.Location = new System.Drawing.Point(91, 341);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Chưa có tài khoản?";
            // 
            // reglink
            // 
            this.reglink.AutoSize = true;
            this.reglink.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reglink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.reglink.Location = new System.Drawing.Point(212, 341);
            this.reglink.Name = "reglink";
            this.reglink.Size = new System.Drawing.Size(81, 16);
            this.reglink.TabIndex = 8;
            this.reglink.TabStop = true;
            this.reglink.Text = "Đăng kí ngay!";
            this.reglink.Click += new System.EventHandler(this.Reglink_Click);
            // 
            // sttLg
            // 
            this.sttLg.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.sttLg.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sttLg.ForeColor = System.Drawing.Color.Red;
            this.sttLg.Location = new System.Drawing.Point(12, 116);
            this.sttLg.Name = "sttLg";
            this.sttLg.Size = new System.Drawing.Size(413, 22);
            this.sttLg.TabIndex = 9;
            this.sttLg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Pbot
            // 
            this.Pbot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Pbot.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Pbot.BackgroundImage")));
            this.Pbot.Location = new System.Drawing.Point(14, 367);
            this.Pbot.Name = "Pbot";
            this.Pbot.Size = new System.Drawing.Size(411, 21);
            this.Pbot.TabIndex = 17;
            // 
            // Pright
            // 
            this.Pright.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Pright.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Pright.BackgroundImage")));
            this.Pright.Location = new System.Drawing.Point(423, -1);
            this.Pright.Name = "Pright";
            this.Pright.Size = new System.Drawing.Size(14, 389);
            this.Pright.TabIndex = 16;
            // 
            // pTop
            // 
            this.pTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pTop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pTop.BackgroundImage")));
            this.pTop.Location = new System.Drawing.Point(14, -2);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(411, 20);
            this.pTop.TabIndex = 15;
            this.pTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PTop_MouseDown);
            this.pTop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PTop_MouseMove);
            this.pTop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PTop_MouseUp);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Location = new System.Drawing.Point(0, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(14, 389);
            this.panel1.TabIndex = 10;
            // 
            // loginBt
            // 
            this.loginBt.ActiveBorderThickness = 1;
            this.loginBt.ActiveCornerRadius = 20;
            this.loginBt.ActiveFillColor = System.Drawing.Color.SeaGreen;
            this.loginBt.ActiveForecolor = System.Drawing.Color.White;
            this.loginBt.ActiveLineColor = System.Drawing.Color.SeaGreen;
            this.loginBt.BackColor = System.Drawing.Color.Black;
            this.loginBt.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("loginBt.BackgroundImage")));
            this.loginBt.ButtonText = "Đăng nhập";
            this.loginBt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loginBt.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginBt.ForeColor = System.Drawing.Color.SeaGreen;
            this.loginBt.IdleBorderThickness = 1;
            this.loginBt.IdleCornerRadius = 20;
            this.loginBt.IdleFillColor = System.Drawing.Color.White;
            this.loginBt.IdleForecolor = System.Drawing.Color.SeaGreen;
            this.loginBt.IdleLineColor = System.Drawing.Color.SeaGreen;
            this.loginBt.Location = new System.Drawing.Point(66, 280);
            this.loginBt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.loginBt.Name = "loginBt";
            this.loginBt.Size = new System.Drawing.Size(312, 57);
            this.loginBt.TabIndex = 6;
            this.loginBt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.loginBt.Click += new System.EventHandler(this.LoginBt_Click);
            // 
            // paswd
            // 
            this.paswd.AcceptsReturn = false;
            this.paswd.AcceptsTab = false;
            this.paswd.AnimationSpeed = 200;
            this.paswd.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.paswd.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.paswd.BackColor = System.Drawing.Color.Transparent;
            this.paswd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("paswd.BackgroundImage")));
            this.paswd.BorderColorActive = System.Drawing.Color.DodgerBlue;
            this.paswd.BorderColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(161)))), ((int)(((byte)(161)))));
            this.paswd.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.paswd.BorderColorIdle = System.Drawing.Color.Silver;
            this.paswd.BorderRadius = 1;
            this.paswd.BorderThickness = 1;
            this.paswd.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.paswd.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.paswd.DefaultFont = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.paswd.DefaultText = "";
            this.paswd.FillColor = System.Drawing.Color.White;
            this.paswd.HideSelection = true;
            this.paswd.IconLeft = ((System.Drawing.Image)(resources.GetObject("paswd.IconLeft")));
            this.paswd.IconLeftCursor = System.Windows.Forms.Cursors.IBeam;
            this.paswd.IconPadding = 10;
            this.paswd.IconRight = null;
            this.paswd.IconRightCursor = System.Windows.Forms.Cursors.IBeam;
            this.paswd.Lines = new string[0];
            this.paswd.Location = new System.Drawing.Point(66, 210);
            this.paswd.MaxLength = 32767;
            this.paswd.MinimumSize = new System.Drawing.Size(100, 35);
            this.paswd.Modified = false;
            this.paswd.Multiline = false;
            this.paswd.Name = "paswd";
            stateProperties1.BorderColor = System.Drawing.Color.DodgerBlue;
            stateProperties1.FillColor = System.Drawing.Color.Empty;
            stateProperties1.ForeColor = System.Drawing.Color.Empty;
            stateProperties1.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.paswd.OnActiveState = stateProperties1;
            stateProperties2.BorderColor = System.Drawing.Color.Empty;
            stateProperties2.FillColor = System.Drawing.Color.White;
            stateProperties2.ForeColor = System.Drawing.Color.Empty;
            stateProperties2.PlaceholderForeColor = System.Drawing.Color.Silver;
            this.paswd.OnDisabledState = stateProperties2;
            stateProperties3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties3.FillColor = System.Drawing.Color.Empty;
            stateProperties3.ForeColor = System.Drawing.Color.Empty;
            stateProperties3.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.paswd.OnHoverState = stateProperties3;
            stateProperties4.BorderColor = System.Drawing.Color.Silver;
            stateProperties4.FillColor = System.Drawing.Color.White;
            stateProperties4.ForeColor = System.Drawing.Color.Empty;
            stateProperties4.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.paswd.OnIdleState = stateProperties4;
            this.paswd.PasswordChar = '•';
            this.paswd.PlaceholderForeColor = System.Drawing.Color.Silver;
            this.paswd.PlaceholderText = "Mật khẩu";
            this.paswd.ReadOnly = false;
            this.paswd.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.paswd.SelectedText = "";
            this.paswd.SelectionLength = 0;
            this.paswd.SelectionStart = 0;
            this.paswd.ShortcutsEnabled = true;
            this.paswd.Size = new System.Drawing.Size(312, 41);
            this.paswd.Style = Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox._Style.Bunifu;
            this.paswd.TabIndex = 3;
            this.paswd.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.paswd.TextMarginBottom = 0;
            this.paswd.TextMarginLeft = 5;
            this.paswd.TextMarginTop = 0;
            this.paswd.TextPlaceholder = "Mật khẩu";
            this.paswd.UseSystemPasswordChar = false;
            this.paswd.WordWrap = true;
            this.paswd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Usname_KeyPress);
            // 
            // usname
            // 
            this.usname.AcceptsReturn = false;
            this.usname.AcceptsTab = false;
            this.usname.AnimationSpeed = 200;
            this.usname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.usname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.usname.BackColor = System.Drawing.Color.Transparent;
            this.usname.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("usname.BackgroundImage")));
            this.usname.BorderColorActive = System.Drawing.Color.DodgerBlue;
            this.usname.BorderColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(161)))), ((int)(((byte)(161)))));
            this.usname.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.usname.BorderColorIdle = System.Drawing.Color.Silver;
            this.usname.BorderRadius = 1;
            this.usname.BorderThickness = 1;
            this.usname.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.usname.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.usname.DefaultFont = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.usname.DefaultText = "";
            this.usname.FillColor = System.Drawing.Color.White;
            this.usname.HideSelection = true;
            this.usname.IconLeft = ((System.Drawing.Image)(resources.GetObject("usname.IconLeft")));
            this.usname.IconLeftCursor = System.Windows.Forms.Cursors.IBeam;
            this.usname.IconPadding = 10;
            this.usname.IconRight = null;
            this.usname.IconRightCursor = System.Windows.Forms.Cursors.IBeam;
            this.usname.Lines = new string[0];
            this.usname.Location = new System.Drawing.Point(66, 163);
            this.usname.MaxLength = 32767;
            this.usname.MinimumSize = new System.Drawing.Size(100, 35);
            this.usname.Modified = false;
            this.usname.Multiline = false;
            this.usname.Name = "usname";
            stateProperties5.BorderColor = System.Drawing.Color.DodgerBlue;
            stateProperties5.FillColor = System.Drawing.Color.Empty;
            stateProperties5.ForeColor = System.Drawing.Color.Empty;
            stateProperties5.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.usname.OnActiveState = stateProperties5;
            stateProperties6.BorderColor = System.Drawing.Color.Empty;
            stateProperties6.FillColor = System.Drawing.Color.White;
            stateProperties6.ForeColor = System.Drawing.Color.Empty;
            stateProperties6.PlaceholderForeColor = System.Drawing.Color.Silver;
            this.usname.OnDisabledState = stateProperties6;
            stateProperties7.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties7.FillColor = System.Drawing.Color.Empty;
            stateProperties7.ForeColor = System.Drawing.Color.Empty;
            stateProperties7.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.usname.OnHoverState = stateProperties7;
            stateProperties8.BorderColor = System.Drawing.Color.Silver;
            stateProperties8.FillColor = System.Drawing.Color.White;
            stateProperties8.ForeColor = System.Drawing.Color.Empty;
            stateProperties8.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.usname.OnIdleState = stateProperties8;
            this.usname.PasswordChar = '\0';
            this.usname.PlaceholderForeColor = System.Drawing.Color.Silver;
            this.usname.PlaceholderText = "Tên đăng nhập";
            this.usname.ReadOnly = false;
            this.usname.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.usname.SelectedText = "";
            this.usname.SelectionLength = 0;
            this.usname.SelectionStart = 0;
            this.usname.ShortcutsEnabled = true;
            this.usname.Size = new System.Drawing.Size(312, 41);
            this.usname.Style = Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox._Style.Bunifu;
            this.usname.TabIndex = 2;
            this.usname.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.usname.TextMarginBottom = 0;
            this.usname.TextMarginLeft = 5;
            this.usname.TextMarginTop = 0;
            this.usname.TextPlaceholder = "Tên đăng nhập";
            this.usname.UseSystemPasswordChar = false;
            this.usname.WordWrap = true;
            this.usname.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Usname_KeyPress);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DotA_Allstars.Properties.Resources._84838089_846009162491460_6888765451271143424_n;
            this.pictureBox1.Location = new System.Drawing.Point(18, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(401, 139);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // mmmBt
            // 
            this.mmmBt.AllowFocused = false;
            this.mmmBt.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.mmmBt.BackColor = System.Drawing.Color.Transparent;
            this.mmmBt.BorderRadius = 100;
            this.mmmBt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mmmBt.Image = global::DotA_Allstars.Properties.Resources.minimize_window_96px;
            this.mmmBt.IsCircle = false;
            this.mmmBt.Location = new System.Drawing.Point(372, 0);
            this.mmmBt.Name = "mmmBt";
            this.mmmBt.Size = new System.Drawing.Size(31, 31);
            this.mmmBt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mmmBt.TabIndex = 23;
            this.mmmBt.TabStop = false;
            this.mmmBt.Type = Bunifu.UI.WinForms.BunifuPictureBox.Types.Custom;
            this.mmmBt.Click += new System.EventHandler(this.MmmBt_Click);
            // 
            // clBt
            // 
            this.clBt.AllowFocused = false;
            this.clBt.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.clBt.BackColor = System.Drawing.Color.Transparent;
            this.clBt.BorderRadius = 100;
            this.clBt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.clBt.Image = global::DotA_Allstars.Properties.Resources.close_window_96px;
            this.clBt.IsCircle = false;
            this.clBt.Location = new System.Drawing.Point(404, 0);
            this.clBt.Name = "clBt";
            this.clBt.Size = new System.Drawing.Size(31, 31);
            this.clBt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.clBt.TabIndex = 22;
            this.clBt.TabStop = false;
            this.clBt.Type = Bunifu.UI.WinForms.BunifuPictureBox.Types.Custom;
            this.clBt.Click += new System.EventHandler(this.ClBt_Click);
            // 
            // login
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(437, 385);
            this.Controls.Add(this.sttLg);
            this.Controls.Add(this.mmmBt);
            this.Controls.Add(this.clBt);
            this.Controls.Add(this.Pbot);
            this.Controls.Add(this.Pright);
            this.Controls.Add(this.pTop);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.reglink);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.loginBt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.remember);
            this.Controls.Add(this.paswd);
            this.Controls.Add(this.usname);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MobaZ - Đăng nhập";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mmmBt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clBt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox usname;
        private Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox paswd;
        private Bunifu.Framework.UI.BunifuCheckbox remember;
        private System.Windows.Forms.Label label1;
        private Bunifu.Framework.UI.BunifuThinButton2 loginBt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel reglink;
        private Bunifu.Framework.UI.BunifuCustomLabel sttLg;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pTop;
        private System.Windows.Forms.Panel Pright;
        private System.Windows.Forms.Panel Pbot;
        private Bunifu.UI.WinForms.BunifuPictureBox mmmBt;
        private Bunifu.UI.WinForms.BunifuPictureBox clBt;
    }
}

