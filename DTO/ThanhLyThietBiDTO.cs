using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ThanhLyThietBiDTO
    {
        public int MaThanhLy { get; set; }
        public string MaNguoiDung { get; set; }
        public DateTime? NgayThanhLy { get; set; }
        public int SoLuong { get; set; }
        public float TongTien { get; set; }
    }

}
