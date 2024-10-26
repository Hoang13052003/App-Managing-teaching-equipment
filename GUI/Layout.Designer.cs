namespace GUI
{
    partial class Layout
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
            this.SwitchSangToi = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.SuspendLayout();
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 0;
            this.guna2Elipse1.TargetControl = this;
            // 
            // SwitchSangToi
            // 
            this.SwitchSangToi.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(193)))), ((int)(((byte)(255)))));
            this.SwitchSangToi.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(193)))), ((int)(((byte)(255)))));
            this.SwitchSangToi.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.SwitchSangToi.CheckedState.InnerColor = System.Drawing.Color.White;
            this.SwitchSangToi.Location = new System.Drawing.Point(125, 12);
            this.SwitchSangToi.Name = "SwitchSangToi";
            this.SwitchSangToi.Size = new System.Drawing.Size(35, 20);
            this.SwitchSangToi.TabIndex = 0;
            this.SwitchSangToi.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.SwitchSangToi.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.SwitchSangToi.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.SwitchSangToi.UncheckedState.InnerColor = System.Drawing.Color.White;
            this.SwitchSangToi.CheckedChanged += new System.EventHandler(this.SwitchSangToi_CheckedChanged);
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("JetBrains Mono", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(12, 13);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(107, 19);
            this.guna2HtmlLabel1.TabIndex = 1;
            this.guna2HtmlLabel1.Text = "Giao diện tối";
            // 
            // Layout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(760, 650);
            this.Controls.Add(this.guna2HtmlLabel1);
            this.Controls.Add(this.SwitchSangToi);
            this.Font = new System.Drawing.Font("JetBrains Mono", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Layout";
            this.Text = "Layout";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2ToggleSwitch SwitchSangToi;
    }
}