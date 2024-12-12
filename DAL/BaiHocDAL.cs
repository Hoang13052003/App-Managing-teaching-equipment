using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class BaiHocDAL : DatabaseHelper
{
    // Lấy tất cả bài học
    public List<BaiHocDTO> GetAll()
    {
        List<BaiHocDTO> list = new List<BaiHocDTO>();
        string query = "SELECT * FROM BaiHoc";
        DataTable dataTable = GetDataTable(query);

        foreach (DataRow row in dataTable.Rows)
        {
            list.Add(new BaiHocDTO
            {
                MaBaiHoc = Convert.ToInt32(row["MaBaiHoc"]),
                TenBaiHoc = row["TenBaiHoc"].ToString(),
                MaMon = Convert.ToInt32(row["MaMon"])
            });
        }
        return list;
    }

    // Lấy bài học theo ID
    public BaiHocDTO GetByID(int maBaiHoc)
    {
        string query = "SELECT * FROM BaiHoc WHERE MaBaiHoc = @MaBaiHoc";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaBaiHoc", maBaiHoc);
            connection.Open();

            DataTable dataTable = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }

            if (dataTable.Rows.Count == 1)
            {
                DataRow row = dataTable.Rows[0];
                return new BaiHocDTO
                {
                    MaBaiHoc = Convert.ToInt32(row["MaBaiHoc"]),
                    TenBaiHoc = row["TenBaiHoc"].ToString(),
                    MaMon = Convert.ToInt32(row["MaMon"])
                };
            }
        }
        return null;
    }
    //public BaiHocDTO GetByIDMonHoc(int maMonHoc)
    //{
    //    string query = "SELECT * FROM BaiHoc WHERE MaMon = @MaMonHoc";
    //    using (SqlConnection connection = GetConnection())
    //    {
    //        SqlCommand command = new SqlCommand(query, connection);
    //        command.Parameters.AddWithValue("@MaMonHoc", maMonHoc);
    //        connection.Open();

    //        DataTable dataTable = new DataTable();
    //        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
    //        {
    //            adapter.Fill(dataTable);
    //        }

    //        if (dataTable.Rows.Count == 1)
    //        {
    //            DataRow row = dataTable.Rows[0];
    //            return new BaiHocDTO
    //            {
    //                MaBaiHoc = Convert.ToInt32(row["MaBaiHoc"]),
    //                TenBaiHoc = row["TenBaiHoc"].ToString(),
    //                MaMon = Convert.ToInt32(row["MaMon"])
    //            };
    //        }
    //    }
    //    return null;
    //}
    // Thêm bài học
    public bool Insert(BaiHocDTO baiHoc)
    {
        string query = "INSERT INTO BaiHoc (TenBaiHoc, MaMon) VALUES (@TenBaiHoc, @MaMon)";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TenBaiHoc", baiHoc.TenBaiHoc);
            command.Parameters.AddWithValue("@MaMon", baiHoc.MaMon);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Cập nhật bài học
    public bool Update(BaiHocDTO baiHoc)
    {
        string query = "UPDATE BaiHoc SET TenBaiHoc = @TenBaiHoc, MaMon = @MaMon WHERE MaBaiHoc = @MaBaiHoc";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaBaiHoc", baiHoc.MaBaiHoc);
            command.Parameters.AddWithValue("@TenBaiHoc", baiHoc.TenBaiHoc);
            command.Parameters.AddWithValue("@MaMon", baiHoc.MaMon);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Xóa bài học
    public bool Delete(int maBaiHoc)
    {
        string query = "DELETE FROM BaiHoc WHERE MaBaiHoc = @MaBaiHoc";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaBaiHoc", maBaiHoc);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }
}
