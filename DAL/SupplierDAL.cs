using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SupplierDAL: DatabaseHelper
    {
        public List<SupplierDTO> getAll()
        {
            List<SupplierDTO> list = new List<SupplierDTO>();
            string query = "SELECT * FROM NhaCungCap";
            DataTable dataTable = GetDataTable(query);

            foreach (DataRow row in dataTable.Rows)
            {
                list.Add(new SupplierDTO
                {
                    MaNCC = Convert.ToInt32(row["MaNCC"]),
                    TenNCC = row["TenNCC"].ToString(),
                    DiaChi = row["DiaChi"].ToString(),
                    SDT = row["SDT"].ToString()
                });
            }
            return list;
        }

        public List<SupplierDTO> SearchNhaCungCap(string keyword)
        {
            List<SupplierDTO> list = new List<SupplierDTO>();
            string query = @"SELECT * 
                     FROM NhaCungCap 
                     WHERE TenNCC LIKE '%' + @keyword + '%' OR 
                           CAST(MaNCC AS NVARCHAR) LIKE '%' + @keyword + '%' OR 
                           DiaChi LIKE '%' + @keyword + '%' OR 
                           SDT LIKE '%' + @keyword + '%'";

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@keyword", keyword);
                connection.Open();

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        list.Add(new SupplierDTO
                        {
                            MaNCC = Convert.ToInt32(row["MaNCC"]),
                            TenNCC = row["TenNCC"].ToString(),
                            DiaChi = row["DiaChi"].ToString(),
                            SDT = row["SDT"].ToString()
                        });
                    }
                }
            }

            return list;
        }

        public bool ThemNCC(SupplierDTO supplierDTO)
        {
            string query = "INSERT INTO NhaCungCap (TenNCC, DiaChi, SDT) VALUES (@TenNCC, @DiaChi, @SDT)";
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TenNCC", supplierDTO.TenNCC);
                command.Parameters.AddWithValue("@DiaChi", supplierDTO.DiaChi);
                command.Parameters.AddWithValue("@SDT", supplierDTO.SDT);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool SuaNCC(int pMaNCC, string pTenNCC, string pDiaChi, string pSDT)
        {   
            string query = "UPDATE NhaCungCap SET TenNCC = @TenNCC, DiaChi = @DiaChi, SDT = @SDT WHERE MaNCC = @MaNCC";
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaNCC", pMaNCC);
                command.Parameters.AddWithValue("@TenNCC", pTenNCC);
                command.Parameters.AddWithValue("@DiaChi", pDiaChi);
                command.Parameters.AddWithValue("@SDT", pSDT);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }


        public bool XoaNCC(int pMaNCC)
        {
            string query = "DELETE FROM NhaCungCap WHERE MaNCC = @MaNCC";
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaNCC", pMaNCC);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        //QLThietBiDayHocDataContext qltb = new QLThietBiDayHocDataContext();
        //public SupplierDAL()
        //{

        //}

        //public List<NhaCungCap> getNCC()
        //{
        //    return qltb.NhaCungCaps.Select(n => n).ToList<NhaCungCap>();
        //}

        //public bool ThemNCC(NhaCungCap pNCC)
        //{
        //    try
        //    {
        //        qltb.NhaCungCaps.InsertOnSubmit(pNCC);
        //        qltb.SubmitChanges();
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        //public bool XoaNCC(int pMaNCC)
        //{
        //    NhaCungCap dt = qltb.NhaCungCaps.Where(d => d.MaNCC == pMaNCC).FirstOrDefault();
        //    if (dt != null)
        //    {
        //        qltb.NhaCungCaps.DeleteOnSubmit(dt);
        //        qltb.SubmitChanges();
        //        return true;
        //    }
        //    else return false;
        //}

        //public bool SuaNCC(int pMaNCC, string pTenNCC, string pDiaChi, string pSDT)
        //{
        //    NhaCungCap dt = qltb.NhaCungCaps.Where(d => d.MaNCC == pMaNCC).FirstOrDefault();
        //    if (dt != null)
        //    {
        //        dt.TenNCC = pTenNCC;
        //        dt.DiaChi = pDiaChi;
        //        dt.SDT = pSDT;
        //        qltb.SubmitChanges();
        //        return true;
        //    }
        //    else return false;
        //}

        //public IQueryable searchNCC(string keyword)
        //{
        //    var ncc = from n in qltb.NhaCungCaps
        //              where n.TenNCC.Contains(keyword) ||
        //                    n.MaNCC.ToString().Contains(keyword) ||
        //                    n.DiaChi.Contains(keyword) ||
        //                    n.SDT.Contains(keyword)
        //              select n;
        //    return ncc;
        //}
    }
}
