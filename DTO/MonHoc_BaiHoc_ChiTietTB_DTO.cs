using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class MonHoc_BaiHoc_ChiTietTB_DTO
    {
        public int? MaMH { get; set; }
        public int? MaBH { get; set; }
        public int? MaCTTB { get; set; }
        public int? MaTB { get; set; }
        public string TenTB { get; set; }
        public int MaLoai { get; set; }
    }

    public class MonHoc_BaiHoc_ThietBi_DTO
    {
        public int? MaTB { get; set; }
        public string TenTB { get; set; }
        public int SoLuong { get; set; }
    }
}
