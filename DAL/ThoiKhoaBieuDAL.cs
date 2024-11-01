using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class ThoiKhoaBieuDAL : DatabaseHelper
{
    // Lấy tất cả thời khóa biểu
    public List<ThoiKhoaBieuDTO> GetAll()
    {
        List<ThoiKhoaBieuDTO> list = new List<ThoiKhoaBieuDTO>();
        string query = "SELECT * FROM ThoiKhoaBieu";
        DataTable dataTable = GetDataTable(query);

        foreach (DataRow row in dataTable.Rows)
        {
            list.Add(new ThoiKhoaBieuDTO
            {
                MaTKB = Convert.ToInt32(row["MaTKB"]),
                MaNguoiDung = row["MaNguoiDung"].ToString(),
                MaMon = row["MaMon"] as int?,
                MaBaiHoc = row["MaBaiHoc"] as int?,
                MaPhong = row["MaPhong"] as int?,
                MaLopHoc = row["MaLopHoc"] as int?,
                GioHoc = row["GioHoc"] as TimeSpan?,
                NgayHoc = row["NgayHoc"] as DateTime?
            });
        }
        return list;
    }

    // Lấy thời khóa biểu theo mã
    public ThoiKhoaBieuDTO GetByID(int maTKB)
    {
        string query = "SELECT * FROM ThoiKhoaBieu WHERE MaTKB = @MaTKB";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaTKB", maTKB);
            connection.Open();
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }

            if (dataTable.Rows.Count == 1)
            {
                DataRow row = dataTable.Rows[0];
                return new ThoiKhoaBieuDTO
                {
                    MaTKB = Convert.ToInt32(row["MaTKB"]),
                    MaNguoiDung = row["MaNguoiDung"].ToString(),
                    MaMon = row["MaMon"] as int?,
                    MaBaiHoc = row["MaBaiHoc"] as int?,
                    MaPhong = row["MaPhong"] as int?,
                    MaLopHoc = row["MaLopHoc"] as int?,
                    GioHoc = row["GioHoc"] as TimeSpan?,
                    NgayHoc = row["NgayHoc"] as DateTime?
                };
            }
        }
        return null;
    }

    // Thêm thời khóa biểu
    public bool Insert(ThoiKhoaBieuDTO tkb)
    {
        string query = "INSERT INTO ThoiKhoaBieu (MaNguoiDung, MaMon, MaBaiHoc, MaPhong, MaLopHoc, GioHoc, NgayHoc) VALUES (@MaNguoiDung, @MaMon, @MaBaiHoc, @MaPhong, @MaLopHoc, @GioHoc, @NgayHoc)";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaNguoiDung", tkb.MaNguoiDung);
            command.Parameters.AddWithValue("@MaMon", tkb.MaMon.HasValue ? (object)tkb.MaMon.Value : DBNull.Value);
            command.Parameters.AddWithValue("@MaBaiHoc", tkb.MaBaiHoc.HasValue ? (object)tkb.MaBaiHoc.Value : DBNull.Value);
            command.Parameters.AddWithValue("@MaPhong", tkb.MaPhong.HasValue ? (object)tkb.MaPhong.Value : DBNull.Value);
            command.Parameters.AddWithValue("@MaLopHoc", tkb.MaLopHoc.HasValue ? (object)tkb.MaLopHoc.Value : DBNull.Value);
            command.Parameters.AddWithValue("@GioHoc", tkb.GioHoc.HasValue ? (object)tkb.GioHoc.Value : DBNull.Value);
            command.Parameters.AddWithValue("@NgayHoc", tkb.NgayHoc.HasValue ? (object)tkb.NgayHoc.Value : DBNull.Value);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Cập nhật thời khóa biểu
    public bool Update(ThoiKhoaBieuDTO tkb)
    {
        string query = "UPDATE ThoiKhoaBieu SET MaNguoiDung = @MaNguoiDung, MaMon = @MaMon, MaBaiHoc = @MaBaiHoc, MaPhong = @MaPhong, MaLopHoc = @MaLopHoc, GioHoc = @GioHoc, NgayHoc = @NgayHoc WHERE MaTKB = @MaTKB";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaTKB", tkb.MaTKB);
            command.Parameters.AddWithValue("@MaNguoiDung", tkb.MaNguoiDung);
            command.Parameters.AddWithValue("@MaMon", tkb.MaMon.HasValue ? (object)tkb.MaMon.Value : DBNull.Value);
            command.Parameters.AddWithValue("@MaBaiHoc", tkb.MaBaiHoc.HasValue ? (object)tkb.MaBaiHoc.Value : DBNull.Value);
            command.Parameters.AddWithValue("@MaPhong", tkb.MaPhong.HasValue ? (object)tkb.MaPhong.Value : DBNull.Value);
            command.Parameters.AddWithValue("@MaLopHoc", tkb.MaLopHoc.HasValue ? (object)tkb.MaLopHoc.Value : DBNull.Value);
            command.Parameters.AddWithValue("@GioHoc", tkb.GioHoc.HasValue ? (object)tkb.GioHoc.Value : DBNull.Value);
            command.Parameters.AddWithValue("@NgayHoc", tkb.NgayHoc.HasValue ? (object)tkb.NgayHoc.Value : DBNull.Value);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Xóa thời khóa biểu
    public bool Delete(int maTKB)
    {
        string query = "DELETE FROM ThoiKhoaBieu WHERE MaTKB = @MaTKB";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaTKB", maTKB);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Tìm kiếm thời khóa biểu theo từ khóa (full-text search)
    public List<ThoiKhoaBieuDTO> Search(string keyword)
    {
        List<ThoiKhoaBieuDTO> list = new List<ThoiKhoaBieuDTO>();
        string query = "SELECT * FROM ThoiKhoaBieu WHERE CONTAINS((MaNguoiDung, MaMon, MaBaiHoc, MaPhong, MaLopHoc), @Keyword)";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Keyword", $"\"{keyword}\"");
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }

            foreach (DataRow row in dataTable.Rows)
            {
                list.Add(new ThoiKhoaBieuDTO
                {
                    MaTKB = Convert.ToInt32(row["MaTKB"]),
                    MaNguoiDung = row["MaNguoiDung"].ToString(),
                    MaMon = row["MaMon"] as int?,
                    MaBaiHoc = row["MaBaiHoc"] as int?,
                    MaPhong = row["MaPhong"] as int?,
                    MaLopHoc = row["MaLopHoc"] as int?,
                    GioHoc = row["GioHoc"] as TimeSpan?,
                    NgayHoc = row["NgayHoc"] as DateTime?
                });
            }
        }
        return list;
    }
}
