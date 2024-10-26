using DTO;
using BUS;
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
    public partial class ResetPassword : Form
    {
        private NguoiDungDTO _nguoiDung;
        private NguoiDungBUS NDBUS = new NguoiDungBUS();
        public ResetPassword(NguoiDungDTO nguoiDung)
        {
            _nguoiDung = nguoiDung;
            InitializeComponent();
            
        }

        private void swichHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            if (swichHienMatKhau.Checked)
            {
                txtMatKhauMoi.PasswordChar = '\0';
                txtConfimMatKhau.PasswordChar = '\0';
            }
            else
            {
                txtMatKhauMoi.PasswordChar = '*';
                txtConfimMatKhau.PasswordChar = '*';
            }
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            string NewPassword = txtMatKhauMoi.Text.Trim();
            string ConfirmPassword = txtConfimMatKhau.Text.Trim();
            if (NewPassword == ConfirmPassword && !string.IsNullOrEmpty(NewPassword))
            {
                _nguoiDung.MatKhau = NewPassword;
                if (NDBUS.UpdateUser(_nguoiDung))
                {
                    MessageBox.Show("Đặt lại mật khẩu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }

        }
    }
}
