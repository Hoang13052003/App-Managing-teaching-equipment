using System;
using System.Collections.Generic;
using DTO;

public class BaiHocBUS
{
    private BaiHocDAL baiHocDAL;

    public BaiHocBUS()
    {
        baiHocDAL = new BaiHocDAL();
    }

    // Lấy tất cả bài học
    public List<BaiHocDTO> GetAll()
    {
        try
        {
            return baiHocDAL.GetAll();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi khi lấy danh sách bài học: " + ex.Message);
            return new List<BaiHocDTO>();
        }
    }

    // Lấy bài học theo mã
    public BaiHocDTO GetByID(int maBaiHoc)
    {
        try
        {
            return baiHocDAL.GetByID(maBaiHoc);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi khi lấy bài học theo mã: " + ex.Message);
            return null;
        }
    }

    // Thêm bài học
    public bool Insert(BaiHocDTO baiHoc)
    {
        try
        {
            return baiHocDAL.Insert(baiHoc);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi khi thêm bài học: " + ex.Message);
            return false;
        }
    }

    // Cập nhật bài học
    public bool Update(BaiHocDTO baiHoc)
    {
        try
        {
            return baiHocDAL.Update(baiHoc);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi khi cập nhật bài học: " + ex.Message);
            return false;
        }
    }

    // Xóa bài học
    public bool Delete(int maBaiHoc)
    {
        try
        {
            return baiHocDAL.Delete(maBaiHoc);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi khi xóa bài học: " + ex.Message);
            return false;
        }
    }
}
