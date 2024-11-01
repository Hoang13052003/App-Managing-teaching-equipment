using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PhanHoiGiaoVienDTO
    {
        public int MaPH { get; set; }
        public string MaNguoiDung { get; set; }
        public string NoiDung { get; set; }
        public DateTime? NgayPhanHoi { get; set; }
    }
}
