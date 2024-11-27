using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class MuonThietBiDTO
    {
        public int MaMuon { get; set; }
        public string MaNguoiDung { get; set; }  // Thay đổi kiểu dữ liệu thành string
        public int MaTKB { get; set; }
        public DateTime? NgayMuon { get; set; }
        public DateTime? NgayTra { get; set; }
        public string TinhTrangTraTB { get; set; }
        public bool TrangThai { get; set; }
        public string GhiChuTraThietBi { get; set; }

    }
}
