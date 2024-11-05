using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ChiTietMuonThietBiDTO
    {
        public int MaMuon { get; set; }
        public int MaCTTB_NCC { get; set; }
        public DateTime? NgayMuon { get; set; }
        public DateTime? NgayTra { get; set; }
        public bool TrangThai { get; set; } // Đã trả / Chưa trả
    }

}
