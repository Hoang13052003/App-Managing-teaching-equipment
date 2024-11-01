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
    public class LoaiThietBiDAL : DatabaseHelper
    {
        public List<LoaiThietBiDTO> GetLoaiThietBi()
        {
            List<LoaiThietBiDTO> list = new List<LoaiThietBiDTO>();
            string query = "SELECT * FROM LoaiThietBi";
            DataTable dataTable = GetDataTable(query);

            foreach (DataRow row in dataTable.Rows)
            {
                list.Add(new LoaiThietBiDTO
                {
                    MaLoai = Convert.ToInt32(row["MaLoai"]),
                    TenLoai = row["TenLoai"].ToString()
                });
            }

            return list;
        }

        public List<LoaiThietBiDTO> SearchLoaiThietBi(string keyword)
        {
            List<LoaiThietBiDTO> list = new List<LoaiThietBiDTO>();
            string query = @"SELECT * 
                     FROM LoaiThietBi 
                     WHERE TenLoai LIKE '%' + @keyword + '%'";

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
                        list.Add(new LoaiThietBiDTO
                        {
                            MaLoai = Convert.ToInt32(row["MaLoai"]),
                            TenLoai = row["TenLoai"].ToString()
                        });
                    }
                }
            }

            return list;
        }
        public bool ThemLoaiThietBi(LoaiThietBiDTO loaiThietBiDTO)
        {
            string query = "INSERT INTO LoaiThietBi (TenLoai) VALUES (@TenLoai)";
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TenLoai", loaiThietBiDTO.TenLoai);

                connection.Open();
                return command.ExecuteNonQuery() > 0; // Trả về true nếu thêm thành công
            }
        }
        public bool XoaLoaiThietBi(int maLoai)
        {
            string query = "DELETE FROM LoaiThietBi WHERE MaLoai = @MaLoai";
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaLoai", maLoai);

                connection.Open();

                try
                {
                    // Thực hiện xóa
                    return command.ExecuteNonQuery() > 0;
                }
                catch (SqlException ex)
                {
                    // Kiểm tra lỗi khóa ngoại
                    if (ex.Number == 547) // 547 là mã lỗi cho vi phạm khóa ngoại
                    {
                        Console.WriteLine("Không thể xóa loại thiết bị vì có bản ghi khác liên quan.");
                    }
                    else
                    {
                        // Xử lý các lỗi khác
                        Console.WriteLine("Lỗi xảy ra: " + ex.Message);
                    }

                    return false;
                }
            }
        }

        public bool SuaLoaiThietBi(int maLoai, string tenLoai)
        {
            string query = "UPDATE LoaiThietBi SET TenLoai = @TenLoai WHERE MaLoai = @MaLoai";
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaLoai", maLoai);
                command.Parameters.AddWithValue("@TenLoai", tenLoai);

                connection.Open();
                return command.ExecuteNonQuery() > 0; // Trả về true nếu cập nhật thành công
            }
        }

    }
}
