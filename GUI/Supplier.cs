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
        YeuCauThietBiBUS y = new YeuCauThietBiBUS();
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
            this.dgvLoaiThietBi.CellClick += DgvLoaiThietBi_CellClick;
        }

        private void DgvLoaiThietBi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgvThietBi.DataSource = sup.SearchThietBi(Convert.ToInt32(dgvLoaiThietBi.Rows[e.RowIndex].Cells[0].Value));
                dgvThietBi.Columns[0].HeaderText = "Mã thiết bị";
                dgvThietBi.Columns[1].HeaderText = "Tên thiết bị";
                dgvThietBi.Columns["MaLoai"].Visible = false;
                dgvThietBi.Columns["SoLuong"].Visible = false;
                dgvThietBi.Columns["NSX"].Visible = false;

            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == string.Empty) return;
            dgvNCC.DataSource = sup.SearchNhaCungCap(txtSearch.Text);
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
                MessageBoxIcon.Question
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
            LoadLoaiTB();
            LoadThietBi();
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

                dgvLoaiThietBi.DataSource = sup.SearchLoaiTB(Convert.ToInt32(dgvNCC.Rows[e.RowIndex].Cells[0].Value));
                dgvLoaiThietBi.Columns[0].HeaderText = "Mã loại";
                dgvLoaiThietBi.Columns[1].HeaderText = "Tên loại";
            }
        }

        private void Supplier_Load(object sender, EventArgs e)
        {
            LoadNCC();
            LoadLoaiTB();
            LoadThietBi();
        }

        void LoadNCC()
        {
            dgvNCC.DataSource = sup.getAll();
            dgvNCC.Columns[0].HeaderText = "Mã nhà cung cấp";
            dgvNCC.Columns[1].HeaderText = "Tên nhà cung cấp";
            dgvNCC.Columns[2].HeaderText = "Địa chỉ";
            dgvNCC.Columns[3].HeaderText = "Số điện thoại";
        }

        public void BingDings(int rowIndex)
        {
            DataGridViewRow row = dgvNCC.Rows[rowIndex];
            txtMaNCC.Text = row.Cells[0].Value.ToString();
            txtTenNCC.Text = row.Cells[1].Value.ToString();
            txtDiaChi.Text = row.Cells[2].Value.ToString();
            txtSDT.Text = row.Cells[3].Value.ToString();
        }
        public void LoadLoaiTB()
        {
            dgvLoaiThietBi.DataSource = y.getAllLoaiTB();
            dgvLoaiThietBi.Columns[0].HeaderText = "Mã loại";
            dgvLoaiThietBi.Columns[1].HeaderText = "Tên loại";

            //dgvLoaiThietBi.Columns[0].Width = 70; 
            //dgvLoaiThietBi.Columns[1].Width = 200;

            //dgvLoaiThietBi.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgvLoaiThietBi.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        public void LoadThietBi()
        {
            dgvThietBi.DataSource = y.getAllThietBi();
            dgvThietBi.Columns[0].HeaderText = "Mã thiết bị";
            dgvThietBi.Columns[1].HeaderText = "Tên thiết bị";

            //dgvThietBi.Columns[0].Width = 70;
            //dgvThietBi.Columns[1].Width = 200;

            //dgvThietBi.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgvThietBi.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
    }
}
