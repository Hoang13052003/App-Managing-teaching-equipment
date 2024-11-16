using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ChiTietThietBiDTO
    {
        public int MaCTTB_NCC { get; set; }
        public string TenTB { get; set; }
        public string TenPhong { get; set; }
        public string TinhTrang { get; set; }
        public string TrangThai {  get; set; }
        public DateTime? NgayMua { get; set; }
    }
}
