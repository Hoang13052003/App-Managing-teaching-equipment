﻿using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using OfficeOpenXml;

namespace GUI
{
    public partial class FormMuonThietBi : Form
    {
        //Biến


        //Models
        private MuonThietBiDTO _MuonThietBi_Click_Row = null;

        //BUS
        private MuonThietBiBUS mtbBUS = new MuonThietBiBUS();
        private ChiTietMuonThietBiBUS ctmtbBUS = new ChiTietMuonThietBiBUS();
        private ThongTinCaNhanBUS ttcnBUS = new ThongTinCaNhanBUS();

        private ChiTietThietBi_ThietBiBUS cttbBUS = new ChiTietThietBi_ThietBiBUS();

        //List
        private List<MuonThietBiDTO> _list_DS_PhieuMuon = new List<MuonThietBiDTO>();
        private List<ChiTietMuonThietBiDTO> _list_ChiTiet_MuonTB = new List<ChiTietMuonThietBiDTO>();

        public FormMuonThietBi()
        {
            InitializeComponent();
        }

        //Funsion
        private void LoadTinhTrangComboBox()
        {
            cbbTinhTrang.Items.Clear();
            cbbTinhTrang.Items.Add("Chưa trả");
            cbbTinhTrang.Items.Add("Đã trả");
            cbbTinhTrang.Items.Add("Trả thiếu");
            cbbTinhTrang.Items.Add("Trả muộn");
            cbbTinhTrang.SelectedIndex = 0; // Chọn "Chưa trả" là mặc định
        }

        private void Load_CBB_Filter_TinhTrang()
        {
            cbb_Filter_TinhTrang.Items.Clear();
            cbb_Filter_TinhTrang.Items.Add("Tất cả");
            cbb_Filter_TinhTrang.Items.Add("Chưa trả");
            cbb_Filter_TinhTrang.Items.Add("Đã trả");
            cbb_Filter_TinhTrang.Items.Add("Trả thiếu");
            cbb_Filter_TinhTrang.Items.Add("Trả muộn");
            cbb_Filter_TinhTrang.SelectedIndex = 0; // Chọn "Chưa trả" là mặc định
        }
        
        private void Load_CBB_Filter_TrangThai()
        {
            cbb_filter_TrangThai.Items.Clear(); 
            cbb_filter_TrangThai.Items.Add("Chưa duyệt");
            cbb_filter_TrangThai.Items.Add("Đã duyệt");
            cbb_filter_TrangThai.SelectedIndex = 0; // Chọn "Chưa trả" là mặc định
        }
        
        private void LoadData_DGV_DSPhieuMuon(List<MuonThietBiDTO> _list_DS_PhieuMuon, string _Tinh_Trang, bool _Trang_Thai)
        {
            var data = _list_DS_PhieuMuon.Where(item => item.TrangThai == _Trang_Thai).ToList();
            if (_Tinh_Trang != null)
            {
                data = data.Where(item => string.Equals(item.TinhTrangTraTB, _Tinh_Trang)).ToList();
            }

            // Tạo một danh sách mới với cột trạng thái được ánh xạ
            var modifiedData = data.Select(item => new
            {
                item.MaMuon,
                item.MaNguoiDung,
                item.MaTKB,
                item.NgayMuon,
                item.NgayTra,
                item.TinhTrangTraTB,
                item.TrangThai,
                TrangThaiHienThi = item.TrangThai ? "Đã duyệt" : "Chưa duyệt", // Thay đổi giá trị TrangThai
                item.GhiChuTraThietBi
            }).ToList();

            

            // Gán dữ liệu vào DataGridView
            dgvDSPhieuMuon.DataSource = modifiedData;

            // Thiết lập tiêu đề cột
            dgvDSPhieuMuon.Columns["MaMuon"].HeaderText = "Mã mượn";
            dgvDSPhieuMuon.Columns["MaNguoiDung"].HeaderText = "Mã người dùng";
            dgvDSPhieuMuon.Columns["MaTKB"].HeaderText = "Mã TKB";
            dgvDSPhieuMuon.Columns["NgayMuon"].HeaderText = "Ngày mượn";
            dgvDSPhieuMuon.Columns["NgayTra"].HeaderText = "Ngày trả";
            dgvDSPhieuMuon.Columns["TinhTrangTraTB"].HeaderText = "Tình trạng trả";
            dgvDSPhieuMuon.Columns["TrangThai"].Visible = false;
            dgvDSPhieuMuon.Columns["TrangThaiHienThi"].HeaderText = "Trạng thái"; // Cột hiển thị
            dgvDSPhieuMuon.Columns["GhiChuTraThietBi"].Visible = false;
        }

