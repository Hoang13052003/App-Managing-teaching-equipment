﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NhapThietBiDTO
    {
        public int MaNhap { get; set; }
        public string MaNguoiDung { get; set; }
        public string HoTen { get; set; }
        public DateTime? NgayNhap { get; set; }
        public int SoLuong { get; set; }
        public decimal TongTien { get; set; }
        public int MaNCC { get; set; }
        public string TenNCC { get; set; }
    }

}
