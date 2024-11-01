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
        public List<LoaiThietBiDTO> getAllLoaiTB()
        {
            return y.getAllLoaiTB();
        }
        public List<ThietBiDTO> getAllThietBi()
        {
            return y.getAllThietBi();
        }
        public List<ThietBiDTO> SearchThietBi(int pMaLoai)
        {
            return y.SearchThietBi(pMaLoai);
        }
        public List<ChiTietThietBiDTO> SearchChiTietThietBi(int pMaTB)
        {
            return y.SearchChiTietThietBi(pMaTB);
        }
        public List<ChiTietThietBiDTO> getAllChiTietThietBi()
        {
            return y.getAllChiTietThietBi();
        }
        public List<ChiTietThietBiDTO> SearchKeyChiTietThietBi(string keyword)
        {
            return y.SearchKeyChiTietThietBi(keyword);
        }
        public bool TaoYeuCauThietBi(YeuCauThietBiDTO yeuCauThietBiDTO, List<ChiTietYeuCauThietBiDTO> chiTietList)
        {
            return y.TaoYeuCauThietBi(yeuCauThietBiDTO, chiTietList);
        }
    }
}
