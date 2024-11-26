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
        public bool TaoYeuCauThietBi2(YeuCauThietBiDTO yeuCauThietBiDTO, List<ChiTietYeuCauThietBiDTO> chiTietList)
        {
            return y.TaoYeuCauThietBi2(yeuCauThietBiDTO, chiTietList);
        }
        public List<ChiTietThietBiDTO> SearchChiTietThietBi2(int pMaLoaiTB)
        {
            return y.SearchChiTietThietBi2(pMaLoaiTB);
        }
        public List<YeuCauThietBiDTO> getAllYeuCauThietBi()
        {
            return y.getAllYeuCauThietBi();
        }
        public List<ChiTietYeuCauThietBiDTO> getAllChiTietYeuCauSuaThietBi()
        {
            return y.getAllChiTietYeuCauSuaThietBi();
        }
        public List<ChiTietYeuCauThietBiDTO> searchChiTietYeuCauSuaThietBi(int pMaYC)
        {
            return y.searchChiTietYeuCauSuaThietBi(pMaYC);
        }
        public bool UpdataTrangThaiCTYCTB(int pMaYC, int pMaCTTB_NCC, int pTrangThai, string pKetQua, float pChiPhi)
        {
            return y.UpdataTrangThaiCTYCTB(pMaYC, pMaCTTB_NCC, pTrangThai, pKetQua, pChiPhi);
        }
        public List<ChiTietThietBi_TKBDTO> getAllChiTietThietBi_TKB(int pMaTKB)
        {
            return y.getAllChiTietThietBi_TKB(pMaTKB);
        }
        public DateTime ngayHoc_TKB(int pMaTKB)
        {
            return y.ngayHoc_TKB(pMaTKB);
        }
        public TimeSpan gioHoc_TKB(int pMaTKB)
        {
            return y.gioHoc_TKB(pMaTKB);
        }
    }
}
