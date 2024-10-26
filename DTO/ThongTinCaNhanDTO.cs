using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ThongTinCaNhanDTO
    {
        public string MaNguoiDung { get; set; }  // Mã người dùng
        public string HoTen { get; set; }        // Họ và tên
        public string GioiTinh { get; set; }     // Giới tính
        public DateTime? NgaySinh { get; set; }  // Ngày sinh
        public string Email { get; set; }        // Địa chỉ email
        public string SDT { get; set; }          // Số điện thoại
        public string DiaChi { get; set; }       // Địa chỉ
    }
}
