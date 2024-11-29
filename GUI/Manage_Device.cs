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
    public partial class Manage_Device : Form
    {
        ThietBiBUS bus = new ThietBiBUS();
        LoaiThietBiBUS ltb = new LoaiThietBiBUS();
        public Manage_Device()
        {
            InitializeComponent();
            this.Load += Manage_Device_Load;
            dgvDSTB.CellClick += DgvDSTB_CellClick;
            btnTimKiem.Click += BtnTimKiem_Click;
            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnLamMoi.Click += BtnLamMoi_Click;
            txtMaTB.Enabled = false;
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

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa thiết bị này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (dgvDSTB.SelectedRows[0].Cells["MaTB"].Value == null)
                {
                    MessageBox.Show("Không thể lấy mã thiết bị. Vui lòng chọn một dòng hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int maTB = Convert.ToInt32(dgvDSTB.SelectedRows[0].Cells["MaTB"].Value);
                bool isSuccess = bus.DeleteSevice(maTB);

                if (isSuccess)
                {
                    MessageBox.Show("Xóa thiết bị thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataToDataGridView();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Xóa thiết bị không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void BtnSua_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn sửa thiết bị này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {

                if (dgvDSTB.SelectedRows[0].Cells["MaTB"].Value == null)
                {
                    MessageBox.Show("Không thể lấy mã thiết bị. Vui lòng chọn một dòng hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; 
                }

                // Lấy mã môn học từ dòng đã chọn
                int maTB = Convert.ToInt32(dgvDSTB.SelectedRows[0].Cells["MaTB"].Value);
                string tenTB = txtTenTB.Text;
                int maLoai = Convert.ToInt32(cbbLoaiTB.SelectedValue);
                string nsx = txtNSX.Text;
                int soLuong = Convert.ToInt32(txtSL.Text);

                bool isSuccess = bus.UpdateSevice(maTB, tenTB, maLoai, nsx, soLuong);

                if (isSuccess)
                {
                    MessageBox.Show("Sửa thiết bị thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataToDataGridView();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Sửa thiết bị không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenTB.Text) || string.IsNullOrWhiteSpace(txtSL.Text) ||
                cbbLoaiTB.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin thiết bị!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo đối tượng ThietBiDTO mới
            ThietBiDTO thietBi = new ThietBiDTO
            {
                TenTB = txtTenTB.Text,
                SoLuong = int.Parse(txtSL.Text),
                MaLoai = Convert.ToInt32(cbbLoaiTB.SelectedValue ?? 0),
                NSX = txtNSX.Text
            };


            // Tạo đối tượng ThietBiBUS
            ThietBiBUS thietBiBUS = new ThietBiBUS();

            // Gọi phương thức thêm thiết bị
            bool isSuccess = thietBiBUS.Add(thietBi);

            // Kiểm tra kết quả thêm
            if (isSuccess)
            {
                MessageBox.Show("Thêm thiết bị thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
                LoadDataToDataGridView();
            }
            else
            {
                MessageBox.Show("Thêm thiết bị thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hàm để làm trống các trường nhập liệu
        private void ClearFields()
        {
            txtTenTB.Clear();
            txtSL.Clear();
            txtNSX.Clear();
            txtTimKiem.Clear();
            txtMaTB.Text = string.Empty;
            txtMaTB.ReadOnly = true;
        }


        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            string tenTB = txtTimKiem.Text.Trim();
            List<ThietBiDTO> results = bus.Search(tenTB);

            // Kiểm tra nếu không có kết quả
            if (results == null || results.Count == 0)
            {
                MessageBox.Show("Không tìm thấy thiết bị nào với tên đã nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvDSTB.DataSource = null; // Hoặc bạn có thể làm trống DataGridView

            }
            else
            {
                // Hiển thị kết quả nếu có
                dgvDSTB.DataSource = results;
                ClearFields();
            }
        }


        private void Manage_Device_Load(object sender, EventArgs e)
        {
            LoadDataToDataGridView();
            loadCBB_LoaiTB();
        }

        private void LoadDataToDataGridView()
        {
            var listThietBi = bus.GetAll();
            dgvDSTB.DataSource = listThietBi;
            dgvDSTB.Columns["MaTB"].HeaderText = "Mã thiết bị";
            dgvDSTB.Columns["TenTB"].HeaderText = "Tên thiết bị";
            dgvDSTB.Columns["MaLoai"].HeaderText = "Mã loại";
            dgvDSTB.Columns["NSX"].HeaderText = "Nhà sản xuất";
            dgvDSTB.Columns["SoLuong"].HeaderText = "Số lượng";
        }

        private void loadCBB_LoaiTB()
        {
            List<LoaiThietBiDTO> loaiThietBis = ltb.LayLoaiThietBi();
            cbbLoaiTB.DataSource = loaiThietBis; // Gán danh sách vào ComboBox
            cbbLoaiTB.DisplayMember = "TenLoai"; // Tên hiển thị
            cbbLoaiTB.ValueMember = "MaLoai"; // Giá trị thực

        }

        private void DgvDSTB_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy dòng đã chọn
                DataGridViewRow row = dgvDSTB.Rows[e.RowIndex];

                // Bind dữ liệu vào các điều khiển
                txtMaTB.Text = row.Cells["MaTB"].Value.ToString();
                txtTenTB.Text = row.Cells["TenTB"].Value.ToString();
                txtSL.Text = row.Cells["SoLuong"].Value.ToString();
                cbbLoaiTB.SelectedValue = row.Cells["MaLoai"].Value;
                txtNSX.Text = row.Cells["NSX"].Value.ToString();
            }
        }

    }
}
