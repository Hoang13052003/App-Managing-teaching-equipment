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
    public partial class QuanLyThietBi_MonHoc_BaiHoc : Form
    {
        //Biến
        private MonHoc_BaiHoc_ThietBi_DTO _click_Row;


        //BUS
        private MonHocBUS mhBUS = new MonHocBUS();
        private BaiHocBUS bhBUS = new BaiHocBUS();

        private LoaiThietBiBUS ltbBUS = new LoaiThietBiBUS();
        private ThietBiBUS tbBUS = new ThietBiBUS();

        private MonHoc_BaiHoc_ChiTietTB_BUS monHoc_BaiHoc_ChiTietTB_BUS = new MonHoc_BaiHoc_ChiTietTB_BUS();


        //List

        //Danh sách thiết bị theo môn học bài học
        private List<BaiHocDTO> _list_BH;
        private List<ThietBiDTO> _list_TB;
        private List<MonHoc_BaiHoc_ThietBi_DTO> _list_MonHoc_BaiHoc_ThietBi;

        public QuanLyThietBi_MonHoc_BaiHoc()
        {
            InitializeComponent();
        }

        private void QuanLyThietBi_MonHoc_BaiHoc_Load(object sender, EventArgs e)
        {
            _list_BH = bhBUS.GetAll();
            _list_TB = tbBUS.GetAll();
            _list_MonHoc_BaiHoc_ThietBi = monHoc_BaiHoc_ChiTietTB_BUS.GetAllDetails();

            loadCBBLoaiThietBi();
            loadData_CBB_MonHoc();
        }


        private void loadData_CBB_MonHoc()
        {
            List<MonHocDTO> list = mhBUS.GetAll();
            cbbMonHoc.DataSource = null;
            cbbMonHoc.DataSource = list;
            cbbMonHoc.DisplayMember = "TenMon";
            cbbMonHoc.ValueMember = "MaMon";
            cbbMonHoc.SelectedIndex = 0;
        }

        private void loadCBBBaiHoc(List<BaiHocDTO> list)
        {
            cbbBaiHoc.DataSource = null;
            // Gán danh sách thiết bị vào ComboBox
            cbbBaiHoc.DataSource = list;
            cbbBaiHoc.DisplayMember = "TenBaiHoc"; // Hiển thị tên thiết bị
            cbbBaiHoc.ValueMember = "MaBaiHoc"; // Giá trị là mã thiết bị
        }

        private void loadCBBLoaiThietBi()
        {
            List<LoaiThietBiDTO> loaiThietBis = ltbBUS.LayLoaiThietBi();
            cbbLoaiTB.DataSource = null;
            cbbLoaiTB.DataSource = loaiThietBis;
            cbbLoaiTB.DisplayMember = "TenLoai";
            cbbLoaiTB.ValueMember = "MaLoai";
        }

        private void loadCBBThietBi(List<ThietBiDTO> danhSachThietBi)
        {
            cbbThietBi.DataSource = null;
            // Gán danh sách thiết bị vào ComboBox
            cbbThietBi.DataSource = danhSachThietBi;
            cbbThietBi.DisplayMember = "TenTB"; // Hiển thị tên thiết bị
            cbbThietBi.ValueMember = "MaTB"; // Giá trị là mã thiết bị
        }
        private void cbbMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbMonHoc.SelectedValue != null && int.TryParse(cbbMonHoc.SelectedValue.ToString(), out int maMon))
            {
                // Gọi BUS để lấy danh sách thiết bị theo mã loại
                var fiter = _list_BH.Where(x => x.MaMon == maMon).ToList();

                var list = _list_MonHoc_BaiHoc_ThietBi.Where(x => x.MaMH == maMon).ToList();
                // Cập nhật dữ liệu cho ComboBox thiết bị
                loadCBBBaiHoc(fiter);
                loadDGV_ThietBi_MonHoc_BaiHoc(list);
            }
        }

        private void cbbLoaiTB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbLoaiTB.SelectedValue != null && int.TryParse(cbbLoaiTB.SelectedValue.ToString(), out int maLoai))
            {
                // Gọi BUS để lấy danh sách thiết bị theo mã loại
                var fiter = _list_TB.Where(x => x.MaLoai == maLoai).ToList();

                // Cập nhật dữ liệu cho ComboBox thiết bị
                loadCBBThietBi(fiter);
            }
        }
        private void loadDGV_ThietBi_MonHoc_BaiHoc(List<MonHoc_BaiHoc_ThietBi_DTO> _list_MonHoc_BaiHoc_ChiTietTB)
        {
            dgvDSTB_MH_BH.DataSource = null;
            // Gán dữ liệu cho DataGridView từ BUS
            dgvDSTB_MH_BH.DataSource = _list_MonHoc_BaiHoc_ChiTietTB;
            dgvDSTB_MH_BH.Columns["TenMon"].HeaderText = "Tên môn học";
            dgvDSTB_MH_BH.Columns["TenBaiHoc"].HeaderText = "Tên bài học";
            dgvDSTB_MH_BH.Columns["MaTB"].HeaderText = "Mã thiết bị";
            dgvDSTB_MH_BH.Columns["TenTB"].HeaderText = "Tên thiết bị";
            dgvDSTB_MH_BH.Columns["SoLuong"].HeaderText = "Số lượng";
            dgvDSTB_MH_BH.Columns["MaMH"].Visible = false;
            dgvDSTB_MH_BH.Columns["MaBH"].Visible = false;
            dgvDSTB_MH_BH.Columns["MaLoai"].Visible = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (check_null_data())
                {
                    if (_list_MonHoc_BaiHoc_ThietBi.FirstOrDefault(x => x.MaMH == int.Parse(cbbMonHoc.SelectedValue.ToString()) && x.MaBH == int.Parse(cbbBaiHoc.SelectedValue.ToString()) && x.MaTB == int.Parse(cbbThietBi.SelectedValue.ToString())) != null)
                    {
                        MessageBox.Show("Môn học bài học này đã có thiết bị này, không thể thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        var item = new MonHoc_BaiHoc_ThietBi_DTO
                        {
                            MaMH = int.Parse(cbbMonHoc.SelectedValue.ToString()),
                            MaBH = int.Parse(cbbBaiHoc.SelectedValue.ToString()),
                            MaTB = int.Parse(cbbThietBi.SelectedValue.ToString()),
                            SoLuong = int.Parse(txtSoLuong.Value.ToString()) != 0 ? int.Parse(txtSoLuong.Value.ToString()) : 1,
                        };

                        if (monHoc_BaiHoc_ChiTietTB_BUS.Insert(item))
                        {
                            MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK);
                            QuanLyThietBi_MonHoc_BaiHoc_Load(sender, e);
                        }
                    } 
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi trong quá trình thêm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool check_null_data()
        {
            if (cbbMonHoc.SelectedValue != null)
                return true;
            if (cbbBaiHoc.SelectedValue != null)
                return true;
            if (cbbLoaiTB.SelectedValue != null)
                return true;
            if (cbbThietBi.SelectedValue != null)
                return true;
            return false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (_click_Row == null)
                {
                    MessageBox.Show("Vui lòng chọn dòng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Xác nhận xóa
                var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa thiết bị này không?",
                                                    "Xác nhận",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question);
                if (confirmResult == DialogResult.Yes)
                {
                    // Gọi BUS để xóa
                    if (monHoc_BaiHoc_ChiTietTB_BUS.Delete(_click_Row))
                    {
                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK);
                        QuanLyThietBi_MonHoc_BaiHoc_Load(sender, e); // Tải lại danh sách sau khi xóa
                        controls(true); // Kích hoạt lại controls
                    }
                    else
                    {
                        MessageBox.Show("Xóa không thành công. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi xóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (_click_Row == null)
                {
                    MessageBox.Show("Vui lòng chọn dòng cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra nếu thông tin không thay đổi
                if (_click_Row.SoLuong == int.Parse(txtSoLuong.Value.ToString()))
                {
                    MessageBox.Show("Thông tin chưa có sự thay đổi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                _click_Row.SoLuong = int.Parse(txtSoLuong.Value.ToString());

                // Xác nhận sửa
                var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn cập nhật thông tin không?",
                                                    "Xác nhận",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question);
                if (confirmResult == DialogResult.Yes)
                {
                    // Gọi BUS để sửa
                    if (monHoc_BaiHoc_ChiTietTB_BUS.Update(_click_Row))
                    {
                        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK);
                        QuanLyThietBi_MonHoc_BaiHoc_Load(sender, e); // Tải lại danh sách sau khi sửa
                        controls(true); // Kích hoạt lại controls
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật không thành công. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi cập nhật: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void controls(bool value)
        {
            cbbMonHoc.Enabled = value;
            cbbBaiHoc.Enabled = value;
            cbbLoaiTB.Enabled = value;
            cbbThietBi.Enabled = value;
            btnThem.Enabled = value;
        }

        private void dgvDSTB_MH_BH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                _click_Row = null;
                controls(false);
                // Khởi tạo đối tượng DTO để lưu thông tin dòng được chọn
                _click_Row = new MonHoc_BaiHoc_ThietBi_DTO
                {
                    MaMH = Convert.ToInt32(dgvDSTB_MH_BH.Rows[e.RowIndex].Cells["MaMH"].Value),
                    MaBH = Convert.ToInt32(dgvDSTB_MH_BH.Rows[e.RowIndex].Cells["MaBH"].Value),
                    MaTB = Convert.ToInt32(dgvDSTB_MH_BH.Rows[e.RowIndex].Cells["MaTB"].Value),
                    MaLoai = Convert.ToInt32(dgvDSTB_MH_BH.Rows[e.RowIndex].Cells["MaLoai"].Value),
                    SoLuong = Convert.ToInt32(dgvDSTB_MH_BH.Rows[e.RowIndex].Cells["SoLuong"].Value)
                };

                // Gán dữ liệu vào các ComboBox và TextBox
                cbbMonHoc.SelectedValue = _click_Row.MaMH;
                cbbBaiHoc.SelectedValue = _click_Row.MaBH;
                cbbLoaiTB.SelectedValue = _click_Row.MaLoai;
                cbbThietBi.SelectedValue = _click_Row.MaTB;
                txtSoLuong.Value = _click_Row.SoLuong;
            }
            else
            {
                // Nếu dòng không hợp lệ hoặc không có giá trị, đặt lại _click_Row
                _click_Row = null;
                MessageBox.Show("Vui lòng chọn một dòng hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            QuanLyThietBi_MonHoc_BaiHoc_Load(sender, e);
        }
    }
}
