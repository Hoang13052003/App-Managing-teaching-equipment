using BUS;
using DTO;
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
    public partial class Login : Form
    {
        private LoginBUS loginBUS = new LoginBUS();
        public Login()
        {
            InitializeComponent();
        }
        private void ResetFieldsAndFocus()
        {
            // Đặt nội dung của các trường về rỗng
            txtMatKhauDangKy.Text = string.Empty;
            txtMatKhauDangNhap.Text = string.Empty;
            txtTenDangKy.Text = string.Empty;
            txtTenDangNhap.Text = string.Empty;
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void btnChuyenQuaDangNhap_Click(object sender, EventArgs e)
        {
            panelDangNhap.Enabled = true;
            guna2Transition1.ShowSync(panelDangNhap);
        }

        private void btnChuyenQuaDangKy_Click(object sender, EventArgs e)
        {
            panelDangNhap.Enabled = false;
            guna2Transition1.HideSync(panelDangNhap);
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTenDangNhap.Text;
            string matKhau = txtMatKhauDangNhap.Text;
            try
            {
                NguoiDungDTO nguoiDung = loginBUS.Login(tenDangNhap, matKhau);

                if (nguoiDung != null)
                {
                    MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Chuyển đến giao diện chính hoặc các thao tác khác

                    switch (loginBUS.CheckRoles(nguoiDung.MaNguoiDung))
                    {
                        case "Admin":
                            {
                                AccountInfo.SetAccountInfo(nguoiDung);
                                FormTask.OpenForm<Dashboard_Admin>(this);
                            }
                            break;
                        case "Teacher":
                            {
                                AccountInfo.SetAccountInfo(nguoiDung);
                                FormTask.OpenForm<Dashboard_User>(this);
                            }
                            break;
                        case "Staff": { 
                            
                            } break;
                        default: MessageBox.Show("Người dùng chưa có quyền!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);break;
                    }
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            // Tạo một đối tượng NguoiDungDTO mới để lưu thông tin đăng ký
            NguoiDungDTO nguoiDung = new NguoiDungDTO
            {
                TenDangNhap = txtTenDangKy.Text.Trim(), // Giả sử bạn có một TextBox để nhập tên đăng nhập
                MatKhau = txtMatKhauDangKy.Text.Trim(), // Giả sử bạn có một TextBox để nhập mật khẩu
                TrangThai = true // Hoặc một giá trị khác, tùy thuộc vào logic của bạn
            };

            try
            {
                // Gọi phương thức Register từ lớp BUS hoặc DAL
                if (loginBUS.Register(nguoiDung)) // Giả sử bạn có một lớp BUS để xử lý logic
                {
                    MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Bạn có thể làm gì đó sau khi đăng ký thành công, như đóng form hoặc mở form khác
                    ResetFieldsAndFocus();
                    panelDangNhap.Enabled = true;
                    guna2Transition1.ShowSync(panelDangNhap);
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại. Vui lòng chọn tên khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void swichHienMatKhauDangKy_CheckedChanged(object sender, EventArgs e)
        {
            if (swichHienMatKhauDangKy.Checked)
            {
                txtMatKhauDangKy.PasswordChar = '\0'; // Hiện mật khẩu
            }
            else
            {
                txtMatKhauDangKy.PasswordChar = '*'; // Ẩn mật khẩu
            }
        }

        private void swichHienMatKhauDangNhap_CheckedChanged(object sender, EventArgs e)
        {
            if (swichHienMatKhauDangNhap.Checked)
            {
                txtMatKhauDangNhap.PasswordChar = '\0'; // Hiện mật khẩu
            }
            else
            {
                txtMatKhauDangNhap.PasswordChar = '*'; // Ẩn mật khẩu
            }
        }

        private void lbQuenMatKhau_Click(object sender, EventArgs e)
        {
            try
            {
                string tenDangNhap = txtTenDangNhap.Text;
                NguoiDungDTO nguoiDung = loginBUS.RecoverPassword(tenDangNhap);

                if (nguoiDung != null)
                {
                    FormTask.OpenForm<ResetPassword>(nguoiDung);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}
