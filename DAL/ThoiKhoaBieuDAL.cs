using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class ThoiKhoaBieuDAL : DatabaseHelper
{
    // Lấy tất cả thời khóa biểu
    public List<ThoiKhoaBieuDTO> GetAll()
    {
        List<ThoiKhoaBieuDTO> list = new List<ThoiKhoaBieuDTO>();
        string query = "SELECT * FROM ThoiKhoaBieu";
        DataTable dataTable = GetDataTable(query);

        foreach (DataRow row in dataTable.Rows)
        {
            list.Add(new ThoiKhoaBieuDTO
            {
                MaTKB = Convert.ToInt32(row["MaTKB"]),
                MaNguoiDung = row["MaNguoiDung"].ToString(),
                MaMon = row["MaMon"] as int?,
                MaBaiHoc = row["MaBaiHoc"] as int?,
                MaPhong = row["MaPhong"] as int?,
                MaLopHoc = row["MaLopHoc"] as int?,
                GioHoc = row["GioHoc"] as TimeSpan?,
                NgayHoc = row["NgayHoc"] as DateTime?
            });
        }
        return list;
    }
    public List<ThoiKhoaBieuChiTietDTO> GetThoiKhoaBieuByUser(string maNguoiDung)
    {
        // Khởi tạo danh sách kết quả
        List<ThoiKhoaBieuChiTietDTO> list = new List<ThoiKhoaBieuChiTietDTO>();

        // Câu truy vấn SQL để lấy thông tin thời khóa biểu của người dùng
        string query = @"
        SELECT 
            tkb.MaTKB,
            tkb.MaNguoiDung,
            mh.TenMon,
            bh.TenBaiHoc,
            ph.TenPhong,
            lh.TenLopHoc,
            tkb.GioHoc,
            tkb.NgayHoc
        FROM ThoiKhoaBieu tkb
        LEFT JOIN MonHoc mh ON tkb.MaMon = mh.MaMon
        LEFT JOIN BaiHoc bh ON tkb.MaBaiHoc = bh.MaBaiHoc
        LEFT JOIN PhongHoc ph ON tkb.MaPhong = ph.MaPhong
        LEFT JOIN LopHoc lh ON tkb.MaLopHoc = lh.MaLopHoc
        WHERE tkb.MaNguoiDung = @MaNguoiDung";

        SqlParameter[] parameters = new SqlParameter[]
        {
        new SqlParameter("@MaNguoiDung", maNguoiDung)
        };

        DataTable dataTable = GetDataTableQuery(query, parameters);

        // Kiểm tra nếu DataTable không có dữ liệu
        if (dataTable != null && dataTable.Rows.Count > 0)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                list.Add(new ThoiKhoaBieuChiTietDTO
                {
                    MaTKB = Convert.ToInt32(row["MaTKB"]),
                    MaNguoiDung = row["MaNguoiDung"].ToString(),
                    TenMonHoc = row["TenMon"] != DBNull.Value ? row["TenMon"].ToString() : string.Empty,
                    TenBaiHoc = row["TenBaiHoc"] != DBNull.Value ? row["TenBaiHoc"].ToString() : string.Empty,
                    TenPhong = row["TenPhong"] != DBNull.Value ? row["TenPhong"].ToString() : string.Empty,
                    TenLop = row["TenLopHoc"] != DBNull.Value ? row["TenLopHoc"].ToString() : string.Empty,
                    GioHoc = row["GioHoc"] != DBNull.Value ? (TimeSpan?)row["GioHoc"] : null,
                    NgayHoc = row["NgayHoc"] != DBNull.Value ? (DateTime?)row["NgayHoc"] : null
                });
            }
        }
        else
        {
            list = new List<ThoiKhoaBieuChiTietDTO>();
        }

        return list;
    }

    public List<ThoiKhoaBieuChiTietDTO> GetThoiKhoaBieuByUserAndDate(string maNguoiDung, DateTime startDate, DateTime endDate)
    {
        List<ThoiKhoaBieuChiTietDTO> list = new List<ThoiKhoaBieuChiTietDTO>();
        string query = "SELECT * FROM ThoiKhoaBieu WHERE MaNguoiDung = @MaNguoiDung AND NgayHoc BETWEEN @StartDate AND @EndDate";

        // Thêm logic để truyền tham số cho câu truy vấn SQL

        DataTable dataTable = GetDataTable(query);

        foreach (DataRow row in dataTable.Rows)
        {
            list.Add(new ThoiKhoaBieuChiTietDTO
            {
                MaTKB = Convert.ToInt32(row["MaTKB"]),
                MaNguoiDung = row["MaNguoiDung"].ToString(),
                TenMonHoc = row["TenMonHoc"].ToString(),
                TenBaiHoc = row["TenBaiHoc"].ToString(),
                TenPhong = row["TenPhong"].ToString(),
                TenLop = row["TenLop"].ToString(),
                GioHoc = row["GioHoc"] as TimeSpan?,
                NgayHoc = row["NgayHoc"] as DateTime?
            });
        }
        return list;
    }

    // Lấy thời khóa biểu theo mã
    public ThoiKhoaBieuDTO GetByID(int maTKB)
    {
        string query = "SELECT * FROM ThoiKhoaBieu WHERE MaTKB = @MaTKB";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaTKB", maTKB);
            connection.Open();
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }

            if (dataTable.Rows.Count == 1)
            {
                DataRow row = dataTable.Rows[0];
                return new ThoiKhoaBieuDTO
                {
                    MaTKB = Convert.ToInt32(row["MaTKB"]),
                    MaNguoiDung = row["MaNguoiDung"].ToString(),
                    MaMon = row["MaMon"] as int?,
                    MaBaiHoc = row["MaBaiHoc"] as int?,
                    MaPhong = row["MaPhong"] as int?,
                    MaLopHoc = row["MaLopHoc"] as int?,
                    GioHoc = row["GioHoc"] as TimeSpan?,
                    NgayHoc = row["NgayHoc"] as DateTime?
                };
            }
        }
        return null;
    }

    // Thêm thời khóa biểu
    public bool Insert(ThoiKhoaBieuDTO tkb)
    {
        string query = "INSERT INTO ThoiKhoaBieu (MaNguoiDung, MaMon, MaBaiHoc, MaPhong, MaLopHoc, GioHoc, NgayHoc) VALUES (@MaNguoiDung, @MaMon, @MaBaiHoc, @MaPhong, @MaLopHoc, @GioHoc, @NgayHoc)";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaNguoiDung", tkb.MaNguoiDung);
            command.Parameters.AddWithValue("@MaMon", tkb.MaMon.HasValue ? (object)tkb.MaMon.Value : DBNull.Value);
            command.Parameters.AddWithValue("@MaBaiHoc", tkb.MaBaiHoc.HasValue ? (object)tkb.MaBaiHoc.Value : DBNull.Value);
            command.Parameters.AddWithValue("@MaPhong", tkb.MaPhong.HasValue ? (object)tkb.MaPhong.Value : DBNull.Value);
            command.Parameters.AddWithValue("@MaLopHoc", tkb.MaLopHoc.HasValue ? (object)tkb.MaLopHoc.Value : DBNull.Value);
            command.Parameters.AddWithValue("@GioHoc", tkb.GioHoc.HasValue ? (object)tkb.GioHoc.Value : DBNull.Value);
            command.Parameters.AddWithValue("@NgayHoc", tkb.NgayHoc.HasValue ? (object)tkb.NgayHoc.Value : DBNull.Value);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Cập nhật thời khóa biểu
    public bool Update(ThoiKhoaBieuDTO tkb)
    {
        string query = "UPDATE ThoiKhoaBieu SET MaNguoiDung = @MaNguoiDung, MaMon = @MaMon, MaBaiHoc = @MaBaiHoc, MaPhong = @MaPhong, MaLopHoc = @MaLopHoc, GioHoc = @GioHoc, NgayHoc = @NgayHoc WHERE MaTKB = @MaTKB";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaTKB", tkb.MaTKB);
            command.Parameters.AddWithValue("@MaNguoiDung", tkb.MaNguoiDung);
            command.Parameters.AddWithValue("@MaMon", tkb.MaMon.HasValue ? (object)tkb.MaMon.Value : DBNull.Value);
            command.Parameters.AddWithValue("@MaBaiHoc", tkb.MaBaiHoc.HasValue ? (object)tkb.MaBaiHoc.Value : DBNull.Value);
            command.Parameters.AddWithValue("@MaPhong", tkb.MaPhong.HasValue ? (object)tkb.MaPhong.Value : DBNull.Value);
            command.Parameters.AddWithValue("@MaLopHoc", tkb.MaLopHoc.HasValue ? (object)tkb.MaLopHoc.Value : DBNull.Value);
            command.Parameters.AddWithValue("@GioHoc", tkb.GioHoc.HasValue ? (object)tkb.GioHoc.Value : DBNull.Value);
            command.Parameters.AddWithValue("@NgayHoc", tkb.NgayHoc.HasValue ? (object)tkb.NgayHoc.Value : DBNull.Value);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Xóa thời khóa biểu
    public bool Delete(int maTKB)
    {
        string query = "DELETE FROM ThoiKhoaBieu WHERE MaTKB = @MaTKB";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaTKB", maTKB);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Tìm kiếm thời khóa biểu theo từ khóa (full-text search)
    public List<ThoiKhoaBieuDTO> Search(string keyword)
    {
        List<ThoiKhoaBieuDTO> list = new List<ThoiKhoaBieuDTO>();
        string query = "SELECT * FROM ThoiKhoaBieu WHERE CONTAINS((MaNguoiDung, MaMon, MaBaiHoc, MaPhong, MaLopHoc), @Keyword)";
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
                list.Add(new ThoiKhoaBieuDTO
                {
                    MaTKB = Convert.ToInt32(row["MaTKB"]),
                    MaNguoiDung = row["MaNguoiDung"].ToString(),
                    MaMon = row["MaMon"] as int?,
                    MaBaiHoc = row["MaBaiHoc"] as int?,
                    MaPhong = row["MaPhong"] as int?,
                    MaLopHoc = row["MaLopHoc"] as int?,
                    GioHoc = row["GioHoc"] as TimeSpan?,
                    NgayHoc = row["NgayHoc"] as DateTime?
                });
            }
        }
        return list;
    }
}
