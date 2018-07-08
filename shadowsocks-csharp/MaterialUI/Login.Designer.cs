namespace MaterialUI
{
    partial class Login
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.materialSingleLineTextFieldAccount = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialSingleLineTextFieldPassword = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.mLabelAccount = new System.Windows.Forms.Label();
            this.mLabelPassword = new System.Windows.Forms.Label();
            this.materialFlatButtonLogin = new MaterialSkin.Controls.MaterialFlatButton();
            this.materialCheckBox1 = new MaterialSkin.Controls.MaterialCheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // materialSingleLineTextFieldAccount
            // 
            this.materialSingleLineTextFieldAccount.BackColor = System.Drawing.SystemColors.Control;
            this.materialSingleLineTextFieldAccount.Depth = 0;
            this.materialSingleLineTextFieldAccount.Font = new System.Drawing.Font("Microsoft YaHei UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.materialSingleLineTextFieldAccount.Hint = "";
            this.materialSingleLineTextFieldAccount.Location = new System.Drawing.Point(42, 236);
            this.materialSingleLineTextFieldAccount.MaxLength = 32767;
            this.materialSingleLineTextFieldAccount.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialSingleLineTextFieldAccount.Name = "materialSingleLineTextFieldAccount";
            this.materialSingleLineTextFieldAccount.PasswordChar = '\0';
            this.materialSingleLineTextFieldAccount.SelectedText = "";
            this.materialSingleLineTextFieldAccount.SelectionLength = 0;
            this.materialSingleLineTextFieldAccount.SelectionStart = 0;
            this.materialSingleLineTextFieldAccount.Size = new System.Drawing.Size(214, 23);
            this.materialSingleLineTextFieldAccount.TabIndex = 2;
            this.materialSingleLineTextFieldAccount.TabStop = false;
            this.materialSingleLineTextFieldAccount.UseSystemPasswordChar = false;
            // 
            // materialSingleLineTextFieldPassword
            // 
            this.materialSingleLineTextFieldPassword.BackColor = System.Drawing.SystemColors.Control;
            this.materialSingleLineTextFieldPassword.Depth = 0;
            this.materialSingleLineTextFieldPassword.Font = new System.Drawing.Font("Microsoft YaHei UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.materialSingleLineTextFieldPassword.Hint = "";
            this.materialSingleLineTextFieldPassword.Location = new System.Drawing.Point(42, 284);
            this.materialSingleLineTextFieldPassword.MaxLength = 32767;
            this.materialSingleLineTextFieldPassword.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialSingleLineTextFieldPassword.Name = "materialSingleLineTextFieldPassword";
            this.materialSingleLineTextFieldPassword.PasswordChar = '\0';
            this.materialSingleLineTextFieldPassword.SelectedText = "";
            this.materialSingleLineTextFieldPassword.SelectionLength = 0;
            this.materialSingleLineTextFieldPassword.SelectionStart = 0;
            this.materialSingleLineTextFieldPassword.Size = new System.Drawing.Size(214, 23);
            this.materialSingleLineTextFieldPassword.TabIndex = 3;
            this.materialSingleLineTextFieldPassword.TabStop = false;
            this.materialSingleLineTextFieldPassword.UseSystemPasswordChar = true;
            this.materialSingleLineTextFieldPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.materialSingleLineTextFieldPassword_KeyDown);
            // 
            // mLabelAccount
            // 
            this.mLabelAccount.AutoSize = true;
            this.mLabelAccount.BackColor = System.Drawing.Color.White;
            this.mLabelAccount.Font = new System.Drawing.Font("Mongolian Baiti", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mLabelAccount.Location = new System.Drawing.Point(40, 221);
            this.mLabelAccount.Name = "mLabelAccount";
            this.mLabelAccount.Size = new System.Drawing.Size(47, 13);
            this.mLabelAccount.TabIndex = 4;
            this.mLabelAccount.Text = "Account";
            // 
            // mLabelPassword
            // 
            this.mLabelPassword.AutoSize = true;
            this.mLabelPassword.BackColor = System.Drawing.Color.White;
            this.mLabelPassword.Font = new System.Drawing.Font("Mongolian Baiti", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mLabelPassword.Location = new System.Drawing.Point(40, 269);
            this.mLabelPassword.Name = "mLabelPassword";
            this.mLabelPassword.Size = new System.Drawing.Size(54, 13);
            this.mLabelPassword.TabIndex = 5;
            this.mLabelPassword.Text = "Password";
            // 
            // materialFlatButtonLogin
            // 
            this.materialFlatButtonLogin.AutoSize = true;
            this.materialFlatButtonLogin.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialFlatButtonLogin.Depth = 0;
            this.materialFlatButtonLogin.Icon = null;
            this.materialFlatButtonLogin.Location = new System.Drawing.Point(186, 318);
            this.materialFlatButtonLogin.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialFlatButtonLogin.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialFlatButtonLogin.Name = "materialFlatButtonLogin";
            this.materialFlatButtonLogin.Primary = false;
            this.materialFlatButtonLogin.Size = new System.Drawing.Size(61, 36);
            this.materialFlatButtonLogin.TabIndex = 7;
            this.materialFlatButtonLogin.Text = "Login";
            this.materialFlatButtonLogin.UseVisualStyleBackColor = true;
            this.materialFlatButtonLogin.Click += new System.EventHandler(this.materialFlatButtonLogin_Click);
            // 
            // materialCheckBox1
            // 
            this.materialCheckBox1.AutoSize = true;
            this.materialCheckBox1.Depth = 0;
            this.materialCheckBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.materialCheckBox1.Font = new System.Drawing.Font("Roboto", 10F);
            this.materialCheckBox1.Location = new System.Drawing.Point(42, 320);
            this.materialCheckBox1.Margin = new System.Windows.Forms.Padding(0);
            this.materialCheckBox1.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialCheckBox1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCheckBox1.Name = "materialCheckBox1";
            this.materialCheckBox1.Ripple = true;
            this.materialCheckBox1.Size = new System.Drawing.Size(97, 30);
            this.materialCheckBox1.TabIndex = 8;
            this.materialCheckBox1.Text = "Remember";
            this.materialCheckBox1.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Shadowsocks.Properties.Resources._235104_16050611355525;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(302, 64);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(304, 484);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseHover += new System.EventHandler(this.pictureBox1_MouseHover);
            // 
            // linkLabel1
            // 
            this.linkLabel1.ActiveLinkColor = System.Drawing.Color.SkyBlue;
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.BackColor = System.Drawing.Color.White;
            this.linkLabel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.linkLabel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLabel1.LinkColor = System.Drawing.Color.SkyBlue;
            this.linkLabel1.Location = new System.Drawing.Point(106, 495);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(74, 21);
            this.linkLabel1.TabIndex = 10;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "注册账号";
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.SkyBlue;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 548);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.materialCheckBox1);
            this.Controls.Add(this.materialFlatButtonLogin);
            this.Controls.Add(this.mLabelPassword);
            this.Controls.Add(this.mLabelAccount);
            this.Controls.Add(this.materialSingleLineTextFieldPassword);
            this.Controls.Add(this.materialSingleLineTextFieldAccount);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录至 ILYS 代理";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Login_Closed);
            this.Load += new System.EventHandler(this.Login_Load);
            this.MouseHover += new System.EventHandler(this.Login_MouseHover);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MaterialSkin.Controls.MaterialSingleLineTextField materialSingleLineTextFieldAccount;
        private MaterialSkin.Controls.MaterialSingleLineTextField materialSingleLineTextFieldPassword;
        private System.Windows.Forms.Label mLabelAccount;
        private System.Windows.Forms.Label mLabelPassword;
        private MaterialSkin.Controls.MaterialFlatButton materialFlatButtonLogin;
        private MaterialSkin.Controls.MaterialCheckBox materialCheckBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

