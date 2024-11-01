using DTO;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class ThietBiBUS
    {
        private ThietBiDAL tb = new ThietBiDAL();

        // Lấy tất cả người dùng
        public List<ThietBiDTO> GetAll()
        {
            return tb.GetAll();
        }

        public List<ThietBiDTO> Search(string tenTB)
        {
            return tb.SearchThietBi(tenTB);
        }
        public bool Add(ThietBiDTO thietBi)
        {
            return tb.ThemThietBi(thietBi);
        }
        public bool UpdateSevice(int pMaTB, string pTenTB, int pMaLoai, DateTime pNSX, int pSoLuong)
        {
            return tb.SuaThietBi(pMaTB, pTenTB, pMaLoai, pNSX, pSoLuong);

        }
        public bool DeleteSevice(int pMaTB)
        {
            return tb.XoaThietBi(pMaTB);
        }
    }
}
