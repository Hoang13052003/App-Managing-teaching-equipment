using DTO;
using System;
using System.Collections.Generic;

public class NguoiDungBUS
{
    private NguoiDungDAL nguoiDungDAL = new NguoiDungDAL();

    // Lấy tất cả người dùng
    public List<NguoiDungDTO> GetAllUsers()
    {
        return nguoiDungDAL.GetAll();
    }

    // Lấy người dùng theo ID
    public NguoiDungDTO GetUserByID(string maNguoiDung)
    {
        return nguoiDungDAL.GetByID(maNguoiDung);
    }

    // Thêm người dùng mới
    public bool AddUser(NguoiDungDTO nguoiDung)
    {
        // Kiểm tra tên đăng nhập đã tồn tại chưa
        if (IsUsernameExists(nguoiDung.TenDangNhap))
        {
            return false; // Tên đăng nhập đã tồn tại
        }

        return nguoiDungDAL.Insert(nguoiDung);
    }
    
    // Cập nhật người dùng
    public bool UpdateUser(NguoiDungDTO nguoiDung)
    {
        return nguoiDungDAL.Update(nguoiDung);
    }

    // Xóa người dùng
    public bool DeleteUser(string maNguoiDung)
    {
        return nguoiDungDAL.Delete(maNguoiDung);
    }

    // Kiểm tra tên đăng nhập đã tồn tại chưa
    private bool IsUsernameExists(string tenDangNhap)
    {
        return nguoiDungDAL.CheckIfUsernameExists(tenDangNhap);
    }
}
