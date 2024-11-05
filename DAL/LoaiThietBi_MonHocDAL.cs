using DTO;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class LoaiThietBi_MonHocDAL : DatabaseHelper
{
    public bool LuuMonHocLoaiTB(int maMH, int maLoaiTB)
    {
        string query = "INSERT INTO LoaiThietBi_MonHoc (MaLoai, MaMon) VALUES (@MaLoaiTB, @MaMH)";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            // Sử dụng đúng tên tham số
            command.Parameters.AddWithValue("@MaLoaiTB", maLoaiTB);
            command.Parameters.AddWithValue("@MaMH", maMH);

            connection.Open();
            return command.ExecuteNonQuery() > 0; // Trả về true nếu lưu thành công
        }
    }

    public DataTable LayDanhSachMonHocLoaiTB()
    {
        DataTable dataTable = new DataTable();
        string query = @"
        SELECT L.MaMon, M.TenMon, LT.MaLoai, LT.TenLoai
        FROM LoaiThietBi_MonHoc L
        INNER JOIN MonHoc M ON L.MaMon = M.MaMon
        INNER JOIN LoaiThietBi LT ON L.MaLoai = LT.MaLoai";

        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dataTable); // Đổ dữ liệu vào DataTable
        }
        return dataTable;
    }
    public bool XoaLTB_MH(int maLoai, int maMon)
    {
        string query = "DELETE FROM LoaiThietBi_MonHoc WHERE MaLoai = @MaLoai AND MaMon = @MaMon";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaLoai", maLoai);
            command.Parameters.AddWithValue("@MaMon", maMon);

            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

}
