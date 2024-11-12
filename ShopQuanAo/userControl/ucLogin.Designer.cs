namespace userControl
{
    partial class ucLogin
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.c = new Guna.UI2.WinForms.Guna2TextBox();
            this.tctPassword = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnLogin = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // c
            // 
            this.c.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.c.DefaultText = "";
            this.c.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.c.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.c.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.c.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.c.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.c.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.c.HoverState.BorderColor = System.Drawing.Color.SandyBrown;
            this.c.HoverState.PlaceholderForeColor = System.Drawing.Color.White;
            this.c.IconLeft = global::userControl.Properties.Resources.user;
            this.c.IconLeftOffset = new System.Drawing.Point(5, 0);
            this.c.IconLeftSize = new System.Drawing.Size(30, 30);
            this.c.Location = new System.Drawing.Point(22, 29);
            this.c.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.c.Name = "c";
            this.c.PasswordChar = '\0';
            this.c.PlaceholderText = "Tên đăng nhập";
            this.c.SelectedText = "";
            this.c.Size = new System.Drawing.Size(266, 43);
            this.c.TabIndex = 0;
            // 
            // tctPassword
            // 
            this.tctPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tctPassword.DefaultText = "";
            this.tctPassword.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.tctPassword.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.tctPassword.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tctPassword.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tctPassword.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tctPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tctPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tctPassword.HoverState.BorderColor = System.Drawing.Color.SandyBrown;
            this.tctPassword.HoverState.PlaceholderForeColor = System.Drawing.Color.White;
            this.tctPassword.IconLeft = global::userControl.Properties.Resources._lock;
            this.tctPassword.IconLeftOffset = new System.Drawing.Point(5, 0);
            this.tctPassword.IconLeftSize = new System.Drawing.Size(30, 30);
            this.tctPassword.Location = new System.Drawing.Point(22, 95);
            this.tctPassword.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tctPassword.Name = "tctPassword";
            this.tctPassword.PasswordChar = '\0';
            this.tctPassword.PlaceholderText = "Mật khẩu";
            this.tctPassword.SelectedText = "";
            this.tctPassword.Size = new System.Drawing.Size(266, 43);
            this.tctPassword.TabIndex = 1;
            // 
            // btnLogin
            // 
            this.btnLogin.BorderRadius = 20;
            this.btnLogin.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLogin.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLogin.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLogin.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLogin.FillColor = System.Drawing.Color.SandyBrown;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(54, 167);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(207, 44);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "Login";
            // 
            // ucLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.tctPassword);
            this.Controls.Add(this.c);
            this.Name = "ucLogin";
            this.Size = new System.Drawing.Size(315, 235);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2TextBox c;
        private Guna.UI2.WinForms.Guna2TextBox tctPassword;
        private Guna.UI2.WinForms.Guna2Button btnLogin;
    }
}
