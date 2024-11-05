using System;
using System.Collections.Generic;
using DTO;

public class MonHocBUS
{
    private MonHocDAL monHocDAL;

    public MonHocBUS()
    {
        monHocDAL = new MonHocDAL();
    }

    // Lấy tất cả môn học
    public List<MonHocDTO> GetAll()
    {
        try
        {
            return monHocDAL.GetAll();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi khi lấy danh sách môn học: " + ex.Message);
            return new List<MonHocDTO>();
        }
    }

    // Lấy môn học theo mã
    public MonHocDTO GetByID(int maMon)
    {
        try
        {
            return monHocDAL.GetByID(maMon);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi khi lấy môn học theo mã: " + ex.Message);
            return null;
        }
    }

    // Thêm môn học
    public bool Insert(MonHocDTO monHoc)
    {
        try
        {
            return monHocDAL.Insert(monHoc);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi khi thêm môn học: " + ex.Message);
            return false;
        }
    }

    // Cập nhật môn học
    public bool Update(MonHocDTO monHoc)
    {
        try
        {
            return monHocDAL.Update(monHoc);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi khi cập nhật môn học: " + ex.Message);
            return false;
        }
    }

    // Xóa môn học
    public bool Delete(int maMon)
    {
        try
        {
            return monHocDAL.Delete(maMon);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi khi xóa môn học: " + ex.Message);
            return false;
        }
    }

    public bool CheckMonHocExists(string ten)
    {
        try
        {
            return monHocDAL.CheckMonHocExists(ten);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi khi kiểm tra trùng tên môn học: " + ex.Message);
            return false;
        }
    }
}
