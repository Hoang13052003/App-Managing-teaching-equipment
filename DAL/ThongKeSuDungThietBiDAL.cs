using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class ThongKeSuDungThietBiDAL : DatabaseHelper
{
    // Lấy tất cả bản ghi thống kê sử dụng thiết bị
    public List<ThongKeSuDungThietBiDTO> GetAll()
    {
        List<ThongKeSuDungThietBiDTO> list = new List<ThongKeSuDungThietBiDTO>();
        string query = "SELECT * FROM ThongKeSuDungThietBi";
        DataTable dataTable = GetDataTable(query);

        foreach (DataRow row in dataTable.Rows)
        {
            list.Add(new ThongKeSuDungThietBiDTO
            {
                MaBC = Convert.ToInt32(row["MaBC"]),
                MaCTTB_NCC = Convert.ToInt32(row["MaCTTB_NCC"]),
                SoLanMuon = Convert.ToInt32(row["SoLanMuon"]),
                NgaySuDungGanNhat = row["NgaySuDungGanNhat"] as DateTime?
            });
        }
        return list;
    }

    // Lấy thống kê sử dụng thiết bị theo mã báo cáo
    public ThongKeSuDungThietBiDTO GetByID(int maBC)
    {
        string query = "SELECT * FROM ThongKeSuDungThietBi WHERE MaBC = @MaBC";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaBC", maBC);
            connection.Open();
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }

            if (dataTable.Rows.Count == 1)
            {
                DataRow row = dataTable.Rows[0];
                return new ThongKeSuDungThietBiDTO
                {
                    MaBC = Convert.ToInt32(row["MaBC"]),
                    MaCTTB_NCC = Convert.ToInt32(row["MaCTTB_NCC"]),
                    SoLanMuon = Convert.ToInt32(row["SoLanMuon"]),
                    NgaySuDungGanNhat = row["NgaySuDungGanNhat"] as DateTime?
                };
            }
        }
        return null;
    }

    // Thêm thống kê sử dụng thiết bị
    public bool Insert(ThongKeSuDungThietBiDTO thongKe)
    {
        string query = "INSERT INTO ThongKeSuDungThietBi (MaCTTB_NCC, SoLanMuon, NgaySuDungGanNhat) VALUES (@MaCTTB_NCC, @SoLanMuon, @NgaySuDungGanNhat)";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaCTTB_NCC", thongKe.MaCTTB_NCC);
            command.Parameters.AddWithValue("@SoLanMuon", thongKe.SoLanMuon);
            command.Parameters.AddWithValue("@NgaySuDungGanNhat", thongKe.NgaySuDungGanNhat.HasValue ? (object)thongKe.NgaySuDungGanNhat.Value : DBNull.Value);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Cập nhật thống kê sử dụng thiết bị
    public bool Update(ThongKeSuDungThietBiDTO thongKe)
    {
        string query = "UPDATE ThongKeSuDungThietBi SET MaCTTB_NCC = @MaCTTB_NCC, SoLanMuon = @SoLanMuon, NgaySuDungGanNhat = @NgaySuDungGanNhat WHERE MaBC = @MaBC";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaBC", thongKe.MaBC);
            command.Parameters.AddWithValue("@MaCTTB_NCC", thongKe.MaCTTB_NCC);
            command.Parameters.AddWithValue("@SoLanMuon", thongKe.SoLanMuon);
            command.Parameters.AddWithValue("@NgaySuDungGanNhat", thongKe.NgaySuDungGanNhat.HasValue ? (object)thongKe.NgaySuDungGanNhat.Value : DBNull.Value);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Xóa thống kê sử dụng thiết bị
    public bool Delete(int maBC)
    {
        string query = "DELETE FROM ThongKeSuDungThietBi WHERE MaBC = @MaBC";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaBC", maBC);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Tìm kiếm thống kê sử dụng thiết bị theo từ khóa (full-text search)
    public List<ThongKeSuDungThietBiDTO> Search(string keyword)
    {
        List<ThongKeSuDungThietBiDTO> list = new List<ThongKeSuDungThietBiDTO>();
        string query = "SELECT * FROM ThongKeSuDungThietBi WHERE CONTAINS((MaCTTB_NCC, SoLanMuon, NgaySuDungGanNhat), @Keyword)";
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
                list.Add(new ThongKeSuDungThietBiDTO
                {
                    MaBC = Convert.ToInt32(row["MaBC"]),
                    MaCTTB_NCC = Convert.ToInt32(row["MaCTTB_NCC"]),
                    SoLanMuon = Convert.ToInt32(row["SoLanMuon"]),
                    NgaySuDungGanNhat = row["NgaySuDungGanNhat"] as DateTime?
                });
            }
        }
        return list;
    }
}
