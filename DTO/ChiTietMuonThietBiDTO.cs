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
        public int? MaCTTB { get; set; }
        public int? MaTB { get; set; }
        public string TenTB { get; set; }
        public bool TrangThai { get; set; }
    }
    public class ChiTietMuonThietBiDTO_ThietBi
    {
        public int? MaTB { get; set; }
        public string TenTB { get; set; }
        public int SoLuong { get; set; }
    }
}
