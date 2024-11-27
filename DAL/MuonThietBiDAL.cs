using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Windows.Forms;

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
                MaTKB = Convert.ToInt32(row["MaTKB"]),
                NgayMuon = Convert.ToDateTime(row["NgayMuon"]),
                NgayTra = row["NgayTra"] != DBNull.Value ? Convert.ToDateTime(row["NgayTra"]) : (DateTime?)null,
                TinhTrangTraTB = row["TinhTrangTraTB"].ToString(),
                TrangThai = Convert.ToBoolean(row["TrangThai"]),
                GhiChuTraThietBi = row["GhiChuTraThietBi"].ToString()
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
                    MaTKB = Convert.ToInt32(row["MaTKB"]),
                    NgayMuon = Convert.ToDateTime(row["NgayMuon"]),
                    NgayTra = row["NgayTra"] != DBNull.Value ? Convert.ToDateTime(row["NgayTra"]) : (DateTime?)null,
                    TinhTrangTraTB = row["TinhTrangTraTB"].ToString(),
                    TrangThai = Convert.ToBoolean(row["TrangThai"]),
                    GhiChuTraThietBi = row["GhiChuTraThietBi"].ToString()
                };
            }
        }
        return null;
    }
    public MuonThietBiDTO GetByMaND_MaTKB(string maND, int maTKB)
    {
        string query = "SELECT * FROM MuonThietBi WHERE MaNguoiDung = @MaNguoiDung AND MaTKB = @MaTKB";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaNguoiDung", maND);
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
                return new MuonThietBiDTO
                {
                    MaMuon = Convert.ToInt32(row["MaMuon"]),
                    MaNguoiDung = row["MaNguoiDung"].ToString(),
                    MaTKB = Convert.ToInt32(row["MaTKB"]),
                    NgayMuon = Convert.ToDateTime(row["NgayMuon"]),
                    NgayTra = row["NgayTra"] != DBNull.Value ? Convert.ToDateTime(row["NgayTra"]) : (DateTime?)null,
                    TinhTrangTraTB = row["TinhTrangTraTB"].ToString(),
                    TrangThai = Convert.ToBoolean(row["TrangThai"]),
                    GhiChuTraThietBi = row["GhiChuTraThietBi"].ToString()
                };
            }
        }
        return null;
    }
    // Thêm mới bản ghi mượn thiết bị
    public bool Insert(MuonThietBiDTO thietBiMuon)
    {
        string query = @"INSERT INTO MuonThietBi (MaNguoiDung, MaTKB, NgayMuon, TinhTrangTraTB) 
                     VALUES (@MaNguoiDung, @MaTKB, @NgayMuon, @TinhTrangTraTB)";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaNguoiDung", thietBiMuon.MaNguoiDung);
            command.Parameters.AddWithValue("@MaTKB", thietBiMuon.MaTKB);
            command.Parameters.AddWithValue("@NgayMuon", thietBiMuon.NgayMuon);
            command.Parameters.AddWithValue("@TinhTrangTraTB", thietBiMuon.TinhTrangTraTB);

            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Cập nhật bản ghi mượn thiết bị
    public bool Update(MuonThietBiDTO thietBiMuon)
    {
        string query = @"UPDATE MuonThietBi 
                         SET NgayTra = @NgayTra, 
                             TinhTrangTraTB = @TinhTrangTraTB,
                             GhiChuTraThietBi = @GhiChu
                         WHERE MaMuon = @MaMuon";

        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaMuon", thietBiMuon.MaMuon); // Điều kiện cập nhật
            command.Parameters.AddWithValue("@NgayTra", thietBiMuon.NgayTra ?? (object)DBNull.Value); // Xử lý giá trị null cho NgayTra
            command.Parameters.AddWithValue("@TinhTrangTraTB", thietBiMuon.TinhTrangTraTB);
            command.Parameters.AddWithValue("@GhiChu", thietBiMuon.GhiChuTraThietBi);

            connection.Open();
            return command.ExecuteNonQuery() > 0; // Trả về true nếu cập nhật thành công
        }
    }
    public bool Update_TrangThai(int maMuon ,bool TrangThai)
    {
        string query = @"UPDATE MuonThietBi 
                         SET TrangThai = @TrangThai 
                         WHERE MaMuon = @MaMuon";

        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TrangThai", TrangThai);
            command.Parameters.AddWithValue("@MaMuon", maMuon);

            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }
    public bool Update_TinhTrang(int maMuon, string tinhTrang)
    {
        string query = @"UPDATE MuonThietBi 
                         SET TinhTrangTraTB = @TinhTrang 
                         WHERE MaMuon = @MaMuon";

        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TinhTrang", tinhTrang);
            command.Parameters.AddWithValue("@MaMuon", maMuon);

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
            command.Parameters.AddWithValue("@MaMuon", maMuon); // Truyền tham số MaMuon

            connection.Open();
            return command.ExecuteNonQuery() > 0; // Trả về true nếu xóa thành công
        }
    }



    // Tìm kiếm bản ghi mượn thiết bị theo từ khóa
    public List<MuonThietBiDTO> Search(string keyword)
    {
        List<MuonThietBiDTO> list = new List<MuonThietBiDTO>();
        string query = @"
        SELECT * 
        FROM MuonThietBi 
        WHERE 
            MaNguoiDung LIKE @Keyword OR
            MaTKB LIKE @Keyword OR
            TinhTrangTraTB LIKE @Keyword OR
            TrangThai LIKE @Keyword";

        SqlParameter[] parameters = new SqlParameter[]
        {
        new SqlParameter("@Keyword", $"%{keyword}%")
        };

        DataTable dataTable = GetDataTableQuery(query, parameters);

        foreach (DataRow row in dataTable.Rows)
        {
            list.Add(new MuonThietBiDTO
            {
                MaMuon = Convert.ToInt32(row["MaMuon"]),
                //MaNguoiDung = Convert.ToInt32(row["MaNguoiDung"]),
                MaTKB = Convert.ToInt32(row["MaTKB"]),
                NgayMuon = Convert.ToDateTime(row["NgayMuon"]),
                NgayTra = row["NgayTra"] != DBNull.Value ? Convert.ToDateTime(row["NgayTra"]) : (DateTime?)null,
                TinhTrangTraTB = row["TinhTrangTraTB"].ToString(),
                TrangThai = Convert.ToBoolean(row["TrangThai"]),
                GhiChuTraThietBi = row["GhiChuTraThietBi"].ToString()
            });
        }

        return list;
    }



    

}