        private void loadData_DGV_DS_ThietBi(List<ChiTietMuonThietBiDTO> _list_ChiTiet_MuonTB)
        {
            // Gán dữ liệu cho DataGridView từ BUS
            dgv_DSTB.DataSource = GetUniqueThietBiList(_list_ChiTiet_MuonTB);
            dgv_DSTB.Columns["MaTB"].HeaderText = "Mã thiết bị";
            dgv_DSTB.Columns["TenTB"].HeaderText = "Tên thiết bị";
            dgv_DSTB.Columns["SoLuong"].HeaderText = "Số lượng";
        }

        public List<ChiTietMuonThietBiDTO_ThietBi> GetUniqueThietBiList(List<ChiTietMuonThietBiDTO> _list_ChiTiet_MuonTB)
        {
            // Sử dụng LINQ để nhóm theo MaTB và tính số lượng
            var groupedResult = _list_ChiTiet_MuonTB
                .GroupBy(tb => new { tb.MaTB, tb.TenTB }) // Nhóm theo MaTB và TenTB
                .Select(group => new ChiTietMuonThietBiDTO_ThietBi
                {
                    MaTB = group.Key.MaTB,                 // Lấy MaTB
                    TenTB = group.Key.TenTB,               // Lấy TenTB
                    SoLuong = group.Count()         // Lấy MaCTTB bất kỳ từ nhóm
                })
                .ToList();

            return groupedResult;
        }

        private void loadData_DGV_DS_ChiTietThietBi(List<ChiTietMuonThietBiDTO> _list_ChiTiet_MuonTB)
        {
            // Gán dữ liệu cho DataGridView từ BUS
            dgv_DSChiTietThietBi.DataSource = _list_ChiTiet_MuonTB;
            dgv_DSChiTietThietBi.Columns["MaCTTB"].HeaderText = "Mã chi tiết TB";
            dgv_DSChiTietThietBi.Columns["MaTB"].HeaderText = "Mã thiết bị";
            dgv_DSChiTietThietBi.Columns["TenTB"].HeaderText = "Tên thiết bị";
            dgv_DSChiTietThietBi.Columns["MaMuon"].Visible = false;
            dgv_DSChiTietThietBi.Columns["TrangThai"].Visible = false;
        }
        private void loadData_DGV_DS_ChiTietThietBi_TrangThai(List<ChiTietMuonThietBiDTO> _list_ChiTiet_MuonTB)
        {
            dgv_DSChiTietThietBi.DataSource = null;
            // Gán dữ liệu cho DataGridView từ BUS
            dgv_DSChiTietThietBi.DataSource = _list_ChiTiet_MuonTB;
            dgv_DSChiTietThietBi.Columns["MaCTTB"].HeaderText = "Mã chi tiết TB";
            dgv_DSChiTietThietBi.Columns["MaTB"].HeaderText = "Mã thiết bị";
            dgv_DSChiTietThietBi.Columns["TenTB"].HeaderText = "Tên thiết bị";
            dgv_DSChiTietThietBi.Columns["MaMuon"].Visible = false;
            dgv_DSChiTietThietBi.Columns["TrangThai"].HeaderText = "Trạng thái";
        }

