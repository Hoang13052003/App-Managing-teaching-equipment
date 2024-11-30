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
using DTO;

namespace GUI
{
    public partial class NhapThietBi : Form
    {
        NhapThietBiBUS n = new NhapThietBiBUS();
        YeuCauThietBiBUS y = new YeuCauThietBiBUS();
        public NhapThietBi()
        {
            InitializeComponent();
            this.Load += NhapThietBi_Load;
            this.dgvNhap.CellClick += DgvNhap_CellClick;
            this.btnLamMoi.Click += BtnLamMoi_Click;
            this.dgvYeuCau.CellClick += DgvYeuCau_CellClick;
        }

        private void DgvYeuCau_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            { 
                BingDingYeuCaus(e.RowIndex);
                SearchChiTietYCTB_Mua(Convert.ToInt32(dgvYeuCau.Rows[e.RowIndex].Cells[0].Value));
            }
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
            txtNguoiLap.Text= row.Cells[2].Value.ToString();
            ngayNhap.Value = Convert.ToDateTime(row.Cells[3].Value);
            txtSoLuong.Text = row.Cells[4].Value.ToString();
            txtTongTien.Text = row.Cells[5].Value.ToString();
            guna2TextBox1.Text = row.Cells[7].Value.ToString();
        }
        private void NhapThietBi_Load(object sender, EventArgs e)
        {
            LoadNhapTB();
            LoadYeuCau();
            LoadChiTietYC();
        }

        void LoadNhapTB()
        {
            dgvNhap.DataSource = n.GetAll();
            dgvNhap.Columns["MaNhap"].HeaderText = "Mã nhập";
            dgvNhap.Columns["HoTen"].HeaderText = "Người lập";
            dgvNhap.Columns["NgayNhap"].HeaderText = "Ngày lập";
            dgvNhap.Columns["SoLuong"].HeaderText = "Số lượng";
            dgvNhap.Columns["TongTien"].HeaderText = "Tổng tiền";
            dgvNhap.Columns["TenNCC"].HeaderText = "Nhà cung cấp";
            dgvNhap.Columns["MaNguoiDung"].Visible = false;
            dgvNhap.Columns["MaNCC"].Visible = false;
        }
        void LoadCTNhapTB(int maNhap)
        {
            dgvChiTietNhap.DataSource = n.GetAll(maNhap);
            dgvChiTietNhap.Columns["MaNhap"].HeaderText = "Mã nhập";
            dgvChiTietNhap.Columns["TenTB"].HeaderText = "Tên thiết bị";
            dgvChiTietNhap.Columns["GiaNhap"].HeaderText = "Giá nhập";
            dgvChiTietNhap.Columns["SoLuong"].HeaderText = "Số lượng";
            dgvChiTietNhap.Columns["ThanhTien"].HeaderText = "Thành tiền";
            dgvChiTietNhap.Columns["MaTB"].Visible = false;
        }
        void LoadYeuCau()
        {
            dgvYeuCau.DataSource = y.getAllYeuCauThietBi();
            dgvYeuCau.Columns["MaYC"].HeaderText = "Mã yêu cầu";
            dgvYeuCau.Columns["NgayYeuCau"].HeaderText = "Ngày yêu cầu";
            dgvYeuCau.Columns["TenNguoiDung"].HeaderText = "Người yêu cầu";
            dgvYeuCau.Columns["MaNguoiDung"].Visible = false;
        }

        void BingDingYeuCaus(int rowIndex)
        {
            DataGridViewRow row = dgvYeuCau.Rows[rowIndex];
            txtMaYC.Text = row.Cells[0].Value.ToString();
            txtNguoiYeuCau.Text = row.Cells[2].Value.ToString();
            ngayYeuCau.Value = Convert.ToDateTime(row.Cells[3].Value);
        }
        void SearchChiTietYCTB_Mua(int pMaYC)
        {
            dgvChiTietYC.DataSource = y.searchChiTietYeuCauMuaThietBi(pMaYC);
            dgvChiTietYC.Columns["MaYC"].HeaderText = "Mã yêu cầu";
            dgvChiTietYC.Columns["TenTB"].HeaderText = "Tên thiết bị";
            dgvChiTietYC.Columns["TenPhong"].HeaderText = "Phòng";
            dgvChiTietYC.Columns["LoaiYeuCau"].HeaderText = "Loại yêu cầu";
            dgvChiTietYC.Columns["GhiChu"].HeaderText = "Ghi chú";
            dgvChiTietYC.Columns["TrangThai"].HeaderText = "Trạng thái";
            dgvChiTietYC.Columns["MaCTTB_NCC"].Visible = false;
        }

