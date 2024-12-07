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

        public List<LoaiThietBiDTO> SearchLoaiTB(int pMaNCC)
        {
            List<LoaiThietBiDTO> listLoaiThietBi = new List<LoaiThietBiDTO>();
            string query = @"SELECT DISTINCT LTB.MaLoai, LTB.TenLoai
                            FROM NhaCungCap NCC
                            JOIN ChiTietThietBi_NhaCungCap CTTB_NCC ON NCC.MaNCC = CTTB_NCC.MaNCC
                            JOIN ChiTietThietBi CTTB ON CTTB.MaCTTB = CTTB_NCC.MaCTTB
                            JOIN ThietBi TB ON CTTB.MaTB = TB.MaTB
                            JOIN LoaiThietBi LTB ON TB.MaLoai = LTB.MaLoai
                            WHERE NCC.MaNCC = @MaNCC";

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaNCC", pMaNCC);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    LoaiThietBiDTO loaiThietBi = new LoaiThietBiDTO
                    {
                        MaLoai = reader.GetInt32(reader.GetOrdinal("MaLoai")),
                        TenLoai = reader.GetString(reader.GetOrdinal("TenLoai"))
                    };
                    listLoaiThietBi.Add(loaiThietBi);
                }
            }
            return listLoaiThietBi;
        }

        public List<ThietBiDTO> SearchThietBi(int pMaLoai)
        {
            List<ThietBiDTO> listThietBi = new List<ThietBiDTO>();
            string query = @"SELECT DISTINCT TB.MaTB, TB.TenTB
                            FROM LoaiThietBi LTB
                            JOIN ThietBi TB ON LTB.MaLoai = TB.MaLoai
                            WHERE LTB.MaLoai = @MaLoai";

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaLoai", pMaLoai);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ThietBiDTO thietBi = new ThietBiDTO
                    {
                        MaTB = reader.GetInt32(reader.GetOrdinal("MaTB")),
                        TenTB = reader.GetString(reader.GetOrdinal("TenTB"))
                    };
                    listThietBi.Add(thietBi);
                }
            }
            return listThietBi;
        }

        public List<ThietBiDTO> SearchThietBi_NCC(int pMaNCC)
        {
            List<ThietBiDTO> listThietBi = new List<ThietBiDTO>();
            string query = @"SELECT DISTINCT TB.MaTB, TB.TenTB, TB.SoLuong
                    FROM NhaCungCap NCC
                    JOIN ChiTietThietBi_NhaCungCap CTTB_NCC ON NCC.MaNCC = CTTB_NCC.MaNCC
                    JOIN ChiTietThietBi CTTB ON CTTB.MaCTTB = CTTB_NCC.MaCTTB
                    JOIN ThietBi TB ON CTTB.MaTB = TB.MaTB
                    WHERE NCC.MaNCC = @MaNCC";

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaNCC", pMaNCC);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ThietBiDTO thietBi = new ThietBiDTO
                    {
                        MaTB = reader.GetInt32(reader.GetOrdinal("MaTB")),
                        TenTB = reader.GetString(reader.GetOrdinal("TenTB")),
                        SoLuong = reader.GetInt32(reader.GetOrdinal("SoLuong"))
                    };
                    listThietBi.Add(thietBi);
                }
            }
            return listThietBi;
        }

        public string tenNCC(int pMaNCC)
        {
            string tenNCC = "";

            string query = @"SELECT TenNCC
                     FROM NhaCungCap
                     WHERE MaNCC = " + pMaNCC;

            DataTable dataTable = GetDataTable(query);

            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                if (row["TenNCC"] != DBNull.Value)
                {
                    tenNCC = (row["TenNCC"]).ToString();
                }
            }
            return tenNCC;
        }
    }
}