        //Controls
        private void FormMuonThietBi_Load(object sender, EventArgs e)
        {
            //Load data
            _list_DS_PhieuMuon = mtbBUS.GetAll();

            //Controls
            cbbTinhTrang.Enabled = false;
            btnCapNhat.Enabled = false;
            btnDuyet.Enabled = false;
            btnExcel.Enabled = false;


            txt_GhiChu.Text = string.Empty;
            lb_GhiChu.Visible = false;
            txt_GhiChu.Visible = false;
            dgv_DSChiTietThietBi.ReadOnly = true;
            gbx_ThaoTac.Height = 94;
            gbx_ThongTin.Height = 232;

            LoadTinhTrangComboBox();
            //LoadTrangThaiComboBox();
            Load_CBB_Filter_TinhTrang();
            Load_CBB_Filter_TrangThai();
            LoadData_DGV_DSPhieuMuon(_list_DS_PhieuMuon, null, false);

            loading_focus();
        }

        private void cbb_filter_TrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTinhTrang = cbb_Filter_TinhTrang.SelectedItem?.ToString();
            bool selectedTrangThai = cbb_filter_TrangThai.SelectedIndex == 1;

            LoadData_DGV_DSPhieuMuon(_list_DS_PhieuMuon, string.Equals(selectedTinhTrang, "Tất cả") ? null : selectedTinhTrang, selectedTrangThai);
        }

        private void cbb_Filter_TinhTrang_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTinhTrang = cbb_Filter_TinhTrang.SelectedItem?.ToString();
            bool selectedTrangThai = cbb_filter_TrangThai.SelectedIndex == 1;

