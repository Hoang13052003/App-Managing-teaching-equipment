using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

public class MuonThietBiDAL : DatabaseHelper
{
    // Lấy tất cả bản ghi mượn thiết bị
    public List<MuonThietBiDTO> GetAll()
    {
        List<MuonThietBiDTO> list = new List<MuonThietBiDTO>();
        string query = "SELECT * FROM MuonThietBi";
        DataTable dataTable = GetDataTable(query);

        foreach (DataRow row in dataTable.Rows)
        {
            list.Add(new MuonThietBiDTO
            {
                MaMuon = Convert.ToInt32(row["MaMuon"]),
                MaNguoiDung = row["MaNguoiDung"].ToString(),
                MaPhong = Convert.ToInt32(row["MaPhong"])
            });
        }
        return list;
    }

    // Lấy bản ghi mượn thiết bị theo mã mượn
    public MuonThietBiDTO GetByID(int maMuon)
    {
        string query = "SELECT * FROM MuonThietBi WHERE MaMuon = @MaMuon";
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

            if (dataTable.Rows.Count == 1)
            {
                DataRow row = dataTable.Rows[0];
                return new MuonThietBiDTO
                {
                    MaMuon = Convert.ToInt32(row["MaMuon"]),
                    MaNguoiDung = row["MaNguoiDung"].ToString(),
                    MaPhong = Convert.ToInt32(row["MaPhong"])
                };
            }
        }
        return null;
    }

    // Thêm bản ghi mượn thiết bị
    public bool Insert(MuonThietBiDTO muonThietBi)
    {
        string query = "INSERT INTO MuonThietBi (MaNguoiDung, MaPhong) VALUES (@MaNguoiDung, @MaPhong)";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaNguoiDung", muonThietBi.MaNguoiDung);
            command.Parameters.AddWithValue("@MaPhong", muonThietBi.MaPhong);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Cập nhật bản ghi mượn thiết bị
    public bool Update(MuonThietBiDTO muonThietBi)
    {
        string query = "UPDATE MuonThietBi SET MaNguoiDung = @MaNguoiDung, MaPhong = @MaPhong WHERE MaMuon = @MaMuon";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaMuon", muonThietBi.MaMuon);
            command.Parameters.AddWithValue("@MaNguoiDung", muonThietBi.MaNguoiDung);
            command.Parameters.AddWithValue("@MaPhong", muonThietBi.MaPhong);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Xóa bản ghi mượn thiết bị
    public bool Delete(int maMuon)
    {
        string query = "DELETE FROM MuonThietBi WHERE MaMuon = @MaMuon";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaMuon", maMuon);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Tìm kiếm bản ghi mượn thiết bị theo từ khóa
    public List<MuonThietBiDTO> Search(string keyword)
    {
        List<MuonThietBiDTO> list = new List<MuonThietBiDTO>();
        string query = "SELECT * FROM MuonThietBi WHERE CONTAINS(MaNguoiDung, @Keyword) OR CONTAINS(MaPhong, @Keyword)";
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
                list.Add(new MuonThietBiDTO
                {
                    MaMuon = Convert.ToInt32(row["MaMuon"]),
                    MaNguoiDung = row["MaNguoiDung"].ToString(),
                    MaPhong = Convert.ToInt32(row["MaPhong"])
                });
            }
        }
        return list;
    }


    public ChiTietMuonThietBiDTO GetCTPMByID(int maMuon)
    {
        string query = "SELECT * FROM ChiTietMuonThietBi WHERE MaMuon = @MaMuon";
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

            if (dataTable.Rows.Count == 1)
            {
                DataRow row = dataTable.Rows[0];
                return new ChiTietMuonThietBiDTO
                {
                    MaMuon = Convert.ToInt32(row["MaMuon"]),
                    MaCTTB_NCC = Convert.ToInt32(row["MaCTTB_NCC"]),
                    NgayMuon = Convert.ToDateTime(row["NgayMuon"]),
                    NgayTra = row["NgayTra"] != DBNull.Value ? Convert.ToDateTime(row["NgayTra"]) : (DateTime?)null,

                    TrangThai = Convert.ToInt32(row["TrangThai"]) == 0
                };
            }

        }
        return null;
    }

    public DataTable GetMuonThietBiAndDetails()
    {
        DataTable dt = new DataTable();
        string query = @"
            SELECT 
                m.MaMuon,
                m.MaNguoiDung,
                m.MaPhong,
                c.MaCTTB_NCC,
                c.NgayMuon,
                c.NgayTra,
                c.TrangThai
            FROM 
                MuonThietBi m
            INNER JOIN 
                ChiTietMuonThietBi c ON m.MaMuon = c.MaMuon";

        try
        {
            using (SqlConnection conn = GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }
        }
        catch (Exception ex)
        {
            // Xử lý lỗi (nếu có)
            Console.WriteLine("Error: " + ex.Message);
        }

        return dt;
    }

    public DataTable GetThietBiDetailsByMaCTTB(int maCTTB_NCC)
    {
        string query = @"
        SELECT t.MaTB, t.TenTB, t.NSX, t.SoLuong,
               ncc.TenNCC
        FROM ThietBi t
        INNER JOIN ChiTietThietBi c ON t.MaTB = c.MaTB
        LEFT JOIN ChiTietThietBi_NhaCungCap ct ON c.MaCTTB = ct.MaCTTB
        LEFT JOIN NhaCungCap ncc ON ct.MaNCC = ncc.MaNCC
        WHERE ct.MaCTTB = @MaCTTB_NCC";

        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            // Thay 'AddWithValue' bằng 'Add' và chỉ rõ kiểu dữ liệu
            command.Parameters.Add("@MaCTTB_NCC", SqlDbType.Int).Value = maCTTB_NCC;

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();

            // Mở kết nối và lấy dữ liệu
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                MessageBox.Show("Có lỗi xảy ra khi truy vấn dữ liệu: " + ex.Message);
            }
            finally
            {
                // Đảm bảo kết nối luôn được đóng
                connection.Close();
            }

            return dataTable;
        }
    }

}