        private bool isEventRegistered = false;
        void LoadChiTietYC()
        {
            dgvChiTietYC.DataSource = y.getAllChiTietYeuCauMuaThietBi();

            dgvChiTietYC.Columns["MaYC"].HeaderText = "Mã yêu cầu";
            dgvChiTietYC.Columns["TenTB"].HeaderText = "Tên thiết bị";
            dgvChiTietYC.Columns["TenPhong"].HeaderText = "Phòng";
            dgvChiTietYC.Columns["LoaiYeuCau"].HeaderText = "Loại yêu cầu";
            dgvChiTietYC.Columns["GhiChu"].HeaderText = "Ghi chú";
            dgvChiTietYC.Columns["TrangThai"].HeaderText = "Trạng thái";
            dgvChiTietYC.Columns["MaCTTB_NCC"].Visible = false;

            // Chỉ thêm cột button nếu chưa có
            if (!dgvChiTietYC.Columns.Contains("btnProcess"))
            {
                DataGridViewButtonColumn btnProcessColumn = new DataGridViewButtonColumn();
                btnProcessColumn.HeaderText = "Xử lý";
                btnProcessColumn.Name = "btnProcess";
                btnProcessColumn.Text = "✓ / ×";
                btnProcessColumn.UseColumnTextForButtonValue = false;
                dgvChiTietYC.Columns.Add(btnProcessColumn);
            }

            // Đăng ký sự kiện chỉ một lần
            if (!isEventRegistered)
            {
                dgvChiTietYC.CellFormatting += DgvChiTietYC_CellFormatting;
                dgvChiTietYC.CellPainting += DgvChiTietYC_CellPainting;
                dgvChiTietYC.CellContentClick += DgvChiTietYC_CellContentClick;
                isEventRegistered = true;
            }
        }


        private void DgvChiTietYC_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvChiTietYC.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                int trangThai = (int)e.Value;
                switch (trangThai)
                {
                    case 0:
                        e.Value = "Đang xử lý";
                        break;
                    case 1:
                        e.Value = "Hoàn thành";
                        break;
                    case 2:
                        e.Value = "Từ chối";
                        break;
                    default:
                        e.Value = "Không xác định";
                        break;
                }
                e.FormattingApplied = true;
            }
        }

        private void DgvChiTietYC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvChiTietYC.Columns["btnProcess"].Index && e.RowIndex >= 0)
            {
                var cellBounds = dgvChiTietYC.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                var clickLocation = dgvChiTietYC.PointToClient(Cursor.Position);

                var btn1Bounds = new Rectangle(cellBounds.Left + 5, cellBounds.Top + 5, cellBounds.Width / 2 - 10, cellBounds.Height - 10);
                var btn2Bounds = new Rectangle(cellBounds.Left + cellBounds.Width / 2 + 5, cellBounds.Top + 5, cellBounds.Width / 2 - 10, cellBounds.Height - 10);

                // Kiểm tra vị trí nhấn nút
                if (btn1Bounds.Contains(clickLocation)) // Nút "Hoàn thành"
                {
                    if (txtChiPhiSua.Text == string.Empty || txtKetQuaSua.Text == string.Empty)
                    {
                        MessageBox.Show("Cần cho biết kết quả và chi phí mua dự định trước khi xác nhận!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    int maYC = Convert.ToInt32(dgvChiTietYC.Rows[e.RowIndex].Cells["MaYC"].Value);
                    int maCTTB_NCC = Convert.ToInt32(dgvChiTietYC.Rows[e.RowIndex].Cells["MaCTTB_NCC"].Value);
                    float chiPhi = Convert.ToSingle(txtChiPhiSua.Text);

                    YeuCauThietBiDTO yctb = y.getAllYeuCauThietBi().FirstOrDefault(item => item.MaYC == maYC);
                    bool success = y.UpdataTrangThaiCTYCTB(maYC, maCTTB_NCC, yctb.MaNguoiDung, 1, txtKetQuaSua.Text, chiPhi);

                    if (success)
                    {
                        MessageBox.Show("Đã hoàn thành cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lamMoi();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thất bại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (btn2Bounds.Contains(clickLocation)) // Nút "Từ chối"
                {
                    int maYC = Convert.ToInt32(dgvChiTietYC.Rows[e.RowIndex].Cells["MaYC"].Value);
                    int maCTTB_NCC = Convert.ToInt32(dgvChiTietYC.Rows[e.RowIndex].Cells["MaCTTB_NCC"].Value);

                    bool success = y.UpdataTrangThaiCTYCTB_Mua(maYC, maCTTB_NCC, 2, "", 0);

                    if (success)
                    {
                        MessageBox.Show("Đã hoàn thành cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lamMoi();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thất bại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }

        private void DgvChiTietYC_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == dgvChiTietYC.Columns["btnProcess"].Index && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var btn1Bounds = new Rectangle(e.CellBounds.Left + 5, e.CellBounds.Top + 5, e.CellBounds.Width / 2 - 10, e.CellBounds.Height - 10);
                var btn2Bounds = new Rectangle(e.CellBounds.Left + e.CellBounds.Width / 2 + 5, e.CellBounds.Top + 5, e.CellBounds.Width / 2 - 10, e.CellBounds.Height - 10);

                Color completeButtonColor = Color.Green;
                Color rejectButtonColor = Color.Red;

                using (Brush brush = new SolidBrush(completeButtonColor))
                {
                    e.Graphics.FillRectangle(brush, btn1Bounds);
                }
                TextRenderer.DrawText(e.Graphics, "✓", e.CellStyle.Font, btn1Bounds, Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                using (Brush brush = new SolidBrush(rejectButtonColor))
                {
                    e.Graphics.FillRectangle(brush, btn2Bounds);
                }
                TextRenderer.DrawText(e.Graphics, "×", e.CellStyle.Font, btn2Bounds, Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                e.Handled = true;
            }
        }

        private void btnNhapThemTB_Click(object sender, EventArgs e)
        {
            ChonNhaCungCap_NhapHang frm = new ChonNhaCungCap_NhapHang();
            frm.ShowDialog();
        }

        private void btnLamMoi_Click_1(object sender, EventArgs e)
        {
            lamMoi();
        }


    }
}
