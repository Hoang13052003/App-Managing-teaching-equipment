using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using BUS;
using DTO;
namespace GUI
{
    public partial class Show_Subjects : Form
    {
        public ThoiKhoaBieuChiTietDTO _tkbDTO { get; set; }
        public Color _color { get; set; }

        public Show_Subjects()
        {
            InitializeComponent();
        }
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

            if(new ThoiKhoaBieuBUS().GetByID(_tkbDTO.MaTKB).NgayHoc < DateTime.Now)
            {
                pannel_Lich_Hoc.FillColor = Color.FromArgb(179,200, 207);
                pannel_Lich_Hoc.Enabled = false;
            }
            else
            {
                pannel_Lich_Hoc.FillColor = _color;
            }
        }

        private void pannel_Lich_Hoc_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pannel_Lich_Hoc_Click(object sender, EventArgs e)
        {
            FormTask.OpenFormInPanel<formThietBiDayHoc_MonHoc_YeuCauMuonThemThietBi>(FormTask.Pannel_change, _tkbDTO);
            Form x = new formThietBiDayHoc_MonHoc_YeuCauMuonThemThietBi();
            FormTask.LbNameForm.Text = x.Text;
        }


    }
}
