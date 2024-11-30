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
        public List<LoaiThietBiDTO> SearchLoaiTB(int pMaNCC)
        {
            return sup.SearchLoaiTB(pMaNCC);
        }
        public List<ThietBiDTO> SearchThietBi(int pMaLoai)
        {
            return sup.SearchThietBi(pMaLoai);
        }

        public List<ThietBiDTO> SearchThietBi_NCC(int pMaNCC)
        {
            return sup.SearchThietBi_NCC(pMaNCC);
        }
        public string tenNCC(int pMaNCC)
        {
            return sup.tenNCC(pMaNCC);
        }
    }
}
