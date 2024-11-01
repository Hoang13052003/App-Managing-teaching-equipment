using DTO;
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
        public List<pLoaiThietBiDTO> getAllLoaiTB()
        {
            List<pLoaiThietBiDTO> list = new List<pLoaiThietBiDTO>();
            string query = "SELECT * FROM LoaiThietBi";
            DataTable dataTable = GetDataTable(query);

            foreach (DataRow row in dataTable.Rows)
            {
                list.Add(new pLoaiThietBiDTO
                {
                    MaLoai = Convert.ToInt32(row["MaLoai"]),
                    TenLoai = row["TenLoai"].ToString(),
                });
            }
            return list;
        }
        public List<pThietBiDTO> SearchThietBi(int pMaLoai)
        {
            List<pThietBiDTO> list = new List<pThietBiDTO>();
            string query = "SELECT * FROM ThietBi WHERE MaLoai = '"+pMaLoai+"'";
            DataTable dataTable = GetDataTable(query);

            foreach (DataRow row in dataTable.Rows)
            {
                list.Add(new pThietBiDTO
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

        public List<pThietBiDTO> getAllThietBi()
        {
            List<pThietBiDTO> list = new List<pThietBiDTO>();
            string query = "SELECT * FROM ThietBi";
            DataTable dataTable = GetDataTable(query);

            foreach (DataRow row in dataTable.Rows)
            {
                list.Add(new pThietBiDTO
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

        public List<pChiTietThietBiDTO> getAllChiTietThietBi()
        {
            List<pChiTietThietBiDTO> list = new List<pChiTietThietBiDTO>();
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

                list.Add(new pChiTietThietBiDTO
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

        public List<pChiTietThietBiDTO> SearchChiTietThietBi(int pMaTB)
        {
            List<pChiTietThietBiDTO> list = new List<pChiTietThietBiDTO>();
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

                list.Add(new pChiTietThietBiDTO
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
        public List<pChiTietThietBiDTO> SearchKeyChiTietThietBi(string keyword)
        {
            List<pChiTietThietBiDTO> list = new List<pChiTietThietBiDTO>();
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

                list.Add(new pChiTietThietBiDTO
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

        public bool TaoYeuCauThietBi(YeuCauThietBiDTO yeuCauThietBiDTO, List<ChiTietYeuCauThietBiDTO> chiTietList)
        {
            string insertYeuCauQuery = "INSERT INTO YeuCauThietBi (MaNguoiDung, NgayYeuCau) VALUES (@MaNguoiDung, @NgayYeuCau); SELECT SCOPE_IDENTITY();";
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(insertYeuCauQuery, connection);
                command.Parameters.AddWithValue("@MaNguoiDung", yeuCauThietBiDTO.MaNguoiDung);
                command.Parameters.AddWithValue("@NgayYeuCau", yeuCauThietBiDTO.NgayYeuCau);

                connection.Open();
                int newMaYC = Convert.ToInt32(command.ExecuteScalar()); // Thực hiện câu lệnh và lấy MaYC mới nhất

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
                        chiTietCommand.Parameters.AddWithValue("@GhiChu", chiTiet.GhiChu ?? (object)DBNull.Value); // Nếu ghi chú có thể null
                        chiTietCommand.Parameters.AddWithValue("@TrangThai", chiTiet.TrangThai);

                        chiTietCommand.ExecuteNonQuery();
                    }
                    return true;
                }
            }
            return false;
        }

    }
}
