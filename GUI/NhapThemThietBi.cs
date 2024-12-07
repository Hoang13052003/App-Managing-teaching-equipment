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
    public partial class NhapThemThietBi : Form
    {
        SupplierBUS sup = new SupplierBUS();
        NhapThietBiBUS n = new NhapThietBiBUS();
        int maNCC;
        public NhapThemThietBi(int pMaNCC)
        {
            InitializeComponent();
            this.Load += NhapThemThietBi_Load;
            maNCC = pMaNCC;
        }

        private void NhapThemThietBi_Load(object sender, EventArgs e)
        {
            txtTenNCC.Text = sup.tenNCC(maNCC);
            LoadThietBi();
            LoadThietBiNhap();
        }
        void LoadThietBi()
        {
            dgvDSThietBi.DataSource = sup.SearchThietBi_NCC(maNCC);
            dgvDSThietBi.Columns["MaTB"].HeaderText = "Mã thiết bị";
            dgvDSThietBi.Columns["TenTB"].HeaderText = "Tên thiết bị";
            dgvDSThietBi.Columns["SoLuong"].HeaderText = "Số lượng có";
            dgvDSThietBi.Columns["MaLoai"].Visible = false;
            dgvDSThietBi.Columns["NSX"].Visible = false;
        }
        void LoadThietBiNhap()
        {
            dgvDSThietBiNhap.Columns.Add("MaTB", "Mã chi tiết");
            dgvDSThietBiNhap.Columns.Add("TenTB", "Tên thiết bị");
            dgvDSThietBiNhap.Columns.Add("SoLuong", "Số lượng");
            dgvDSThietBiNhap.Columns.Add("MaLoai", "Mã loại");
            dgvDSThietBiNhap.Columns.Add("NSX", "NSX");
            dgvDSThietBiNhap.Columns["MaLoai"].Visible = false;
            dgvDSThietBiNhap.Columns["NSX"].Visible = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (dgvDSThietBi.SelectedRows.Count > 0 )
            {
                if (txtSoLuong.Value <= 0 || txtSoLuong.Value.ToString() == string.Empty)
                {
                    MessageBox.Show("Vui lòng nhập số lượng cần nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    DataGridViewRow selectedRow = dgvDSThietBi.SelectedRows[0];

                    DataGridViewRow newRow = new DataGridViewRow();
                    newRow.CreateCells(dgvDSThietBiNhap);

                    for (int i = 0; i < selectedRow.Cells.Count; i++)
                    {
                        newRow.Cells[i].Value = selectedRow.Cells[i].Value;
                    }

                    newRow.Cells[2].Value = txtSoLuong.Value.ToString();

                    bool exists = false;
                    foreach (DataGridViewRow row in dgvDSThietBiNhap.Rows)
                    {
                        var cellValue1 = row.Cells["MaTB"].Value;
                        var cellValue2 = selectedRow.Cells["MaTB"].Value;

                        if (cellValue1 == null || cellValue2 == null)
                            continue;

                        if (cellValue1.ToString() == cellValue2.ToString())
                        {
                            exists = true;
                            break;
                        }
                    }

                    if (!exists)
                    {
                        dgvDSThietBiNhap.Rows.Add(newRow);
                    }
                    txtSoLuong.Value = 0;
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một thiết bị để nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDSThietBiNhap.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvDSThietBiNhap.SelectedRows)
                {
                    // Xóa dòng trong DataGridView
                    dgvDSThietBiNhap.Rows.Remove(row);
                }
                dgvDSThietBiNhap.ClearSelection();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một thiết bị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGuiYeuCau_Click_1(object sender, EventArgs e)
        {
            if (dgvDSThietBiNhap.Rows.Count == 0)
            {
                MessageBox.Show("Danh sách thiết bị cần nhập đang trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Xác nhận tạo phiếu nhập?",
                "Thông báo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                NhapThietBiDTO nhapThietBi = new NhapThietBiDTO
                {
                    MaNguoiDung = "ND00000002",
                    NgayNhap = DateTime.Now,
                    SoLuong = dgvDSThietBiNhap.Rows.Count,
                    TongTien = 1,
                    MaNCC = maNCC
                };

                // Tạo danh sách ChiTietNhapDTO
                List<ChiTietNhapDTO> chiTietList = new List<ChiTietNhapDTO>();
                foreach (DataGridViewRow row in dgvDSThietBiNhap.Rows)
                {
                    if (row.Cells["MaTB"].Value != null)
                    {
                        chiTietList.Add(new ChiTietNhapDTO
                        {
                            MaTB = Convert.ToInt32(row.Cells["MaTB"].Value),
                            GiaNhap = 1,
                            SoLuong = Convert.ToInt32(row.Cells["SoLuong"].Value),
                            ThanhTien = 1
                        });
                    }
                }

                bool isSuccess = n.InsertNhapThietBi(nhapThietBi, chiTietList);

                if (isSuccess)
                {
                    MessageBox.Show("Đã tạo phiếu nhập thiết bị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvDSThietBiNhap.Rows.Clear();
                }
                else
                {
                    MessageBox.Show("Đã xảy ra lỗi khi tạo phiếu nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
