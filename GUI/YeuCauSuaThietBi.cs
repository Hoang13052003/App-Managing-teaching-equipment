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
    public partial class YeuCauSuaThietBi : Form
    {
        YeuCauThietBiBUS y = new YeuCauThietBiBUS();
        public YeuCauSuaThietBi()
        {
            InitializeComponent();
            this.Load += YeuCauSuaThietBi_Load;
            this.cboLoaiTB.SelectedIndexChanged += CboLoaiTB_SelectedIndexChanged;
            this.cboThietBi.SelectedIndexChanged += CboThietBi_SelectedIndexChanged;
            this.btnSearch.Click += BtnSearch_Click;
            this.btnLamMoi.Click += BtnLamMoi_Click;
            this.btnGuiYeuCau.Click += BtnGuiYeuCau_Click;
            this.btnThem.Click += BtnThem_Click;
            this.btnXoa.Click += BtnXoa_Click;
            this.btnYeuCauMua.Click += BtnYeuCauMua_Click;
        }

        private void BtnYeuCauMua_Click(object sender, EventArgs e)
        {
            this.Hide();
            YeuCauMuaThietBi frm = new YeuCauMuaThietBi();
            frm.ShowDialog();
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDSThietBiSua.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvDSThietBiSua.SelectedRows)
                {
                    dgvDSThietBiSua.Rows.Remove(row);
                }
                dgvDSThietBiSua.ClearSelection();
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

                var trangThaiCell = selectedRow.Cells["TrangThai"].Value;
                if (trangThaiCell != null && trangThaiCell.ToString() == "Đang bảo dưỡng")
                {
                    MessageBox.Show("Thiết bị đang trong trạng thái bảo dưỡng và không thể thêm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; 
                }

                if (dgvDSThietBiSua.Rows.Count == 0)
                {
                    DataGridViewRow newRow = (DataGridViewRow)selectedRow.Clone();
                    foreach (DataGridViewCell cell in selectedRow.Cells)
                    {
                        newRow.Cells[cell.ColumnIndex].Value = cell.Value;
                    }
                    dgvDSThietBiSua.Rows.Add(newRow);
                }
                else
                {
                    bool exists = false;

                    foreach (DataGridViewRow row in dgvDSThietBiSua.Rows)
                    {
                        var cellValue1 = row.Cells["MaCTTB_NCC"].Value;
                        var cellValue2 = selectedRow.Cells["MaCTTB_NCC"].Value;

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
                        dgvDSThietBiSua.Rows.Add(newRow);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một thiết bị để thêm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnGuiYeuCau_Click(object sender, EventArgs e)
        {
            if (dgvDSThietBiSua.Rows.Count == 0 || dgvDSThietBiSua.Rows.Cast<DataGridViewRow>().All(row => row.IsNewRow))
            {
                MessageBox.Show("Không có thiết bị nào để gửi yêu cầu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }
            DialogResult result = MessageBox.Show(
                "Xác nhận gửi yêu cầu sửa chữa?",
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
                foreach (DataGridViewRow row in dgvDSThietBiSua.Rows)
                {
                    if (row.IsNewRow) continue; // Bỏ qua hàng mới (nếu có)

                    ChiTietYeuCauThietBiDTO chiTietYeuCauThietBiDTO = new ChiTietYeuCauThietBiDTO
                    {
                        MaCTTB_NCC = Convert.ToInt32(row.Cells["MaCTTB_NCC"].Value),
                        LoaiYeuCau = "Sửa chữa",
                        GhiChu = "Cần kiểm tra sửa chữa.",
                        TrangThai = 0
                    };
                    chiTiet.Add(chiTietYeuCauThietBiDTO);
                }
                var success = y.TaoYeuCauThietBi(yeuCauThietBiDTO, chiTiet);
                if (success)
                {
                    MessageBox.Show("Yêu cầu thiết bị đã được tạo thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LamMoi();
                    dgvDSThietBiSua.Rows.Clear();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi tạo yêu cầu thiết bị.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
        }
        void LamMoi()
        {
            LoadCboLoaiThietBi();
            LoadThietBi();
            LoadDgvDSChiTietThietBi();
        }
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == string.Empty) return;
            dgvDSChiTietThietBi.DataSource = y.SearchKeyChiTietThietBi(txtSearch.Text);
        }

        private void CboThietBi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboThietBi.SelectedValue != null && int.TryParse(cboThietBi.SelectedValue.ToString(), out int maThietBi))
            {
                SearchDgvDSChiTietThietBi(maThietBi);
            }
        }

        private void CboLoaiTB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLoaiTB.SelectedValue != null && int.TryParse(cboLoaiTB.SelectedValue.ToString(), out int maLoai))
            {
                cboThietBi.DataSource = y.SearchThietBi(maLoai);
                cboThietBi.DisplayMember = "TenTB";
                cboThietBi.ValueMember = "MaTB";
            }
        }

        private void YeuCauSuaThietBi_Load(object sender, EventArgs e)
        {
            LoadCboLoaiThietBi();
            LoadThietBi();
            LoadDgvDSChiTietThietBi();
            LoadDgvDSThietBiSua();
        }
        void LoadCboLoaiThietBi()
        {
            cboLoaiTB.DataSource = y.getAllLoaiTB();
            cboLoaiTB.DisplayMember = "TenLoai";
            cboLoaiTB.ValueMember = "MaLoai";
        }
        void LoadThietBi()
        {
            cboThietBi.DataSource = y.getAllThietBi();
            cboThietBi.DisplayMember = "TenTB";
            cboThietBi.ValueMember = "MaTB";
        }
        void SearchDgvDSChiTietThietBi(int pMaTB)
        {
            dgvDSChiTietThietBi.DataSource = y.SearchChiTietThietBi(pMaTB);
            dgvDSChiTietThietBi.Columns[0].HeaderText = "Mã chi tiết";
            dgvDSChiTietThietBi.Columns[1].HeaderText = "Tên thiết bị";
            dgvDSChiTietThietBi.Columns[2].HeaderText = "Tình trạng";
            dgvDSChiTietThietBi.Columns[3].HeaderText = "Trạng thái";
            dgvDSChiTietThietBi.Columns[4].HeaderText = "Ngày mua";
        }

        void LoadDgvDSChiTietThietBi()
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
                    case "Không sử dụng": //0
                        e.CellStyle.BackColor = Color.White;
                        e.CellStyle.ForeColor = Color.Black;
                        break;
                    case "Đang sử dụng": //1
                        e.CellStyle.BackColor = Color.LightGreen;
                        e.CellStyle.ForeColor = Color.Black;
                        break;
                    case "Đang bảo dưỡng": //2
                        e.CellStyle.BackColor = Color.Red;
                        e.CellStyle.ForeColor = Color.White;
                        break;
                }
            }
        }

        void LoadDgvDSThietBiSua()
        {
            dgvDSThietBiSua.Columns.Add("MaCTTB_NCC", "Mã chi tiết");
            dgvDSThietBiSua.Columns.Add("TenTB", "Tên thiết bị");
            dgvDSThietBiSua.Columns.Add("TinhTrang", "Tình trạng");
            dgvDSThietBiSua.Columns.Add("TrangThai", "Trạng thái");
            dgvDSThietBiSua.Columns.Add("NgayMua", "Ngày mua");
        }

    }
}
