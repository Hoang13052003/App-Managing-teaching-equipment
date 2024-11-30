using Microsoft.Win32;
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
            FormTask.LbNameForm = lb_NameForm;
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
            Form x = new Home_User();
            lb_NameForm.Text = x.Text;
        }

        private void btn_Lich_Day_Click(object sender, EventArgs e)
        {
            FormTask.OpenFormInPanel<Teaching_Schedule>(Panel_Change_Form);
            Form x = new Teaching_Schedule();
            lb_NameForm.Text = x.Text;
        }

        private void btnYeuCauSuaThietBi_Click(object sender, EventArgs e)
        {
            FormTask.OpenFormInPanel<YeuCauSuaThietBi>(Panel_Change_Form);
            Form x = new YeuCauSuaThietBi();
            lb_NameForm.Text = x.Text;
        }
        

        private void controlClose_Click(object sender, EventArgs e)
        {
           
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất không?",
                "Xác nhận đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                // Cập nhật trạng thái đăng nhập trong Registry về false
                UpdateLoginState(false);

                // Mở lại form đăng nhập
                FormTask.OpenDashboard_Out<Login>(this);
            }
            else
            {
                // Nếu người dùng chọn "No", hủy thao tác đăng xuất
                MessageBox.Show("Hủy đăng xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void UpdateLoginState(bool isLoggedIn)
        {
            // Truy cập Registry và cập nhật trạng thái đăng nhập
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\MyApp", true);
            if (key != null)
            {
                key.SetValue("IsLoggedIn", isLoggedIn);
                key.Close();
            }
        }

        private void Dashboard_User_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show(
               "Bạn có chắc chắn muốn thoát ứng dụng không?",
               "Xác nhận",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question
           );

            if (result == DialogResult.No)
            {
                e.Cancel = true;  // Hủy hành động đóng form
            }
            else
            {
                Environment.Exit(0);
            }
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            new Profile().ShowDialog();
        }
    }
}
