using BUS;
using DTO;
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
    public partial class Profile : Form
    {
        private ThongTinCaNhanBUS _ttcnBUS;
        private ThongTinCaNhanDTO _dto;

        public Profile()
        {
            _ttcnBUS = new ThongTinCaNhanBUS();
            InitializeComponent();
        }

        private void Profile_Load(object sender, EventArgs e)
        {
            _dto = _ttcnBUS.GetByMaNguoiDung(AccountInfo.MaNguoiDung);
            
            load_data_cbbGioTinh();
            load_data();
            controls(false);
        }

        private void load_data()
        {
            lbName.Text = _dto.HoTen;
            lbEmail.Text = _dto.Email;
            txtMaGV.Text = _dto.MaNguoiDung;
            txtHoTen.Text = _dto.HoTen;
            txtSDT.Text = _dto.SDT;
            txtGmail.Text = _dto.Email;
            txtDiaChi.Text = _dto.DiaChi;
            dtbNgaySinh.Value = _dto.NgaySinh ?? DateTime.Now;
            cbbGioiTinh.SelectedItem = _dto.GioiTinh;
        }

        private void load_data_cbbGioTinh()
        {
            cbbGioiTinh.Items.Clear();
            cbbGioiTinh.Items.Add("Nam");
            cbbGioiTinh.Items.Add("Nữ");
            cbbGioiTinh.SelectedIndex = 0;
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            controls(true);
        }

        private void controls(bool value)
        {
            txtHoTen.Enabled = value;
            txtSDT.Enabled = value;
            txtGmail.Enabled = value;
            txtDiaChi.Enabled = value;
            cbbGioiTinh.Enabled = value;
            btnLuuThongTin.Visible = value;
        }

        private void btnResetPassWord_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại yêu cầu nhập mật khẩu cũ
            string oldPassword = PromptForPassword();

            if (string.IsNullOrEmpty(oldPassword))
            {
                MessageBox.Show("Bạn chưa nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var (userName, password, isLoggedIn) = LoadLoginStateFromRegistry();
            // Kiểm tra mật khẩu cũ
            if (isLoggedIn)
            {

                ResetPassword formNew = new ResetPassword(new NguoiDungDTO { MaNguoiDung = AccountInfo.MaNguoiDung, TenDangNhap = AccountInfo.TenDangNhap});
                formNew.ShowDialog();
            }
            else
            {
                MessageBox.Show("Mật khẩu cũ không chính xác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Hàm hiển thị hộp thoại nhập mật khẩu cũ
        private string PromptForPassword()
        {
            string inputPassword = Microsoft.VisualBasic.Interaction.InputBox(
                "Vui lòng nhập mật khẩu cũ để xác nhận:",
                "Xác nhận mật khẩu",
                "",
                -1, -1
            );
            return inputPassword;
        }

        private (string UserName, string PassWord, bool IsLoggedIn) LoadLoginStateFromRegistry()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\MyApp");
            if (key != null)
            {
                string userName = key.GetValue("UserName")?.ToString();
                string passWord = key.GetValue("PassWord")?.ToString();
                bool isLoggedIn = Convert.ToBoolean(key.GetValue("IsLoggedIn"));
                key.Close();
                return (userName, passWord, isLoggedIn);
            }
            return (null, null, false);
        }
        private void btnLuuThongTin_Click(object sender, EventArgs e)
        {
            if (!IsValidEmail(txtGmail.Text))
            {
                MessageBox.Show("Email không hợp lệ. Vui lòng nhập đúng định dạng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra định dạng Số điện thoại
            if (!IsValidPhoneNumber(txtSDT.Text))
            {
                MessageBox.Show("Số điện thoại không hợp lệ. Vui lòng nhập đúng định dạng (10-11 chữ số)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var item = new ThongTinCaNhanDTO
            {
                MaNguoiDung = _dto.MaNguoiDung,
                HoTen = txtHoTen.Text,
                NgaySinh = dtbNgaySinh.Value.Date,
                Email = txtGmail.Text,
                DiaChi = txtDiaChi.Text,
                GioiTinh = cbbGioiTinh.SelectedItem.ToString(),
                SDT = txtSDT.Text
            };
            if (_ttcnBUS.Update(item))
            {
                MessageBox.Show("Cập nhật thông tin cá nhân thành công!", "Thông báo", MessageBoxButtons.OK);
                Profile_Load(sender, e);
            }
            else
            {

                MessageBox.Show("Cập nhật thông tin cá nhân thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Hàm kiểm tra định dạng Số điện thoại
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // Chỉ chấp nhận số điện thoại từ 10 đến 12 chữ số
            if (string.IsNullOrEmpty(phoneNumber))
                return false;

            // Kiểm tra độ dài và ký tự
            return phoneNumber.Length >= 10 && phoneNumber.Length <= 12 &&
                   System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, @"^\d+$");
        }
        private void Profile_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool checkThongTinCaNhan = new ThongTinCaNhanBUS().CheckIfDataExists(AccountInfo.MaNguoiDung);

            if (checkThongTinCaNhan)
            {
                // Hiển thị thông báo
                MessageBox.Show("Bạn phải hoàn thành việc nhập thông tin cá nhân trước khi đóng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Hủy việc đóng form
                e.Cancel = true;
            }
        }
    }
}
