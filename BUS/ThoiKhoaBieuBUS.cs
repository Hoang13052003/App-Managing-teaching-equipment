using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace BUS
{
    public class ThoiKhoaBieuBUS
    {
        private ThoiKhoaBieuDAL tkb = new ThoiKhoaBieuDAL();
        public List<ThoiKhoaBieuChiTietDTO> GetThoiKhoaBieuByUser(string maNguoiDung)
        {
            if (string.IsNullOrWhiteSpace(maNguoiDung))
            {
                throw new ArgumentException("Mã người dùng không được để trống.", nameof(maNguoiDung));
            }

            try
            {
                var result = tkb.GetThoiKhoaBieuByUser(maNguoiDung);

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
                var result = tkb.GetThoiKhoaBieuByUserAndDate(maNguoiDung, startDate, endDate);

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
