using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    using DTO;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class BaoDuongDAL : DatabaseHelper
    {
        public List<BaoDuongDTO> GetAll()
        {
            List<BaoDuongDTO> list = new List<BaoDuongDTO>();
            string query = @"Select MaBD, BD.MaCTTB_NCC, TenTB, TenPhong, NgayBD, KetQua, ChiPhi
                            From BaoDuong BD
                            JOIN ChiTietThietBi_NhaCungCap CTTBNCC ON CTTBNCC.MaCTTB_NCC = BD.MaCTTB_NCC
                            JOIN ChiTietThietBi CTTB ON CTTB.MaCTTB = CTTBNCC.MaCTTB
                            JOIN ThietBi TB ON TB.MaTB = CTTB.MaTB
                            LEFT JOIN ChiTietThietBi_Phong CP ON CTTB.MaCTTB = CP.MaCTTB
                            LEFT JOIN PhongHoc P ON P.MaPhong = CP.MaPhong";
            DataTable dataTable = GetDataTable(query);

            foreach (DataRow row in dataTable.Rows)
            {
                list.Add(new BaoDuongDTO
                {
                    MaBD = Convert.ToInt32(row["MaBD"]),
                    MaCTTB_NCC = Convert.ToInt32(row["MaCTTB_NCC"]),
                    TenTB = row["TenTB"].ToString(),
                    TenPhong = row["TenPhong"].ToString(),
                    NgayBD = row["NgayBD"] as DateTime?,
                    KetQua = row["KetQua"].ToString(),
                    ChiPhi = Convert.ToSingle(row["ChiPhi"])
                });
            }
            return list;
        }

        public BaoDuongDTO GetByID(int maBD)
        {
            string query = "SELECT * FROM BaoDuong WHERE MaBD = @MaBD";
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaBD", maBD);
                connection.Open();
                DataTable dataTable = new DataTable();
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }

                if (dataTable.Rows.Count == 1)
                {
                    DataRow row = dataTable.Rows[0];
                    return new BaoDuongDTO
                    {
                        MaBD = Convert.ToInt32(row["MaBD"]),
                        MaCTTB_NCC = Convert.ToInt32(row["MaCTTB_NCC"]),
                        NgayBD = row["NgayBD"] as DateTime?,
                        KetQua = row["KetQua"].ToString(),
                        ChiPhi = Convert.ToSingle(row["ChiPhi"])
                    };
                }
            }
            return null;
        }

        // Thêm bản ghi bảo dưỡng
        public bool Insert(BaoDuongDTO baoDuong)
        {
            string query = "INSERT INTO BaoDuong (MaCTTB_NCC, NgayBD, KetQua, ChiPhi) VALUES (@MaCTTB_NCC, @NgayBD, @KetQua, @ChiPhi)";
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaCTTB_NCC", baoDuong.MaCTTB_NCC);
                command.Parameters.AddWithValue("@NgayBD", baoDuong.NgayBD.HasValue ? (object)baoDuong.NgayBD.Value : DBNull.Value);
                command.Parameters.AddWithValue("@KetQua", baoDuong.KetQua ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ChiPhi", baoDuong.ChiPhi);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        // Cập nhật bản ghi bảo dưỡng
        public bool Update(BaoDuongDTO baoDuong)
        {
            string query = "UPDATE BaoDuong SET MaCTTB_NCC = @MaCTTB_NCC, NgayBD = @NgayBD, KetQua = @KetQua, ChiPhi = @ChiPhi WHERE MaBD = @MaBD";
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaBD", baoDuong.MaBD);
                command.Parameters.AddWithValue("@MaCTTB_NCC", baoDuong.MaCTTB_NCC);
                command.Parameters.AddWithValue("@NgayBD", baoDuong.NgayBD.HasValue ? (object)baoDuong.NgayBD.Value : DBNull.Value);
                command.Parameters.AddWithValue("@KetQua", baoDuong.KetQua ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ChiPhi", baoDuong.ChiPhi);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        // Xóa bản ghi bảo dưỡng
        public bool Delete(int maBD)
        {
            string query = "DELETE FROM BaoDuong WHERE MaBD = @MaBD";
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaBD", maBD);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        // Tìm kiếm bản ghi bảo dưỡng theo từ khóa
        public List<BaoDuongDTO> Search(string keyword)
        {
            List<BaoDuongDTO> list = new List<BaoDuongDTO>();
            string query = "SELECT * FROM BaoDuong WHERE CONTAINS(KetQua, @Keyword)";
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Keyword", $"\"*{keyword}*\"");
                DataTable dataTable = new DataTable();
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }

                foreach (DataRow row in dataTable.Rows)
                {
                    list.Add(new BaoDuongDTO
                    {
                        MaBD = Convert.ToInt32(row["MaBD"]),
                        MaCTTB_NCC = Convert.ToInt32(row["MaCTTB_NCC"]),
                        NgayBD = row["NgayBD"] as DateTime?,
                        KetQua = row["KetQua"].ToString(),
                        ChiPhi = Convert.ToSingle(row["ChiPhi"])
                    });
                }
            }
            return list;
        }
    }

}
