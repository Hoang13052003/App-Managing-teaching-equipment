using DTO; // Đảm bảo đã có DTO cho BienBanXuLy
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;
using System.Windows.Forms;

public class BienBanXuLyDAL : DatabaseHelper
{
    // Lấy tất cả biên bản xử lý
    public List<BienBanXuLyDTO> GetAll()
    {
        List<BienBanXuLyDTO> list = new List<BienBanXuLyDTO>();
        string query = "SELECT * FROM BienBanXuLy";
        DataTable dataTable = GetDataTable(query);

        foreach (DataRow row in dataTable.Rows)
        {
            list.Add(new BienBanXuLyDTO
            {
                MaBB = Convert.ToInt32(row["MaBB"]),
                TenNguoiLamHong = row["TenNguoiLamHong"].ToString(),
                VaiTro = row["VaiTro"].ToString(),
                ThoiGianLamHong = Convert.ToDateTime(row["ThoiGianLamHong"]),
                ThoiGianXuLy = row["ThoiGianXuLy"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["ThoiGianXuLy"]) : null,
                ChiPhiSuaChua = row["ChiPhiSuaChua"] != DBNull.Value ? Convert.ToDouble(row["ChiPhiSuaChua"]) : 0,
                TinhTrang = Convert.ToInt32(row["TinhTrang"])
            });
        }
        return list;
    }
    public List<BienBanXuLyDTO> SearchBienBan(string keyword)
    {
        List<BienBanXuLyDTO> list = new List<BienBanXuLyDTO>();
        string query = "SELECT * FROM BienBanXuLy " +
                   "WHERE TenNguoiLamHong LIKE '%" + keyword + "%' " +
                   "OR VaiTro LIKE '%" + keyword + "%' " +
                   "OR MoTaChiTiet LIKE '%" + keyword + "%' " +
                   "OR CONVERT(VARCHAR, ThoiGianLamHong, 120) LIKE '%" + keyword + "%' " +
                   "OR CONVERT(VARCHAR, ThoiGianXuLy, 120) LIKE '%" + keyword + "%'";
        DataTable dataTable = GetDataTable(query);
        foreach (DataRow row in dataTable.Rows)
        {
            list.Add(new BienBanXuLyDTO
            {
                MaBB = Convert.ToInt32(row["MaBB"]),
                TenNguoiLamHong = row["TenNguoiLamHong"].ToString(),
                VaiTro = row["VaiTro"].ToString(),
                ThoiGianLamHong = Convert.ToDateTime(row["ThoiGianLamHong"]),
                ThoiGianXuLy = row["ThoiGianXuLy"] != DBNull.Value ? Convert.ToDateTime(row["ThoiGianXuLy"]) : (DateTime?)null,
               
                ChiPhiSuaChua = row["ChiPhiSuaChua"] != DBNull.Value ? Convert.ToDouble(row["ChiPhiSuaChua"]) : 0,
                TinhTrang = Convert.ToInt32(row["TinhTrang"])
            });
        }
        return list;
    }
    public List<ChiTietBienBanDTO> GetAllChiTietBB()
    {
        List<ChiTietBienBanDTO> list = new List<ChiTietBienBanDTO>();
        string query = @"SELECT MaBB, CTBB.MaCTTB_NCC, TenTB, HinhAnh, MoTaChiTiet 
                        FROM ChiTietBienBan CTBB
                        JOIN ChiTietThietBi_NhaCungCap CN ON CN.MaCTTB_NCC = CTBB.MaCTTB_NCC
                        JOIN ChiTietThietBi CTTB ON CTTB.MaCTTB = CN.MaCTTB
                        JOIN ThietBi TB ON TB.MaTB = CTTB.MaTB";
        DataTable dataTable = GetDataTable(query);

        foreach (DataRow row in dataTable.Rows)
        {
            list.Add(new ChiTietBienBanDTO
            {
                MaBB = Convert.ToInt32(row["MaBB"]),
                MaCTTB_NCC = Convert.ToInt32(row["MaCTTB_NCC"]),
                TenTB = row["TenTB"].ToString(),
                HinhAnh = row["HinhAnh"].ToString(),
                MoTaChiTiet = row["MoTaChiTiet"].ToString()
            });
        }
        return list;
    }
    public List<ChiTietBienBanDTO> SearchChiTietBB(int pMaBB)
    {
        List<ChiTietBienBanDTO> list = new List<ChiTietBienBanDTO>();
        string query = @"SELECT MaBB, CTBB.MaCTTB_NCC, TenTB, HinhAnh, MoTaChiTiet
                        FROM ChiTietBienBan CTBB
                        JOIN ChiTietThietBi_NhaCungCap CN ON CN.MaCTTB_NCC = CTBB.MaCTTB_NCC
                        JOIN ChiTietThietBi CTTB ON CTTB.MaCTTB = CN.MaCTTB
                        JOIN ThietBi TB ON TB.MaTB = CTTB.MaTB
                        AND MaBB = '"+pMaBB+"'";
        DataTable dataTable = GetDataTable(query);

        foreach (DataRow row in dataTable.Rows)
        {
            list.Add(new ChiTietBienBanDTO
            {
                MaBB = Convert.ToInt32(row["MaBB"]),
                MaCTTB_NCC = Convert.ToInt32(row["MaCTTB_NCC"]),
                TenTB = row["TenTB"].ToString(),
                HinhAnh = row["HinhAnh"].ToString(),
                MoTaChiTiet = row["MoTaChiTiet"].ToString()
            });
        }
        return list;
    }

