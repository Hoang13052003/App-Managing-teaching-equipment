using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BienBanXuLyDTO
    {
        public int MaBB { get; set; }
        public string TenNguoiLamHong { get; set; }
        public string VaiTro { get; set; }
        public DateTime ThoiGianLamHong { get; set; }
        public DateTime? ThoiGianXuLy { get; set; }
        public string MoTaChiTiet { get; set; }
        public double ChiPhiSuaChua { get; set; }
        public int TinhTrang { get; set; } // Đã xử lý / Chưa xử lý
    }

}
