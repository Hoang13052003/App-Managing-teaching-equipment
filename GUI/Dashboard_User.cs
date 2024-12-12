using BUS;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace GUI
{
    public partial class Dashboard_User : Form
    {
        private bool isLoggingOut = false;

        private ThongTinCaNhanBUS _ttcnBUS = new ThongTinCaNhanBUS();
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

        private async void btn_Lich_Day_Click(object sender, EventArgs e)
        {
            loading_panel loading = new loading_panel();
            loading.TopLevel = false;
            Panel_Change_Form.Controls.Clear();
            Panel_Change_Form.Controls.Add(loading);
            loading.Dock = DockStyle.Fill;
            loading.Show();

            // Mở form Teaching_Schedule ngầm (ẩn)
            Teaching_Schedule newForm = new Teaching_Schedule();
            newForm.TopLevel = false;
            newForm.FormBorderStyle = FormBorderStyle.None;
            newForm.Dock = DockStyle.Fill;
            newForm.Visible = false; // Form không hiển thị ngay lập tức
            Panel_Change_Form.Controls.Add(newForm);

            // Giả lập thời gian chờ 5 giây (bạn có thể thay thế bằng các tác vụ nặng thực tế)
            await Task.Delay(5000); // 5 giây

            // Ẩn loading và hiển thị form Teaching_Schedule
            loading.Close(); // Đóng loading
            newForm.Visible = true; // Hiển thị Teaching_Schedule form

            lb_NameForm.Text = newForm.Text;
        }
        
        private void timer_show_form_Tick(object sender, EventArgs e)
        {
            
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

        private void Dashboard_User_FormClosing(object sender, FormClosingEventArgs e)
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

        private void btnAccount_Click(object sender, EventArgs e)
        {
            new Profile().ShowDialog();
        }

        private async void Dashboard_User_Load(object sender, EventArgs e)
        {
            // Kiểm tra tồn tại thông tin cá nhân
            bool checkThongTinCaNhan = _ttcnBUS.CheckIfCodeExists(AccountInfo.MaNguoiDung);
            if (!checkThongTinCaNhan)
            {
                _ttcnBUS.InsertNewNull(AccountInfo.MaNguoiDung);

                // Delay 2 giây
                await Task.Delay(1000);

                // Hiển thị thông báo và mở form nhập thông tin cá nhân
                MessageBox.Show("Bạn vui lòng nhập đầy đủ thông tin cá nhân để tránh mất tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                new Profile().ShowDialog();
            }
        }
    }
}
