using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace BUS
{
    public class ThoiKhoaBieuBUS
    {
        private ThoiKhoaBieuDAL _tkb = new ThoiKhoaBieuDAL();

        public List<ThoiKhoaBieuDTO> GetAll()
        {
            return _tkb.GetAll();
        }
        public List<ThoiKhoaBieuChiTiet_NguoiDungDTO> GetAllTKBChiTiet()
        {
            return _tkb.GetAllThoiKhoaBieuChiTiet();
        }
        public ThoiKhoaBieuDTO GetByID(int maTKB)
        {
            return _tkb.GetByID(maTKB);  
        }

        // Thêm mới thời khóa biểu
        public bool Insert(ThoiKhoaBieuDTO tkb)
        {
            try
            {
                // Thực hiện thêm
                if (_tkb.Insert(tkb))
                {
                    // Thông báo thành công
                    Console.WriteLine("Thêm mới thời khóa biểu thành công!", "Thông báo");
                    return true;
                }
                else
                {
                    // Thông báo thất bại
                    Console.WriteLine("Thêm mới thời khóa biểu thất bại.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
                Console.WriteLine($"Đã xảy ra lỗi: {ex.Message}");
                return false;
            }
        }

        // Cập nhật thời khóa biểu
        public bool Update(ThoiKhoaBieuDTO tkb)
        {
            try
            {
                // Thực hiện cập nhật
                if (_tkb.Update(tkb))
                {
                    // Thông báo thành công
                    Console.WriteLine("Cập nhật thời khóa biểu thành công!");
                    return true;
                }
                else
                {
                    // Thông báo thất bại
                    Console.WriteLine("Cập nhật thời khóa biểu thất bại.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
                Console.WriteLine($"Đã xảy ra lỗi: {ex.Message}", "Lỗi");
                return false;
            }
        }

        // Xóa thời khóa biểu
        public bool Delete(int maTKB)
        {
            try
            {
                // Thực hiện xóa
                if (_tkb.Delete(maTKB))
                {
                    // Thông báo thành công
                    Console.WriteLine("Xóa thời khóa biểu thành công!");
                    return true;
                }
                else
                {
                    // Thông báo thất bại
                    Console.WriteLine("Xóa thời khóa biểu thất bại.", "Lỗi");
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
                Console.WriteLine($"Đã xảy ra lỗi: {ex.Message}", "Lỗi");
                return false;
            }
        }


        public List<ThoiKhoaBieuChiTietDTO> GetThoiKhoaBieuByUser(string maNguoiDung, DateTime startDate, DateTime endDate)
        {
            if (string.IsNullOrWhiteSpace(maNguoiDung))
            {
                throw new ArgumentException("Mã người dùng không được để trống.", nameof(maNguoiDung));
            }

            try
            {
                var result = _tkb.GetThoiKhoaBieuByUser(maNguoiDung, startDate, endDate);

                if (result == null || result.Count == 0)
                {
                    Console.WriteLine("Không tìm thấy thời khóa biểu cho người dùng có mã: " + maNguoiDung);
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi lấy thời khóa biểu: " + ex.Message);
                throw new ApplicationException("Không thể lấy dữ liệu thời khóa biểu tại thời điểm này.", ex);
            }
        }
        public List<ThoiKhoaBieuChiTietDTO> GetThoiKhoaBieuByUserAndDate(string maNguoiDung, DateTime startDate, DateTime endDate)
        {
            if (string.IsNullOrWhiteSpace(maNguoiDung))
            {
                throw new ArgumentException("Mã người dùng không được để trống.", nameof(maNguoiDung));
            }

            try
            {
                var result = _tkb.GetThoiKhoaBieuByUserAndDate(maNguoiDung, startDate, endDate);

                if (result == null || result.Count == 0)
                {
                    Console.WriteLine("Không tìm thấy thời khóa biểu cho người dùng có mã: " + maNguoiDung);
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi lấy thời khóa biểu: " + ex.Message);
                throw new ApplicationException("Không thể lấy dữ liệu thời khóa biểu tại thời điểm này.", ex);
            }
        }
    }
}
