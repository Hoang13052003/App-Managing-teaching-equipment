using BUS;
using DAL;
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
    public partial class YeuCauMuaThietBi : Form
    {
        YeuCauThietBiBUS y = new YeuCauThietBiBUS();
        SupplierBUS sup = new SupplierBUS();
        public YeuCauMuaThietBi()
        {
            InitializeComponent();
            this.Load += YeuCauMuaThietBi_Load;
            this.cboNCC.SelectedIndexChanged += CboNCC_SelectedIndexChanged;
            this.cboLoaiTB.SelectedIndexChanged += CboLoaiTB_SelectedIndexChanged;
            this.btnLamMoi.Click += BtnLamMoi_Click;
            this.btnSearch.Click += BtnSearch_Click;
            this.btnThem.Click += BtnThem_Click;
            this.btnXoa.Click += BtnXoa_Click;
            this.btnGuiYeuCau.Click += BtnGuiYeuCau_Click;
        }

        private void BtnGuiYeuCau_Click(object sender, EventArgs e)
        {
            if (dgvDSThietBiMua.Rows.Count == 0 || dgvDSThietBiMua.Rows.Cast<DataGridViewRow>().All(row => row.IsNewRow))
            {
                MessageBox.Show("Không có thiết bị nào để gửi yêu cầu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult result = MessageBox.Show(
                "Xác nhận gửi yêu cầu mua các thiết bị trong danh sách?",
                "Thông báo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                YeuCauThietBiDTO yeuCauThietBiDTO = new YeuCauThietBiDTO
                {
                    MaNguoiDung = "ND00000001",
                    NgayYeuCau = DateTime.Now
                };
                var chiTiet = new List<ChiTietYeuCauThietBiDTO>();
                foreach (DataGridViewRow row in dgvDSThietBiMua.Rows)
                {
                    if (row.IsNewRow) continue; // Bỏ qua hàng mới (nếu có)

                    ChiTietYeuCauThietBiDTO chiTietYeuCauThietBiDTO = new ChiTietYeuCauThietBiDTO
                    {
                        MaCTTB_NCC = Convert.ToInt32(row.Cells["MaCTTB_NCC"].Value),
                        LoaiYeuCau = "Mua",
                        GhiChu = "Mua thiết bị mới.",
                        TrangThai = 0
                    };
                    chiTiet.Add(chiTietYeuCauThietBiDTO);
                }
                var success = y.TaoYeuCauThietBi2(yeuCauThietBiDTO, chiTiet);
                if (success)
                {
                    MessageBox.Show("Yêu cầu thiết bị đã được tạo thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LamMoi();
                    dgvDSThietBiMua.Rows.Clear();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi tạo yêu cầu thiết bị.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        void LamMoi()
        {
            LoadLoaiTB();
            LoadNCC();
            LoadCTTB();
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDSThietBiMua.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvDSThietBiMua.SelectedRows)
                {
                    dgvDSThietBiMua.Rows.Remove(row);
                }
                dgvDSThietBiMua.ClearSelection();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một thiết bị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (dgvDSChiTietThietBi.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvDSChiTietThietBi.SelectedRows[0];

                if (dgvDSThietBiMua.Rows.Count == 0)
                {
                    DataGridViewRow newRow = (DataGridViewRow)selectedRow.Clone();
                    foreach (DataGridViewCell cell in selectedRow.Cells)
                    {
                        newRow.Cells[cell.ColumnIndex].Value = cell.Value;
                    }
                    dgvDSThietBiMua.Rows.Add(newRow);
                }
                else
                {
                    bool exists = false;

                    foreach (DataGridViewRow row in dgvDSThietBiMua.Rows)
                    {
                        var cellValue1 = row.Cells["TenTB"].Value;
                        var cellValue2 = selectedRow.Cells["TenTB"].Value;

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
                        DataGridViewRow newRow = (DataGridViewRow)selectedRow.Clone();
                        foreach (DataGridViewCell cell in selectedRow.Cells)
                        {
                            newRow.Cells[cell.ColumnIndex].Value = cell.Value;
                        }
                        dgvDSThietBiMua.Rows.Add(newRow);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một thiết bị để thêm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == string.Empty) return;
            dgvDSChiTietThietBi.DataSource = y.SearchKeyChiTietThietBi(txtSearch.Text);
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            LoadNCC();
            LoadLoaiTB();
            LoadCTTB();
        }

        private void CboLoaiTB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLoaiTB.SelectedValue != null && int.TryParse(cboLoaiTB.SelectedValue.ToString(), out int maLoai))
            {
                dgvDSChiTietThietBi.DataSource = y.SearchChiTietThietBi2(maLoai);
                dgvDSChiTietThietBi.Columns[0].HeaderText = "Mã chi tiết";
                dgvDSChiTietThietBi.Columns[1].HeaderText = "Tên thiết bị";
                dgvDSChiTietThietBi.Columns[2].HeaderText = "Tình trạng";
                dgvDSChiTietThietBi.Columns[3].HeaderText = "Trạng thái";
                dgvDSChiTietThietBi.Columns[4].HeaderText = "Ngày mua";
            }
        }

        private void CboNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboNCC.SelectedValue != null && int.TryParse(cboNCC.SelectedValue.ToString(), out int maNCC))
            {
                cboNCC.DataSource = y.SearchThietBi(maNCC);
                cboNCC.DisplayMember = "TenNCC";
                cboNCC.ValueMember = "MaNCC";
            }
        }

        private void YeuCauMuaThietBi_Load(object sender, EventArgs e)
        {
            LoadNCC();
            LoadLoaiTB();
            LoadCTTB();
            LoadDgvDSThietBiMua();
        }
        void LoadNCC()
        {
            cboNCC.DataSource = sup.getAll();
            cboNCC.DisplayMember = "TenNCC";
            cboNCC.ValueMember = "MaNCC";
        }

        void LoadLoaiTB()
        {
            cboLoaiTB.DataSource = y.getAllLoaiTB();
            cboLoaiTB.DisplayMember = "TenLoai";
            cboLoaiTB.ValueMember = "MaLoai";
        }
        void LoadDgvDSThietBiMua()
        {
            dgvDSThietBiMua.Columns.Add("MaCTTB_NCC", "Mã chi tiết");
            dgvDSThietBiMua.Columns.Add("TenTB", "Tên thiết bị");
            dgvDSThietBiMua.Columns.Add("TinhTrang", "Tình trạng");
            dgvDSThietBiMua.Columns.Add("TrangThai", "Trạng thái");
            dgvDSThietBiMua.Columns.Add("NgayMua", "Ngày mua");
        }
        void LoadCTTB()
        {
            dgvDSChiTietThietBi.DataSource = y.getAllChiTietThietBi();
            dgvDSChiTietThietBi.Columns[0].HeaderText = "Mã chi tiết";
            dgvDSChiTietThietBi.Columns[1].HeaderText = "Tên thiết bị";
            dgvDSChiTietThietBi.Columns[2].HeaderText = "Tình trạng";
            dgvDSChiTietThietBi.Columns[3].HeaderText = "Trạng thái";
            dgvDSChiTietThietBi.Columns[4].HeaderText = "Ngày mua";

            dgvDSChiTietThietBi.CellFormatting += DgvDSChiTietThietBi_CellFormatting;
        }
        private void DgvDSChiTietThietBi_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvDSChiTietThietBi.Columns[e.ColumnIndex].HeaderText == "Trạng thái")
            {
                string trangThai = e.Value.ToString();
                switch (trangThai)
                {
                    case "Không sử dụng": // 0
                        e.CellStyle.BackColor = Color.White;
                        e.CellStyle.ForeColor = Color.Black;
                        break;
                    case "Đang sử dụng": // 1
                        e.CellStyle.BackColor = Color.LightGreen;
                        e.CellStyle.ForeColor = Color.Black;
                        break;
                    case "Đang bảo dưỡng": // 2
                        e.CellStyle.BackColor = Color.Red;
                        e.CellStyle.ForeColor = Color.White;
                        break;
                }
            }
        }
    }
}
