using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BienBanXuLyBUS
    {
        BienBanXuLyDAL bienBanXuLyDAL = new BienBanXuLyDAL();
        public List<BienBanXuLyDTO> GetAll()
        {
            return bienBanXuLyDAL.GetAll();
        }
        public List<BienBanXuLyDTO> SearchBienBan(string keyword)
        {
            return bienBanXuLyDAL.SearchBienBan(keyword);
        }
        public List<ChiTietBienBanDTO> GetAllChiTietBB()
        {
            return bienBanXuLyDAL.GetAllChiTietBB();
        }
        public List<ChiTietBienBanDTO> SearchChiTietBB(int pMaBB)
        {
            return bienBanXuLyDAL.SearchChiTietBB(pMaBB);
        }
        public bool Insert(BienBanXuLyDTO bienBan, List<ChiTietBienBanDTO> chiTietList)
        {
            return bienBanXuLyDAL.Insert(bienBan, chiTietList);
        }
        public bool Update(BienBanXuLyDTO bienBan, List<ChiTietBienBanDTO> chiTietList)
        {
            return bienBanXuLyDAL.Update(bienBan, chiTietList);
        }
        public string GetImage(int maBB, int maCTTB_NCC)
        {
            return bienBanXuLyDAL.GetImage(maBB, maCTTB_NCC);
        }
    }
}
