using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class NhapThietBiDAL : DatabaseHelper
{
    // Lấy tất cả bản ghi nhập thiết bị
    public List<NhapThietBiDTO> GetAll()
    {
        List<NhapThietBiDTO> list = new List<NhapThietBiDTO>();
        string query = @"SELECT MaNhap, tt.HoTen, NgayNhap, SoLuong, TongTien, ncc.TenNCC
                        FROM NhapThietBi ntb, NguoiDung nd, ThongTinCaNhan tt, NhaCungCap ncc
                        Where ntb.MaNCC = ncc.MaNCC
                        And ntb.MaNguoiDung = nd.MaNguoiDung
                        And nd.MaNguoiDung = tt.MaNguoiDung";
        DataTable dataTable = GetDataTable(query);

        foreach (DataRow row in dataTable.Rows)
        {
            list.Add(new NhapThietBiDTO
            {
                MaNhap = Convert.ToInt32(row["MaNhap"]),
                HoTen = row["HoTen"].ToString(),
                NgayNhap = row["NgayNhap"] as DateTime?,
                SoLuong = Convert.ToInt32(row["SoLuong"]),
                TongTien = Convert.ToDecimal(row["TongTien"]),
                TenNCC = row["TenNCC"].ToString(),
            });
        }
        return list;
    }

    // Lấy bản ghi nhập thiết bị theo mã
    public NhapThietBiDTO GetByID(int maNhap)
    {
        string query = "SELECT * FROM NhapThietBi WHERE MaNhap = @MaNhap";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaNhap", maNhap);
            connection.Open();
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }

            if (dataTable.Rows.Count == 1)
            {
                DataRow row = dataTable.Rows[0];
                return new NhapThietBiDTO
                {
                    MaNhap = Convert.ToInt32(row["MaNhap"]),
                    MaNguoiDung = row["MaNguoiDung"].ToString(),
                    NgayNhap = row["NgayNhap"] as DateTime?,
                    SoLuong = Convert.ToInt32(row["SoLuong"]),
                    TongTien = Convert.ToDecimal(row["TongTien"])
                };
            }
        }
        return null;
    }

    // Thêm bản ghi nhập thiết bị
    public bool InsertNhapThietBi(NhapThietBiDTO nhapThietBi, List<ChiTietNhapDTO> chiTietList)
    {
        string insertNhapThietBiQuery = @"
            INSERT INTO NhapThietBi (MaNguoiDung, NgayNhap, SoLuong, TongTien, MaNCC) 
            VALUES (@MaNguoiDung, @NgayNhap, @SoLuong, @TongTien, @MaNCC); 
            SELECT SCOPE_IDENTITY();";

        using (SqlConnection connection = GetConnection())
        {
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand(insertNhapThietBiQuery, connection, transaction);
                    command.Parameters.AddWithValue("@MaNguoiDung", nhapThietBi.MaNguoiDung);
                    command.Parameters.AddWithValue("@NgayNhap", nhapThietBi.NgayNhap.HasValue ? (object)nhapThietBi.NgayNhap.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@SoLuong", nhapThietBi.SoLuong);
                    command.Parameters.AddWithValue("@TongTien", nhapThietBi.TongTien);
                    command.Parameters.AddWithValue("@MaNCC", nhapThietBi.MaNCC);

                    int newMaNhap = Convert.ToInt32(command.ExecuteScalar());

                    if (newMaNhap > 0)
                    {
                        foreach (var chiTiet in chiTietList)
                        {
                            // Thêm chi tiết nhập
                            string insertChiTietNhapQuery = @"
                                INSERT INTO ChiTietNhap (MaNhap, MaTB, GiaNhap, SoLuong, ThanhTien) 
                                VALUES (@MaNhap, @MaTB, @GiaNhap, @SoLuong, @ThanhTien)";
                            SqlCommand chiTietCommand = new SqlCommand(insertChiTietNhapQuery, connection, transaction);
                            chiTietCommand.Parameters.AddWithValue("@MaNhap", newMaNhap);
                            chiTietCommand.Parameters.AddWithValue("@MaTB", chiTiet.MaTB);
                            chiTietCommand.Parameters.AddWithValue("@GiaNhap", chiTiet.GiaNhap);
                            chiTietCommand.Parameters.AddWithValue("@SoLuong", chiTiet.SoLuong);
                            chiTietCommand.Parameters.AddWithValue("@ThanhTien", chiTiet.ThanhTien);
                            chiTietCommand.ExecuteNonQuery();

                            // 1. Cập nhật số lượng thiết bị
                            string updateSoLuongQuery = @"
                                UPDATE ThietBi 
                                SET SoLuong = SoLuong + @SoLuong
                                WHERE MaTB = @MaTB";
                            SqlCommand updateSoLuongCommand = new SqlCommand(updateSoLuongQuery, connection, transaction);
                            updateSoLuongCommand.Parameters.AddWithValue("@SoLuong", chiTiet.SoLuong);
                            updateSoLuongCommand.Parameters.AddWithValue("@MaTB", chiTiet.MaTB);
                            updateSoLuongCommand.ExecuteNonQuery();

                            // 2. Thêm dòng vào ChiTietThietBi
                            for (int i = 0; i < chiTiet.SoLuong; i++)
                            {
                                string insertChiTietThietBiQuery = @"
                                    INSERT INTO ChiTietThietBi (MaTB, TinhTrang, TrangThai, NgayMua) 
                                    VALUES (@MaTB, N'Mới', 0, GETDATE());
                                    SELECT SCOPE_IDENTITY();";
                                SqlCommand insertChiTietThietBiCommand = new SqlCommand(insertChiTietThietBiQuery, connection, transaction);
                                insertChiTietThietBiCommand.Parameters.AddWithValue("@MaTB", chiTiet.MaTB);
                                int newMaCTTB = Convert.ToInt32(insertChiTietThietBiCommand.ExecuteScalar());

                                // 3. Thêm dòng vào ChiTietThietBi_NhaCungCap
                                string insertChiTietThietBiNCCQuery = @"
                                    INSERT INTO ChiTietThietBi_NhaCungCap (MaCTTB, MaNCC, NgayBatDau) 
                                    VALUES (@MaCTTB, @MaNCC, GETDATE())";
                                SqlCommand insertChiTietThietBiNCCCommand = new SqlCommand(insertChiTietThietBiNCCQuery, connection, transaction);
                                insertChiTietThietBiNCCCommand.Parameters.AddWithValue("@MaCTTB", newMaCTTB);
                                insertChiTietThietBiNCCCommand.Parameters.AddWithValue("@MaNCC", nhapThietBi.MaNCC);
                                insertChiTietThietBiNCCCommand.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Lỗi khi thêm bản ghi nhập thiết bị: " + ex.Message);
                }
            }
        }
        return false;
    }

    // Cập nhật bản ghi nhập thiết bị
    public bool Update(NhapThietBiDTO nhapThietBi)
    {
        string query = "UPDATE NhapThietBi SET MaNguoiDung = @MaNguoiDung, NgayNhap = @NgayNhap, SoLuong = @SoLuong, TongTien = @TongTien WHERE MaNhap = @MaNhap";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaNhap", nhapThietBi.MaNhap);
            command.Parameters.AddWithValue("@MaNguoiDung", nhapThietBi.MaNguoiDung);
            command.Parameters.AddWithValue("@NgayNhap", nhapThietBi.NgayNhap.HasValue ? (object)nhapThietBi.NgayNhap.Value : DBNull.Value);
            command.Parameters.AddWithValue("@SoLuong", nhapThietBi.SoLuong);
            command.Parameters.AddWithValue("@TongTien", nhapThietBi.TongTien);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Xóa bản ghi nhập thiết bị
    public bool Delete(int maNhap)
    {
        string query = "DELETE FROM NhapThietBi WHERE MaNhap = @MaNhap";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaNhap", maNhap);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Lấy tất cả chi tiết nhập theo mã nhập
    public List<ChiTietNhapDTO> GetAll(int maNhap)
    {
        List<ChiTietNhapDTO> list = new List<ChiTietNhapDTO>();
        string query = @"SELECT MaNhap, TenTB, GiaNhap, ctn.SoLuong, ThanhTien 
                        FROM ChiTietNhap ctn, ThietBi tb
                        WHERE MaNhap = @MaNhap
                        AND ctn.MaTB = tb.MaTB";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaNhap", maNhap);
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }

            foreach (DataRow row in dataTable.Rows)
            {
                list.Add(new ChiTietNhapDTO
                {
                    MaNhap = Convert.ToInt32(row["MaNhap"]),
                    TenTB = row["TenTB"].ToString(),
                    GiaNhap = Convert.ToDecimal(row["GiaNhap"]),
                    SoLuong = Convert.ToInt32(row["SoLuong"]),
                    ThanhTien = Convert.ToDecimal(row["ThanhTien"])
                });
            }
        }
        return list;
    }

    // Thêm chi tiết nhập
    public bool Insert(ChiTietNhapDTO chiTietNhap)
    {
        string query = "INSERT INTO ChiTietNhap (MaNhap, MaTB, GiaNhap, SoLuong, ThanhTien) VALUES (@MaNhap, @MaTB, @GiaNhap, @SoLuong, @ThanhTien)";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaNhap", chiTietNhap.MaNhap);
            command.Parameters.AddWithValue("@MaTB", chiTietNhap.MaTB);
            command.Parameters.AddWithValue("@GiaNhap", chiTietNhap.GiaNhap);
            command.Parameters.AddWithValue("@SoLuong", chiTietNhap.SoLuong);
            command.Parameters.AddWithValue("@ThanhTien", chiTietNhap.ThanhTien);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }

    // Xóa chi tiết nhập theo mã nhập và mã thiết bị nhà cung cấp
    public bool Delete(int maNhap, int maTB)
    {
        string query = "DELETE FROM ChiTietNhap WHERE MaNhap = @MaNhap AND MaTB = @MaTB";
        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaNhap", maNhap);
            command.Parameters.AddWithValue("@MaCTTB_NCC", maTB);
            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }
}
