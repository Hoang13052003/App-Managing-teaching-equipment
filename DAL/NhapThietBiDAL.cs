using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class NhapThietBiDAL : DatabaseHelper
{
    // Lấy tất cả bản ghi nhập thiết bị
    public List<NhapThietBiDTO> GetAll()
    {
        List<NhapThietBiDTO> list = new List<NhapThietBiDTO>();
        string query = "SELECT * FROM NhapThietBi";
        DataTable dataTable = GetDataTable(query);

        foreach (DataRow row in dataTable.Rows)
        {
            list.Add(new NhapThietBiDTO
            {
                MaNhap = Convert.ToInt32(row["MaNhap"]),
                MaNguoiDung = row["MaNguoiDung"].ToString(),
                NgayNhap = row["NgayNhap"] as DateTime?,
                SoLuong = Convert.ToInt32(row["SoLuong"]),
                TongTien = Convert.ToSingle(row["TongTien"])
            });
        }
        return list;
    }

    // Lấy bản ghi nhập thiết bị theo mã
    public NhapThietBiDTO GetByID(int maNhap)
    {
        string query = "SELECT * FROM NhapThietBi WHERE MaNhap = @MaNhap";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaNhap", maNhap);
            connection.Open();
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }

            if (dataTable.Rows.Count == 1)
            {
                DataRow row = dataTable.Rows[0];
                return new NhapThietBiDTO
                {
                    MaNhap = Convert.ToInt32(row["MaNhap"]),
                    MaNguoiDung = row["MaNguoiDung"].ToString(),
                    NgayNhap = row["NgayNhap"] as DateTime?,
                    SoLuong = Convert.ToInt32(row["SoLuong"]),
                    TongTien = Convert.ToSingle(row["TongTien"])
                };
            }
        }
        return null;
    }

    // Thêm bản ghi nhập thiết bị
    public bool Insert(NhapThietBiDTO nhapThietBi)
    {
        string query = "INSERT INTO NhapThietBi (MaNguoiDung, NgayNhap, SoLuong, TongTien) VALUES (@MaNguoiDung, @NgayNhap, @SoLuong, @TongTien)";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaNguoiDung", nhapThietBi.MaNguoiDung);
            command.Parameters.AddWithValue("@NgayNhap", nhapThietBi.NgayNhap.HasValue ? (object)nhapThietBi.NgayNhap.Value : DBNull.Value);
            command.Parameters.AddWithValue("@SoLuong", nhapThietBi.SoLuong);
            command.Parameters.AddWithValue("@TongTien", nhapThietBi.TongTien);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Cập nhật bản ghi nhập thiết bị
    public bool Update(NhapThietBiDTO nhapThietBi)
    {
        string query = "UPDATE NhapThietBi SET MaNguoiDung = @MaNguoiDung, NgayNhap = @NgayNhap, SoLuong = @SoLuong, TongTien = @TongTien WHERE MaNhap = @MaNhap";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaNhap", nhapThietBi.MaNhap);
            command.Parameters.AddWithValue("@MaNguoiDung", nhapThietBi.MaNguoiDung);
            command.Parameters.AddWithValue("@NgayNhap", nhapThietBi.NgayNhap.HasValue ? (object)nhapThietBi.NgayNhap.Value : DBNull.Value);
            command.Parameters.AddWithValue("@SoLuong", nhapThietBi.SoLuong);
            command.Parameters.AddWithValue("@TongTien", nhapThietBi.TongTien);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Xóa bản ghi nhập thiết bị
    public bool Delete(int maNhap)
    {
        string query = "DELETE FROM NhapThietBi WHERE MaNhap = @MaNhap";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaNhap", maNhap);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Lấy tất cả chi tiết nhập theo mã nhập
    public List<ChiTietNhapDTO> GetAll(int maNhap)
    {
        List<ChiTietNhapDTO> list = new List<ChiTietNhapDTO>();
        string query = "SELECT * FROM ChiTietNhap WHERE MaNhap = @MaNhap";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaNhap", maNhap);
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }

            foreach (DataRow row in dataTable.Rows)
            {
                list.Add(new ChiTietNhapDTO
                {
                    MaNhap = Convert.ToInt32(row["MaNhap"]),
                    MaCTTB_NCC = Convert.ToInt32(row["MaCTTB_NCC"]),
                    GiaNhap = Convert.ToSingle(row["GiaNhap"]),
                    SoLuong = Convert.ToInt32(row["SoLuong"]),
                    ThanhTien = Convert.ToSingle(row["ThanhTien"])
                });
            }
        }
        return list;
    }

    // Thêm chi tiết nhập
    public bool Insert(ChiTietNhapDTO chiTietNhap)
    {
        string query = "INSERT INTO ChiTietNhap (MaNhap, MaCTTB_NCC, GiaNhap, SoLuong, ThanhTien) VALUES (@MaNhap, @MaCTTB_NCC, @GiaNhap, @SoLuong, @ThanhTien)";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaNhap", chiTietNhap.MaNhap);
            command.Parameters.AddWithValue("@MaCTTB_NCC", chiTietNhap.MaCTTB_NCC);
            command.Parameters.AddWithValue("@GiaNhap", chiTietNhap.GiaNhap);
            command.Parameters.AddWithValue("@SoLuong", chiTietNhap.SoLuong);
            command.Parameters.AddWithValue("@ThanhTien", chiTietNhap.ThanhTien);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Xóa chi tiết nhập theo mã nhập và mã thiết bị nhà cung cấp
    public bool Delete(int maNhap, int maCTTB_NCC)
    {
        string query = "DELETE FROM ChiTietNhap WHERE MaNhap = @MaNhap AND MaCTTB_NCC = @MaCTTB_NCC";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaNhap", maNhap);
            command.Parameters.AddWithValue("@MaCTTB_NCC", maCTTB_NCC);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }
}
