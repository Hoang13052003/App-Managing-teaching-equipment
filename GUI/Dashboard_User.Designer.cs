namespace GUI
{
    partial class Dashboard_User
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
            this.lb_home = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2ControlBox2 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.controlClose = new Guna.UI2.WinForms.Guna2ControlBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Panel_Change_Form = new Guna.UI2.WinForms.Guna2Panel();
            this.Navbar = new System.Windows.Forms.Panel();
            this.main = new System.Windows.Forms.Panel();
            this.Sidebar = new System.Windows.Forms.Panel();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.btnYeuCauSuaThietBi = new Guna.UI2.WinForms.Guna2Button();
            this.btn_Lich_Day = new Guna.UI2.WinForms.Guna2Button();
            this.btnAccount = new Guna.UI2.WinForms.Guna2Button();
            this.btnSetting = new Guna.UI2.WinForms.Guna2Button();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lbMenu = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnHome = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Elipse2 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.lb_NameForm = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.Navbar.SuspendLayout();
            this.main.SuspendLayout();
            this.Sidebar.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 10;
            this.guna2Elipse1.TargetControl = this;
            // 
            // lb_home
            // 
            this.lb_home.BackColor = System.Drawing.Color.Transparent;
            this.lb_home.Font = new System.Drawing.Font("JetBrains Mono", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_home.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(148)))), ((int)(((byte)(249)))));
            this.lb_home.Location = new System.Drawing.Point(108, 13);
            this.lb_home.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lb_home.Name = "lb_home";
            this.lb_home.Size = new System.Drawing.Size(67, 38);
            this.lb_home.TabIndex = 0;
            this.lb_home.Text = "USER";
            // 
            // guna2ControlBox2
            // 
            this.guna2ControlBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox2.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.guna2ControlBox2.FillColor = System.Drawing.Color.White;
            this.guna2ControlBox2.IconColor = System.Drawing.Color.DimGray;
            this.guna2ControlBox2.Location = new System.Drawing.Point(1353, 17);
            this.guna2ControlBox2.Name = "guna2ControlBox2";
            this.guna2ControlBox2.Size = new System.Drawing.Size(30, 30);
            this.guna2ControlBox2.TabIndex = 9;
            // 
            // controlClose
            // 
            this.controlClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.controlClose.FillColor = System.Drawing.Color.White;
            this.controlClose.IconColor = System.Drawing.Color.DimGray;
            this.controlClose.Location = new System.Drawing.Point(1389, 17);
            this.controlClose.Name = "controlClose";
            this.controlClose.Size = new System.Drawing.Size(30, 30);
            this.controlClose.TabIndex = 7;
            this.controlClose.Click += new System.EventHandler(this.controlClose_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.lb_home);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 65);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // Panel_Change_Form
            // 
            this.Panel_Change_Form.BackColor = System.Drawing.Color.White;
            this.Panel_Change_Form.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Change_Form.Location = new System.Drawing.Point(240, 65);
            this.Panel_Change_Form.Name = "Panel_Change_Form";
            this.Panel_Change_Form.Padding = new System.Windows.Forms.Padding(3);
            this.Panel_Change_Form.Size = new System.Drawing.Size(1200, 895);
            this.Panel_Change_Form.TabIndex = 3;
            // 
            // Navbar
            // 
            this.Navbar.BackColor = System.Drawing.Color.White;
            this.Navbar.Controls.Add(this.lb_NameForm);
            this.Navbar.Controls.Add(this.guna2ControlBox2);
            this.Navbar.Controls.Add(this.controlClose);
            this.Navbar.Controls.Add(this.panel1);
            this.Navbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.Navbar.Location = new System.Drawing.Point(0, 0);
            this.Navbar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Navbar.Name = "Navbar";
            this.Navbar.Size = new System.Drawing.Size(1440, 65);
            this.Navbar.TabIndex = 1;
            this.Navbar.Paint += new System.Windows.Forms.PaintEventHandler(this.Navbar_Paint);
            // 
            // main
            // 
            this.main.Controls.Add(this.Panel_Change_Form);
            this.main.Controls.Add(this.Sidebar);
            this.main.Controls.Add(this.Navbar);
            this.main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.main.Location = new System.Drawing.Point(0, 0);
            this.main.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.main.Name = "main";
            this.main.Size = new System.Drawing.Size(1440, 960);
            this.main.TabIndex = 1;
            // 
            // Sidebar
            // 
            this.Sidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.Sidebar.Controls.Add(this.guna2Button1);
            this.Sidebar.Controls.Add(this.btnYeuCauSuaThietBi);
            this.Sidebar.Controls.Add(this.btn_Lich_Day);
            this.Sidebar.Controls.Add(this.btnAccount);
            this.Sidebar.Controls.Add(this.btnSetting);
            this.Sidebar.Controls.Add(this.guna2HtmlLabel1);
            this.Sidebar.Controls.Add(this.lbMenu);
            this.Sidebar.Controls.Add(this.btnHome);
            this.Sidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.Sidebar.Location = new System.Drawing.Point(0, 65);
            this.Sidebar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Sidebar.Name = "Sidebar";
            this.Sidebar.Size = new System.Drawing.Size(240, 895);
            this.Sidebar.TabIndex = 2;
            // 
            // guna2Button1
            // 
            this.guna2Button1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.guna2Button1.BorderRadius = 8;
            this.guna2Button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.Color.Transparent;
            this.guna2Button1.Font = new System.Drawing.Font("JetBrains Mono", 9.75F);
            this.guna2Button1.ForeColor = System.Drawing.Color.Gray;
            this.guna2Button1.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(217)))), ((int)(((byte)(242)))));
            this.guna2Button1.Image = global::GUI.Properties.Resources.icons8_logout_25;
            this.guna2Button1.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.guna2Button1.Location = new System.Drawing.Point(12, 791);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(210, 45);
            this.guna2Button1.TabIndex = 9;
            this.guna2Button1.Text = "Đăng xuất";
            this.guna2Button1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // btnYeuCauSuaThietBi
            // 
            this.btnYeuCauSuaThietBi.BackColor = System.Drawing.Color.Transparent;
            this.btnYeuCauSuaThietBi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnYeuCauSuaThietBi.BorderRadius = 8;
            this.btnYeuCauSuaThietBi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnYeuCauSuaThietBi.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnYeuCauSuaThietBi.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnYeuCauSuaThietBi.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnYeuCauSuaThietBi.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnYeuCauSuaThietBi.FillColor = System.Drawing.Color.Transparent;
            this.btnYeuCauSuaThietBi.Font = new System.Drawing.Font("JetBrains Mono", 9.75F);
            this.btnYeuCauSuaThietBi.ForeColor = System.Drawing.Color.Gray;
            this.btnYeuCauSuaThietBi.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(217)))), ((int)(((byte)(242)))));
            this.btnYeuCauSuaThietBi.Image = global::GUI.Properties.Resources.icons8_YeuCauSuaChua_25;
            this.btnYeuCauSuaThietBi.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnYeuCauSuaThietBi.Location = new System.Drawing.Point(12, 157);
            this.btnYeuCauSuaThietBi.Name = "btnYeuCauSuaThietBi";
            this.btnYeuCauSuaThietBi.Size = new System.Drawing.Size(210, 45);
            this.btnYeuCauSuaThietBi.TabIndex = 7;
            this.btnYeuCauSuaThietBi.Text = "Yêu cầu sửa thiết bị";
            this.btnYeuCauSuaThietBi.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnYeuCauSuaThietBi.Click += new System.EventHandler(this.btnYeuCauSuaThietBi_Click);
            // 
            // btn_Lich_Day
            // 
            this.btn_Lich_Day.BackColor = System.Drawing.Color.Transparent;
            this.btn_Lich_Day.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_Lich_Day.BorderRadius = 8;
            this.btn_Lich_Day.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Lich_Day.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_Lich_Day.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_Lich_Day.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_Lich_Day.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_Lich_Day.FillColor = System.Drawing.Color.Transparent;
            this.btn_Lich_Day.Font = new System.Drawing.Font("JetBrains Mono", 9.75F);
            this.btn_Lich_Day.ForeColor = System.Drawing.Color.Gray;
            this.btn_Lich_Day.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(217)))), ((int)(((byte)(242)))));
            this.btn_Lich_Day.Image = global::GUI.Properties.Resources.icons8_schedule_25;
            this.btn_Lich_Day.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btn_Lich_Day.Location = new System.Drawing.Point(12, 106);
            this.btn_Lich_Day.Name = "btn_Lich_Day";
            this.btn_Lich_Day.Size = new System.Drawing.Size(210, 45);
            this.btn_Lich_Day.TabIndex = 6;
            this.btn_Lich_Day.Text = "Lịch dạy";
            this.btn_Lich_Day.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btn_Lich_Day.Click += new System.EventHandler(this.btn_Lich_Day_Click);
            // 
            // btnAccount
            // 
            this.btnAccount.BackColor = System.Drawing.Color.Transparent;
            this.btnAccount.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAccount.BorderRadius = 8;
            this.btnAccount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAccount.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAccount.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAccount.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAccount.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAccount.FillColor = System.Drawing.Color.Transparent;
            this.btnAccount.Font = new System.Drawing.Font("JetBrains Mono", 9.75F);
            this.btnAccount.ForeColor = System.Drawing.Color.Gray;
            this.btnAccount.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(217)))), ((int)(((byte)(242)))));
            this.btnAccount.Image = global::GUI.Properties.Resources.account;
            this.btnAccount.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnAccount.Location = new System.Drawing.Point(12, 740);
            this.btnAccount.Name = "btnAccount";
            this.btnAccount.Size = new System.Drawing.Size(210, 45);
            this.btnAccount.TabIndex = 4;
            this.btnAccount.Text = "Tài khoản";
            this.btnAccount.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // btnSetting
            // 
            this.btnSetting.BackColor = System.Drawing.Color.Transparent;
            this.btnSetting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSetting.BorderRadius = 8;
            this.btnSetting.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSetting.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSetting.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSetting.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSetting.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSetting.FillColor = System.Drawing.Color.Transparent;
            this.btnSetting.Font = new System.Drawing.Font("JetBrains Mono", 9.75F);
            this.btnSetting.ForeColor = System.Drawing.Color.Gray;
            this.btnSetting.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(217)))), ((int)(((byte)(242)))));
            this.btnSetting.Image = global::GUI.Properties.Resources.setting;
            this.btnSetting.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnSetting.Location = new System.Drawing.Point(11, 684);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(210, 45);
            this.btnSetting.TabIndex = 3;
            this.btnSetting.Text = "Cài đặt";
            this.btnSetting.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("JetBrains Mono", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.ForeColor = System.Drawing.Color.Gray;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(23, 651);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(69, 27);
            this.guna2HtmlLabel1.TabIndex = 2;
            this.guna2HtmlLabel1.Text = "OTHERS";
            // 
            // lbMenu
            // 
            this.lbMenu.AutoSize = false;
            this.lbMenu.BackColor = System.Drawing.Color.Transparent;
            this.lbMenu.Font = new System.Drawing.Font("JetBrains Mono", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMenu.ForeColor = System.Drawing.Color.Gray;
            this.lbMenu.Location = new System.Drawing.Point(23, 17);
            this.lbMenu.Name = "lbMenu";
            this.lbMenu.Size = new System.Drawing.Size(46, 27);
            this.lbMenu.TabIndex = 1;
            this.lbMenu.Text = "Menu";
            // 
            // btnHome
            // 
            this.btnHome.BackColor = System.Drawing.Color.Transparent;
            this.btnHome.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnHome.BorderRadius = 8;
            this.btnHome.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHome.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnHome.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnHome.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnHome.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnHome.FillColor = System.Drawing.Color.Transparent;
            this.btnHome.Font = new System.Drawing.Font("JetBrains Mono", 9.75F);
            this.btnHome.ForeColor = System.Drawing.Color.Gray;
            this.btnHome.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(217)))), ((int)(((byte)(242)))));
            this.btnHome.Image = global::GUI.Properties.Resources.icons8_home_25;
            this.btnHome.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnHome.Location = new System.Drawing.Point(12, 50);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(210, 45);
            this.btnHome.TabIndex = 0;
            this.btnHome.Text = "Trang chủ";
            this.btnHome.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // guna2Elipse2
            // 
            this.guna2Elipse2.BorderRadius = 10;
            // 
            // lb_NameForm
            // 
            this.lb_NameForm.AutoSize = true;
            this.lb_NameForm.Font = new System.Drawing.Font("JetBrains Mono", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_NameForm.Location = new System.Drawing.Point(264, 21);
            this.lb_NameForm.Name = "lb_NameForm";
            this.lb_NameForm.Size = new System.Drawing.Size(60, 26);
            this.lb_NameForm.TabIndex = 11;
            this.lb_NameForm.Text = "Home";
            // 
            // Dashboard_User
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1440, 960);
            this.Controls.Add(this.main);
            this.Font = new System.Drawing.Font("JetBrains Mono", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Dashboard_User";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard_User";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.Navbar.ResumeLayout(false);
            this.Navbar.PerformLayout();
            this.main.ResumeLayout(false);
            this.Sidebar.ResumeLayout(false);
            this.Sidebar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private System.Windows.Forms.Panel main;
        private Guna.UI2.WinForms.Guna2Panel Panel_Change_Form;
        private System.Windows.Forms.Panel Sidebar;
        private Guna.UI2.WinForms.Guna2Button btn_Lich_Day;
        private Guna.UI2.WinForms.Guna2Button btnAccount;
        private Guna.UI2.WinForms.Guna2Button btnSetting;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbMenu;
        private Guna.UI2.WinForms.Guna2Button btnHome;
        private System.Windows.Forms.Panel Navbar;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox2;
        private Guna.UI2.WinForms.Guna2ControlBox controlClose;
        private System.Windows.Forms.Panel panel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lb_home;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse2;
        private Guna.UI2.WinForms.Guna2Button btnYeuCauSuaThietBi;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private System.Windows.Forms.Label lb_NameForm;
    }
}