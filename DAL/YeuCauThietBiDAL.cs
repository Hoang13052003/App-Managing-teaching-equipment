﻿using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAL
{
    public class YeuCauThietBiDAL: DatabaseHelper
    {
        public List<LoaiThietBiDTO> getAllLoaiTB()
        {
            List<LoaiThietBiDTO> list = new List<LoaiThietBiDTO>();
            string query = "SELECT * FROM LoaiThietBi";
            DataTable dataTable = GetDataTable(query);

            foreach (DataRow row in dataTable.Rows)
            {
                list.Add(new LoaiThietBiDTO
                {
                    MaLoai = Convert.ToInt32(row["MaLoai"]),
                    TenLoai = row["TenLoai"].ToString(),
                });
            }
            return list;
        }
        public List<ThietBiDTO> SearchThietBi(int pMaLoai)
        {
            List<ThietBiDTO> list = new List<ThietBiDTO>();

            string query = "SELECT * FROM ThietBi WHERE MaLoai = '"+pMaLoai+"'";
            DataTable dataTable = GetDataTable(query);

            foreach (DataRow row in dataTable.Rows)
            {
                list.Add(new ThietBiDTO
                {
                    MaTB = Convert.ToInt32(row["MaTB"]),
                    TenTB = row["TenTB"].ToString(),
                    MaLoai = Convert.ToInt32(row["MaLoai"]),
                    NSX = row["NSX"].ToString(),
                    SoLuong = Convert.ToInt32(row["SoLuong"]),
                });
            }
            return list;
        }

        public List<ThietBiDTO> getAllThietBi()
        {
            List<ThietBiDTO> list = new List<ThietBiDTO>();
            string query = "SELECT * FROM ThietBi";
            DataTable dataTable = GetDataTable(query);

            foreach (DataRow row in dataTable.Rows)
            {
                list.Add(new ThietBiDTO
                {
                    MaTB = Convert.ToInt32(row["MaTB"]),
                    TenTB = row["TenTB"].ToString(),
                    MaLoai = Convert.ToInt32(row["MaLoai"]),
                    NSX = row["NSX"].ToString(),
                    SoLuong = Convert.ToInt32(row["SoLuong"]),
                });
            }
            return list;
        }

        public List<ChiTietThietBiDTO> getAllChiTietThietBi()
        {
            List<ChiTietThietBiDTO> list = new List<ChiTietThietBiDTO>();
            string query = @"Select cn.MaCTTB_NCC, TenTB, TinhTrang, TrangThai, NgayMua
                            From ThietBi t, ChiTietThietBi c, ChiTietThietBi_NhaCungCap cn
                            Where cn.MaCTTB = c.MaCTTB 
                            And t.MaTB = c.MaTB";
            DataTable dataTable = GetDataTable(query);

            foreach (DataRow row in dataTable.Rows)
            {
                int trangThai = Convert.ToInt32(row["TrangThai"]);
                string trangThaiText;
                switch (trangThai)
                {
                    case 0:
                        trangThaiText = "Đang sử dụng";
                        break;
                    case 1:
                        trangThaiText = "Không sử dụng";
                        break;
                    case 2:
                        trangThaiText = "Đang bảo dưỡng";
                        break;
                    default:
                        trangThaiText = "Không xác định";
                        break;
                }

                list.Add(new ChiTietThietBiDTO
                {
                    MaCTTB_NCC = Convert.ToInt32(row["MaCTTB_NCC"]),
                    TenTB = row["TenTB"].ToString(),
                    TinhTrang = row["TinhTrang"].ToString(),
                    TrangThai = trangThaiText,
                    NgayMua = row["NgayMua"] as DateTime?
                });
            }
            return list;
        }

        public List<ChiTietThietBiDTO> SearchChiTietThietBi(int pMaTB)
        {
            List<ChiTietThietBiDTO> list = new List<ChiTietThietBiDTO>();
            string query = @"Select cn.MaCTTB_NCC, TenTB, TinhTrang, TrangThai, NgayMua
                            From ThietBi t, ChiTietThietBi c, ChiTietThietBi_NhaCungCap cn
                            Where cn.MaCTTB = c.MaCTTB 
                            And t.MaTB = c.MaTB
                            And c.MaTB = '" + pMaTB+"'";
            DataTable dataTable = GetDataTable(query);

            foreach (DataRow row in dataTable.Rows)
            {
                int trangThai = Convert.ToInt32(row["TrangThai"]);
                string trangThaiText;
                switch (trangThai)
                {
                    case 0:
                        trangThaiText = "Đang sử dụng";
                        break;
                    case 1:
                        trangThaiText = "Không sử dụng";
                        break;
                    case 2:
                        trangThaiText = "Đang bảo dưỡng";
                        break;
                    default:
                        trangThaiText = "Không xác định";
                        break;
                }

                list.Add(new ChiTietThietBiDTO
                {
                    MaCTTB_NCC = Convert.ToInt32(row["MaCTTB_NCC"]),
                    TenTB = row["TenTB"].ToString(),
                    TinhTrang = row["TinhTrang"].ToString(),
                    TrangThai = trangThaiText,
                    NgayMua = row["NgayMua"] as DateTime?
                });
            }
            return list;
        }
        public List<ChiTietThietBiDTO> SearchChiTietThietBi2(int pMaLoaiTB)
        {
            List<ChiTietThietBiDTO> list = new List<ChiTietThietBiDTO>();
            string query = @"Select MaCTTB_NCC, TenTB, TinhTrang, TrangThai, NgayMua
                            From ThietBi t, ChiTietThietBi c, ChiTietThietBi_NhaCungCap cn, LoaiThietBi l
                            Where cn.MaCTTB = c.MaCTTB 
                            And t.MaTB = c.MaTB
                            And c.MaTB = t.MaTB
                            And t.MaLoai = '"+pMaLoaiTB+"'";

            DataTable dataTable = GetDataTable(query);

            foreach (DataRow row in dataTable.Rows)
            {
                int trangThai = Convert.ToInt32(row["TrangThai"]);
                string trangThaiText;
                switch (trangThai)
                {
                    case 0:
                        trangThaiText = "Đang sử dụng";
                        break;
                    case 1:
                        trangThaiText = "Không sử dụng";
                        break;
                    case 2:
                        trangThaiText = "Đang bảo dưỡng";
                        break;
                    default:
                        trangThaiText = "Không xác định";
                        break;
                }

                list.Add(new ChiTietThietBiDTO
                {
                    MaCTTB_NCC = Convert.ToInt32(row["MaCTTB_NCC"]),
                    TenTB = row["TenTB"].ToString(),
                    TinhTrang = row["TinhTrang"].ToString(),
                    TrangThai = trangThaiText,
                    NgayMua = row["NgayMua"] as DateTime?
                });
            }
            return list;
        }
        public List<ChiTietThietBiDTO> SearchKeyChiTietThietBi(string keyword)
        {
            List<ChiTietThietBiDTO> list = new List<ChiTietThietBiDTO>();
            string query = @"Select cn.MaCTTB_NCC, TenTB, TinhTrang, TrangThai, NgayMua
                            From ThietBi t, ChiTietThietBi c, ChiTietThietBi_NhaCungCap cn
                            Where cn.MaCTTB = c.MaCTTB 
                            And t.MaTB = c.MaTB
                            And (TenTB LIKE '%" + keyword+ "%' OR TinhTrang LIKE '%"+keyword+"%')";
            DataTable dataTable = GetDataTable(query);

            foreach (DataRow row in dataTable.Rows)
            {
                int trangThai = Convert.ToInt32(row["TrangThai"]);
                string trangThaiText;
                switch (trangThai)
                {
                    case 0:
                        trangThaiText = "Đang sử dụng";
                        break;
                    case 1:
                        trangThaiText = "Không sử dụng";
                        break;
                    case 2:
                        trangThaiText = "Đang bảo dưỡng";
                        break;
                    default:
                        trangThaiText = "Không xác định";
                        break;
                }

                list.Add(new ChiTietThietBiDTO
                {
                    MaCTTB_NCC = Convert.ToInt32(row["MaCTTB_NCC"]),
                    TenTB = row["TenTB"].ToString(),
                    TinhTrang = row["TinhTrang"].ToString(),
                    TrangThai = trangThaiText,
                    NgayMua = row["NgayMua"] as DateTime?
                });
            }
            return list;
        }

        public bool TaoYeuCauThietBi(YeuCauThietBiDTO yeuCauThietBiDTO, List<ChiTietYeuCauThietBiDTO> chiTietList)//Sửa chữa
        {
            string insertYeuCauQuery = "INSERT INTO YeuCauThietBi (MaNguoiDung, NgayYeuCau) VALUES (@MaNguoiDung, @NgayYeuCau); SELECT SCOPE_IDENTITY();";
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(insertYeuCauQuery, connection);
                command.Parameters.AddWithValue("@MaNguoiDung", yeuCauThietBiDTO.MaNguoiDung);
                command.Parameters.AddWithValue("@NgayYeuCau", yeuCauThietBiDTO.NgayYeuCau);

                connection.Open();
                int newMaYC = Convert.ToInt32(command.ExecuteScalar()); // Lấy MaYC mới nhất

                if (newMaYC > 0)
                {
                    foreach (var chiTiet in chiTietList)
                    {
                        string insertChiTietQuery = @"INSERT INTO ChiTietYeuCauThietBi (MaYC, MaCTTB_NCC, LoaiYeuCau, GhiChu, TrangThai) 
                                              VALUES (@MaYC, @MaCTTB_NCC, @LoaiYeuCau, @GhiChu, @TrangThai)";

                        SqlCommand chiTietCommand = new SqlCommand(insertChiTietQuery, connection);
                        chiTietCommand.Parameters.AddWithValue("@MaYC", newMaYC);
                        chiTietCommand.Parameters.AddWithValue("@MaCTTB_NCC", chiTiet.MaCTTB_NCC);
                        chiTietCommand.Parameters.AddWithValue("@LoaiYeuCau", chiTiet.LoaiYeuCau);
                        chiTietCommand.Parameters.AddWithValue("@GhiChu", chiTiet.GhiChu ?? (object)DBNull.Value);
                        chiTietCommand.Parameters.AddWithValue("@TrangThai", chiTiet.TrangThai);

                        chiTietCommand.ExecuteNonQuery();

                        // Cập nhật trạng thái của từng thiết bị trong ChiTietThietBi thành 2
                        string updateTrangThaiQuery = @"UPDATE ChiTietThietBi 
                                                        SET TrangThai = 2 
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
        public bool TaoYeuCauThietBi2(YeuCauThietBiDTO yeuCauThietBiDTO, List<ChiTietYeuCauThietBiDTO> chiTietList)  //Mua thiết bị
        {
            string insertYeuCauQuery = "INSERT INTO YeuCauThietBi (MaNguoiDung, NgayYeuCau) VALUES (@MaNguoiDung, @NgayYeuCau); SELECT SCOPE_IDENTITY();";
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(insertYeuCauQuery, connection);
                command.Parameters.AddWithValue("@MaNguoiDung", yeuCauThietBiDTO.MaNguoiDung);
                command.Parameters.AddWithValue("@NgayYeuCau", yeuCauThietBiDTO.NgayYeuCau);

                connection.Open();
                int newMaYC = Convert.ToInt32(command.ExecuteScalar()); // Lấy MaYC mới nhất

                if (newMaYC > 0)
                {
                    foreach (var chiTiet in chiTietList)
                    {
                        string insertChiTietQuery = @"INSERT INTO ChiTietYeuCauThietBi (MaYC, MaCTTB_NCC, LoaiYeuCau, GhiChu, TrangThai) 
                                              VALUES (@MaYC, @MaCTTB_NCC, @LoaiYeuCau, @GhiChu, @TrangThai)";

                        SqlCommand chiTietCommand = new SqlCommand(insertChiTietQuery, connection);
                        chiTietCommand.Parameters.AddWithValue("@MaYC", newMaYC);
                        chiTietCommand.Parameters.AddWithValue("@MaCTTB_NCC", chiTiet.MaCTTB_NCC);
                        chiTietCommand.Parameters.AddWithValue("@LoaiYeuCau", chiTiet.LoaiYeuCau);
                        chiTietCommand.Parameters.AddWithValue("@GhiChu", chiTiet.GhiChu ?? (object)DBNull.Value);
                        chiTietCommand.Parameters.AddWithValue("@TrangThai", chiTiet.TrangThai);

                        chiTietCommand.ExecuteNonQuery();
                    }
                    return true;
                }
            }
            return false;
        }

        public List<YeuCauThietBiDTO> getAllYeuCauThietBi()
        {
            List<YeuCauThietBiDTO> list = new List<YeuCauThietBiDTO>();
            string query = @"SELECT YC.MaYC, YC.NgayYeuCau, ND.HoTen AS TenNguoiDung
                            FROM YeuCauThietBi YC
                            JOIN ThongTinCaNhan ND ON YC.MaNguoiDung = ND.MaNguoiDung";
            DataTable dataTable = GetDataTable(query);

            foreach (DataRow row in dataTable.Rows)
            {
                list.Add(new YeuCauThietBiDTO
                {
                    MaYC = Convert.ToInt32(row["MaYC"]),
                    TenNguoiDung = row["TenNguoiDung"].ToString(), 
                    NgayYeuCau = row["NgayYeuCau"] as DateTime?
                });
            }
            return list;
        }

        public List<ChiTietYeuCauThietBiDTO> getAllChiTietYeuCauSuaThietBi()
        {
            List<ChiTietYeuCauThietBiDTO> list = new List<ChiTietYeuCauThietBiDTO>();
            string query = @"SELECT MaYC, CTYC.MaCTTB_NCC, TenTB, LoaiYeuCau, GhiChu, CTYC.TrangThai
                FROM ChiTietYeuCauThietBi CTYC
                JOIN ChiTietThietBi_NhaCungCap CTTBNCC ON CTYC.MaCTTB_NCC = CTTBNCC.MaCTTB_NCC
                JOIN ChiTietThietBi CTTB ON CTTB.MaCTTB = CTYC.MaCTTB_NCC
                JOIN ThietBi TB ON CTTB.MaTB = TB.MaTB
                AND LoaiYeuCau = N'Sửa chữa'
                AND (CTYC.TrangThai = 0 OR CTYC.TrangThai = 2)";
            DataTable dataTable = GetDataTable(query);

            foreach (DataRow row in dataTable.Rows)
            {
                list.Add(new ChiTietYeuCauThietBiDTO
                {
                    MaYC = Convert.ToInt32(row["MaYC"]),
                    MaCTTB_NCC = Convert.ToInt32(row["MaCTTB_NCC"]),
                    TenTB = row["TenTB"].ToString(),
                    LoaiYeuCau = row["LoaiYeuCau"].ToString(),
                    GhiChu = row["GhiChu"].ToString(),
                    TrangThai = Convert.ToInt32(row["TrangThai"]),
                });
            }
            return list;
        }
        public List<ChiTietYeuCauThietBiDTO> searchChiTietYeuCauSuaThietBi(int pMaYC)
        {
            List<ChiTietYeuCauThietBiDTO> list = new List<ChiTietYeuCauThietBiDTO>();
            string query = @"SELECT MaYC, CTYC.MaCTTB_NCC, TenTB, LoaiYeuCau, GhiChu, CTYC.TrangThai
                FROM ChiTietYeuCauThietBi CTYC
                JOIN ChiTietThietBi_NhaCungCap CTTBNCC ON CTYC.MaCTTB_NCC = CTTBNCC.MaCTTB_NCC
                JOIN ChiTietThietBi CTTB ON CTTB.MaCTTB = CTYC.MaCTTB_NCC
                JOIN ThietBi TB ON CTTB.MaTB = TB.MaTB
                AND CTYC.MaYC = '" + pMaYC+"' AND LoaiYeuCau = N'Sửa chữa'";
            DataTable dataTable = GetDataTable(query);

            foreach (DataRow row in dataTable.Rows)
            {
                list.Add(new ChiTietYeuCauThietBiDTO
                {
                    MaYC = Convert.ToInt32(row["MaYC"]),
                    MaCTTB_NCC = Convert.ToInt32(row["MaCTTB_NCC"]),
                    TenTB = row["TenTB"].ToString(),
                    LoaiYeuCau = row["LoaiYeuCau"].ToString(),
                    GhiChu = row["GhiChu"].ToString(),
                    TrangThai = Convert.ToInt32(row["TrangThai"]),
                });
            }
            return list;
        }

        public bool UpdataTrangThaiCTYCTB(int pMaYC, int pMaCTTB_NCC, int pTrangThai, string pKetQua, float pChiPhi)
        {
            string updateQuery = @"UPDATE ChiTietYeuCauThietBi 
                                SET TrangThai = @TrangThai
                                WHERE MaYC = @MaYC 
                                AND MaCTTB_NCC = @MaCTTB_NCC";

            string insertBaoDuongQuery = @"INSERT INTO BaoDuong (MaCTTB_NCC, NgayBD, KetQua, ChiPhi) 
                                        VALUES (@MaCTTB_NCC, @NgayBD, @KetQua, @ChiPhi)";

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection, transaction))
                    {
                        updateCommand.Parameters.AddWithValue("@MaYC", pMaYC);
                        updateCommand.Parameters.AddWithValue("@MaCTTB_NCC", pMaCTTB_NCC);
                        updateCommand.Parameters.AddWithValue("@TrangThai", pTrangThai);
                        int rowsAffected = updateCommand.ExecuteNonQuery();

                        // Kiểm tra xem có cập nhật được không
                        if (rowsAffected == 0)
                        {
                            throw new Exception("Không tìm thấy bản ghi để cập nhật.");
                        }
                    }

                    // Chỉ thêm vào BaoDuong nếu TrangThai khác 2
                    if (pTrangThai != 2)
                    {
                        using (SqlCommand insertCommand = new SqlCommand(insertBaoDuongQuery, connection, transaction))
                        {
                            insertCommand.Parameters.AddWithValue("@MaCTTB_NCC", pMaCTTB_NCC);
                            insertCommand.Parameters.AddWithValue("@NgayBD", DateTime.Now);
                            insertCommand.Parameters.AddWithValue("@KetQua", pKetQua);
                            insertCommand.Parameters.AddWithValue("@ChiPhi", pChiPhi);

                            insertCommand.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("Lỗi: " + ex.Message);
                    return false;
                }
            }
        }

    }
}
