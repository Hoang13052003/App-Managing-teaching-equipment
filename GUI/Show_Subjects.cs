using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
namespace GUI
{
    public partial class Show_Subjects : Form
    {
        private ThoiKhoaBieuChiTietDTO _tkbDTO;
        public Show_Subjects(ThoiKhoaBieuChiTietDTO tkbDTO)
        {
            InitializeComponent();
            _tkbDTO = tkbDTO;
        }

        private void Show_Subjects_Load(object sender, EventArgs e)
        {
            lb_Ten_Mon_Hoc.Text = _tkbDTO.TenMonHoc.ToString();
            lb_MaMH_Lop.Text = _tkbDTO.TenLop.ToString();
            lb_Ten_Bai_Hoc.Text = _tkbDTO.TenBaiHoc.ToString();
            lb_Phong_Hoc.Text = _tkbDTO.TenPhong.ToString();
            lb_Gio_Hoc.Text = _tkbDTO.GioHoc.ToString();
        }

        private void pannel_Lich_Hoc_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pannel_Lich_Hoc_Click(object sender, EventArgs e)
        {

        }


    }
}
