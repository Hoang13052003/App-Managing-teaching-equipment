using DTO; // Đảm bảo đã có DTO cho BienBanXuLy
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class BienBanXuLyDAL : DatabaseHelper
{
    // Lấy tất cả biên bản xử lý
    public List<BienBanXuLyDTO> GetAll()
    {
        List<BienBanXuLyDTO> list = new List<BienBanXuLyDTO>();
        string query = "SELECT * FROM BienBanXuLy";
        DataTable dataTable = GetDataTable(query);

        foreach (DataRow row in dataTable.Rows)
        {
            list.Add(new BienBanXuLyDTO
            {
                MaBB = Convert.ToInt32(row["MaBB"]),
                MaCTTB_NCC = Convert.ToInt32(row["MaCTTB_NCC"]),
                TenNguoiLamHong = row["TenNguoiLamHong"].ToString(),
                VaiTro = row["VaiTro"].ToString(),
                ThoiGianLamHong = Convert.ToDateTime(row["ThoiGianLamHong"]),
                MoTaChiTiet = row["MoTaChiTiet"].ToString(),
                ChiPhiSuaChua = row["ChiPhiSuaChua"] != DBNull.Value ? Convert.ToDouble(row["ChiPhiSuaChua"]) : 0,
                TinhTrang = Convert.ToInt32(row["TinhTrang"])
            });
        }
        return list;
    }

    // Lấy biên bản xử lý theo mã
    public BienBanXuLyDTO GetByID(int maBB)
    {
        string query = "SELECT * FROM BienBanXuLy WHERE MaBB = @MaBB";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaBB", maBB);
            connection.Open();
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }

            if (dataTable.Rows.Count == 1)
            {
                DataRow row = dataTable.Rows[0];
                return new BienBanXuLyDTO
                {
                    MaBB = Convert.ToInt32(row["MaBB"]),
                    MaCTTB_NCC = Convert.ToInt32(row["MaCTTB_NCC"]),
                    TenNguoiLamHong = row["TenNguoiLamHong"].ToString(),
                    VaiTro = row["VaiTro"].ToString(),
                    ThoiGianLamHong = Convert.ToDateTime(row["ThoiGianLamHong"]),
                    MoTaChiTiet = row["MoTaChiTiet"].ToString(),
                    ChiPhiSuaChua = row["ChiPhiSuaChua"] != DBNull.Value ? Convert.ToDouble(row["ChiPhiSuaChua"]) : 0,
                    TinhTrang = Convert.ToInt32(row["TinhTrang"])
                };
            }
        }
        return null;
    }

    // Thêm biên bản xử lý
    public bool Insert(BienBanXuLyDTO bienBan)
    {
        string query = "INSERT INTO BienBanXuLy (MaCTTB_NCC, TenNguoiLamHong, VaiTro, ThoiGianLamHong, MoTaChiTiet, ChiPhiSuaChua, TinhTrang) " +
                       "VALUES (@MaCTTB_NCC, @TenNguoiLamHong, @VaiTro, @ThoiGianLamHong, @MoTaChiTiet, @ChiPhiSuaChua, @TinhTrang)";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaCTTB_NCC", bienBan.MaCTTB_NCC);
            command.Parameters.AddWithValue("@TenNguoiLamHong", bienBan.TenNguoiLamHong);
            command.Parameters.AddWithValue("@VaiTro", bienBan.VaiTro);
            command.Parameters.AddWithValue("@ThoiGianLamHong", bienBan.ThoiGianLamHong);
            command.Parameters.AddWithValue("@MoTaChiTiet", bienBan.MoTaChiTiet);
            command.Parameters.AddWithValue("@ChiPhiSuaChua", bienBan.ChiPhiSuaChua);
            command.Parameters.AddWithValue("@TinhTrang", bienBan.TinhTrang);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Cập nhật biên bản xử lý
    public bool Update(BienBanXuLyDTO bienBan)
    {
        string query = "UPDATE BienBanXuLy SET MaCTTB_NCC = @MaCTTB_NCC, TenNguoiLamHong = @TenNguoiLamHong, " +
                       "VaiTro = @VaiTro, ThoiGianLamHong = @ThoiGianLamHong, MoTaChiTiet = @MoTaChiTiet, " +
                       "ChiPhiSuaChua = @ChiPhiSuaChua, TinhTrang = @TinhTrang WHERE MaBB = @MaBB";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaBB", bienBan.MaBB);
            command.Parameters.AddWithValue("@MaCTTB_NCC", bienBan.MaCTTB_NCC);
            command.Parameters.AddWithValue("@TenNguoiLamHong", bienBan.TenNguoiLamHong);
            command.Parameters.AddWithValue("@VaiTro", bienBan.VaiTro);
            command.Parameters.AddWithValue("@ThoiGianLamHong", bienBan.ThoiGianLamHong);
            command.Parameters.AddWithValue("@MoTaChiTiet", bienBan.MoTaChiTiet);
            command.Parameters.AddWithValue("@ChiPhiSuaChua", bienBan.ChiPhiSuaChua);
            command.Parameters.AddWithValue("@TinhTrang", bienBan.TinhTrang);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Xóa biên bản xử lý
    public bool Delete(int maBB)
    {
        string query = "DELETE FROM BienBanXuLy WHERE MaBB = @MaBB";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaBB", maBB);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Tìm kiếm biên bản xử lý theo mô tả chi tiết (Search)
    public List<BienBanXuLyDTO> Search(string searchTerm)
    {
        List<BienBanXuLyDTO> list = new List<BienBanXuLyDTO>();
        string query = "SELECT * FROM BienBanXuLy WHERE CONTAINS(MoTaChiTiet, @SearchTerm)";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@SearchTerm", searchTerm);
            connection.Open();
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }

            foreach (DataRow row in dataTable.Rows)
            {
                list.Add(new BienBanXuLyDTO
                {
                    MaBB = Convert.ToInt32(row["MaBB"]),
                    MaCTTB_NCC = Convert.ToInt32(row["MaCTTB_NCC"]),
                    TenNguoiLamHong = row["TenNguoiLamHong"].ToString(),
                    VaiTro = row["VaiTro"].ToString(),
                    ThoiGianLamHong = Convert.ToDateTime(row["ThoiGianLamHong"]),
                    MoTaChiTiet = row["MoTaChiTiet"].ToString(),
                    ChiPhiSuaChua = row["ChiPhiSuaChua"] != DBNull.Value ? Convert.ToDouble(row["ChiPhiSuaChua"]) : 0,
                    TinhTrang = Convert.ToInt32(row["TinhTrang"])
                });
            }
        }
        return list;
    }
}
