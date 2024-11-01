using System;
using System.Collections.Generic;
using DTO;

public class PhanHoiGiaoVienBUS
{
    private PhanHoiGiaoVienDAL phanHoiGiaoVienDAL;

    public PhanHoiGiaoVienBUS()
    {
        phanHoiGiaoVienDAL = new PhanHoiGiaoVienDAL();
    }

    // Lấy tất cả phản hồi giáo viên
    public List<PhanHoiGiaoVienDTO> GetAll()
    {
        try
        {
            return phanHoiGiaoVienDAL.GetAll();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi khi lấy danh sách phản hồi: " + ex.Message);
            return new List<PhanHoiGiaoVienDTO>();
        }
    }

    // Lấy phản hồi giáo viên theo mã
    public PhanHoiGiaoVienDTO GetByID(int maPH)
    {
        try
        {
            return phanHoiGiaoVienDAL.GetByID(maPH);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi khi lấy phản hồi theo mã: " + ex.Message);
            return null;
        }
    }

    // Thêm phản hồi giáo viên
    public bool Insert(PhanHoiGiaoVienDTO phanHoi)
    {
        try
        {
            return phanHoiGiaoVienDAL.Insert(phanHoi);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi khi thêm phản hồi: " + ex.Message);
            return false;
        }
    }

    // Cập nhật phản hồi giáo viên
    public bool Update(PhanHoiGiaoVienDTO phanHoi)
    {
        try
        {
            return phanHoiGiaoVienDAL.Update(phanHoi);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi khi cập nhật phản hồi: " + ex.Message);
            return false;
        }
    }

    // Xóa phản hồi giáo viên
    public bool Delete(int maPH)
    {
        try
        {
            return phanHoiGiaoVienDAL.Delete(maPH);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi khi xóa phản hồi: " + ex.Message);
            return false;
        }
    }
}
