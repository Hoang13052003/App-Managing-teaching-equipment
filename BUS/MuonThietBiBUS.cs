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
        private LoaiThietBiDAL ltb = new LoaiThietBiDAL();
        private MuonThietBiDAL mtb = new MuonThietBiDAL();
        public List<MuonThietBiDTO> GetAll()
        {
            return mtb.GetAll();
        }

        public ChiTietMuonThietBiDTO GetCTPMByID(int pmaMuon)
        {
            return mtb.GetCTPMByID(pmaMuon);
        }

        public DataTable GetMuonThietBiWithDetails()
        {
            return mtb.GetMuonThietBiAndDetails();
        }

        public DataTable GetThietBiDetails(int maCTTB_NCC)
        {
            return mtb.GetThietBiDetailsByMaCTTB(maCTTB_NCC);
        }
    }
}
