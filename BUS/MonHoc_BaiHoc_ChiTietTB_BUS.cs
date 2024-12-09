using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class MonHoc_BaiHoc_ChiTietTB_BUS
    {
        private MonHoc_BaiHoc_ChiTietTB_DAL dal = new MonHoc_BaiHoc_ChiTietTB_DAL();

        public List<MonHoc_BaiHoc_ChiTietTB_DTO> GetAll()
        {
            return dal.GetAll();
        }
        public List<MonHoc_BaiHoc_ThietBi_DTO> GetAllDetails()
        {
            return dal.GetAllDetails();
        }
        public List<MonHoc_BaiHoc_ChiTietTB_DTO> GetAllGetByMaMH_MaBH(int? maMH, int? maBH)
        {
            return dal.GetByMaMH_MaBH(maMH, maBH);
        }
        public List<ThietBiDTO> GetThietBiByMaMH_MaBH(int maMH, int maBH)
        {
            return dal.GetThietBiByMaMH_MaBH(maMH, maBH);
        }

        
        public bool Insert(MonHoc_BaiHoc_ThietBi_DTO dto)
        {
            return dal.Insert(dto);
        }

        public bool Update(MonHoc_BaiHoc_ThietBi_DTO dto)
        {
            return dal.Update(dto);
        }

        public bool Delete(MonHoc_BaiHoc_ThietBi_DTO dto)
        {
            return dal.Delete(dto);
        }

    }
}
