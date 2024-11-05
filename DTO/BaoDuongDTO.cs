using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BaoDuongDTO
    {
        public int MaBD { get; set; }
        public int MaCTTB_NCC { get; set; }
        public string TenTB { get; set; }
        public DateTime? NgayBD { get; set; }
        public string KetQua { get; set; }
        public float ChiPhi { get; set; }
    }

}
