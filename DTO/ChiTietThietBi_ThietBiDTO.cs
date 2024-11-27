using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ChiTietThietBi_ThietBiDTO
    {
        public int MaCTTB { get; set; }
        public int MaTB { get; set; }
        public string TenTB { get; set; }
        public string TinhTrang { get; set; }
        public int TrangThai { get; set; }
        public DateTime? NgayMua { get; set; }
    }
}
