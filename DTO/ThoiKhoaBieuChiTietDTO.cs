using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ThoiKhoaBieuChiTietDTO
    {
        public int MaTKB { get; set; }
        public string MaNguoiDung { get; set; }
        public string TenMonHoc { get; set; }
        public string TenBaiHoc { get; set; }
        public string TenPhong { get; set; }
        public string TenLop { get; set; }
        public TimeSpan? GioHoc { get; set; }
        public DateTime? NgayHoc { get; set; }
        public bool TinhTrang {  get; set; }
    }
    public class ThoiKhoaBieuChiTiet_NguoiDungDTO
    {
        public int MaTKB { get; set; }
        public string MaNguoiDung { get; set; }
        public string HoTen { get; set; }
        public int? MaMon { get; set; }
        public string TenMonHoc { get; set; }
        public int? MaBaiHoc { get; set; }
        public string TenBaiHoc { get; set; }
        public int? MaPhong { get; set; }
        public string TenPhong { get; set; }
        public int? MaLopHoc { get; set; }
        public string TenLop { get; set; }
        public TimeSpan? GioHoc { get; set; }
        public DateTime? NgayHoc { get; set; }
        public bool TinhTrang { get; set; }
    }
}
