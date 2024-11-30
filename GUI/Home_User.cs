using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Home_User : Form
    {
        private ThongTinCaNhanBUS _ttcnBUS;
        public Home_User()
        {
            InitializeComponent();
            _ttcnBUS = new ThongTinCaNhanBUS();
        }

        private void pannel_Lich_Day_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void lb_Xem_Ho_So_Chi_Tiet_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void lb_Xem_Danh_Sach_Thong_Bao_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void pannel_Lich_Day_Click(object sender, EventArgs e)
        {
            FormTask.OpenFormInPanel<Teaching_Schedule>(FormTask.Pannel_change);
        }

        private void Home_User_Load(object sender, EventArgs e)
        {
            ThongTinCaNhanDTO item = _ttcnBUS.GetByMaNguoiDung(AccountInfo.MaNguoiDung);
            if (item != null)
            {
                lbMaGiaoVien.Text = "Mã giáo viên: " + item.MaNguoiDung.ToString();
                lbHoTen.Text = "Họ và tên: "+item.HoTen.ToString();
                lbGioTinh.Text = "Giớ tính: "+item.GioiTinh.ToString();
                lbNgaySinh.Text ="Ngày sinh: "+ item.NgaySinh.ToString();
                lbDiaChi.Text = "Địa chỉ: "+item.DiaChi.ToString();
            }
        }
    }
}
