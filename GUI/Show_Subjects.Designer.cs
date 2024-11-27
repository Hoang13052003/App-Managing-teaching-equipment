namespace GUI
{
    partial class Show_Subjects
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
            this.pannel_Lich_Hoc = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.lb_Ten_Bai_Hoc = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lb_Phong_Hoc = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lb_Gio_Hoc = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lb_MaMH_Lop = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lb_Ten_Mon_Hoc = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.pannel_Lich_Hoc.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 0;
            this.guna2Elipse1.TargetControl = this;
            // 
            // pannel_Lich_Hoc
            // 
            this.pannel_Lich_Hoc.AutoSize = true;
            this.pannel_Lich_Hoc.BackColor = System.Drawing.Color.Transparent;
            this.pannel_Lich_Hoc.Controls.Add(this.lb_Ten_Bai_Hoc);
            this.pannel_Lich_Hoc.Controls.Add(this.lb_Phong_Hoc);
            this.pannel_Lich_Hoc.Controls.Add(this.lb_Gio_Hoc);
            this.pannel_Lich_Hoc.Controls.Add(this.lb_MaMH_Lop);
            this.pannel_Lich_Hoc.Controls.Add(this.lb_Ten_Mon_Hoc);
            this.pannel_Lich_Hoc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pannel_Lich_Hoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pannel_Lich_Hoc.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.pannel_Lich_Hoc.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pannel_Lich_Hoc.Location = new System.Drawing.Point(0, 0);
            this.pannel_Lich_Hoc.Name = "pannel_Lich_Hoc";
            this.pannel_Lich_Hoc.Padding = new System.Windows.Forms.Padding(10);
            this.pannel_Lich_Hoc.Radius = 10;
            this.pannel_Lich_Hoc.ShadowColor = System.Drawing.Color.Black;
            this.pannel_Lich_Hoc.ShadowDepth = 35;
            this.pannel_Lich_Hoc.Size = new System.Drawing.Size(227, 165);
            this.pannel_Lich_Hoc.TabIndex = 1;
            this.pannel_Lich_Hoc.Click += new System.EventHandler(this.pannel_Lich_Hoc_Click);
            this.pannel_Lich_Hoc.Paint += new System.Windows.Forms.PaintEventHandler(this.pannel_Lich_Hoc_Paint);
            // 
            // lb_Ten_Bai_Hoc
            // 
            this.lb_Ten_Bai_Hoc.BackColor = System.Drawing.Color.Transparent;
            this.lb_Ten_Bai_Hoc.Enabled = false;
            this.lb_Ten_Bai_Hoc.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Ten_Bai_Hoc.Location = new System.Drawing.Point(18, 115);
            this.lb_Ten_Bai_Hoc.Name = "lb_Ten_Bai_Hoc";
            this.lb_Ten_Bai_Hoc.Size = new System.Drawing.Size(59, 25);
            this.lb_Ten_Bai_Hoc.TabIndex = 4;
            this.lb_Ten_Bai_Hoc.Text = "Bài học";
            this.lb_Ten_Bai_Hoc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_Phong_Hoc
            // 
            this.lb_Phong_Hoc.BackColor = System.Drawing.Color.Transparent;
            this.lb_Phong_Hoc.Enabled = false;
            this.lb_Phong_Hoc.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Phong_Hoc.Location = new System.Drawing.Point(18, 91);
            this.lb_Phong_Hoc.Name = "lb_Phong_Hoc";
            this.lb_Phong_Hoc.Size = new System.Drawing.Size(96, 25);
            this.lb_Phong_Hoc.TabIndex = 3;
            this.lb_Phong_Hoc.Text = "Phòng A209";
            this.lb_Phong_Hoc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_Gio_Hoc
            // 
            this.lb_Gio_Hoc.BackColor = System.Drawing.Color.Transparent;
            this.lb_Gio_Hoc.Enabled = false;
            this.lb_Gio_Hoc.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Gio_Hoc.Location = new System.Drawing.Point(18, 66);
            this.lb_Gio_Hoc.Name = "lb_Gio_Hoc";
            this.lb_Gio_Hoc.Size = new System.Drawing.Size(66, 25);
            this.lb_Gio_Hoc.TabIndex = 2;
            this.lb_Gio_Hoc.Text = "Tiết 1 -2";
            this.lb_Gio_Hoc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_MaMH_Lop
            // 
            this.lb_MaMH_Lop.BackColor = System.Drawing.Color.Transparent;
            this.lb_MaMH_Lop.Enabled = false;
            this.lb_MaMH_Lop.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_MaMH_Lop.Location = new System.Drawing.Point(18, 41);
            this.lb_MaMH_Lop.Name = "lb_MaMH_Lop";
            this.lb_MaMH_Lop.Size = new System.Drawing.Size(139, 25);
            this.lb_MaMH_Lop.TabIndex = 1;
            this.lb_MaMH_Lop.Text = "324552252 - 12A8";
            this.lb_MaMH_Lop.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_Ten_Mon_Hoc
            // 
            this.lb_Ten_Mon_Hoc.BackColor = System.Drawing.Color.Transparent;
            this.lb_Ten_Mon_Hoc.Enabled = false;
            this.lb_Ten_Mon_Hoc.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Ten_Mon_Hoc.Location = new System.Drawing.Point(18, 13);
            this.lb_Ten_Mon_Hoc.Name = "lb_Ten_Mon_Hoc";
            this.lb_Ten_Mon_Hoc.Size = new System.Drawing.Size(78, 25);
            this.lb_Ten_Mon_Hoc.TabIndex = 0;
            this.lb_Ten_Mon_Hoc.Text = "Toán Học";
            this.lb_Ten_Mon_Hoc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Show_Subjects
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(227, 165);
            this.Controls.Add(this.pannel_Lich_Hoc);
            this.Font = new System.Drawing.Font("JetBrains Mono", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Show_Subjects";
            this.Text = "Show_Subjects";
            this.Load += new System.EventHandler(this.Show_Subjects_Load);
            this.pannel_Lich_Hoc.ResumeLayout(false);
            this.pannel_Lich_Hoc.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2ShadowPanel pannel_Lich_Hoc;
        private Guna.UI2.WinForms.Guna2HtmlLabel lb_Phong_Hoc;
        private Guna.UI2.WinForms.Guna2HtmlLabel lb_Gio_Hoc;
        private Guna.UI2.WinForms.Guna2HtmlLabel lb_MaMH_Lop;
        private Guna.UI2.WinForms.Guna2HtmlLabel lb_Ten_Mon_Hoc;
        private Guna.UI2.WinForms.Guna2HtmlLabel lb_Ten_Bai_Hoc;
    }
}