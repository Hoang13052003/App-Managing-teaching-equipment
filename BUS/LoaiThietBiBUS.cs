using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class LoaiThietBiBUS
    {
        private LoaiThietBiDAL ltb = new LoaiThietBiDAL();
        private LoaiThietBi_MonHocDAL lm = new LoaiThietBi_MonHocDAL();
        public List<LoaiThietBiDTO> LayLoaiThietBi()
        {
            return ltb.GetLoaiThietBi();
        }
        public List<LoaiThietBiDTO> Search(string tenTB)
        {
            return ltb.SearchLoaiThietBi(tenTB);
        }
        public bool Add(LoaiThietBiDTO loaithietBi)
        {
            return ltb.ThemLoaiThietBi(loaithietBi);
        }
        public bool UpdateSevice(int pMaTB, string pTenTB)
        {
            return ltb.SuaLoaiThietBi(pMaTB, pTenTB);

        }
        public bool DeleteSevice(int pMaTB)
        {
            return ltb.XoaLoaiThietBi(pMaTB);
        }
        public bool LuuMonHocLoaiTB(int pMaTB, int pmaMH)
        {
            return lm.LuuMonHocLoaiTB(pMaTB, pmaMH);
        }

        public DataTable LayDanhSachMonHocLoaiTB()
        {
            return lm.LayDanhSachMonHocLoaiTB();
        }

        public bool XoaMonHocLoaiTB(int pMaTB, int pmaMH)
        {
            return lm.XoaLTB_MH(pMaTB, pmaMH);
        }
    }
}
