using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class YeuCauThietBiBUS
    {
        YeuCauThietBiDAL y = new YeuCauThietBiDAL();
        public List<pLoaiThietBiDTO> getAllLoaiTB()
        {
            return y.getAllLoaiTB();
        }
        public List<pThietBiDTO> getAllThietBi()
        {
            return y.getAllThietBi();
        }
        public List<pThietBiDTO> SearchThietBi(int pMaLoai)
        {
            return y.SearchThietBi(pMaLoai);
        }
        public List<pChiTietThietBiDTO> SearchChiTietThietBi(int pMaTB)
        {
            return y.SearchChiTietThietBi(pMaTB);
        }
        public List<pChiTietThietBiDTO> getAllChiTietThietBi()
        {
            return y.getAllChiTietThietBi();
        }
        public List<pChiTietThietBiDTO> SearchKeyChiTietThietBi(string keyword)
        {
            return y.SearchKeyChiTietThietBi(keyword);
        }
        public bool TaoYeuCauThietBi(YeuCauThietBiDTO yeuCauThietBiDTO, List<ChiTietYeuCauThietBiDTO> chiTietList)
        {
            return y.TaoYeuCauThietBi(yeuCauThietBiDTO, chiTietList);
        }
    }
}
