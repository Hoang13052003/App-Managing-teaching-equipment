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
    public partial class Dashboard_User : Form
    {
        public Dashboard_User()
        {
            InitializeComponent();
            FormTask.Pannel_change = Panel_Change_Form;
            FormTask.OpenFormInPanel<Home_User>(Panel_Change_Form);
        }

        private void Navbar_Paint(object sender, PaintEventArgs e)
        {
            int borderThickness = 1;
            Color borderColor = Color.FromArgb(200, 203, 217);

            // Vẽ border bottom
            using (Pen pen = new Pen(borderColor, borderThickness))
            {
                e.Graphics.DrawLine(pen, 0, Navbar.Height - borderThickness, Navbar.Width, Navbar.Height - borderThickness);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            int borderThickness = 1;
            Color borderColor = Color.FromArgb(200, 203, 217);

            // Vẽ border bottom
            using (Pen pen = new Pen(borderColor, borderThickness))
            {
                e.Graphics.DrawLine(pen, 0, panel1.Height - borderThickness, panel1.Width, panel1.Height - borderThickness);
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            FormTask.OpenFormInPanel<Home_User>(Panel_Change_Form);
        }

        private void btn_Lich_Day_Click(object sender, EventArgs e)
        {
            FormTask.OpenFormInPanel<Teaching_Schedule>(Panel_Change_Form);
        }

        private void btnYeuCauSuaThietBi_Click(object sender, EventArgs e)
        {
            FormTask.OpenFormInPanel<YeuCauSuaThietBi>(Panel_Change_Form);
        }
        private void btnBaoCaoThietBiHuHong_Click(object sender, EventArgs e)
        {
            FormTask.OpenFormInPanel<BaoCaoThietBiHuHong>(Panel_Change_Form);
        }

        private void controlClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
