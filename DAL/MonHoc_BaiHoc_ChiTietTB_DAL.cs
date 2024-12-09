using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.Devices;

namespace DAL
{
    public class MonHoc_BaiHoc_ChiTietTB_DAL : DatabaseHelper
    {
        public List<MonHoc_BaiHoc_ChiTietTB_DTO> GetAll()
        {
            List<MonHoc_BaiHoc_ChiTietTB_DTO> list = new List<MonHoc_BaiHoc_ChiTietTB_DTO>();
            string query = "SELECT mh_bh_cttb.MaMH, mh_bh_cttb.MaBH, mh_bh_cttb.MaTB, mh_bh_cttb.SoLuong, tb.TenTB, tb.MaLoai " +
                           "FROM MonHoc_BaiHoc_ChiTietTB mh_bh_cttb " +
                           "INNER JOIN ThietBi tb ON mh_bh_cttb.MaTB = tb.MaTB ";
            DataTable dataTable = GetDataTable(query);
            foreach (DataRow row in dataTable.Rows)
            {
                list.Add(new MonHoc_BaiHoc_ChiTietTB_DTO
                {
                    MaMH = Convert.ToInt32(row["MaMH"]),
                    MaBH = Convert.ToInt32(row["MaBH"]),
                    MaTB = Convert.ToInt32(row["MaTB"]),
                    TenTB = row["TenTB"].ToString(),
                    MaLoai = Convert.ToInt32(row["MaLoai"]),
                    SoLuong = Convert.ToInt32(row["SoLuong"])
                });
            }
            return list;
        }
        public List<MonHoc_BaiHoc_ThietBi_DTO> GetAllDetails()
        {
            List<MonHoc_BaiHoc_ThietBi_DTO> list = new List<MonHoc_BaiHoc_ThietBi_DTO>();
            string query = "SELECT mh_bh_cttb.MaMH, mh.TenMon, mh_bh_cttb.MaBH, bh.TenBaiHoc, mh_bh_cttb.MaTB, mh_bh_cttb.SoLuong, tb.TenTB, tb.MaLoai " +
                           "FROM MonHoc_BaiHoc_ChiTietTB mh_bh_cttb " +
                           "INNER JOIN MonHoc mh ON mh_bh_cttb.MaMH = mh.MaMon " +
                           "INNER JOIN BaiHoc bh ON mh_bh_cttb.MaBH = bh.MaBaiHoc " +
                           "INNER JOIN ThietBi tb ON mh_bh_cttb.MaTB = tb.MaTB ";
            DataTable dataTable = GetDataTable(query);
            foreach (DataRow row in dataTable.Rows)
            {
                list.Add(new MonHoc_BaiHoc_ThietBi_DTO
                {
                    MaMH = Convert.ToInt32(row["MaMH"]),
                    TenMon = row["TenMon"].ToString(),
                    MaBH = Convert.ToInt32(row["MaBH"]),
                    TenBaiHoc = row["TenBaiHoc"].ToString(),
                    MaTB = Convert.ToInt32(row["MaTB"]),
                    TenTB = row["TenTB"].ToString(),
                    MaLoai = Convert.ToInt32(row["MaLoai"]),
                    SoLuong = Convert.ToInt32(row["SoLuong"])
                });
            }
            return list;
        }

        // Lấy danh sách bản ghi theo MaMH và MaBH
        public List<MonHoc_BaiHoc_ChiTietTB_DTO> GetByMaMH_MaBH(int? maMH, int? maBH)
        {
            List<MonHoc_BaiHoc_ChiTietTB_DTO> list = new List<MonHoc_BaiHoc_ChiTietTB_DTO>();

            string query = "SELECT mh_bh_cttb.MaMH, mh_bh_cttb.MaBH, mh_bh_cttb.MaTB, mh_bh_cttb.SoLuong, tb.TenTB, tb.MaLoai " +
                           "FROM MonHoc_BaiHoc_ChiTietTB mh_bh_cttb " +
                           "INNER JOIN ThietBi tb ON mh_bh_cttb.MaTB = tb.MaTB " +
                           "WHERE mh_bh_cttb.MaMH = @MaMH AND mh_bh_cttb.MaBH = @MaBH";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaMH", maMH ?? (object)DBNull.Value),
                new SqlParameter("@MaBH", maBH ?? (object)DBNull.Value)
            };

