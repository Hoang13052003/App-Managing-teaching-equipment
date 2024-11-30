using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GUI
{
    public partial class ThoiKhoaBieu : Form
    {
        //Biến
        

        //Models
        private ThoiKhoaBieuChiTiet_NguoiDungDTO _ThoiKhoaBieu_Click_Row = null;

        //BUS
        private ThoiKhoaBieuBUS _tkbBUS;
        private MonHocBUS _monHocBUS;
        private BaiHocBUS _baiHocBUS;
        private PhongHocBUS _phongHocBUS;
        private LopHocBUS _lopHocBUS;

        //List
        private List<ThoiKhoaBieuChiTiet_NguoiDungDTO> _list_TKB = new List<ThoiKhoaBieuChiTiet_NguoiDungDTO>();
        private List<MonHocDTO> _list_MH = new List<MonHocDTO>();
        private List<BaiHocDTO> _list_BH = new List<BaiHocDTO>();
        private List<PhongHocDTO> _list_PH = new List<PhongHocDTO>();
        private List<LopHocDTO> _list_LH = new List<LopHocDTO>();



        public ThoiKhoaBieu()
        {
            _tkbBUS = new ThoiKhoaBieuBUS();
            _monHocBUS = new MonHocBUS();
            _baiHocBUS = new BaiHocBUS();
            _phongHocBUS = new PhongHocBUS();
            _lopHocBUS = new LopHocBUS();
            _list_TKB = _tkbBUS.GetAllTKBChiTiet();


            InitializeComponent();
        }

        private void ThoiKhoaBieu_Load(object sender, EventArgs e)
        {
            LoadMonHocCBB();

            int maMonHoc = Convert.ToInt32(cbbMonHoc.SelectedValue);
            LoadBaiHocCBB(maMonHoc);
            LoadPhongHocCBB();
            LoadLopHocCBB();
            LoadGioHocComboBox();
            LoadDataGridView();
        }
        private void LoadMonHocCBB()
        {
            var monHocList = _monHocBUS.GetAll();
            cbbMonHoc.DataSource = monHocList;
            cbbMonHoc.DisplayMember = "TenMon";
            cbbMonHoc.ValueMember = "MaMon";
            cbbMonHoc.SelectedIndex = 0;
        }

        private void LoadBaiHocCBB(int maMon)
        {
            _list_BH = _baiHocBUS.GetAll();
            var filter = _list_BH.Where(item => item.MaMon == maMon).ToList();
            cbbBaiHoc.DataSource = filter;
            cbbBaiHoc.DisplayMember = "TenBaiHoc";
            cbbBaiHoc.ValueMember = "MaBaiHoc";
        }

        private void LoadPhongHocCBB()
        {
            var phongHocList = _phongHocBUS.GetAll();
            cbbPhongHoc.DataSource = phongHocList;
            cbbPhongHoc.DisplayMember = "TenPhong";
            cbbPhongHoc.ValueMember = "MaPhong";
        }

        private void LoadLopHocCBB()
        {
            var lopHocList = _lopHocBUS.GetAll();
            cbbLopHoc.DataSource = lopHocList;
            cbbLopHoc.DisplayMember = "TenLopHoc";
            cbbLopHoc.ValueMember = "MaLopHoc";
        }
        private void LoadGioHocComboBox()
        {
            cbbGioHoc.Items.Clear();

            cbbGioHoc.Items.Add("07:00");
            cbbGioHoc.Items.Add("08:00");
            cbbGioHoc.Items.Add("09:00");
            cbbGioHoc.Items.Add("10:00");
            cbbGioHoc.Items.Add("11:00");
            cbbGioHoc.Items.Add("13:00");
            cbbGioHoc.Items.Add("14:00");
            cbbGioHoc.Items.Add("15:00");
            cbbGioHoc.Items.Add("16:00");

            cbbGioHoc.SelectedIndex = 0;
        }

        private void LoadDataGridView()
        {
            var filteredList = _list_TKB;

            var weekStartDate = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            var weekEndDate = weekStartDate.AddDays(6);
            filteredList = filteredList.Where(tkb => tkb.NgayHoc >= weekStartDate && tkb.NgayHoc <= weekEndDate).ToList();
            // Đặt danh sách dữ liệu cho DataGridView
            dgvTKB.DataSource = filteredList;

            dgvTKB.Columns["MaTKB"].HeaderText = "Mã TKB";
            //dgvTKB.Columns["MaNguoiDung"].HeaderText = "Mã Người Dùng";
            dgvTKB.Columns["HoTen"].HeaderText = "Giáo viên";
            dgvTKB.Columns["TenMonHoc"].HeaderText = "Tên Môn Học";
            dgvTKB.Columns["TenBaiHoc"].HeaderText = "Tên Bài Học";
            dgvTKB.Columns["TenPhong"].HeaderText = "Tên Phòng";
            dgvTKB.Columns["TenLop"].HeaderText = "Tên Lớp";
            dgvTKB.Columns["GioHoc"].HeaderText = "Giờ Học";
            dgvTKB.Columns["NgayHoc"].HeaderText = "Ngày Học";
            //dgvTKB.Columns["TinhTrang"].HeaderText = "Tình Trạng";

            // Thiết lập các cột hiển thị dưới dạng thời gian hoặc ngày tháng nếu cần
            //dgvTKB.Columns["GioHoc"].DefaultCellStyle.Format = "HH:mm";
            //dgvTKB.Columns["NgayHoc"].DefaultCellStyle.Format = "yyyy-MM-dd";

            // Bạn có thể ẩn các cột nếu không muốn hiển thị chúng, ví dụ: MaMon, MaBaiHoc, MaPhong
            dgvTKB.Columns["MaNguoiDung"].Visible = false;
            dgvTKB.Columns["MaMon"].Visible = false;
            dgvTKB.Columns["MaBaiHoc"].Visible = false;
            dgvTKB.Columns["MaPhong"].Visible = false;
            dgvTKB.Columns["MaLopHoc"].Visible = false;
            dgvTKB.Columns["TinhTrang"].Visible = false;
        }

        private void LoadDataGridView_Filter()
        {
            // Lọc dữ liệu dựa trên các tiêu chí
            var filteredList = _list_TKB;

            // Lọc theo tuần
            if (dtbNgayHoc_Filter.Value != null)
            {
                var weekStartDate = dtbNgayHoc_Filter.Value.StartOfWeek(DayOfWeek.Monday);
                var weekEndDate = weekStartDate.AddDays(6);
                filteredList = filteredList.Where(tkb => tkb.NgayHoc >= weekStartDate && tkb.NgayHoc <= weekEndDate).ToList();
            }

            //// Lọc theo ngày
            //if (dtbNgayHoc.Value != null)
            //{
            //    filteredList = filteredList.Where(tkb => tkb.NgayHoc.Value.Date == dtbNgayHoc.Value.Date).ToList();
            //}

            //// Lọc theo mã môn
            //if (!string.IsNullOrEmpty(cbbMonHoc.SelectedValue.ToString()))
            //{
            //    int maMon = Convert.ToInt32(cbbMonHoc.SelectedValue);
            //    filteredList = filteredList.Where(tkb => tkb.MaMon == maMon).ToList();
            //}

            //// Lọc theo mã bài học
            //if (!string.IsNullOrEmpty(cbbBaiHoc.SelectedValue.ToString()))
            //{
            //    int maBaiHoc = Convert.ToInt32(cbbBaiHoc.SelectedValue);
            //    filteredList = filteredList.Where(tkb => tkb.MaBaiHoc == maBaiHoc).ToList();
            //}

            //// Lọc theo mã phòng
            //if (!string.IsNullOrEmpty(cbbPhongHoc.SelectedValue.ToString()))
            //{
            //    int maPhong = Convert.ToInt32(cbbPhongHoc.SelectedValue);
            //    filteredList = filteredList.Where(tkb => tkb.MaPhong == maPhong).ToList();
            //}

            //// Lọc theo mã lớp học
            //if (!string.IsNullOrEmpty(cbbLopHoc.SelectedValue.ToString()))
            //{
            //    int maLopHoc = Convert.ToInt32(cbbLopHoc.SelectedValue);
            //    filteredList = filteredList.Where(tkb => tkb.MaLopHoc == maLopHoc).ToList();
            //}

            dgvTKB.DataSource = null;
            // Cập nhật DataGridView với dữ liệu đã lọc
            dgvTKB.DataSource = filteredList;

            // Thiết lập các header cho cột
            dgvTKB.Columns["MaTKB"].HeaderText = "Mã TKB";
            dgvTKB.Columns["HoTen"].HeaderText = "Giáo viên";
            dgvTKB.Columns["TenMonHoc"].HeaderText = "Tên Môn Học";
            dgvTKB.Columns["TenBaiHoc"].HeaderText = "Tên Bài Học";
            dgvTKB.Columns["TenPhong"].HeaderText = "Tên Phòng";
            dgvTKB.Columns["TenLop"].HeaderText = "Tên Lớp";
            dgvTKB.Columns["GioHoc"].HeaderText = "Giờ Học";
            dgvTKB.Columns["NgayHoc"].HeaderText = "Ngày Học";

            // Ẩn các cột không cần thiết
            dgvTKB.Columns["MaNguoiDung"].Visible = false;
            dgvTKB.Columns["MaMon"].Visible = false;
            dgvTKB.Columns["MaBaiHoc"].Visible = false;
            dgvTKB.Columns["MaPhong"].Visible = false;
            dgvTKB.Columns["MaLopHoc"].Visible = false;
            dgvTKB.Columns["TinhTrang"].Visible = false;
        }

        // Helper method to calculate the start of the week for a given date
        


        private void cbbMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbMonHoc.SelectedValue != null && int.TryParse(cbbMonHoc.SelectedValue.ToString(), out int maMonHoc))
            { 
                LoadBaiHocCBB(maMonHoc);
            } 
        }

        private void btnThemExcel_Click(object sender, EventArgs e)
        {
            try
            {
                // Mở hộp thoại chọn file CSV
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "CSV Files|*.csv",
                    Title = "Chọn file CSV"
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    // Đảm bảo file không bị khóa và đọc nội dung
                    var lines = System.IO.File.ReadAllLines(filePath, Encoding.UTF8).Skip(1);

                    // Duyệt qua từng dòng (bỏ qua dòng tiêu đề)
                    foreach (var line in lines) // Skip header
                    {
                        // Tách giá trị và loại bỏ dấu " nếu có
                        var values = line.Split(',')
                 .Select(value => value.Trim('"')) // Loại bỏ dấu nháy kép
                 .ToArray();

                        if (values.Length >= 7) // Đảm bảo có đủ 7 cột
                        {
                            try
                            {
                                var tkb = new ThoiKhoaBieuDTO
                                {
                                    MaNguoiDung = values[0].Trim(),
                                    MaMon = int.TryParse(values[1].Trim(), out int maMon) ? (int?)maMon : null,
                                    MaBaiHoc = int.TryParse(values[2].Trim(), out int maBaiHoc) ? (int?)maBaiHoc : null,
                                    MaPhong = int.TryParse(values[3].Trim(), out int maPhong) ? (int?)maPhong : null,
                                    MaLopHoc = int.TryParse(values[4].Trim(), out int maLopHoc) ? (int?)maLopHoc : null,
                                    GioHoc = TimeSpan.TryParse(values[5].Trim(), out TimeSpan gioHoc) ? (TimeSpan?)gioHoc : null,
                                    NgayHoc = DateTime.TryParse(values[6].Trim(), CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ngayHoc) ? (DateTime?)ngayHoc : null
                                };

                                // Thêm vào cơ sở dữ liệu
                                _tkbBUS.Insert(tkb);
                            }
                            catch (Exception innerEx)
                            {
                                MessageBox.Show($"Lỗi xử lý dòng: {line}. Chi tiết lỗi: {innerEx.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Dòng dữ liệu không đủ cột: {line}", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }

                    // Làm mới dữ liệu sau khi thêm

                    MessageBox.Show("Thêm thời khóa biểu từ file CSV thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ThoiKhoaBieu_Load(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm thời khóa biểu từ file CSV: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void btnSua_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (_ThoiKhoaBieu_Click_Row == null)
        //        {
        //            MessageBox.Show("Vui lòng chọn một thời khóa biểu để sửa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }

        //        var tkb = new ThoiKhoaBieuDTO
        //        {
        //            //MaTKB = _ThoiKhoaBieu_Click_Row.MaTKB,
        //            //MaNguoiDung = txtMaNguoiDung.Text,
        //            //MaMon = Convert.ToInt32(cbbMonHoc.SelectedValue),
        //            //GioHoc = TimeSpan.Parse(cbbGioHoc.SelectedItem.ToString()),
        //            //NgayHoc = dtbNgayHoc.Value.Date
        //        };

        //        if (_tkbBUS.Update(tkb))
        //        {
        //            MessageBox.Show("Cập nhật thời khóa biểu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            ThoiKhoaBieu_Load(sender, e);
        //        }
        //        else
        //        {
        //            MessageBox.Show("Cập nhật thời khóa biểu thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Lỗi khi cập nhật thời khóa biểu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (_ThoiKhoaBieu_Click_Row == null)
                {
                    MessageBox.Show("Vui lòng chọn một thời khóa biểu để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (MessageBox.Show("Bạn có chắc chắn muốn xóa thời khóa biểu này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (_tkbBUS.Delete(_ThoiKhoaBieu_Click_Row.MaTKB))
                    {
                        MessageBox.Show("Xóa thời khóa biểu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ThoiKhoaBieu_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Xóa thời khóa biểu thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa thời khóa biểu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ThoiKhoaBieu_Load(sender, e);
        }

        private void dgvTKB_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                _ThoiKhoaBieu_Click_Row = _list_TKB[e.RowIndex];
                txtMaTKB.Text = _ThoiKhoaBieu_Click_Row.MaTKB.ToString();
                txtMaNguoiDung.Text = _ThoiKhoaBieu_Click_Row.MaNguoiDung;
                cbbMonHoc.SelectedValue = _ThoiKhoaBieu_Click_Row.MaMon;
                cbbBaiHoc.SelectedValue = _ThoiKhoaBieu_Click_Row.MaBaiHoc;
                cbbPhongHoc.SelectedValue = _ThoiKhoaBieu_Click_Row.MaPhong;
                cbbLopHoc.SelectedValue = _ThoiKhoaBieu_Click_Row.MaLopHoc;
                cbbGioHoc.SelectedItem = _ThoiKhoaBieu_Click_Row.GioHoc.Value.ToString(@"hh\:mm");
                dtbNgayHoc.Value = _ThoiKhoaBieu_Click_Row.NgayHoc ?? DateTime.Now;
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            LoadDataGridView_Filter();
        }
    }
}
