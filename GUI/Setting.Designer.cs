namespace GUI
{
    partial class Setting
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
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.main = new Guna.UI2.WinForms.Guna2Panel();
            this.Form_Change = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            this.btnLayout = new Guna.UI2.WinForms.Guna2Button();
            this.nav = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2ControlBox2 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.controlClose = new Guna.UI2.WinForms.Guna2ControlBox();
            this.main.SuspendLayout();
            this.guna2Panel3.SuspendLayout();
            this.nav.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 10;
            this.guna2Elipse1.TargetControl = this;
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("JetBrains Mono", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(14, 13);
            this.guna2HtmlLabel1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(59, 19);
            this.guna2HtmlLabel1.TabIndex = 8;
            this.guna2HtmlLabel1.Text = "Setting";
            // 
            // main
            // 
            this.main.Controls.Add(this.Form_Change);
            this.main.Controls.Add(this.guna2Panel3);
            this.main.Controls.Add(this.nav);
            this.main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.main.Location = new System.Drawing.Point(0, 0);
            this.main.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.main.Name = "main";
            this.main.Size = new System.Drawing.Size(940, 700);
            this.main.TabIndex = 9;
            // 
            // Form_Change
            // 
            this.Form_Change.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Form_Change.Location = new System.Drawing.Point(180, 50);
            this.Form_Change.Name = "Form_Change";
            this.Form_Change.Size = new System.Drawing.Size(760, 650);
            this.Form_Change.TabIndex = 11;
            // 
            // guna2Panel3
            // 
            this.guna2Panel3.Controls.Add(this.btnLayout);
            this.guna2Panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.guna2Panel3.Location = new System.Drawing.Point(0, 50);
            this.guna2Panel3.Name = "guna2Panel3";
            this.guna2Panel3.Size = new System.Drawing.Size(180, 650);
            this.guna2Panel3.TabIndex = 10;
            // 
            // btnLayout
            // 
            this.btnLayout.BackColor = System.Drawing.Color.Transparent;
            this.btnLayout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnLayout.BorderRadius = 8;
            this.btnLayout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLayout.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLayout.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLayout.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLayout.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLayout.FillColor = System.Drawing.Color.Transparent;
            this.btnLayout.Font = new System.Drawing.Font("JetBrains Mono", 9.75F);
            this.btnLayout.ForeColor = System.Drawing.Color.Gray;
            this.btnLayout.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.btnLayout.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnLayout.Location = new System.Drawing.Point(14, 8);
            this.btnLayout.Name = "btnLayout";
            this.btnLayout.Size = new System.Drawing.Size(150, 38);
            this.btnLayout.TabIndex = 1;
            this.btnLayout.Text = "Layout";
            this.btnLayout.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnLayout.Click += new System.EventHandler(this.btnLayout_Click);
            // 
            // nav
            // 
            this.nav.Controls.Add(this.guna2ControlBox2);
            this.nav.Controls.Add(this.guna2HtmlLabel1);
            this.nav.Controls.Add(this.guna2ControlBox1);
            this.nav.Controls.Add(this.controlClose);
            this.nav.Dock = System.Windows.Forms.DockStyle.Top;
            this.nav.Location = new System.Drawing.Point(0, 0);
            this.nav.Name = "nav";
            this.nav.Size = new System.Drawing.Size(940, 50);
            this.nav.TabIndex = 9;
            this.nav.Paint += new System.Windows.Forms.PaintEventHandler(this.nav_Paint);
            // 
            // guna2ControlBox2
            // 
            this.guna2ControlBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox2.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.guna2ControlBox2.FillColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox2.IconColor = System.Drawing.Color.DimGray;
            this.guna2ControlBox2.Location = new System.Drawing.Point(830, 12);
            this.guna2ControlBox2.Name = "guna2ControlBox2";
            this.guna2ControlBox2.Size = new System.Drawing.Size(30, 30);
            this.guna2ControlBox2.TabIndex = 12;
            // 
            // guna2ControlBox1
            // 
            this.guna2ControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox1.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MaximizeBox;
            this.guna2ControlBox1.FillColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox1.IconColor = System.Drawing.Color.DimGray;
            this.guna2ControlBox1.Location = new System.Drawing.Point(866, 12);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.Size = new System.Drawing.Size(30, 30);
            this.guna2ControlBox1.TabIndex = 11;
            // 
            // controlClose
            // 
            this.controlClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.controlClose.FillColor = System.Drawing.Color.Transparent;
            this.controlClose.IconColor = System.Drawing.Color.DimGray;
            this.controlClose.Location = new System.Drawing.Point(902, 12);
            this.controlClose.Name = "controlClose";
            this.controlClose.Size = new System.Drawing.Size(30, 30);
            this.controlClose.TabIndex = 10;
            // 
            // Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(940, 700);
            this.Controls.Add(this.main);
            this.Font = new System.Drawing.Font("JetBrains Mono", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "Setting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Setting";
            this.Load += new System.EventHandler(this.Setting_Load);
            this.main.ResumeLayout(false);
            this.guna2Panel3.ResumeLayout(false);
            this.nav.ResumeLayout(false);
            this.nav.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2Panel main;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
        private Guna.UI2.WinForms.Guna2Panel nav;
        private Guna.UI2.WinForms.Guna2Button btnLayout;
        private Guna.UI2.WinForms.Guna2Panel Form_Change;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox2;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private Guna.UI2.WinForms.Guna2ControlBox controlClose;
    }
}