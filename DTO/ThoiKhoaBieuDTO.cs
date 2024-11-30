using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ThoiKhoaBieuDTO
    {
        public int MaTKB { get; set; }
        public string MaNguoiDung { get; set; }
        public int? MaMon { get; set; }
        public int? MaBaiHoc { get; set; }
        public int? MaPhong { get; set; }
        public int? MaLopHoc { get; set; }
        public TimeSpan? GioHoc { get; set; }
        public DateTime? NgayHoc { get; set; }
        public bool TinhTrang { get; set; }
    }

}
