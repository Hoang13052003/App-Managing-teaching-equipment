﻿namespace GUI
{
    partial class YeuCauSuaThietBi
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnSearch = new Guna.UI2.WinForms.Guna2ImageButton();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.btnLamMoi = new Guna.UI2.WinForms.Guna2Button();
            this.main = new System.Windows.Forms.Panel();
            this.btnYeuCauMua = new Guna.UI2.WinForms.Guna2Button();
            this.cboThietBi = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cboLoaiTB = new Guna.UI2.WinForms.Guna2ComboBox();
            this.guna2HtmlLabel7 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel6 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnGuiYeuCau = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoa = new Guna.UI2.WinForms.Guna2Button();
            this.btnThem = new Guna.UI2.WinForms.Guna2Button();
            this.dgvDSThietBiSua = new Guna.UI2.WinForms.Guna2DataGridView();
            this.dgvDSChiTietThietBi = new Guna.UI2.WinForms.Guna2DataGridView();
            this.Navbar = new System.Windows.Forms.Panel();
            this.guna2HtmlLabel5 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2ControlBox2 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.controlClose = new Guna.UI2.WinForms.Guna2ControlBox();
            this.txtSearch = new Guna.UI2.WinForms.Guna2TextBox();
            this.main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSThietBiSua)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSChiTietThietBi)).BeginInit();
            this.Navbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.btnSearch.CausesValidation = false;
            this.btnSearch.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnSearch.HoverState.Image = global::GUI.Properties.Resources.Search;
            this.btnSearch.HoverState.ImageSize = new System.Drawing.Size(24, 24);
            this.btnSearch.Image = global::GUI.Properties.Resources.Search;
            this.btnSearch.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnSearch.ImageRotate = 0F;
            this.btnSearch.ImageSize = new System.Drawing.Size(24, 24);
            this.btnSearch.Location = new System.Drawing.Point(867, 23);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.PressedState.ImageSize = new System.Drawing.Size(24, 24);
            this.btnSearch.Size = new System.Drawing.Size(24, 24);
            this.btnSearch.TabIndex = 4;
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 10;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BorderRadius = 10;
            this.btnLamMoi.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLamMoi.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLamMoi.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLamMoi.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLamMoi.Font = new System.Drawing.Font("JetBrains Mono", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(373, 88);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(88, 30);
            this.btnLamMoi.TabIndex = 7;
            this.btnLamMoi.Text = "Làm mới";
            // 
            // main
            // 
            this.main.Controls.Add(this.btnYeuCauMua);
            this.main.Controls.Add(this.cboThietBi);
            this.main.Controls.Add(this.cboLoaiTB);
            this.main.Controls.Add(this.btnLamMoi);
            this.main.Controls.Add(this.guna2HtmlLabel7);
            this.main.Controls.Add(this.guna2HtmlLabel6);
            this.main.Controls.Add(this.btnGuiYeuCau);
            this.main.Controls.Add(this.btnXoa);
            this.main.Controls.Add(this.btnThem);
            this.main.Controls.Add(this.dgvDSThietBiSua);
            this.main.Controls.Add(this.dgvDSChiTietThietBi);
            this.main.Controls.Add(this.Navbar);
            this.main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.main.Location = new System.Drawing.Point(0, 0);
            this.main.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.main.Name = "main";
            this.main.Size = new System.Drawing.Size(1100, 500);
            this.main.TabIndex = 2;
            // 
            // btnYeuCauMua
            // 
            this.btnYeuCauMua.BorderRadius = 10;
            this.btnYeuCauMua.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnYeuCauMua.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnYeuCauMua.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnYeuCauMua.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnYeuCauMua.Font = new System.Drawing.Font("JetBrains Mono", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnYeuCauMua.ForeColor = System.Drawing.Color.White;
            this.btnYeuCauMua.Location = new System.Drawing.Point(945, 75);
            this.btnYeuCauMua.Name = "btnYeuCauMua";
            this.btnYeuCauMua.Size = new System.Drawing.Size(134, 35);
            this.btnYeuCauMua.TabIndex = 9;
            this.btnYeuCauMua.Text = "Yêu cầu mua →";
            // 
            // cboThietBi
            // 
            this.cboThietBi.BackColor = System.Drawing.Color.Transparent;
            this.cboThietBi.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboThietBi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboThietBi.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboThietBi.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboThietBi.Font = new System.Drawing.Font("JetBrains Mono", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboThietBi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboThietBi.ItemHeight = 30;
            this.cboThietBi.Location = new System.Drawing.Point(122, 130);
            this.cboThietBi.Name = "cboThietBi";
            this.cboThietBi.Size = new System.Drawing.Size(230, 36);
            this.cboThietBi.TabIndex = 8;
            // 
            // cboLoaiTB
            // 
            this.cboLoaiTB.BackColor = System.Drawing.Color.Transparent;
            this.cboLoaiTB.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboLoaiTB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiTB.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboLoaiTB.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboLoaiTB.Font = new System.Drawing.Font("JetBrains Mono", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLoaiTB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboLoaiTB.ItemHeight = 30;
            this.cboLoaiTB.Location = new System.Drawing.Point(122, 85);
            this.cboLoaiTB.Name = "cboLoaiTB";
            this.cboLoaiTB.Size = new System.Drawing.Size(230, 36);
            this.cboLoaiTB.TabIndex = 8;
            // 
            // guna2HtmlLabel7
            // 
            this.guna2HtmlLabel7.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel7.Location = new System.Drawing.Point(22, 140);
            this.guna2HtmlLabel7.Name = "guna2HtmlLabel7";
            this.guna2HtmlLabel7.Size = new System.Drawing.Size(38, 15);
            this.guna2HtmlLabel7.TabIndex = 5;
            this.guna2HtmlLabel7.Text = "Thiết bị";
            // 
            // guna2HtmlLabel6
            // 
            this.guna2HtmlLabel6.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel6.Location = new System.Drawing.Point(22, 95);
            this.guna2HtmlLabel6.Name = "guna2HtmlLabel6";
            this.guna2HtmlLabel6.Size = new System.Drawing.Size(57, 15);
            this.guna2HtmlLabel6.TabIndex = 5;
            this.guna2HtmlLabel6.Text = "Loại thiết bị";
            // 
            // btnGuiYeuCau
            // 
            this.btnGuiYeuCau.BorderRadius = 10;
            this.btnGuiYeuCau.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnGuiYeuCau.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnGuiYeuCau.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnGuiYeuCau.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnGuiYeuCau.Font = new System.Drawing.Font("JetBrains Mono", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuiYeuCau.ForeColor = System.Drawing.Color.White;
            this.btnGuiYeuCau.Location = new System.Drawing.Point(945, 140);
            this.btnGuiYeuCau.Name = "btnGuiYeuCau";
            this.btnGuiYeuCau.Size = new System.Drawing.Size(134, 35);
            this.btnGuiYeuCau.TabIndex = 3;
            this.btnGuiYeuCau.Text = "Gửi yêu cầu →";
            // 
            // btnXoa
            // 
            this.btnXoa.BorderRadius = 10;
            this.btnXoa.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXoa.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXoa.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXoa.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXoa.FillColor = System.Drawing.Color.Red;
            this.btnXoa.Font = new System.Drawing.Font("JetBrains Mono", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(528, 363);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(45, 45);
            this.btnXoa.TabIndex = 3;
            this.btnXoa.Text = "←";
            // 
            // btnThem
            // 
            this.btnThem.BorderRadius = 10;
            this.btnThem.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnThem.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnThem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnThem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnThem.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnThem.Font = new System.Drawing.Font("JetBrains Mono", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(528, 275);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(45, 45);
            this.btnThem.TabIndex = 3;
            this.btnThem.Text = "→";
            // 
            // dgvDSThietBiSua
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvDSThietBiSua.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDSThietBiSua.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgvDSThietBiSua.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("JetBrains Mono", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDSThietBiSua.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDSThietBiSua.ColumnHeadersHeight = 15;
            this.dgvDSThietBiSua.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("JetBrains Mono", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDSThietBiSua.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDSThietBiSua.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDSThietBiSua.Location = new System.Drawing.Point(579, 187);
            this.dgvDSThietBiSua.Name = "dgvDSThietBiSua";
            this.dgvDSThietBiSua.RowHeadersVisible = false;
            this.dgvDSThietBiSua.Size = new System.Drawing.Size(510, 300);
            this.dgvDSThietBiSua.TabIndex = 2;
            this.dgvDSThietBiSua.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvDSThietBiSua.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvDSThietBiSua.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvDSThietBiSua.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvDSThietBiSua.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvDSThietBiSua.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvDSThietBiSua.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDSThietBiSua.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvDSThietBiSua.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvDSThietBiSua.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("JetBrains Mono", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDSThietBiSua.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvDSThietBiSua.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvDSThietBiSua.ThemeStyle.HeaderStyle.Height = 15;
            this.dgvDSThietBiSua.ThemeStyle.ReadOnly = false;
            this.dgvDSThietBiSua.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvDSThietBiSua.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvDSThietBiSua.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("JetBrains Mono", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDSThietBiSua.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvDSThietBiSua.ThemeStyle.RowsStyle.Height = 22;
            this.dgvDSThietBiSua.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDSThietBiSua.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // dgvDSChiTietThietBi
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.dgvDSChiTietThietBi.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvDSChiTietThietBi.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgvDSChiTietThietBi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("JetBrains Mono", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDSChiTietThietBi.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvDSChiTietThietBi.ColumnHeadersHeight = 15;
            this.dgvDSChiTietThietBi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("JetBrains Mono", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDSChiTietThietBi.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvDSChiTietThietBi.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDSChiTietThietBi.Location = new System.Drawing.Point(12, 187);
            this.dgvDSChiTietThietBi.Name = "dgvDSChiTietThietBi";
            this.dgvDSChiTietThietBi.RowHeadersVisible = false;
            this.dgvDSChiTietThietBi.Size = new System.Drawing.Size(510, 300);
            this.dgvDSChiTietThietBi.TabIndex = 2;
            this.dgvDSChiTietThietBi.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvDSChiTietThietBi.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvDSChiTietThietBi.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvDSChiTietThietBi.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvDSChiTietThietBi.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvDSChiTietThietBi.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvDSChiTietThietBi.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDSChiTietThietBi.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvDSChiTietThietBi.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvDSChiTietThietBi.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("JetBrains Mono", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDSChiTietThietBi.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvDSChiTietThietBi.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvDSChiTietThietBi.ThemeStyle.HeaderStyle.Height = 15;
            this.dgvDSChiTietThietBi.ThemeStyle.ReadOnly = false;
            this.dgvDSChiTietThietBi.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvDSChiTietThietBi.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvDSChiTietThietBi.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("JetBrains Mono", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDSChiTietThietBi.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvDSChiTietThietBi.ThemeStyle.RowsStyle.Height = 22;
            this.dgvDSChiTietThietBi.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDSChiTietThietBi.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // Navbar
            // 
            this.Navbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Navbar.Controls.Add(this.guna2HtmlLabel5);
            this.Navbar.Controls.Add(this.guna2ControlBox2);
            this.Navbar.Controls.Add(this.guna2ControlBox1);
            this.Navbar.Controls.Add(this.controlClose);
            this.Navbar.Controls.Add(this.btnSearch);
            this.Navbar.Controls.Add(this.txtSearch);
            this.Navbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.Navbar.Location = new System.Drawing.Point(0, 0);
            this.Navbar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Navbar.Name = "Navbar";
            this.Navbar.Size = new System.Drawing.Size(1100, 65);
            this.Navbar.TabIndex = 1;
            // 
            // guna2HtmlLabel5
            // 
            this.guna2HtmlLabel5.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel5.Font = new System.Drawing.Font("JetBrains Mono", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel5.ForeColor = System.Drawing.Color.White;
            this.guna2HtmlLabel5.Location = new System.Drawing.Point(22, 13);
            this.guna2HtmlLabel5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.guna2HtmlLabel5.Name = "guna2HtmlLabel5";
            this.guna2HtmlLabel5.Size = new System.Drawing.Size(403, 38);
            this.guna2HtmlLabel5.TabIndex = 0;
            this.guna2HtmlLabel5.Text = "YÊU CẦU SỬA CHỮA THIẾT BỊ";
            // 
            // guna2ControlBox2
            // 
            this.guna2ControlBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox2.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.guna2ControlBox2.FillColor = System.Drawing.Color.White;
            this.guna2ControlBox2.IconColor = System.Drawing.Color.DimGray;
            this.guna2ControlBox2.Location = new System.Drawing.Point(977, 17);
            this.guna2ControlBox2.Name = "guna2ControlBox2";
            this.guna2ControlBox2.Size = new System.Drawing.Size(30, 30);
            this.guna2ControlBox2.TabIndex = 9;
            // 
            // guna2ControlBox1
            // 
            this.guna2ControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox1.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MaximizeBox;
            this.guna2ControlBox1.FillColor = System.Drawing.Color.White;
            this.guna2ControlBox1.IconColor = System.Drawing.Color.DimGray;
            this.guna2ControlBox1.Location = new System.Drawing.Point(1013, 17);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.Size = new System.Drawing.Size(30, 30);
            this.guna2ControlBox1.TabIndex = 8;
            // 
            // controlClose
            // 
            this.controlClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.controlClose.FillColor = System.Drawing.Color.White;
            this.controlClose.IconColor = System.Drawing.Color.DimGray;
            this.controlClose.Location = new System.Drawing.Point(1049, 17);
            this.controlClose.Name = "controlClose";
            this.controlClose.Size = new System.Drawing.Size(30, 30);
            this.controlClose.TabIndex = 7;
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.Transparent;
            this.txtSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtSearch.BorderColor = System.Drawing.Color.White;
            this.txtSearch.BorderRadius = 10;
            this.txtSearch.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            this.txtSearch.BorderThickness = 0;
            this.txtSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSearch.DefaultText = "";
            this.txtSearch.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSearch.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSearch.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.txtSearch.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSearch.Font = new System.Drawing.Font("JetBrains Mono", 9.75F);
            this.txtSearch.ForeColor = System.Drawing.Color.Black;
            this.txtSearch.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSearch.Location = new System.Drawing.Point(507, 13);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PasswordChar = '\0';
            this.txtSearch.PlaceholderForeColor = System.Drawing.Color.Black;
            this.txtSearch.PlaceholderText = "Search ...";
            this.txtSearch.SelectedText = "";
            this.txtSearch.Size = new System.Drawing.Size(397, 40);
            this.txtSearch.TabIndex = 0;
            // 
            // YeuCauSuaThietBi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 500);
            this.Controls.Add(this.main);
            this.Font = new System.Drawing.Font("JetBrains Mono", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "YeuCauSuaThietBi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YeuCauThietBi";
            this.main.ResumeLayout(false);
            this.main.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSThietBiSua)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSChiTietThietBi)).EndInit();
            this.Navbar.ResumeLayout(false);
            this.Navbar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2ImageButton btnSearch;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2Button btnLamMoi;
        private System.Windows.Forms.Panel main;
        private Guna.UI2.WinForms.Guna2Button btnXoa;
        private Guna.UI2.WinForms.Guna2Button btnThem;
        private Guna.UI2.WinForms.Guna2DataGridView dgvDSChiTietThietBi;
        private System.Windows.Forms.Panel Navbar;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel5;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox2;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private Guna.UI2.WinForms.Guna2ControlBox controlClose;
        private Guna.UI2.WinForms.Guna2TextBox txtSearch;
        private Guna.UI2.WinForms.Guna2ComboBox cboLoaiTB;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel6;
        private Guna.UI2.WinForms.Guna2ComboBox cboThietBi;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel7;
        private Guna.UI2.WinForms.Guna2DataGridView dgvDSThietBiSua;
        private Guna.UI2.WinForms.Guna2Button btnGuiYeuCau;
        private Guna.UI2.WinForms.Guna2Button btnYeuCauMua;
    }
}