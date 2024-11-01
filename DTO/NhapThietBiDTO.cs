using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NhapThietBiDTO
    {
        public int MaNhap { get; set; }
        public string MaNguoiDung { get; set; }
        public DateTime? NgayNhap { get; set; }
        public int SoLuong { get; set; }
        public float TongTien { get; set; }
    }

}
