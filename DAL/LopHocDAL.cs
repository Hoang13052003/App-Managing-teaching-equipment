using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class LopHocDAL : DatabaseHelper
{
    public List<LopHocDTO> GetAll()
    {
        List<LopHocDTO> list = new List<LopHocDTO>();
        string query = "SELECT * FROM LopHoc";
        DataTable dataTable = GetDataTable(query);

        foreach (DataRow row in dataTable.Rows)
        {
            list.Add(new LopHocDTO
            {
                MaLopHoc = Convert.ToInt32(row["MaLopHoc"]),
                TenLopHoc = row["TenLopHoc"].ToString(),
                SiSo = Convert.ToInt32(row["SiSo"])
            });
        }
        return list;
    }

    public LopHocDTO GetByID(int maLopHoc)
    {
        string query = "SELECT * FROM LopHoc WHERE MaLopHoc = @MaLopHoc";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaLopHoc", maLopHoc);
            connection.Open();
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }

            if (dataTable.Rows.Count == 1)
            {
                DataRow row = dataTable.Rows[0];
                return new LopHocDTO
                {
                    MaLopHoc = Convert.ToInt32(row["MaLopHoc"]),
                    TenLopHoc = row["TenLopHoc"].ToString(),
                    SiSo = Convert.ToInt32(row["SiSo"])
                };
            }
        }
        return null;
    }

    public bool Insert(LopHocDTO lopHoc)
    {
        string query = "INSERT INTO LopHoc (TenLopHoc, SiSo) VALUES (@TenLopHoc, @SiSo)";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TenLopHoc", lopHoc.TenLopHoc);
            command.Parameters.AddWithValue("@SiSo", lopHoc.SiSo);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    public bool Update(LopHocDTO lopHoc)
    {
        string query = "UPDATE LopHoc SET TenLopHoc = @TenLopHoc, SiSo = @SiSo WHERE MaLopHoc = @MaLopHoc";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaLopHoc", lopHoc.MaLopHoc);
            command.Parameters.AddWithValue("@TenLopHoc", lopHoc.TenLopHoc);
            command.Parameters.AddWithValue("@SiSo", lopHoc.SiSo);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    public bool Delete(int maLopHoc)
    {
        string query = "DELETE FROM LopHoc WHERE MaLopHoc = @MaLopHoc";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaLopHoc", maLopHoc);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    public List<LopHocDTO> Search(string keyword)
    {
        List<LopHocDTO> list = new List<LopHocDTO>();
        string query = "SELECT * FROM LopHoc WHERE CONTAINS(TenLopHoc, @Keyword)";
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
                list.Add(new LopHocDTO
                {
                    MaLopHoc = Convert.ToInt32(row["MaLopHoc"]),
                    TenLopHoc = row["TenLopHoc"].ToString(),
                    SiSo = Convert.ToInt32(row["SiSo"])
                });
            }
        }
        return list;
    }
}
