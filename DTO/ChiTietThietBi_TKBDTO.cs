using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ChiTietThietBi_TKBDTO
    {
        public int MaCTTB { get; set; }
        public string TenTB { get; set; }
        public string TenPhong { get; set; }
        public string TinhTrang { get; set; }
        public string TrangThai { get; set; }
        public int MaMuon {  get; set; }
        public DateTime? NgayHoc { get; set; }
        public TimeSpan? GioHoc { get; set; }
    }
}
