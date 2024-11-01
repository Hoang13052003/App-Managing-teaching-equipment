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
    public class ThietBiDAL : DatabaseHelper
    {
        public ThietBiDAL()
        {
        }
        public List<ThietBiDTO> GetAll()
        {
            List<ThietBiDTO> list = new List<ThietBiDTO>();
            string query = "SELECT * FROM ThietBi";
            DataTable dataTable = GetDataTable(query);

            foreach (DataRow row in dataTable.Rows)
            {
                list.Add(new ThietBiDTO
                {
                    MaTB = Convert.ToInt32(row["MaTB"]),
                    TenTB = row["TenTB"].ToString(),
                    MaLoai = Convert.ToInt32(row["MaLoai"]),
                    NSX = row["NSX"].ToString(),
                    SoLuong = Convert.ToInt32(row["SoLuong"])
                });
            }
            return list;
        }
        // Hàm tìm kiếm thiết bị theo tên
        public List<ThietBiDTO> SearchThietBi(string keyword)
        {
            List<ThietBiDTO> list = new List<ThietBiDTO>();
            string query = @"SELECT * 
                     FROM ThietBi 
                     WHERE TenTB LIKE '%' + @keyword + '%' OR 
                           CAST(MaLoai AS NVARCHAR) LIKE '%' + @keyword + '%' OR 
                           CAST(NSX AS NVARCHAR) LIKE '%' + @keyword + '%' OR 
                           CAST(SoLuong AS NVARCHAR) LIKE '%' + @keyword + '%'";

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
                        list.Add(new ThietBiDTO
                        {
                            MaTB = Convert.ToInt32(row["MaTB"]),
                            TenTB = row["TenTB"].ToString(),
                            MaLoai = Convert.ToInt32(row["MaLoai"]),
                            NSX = row["NSX"].ToString(),
                            SoLuong = Convert.ToInt32(row["SoLuong"])
                        });
                    }
                }
            }

            return list;
        }

        public bool ThemThietBi(ThietBiDTO thietBiDTO)
        {
            string query = "INSERT INTO ThietBi (TenTB, MaLoai, NSX, SoLuong) VALUES (@TenTB, @MaLoai, @NSX, @SoLuong)";
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TenTB", thietBiDTO.TenTB);
                command.Parameters.AddWithValue("@MaLoai", thietBiDTO.MaLoai);
                command.Parameters.AddWithValue("@NSX", thietBiDTO.NSX);
                command.Parameters.AddWithValue("@SoLuong", thietBiDTO.SoLuong);

                connection.Open();
                return command.ExecuteNonQuery() > 0; // Trả về true nếu có ít nhất 1 dòng bị ảnh hưởng
            }
        }

        public bool SuaThietBi(int pMaTB, string pTenTB, int pMaLoai, DateTime pNSX, int pSoLuong)
        {
            string query = "UPDATE ThietBi SET TenTB = @TenTB, MaLoai = @MaLoai, NSX = @NSX, SoLuong = @SoLuong WHERE MaTB = @MaTB";
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaTB", pMaTB);
                command.Parameters.AddWithValue("@TenTB", pTenTB);
                command.Parameters.AddWithValue("@MaLoai", pMaLoai);
                command.Parameters.AddWithValue("@NSX", pNSX);
                command.Parameters.AddWithValue("@SoLuong", pSoLuong);

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool XoaThietBi(int pMaTB)
        {
            string query = "DELETE FROM ThietBi WHERE MaTB = @MaTB";
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaTB", pMaTB);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

    }
}