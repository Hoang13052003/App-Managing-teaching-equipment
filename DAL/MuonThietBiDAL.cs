using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class MuonThietBiDAL : DatabaseHelper
{
    // Lấy tất cả bản ghi mượn thiết bị
    public List<MuonThietBiDTO> GetAll()
    {
        List<MuonThietBiDTO> list = new List<MuonThietBiDTO>();
        string query = "SELECT * FROM MuonThietBi";
        DataTable dataTable = GetDataTable(query);

        foreach (DataRow row in dataTable.Rows)
        {
            list.Add(new MuonThietBiDTO
            {
                MaMuon = Convert.ToInt32(row["MaMuon"]),
                MaNguoiDung = row["MaNguoiDung"].ToString(),
                MaPhong = Convert.ToInt32(row["MaPhong"])
            });
        }
        return list;
    }

    // Lấy bản ghi mượn thiết bị theo mã mượn
    public MuonThietBiDTO GetByID(int maMuon)
    {
        string query = "SELECT * FROM MuonThietBi WHERE MaMuon = @MaMuon";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaMuon", maMuon);
            connection.Open();
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }

            if (dataTable.Rows.Count == 1)
            {
                DataRow row = dataTable.Rows[0];
                return new MuonThietBiDTO
                {
                    MaMuon = Convert.ToInt32(row["MaMuon"]),
                    MaNguoiDung = row["MaNguoiDung"].ToString(),
                    MaPhong = Convert.ToInt32(row["MaPhong"])
                };
            }
        }
        return null;
    }

    // Thêm bản ghi mượn thiết bị
    public bool Insert(MuonThietBiDTO muonThietBi)
    {
        string query = "INSERT INTO MuonThietBi (MaNguoiDung, MaPhong) VALUES (@MaNguoiDung, @MaPhong)";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaNguoiDung", muonThietBi.MaNguoiDung);
            command.Parameters.AddWithValue("@MaPhong", muonThietBi.MaPhong);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Cập nhật bản ghi mượn thiết bị
    public bool Update(MuonThietBiDTO muonThietBi)
    {
        string query = "UPDATE MuonThietBi SET MaNguoiDung = @MaNguoiDung, MaPhong = @MaPhong WHERE MaMuon = @MaMuon";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaMuon", muonThietBi.MaMuon);
            command.Parameters.AddWithValue("@MaNguoiDung", muonThietBi.MaNguoiDung);
            command.Parameters.AddWithValue("@MaPhong", muonThietBi.MaPhong);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Xóa bản ghi mượn thiết bị
    public bool Delete(int maMuon)
    {
        string query = "DELETE FROM MuonThietBi WHERE MaMuon = @MaMuon";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaMuon", maMuon);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Tìm kiếm bản ghi mượn thiết bị theo từ khóa
    public List<MuonThietBiDTO> Search(string keyword)
    {
        List<MuonThietBiDTO> list = new List<MuonThietBiDTO>();
        string query = "SELECT * FROM MuonThietBi WHERE CONTAINS(MaNguoiDung, @Keyword) OR CONTAINS(MaPhong, @Keyword)";
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
                list.Add(new MuonThietBiDTO
                {
                    MaMuon = Convert.ToInt32(row["MaMuon"]),
                    MaNguoiDung = row["MaNguoiDung"].ToString(),
                    MaPhong = Convert.ToInt32(row["MaPhong"])
                });
            }
        }
        return list;
    }
}
