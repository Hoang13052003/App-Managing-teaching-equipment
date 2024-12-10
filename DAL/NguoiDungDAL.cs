using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class NguoiDungDAL : DatabaseHelper
{
    public List<NguoiDungDTO> GetAll()
    {
        List<NguoiDungDTO> list = new List<NguoiDungDTO>();
        string query = "SELECT * FROM NguoiDung";
        DataTable dataTable = GetDataTable(query);

        foreach (DataRow row in dataTable.Rows)
        {
            list.Add(new NguoiDungDTO
            {
                MaNguoiDung = row["MaNguoiDung"].ToString(),
                TenDangNhap = row["TenDangNhap"].ToString(),
                MatKhau = row["MatKhau"].ToString(),
                TrangThai = (bool)row["TrangThai"]
            });
        }
        return list;
    }

    public NguoiDungDTO GetByID(string maNguoiDung)
    {
        string query = "SELECT * FROM NguoiDung WHERE MaNguoiDung = @MaNguoiDung";
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
                return new NguoiDungDTO
                {
                    MaNguoiDung = row["MaNguoiDung"].ToString(),
                    TenDangNhap = row["TenDangNhap"].ToString(),
                    MatKhau = row["MatKhau"].ToString(),
                    TrangThai = (bool)row["TrangThai"]
                };
            }
        }
        return null;
    }
    public NguoiDungDTO GetByTenDangNhap(string tenDangNhap)
    {
        string query = "SELECT * FROM NguoiDung WHERE TenDangNhap = @TenDangNhap";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
            connection.Open();
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }

            if (dataTable.Rows.Count == 1)
            {
                DataRow row = dataTable.Rows[0];
                return new NguoiDungDTO
                {
                    MaNguoiDung = row["MaNguoiDung"].ToString(),
                    TenDangNhap = row["TenDangNhap"].ToString(),
                    MatKhau = row["MatKhau"].ToString(),
                    TrangThai = (bool)row["TrangThai"]
                };
            }
        }
        return null;
    }
    public bool Insert(NguoiDungDTO nguoiDung)
    {
        // Mã hóa mật khẩu trước khi lưu
        nguoiDung.MatKhau = HashPassword(nguoiDung.MatKhau);
        string query = "INSERT INTO NguoiDung (MaNguoiDung, TenDangNhap, MatKhau, TrangThai) VALUES (@MaNguoiDung, @TenDangNhap, @MatKhau, @TrangThai)";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaNguoiDung", GenerateUniqueUserCode());
            command.Parameters.AddWithValue("@TenDangNhap", nguoiDung.TenDangNhap);
            command.Parameters.AddWithValue("@MatKhau", nguoiDung.MatKhau);
            command.Parameters.AddWithValue("@TrangThai", true);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }

    }
    

    public bool Update(NguoiDungDTO nguoiDung)
    {
        // Mã hóa mật khẩu trước khi lưu
        nguoiDung.MatKhau = HashPassword(nguoiDung.MatKhau);

        string query = "UPDATE NguoiDung SET TenDangNhap = @TenDangNhap, MatKhau = @MatKhau, TrangThai = @TrangThai WHERE MaNguoiDung = @MaNguoiDung";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TenDangNhap", nguoiDung.TenDangNhap);
            command.Parameters.AddWithValue("@MatKhau", nguoiDung.MatKhau);
            command.Parameters.AddWithValue("@TrangThai", true);
            command.Parameters.AddWithValue("@MaNguoiDung", nguoiDung.MaNguoiDung);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }


    public bool Delete(string maNguoiDung)
    {
        string query = "DELETE FROM NguoiDung WHERE MaNguoiDung = @MaNguoiDung";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    public string GenerateUniqueUserCode()
    {
        string newCode;
        do
        {
            newCode = "ND" + new Random().Next(10000000, 99999999); // Sinh mã ngẫu nhiên có dạng ND + 8 chữ số
        } while (CheckIfCodeExists(newCode)); // Kiểm tra mã có trùng hay không

        return newCode;
    }

    private bool CheckIfCodeExists(string code)
    {
        string query = "SELECT COUNT(1) FROM NguoiDung WHERE MaNguoiDung = @MaNguoiDung";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaNguoiDung", code);
            connection.Open();
            return (int)command.ExecuteScalar() > 0; // Nếu kết quả > 0, mã đã tồn tại
        }
    }
    public bool CheckIfUsernameExists(string tenDangNhap)
    {
        string query = "SELECT COUNT(1) FROM NguoiDung WHERE TenDangNhap = @TenDangNhap";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
            connection.Open();
            return (int)command.ExecuteScalar() > 0; // Nếu kết quả > 0, tên đăng nhập đã tồn tại
        }
    }
    private string HashPassword(string password)
    {
        using (var sha256 = System.Security.Cryptography.SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return System.BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}