            LoadData_DGV_DSPhieuMuon(_list_DS_PhieuMuon,  string.Equals(selectedTinhTrang, "Tất cả") ? null : selectedTinhTrang, selectedTrangThai);
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            // Lấy giá trị tình trạng được chọn từ ComboBox
            string selectedTinhTrang = cbbTinhTrang.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedTinhTrang))
            {
                MessageBox.Show("Vui lòng chọn tình trạng trước khi cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            switch (selectedTinhTrang)
            {
                case "Đã trả":
                    {
                        if (MessageBox.Show("Bạn có chắc thiết bị đã đc trả đủ không!", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            Update_TinhTrang(sender, e, selectedTinhTrang);
                        }
                    }
                    break;

                case "Trả thiếu":
                    {
                        if (MessageBox.Show("Vui lòng nhập lý do trả muộn và chọn thiết bị thiếu trước khi cập nhật!", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            

                            if (!string.IsNullOrEmpty(txt_GhiChu.Text))
                            {
                                Update_TinhTrang(sender, e, selectedTinhTrang);
                            }
                            else
                            {
                                MessageBox.Show("Vui lòng nhập lý do trả muộn và chọn thiết bị thiếu trước khi cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    break;

                case "Trả muộn":
                    {
                        if (MessageBox.Show("Vui lòng nhập lý do trả muộn và chọn thiết bị thiếu trước khi cập nhật!", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {

                            lb_GhiChu.Visible = true;
                            txt_GhiChu.Visible = true;
                            gbx_ThaoTac.Height = 253;
                            Update_TinhTrang(sender, e, selectedTinhTrang);
                        }
                    }
                    break;

                default:

                    break;
            }
        }

        private void Update_TinhTrang(object sender, EventArgs e, string selectedTinhTrang)
        {
            if (MessageBox.Show($"Bạn có chắc muốn cập nhật tình trạng thành '{selectedTinhTrang}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Cập nhật Tình trạng trong cơ sở dữ liệu
                _MuonThietBi_Click_Row.NgayTra = DateTime.Now.Date;
                _MuonThietBi_Click_Row.TinhTrangTraTB = selectedTinhTrang;
                _MuonThietBi_Click_Row.GhiChuTraThietBi = txt_GhiChu.Text.ToString() != null ? txt_GhiChu.Text.ToString() : null;

                bool isUpdated = mtbBUS.Update(_MuonThietBi_Click_Row);
                
                if (isUpdated)
                {
                    MessageBox.Show("Cập nhật tình trạng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    var list = ctmtbBUS.GetByMaMuon(_MuonThietBi_Click_Row.MaMuon);
                    switch (selectedTinhTrang)
                    {
                        case "Đã trả":
                            {
                                foreach (var item in list)
                                {
                                    if (item != null)
                                    {
                                        cttbBUS.Update_TrangThai(item.MaCTTB, 0);
                                    }
                                }
                            }
                            break;

                        case "Trả thiếu":
                            {
                                foreach (var item in list)
                                {
                                    if (item != null && item.TrangThai == true)
                                    {
                                        cttbBUS.Update_TrangThai(item.MaCTTB, 0);
                                    }
                                }
                            }
                            break;

                        case "Trả muộn":
                            {
                                foreach (var item in list)
                                {
                                    if (item != null)
                                    {
                                        cttbBUS.Update_TrangThai(item.MaCTTB, 0);
                                    }
                                }
                            }
                            break;

                        default:

                            break;
                    }
                    FormMuonThietBi_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Cập nhật tình trạng thất bại. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }        

        private void btnDuyet_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn duyệt phiếu mượn này không?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                mtbBUS.Update_TrangThai(_MuonThietBi_Click_Row.MaMuon,true);
                foreach (var item in ctmtbBUS.GetByMaMuon(_MuonThietBi_Click_Row.MaMuon))
                {
                    cttbBUS.Update_TrangThai(item.MaCTTB, 1);
                }
                SendCodeEmail(new ThongTinCaNhanBUS().GetByMaNguoiDung(_MuonThietBi_Click_Row.MaNguoiDung).Email, _MuonThietBi_Click_Row, _list_ChiTiet_MuonTB);
                FormMuonThietBi_Load(sender, e);
            }
        }

        private void btnLoadDing_Click(object sender, EventArgs e)
        {
            FormMuonThietBi_Load(sender, e);
        }
 
        private void loading_focus()
        {
            txtMaMuon.Text = string.Empty;
            txtNguoiMuon.Text = string.Empty;
            txtTKB.Text = string.Empty;
            txt_NgayMuon.Text = string.Empty;
            txt_TinhTrang.Text = string.Empty;
            txt_TrangThai.Text = string.Empty;

            dgv_DSTB.DataSource = null;
            dgv_DSChiTietThietBi.DataSource = null;
            cbb_Filter_TinhTrang.Focus();
            cbb_filter_TrangThai.Focus();

            cbbTinhTrang.Enabled = false;
            btnCapNhat.Enabled = false;
            btnDuyet.Enabled = false;
            btnExcel.Enabled = false;
        }

        private void dgvDSPhieuMuon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                // Get the selected row
                DataGridViewRow row = dgvDSPhieuMuon.Rows[e.RowIndex];

                //Lưu thông tin Mượn được click
                _MuonThietBi_Click_Row = new MuonThietBiDTO
                {
                    MaMuon = row.Cells["MaMuon"].Value != null ? Convert.ToInt32(row.Cells["MaMuon"].Value) : 0,
                    MaNguoiDung = row.Cells["MaNguoiDung"].Value?.ToString() ?? string.Empty,
                    MaTKB = row.Cells["MaTKB"].Value != null ? Convert.ToInt32(row.Cells["MaTKB"].Value) : 0,
                    NgayMuon = row.Cells["NgayMuon"].Value != null && DateTime.TryParse(row.Cells["NgayMuon"].Value.ToString(), out var ngayMuon)
                    ? ngayMuon
                    : DateTime.MinValue, // Giá trị mặc định nếu không có dữ liệu
                    NgayTra = row.Cells["NgayTra"].Value != null && DateTime.TryParse(row.Cells["NgayTra"].Value.ToString(), out var ngayTra)
                    ? ngayTra
                    : (DateTime?)null, // Cho phép giá trị null
                    TinhTrangTraTB = row.Cells["TinhTrangTraTB"].Value?.ToString() ?? string.Empty,
                    TrangThai = row.Cells["TrangThai"].Value != null && bool.TryParse(row.Cells["TrangThai"].Value.ToString(), out var trangThai)
                    ? trangThai
                    : false, // Mặc định false nếu không hợp lệ
                    GhiChuTraThietBi = row.Cells["GhiChuTraThietBi"].Value?.ToString() ?? string.Empty
                };


                //Load data lên text box
                txtMaMuon.Text = row.Cells["MaMuon"].Value.ToString();
                txtNguoiMuon.Text = ttcnBUS.GetByMaNguoiDung(row.Cells["MaNguoiDung"].Value.ToString()).HoTen;
                txtTKB.Text = row.Cells["MaTKB"].Value.ToString();
                txt_NgayMuon.Text = row.Cells["NgayMuon"].Value.ToString();
                txt_TinhTrang.Text = row.Cells["TinhTrangTraTB"].Value.ToString();
                txt_TrangThai.Text = Convert.ToBoolean(row.Cells["TrangThai"].Value) ? "Đã duyệt" : "Chưa duyệt";
                txt_GhiChu.Text = row.Cells["GhiChuTraThietBi"].Value.ToString();

                //Get data
                _list_ChiTiet_MuonTB = ctmtbBUS.GetByMaMuon(_MuonThietBi_Click_Row.MaMuon);
                loadData_DGV_DS_ThietBi(_list_ChiTiet_MuonTB);
                loadData_DGV_DS_ChiTietThietBi(_list_ChiTiet_MuonTB);
                gbx_ThongTin.Height = 280;


                ////Controls
                if (_MuonThietBi_Click_Row.TrangThai == false)
                {
                    cbbTinhTrang.Enabled = false;
                    btnCapNhat.Enabled = false;
                    btnDuyet.Visible = true;
                    btnDuyet.Enabled = true;
                    lb_GhiChu.Visible = false;
                    txt_GhiChu.Visible = false;
                    gbx_ThaoTac.Height = 94;
                }
                else
                {
                    cbbTinhTrang.Enabled = true;
                    btnCapNhat.Enabled = false;
                    btnDuyet.Visible = false;
                    btnDuyet.Enabled = false;
                    //lb_GhiChu.Visible = true;
                    //txt_GhiChu.Visible = true;
                    //gbx_ThaoTac.Height = 253;
                }
                btnExcel.Enabled = true;
            }
        }

        private void cbbTinhTrang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(string.Equals(cbbTinhTrang.SelectedItem.ToString(), "Trả thiếu"))
            {
                lb_GhiChu.Visible = true;
                txt_GhiChu.Visible = true;
                gbx_ThaoTac.Height = 253;

                dgv_DSChiTietThietBi.ReadOnly = false;
                dgv_DSChiTietThietBi.AllowUserToAddRows = false; // Không cho phép thêm hàng
                dgv_DSChiTietThietBi.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Chọn cả dòng
                loadData_DGV_DS_ChiTietThietBi_TrangThai(_list_ChiTiet_MuonTB);
            }
            else
            {
                lb_GhiChu.Visible = false;
                txt_GhiChu.Visible = false;
                gbx_ThaoTac.Height = 94;
                dgv_DSChiTietThietBi.DataSource = null;
                loadData_DGV_DS_ChiTietThietBi(_list_ChiTiet_MuonTB);
            }
            btnCapNhat.Enabled = true;
        }

        private void dgv_DSChiTietThietBi_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0) // Đảm bảo không phải tiêu đề
            {
                // Kiểm tra nếu cột là "TrangThai" (tên cột checkbox)
                if (dgv_DSChiTietThietBi.Columns[e.ColumnIndex].Name == "TrangThai")
                {
                    var item = new ChiTietMuonThietBiDTO
                    {
                        MaMuon = Convert.ToInt32(dgv_DSChiTietThietBi.Rows[e.RowIndex].Cells["MaMuon"].Value),
                        MaCTTB = Convert.ToInt32(dgv_DSChiTietThietBi.Rows[e.RowIndex].Cells["MaCTTB"].Value),
                        TrangThai = Convert.ToBoolean(dgv_DSChiTietThietBi.Rows[e.RowIndex].Cells["TrangThai"].Value)
                    };
                    // Hỏi xác nhận từ người dùng
                    if (MessageBox.Show("Bạn có chắc thiết bị này thiếu không?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        // Gọi hàm cập nhật trạng thái
                        ctmtbBUS.Update_TrangThaiThieu(item);
                        MessageBox.Show("Cập nhật trạng thái thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Hoàn tác thay đổi nếu người dùng chọn "No"
                        dgv_DSChiTietThietBi.Rows[e.RowIndex].Cells["TrangThai"].Value = true;
                    }
                }
            }
        }

        private void dgv_DSChiTietThietBi_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgv_DSChiTietThietBi.CurrentCell is DataGridViewCheckBoxCell)
            {
                // Commit thay đổi ngay lập tức khi checkbox được thay đổi
                dgv_DSChiTietThietBi.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void SendCodeEmail(string toEmail, MuonThietBiDTO item, List<ChiTietMuonThietBiDTO> _list)
        {
            try
            {
                // Lấy thông tin cấu hình từ App.configs
                string fromEmail = ConfigurationManager.AppSettings["EmailSender"];
                string emailPassword = ConfigurationManager.AppSettings["EmailPassword"];
                string smtpHost = ConfigurationManager.AppSettings["SmtpServer"];
                int smtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);


                if (string.IsNullOrEmpty(fromEmail) || string.IsNullOrEmpty(emailPassword) || string.IsNullOrEmpty(smtpHost))
                {
                    MessageBox.Show("Thông tin cấu hình email không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var thoiKhoaBieu = new ThoiKhoaBieuBUS().GetByID(_MuonThietBi_Click_Row.MaTKB);
                var monHoc = new MonHocBUS().GetByID(thoiKhoaBieu.MaMon);
                var baiHoc = new BaiHocBUS().GetByID(thoiKhoaBieu.MaBaiHoc);

                string excelFilePath = Path.Combine(Path.GetTempPath(), "DanhSachThietBi.xlsx");
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                // Tạo một file Excel mới
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Sheet1");


                    // Tiêu đề
                    worksheet.Cells[1, 2, 1, 5].Merge = true; // Hợp nhất từ B1 đến D1
                    worksheet.Cells[1, 2, 1, 5].Value = "Phiếu đăng ký mượn thiết bị";
                    worksheet.Cells[1, 2, 1, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; // Căn giữa nội dung
                    worksheet.Cells[1, 2, 1, 5].Style.Font.Bold = true;
                    worksheet.Cells[1, 2, 1, 5].Style.Font.Size = 20;

                    worksheet.Cells[2, 2, 2, 5].Merge = true;
                    worksheet.Cells[2, 2, 2, 5].Value = "Họ và tên giáo viên: " + new ThongTinCaNhanBUS().GetByMaNguoiDung(_MuonThietBi_Click_Row.MaNguoiDung).HoTen;
                    worksheet.Cells[2, 2, 2, 5].Style.Font.Bold = true;
                    worksheet.Cells[2, 2, 2, 5].Style.Font.Size = 16;


                    worksheet.Cells[3, 2, 3, 5].Merge = true;
                    if (thoiKhoaBieu != null)
                    {
                        if (monHoc != null)
                        {
                            worksheet.Cells[3, 2, 3, 5].Value = "Môn học: " + monHoc.TenMon;
                        }
                        else
                        {
                            worksheet.Cells[3, 2, 3, 5].Value = "Môn học: Không tìm thấy thông tin.";
                        }
                    }
                    else
                    {
                        worksheet.Cells[3, 2, 3, 5].Value = "Môn học: Không tìm thấy thông tin thời khóa biểu.";
                    }
                    worksheet.Cells[3, 2, 3, 5].Style.Font.Bold = true;
                    worksheet.Cells[3, 2, 3, 5].Style.Font.Size = 16;



                    worksheet.Cells[4, 2].Value = "STT";
                    worksheet.Cells[4, 3].Value = "Mã chi tiết thiết bị";
                    worksheet.Cells[4, 4].Value = "Số kệ";
                    worksheet.Cells[4, 5].Value = "Tên thiết bị";

                    // Định dạng tiêu đề
                    worksheet.Cells[4, 2, 4, 5].Style.Font.Bold = true;
                    worksheet.Cells[4, 2, 4, 5].Style.Font.Size = 14;

                    worksheet.Cells[4, 2, 4, 5].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[4, 2, 4, 5].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);
                    worksheet.Cells[4, 2, 4, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    // Lặp qua các dòng và cột trong DataGridView để nhập dữ liệu
                    for (int row = 0; row < dgv_DSChiTietThietBi.Rows.Count; row++)
                    {
                        worksheet.Cells[row + 5, 2].Value = row + 1;  // STT
                        worksheet.Cells[row + 5, 3].Value = dgv_DSChiTietThietBi.Rows[row].Cells["MaCTTB"].Value; // Mã chi tiết thiết bị
                        worksheet.Cells[row + 5, 4].Value = dgv_DSChiTietThietBi.Rows[row].Cells["MaTB"].Value; // Số kệ
                        worksheet.Cells[row + 5, 5].Value = dgv_DSChiTietThietBi.Rows[row].Cells["TenTB"].Value; // Tên thiết bị
                    }

                    worksheet.Cells.AutoFitColumns();

                    // Lưu file Excel
                    package.SaveAs(new FileInfo(excelFilePath));
                }

                var _list_MuonChiTiet = GetUniqueThietBiList(_list);
                // Tạo nội dung email với bảng HTML
                string tableRows = "";
                for (int i = 0; i < _list_MuonChiTiet.Count; i++)
                {
                    tableRows += $@"
                <tr>
                    <td style='text-align:center;'>{i + 1}</td>
                    <td>{_list_MuonChiTiet[i].MaTB}</td>
                    <td>{_list_MuonChiTiet[i].TenTB}</td>
                    <td style='text-align:center;'>{_list_MuonChiTiet[i].SoLuong}</td>
                </tr>";
                }

                string emailBody = $@"
            <html>
            <head>
                <style>
                    body {{
                        font-family: Arial, sans-serif;
                        line-height: 1.6;
                        color: #333;
                        margin: 0;
                        padding: 0;
                    }}
                    .email-container {{
                        max-width: 600px;
                        margin: 20px auto;
                        padding: 20px;
                        border: 1px solid #ddd;
                        border-radius: 8px;
                        background-color: #f9f9f9;
                    }}
                    .email-header {{
                        font-size: 18px;
                        font-weight: bold;
                        margin-bottom: 20px;
                        color: #0066cc;
                    }}
                    .email-footer {{
                        margin-top: 20px;
                        font-size: 14px;
                        color: #555;
                        border-top: 1px solid #ddd;
                        padding-top: 10px;
                    }}
                    table {{
                        width: 100%;
                        border-collapse: collapse;
                        margin-bottom: 20px;
                    }}
                    table, th, td {{
                        border: 1px solid #ddd;
                    }}
                    th, td {{
                        padding: 8px;
                        text-align: left;
                    }}
                    th {{
                        background-color: #f2f2f2;
                    }}
                </style>
            </head>
            <body>
                <div class='email-container'>
                    <div class='email-header'>
                        Yêu cầu mượn thiết bị của bạn đã được duyệt
                    </div>
                    <p>Mã Mượn: <b>{item.MaMuon}</b> - Môn học: <b>{monHoc.TenMon}</b> - Bài học: <b>{baiHoc.TenBaiHoc}</b></p>
                    <table>
                        <thead>
                            <tr>
                                <th>STT</th>
                                <th>Mã thiết bị</th>
                                <th>Tên thiết bị</th>
                                <th>Số lượng</th>
                            </tr>
                        </thead>
                        <tbody>
                            {tableRows}
                        </tbody>
                    </table>
                </div>
            </body>
            </html>";
                using (var message = new MailMessage())
                {
                    message.To.Add(toEmail);
                    message.Subject = "Yêu cầu mượn thiết bị của bạn đã được duyệt:";
                    message.Body = emailBody;
                    message.From = new MailAddress(fromEmail);
                    message.IsBodyHtml = true;

                    // Đính kèm file Excel
                    message.Attachments.Add(new Attachment(excelFilePath));

                    using (var smtp = new SmtpClient(smtpHost, smtpPort))
                    {
                        smtp.Credentials = new NetworkCredential(fromEmail, emailPassword);
                        smtp.EnableSsl = true;
                        smtp.Send(message);
                    }
                }

                // Xóa file tạm sau khi gửi email
                if (File.Exists(excelFilePath))
                {
                    File.Delete(excelFilePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi gửi email: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if(_MuonThietBi_Click_Row != null)
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel Files|*.xlsx";
                    saveFileDialog.FileName = "Phieu_Muon_Thiet_Bi_"+ _MuonThietBi_Click_Row.MaTKB +"_"+ _MuonThietBi_Click_Row.MaNguoiDung + ".xlsx";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            // Khai báo LicenseContext
                            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                            // Tạo một file Excel mới
                            using (var package = new ExcelPackage())
                            {
                                var worksheet = package.Workbook.Worksheets.Add("Sheet1");


                                // Tiêu đề
                                worksheet.Cells[1, 2, 1, 5].Merge = true; // Hợp nhất từ B1 đến D1
                                worksheet.Cells[1, 2, 1, 5].Value = "Phiếu đăng ký mượn thiết bị";
                                worksheet.Cells[1, 2, 1, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; // Căn giữa nội dung
                                worksheet.Cells[1, 2, 1, 5].Style.Font.Bold = true;
                                worksheet.Cells[1, 2, 1, 5].Style.Font.Size = 20;

                                worksheet.Cells[2, 2, 2, 5].Merge = true;
                                worksheet.Cells[2, 2, 2, 5].Value = "Họ và tên giáo viên: " + new ThongTinCaNhanBUS().GetByMaNguoiDung(_MuonThietBi_Click_Row.MaNguoiDung).HoTen;
                                worksheet.Cells[2, 2, 2, 5].Style.Font.Bold = true;
                                worksheet.Cells[2, 2, 2, 5].Style.Font.Size = 16;

                                var thoiKhoaBieu = new ThoiKhoaBieuBUS().GetByID(_MuonThietBi_Click_Row.MaTKB);
                                
                                worksheet.Cells[3, 2, 3, 5].Merge = true;
                                if (thoiKhoaBieu != null)
                                {
                                    var monHoc = new MonHocBUS().GetByID(thoiKhoaBieu.MaMon);
                                    if (monHoc != null)
                                    {
                                        worksheet.Cells[3, 2, 3, 5].Value = "Môn học: " + monHoc.TenMon;
                                    }
                                    else
                                    {
                                        worksheet.Cells[3, 2, 3, 5].Value = "Môn học: Không tìm thấy thông tin.";
                                    }
                                }
                                else
                                {
                                    worksheet.Cells[3, 2, 3, 5].Value = "Môn học: Không tìm thấy thông tin thời khóa biểu.";
                                }
                                worksheet.Cells[3, 2, 3, 5].Style.Font.Bold = true;
                                worksheet.Cells[3, 2, 3, 5].Style.Font.Size = 16;



                                worksheet.Cells[4, 2].Value = "STT";
                                worksheet.Cells[4, 3].Value = "Mã chi tiết thiết bị";
                                worksheet.Cells[4, 4].Value = "Số kệ";
                                worksheet.Cells[4, 5].Value = "Tên thiết bị";

                                // Định dạng tiêu đề
                                worksheet.Cells[4, 2, 4, 5].Style.Font.Bold = true;
                                worksheet.Cells[4, 2, 4, 5].Style.Font.Size = 14;

                                worksheet.Cells[4, 2, 4, 5].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                worksheet.Cells[4, 2, 4, 5].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);
                                worksheet.Cells[4, 2, 4, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                                // Lặp qua các dòng và cột trong DataGridView để nhập dữ liệu
                                for (int row = 0; row < dgv_DSChiTietThietBi.Rows.Count; row++)
                                {
                                    worksheet.Cells[row + 5, 2].Value = row + 1;  // STT
                                    worksheet.Cells[row + 5, 3].Value = dgv_DSChiTietThietBi.Rows[row].Cells["MaCTTB"].Value; // Mã chi tiết thiết bị
                                    worksheet.Cells[row + 5, 4].Value = dgv_DSChiTietThietBi.Rows[row].Cells["MaTB"].Value; // Số kệ
                                    worksheet.Cells[row + 5, 5].Value = dgv_DSChiTietThietBi.Rows[row].Cells["TenTB"].Value; // Tên thiết bị
                                }

                                worksheet.Cells.AutoFitColumns();

                                // Lưu file Excel
                                FileInfo fi = new FileInfo(saveFileDialog.FileName);
                                package.SaveAs(fi);
                            }

                            MessageBox.Show("Xuất Excel thành công!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Có lỗi xảy ra: {ex.Message}");
                        }
                    }
                }
            }
        }
    }
}
