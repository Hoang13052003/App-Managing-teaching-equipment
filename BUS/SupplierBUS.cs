using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data.SqlClient;

namespace BUS
{
    public class SupplierBUS: DatabaseHelper
    {
        SupplierDAL sup = new SupplierDAL();

        public List<SupplierDTO> getAll()
        {
            return sup.getAll();
        }

        public List<SupplierDTO> SearchNhaCungCap(string keyword)
        {
            return sup.SearchNhaCungCap(keyword);
        }

        public bool ThemNCC(SupplierDTO supplierDTO)
        {
            return sup.ThemNCC(supplierDTO);
        }
        public bool SuaNCC(int pMaNCC, string pTenNCC, string pDiaChi, string pSDT)
        {
            return sup.SuaNCC(pMaNCC, pTenNCC, pDiaChi, pSDT);
        }
        public bool XoaNCC(int pMaNCC)
        {
            return sup.XoaNCC(pMaNCC);
        }

        public bool KTKC(int pMaNCC)
        {
            string query = "SELECT COUNT(*) FROM NhaCungCap WHERE MaNCC = @MaNCC";

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaNCC", pMaNCC);
                connection.Open();

                int count = (int)command.ExecuteScalar();
                return count > 0; // Trả về true nếu nhà cung cấp tồn tại, ngược lại false
            }
        }


        //QLThietBiDayHocDataContext qltb = new QLThietBiDayHocDataContext();
        //SupplierDAL sup = new SupplierDAL();
        //public SupplierBUS()
        //{

        //}
        //public List<NhaCungCap> getNCC()
        //{
        //    return sup.getNCC();
        //}

        //public bool KTKC(int pMaNCC)
        //{
        //    NhaCungCap dt = qltb.NhaCungCaps.Where(d => d.MaNCC == pMaNCC).FirstOrDefault();
        //    if (dt != null)
        //        return true;
        //    else return false;
        //}

        //public bool ThemNCC(NhaCungCap pNCC)
        //{
        //    return sup.ThemNCC(pNCC);
        //}

        //public bool XoaNCC(int pMaNCC)
        //{
        //    return sup.XoaNCC(pMaNCC);
        //}

        //public bool SuaNCC(int pMaNCC, string pTenNCC, string pDiaChi, string pSDT)
        //{
        //    return sup.SuaDiemSV(pMaNCC, pTenNCC, pDiaChi, pSDT);
        //}

        //public IQueryable searchNCC(string keyword)
        //{
        //    return sup.searchNCC(keyword);
        //}
    }
}
