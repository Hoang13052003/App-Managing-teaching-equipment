using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class MonHocDAL : DatabaseHelper
{
    // Lấy tất cả môn học
    public List<MonHocDTO> GetAll()
    {
        List<MonHocDTO> list = new List<MonHocDTO>();
        string query = "SELECT * FROM MonHoc";
        DataTable dataTable = GetDataTable(query);

        foreach (DataRow row in dataTable.Rows)
        {
            list.Add(new MonHocDTO
            {
                MaMon = Convert.ToInt32(row["MaMon"]),
                TenMon = row["TenMon"].ToString()
            });
        }
        return list;
    }

    // Lấy môn học theo ID
    public MonHocDTO GetByID(int maMon)
    {
        string query = "SELECT * FROM MonHoc WHERE MaMon = @MaMon";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaMon", maMon);
            connection.Open();

            DataTable dataTable = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }

            if (dataTable.Rows.Count == 1)
            {
                DataRow row = dataTable.Rows[0];
                return new MonHocDTO
                {
                    MaMon = Convert.ToInt32(row["MaMon"]),
                    TenMon = row["TenMon"].ToString()
                };
            }
        }
        return null;
    }

    // Thêm môn học
    public bool Insert(MonHocDTO monHoc)
    {
        string query = "INSERT INTO MonHoc (TenMon) VALUES (@TenMon)";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TenMon", monHoc.TenMon);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Cập nhật môn học
    public bool Update(MonHocDTO monHoc)
    {
        string query = "UPDATE MonHoc SET TenMon = @TenMon WHERE MaMon = @MaMon";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaMon", monHoc.MaMon);
            command.Parameters.AddWithValue("@TenMon", monHoc.TenMon);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Xóa môn học
    public bool Delete(int maMon)
    {
        string query = "DELETE FROM MonHoc WHERE MaMon = @MaMon";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaMon", maMon);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }


    // Phương thức kiểm tra tên môn học tồn tại
    public bool CheckMonHocExists(string tenMon)
    {
        string query = "SELECT COUNT(*) FROM MonHoc WHERE TenMon = @TenMon";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TenMon", tenMon);
            connection.Open();
            int count = (int)command.ExecuteScalar();
            return count > 0;
        }

    }
}
