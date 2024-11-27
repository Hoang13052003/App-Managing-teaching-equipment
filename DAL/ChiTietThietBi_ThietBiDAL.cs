using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ChiTietThietBi_ThietBiDAL : DatabaseHelper
    {
        public List<ChiTietThietBi_ThietBiDTO> GetAll()
        {
            List<ChiTietThietBi_ThietBiDTO> list = new List<ChiTietThietBi_ThietBiDTO>();
            string query = "SELECT * FROM ChiTietThietBi cttb INNER JOIN ThietBi tb ON cttb.MaTB = tb.MaTB ";
            DataTable dataTable = GetDataTable(query);
            foreach (DataRow row in dataTable.Rows)
            {
                list.Add(new ChiTietThietBi_ThietBiDTO
                {
                    MaCTTB = Convert.ToInt32(row["MaCTTB"]),
                    MaTB = Convert.ToInt32(row["MaTB"]),
                    TenTB = row["TenTB"].ToString(),
                    TinhTrang = row["TinhTrang"].ToString(),
                    TrangThai = Convert.ToInt32(row["TrangThai"]),
                    NgayMua = Convert.ToDateTime(row["NgayMua"]),
                });
            }
            return list;
        }

        public bool Update_TrangThai(int? maCTTB, int trangThai)
        {
            string query = @"UPDATE ChiTietThietBi 
                     SET TrangThai = @TrangThai 
                     WHERE MaCTTB = @MaCTTB";

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaCTTB", maCTTB); // Điều kiện cập nhật
                command.Parameters.AddWithValue("@TrangThai", trangThai);

                connection.Open();
                return command.ExecuteNonQuery() > 0; // Trả về true nếu cập nhật thành công
            }
        }


        // Lấy danh sách bản ghi theo MaTB
        public ChiTietThietBi_ThietBiDTO GetChiTietThietBiByMaCTTB(int maCTTB)
        {
            string query = "SELECT cttb.MaCTTB, cttb.MaTB, tb.TenTB, cttb.TinhTrang, cttb.TrangThai, cttb.NgayMua " +
                           "FROM ChiTietThietBi cttb INNER JOIN ThietBi tb ON cttb.MaTB = tb.MaTB " +
                           "WHERE cttb.MaCTTB = @MaCTTB"; // Sử dụng MaCTTB để lọc chính xác

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaCTTB", maCTTB)
            };

            DataTable dataTable = GetDataTableQuery(query, parameters);

            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                return new ChiTietThietBi_ThietBiDTO
                {
                    MaCTTB = Convert.ToInt32(row["MaCTTB"]),
                    MaTB = Convert.ToInt32(row["MaTB"]),
                    TinhTrang = row["TinhTrang"].ToString(),
                    TrangThai = Convert.ToInt32(row["TrangThai"]),
                    NgayMua = Convert.ToDateTime(row["NgayMua"])
                };
            }
            else
            {
                return null; // Hoặc throw exception tùy theo yêu cầu
            }
        }

        public List<ChiTietThietBi_ThietBiDTO> GetAllChiTietThietBiByMaTB(int maTB)
        {
            List<ChiTietThietBi_ThietBiDTO> list = new List<ChiTietThietBi_ThietBiDTO>();
            string query = "SELECT * FROM ChiTietThietBi cttb INNER JOIN ThietBi tb on cttb.MaTB = tb.MaTB WHERE cttb.MaTB = @MaTB";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaTB", maTB)
            };
            DataTable dataTable = GetDataTableQuery(query, parameters);
            foreach (DataRow row in dataTable.Rows)
            {
                list.Add(new ChiTietThietBi_ThietBiDTO
                {
                    MaCTTB = Convert.ToInt32(row["MaCTTB"]),
                    MaTB = Convert.ToInt32(row["MaTB"]),
                    TenTB = row["TenTB"].ToString(),
                    TinhTrang = row["TinhTrang"].ToString(),
                    TrangThai = Convert.ToInt32(row["TrangThai"]),
                    NgayMua = Convert.ToDateTime(row["NgayMua"]),
                });
            }
            return list;
        }
    }
}
