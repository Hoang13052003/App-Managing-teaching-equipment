using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Setting : Form
    {
        public Setting()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void nav_Paint(object sender, PaintEventArgs e)
        {
            int borderThickness = 1;
            Color borderColor = Color.FromArgb(200, 203, 217);

            // Vẽ border bottom
            using (Pen pen = new Pen(borderColor, borderThickness))
            {
                e.Graphics.DrawLine(pen, 0, nav.Height - borderThickness, nav.Width, nav.Height - borderThickness);
            }
        }

        private void btnLayout_Click(object sender, EventArgs e)
        {
            if (Form_Change.Controls.Count > 0)
            {
                Form_Change.Controls[0].Dispose();
            }

            // Tạo instance của form Layout
            Layout layoutForm = new Layout();

            // Thiết lập Layout form như là một form con (không phải là form độc lập)
            layoutForm.TopLevel = false;
            layoutForm.FormBorderStyle = FormBorderStyle.None;
            layoutForm.Dock = DockStyle.Fill; // Để Layout form khớp với kích thước của Panel

            // Thêm Layout form vào Panel
            Form_Change.Controls.Add(layoutForm);
            Form_Change.Tag = layoutForm;

            // Hiển thị Layout form
            layoutForm.Show();
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            Layout layoutForm = new Layout();

            // Thiết lập Layout form như là một form con (không phải là form độc lập)
            layoutForm.TopLevel = false;
            layoutForm.FormBorderStyle = FormBorderStyle.None;
            layoutForm.Dock = DockStyle.Fill; // Để Layout form khớp với kích thước của Panel

            // Thêm Layout form vào Panel
            Form_Change.Controls.Add(layoutForm);
            Form_Change.Tag = layoutForm;

            // Hiển thị Layout form
            layoutForm.Show();
        }
    }
}
