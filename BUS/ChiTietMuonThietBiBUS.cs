using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class ChiTietMuonThietBiBUS
    {
        private ChiTietMuonThietBiDAL _dal;

        public ChiTietMuonThietBiBUS()
        {
            _dal = new ChiTietMuonThietBiDAL(); // Khởi tạo đối tượng DAL
        }

        public List<ChiTietMuonThietBiDTO> GetByMaMuon(int maMuon)
        {
            try
            {
                return _dal.GetByMaMuon(maMuon);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách chi tiết mượn thiết bị theo mã mượn: " + ex.Message, "Thông Báo");
                return null;
            }
        }

        // Thêm một bản ghi ChiTietMuonThietBi
        public bool Insert(ChiTietMuonThietBiDTO chiTietMuonThietBi)
        {
            try
            {
                return _dal.Insert(chiTietMuonThietBi);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm chi tiết mượn thiết bị: " + ex.Message, "Thông Báo");
                return false;
            }
        }

        // Cập nhật thông tin ChiTietMuonThietBi
        public bool Update(ChiTietMuonThietBiDTO chiTietMuonThietBi)
        {
            try
            {
                return _dal.Update(chiTietMuonThietBi);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật chi tiết mượn thiết bị: " + ex.Message, "Thông Báo");
                return false;
            }
        }

        // Xóa một bản ghi ChiTietMuonThietBi
        public bool Delete(int maMuon, int maCTTB)
        {
            try
            {
                return _dal.Delete(maMuon, maCTTB);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi xóa chi tiết mượn thiết bị: " + ex.Message, "Thông Báo");
                return false;
            }
        }
        public bool Update_TrangThaiThieu(ChiTietMuonThietBiDTO chiTietMuonThietBi)
        {
            return _dal.Update_TrangThaiThieu(chiTietMuonThietBi);
        }
    }
}