            try
            {
                DataTable dataTable = GetDataTableQuery(query, parameters);

                foreach (DataRow row in dataTable.Rows)
                {
                    list.Add(new MonHoc_BaiHoc_ChiTietTB_DTO
                    {
                        MaMH = Convert.ToInt32(row["MaMH"]),
                        MaBH = Convert.ToInt32(row["MaBH"]),
                        MaTB = Convert.ToInt32(row["MaTB"]),
                        TenTB = row["TenTB"].ToString(),
                        MaLoai = Convert.ToInt32(row["MaLoai"]),
                        SoLuong = Convert.ToInt32(row["SoLuong"])
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetByMaMH_MaBH: {ex.Message}");
            }

            return list;
        }


        public List<ThietBiDTO> GetThietBiByMaMH_MaBH(int? maMH, int? maBH)
        {
            List<ThietBiDTO> list = new List<ThietBiDTO>();
            string query = "SELECT tb.MaTB, tb.TenTB, tb.MaLoai, tb.NSX, tb.SoLuong FROM MonHoc_BaiHoc_ChiTietTB item INNER JOIN ChiTietThietBi cttb ON item.MaCTTB = cttb.MaCTTB INNER JOIN ThietBi tb ON cttb.MaTB = tb.MaTB WHERE item.MaMH = @MaMH AND item.MaBH = @MaBH";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaMH", maMH),
                new SqlParameter("@MaBH", maBH)
            };
            DataTable dataTable = GetDataTableQuery(query, parameters);
            foreach (DataRow row in dataTable.Rows)
            {
                list.Add(new ThietBiDTO
                {
                    MaTB = Convert.ToInt32(row["MaMH"]),
                    TenTB = row["TenTB"].ToString(),
                    MaLoai = Convert.ToInt32(row["MaLoai"]),
                    SoLuong = Convert.ToInt32(row["SoLuong"])
                });
            }
            return list;
        }

        public bool Insert(MonHoc_BaiHoc_ThietBi_DTO dto)
        {
            string query = "INSERT INTO MonHoc_BaiHoc_ChiTietTB (MaMH, MaBH, MaTB, SoLuong) VALUES (@MaMH, @MaBH, @MaTB, @SoLuong)";

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaMH", dto.MaMH);
                command.Parameters.AddWithValue("@MaBH", dto.MaBH);
                command.Parameters.AddWithValue("@MaTB", dto.MaTB);
                command.Parameters.AddWithValue("@SoLuong", dto.SoLuong);

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
        public bool Update(MonHoc_BaiHoc_ThietBi_DTO dto)
        {
            string query = "UPDATE MonHoc_BaiHoc_ChiTietTB SET SoLuong = @SoLuong WHERE MaTB = @MaTB AND MaMH = @MaMH AND MaBH = @MaBH";
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaMH", dto.MaMH);
                command.Parameters.AddWithValue("@MaBH", dto.MaBH);
                command.Parameters.AddWithValue("@MaTB", dto.MaTB);
                command.Parameters.AddWithValue("@SoLuong", dto.SoLuong);

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

       

        public bool Delete(MonHoc_BaiHoc_ThietBi_DTO dto)
        {
            string query = "DELETE FROM MonHoc_BaiHoc_ChiTietTB WHERE MaMH = @MaMH AND MaBH = @MaBH AND MaTB = @MaTB";
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaMH", dto.MaMH);
                command.Parameters.AddWithValue("@MaBH", dto.MaBH);
                command.Parameters.AddWithValue("@MaTB", dto.MaTB);

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}