    public bool Insert(BienBanXuLyDTO bienBan, List<ChiTietBienBanDTO> chiTietList)
    {
        string insertBienBanQuery = "INSERT INTO BienBanXuLy (TenNguoiLamHong, VaiTro, ThoiGianLamHong, ThoiGianXuLy, ChiPhiSuaChua, TinhTrang) " +
                                     "VALUES (@TenNguoiLamHong, @VaiTro, @ThoiGianLamHong, @ThoiGianXuLy, @ChiPhiSuaChua, @TinhTrang); " +
                                     "SELECT SCOPE_IDENTITY();"; // Lấy ID của bản ghi vừa chèn

        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(insertBienBanQuery, connection);
            command.Parameters.AddWithValue("@TenNguoiLamHong", bienBan.TenNguoiLamHong);
            command.Parameters.AddWithValue("@VaiTro", bienBan.VaiTro);
            command.Parameters.AddWithValue("@ThoiGianLamHong", bienBan.ThoiGianLamHong);
            command.Parameters.AddWithValue("@ThoiGianXuLy", bienBan.ThoiGianXuLy ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@ChiPhiSuaChua", bienBan.ChiPhiSuaChua);
            command.Parameters.AddWithValue("@TinhTrang", bienBan.TinhTrang);

            connection.Open();
            int newMaBB = Convert.ToInt32(command.ExecuteScalar());

            if (newMaBB > 0)
            {
                foreach (var chiTiet in chiTietList)
                {
                    string insertChiTietQuery = @"INSERT INTO ChiTietBienBan (MaBB, MaCTTB_NCC, MoTaChiTiet, HinhAnh) 
                                             VALUES (@MaBB, @MaCTTB_NCC, @MoTaChiTiet, @HinhAnh)";

                    SqlCommand chiTietCommand = new SqlCommand(insertChiTietQuery, connection);
                    chiTietCommand.Parameters.AddWithValue("@MaBB", newMaBB);
                    chiTietCommand.Parameters.AddWithValue("@MaCTTB_NCC", chiTiet.MaCTTB_NCC);
                    chiTietCommand.Parameters.AddWithValue("@MoTaChiTiet", chiTiet.MoTaChiTiet);
                    chiTietCommand.Parameters.AddWithValue("@HinhAnh", chiTiet.HinhAnh ?? (object)DBNull.Value);

                    chiTietCommand.ExecuteNonQuery();

                    string updateTrangThaiQuery = @"UPDATE ChiTietThietBi 
                                                SET TinhTrang = N'Hỏng'
                                                WHERE MaCTTB = (
                                                    SELECT MaCTTB 
                                                    FROM ChiTietThietBi_NhaCungCap 
                                                    WHERE MaCTTB_NCC = @MaCTTB_NCC
                                                )";

                    SqlCommand updateCommand = new SqlCommand(updateTrangThaiQuery, connection);
                    updateCommand.Parameters.AddWithValue("@MaCTTB_NCC", chiTiet.MaCTTB_NCC);
                    updateCommand.ExecuteNonQuery();
                }
                return true;
            }
        }
        return false;
    }

