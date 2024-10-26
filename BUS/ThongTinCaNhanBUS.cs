using System.Collections.Generic;
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

        public bool Update(ThongTinCaNhanDTO thongTinCaNhan)
        {
            return thongTinCaNhanDAL.Update(thongTinCaNhan);
        }

        public bool Delete(string maNguoiDung)
        {
            return thongTinCaNhanDAL.Delete(maNguoiDung);
        }
    }
}
