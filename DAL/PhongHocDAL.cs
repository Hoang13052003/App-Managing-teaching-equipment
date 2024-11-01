using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class PhongHocDAL : DatabaseHelper
{
    // Lấy tất cả phòng học
    public List<PhongHocDTO> GetAll()
    {
        List<PhongHocDTO> list = new List<PhongHocDTO>();
        string query = "SELECT * FROM PhongHoc";
        DataTable dataTable = GetDataTable(query);

        foreach (DataRow row in dataTable.Rows)
        {
            list.Add(new PhongHocDTO
            {
                MaPhong = Convert.ToInt32(row["MaPhong"]),
                TenPhong = row["TenPhong"].ToString()
            });
        }
        return list;
    }

    // Lấy phòng học theo mã
    public PhongHocDTO GetByID(int maPhong)
    {
        string query = "SELECT * FROM PhongHoc WHERE MaPhong = @MaPhong";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaPhong", maPhong);
            connection.Open();
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }

            if (dataTable.Rows.Count == 1)
            {
                DataRow row = dataTable.Rows[0];
                return new PhongHocDTO
                {
                    MaPhong = Convert.ToInt32(row["MaPhong"]),
                    TenPhong = row["TenPhong"].ToString()
                };
            }
        }
        return null;
    }

    // Thêm phòng học
    public bool Insert(PhongHocDTO phongHoc)
    {
        string query = "INSERT INTO PhongHoc (TenPhong) VALUES (@TenPhong)";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TenPhong", phongHoc.TenPhong);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Cập nhật phòng học
    public bool Update(PhongHocDTO phongHoc)
    {
        string query = "UPDATE PhongHoc SET TenPhong = @TenPhong WHERE MaPhong = @MaPhong";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaPhong", phongHoc.MaPhong);
            command.Parameters.AddWithValue("@TenPhong", phongHoc.TenPhong);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Xóa phòng học
    public bool Delete(int maPhong)
    {
        string query = "DELETE FROM PhongHoc WHERE MaPhong = @MaPhong";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaPhong", maPhong);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Tìm kiếm phòng học theo tên (sử dụng full-text search)
    public List<PhongHocDTO> Search(string keyword)
    {
        List<PhongHocDTO> list = new List<PhongHocDTO>();
        string query = "SELECT * FROM PhongHoc WHERE CONTAINS(TenPhong, @Keyword)";
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
                list.Add(new PhongHocDTO
                {
                    MaPhong = Convert.ToInt32(row["MaPhong"]),
                    TenPhong = row["TenPhong"].ToString()
                });
            }
        }
        return list;
    }
}
