using DTO;
using DAL;
using System;
using System.Runtime.Remoting.Messaging;

namespace BUS
{
    public class LoginBUS
    {
        private LoginDAL loginDAL = new LoginDAL();

        public NguoiDungDTO Login(string tenDangNhap, string matKhau)
        {
            // Kiểm tra thông tin đầu vào
            if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(matKhau))
            {
                throw new ArgumentException("Tên đăng nhập và mật khẩu không được để trống.");
            }

            // Gọi phương thức Login từ LoginDAL
            return loginDAL.Login(tenDangNhap, matKhau);
        }

        public bool Register(NguoiDungDTO nguoiDung)
        {
            // Kiểm tra thông tin đầu vào
            if (nguoiDung == null)
            {
                throw new ArgumentNullException(nameof(nguoiDung), "Thông tin người dùng không hợp lệ.");
            }

            if (string.IsNullOrWhiteSpace(nguoiDung.TenDangNhap) || string.IsNullOrWhiteSpace(nguoiDung.MatKhau))
            {
                throw new ArgumentException("Tên đăng nhập và mật khẩu không được để trống.");
            }

            // Gọi phương thức Register từ LoginDAL
            return loginDAL.Register(nguoiDung);
        }
        public NguoiDungDTO RecoverPassword(string tenDangNhap)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap))
            {
                throw new ArgumentException("Tên đăng nhập không được để trống.");
            }

            return loginDAL.RecoverPassword(tenDangNhap);
        }

        public string CheckRoles(String maNguoiDung)
        {
            return loginDAL.CheckRoles(maNguoiDung);
        }
    }
}
