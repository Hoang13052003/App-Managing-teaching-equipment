using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormLoaiTB_MonHoc : Form
    {
        ThietBiBUS bus = new ThietBiBUS();
        LoaiThietBiBUS ltb = new LoaiThietBiBUS();
        MonHocBUS monHoc = new MonHocBUS();

        public FormLoaiTB_MonHoc()
        {
            InitializeComponent();
            this.Load += FormLoaiTB_MonHoc_Load;
            dgvDSMonHoc.CellClick += DgvDSMonHoc_CellClick;
            btnThem.Click += BtnThem_Click;
            btnXoa.Click += BtnXoa_Click;
            btnSua.Click += BtnSua_Click;
            btnLamMoi.Click += BtnLamMoi_Click;
            btnThemLTB_MH.Click += BtnThemLTB_MH_Click;
            btnXoaLTB_MH.Click += BtnXoaLTB_MH_Click;
            txtMaMH.Enabled = false;
            dgvDSLoaiTB_MH.CellClick += DgvDSLoaiTB_MH_CellClick;

        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void LoadTenTBByMaLoai(int maLoai)
        {

            List<ThietBiDTO> thietBis = bus.GetAllByID(maLoai);

            // Cập nhật ComboBox
            cbbTenTB.DataSource = thietBis;
            cbbTenTB.DisplayMember = "TenTB";
            cbbTenTB.ValueMember = "MaTB";


            if (thietBis.Count == 0)
            {
                cbbTenTB.DataSource = null;
            }
        }

        private void BtnXoaLTB_MH_Click(object sender, EventArgs e)
        {

            if (dgvDSLoaiTB_MH.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn loại thiết bị môn học cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa loại thiết bị môn học đã chọn?",
                                          "Xác nhận xóa",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);


            if (result == DialogResult.Yes)
            {

                int maLoai = Convert.ToInt32(dgvDSLoaiTB_MH.SelectedRows[0].Cells["MaLoai"].Value);
                int maMon = Convert.ToInt32(dgvDSLoaiTB_MH.SelectedRows[0].Cells["MaMon"].Value);


                bool isDeleted = ltb.XoaMonHocLoaiTB(maLoai, maMon);


                if (isDeleted)
                {
                    MessageBox.Show("Xóa loại thiết bị môn học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataToDGVDSLTB_MH();
                    loadCBB_MonHoc();
                }
                else
                {
                    MessageBox.Show("Xóa loại thiết bị môn học thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void DgvDSLoaiTB_MH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDSLoaiTB_MH.Rows[e.RowIndex];
                cbbTenMH.SelectedValue = row.Cells["MaMon"].Value;
                cbbTenMH.Text = row.Cells["TenMon"].Value.ToString();
                cbbTenLoaiTB.SelectedValue = row.Cells["MaLoai"].Value;
                cbbTenLoaiTB.Text = row.Cells["TenLoai"].Value.ToString();
                int maLoai = Convert.ToInt32(row.Cells["MaLoai"].Value);
                LoadTenTBByMaLoai(maLoai);
            }

        }

        private void BtnThemLTB_MH_Click(object sender, EventArgs e)
        {
            if (cbbTenMH.SelectedValue == null || cbbTenLoaiTB.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn môn học và loại thiết bị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maMH = Convert.ToInt32(cbbTenMH.SelectedValue);
            int maLoaiTB = Convert.ToInt32(cbbTenLoaiTB.SelectedValue);

            bool isSaved = ltb.LuuMonHocLoaiTB(maMH, maLoaiTB);

            if (isSaved)
            {
                MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataToDGVDSLTB_MH();
                loadCBB_MonHoc();
            }
            else
            {
                MessageBox.Show("Lưu thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (dgvDSMonHoc.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một môn học để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn sửa thiết bị này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dgvDSMonHoc.SelectedRows[0].Cells["MaMon"].Value == null)
            {
                MessageBox.Show("Không thể lấy mã môn học. Vui lòng chọn một dòng hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy mã môn học từ dòng đã chọn
            int maMH = Convert.ToInt32(dgvDSMonHoc.SelectedRows[0].Cells["MaMon"].Value);
            string tenMonHocMoi = txtTenMon.Text.Trim();

            if (string.IsNullOrEmpty(tenMonHocMoi))
            {
                MessageBox.Show("Vui lòng nhập tên môn học mới.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tenMonHocHienTai = dgvDSMonHoc.SelectedRows[0].Cells["TenMon"].Value.ToString();


            if (tenMonHocMoi.Equals(tenMonHocHienTai, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Tên môn học mới không thay đổi. Vui lòng nhập tên khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MonHocDTO mh = new MonHocDTO { MaMon = maMH, TenMon = tenMonHocMoi };
            bool isUpdated = monHoc.Update(mh);

            if (isUpdated)
            {
                MessageBox.Show("Sửa môn học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Tải lại danh sách môn học sau khi sửa thành công
                LoadDataToDataDGVMonHoc();
                loadCBB_MonHoc();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Sửa môn học thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDSMonHoc.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một môn học để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa môn học này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (dgvDSMonHoc.SelectedRows[0].Cells["MaMon"].Value == null)
                {
                    MessageBox.Show("Không thể lấy mã môn học. Vui lòng chọn một dòng hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                int maMH = Convert.ToInt32(dgvDSMonHoc.SelectedRows[0].Cells["MaMon"].Value);
                // Gọi phương thức xóa
                bool isSuccess = monHoc.Delete(maMH);

                if (isSuccess)
                {
                    MessageBox.Show("Xóa môn học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataToDataDGVMonHoc();
                    loadCBB_MonHoc();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Xóa môn học không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            string tenMon = txtTenMon.Text.Trim();

            if (string.IsNullOrEmpty(tenMon))
            {
                MessageBox.Show("Vui lòng nhập tên môn học.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (monHoc.CheckMonHocExists(tenMon))
            {
                MessageBox.Show("Tên môn học đã tồn tại. Vui lòng nhập tên khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MonHocDTO mhoc = new MonHocDTO { TenMon = tenMon };
            bool result = monHoc.Insert(mhoc);

            if (result)
            {
                MessageBox.Show("Thêm loại thiết bị thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadCBB_MonHoc();
                LoadDataToDataDGVMonHoc();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Thêm loại thiết bị thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            txtTenMon.Clear();
            txtMaMH.Text = string.Empty;
            txtMaMH.ReadOnly = true;
        }



        private void DgvDSMonHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.RowIndex < dgvDSMonHoc.Rows.Count)
            {
                DataGridViewRow row = dgvDSMonHoc.Rows[e.RowIndex];
                txtMaMH.Text = row.Cells["MaMon"].Value?.ToString() ?? string.Empty;
                txtTenMon.Text = row.Cells["TenMon"].Value?.ToString() ?? string.Empty;
            }
        }

        private void FormLoaiTB_MonHoc_Load(object sender, EventArgs e)
        {
            loadCBB_MonHoc();
            LoadDataToDataDGVMonHoc();
            loadCBB_LoaiTB();
            LoadDataToDGVDSLTB_MH();
        }

        private void loadCBB_MonHoc()
        {
            List<MonHocDTO> mh = monHoc.GetAll();
            cbbTenMH.DataSource = mh;
            cbbTenMH.DisplayMember = "TenMon";
            cbbTenMH.ValueMember = "MaMon";

        }

        private void loadCBB_LoaiTB()
        {
            List<LoaiThietBiDTO> loaiThietBis = ltb.LayLoaiThietBi();
            cbbTenLoaiTB.DataSource = loaiThietBis;
            cbbTenLoaiTB.DisplayMember = "TenLoai";
            cbbTenLoaiTB.ValueMember = "MaLoai";

        }
        private void LoadDataToDataDGVMonHoc()
        {
            var mh = monHoc.GetAll();
            dgvDSMonHoc.DataSource = mh;


        }
        private void LoadDataToDGVDSLTB_MH()
        {
            DataTable dataTable = ltb.LayDanhSachMonHocLoaiTB();
            dgvDSLoaiTB_MH.DataSource = dataTable;

            // Đặt tiêu đề cho các cột nếu cần
            dgvDSLoaiTB_MH.Columns["MaMon"].HeaderText = "Mã Môn Học";
            dgvDSLoaiTB_MH.Columns["TenMon"].HeaderText = "Tên Môn Học";
            dgvDSLoaiTB_MH.Columns["MaLoai"].HeaderText = "Mã Loại Thiết Bị";
            dgvDSLoaiTB_MH.Columns["TenLoai"].HeaderText = "Tên Loại Thiết Bị";
        }

    }
}
