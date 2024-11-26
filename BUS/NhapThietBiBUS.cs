using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class NhapThietBiBUS
    {
        NhapThietBiDAL n = new NhapThietBiDAL();
        public NhapThietBiBUS() { }
        public List<NhapThietBiDTO> GetAll()
        {
            return n.GetAll();
        }
        public List<ChiTietNhapDTO> GetAll(int maNhap)
        {
            return n.GetAll(maNhap);
        }
    }
}
