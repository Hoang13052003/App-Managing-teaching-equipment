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
        public List<MonHoc_BaiHoc_ChiTietTB_DTO> GetAllGetByMaMH_MaBH(int? maMH, int? maBH)
        {
            return dal.GetByMaMH_MaBH(maMH, maBH);
        }
        public List<ThietBiDTO> GetThietBiByMaMH_MaBH(int maMH, int maBH)
        {
            return dal.GetThietBiByMaMH_MaBH(maMH, maBH);
        }

    }
}
