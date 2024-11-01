using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class PhanHoiGiaoVienDAL : DatabaseHelper
{
    // Lấy tất cả phản hồi giáo viên
    public List<PhanHoiGiaoVienDTO> GetAll()
    {
        List<PhanHoiGiaoVienDTO> list = new List<PhanHoiGiaoVienDTO>();
        string query = "SELECT * FROM PhanHoiGiaoVien";
        DataTable dataTable = GetDataTable(query);

        foreach (DataRow row in dataTable.Rows)
        {
            list.Add(new PhanHoiGiaoVienDTO
            {
                MaPH = Convert.ToInt32(row["MaPH"]),
                MaNguoiDung = row["MaNguoiDung"].ToString(),
                NoiDung = row["NoiDung"].ToString(),
                NgayPhanHoi = row["NgayPhanHoi"] as DateTime?
            });
        }
        return list;
    }

    // Lấy phản hồi giáo viên theo mã
    public PhanHoiGiaoVienDTO GetByID(int maPH)
    {
        string query = "SELECT * FROM PhanHoiGiaoVien WHERE MaPH = @MaPH";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaPH", maPH);
            connection.Open();
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }

            if (dataTable.Rows.Count == 1)
            {
                DataRow row = dataTable.Rows[0];
                return new PhanHoiGiaoVienDTO
                {
                    MaPH = Convert.ToInt32(row["MaPH"]),
                    MaNguoiDung = row["MaNguoiDung"].ToString(),
                    NoiDung = row["NoiDung"].ToString(),
                    NgayPhanHoi = row["NgayPhanHoi"] as DateTime?
                };
            }
        }
        return null;
    }

    // Thêm phản hồi giáo viên
    public bool Insert(PhanHoiGiaoVienDTO phanHoi)
    {
        string query = "INSERT INTO PhanHoiGiaoVien (MaNguoiDung, NoiDung, NgayPhanHoi) VALUES (@MaNguoiDung, @NoiDung, @NgayPhanHoi)";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaNguoiDung", phanHoi.MaNguoiDung);
            command.Parameters.AddWithValue("@NoiDung", phanHoi.NoiDung);
            command.Parameters.AddWithValue("@NgayPhanHoi", phanHoi.NgayPhanHoi.HasValue ? (object)phanHoi.NgayPhanHoi.Value : DBNull.Value);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Cập nhật phản hồi giáo viên
    public bool Update(PhanHoiGiaoVienDTO phanHoi)
    {
        string query = "UPDATE PhanHoiGiaoVien SET MaNguoiDung = @MaNguoiDung, NoiDung = @NoiDung, NgayPhanHoi = @NgayPhanHoi WHERE MaPH = @MaPH";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaPH", phanHoi.MaPH);
            command.Parameters.AddWithValue("@MaNguoiDung", phanHoi.MaNguoiDung);
            command.Parameters.AddWithValue("@NoiDung", phanHoi.NoiDung);
            command.Parameters.AddWithValue("@NgayPhanHoi", phanHoi.NgayPhanHoi.HasValue ? (object)phanHoi.NgayPhanHoi.Value : DBNull.Value);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Xóa phản hồi giáo viên
    public bool Delete(int maPH)
    {
        string query = "DELETE FROM PhanHoiGiaoVien WHERE MaPH = @MaPH";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaPH", maPH);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }
}
