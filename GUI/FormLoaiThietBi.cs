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
using BUS;

namespace GUI
{
    public partial class FormLoaiThietBi : Form
    {
        LoaiThietBiBUS ltb = new LoaiThietBiBUS();
        public FormLoaiThietBi()
        {
            InitializeComponent();
            dgvDSLoaiTB.CellClick += DgvDSLoaiTB_CellClick;
            txtMaLoaiTB.Enabled = false;
            btnTimKiem.Click += BtnTimKiem_Click;
            btnDong.Click += BtnDong_Click;
            btnLamMoi.Click += BtnLamMoi_Click;
            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
        }

        private void DgvDSLoaiTB_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvDSLoaiTB.Rows.Count) // Kiểm tra chỉ số hàng hợp lệ
            {
                // Lấy dòng đã chọn
                DataGridViewRow row = dgvDSLoaiTB.Rows[e.RowIndex];

                // Gán giá trị cho các điều khiển
                txtMaLoaiTB.Text = row.Cells["MaLoai"].Value?.ToString() ?? string.Empty;
                txtTenLoai.Text = row.Cells["TenLoai"].Value?.ToString() ?? string.Empty;
            }
        }


        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDSLoaiTB.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn loại thiết bị cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy mã loại từ dòng được chọn trong DataGridView
            int maLoai = Convert.ToInt32(dgvDSLoaiTB.SelectedRows[0].Cells["MaLoai"].Value);

            // Xác nhận xóa
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa loại thiết bị này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                bool isDeleted = ltb.DeleteSevice(maLoai);

                if (isDeleted)
                {
                    MessageBox.Show("Xóa loại thiết bị thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataToDataGridView();
                }
                else
                {
                    MessageBox.Show("Xóa loại thiết bị thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (dgvDSLoaiTB.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn loại thiết bị cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maLoai = Convert.ToInt32(dgvDSLoaiTB.SelectedRows[0].Cells["MaLoai"].Value);
            string tenLoaiMoi = txtTenLoai.Text.Trim();

            if (string.IsNullOrEmpty(tenLoaiMoi))
            {
                MessageBox.Show("Vui lòng nhập tên loại thiết bị mới.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy tên loại hiện tại
            string tenLoaiHienTai = dgvDSLoaiTB.SelectedRows[0].Cells["TenLoai"].Value.ToString();

            // Kiểm tra xem tên loại mới có khác tên loại hiện tại không
            if (tenLoaiMoi.Equals(tenLoaiHienTai, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Tên loại thiết bị mới không thay đổi. Vui lòng nhập tên khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            bool isUpdated = ltb.UpdateSevice(maLoai, tenLoaiMoi);

            if (isUpdated)
            {
                MessageBox.Show("Sửa loại thiết bị thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Tải lại danh sách loại thiết bị sau khi sửa thành công
                LoadDataToDataGridView();
            }
            else
            {
                MessageBox.Show("Sửa loại thiết bị thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void BtnThem_Click(object sender, EventArgs e)
        {
            string tenLoaiTB = txtTenLoai.Text.Trim();

            if (string.IsNullOrEmpty(tenLoaiTB))
            {
                MessageBox.Show("Vui lòng nhập tên loại thiết bị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LoaiThietBiDTO loaiThietBi = new LoaiThietBiDTO { TenLoai = tenLoaiTB };
            bool result = ltb.Add(loaiThietBi);

            if (result)
            {
                MessageBox.Show("Thêm loại thiết bị thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Tải lại danh sách loại thiết bị sau khi thêm thành công
                LoadDataToDataGridView();
            }
            else
            {
                MessageBox.Show("Thêm loại thiết bị thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            ClearFields();
            LoadDataToDataGridView();
        }

        private void BtnDong_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đóng form này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }

        }

        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            string tenTB = txtTimKiem.Text.Trim();
            List<LoaiThietBiDTO> results = ltb.Search(tenTB);

            // Kiểm tra nếu không có kết quả
            if (results == null || results.Count == 0)
            {
                MessageBox.Show("Không tìm thấy thiết bị nào với tên đã nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvDSLoaiTB.DataSource = null; // Hoặc bạn có thể làm trống DataGridView

            }
            else
            {
                // Hiển thị kết quả nếu có
                dgvDSLoaiTB.DataSource = results;
                ClearFields();
            }
        }
        private void ClearFields()
        {
            txtTenLoai.Clear();
            txtTimKiem.Clear();
            txtMaLoaiTB.Text = string.Empty;
            txtMaLoaiTB.ReadOnly = true;
        }
        private void FormLoaiThietBi_Load(object sender, EventArgs e)
        {
            LoadDataToDataGridView();
        }
        private void LoadDataToDataGridView()
        {
            var listThietBi = ltb.LayLoaiThietBi();
            dgvDSLoaiTB.DataSource = listThietBi;


        }


    }
}
