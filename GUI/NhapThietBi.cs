using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;

namespace GUI
{
    public partial class NhapThietBi : Form
    {
        NhapThietBiBUS n = new NhapThietBiBUS();
        public NhapThietBi()
        {
            InitializeComponent();
            this.Load += NhapThietBi_Load;
            this.dgvNhap.CellClick += DgvNhap_CellClick;
            this.btnLamMoi.Click += BtnLamMoi_Click;
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            lamMoi();
        }

        void lamMoi()
        {
            txtMaNhap.ResetText();
            txtTongTien.ResetText();
            txtSoLuong.Value = 0;
            txtTongTien.ResetText();
        }

        private void DgvNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                BingDings(e.RowIndex);
                int maNhap = Convert.ToInt32(dgvNhap.Rows[e.RowIndex].Cells[0].Value.ToString());
                LoadCTNhapTB(maNhap);
            }
        }
        void BingDings(int rowIndex)
        {
            DataGridViewRow row = dgvNhap.Rows[rowIndex];
            txtMaNhap.Text = row.Cells[0].Value.ToString();
            txtSoLuong.Text = row.Cells[3].Value.ToString();
            ngayNhap.Value = Convert.ToDateTime(row.Cells[2].Value);
            txtTongTien.Text = row.Cells[4].Value.ToString();
        }
        private void NhapThietBi_Load(object sender, EventArgs e)
        {
            LoadNhapTB();
        }

        void LoadNhapTB()
        {
            dgvNhap.DataSource = n.GetAll();
        }
        void LoadCTNhapTB(int maNhap)
        {
            dgvChiTietNhap.DataSource = n.GetAll(maNhap);
        }
    }
}
