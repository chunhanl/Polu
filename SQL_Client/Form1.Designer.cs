namespace SQL_Client
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.text_account = new System.Windows.Forms.TextBox();
            this.text_password = new System.Windows.Forms.TextBox();
            this.login_button = new System.Windows.Forms.Button();
            this.label_account = new System.Windows.Forms.Label();
            this.label_password = new System.Windows.Forms.Label();
            this.img_login = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.img_login)).BeginInit();
            this.SuspendLayout();
            // 
            // text_account
            // 
            this.text_account.Location = new System.Drawing.Point(261, 84);
            this.text_account.Name = "text_account";
            this.text_account.Size = new System.Drawing.Size(199, 22);
            this.text_account.TabIndex = 0;
            this.text_account.TextChanged += new System.EventHandler(this.text_account_TextChanged);
            // 
            // text_password
            // 
            this.text_password.Location = new System.Drawing.Point(261, 135);
            this.text_password.Name = "text_password";
            this.text_password.PasswordChar = '*';
            this.text_password.Size = new System.Drawing.Size(199, 22);
            this.text_password.TabIndex = 1;
            this.text_password.TextChanged += new System.EventHandler(this.text_password_TextChanged);
            // 
            // login_button
            // 
            this.login_button.BackColor = System.Drawing.Color.Black;
            this.login_button.BackgroundImage = global::SQL_Client.Properties.Resources.login_button1;
            this.login_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.login_button.Location = new System.Drawing.Point(365, 183);
            this.login_button.Name = "login_button";
            this.login_button.Size = new System.Drawing.Size(75, 23);
            this.login_button.TabIndex = 2;
            this.login_button.UseVisualStyleBackColor = false;
            this.login_button.Click += new System.EventHandler(this.login_button_Click);
            // 
            // label_account
            // 
            this.label_account.AutoSize = true;
            this.label_account.Font = new System.Drawing.Font("Sanpya", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_account.Location = new System.Drawing.Point(189, 85);
            this.label_account.Name = "label_account";
            this.label_account.Size = new System.Drawing.Size(52, 24);
            this.label_account.TabIndex = 3;
            this.label_account.Text = "帳號";
            this.label_account.Click += new System.EventHandler(this.label_account_Click);
            // 
            // label_password
            // 
            this.label_password.AutoSize = true;
            this.label_password.Font = new System.Drawing.Font("Sanpya", 14.25F, System.Drawing.FontStyle.Bold);
            this.label_password.Location = new System.Drawing.Point(189, 136);
            this.label_password.Name = "label_password";
            this.label_password.Size = new System.Drawing.Size(52, 24);
            this.label_password.TabIndex = 4;
            this.label_password.Text = "密碼";
            // 
            // img_login
            // 
            this.img_login.BackgroundImage = global::SQL_Client.Properties.Resources.Login_img;
            this.img_login.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.img_login.InitialImage = global::SQL_Client.Properties.Resources.Login_img;
            this.img_login.Location = new System.Drawing.Point(12, 73);
            this.img_login.Name = "img_login";
            this.img_login.Size = new System.Drawing.Size(149, 105);
            this.img_login.TabIndex = 5;
            this.img_login.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SQL_Client.Properties.Resources.login_bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(519, 287);
            this.Controls.Add(this.img_login);
            this.Controls.Add(this.label_password);
            this.Controls.Add(this.label_account);
            this.Controls.Add(this.login_button);
            this.Controls.Add(this.text_password);
            this.Controls.Add(this.text_account);
            this.Name = "Form1";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.img_login)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox text_account;
        private System.Windows.Forms.TextBox text_password;
        private System.Windows.Forms.Button login_button;
        private System.Windows.Forms.Label label_account;
        private System.Windows.Forms.Label label_password;
        private System.Windows.Forms.PictureBox img_login;
    }
}

