using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class ChiTietThietBi_ThietBiBUS
    {
        private ChiTietThietBi_ThietBiDAL ChiTietThietBi_ThietBiDAL = new ChiTietThietBi_ThietBiDAL();
        public List<ChiTietThietBi_ThietBiDTO> GetAll()
        {
            return ChiTietThietBi_ThietBiDAL.GetAll();
        }
        public ChiTietThietBi_ThietBiDTO GetChiTietThietBiByMaCTTB(int maCTTB)
        {
            return ChiTietThietBi_ThietBiDAL.GetChiTietThietBiByMaCTTB(maCTTB);
        }
        public List<ChiTietThietBi_ThietBiDTO> GetAllChiTietThietBiByMaTB(int maTB)
        {
            return ChiTietThietBi_ThietBiDAL.GetAllChiTietThietBiByMaTB(maTB);
        }

        public bool Update_TrangThai(int? maCTTB, int trangThai)
        {
            return ChiTietThietBi_ThietBiDAL.Update_TrangThai(maCTTB, trangThai);
        }

    }
}