    public bool Update(BienBanXuLyDTO bienBan, List<ChiTietBienBanDTO> chiTietList)
    {
        string updateBBQuery = @"UPDATE BienBanXuLy SET TenNguoiLamHong = @TenNguoiLamHong, 
                                VaiTro = @VaiTro, ThoiGianLamHong = @ThoiGianLamHong, ThoiGianXuLy = @ThoiGianXuLy,
                                ChiPhiSuaChua = @ChiPhiSuaChua, TinhTrang = @TinhTrang
                                WHERE MaBB = @MaBB";

        using (SqlConnection connection = GetConnection())
        {
            connection.Open();
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    using (SqlCommand commandBB = new SqlCommand(updateBBQuery, connection, transaction))
                    {
                        commandBB.Parameters.AddWithValue("@MaBB", bienBan.MaBB);
                        commandBB.Parameters.AddWithValue("@TenNguoiLamHong", bienBan.TenNguoiLamHong);
                        commandBB.Parameters.AddWithValue("@VaiTro", bienBan.VaiTro);
                        commandBB.Parameters.AddWithValue("@ThoiGianLamHong", bienBan.ThoiGianLamHong);
                        commandBB.Parameters.AddWithValue("@ThoiGianXuLy", bienBan.ThoiGianXuLy);
                        commandBB.Parameters.AddWithValue("@ChiPhiSuaChua", bienBan.ChiPhiSuaChua);
                        commandBB.Parameters.AddWithValue("@TinhTrang", bienBan.TinhTrang);

                        commandBB.ExecuteNonQuery();
                    }
                    foreach (var chiTiet in chiTietList)
                    {
                        string updateCTTQuery = @"UPDATE ChiTietThietBi
                                                SET TinhTrang = N'Cũ', TrangThai = 1
                                                WHERE MaCTTB = (
                                                    SELECT MaCTTB 
                                                    FROM ChiTietThietBi_NhaCungCap 
                                                    WHERE MaCTTB_NCC = (
                                                        SELECT MaCTTB_NCC 
                                                        FROM ChiTietBienBan 
                                                        WHERE MaBB = @MaBB
                                                          AND MaCTTB_NCC = @MaCTTB_NCC
                                                    )
                                                )";

                        using (SqlCommand commandCTT = new SqlCommand(updateCTTQuery, connection, transaction))
                        {
                            commandCTT.Parameters.AddWithValue("@MaBB", bienBan.MaBB);
                            commandCTT.Parameters.AddWithValue("@MaCTTB_NCC", chiTiet.MaCTTB_NCC);
                            commandCTT.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
    }
    public string GetImage(int maBB, int maCTTB_NCC)
    {
        string imagePath = null;
        string query = "SELECT HinhAnh FROM ChiTietBienBan WHERE MaBB = @MaBB AND MaCTTB_NCC = @MaCTTB_NCC";

        using (SqlConnection connection = GetConnection())
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaCTTB_NCC", maCTTB_NCC);
            command.Parameters.AddWithValue("@MaBB", maBB);
            connection.Open();
            object result = command.ExecuteScalar();

            if (result != null)
            {
                imagePath = result.ToString();
            }
        }

        return imagePath;
    }
    // Xóa biên bản xử lý
    //public bool Delete(int maBB)
    //{
    //    string query = "DELETE FROM BienBanXuLy WHERE MaBB = @MaBB";
    //    using (SqlConnection connection = GetConnection())
    //    {
    //        SqlCommand command = new SqlCommand(query, connection);
    //        command.Parameters.AddWithValue("@MaBB", maBB);
    //        connection.Open();
    //        return command.ExecuteNonQuery() > 0;
    //    }
    //}
    //public List<BienBanXuLyDTO> Search(string searchTerm)
    //{
    //    List<BienBanXuLyDTO> list = new List<BienBanXuLyDTO>();
    //    string query = "SELECT * FROM BienBanXuLy WHERE CONTAINS(MoTaChiTiet, @SearchTerm)";
    //    using (SqlConnection connection = GetConnection())
    //    {
    //        SqlCommand command = new SqlCommand(query, connection);
    //        command.Parameters.AddWithValue("@SearchTerm", searchTerm);
    //        connection.Open();
    //        DataTable dataTable = new DataTable();
    //        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
    //        {
    //            adapter.Fill(dataTable);
    //        }

    //        foreach (DataRow row in dataTable.Rows)
    //        {
    //            list.Add(new BienBanXuLyDTO
    //            {
    //                MaBB = Convert.ToInt32(row["MaBB"]),
    //                TenNguoiLamHong = row["TenNguoiLamHong"].ToString(),
    //                VaiTro = row["VaiTro"].ToString(),
    //                ThoiGianLamHong = Convert.ToDateTime(row["ThoiGianLamHong"]),
    //                MoTaChiTiet = row["MoTaChiTiet"].ToString(),
    //                ChiPhiSuaChua = row["ChiPhiSuaChua"] != DBNull.Value ? Convert.ToDouble(row["ChiPhiSuaChua"]) : 0,
    //                TinhTrang = Convert.ToInt32(row["TinhTrang"])
    //            });
    //        }
    //    }
    //    return list;
    //}
}
