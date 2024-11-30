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
            string query = "SELECT * FROM MonHoc_BaiHoc_ChiTietTB";
            DataTable dataTable = GetDataTable(query);
            foreach (DataRow row in dataTable.Rows)
            {
                list.Add(new MonHoc_BaiHoc_ChiTietTB_DTO
                {
                    MaMH = Convert.ToInt32(row["MaMH"]),
                    MaBH = Convert.ToInt32(row["MaBH"]),
                    MaCTTB = Convert.ToInt32(row["MaCTTB"])
                });
            }
            return list;
        }


        // Lấy danh sách bản ghi theo MaMH và MaBH
        public List<MonHoc_BaiHoc_ChiTietTB_DTO> GetByMaMH_MaBH(int? maMH, int? maBH)
        {
            List<MonHoc_BaiHoc_ChiTietTB_DTO> list = new List<MonHoc_BaiHoc_ChiTietTB_DTO>();

            string query = "SELECT mh_bh_cttb.MaMH, mh_bh_cttb.MaBH, mh_bh_cttb.MaCTTB, cttb.MaTB, tb.TenTB, tb.MaLoai " +
                           "FROM MonHoc_BaiHoc_ChiTietTB mh_bh_cttb " +
                           "INNER JOIN ChiTietThietBi cttb ON mh_bh_cttb.MaCTTB = cttb.MaCTTB " +
                           "INNER JOIN ThietBi tb ON cttb.MaTB = tb.MaTB " +
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
                        MaCTTB = Convert.ToInt32(row["MaCTTB"]),
                        MaTB = Convert.ToInt32(row["MaTB"]),
                        TenTB = row["TenTB"].ToString(),
                        MaLoai = Convert.ToInt32(row["MaLoai"])
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

        public int Insert(MonHoc_BaiHoc_ChiTietTB_DTO dto)
        {
            string query = "INSERT INTO MonHoc_BaiHoc_ChiTietTB (MaMH, MaBH, MaCTTB) VALUES (@MaMH, @MaBH, @MaCTTB)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaMH", dto.MaMH),
                new SqlParameter("@MaBH", dto.MaBH),
                new SqlParameter("@MaCTTB", dto.MaCTTB)
            };

            try
            {
                return GetNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in InsertMonHoc_BaiHoc_ChiTietTB: {ex.Message}");
                return -1; // Indicate error (adjust return type as needed)
            }
        }
        public int Update(MonHoc_BaiHoc_ChiTietTB_DTO dto)
        {
            string query = "UPDATE MonHoc_BaiHoc_ChiTietTB SET MaMH = @MaMH, MaBH = @MaBH WHERE MaCTTB = @MaCTTB";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaMH", dto.MaMH),
                new SqlParameter("@MaBH", dto.MaBH),
                new SqlParameter("@MaCTTB", dto.MaCTTB)
            };

            try
            {
                return GetNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateMonHoc_BaiHoc_ChiTietTB: {ex.Message}");
                return -1; // Indicate error (adjust return type as needed)
            }
        }
        public int DeleteMonHoc_BaiHoc_ChiTietTB(int maCTTB)
        {
            string query = "DELETE FROM MonHoc_BaiHoc_ChiTietTB WHERE MaCTTB = @MaCTTB";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaCTTB", maCTTB)
            };
            try
            {
                return GetNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteMonHoc_BaiHoc_ChiTietTB: {ex.Message}");
                return -1; // Indicate error (adjust return type as needed)
            }
        }
    }
}
