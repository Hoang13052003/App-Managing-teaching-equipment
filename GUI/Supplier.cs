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
using BUS;

namespace GUI
{
    public partial class Supplier : Form
    {
        SupplierBUS sup = new SupplierBUS();
        public Supplier()
        {
            InitializeComponent();
            this.Load += Supplier_Load;
            this.dgvNCC.CellClick += DgvNCC_CellClick;
            this.btnThem.Click += BtnThem_Click;
            this.btnXoa.Click += BtnXoa_Click;
            this.btnSua.Click += BtnSua_Click;
            this.btnLamMoi.Click += BtnLamMoi_Click;
            this.btnSearch.Click += BtnSearch_Click;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == string.Empty) return;
            dgvNCC.DataSource = sup.SearchNhaCungCap(txtSearch.Text);
            //dgvNCC.DataSource = sup.searchNCC(txtSearch.Text);
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (sup.SuaNCC(Convert.ToInt32(txtMaNCC.Text), txtTenNCC.Text, txtDiaChi.Text, txtSDT.Text))
            {
                MessageBox.Show("Đã sửa thông tin nhà cung cấp có mã '" + txtMaNCC.Text + "'", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoi();
            }
            else
            {
                MessageBox.Show("Không tìm thấy nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Xác nhận xóa nhà cung cấp mã '" + txtMaNCC.Text + "'",
                "Thông báo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                if (sup.XoaNCC(Convert.ToInt32(txtMaNCC.Text)))
                {
                    MessageBox.Show("Đã xóa nhà cung cấp có mã '" + txtMaNCC.Text + "'", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LamMoi();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (txtTenNCC.Text == string.Empty || txtDiaChi.Text == string.Empty || txtSDT.Text == string.Empty)
            {
                MessageBox.Show("Cần nhập đầy đủ thông tin nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtMaNCC.Text != string.Empty)
            {
                if (sup.KTKC(Convert.ToInt32(txtMaNCC.Text)))
                {
                    MessageBox.Show("Mã nhà cung cấp đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            SupplierDTO ncc = new SupplierDTO();
            ncc.TenNCC = txtTenNCC.Text;
            ncc.DiaChi = txtDiaChi.Text;
            ncc.SDT = txtSDT.Text;
            if (sup.ThemNCC(ncc))
            {
                MessageBox.Show("Đã thêm nhà cung cấp mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoi();
            }
            else
            {
                MessageBox.Show("Không thêm được nhà cung cấp mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        void LamMoi()
        {
            LoadNCC();
            txtMaNCC.Text = string.Empty;
            txtTenNCC.Text = string.Empty;
            txtDiaChi.Text = string.Empty;
            txtSDT.Text = string.Empty;
        }

        private void DgvNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                BingDings(e.RowIndex);
            }
        }

        private void Supplier_Load(object sender, EventArgs e)
        {
            LoadNCC();
        }

        void LoadNCC()
        {
            //dgvNCC.DataSource = sup.getNCC();
            dgvNCC.DataSource = sup.getAll();
            dgvNCC.Columns[0].HeaderText = "Mã nhà cung cấp";
            dgvNCC.Columns[1].HeaderText = "Tên nhà cung cấp";
            dgvNCC.Columns[2].HeaderText = "Địa chỉ";
            dgvNCC.Columns[3].HeaderText = "Số điện thoại";
            dgvNCC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        public void BingDings(int rowIndex)
        {
            DataGridViewRow row = dgvNCC.Rows[rowIndex];
            txtMaNCC.Text = row.Cells[0].Value.ToString();
            txtTenNCC.Text = row.Cells[1].Value.ToString();
            txtDiaChi.Text = row.Cells[2].Value.ToString();
            txtSDT.Text = row.Cells[3].Value.ToString();
        }
    }
}
