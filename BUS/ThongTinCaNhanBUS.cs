using System.Collections.Generic;
using System.Data.SqlClient;
using DTO;

namespace BUS
{
    public class ThongTinCaNhanBUS
    {
        private ThongTinCaNhanDAL thongTinCaNhanDAL = new ThongTinCaNhanDAL();

        public List<ThongTinCaNhanDTO> GetAll()
        {
            return thongTinCaNhanDAL.GetAll();
        }

        public ThongTinCaNhanDTO GetByMaNguoiDung(string maNguoiDung)
        {
            return thongTinCaNhanDAL.GetByMaNguoiDung(maNguoiDung);
        }

        public bool Add(ThongTinCaNhanDTO thongTinCaNhan)
        {
            return thongTinCaNhanDAL.Insert(thongTinCaNhan);
        }
        public bool InsertNewNull(string maNguoiDung)
        {
            return thongTinCaNhanDAL.InsertNewNull(maNguoiDung);
        }
        public bool Update(ThongTinCaNhanDTO thongTinCaNhan)
        {
            return thongTinCaNhanDAL.Update(thongTinCaNhan);
        }

        public bool Delete(string maNguoiDung)
        {
            return thongTinCaNhanDAL.Delete(maNguoiDung);
        }
        public bool CheckIfCodeExists(string maNguoiDung)
        {
            return thongTinCaNhanDAL.CheckIfCodeExists(maNguoiDung);
        }
        public bool CheckIfDataExists(string maNguoiDung)
        {
            return thongTinCaNhanDAL.CheckIfDataExists(maNguoiDung);
        }
    }
}
