using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NguoiDungDTO
    {
        public string MaNguoiDung { get; set; } // Khóa chính
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; } // Đã mã hóa
        public bool TrangThai { get; set; } // True = Hoạt động, False = Không hoạt động
    }
}
