using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class ThanhLyThietBiDAL : DatabaseHelper
{
    // Lấy tất cả bản ghi thanh lý thiết bị
    public List<ThanhLyThietBiDTO> GetAll()
    {
        List<ThanhLyThietBiDTO> list = new List<ThanhLyThietBiDTO>();
        string query = "SELECT * FROM ThanhLyThietBi";
        DataTable dataTable = GetDataTable(query);

        foreach (DataRow row in dataTable.Rows)
        {
            list.Add(new ThanhLyThietBiDTO
            {
                MaThanhLy = Convert.ToInt32(row["MaThanhLy"]),
                MaNguoiDung = row["MaNguoiDung"].ToString(),
                NgayThanhLy = row["NgayThanhLy"] as DateTime?,
                SoLuong = Convert.ToInt32(row["SoLuong"]),
                TongTien = Convert.ToSingle(row["TongTien"])
            });
        }
        return list;
    }

    // Lấy bản ghi thanh lý thiết bị theo mã
    public ThanhLyThietBiDTO GetByID(int maThanhLy)
    {
        string query = "SELECT * FROM ThanhLyThietBi WHERE MaThanhLy = @MaThanhLy";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaThanhLy", maThanhLy);
            connection.Open();
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }

            if (dataTable.Rows.Count == 1)
            {
                DataRow row = dataTable.Rows[0];
                return new ThanhLyThietBiDTO
                {
                    MaThanhLy = Convert.ToInt32(row["MaThanhLy"]),
                    MaNguoiDung = row["MaNguoiDung"].ToString(),
                    NgayThanhLy = row["NgayThanhLy"] as DateTime?,
                    SoLuong = Convert.ToInt32(row["SoLuong"]),
                    TongTien = Convert.ToSingle(row["TongTien"])
                };
            }
        }
        return null;
    }

    // Thêm bản ghi thanh lý thiết bị
    public bool Insert(ThanhLyThietBiDTO thanhLy)
    {
        string query = "INSERT INTO ThanhLyThietBi (MaNguoiDung, NgayThanhLy, SoLuong, TongTien) VALUES (@MaNguoiDung, @NgayThanhLy, @SoLuong, @TongTien)";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaNguoiDung", thanhLy.MaNguoiDung);
            command.Parameters.AddWithValue("@NgayThanhLy", thanhLy.NgayThanhLy.HasValue ? (object)thanhLy.NgayThanhLy.Value : DBNull.Value);
            command.Parameters.AddWithValue("@SoLuong", thanhLy.SoLuong);
            command.Parameters.AddWithValue("@TongTien", thanhLy.TongTien);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Cập nhật bản ghi thanh lý thiết bị
    public bool Update(ThanhLyThietBiDTO thanhLy)
    {
        string query = "UPDATE ThanhLyThietBi SET MaNguoiDung = @MaNguoiDung, NgayThanhLy = @NgayThanhLy, SoLuong = @SoLuong, TongTien = @TongTien WHERE MaThanhLy = @MaThanhLy";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaThanhLy", thanhLy.MaThanhLy);
            command.Parameters.AddWithValue("@MaNguoiDung", thanhLy.MaNguoiDung);
            command.Parameters.AddWithValue("@NgayThanhLy", thanhLy.NgayThanhLy.HasValue ? (object)thanhLy.NgayThanhLy.Value : DBNull.Value);
            command.Parameters.AddWithValue("@SoLuong", thanhLy.SoLuong);
            command.Parameters.AddWithValue("@TongTien", thanhLy.TongTien);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Xóa bản ghi thanh lý thiết bị
    public bool Delete(int maThanhLy)
    {
        string query = "DELETE FROM ThanhLyThietBi WHERE MaThanhLy = @MaThanhLy";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaThanhLy", maThanhLy);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Lấy tất cả chi tiết thanh lý theo mã thanh lý
    public List<ChiTietThanhLyDTO> GetAll(int maThanhLy)
    {
        List<ChiTietThanhLyDTO> list = new List<ChiTietThanhLyDTO>();
        string query = "SELECT * FROM ChiTietThanhLy WHERE MaThanhLy = @MaThanhLy";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaThanhLy", maThanhLy);
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }

            foreach (DataRow row in dataTable.Rows)
            {
                list.Add(new ChiTietThanhLyDTO
                {
                    MaThanhLy = Convert.ToInt32(row["MaThanhLy"]),
                    MaCTTB_NCC = Convert.ToInt32(row["MaCTTB_NCC"]),
                    GiaThanhLy = Convert.ToSingle(row["GiaThanhLy"])
                });
            }
        }
        return list;
    }

    // Thêm chi tiết thanh lý
    public bool Insert(ChiTietThanhLyDTO chiTiet)
    {
        string query = "INSERT INTO ChiTietThanhLy (MaThanhLy, MaCTTB_NCC, GiaThanhLy) VALUES (@MaThanhLy, @MaCTTB_NCC, @GiaThanhLy)";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaThanhLy", chiTiet.MaThanhLy);
            command.Parameters.AddWithValue("@MaCTTB_NCC", chiTiet.MaCTTB_NCC);
            command.Parameters.AddWithValue("@GiaThanhLy", chiTiet.GiaThanhLy);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Xóa chi tiết thanh lý theo mã thanh lý và mã thiết bị nhà cung cấp
    public bool Delete(int maThanhLy, int maCTTB_NCC)
    {
        string query = "DELETE FROM ChiTietThanhLy WHERE MaThanhLy = @MaThanhLy AND MaCTTB_NCC = @MaCTTB_NCC";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaThanhLy", maThanhLy);
            command.Parameters.AddWithValue("@MaCTTB_NCC", maCTTB_NCC);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }
}
