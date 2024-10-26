namespace GUI
{
    partial class ResetPassword
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
            this.components = new System.ComponentModel.Container();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.btnDoiMatKhau = new Guna.UI2.WinForms.Guna2GradientButton();
            this.guna2HtmlLabel5 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.swichHienMatKhau = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            this.txtConfimMatKhau = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtMatKhauMoi = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.controlClose = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2GradientPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 20;
            this.guna2Elipse1.TargetControl = this;
            // 
            // guna2GradientPanel1
            // 
            this.guna2GradientPanel1.Controls.Add(this.controlClose);
            this.guna2GradientPanel1.Controls.Add(this.btnDoiMatKhau);
            this.guna2GradientPanel1.Controls.Add(this.guna2HtmlLabel5);
            this.guna2GradientPanel1.Controls.Add(this.swichHienMatKhau);
            this.guna2GradientPanel1.Controls.Add(this.txtConfimMatKhau);
            this.guna2GradientPanel1.Controls.Add(this.txtMatKhauMoi);
            this.guna2GradientPanel1.Controls.Add(this.guna2HtmlLabel1);
            this.guna2GradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2GradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.guna2GradientPanel1.Name = "guna2GradientPanel1";
            this.guna2GradientPanel1.Size = new System.Drawing.Size(314, 260);
            this.guna2GradientPanel1.TabIndex = 0;
            // 
            // btnDoiMatKhau
            // 
            this.btnDoiMatKhau.BorderRadius = 20;
            this.btnDoiMatKhau.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDoiMatKhau.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDoiMatKhau.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDoiMatKhau.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDoiMatKhau.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDoiMatKhau.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnDoiMatKhau.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnDoiMatKhau.Font = new System.Drawing.Font("JetBrains Mono SemiBold", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnDoiMatKhau.ForeColor = System.Drawing.Color.White;
            this.btnDoiMatKhau.Location = new System.Drawing.Point(12, 203);
            this.btnDoiMatKhau.Name = "btnDoiMatKhau";
            this.btnDoiMatKhau.Size = new System.Drawing.Size(291, 45);
            this.btnDoiMatKhau.TabIndex = 13;
            this.btnDoiMatKhau.Text = "Đổi mật khẩu";
            this.btnDoiMatKhau.Click += new System.EventHandler(this.btnDoiMatKhau_Click);
            // 
            // guna2HtmlLabel5
            // 
            this.guna2HtmlLabel5.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel5.Font = new System.Drawing.Font("JetBrains Mono", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel5.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.guna2HtmlLabel5.Location = new System.Drawing.Point(58, 168);
            this.guna2HtmlLabel5.Name = "guna2HtmlLabel5";
            this.guna2HtmlLabel5.Size = new System.Drawing.Size(94, 18);
            this.guna2HtmlLabel5.TabIndex = 12;
            this.guna2HtmlLabel5.Text = "Hiện mật khẩu";
            // 
            // swichHienMatKhau
            // 
            this.swichHienMatKhau.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.swichHienMatKhau.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.swichHienMatKhau.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.swichHienMatKhau.CheckedState.InnerColor = System.Drawing.Color.White;
            this.swichHienMatKhau.Location = new System.Drawing.Point(17, 167);
            this.swichHienMatKhau.Name = "swichHienMatKhau";
            this.swichHienMatKhau.Size = new System.Drawing.Size(35, 20);
            this.swichHienMatKhau.TabIndex = 11;
            this.swichHienMatKhau.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.swichHienMatKhau.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.swichHienMatKhau.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.swichHienMatKhau.UncheckedState.InnerColor = System.Drawing.Color.White;
            this.swichHienMatKhau.CheckedChanged += new System.EventHandler(this.swichHienMatKhau_CheckedChanged);
            // 
            // txtConfimMatKhau
            // 
            this.txtConfimMatKhau.Animated = true;
            this.txtConfimMatKhau.BorderRadius = 8;
            this.txtConfimMatKhau.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtConfimMatKhau.DefaultText = "";
            this.txtConfimMatKhau.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtConfimMatKhau.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtConfimMatKhau.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtConfimMatKhau.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtConfimMatKhau.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtConfimMatKhau.Font = new System.Drawing.Font("JetBrains Mono", 12F);
            this.txtConfimMatKhau.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtConfimMatKhau.IconLeft = global::GUI.Properties.Resources.Pasword;
            this.txtConfimMatKhau.Location = new System.Drawing.Point(17, 119);
            this.txtConfimMatKhau.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtConfimMatKhau.Name = "txtConfimMatKhau";
            this.txtConfimMatKhau.PasswordChar = '*';
            this.txtConfimMatKhau.PlaceholderText = "Nhập lại mật khẩu";
            this.txtConfimMatKhau.SelectedText = "";
            this.txtConfimMatKhau.Size = new System.Drawing.Size(286, 42);
            this.txtConfimMatKhau.TabIndex = 10;
            // 
            // txtMatKhauMoi
            // 
            this.txtMatKhauMoi.Animated = true;
            this.txtMatKhauMoi.BorderRadius = 8;
            this.txtMatKhauMoi.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMatKhauMoi.DefaultText = "";
            this.txtMatKhauMoi.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMatKhauMoi.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtMatKhauMoi.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMatKhauMoi.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMatKhauMoi.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMatKhauMoi.Font = new System.Drawing.Font("JetBrains Mono", 12F);
            this.txtMatKhauMoi.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMatKhauMoi.IconLeft = global::GUI.Properties.Resources.Pasword;
            this.txtMatKhauMoi.Location = new System.Drawing.Point(17, 56);
            this.txtMatKhauMoi.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMatKhauMoi.Name = "txtMatKhauMoi";
            this.txtMatKhauMoi.PasswordChar = '*';
            this.txtMatKhauMoi.PlaceholderText = "Mật khẩu mới";
            this.txtMatKhauMoi.SelectedText = "";
            this.txtMatKhauMoi.Size = new System.Drawing.Size(286, 42);
            this.txtMatKhauMoi.TabIndex = 9;
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("JetBrains Mono SemiBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(86, 13);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(135, 27);
            this.guna2HtmlLabel1.TabIndex = 8;
            this.guna2HtmlLabel1.Text = "Đổi mật khẩu";
            // 
            // controlClose
            // 
            this.controlClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.controlClose.BackColor = System.Drawing.Color.Transparent;
            this.controlClose.FillColor = System.Drawing.Color.Transparent;
            this.controlClose.IconColor = System.Drawing.Color.DimGray;
            this.controlClose.Location = new System.Drawing.Point(273, 10);
            this.controlClose.Name = "controlClose";
            this.controlClose.Size = new System.Drawing.Size(30, 30);
            this.controlClose.TabIndex = 14;
            // 
            // ResetPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 260);
            this.Controls.Add(this.guna2GradientPanel1);
            this.Font = new System.Drawing.Font("JetBrains Mono", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "ResetPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ResetPassword";
            this.guna2GradientPanel1.ResumeLayout(false);
            this.guna2GradientPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        private Guna.UI2.WinForms.Guna2GradientButton btnDoiMatKhau;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel5;
        private Guna.UI2.WinForms.Guna2ToggleSwitch swichHienMatKhau;
        private Guna.UI2.WinForms.Guna2TextBox txtConfimMatKhau;
        private Guna.UI2.WinForms.Guna2TextBox txtMatKhauMoi;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2ControlBox controlClose;
    }
}