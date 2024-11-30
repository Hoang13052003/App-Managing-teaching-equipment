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
    public class ChiTietMuonThietBiDAL : DatabaseHelper
    {
        public List<ChiTietMuonThietBiDTO> GetByMaMuon(int maMuon)
        {
            string query = "SELECT item.MaMuon, item.MaCTTB, item.TrangThai, tb.MaTB, tb.TenTB FROM ChiTietMuonThietBi item INNER JOIN ChiTietThietBi cttb ON item.MaCTTB = cttb.MaCTTB INNER JOIN ThietBi tb ON cttb.MaTB = tb.MaTB WHERE MaMuon = @MaMuon";
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaMuon", maMuon);
                connection.Open();
                DataTable dataTable = new DataTable();
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }

                List<ChiTietMuonThietBiDTO> chiTietList = new List<ChiTietMuonThietBiDTO>();

                foreach (DataRow row in dataTable.Rows)
                {
                    chiTietList.Add(new ChiTietMuonThietBiDTO
                    {
                        MaMuon = row["MaMuon"] != DBNull.Value ? Convert.ToInt32(row["MaMuon"]) : 0,
                        MaCTTB = row["MaCTTB"] != DBNull.Value ? Convert.ToInt32(row["MaCTTB"]) : 0,
                        TrangThai = row["TrangThai"] != DBNull.Value && Convert.ToBoolean(row["TrangThai"]),
                        MaTB = Convert.ToInt32(row["MaTB"]),
                        TenTB = row["TenTB"].ToString()
                    });
                }

                return chiTietList;
            }
        }


        // Thêm mới một bản ghi ChiTietMuonThietBi
        public bool Insert(ChiTietMuonThietBiDTO chiTietMuonThietBi)
        {
            string query = @"INSERT INTO ChiTietMuonThietBi (MaMuon, MaCTTB) 
                         VALUES (@MaMuon, @MaCTTB)";
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaMuon", chiTietMuonThietBi.MaMuon);
                command.Parameters.AddWithValue("@MaCTTB", chiTietMuonThietBi.MaCTTB);

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        // Cập nhật thông tin ChiTietMuonThietBi
        public bool Update(ChiTietMuonThietBiDTO chiTietMuonThietBi)
        {
            string query = @"UPDATE ChiTietMuonThietBi 
                         SET MaCTTB = @MaCTTB 
                         WHERE MaMuon = @MaMuon";
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaMuon", chiTietMuonThietBi.MaMuon);
                command.Parameters.AddWithValue("@MaCTTB", chiTietMuonThietBi.MaCTTB);

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
        public bool Update_TrangThaiThieu(ChiTietMuonThietBiDTO chiTietMuonThietBi)
        {
            string query = @"UPDATE ChiTietMuonThietBi 
                         SET TrangThai = @TrangThai 
                         WHERE MaMuon = @MaMuon AND MaCTTB = @MaCTTB";
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaMuon", chiTietMuonThietBi.MaMuon);
                command.Parameters.AddWithValue("@MaCTTB", chiTietMuonThietBi.MaCTTB);
                command.Parameters.AddWithValue("@TrangThai", chiTietMuonThietBi.TrangThai);


                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
        // Xóa một bản ghi ChiTietMuonThietBi
        public bool Delete(int maMuon, int maCTTB)
        {
            string query = @"DELETE FROM ChiTietMuonThietBi 
                         WHERE MaMuon = @MaMuon AND MaCTTB = @MaCTTB";
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaMuon", maMuon);
                command.Parameters.AddWithValue("@MaCTTB", maCTTB);

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}
