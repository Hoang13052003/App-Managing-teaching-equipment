using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BaoDuongBUS
    {
        BaoDuongDAL b = new BaoDuongDAL();
        public List<BaoDuongDTO> GetAll()
        {
            return b.GetAll();
        }
        public bool Delete(int maBD)
        {
            return b.Delete(maBD);
        }
        public bool Update(BaoDuongDTO baoDuong)
        {
            return b.Update(baoDuong);
        }
    }
}
