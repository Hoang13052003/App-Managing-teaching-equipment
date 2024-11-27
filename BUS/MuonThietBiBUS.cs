using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class MuonThietBiBUS
    {
        private MuonThietBiDAL mtbDAL = new MuonThietBiDAL();
        public List<MuonThietBiDTO> GetAll()
        {
            return mtbDAL.GetAll();
        }
        public MuonThietBiDTO GetByID(int maMuon)
        {
            return mtbDAL.GetByID(maMuon);
        }
        public MuonThietBiDTO GetByMaND_MaTKB(string maND, int maTKB)
        {
            return mtbDAL.GetByMaND_MaTKB(maND, maTKB);
        }
        public bool Insert(MuonThietBiDTO thietBiMuon)
        {
            return mtbDAL.Insert(thietBiMuon);
        }

        public bool Update(MuonThietBiDTO thietBiMuon)
        {
            return mtbDAL.Update(thietBiMuon);
        }
        public bool Update_TrangThai(int maMuon, bool thietBiMuon)
        {
            return mtbDAL.Update_TrangThai(maMuon ,thietBiMuon);
        }
        public bool Update_TinhTrang(int maMuon, string tinhTrang)
        {
            return mtbDAL.Update_TinhTrang(maMuon , tinhTrang);
        }
        public bool Delete(int maMuon)
        {
            return mtbDAL.Delete(maMuon);
        }
    }
}
