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
    public partial class Dashboard_Admin : Form
    {
        private bool isLoggingOut = false;

        public Dashboard_Admin()
        {
            InitializeComponent();
        }
        //public static void UpdateFormName(string formName)
        //{
        //    lb_NameForm.Text = formName;
        //}
        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void lb_home_Click(object sender, EventArgs e)
        {

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

        private void btnSetting_Click(object sender, EventArgs e)
        {
            Form x = new Setting();
            x.Show();
            x.BringToFront();
        }

        private void controlClose_Click(object sender, EventArgs e)
        {
           
        }

        private void btnQuanLyThietBi_Click(object sender, EventArgs e)
        {
            FormTask.OpenFormInPanel<Manage_Device>(Panel_Change_Form);
            Form x = new Manage_Device();
            lb_NameForm.Text = x.Text;
        }

        private void btnQuanLyNhaCungCap_Click(object sender, EventArgs e)
        {
            FormTask.OpenFormInPanel<Supplier>(Panel_Change_Form);
            Form x = new Supplier();
            lb_NameForm.Text = x.Text;
        }

        private void btnMuonTraThietBi_Click(object sender, EventArgs e)
        {
            FormTask.OpenFormInPanel<FormMuonThietBi>(Panel_Change_Form);
            Form x = new FormMuonThietBi();
            lb_NameForm.Text = x.Text;
        }

        private void btnQuanLyThietBiHuHong_Click(object sender, EventArgs e)
        {
            FormTask.OpenFormInPanel<XuLyThietBiHuHong>(Panel_Change_Form);
            Form x = new XuLyThietBiHuHong();
            lb_NameForm.Text = x.Text;
        }

        private void btnBaoDuongThietBi_Click(object sender, EventArgs e)
        {
            FormTask.OpenFormInPanel<BaoDuong>(Panel_Change_Form);
            Form x = new BaoDuong();
            lb_NameForm.Text = x.Text;
        }

        private void btnNhapThietBi_Click(object sender, EventArgs e)
        {
            FormTask.OpenFormInPanel<NhapThietBi>(Panel_Change_Form);
            Form x = new NhapThietBi();
            lb_NameForm.Text = x.Text;
        }

        private void btnThoiKhoaBieu_Click(object sender, EventArgs e)
        {
            FormTask.OpenFormInPanel<ThoiKhoaBieu>(Panel_Change_Form);
            Form x = new ThoiKhoaBieu();
            lb_NameForm.Text = x.Text;
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
                isLoggingOut = true;
                // Cập nhật trạng thái đăng nhập trong Registry về false
                UpdateLoginState(false);

                // Mở lại form đăng nhập
                FormTask.OpenDashboard<Login>(this);
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
                key.SetValue("UserName", string.Empty);
                key.SetValue("PassWord", string.Empty);
                key.Close();
            }
        }

        private void Dashboard_Admin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isLoggingOut)
            {
                // Nếu đang đăng xuất, không cần xác nhận và không hủy thao tác đóng form
                isLoggingOut = false;
                return;  // Không làm gì cả, form sẽ đóng
            }

            DialogResult result = MessageBox.Show(
              "Bạn có chắc chắn muốn thoát ứng dụng không?",
              "Xác nhận",
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Question
            );

            if (result == DialogResult.No)
            {
                e.Cancel = true; 
            }
            else
            {
                Environment.Exit(0);
            }
        }

        private void btnTB_MH_BH_Click(object sender, EventArgs e)
        {
            FormTask.OpenFormInPanel<QuanLyThietBi_MonHoc_BaiHoc>(Panel_Change_Form);
            Form x = new QuanLyThietBi_MonHoc_BaiHoc();
            lb_NameForm.Text = x.Text;
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            new Profile().ShowDialog();
        }
    }
}
