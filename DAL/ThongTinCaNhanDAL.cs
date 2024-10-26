using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class ThongTinCaNhanDAL : DatabaseHelper
{
    // Lấy tất cả thông tin cá nhân
    public List<ThongTinCaNhanDTO> GetAll()
    {
        List<ThongTinCaNhanDTO> list = new List<ThongTinCaNhanDTO>();
        string query = "SELECT * FROM ThongTinCaNhan";
        DataTable dataTable = GetDataTable(query);

        foreach (DataRow row in dataTable.Rows)
        {
            list.Add(new ThongTinCaNhanDTO
            {
                MaNguoiDung = row["MaNguoiDung"].ToString(),
                HoTen = row["HoTen"].ToString(),
                GioiTinh = row["GioiTinh"].ToString(),
                NgaySinh = row["NgaySinh"] as DateTime?,
                Email = row["Email"].ToString(),
                SDT = row["SDT"].ToString(),
                DiaChi = row["DiaChi"].ToString()
            });
        }
        return list;
    }

    // Lấy thông tin cá nhân theo mã người dùng
    public ThongTinCaNhanDTO GetByMaNguoiDung(string maNguoiDung)
    {
        string query = "SELECT * FROM ThongTinCaNhan WHERE MaNguoiDung = @MaNguoiDung";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);
            connection.Open();
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }

            if (dataTable.Rows.Count == 1)
            {
                DataRow row = dataTable.Rows[0];
                return new ThongTinCaNhanDTO
                {
                    MaNguoiDung = row["MaNguoiDung"].ToString(),
                    HoTen = row["HoTen"].ToString(),
                    GioiTinh = row["GioiTinh"].ToString(),
                    NgaySinh = row["NgaySinh"] as DateTime?,
                    Email = row["Email"].ToString(),
                    SDT = row["SDT"].ToString(),
                    DiaChi = row["DiaChi"].ToString()
                };
            }
        }
        return null;
    }

    // Thêm thông tin cá nhân
    public bool Insert(ThongTinCaNhanDTO thongTinCaNhan)
    {
        string query = "INSERT INTO ThongTinCaNhan (MaNguoiDung, HoTen, GioiTinh, NgaySinh, Email, SDT, DiaChi) VALUES (@MaNguoiDung, @HoTen, @GioiTinh, @NgaySinh, @Email, @SDT, @DiaChi)";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaNguoiDung", thongTinCaNhan.MaNguoiDung);
            command.Parameters.AddWithValue("@HoTen", thongTinCaNhan.HoTen);
            command.Parameters.AddWithValue("@GioiTinh", thongTinCaNhan.GioiTinh);
            command.Parameters.AddWithValue("@NgaySinh", thongTinCaNhan.NgaySinh.HasValue ? (object)thongTinCaNhan.NgaySinh.Value : DBNull.Value);
            command.Parameters.AddWithValue("@Email", thongTinCaNhan.Email);
            command.Parameters.AddWithValue("@SDT", thongTinCaNhan.SDT);
            command.Parameters.AddWithValue("@DiaChi", thongTinCaNhan.DiaChi);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Cập nhật thông tin cá nhân
    public bool Update(ThongTinCaNhanDTO thongTinCaNhan)
    {
        string query = "UPDATE ThongTinCaNhan SET HoTen = @HoTen, GioiTinh = @GioiTinh, NgaySinh = @NgaySinh, Email = @Email, SDT = @SDT, DiaChi = @DiaChi WHERE MaNguoiDung = @MaNguoiDung";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaNguoiDung", thongTinCaNhan.MaNguoiDung);
            command.Parameters.AddWithValue("@HoTen", thongTinCaNhan.HoTen);
            command.Parameters.AddWithValue("@GioiTinh", thongTinCaNhan.GioiTinh);
            command.Parameters.AddWithValue("@NgaySinh", thongTinCaNhan.NgaySinh.HasValue ? (object)thongTinCaNhan.NgaySinh.Value : DBNull.Value);
            command.Parameters.AddWithValue("@Email", thongTinCaNhan.Email);
            command.Parameters.AddWithValue("@SDT", thongTinCaNhan.SDT);
            command.Parameters.AddWithValue("@DiaChi", thongTinCaNhan.DiaChi);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Xóa thông tin cá nhân
    public bool Delete(string maNguoiDung)
    {
        string query = "DELETE FROM ThongTinCaNhan WHERE MaNguoiDung = @MaNguoiDung";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Kiểm tra xem mã người dùng đã tồn tại chưa
    public bool CheckIfCodeExists(string maNguoiDung)
    {
        string query = "SELECT COUNT(1) FROM ThongTinCaNhan WHERE MaNguoiDung = @MaNguoiDung";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);
            connection.Open();
            return (int)command.ExecuteScalar() > 0; // Nếu kết quả > 0, mã đã tồn tại
        }
    }
}
