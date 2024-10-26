using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PhanQuyenDTO
    {
        public string MaNhom { get; set; } // Khóa ngoại từ NhomNguoiDung
        public string MaMH { get; set; } // Khóa ngoại từ ManHinh
        public bool CoQuyen { get; set; } // True = Có quyền, False = Không có quyền
    }
}
