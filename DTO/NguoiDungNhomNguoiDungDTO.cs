using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NguoiDungNhomNguoiDungDTO
    {
        public string MaNguoiDung { get; set; } // Khóa ngoại từ NguoiDung
        public string MaNhom { get; set; } // Khóa ngoại từ NhomNguoiDung
        public string GhiChu { get; set; }
    }
}
