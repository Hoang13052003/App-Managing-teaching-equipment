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
    public partial class formThietBiDayHoc_MonHoc_YeuCauMuonThemThietBi : Form
    {
        //Biến
        private bool _is_dgv_DSTB_Clicked = false;
        

        //Models
        private ThoiKhoaBieuChiTietDTO _tkbDTO;
        private ThoiKhoaBieuDTO _tkbChiTiet;
        private ChiTietThietBi_ThietBiDTO _chiTietThietBi_Click_Row = null;

        //BUS
        private ThietBiBUS tbBUS = new ThietBiBUS();
        private MuonThietBiBUS mtbBUS = new MuonThietBiBUS();
        private ChiTietMuonThietBiBUS ctmtbBUS = new ChiTietMuonThietBiBUS();
        private LoaiThietBiBUS ltbBUS = new LoaiThietBiBUS();
        private ThoiKhoaBieuBUS tkbBUS = new ThoiKhoaBieuBUS();
        private ChiTietThietBi_ThietBiBUS cttbBUS = new ChiTietThietBi_ThietBiBUS();
        private MonHoc_BaiHoc_ChiTietTB_BUS monHoc_BaiHoc_ChiTietTB_BUS = new MonHoc_BaiHoc_ChiTietTB_BUS();


        //List
        private List<MonHoc_BaiHoc_ChiTietTB_DTO> _list_MonHoc_BaiHoc_ChiTietTB = new List<MonHoc_BaiHoc_ChiTietTB_DTO>();
        List<ChiTietThietBi_ThietBiDTO> _List_CTTB = new List<ChiTietThietBi_ThietBiDTO>();
        List<ChiTietThietBi_ThietBiDTO> _List_CTTB_Change = new List<ChiTietThietBi_ThietBiDTO>();
        private List<ChiTietThietBi_ThietBiDTO> _List_CTTB_Filter;


        //Controls_Form
        public formThietBiDayHoc_MonHoc_YeuCauMuonThemThietBi(ThoiKhoaBieuChiTietDTO tkbDTO)
        {
            InitializeComponent();
            _tkbDTO = tkbDTO;
            AccountInfo.MaNguoiDung = "ND00000001";
        }

        private void formThietBiDayHoc_MonHoc_YeuCauMuonThemThietBi_Load(object sender, EventArgs e)
        {
            if (mtbBUS.GetByMaND_MaTKB(AccountInfo.MaNguoiDung, _tkbDTO.MaTKB) != null)
            {
                btn_ThemTB.Enabled = false;
                btn_XoaTB.Enabled = false;
                btnGuiPhieuMuon.Enabled = false;
                MessageBox.Show("Chú ý, Bạn đã gửi phiếu mượn thiết bị của môn học này!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                // Create data
                _tkbChiTiet = tkbBUS.GetByID(_tkbDTO.MaTKB);
                _list_MonHoc_BaiHoc_ChiTietTB = monHoc_BaiHoc_ChiTietTB_BUS.GetAllGetByMaMH_MaBH(_tkbChiTiet.MaMon, _tkbChiTiet.MaBaiHoc);
                _List_CTTB = cttbBUS.GetAll();
                _List_CTTB_Change = cttbBUS.GetAll();


                //Load thông tin
                loadThongTinMonHoc();
                loadThongTinPhieuMuon();

                //Load data combobox
                loadCBBLoaiThietBi();
                loadCBBThietBi(tbBUS.GetAll());


                //load data datagridview
                loadDGVCTTB_MonHoc_BaiHoc(_list_MonHoc_BaiHoc_ChiTietTB);
            }
            else
            {
                _tkbChiTiet = tkbBUS.GetByID(_tkbDTO.MaTKB);
                _list_MonHoc_BaiHoc_ChiTietTB = monHoc_BaiHoc_ChiTietTB_BUS.GetAllGetByMaMH_MaBH(_tkbChiTiet.MaMon, _tkbChiTiet.MaBaiHoc);
                _List_CTTB = cttbBUS.GetAll();
                _List_CTTB_Change = cttbBUS.GetAll();

                //Controls
                btn_ThemTB.Enabled = false;
                btn_XoaTB.Enabled = false;


                //Load thông tin
                loadThongTinMonHoc();
                loadThongTinPhieuMuon();

                //Load data combobox
                loadCBBLoaiThietBi();
                loadCBBThietBi(tbBUS.GetAll());


                //load data datagridview
                loadDGVCTTB_MonHoc_BaiHoc(_list_MonHoc_BaiHoc_ChiTietTB);
            }
            
        }

        private void loadThongTinPhieuMuon()
        {
            txtTenNguoiMuon.Text = new ThongTinCaNhanBUS().GetByMaNguoiDung(AccountInfo.MaNguoiDung).HoTen;
            txtNgayMuon.Text = DateTime.Now.ToString();
        }

        private void loadThongTinMonHoc()
        {
            txtTenMonHoc.Text = _tkbDTO.TenMonHoc.ToString();
            txtBaiHoc.Text = _tkbDTO.TenBaiHoc.ToString();
            txtPhongHoc.Text = _tkbDTO.TenPhong.ToString();
            txtNgayHoc.Text = _tkbDTO.NgayHoc.ToString();
        }

        private void loadCBBLoaiThietBi()
        {
            List<LoaiThietBiDTO> loaiThietBis = ltbBUS.LayLoaiThietBi();
            cbb_LoaiTB.DataSource = loaiThietBis;
            cbb_LoaiTB.DisplayMember = "TenLoai";
            cbb_LoaiTB.ValueMember = "MaLoai";
        }

        private void loadCBBThietBi(List<ThietBiDTO> danhSachThietBi)
        {
            // Gán danh sách thiết bị vào ComboBox
            cbb_ThietBi.DataSource = danhSachThietBi;
            cbb_ThietBi.DisplayMember = "TenTB"; // Hiển thị tên thiết bị
            cbb_ThietBi.ValueMember = "MaTB"; // Giá trị là mã thiết bị
        }

        private void loadDGVCTTB_MonHoc_BaiHoc(List<MonHoc_BaiHoc_ChiTietTB_DTO> _list_MonHoc_BaiHoc_ChiTietTB)
        {
            // Gán dữ liệu cho DataGridView từ BUS
            dgv_DSTB.DataSource = GetUniqueThietBiList(_list_MonHoc_BaiHoc_ChiTietTB);
            dgv_DSTB.Columns["MaTB"].HeaderText = "Mã thiết bị";
            dgv_DSTB.Columns["TenTB"].HeaderText = "Tên thiết bị";
            dgv_DSTB.Columns["SoLuong"].HeaderText = "Số lượng";
        }

        public List<MonHoc_BaiHoc_ThietBi_DTO> GetUniqueThietBiList(List<MonHoc_BaiHoc_ChiTietTB_DTO> _list_MonHoc_BaiHoc_ChiTietTB)
        {
            // Sử dụng LINQ để nhóm theo MaTB và tính số lượng
            var groupedResult = _list_MonHoc_BaiHoc_ChiTietTB
                .GroupBy(tb => new { tb.MaTB, tb.TenTB }) // Nhóm theo MaTB và TenTB
                .Select(group => new MonHoc_BaiHoc_ThietBi_DTO
                {
                    MaTB = group.Key.MaTB,                 // Lấy MaTB
                    TenTB = group.Key.TenTB,               // Lấy TenTB
                    SoLuong = group.Count()         // Lấy MaCTTB bất kỳ từ nhóm
                })
                .ToList();

            return groupedResult;
        }

        private void cbb_LoaiTB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_LoaiTB.SelectedValue != null && int.TryParse(cbb_LoaiTB.SelectedValue.ToString(), out int maLoai))
            {
                //Controls
                _is_dgv_DSTB_Clicked = false;
                btn_ThemTB.Enabled = false;
                btn_XoaTB.Enabled = false;

                // Gọi BUS để lấy danh sách thiết bị theo mã loại
                List<ThietBiDTO> thietBisTheoLoai = tbBUS.GetByMaLoai(maLoai);

                // Cập nhật dữ liệu cho ComboBox thiết bị
                loadCBBThietBi(thietBisTheoLoai);
            }
        }

        private void cbb_ThietBi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_ThietBi.SelectedValue != null && int.TryParse(cbb_ThietBi.SelectedValue.ToString(), out int maTB))
            {
                ThietBiDTO item = tbBUS.GetAll().Find(tb => tb.MaTB == maTB);
                if (item != null)
                {
                    //Controls
                    _is_dgv_DSTB_Clicked = false;
                    btn_ThemTB.Enabled = false;
                    btn_XoaTB.Enabled = false;

                    var List_CTTB = _List_CTTB_Change.Where(x => x.MaTB == maTB).ToList();
                    List_CTTB = List_CTTB.Where(cttb => !_list_MonHoc_BaiHoc_ChiTietTB.Any(mhcttb => mhcttb.MaCTTB == cttb.MaCTTB)).ToList();
                    var filteredCTTB = List_CTTB.AsParallel().Where(cttb => string.Equals(cttb.TinhTrang, "Mới") || string.Equals(cttb.TinhTrang, "Cũ")).ToList();
                    
                    loadDGV_DSChiTietThietBi(List_CTTB);
                }
            }
        }
        private void loadDGV_DSChiTietThietBi(List<ChiTietThietBi_ThietBiDTO> List_CTTB)
        {
            if(List_CTTB != null)
            {
                List_CTTB = List_CTTB.Where(cttb => !_list_MonHoc_BaiHoc_ChiTietTB.Any(mhcttb => mhcttb.MaCTTB == cttb.MaCTTB)).ToList();
                var filteredCTTB = List_CTTB.AsParallel()
                .Where(cttb => (string.Equals(cttb.TinhTrang, "Mới") || string.Equals(cttb.TinhTrang, "Cũ"))
                               && cttb.TrangThai == 1)
                .ToList();
                if (filteredCTTB != null)
                {
                    dgv_DSChiTietThietBi.DataSource = filteredCTTB;
                    dgv_DSChiTietThietBi.Columns["MaCTTB"].HeaderText = "Mã CT thiết bị";
                    dgv_DSChiTietThietBi.Columns["MaTB"].HeaderText = "Mã thiết bị";
                    dgv_DSChiTietThietBi.Columns["TenTB"].HeaderText = "Tên thiết bị";
                    dgv_DSChiTietThietBi.Columns["TinhTrang"].HeaderText = "Tình trạng";
                    dgv_DSChiTietThietBi.Columns["TrangThai"].Visible = false;
                    dgv_DSChiTietThietBi.Columns["NgayMua"].Visible = false;
                }
                else
                {
                    MessageBox.Show("Thiết bị đã được cho mượn hết!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }    
        }
        

        private void btn_ThemTB_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thêm thiết bị này vào danh sách thiết bị mượn không?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                btn_ThemTB.Enabled = false;

                _list_MonHoc_BaiHoc_ChiTietTB.Add(new MonHoc_BaiHoc_ChiTietTB_DTO
                {
                    MaMH = _tkbChiTiet.MaMon,
                    MaBH = _tkbChiTiet.MaBaiHoc,
                    MaCTTB = _chiTietThietBi_Click_Row.MaCTTB,
                    MaTB = _chiTietThietBi_Click_Row.MaTB,
                    TenTB = _chiTietThietBi_Click_Row.TenTB,
                });

                _List_CTTB_Change.RemoveAll(cttb => cttb.MaCTTB == _chiTietThietBi_Click_Row.MaCTTB);

                loadDGVCTTB_MonHoc_BaiHoc(_list_MonHoc_BaiHoc_ChiTietTB);
                loadDGV_DSChiTietThietBi(_List_CTTB_Change.Where(x => x.MaTB == _chiTietThietBi_Click_Row.MaTB).ToList());

                //
                _chiTietThietBi_Click_Row = null;
            }
        }



        private void btn_XoaTB_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn xóa thiết bị này khỏi danh sách thiết bị mượn của bạn?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    btn_XoaTB.Enabled = false;

                    // Thêm thiết bị vào danh sách thay đổi (_List_CTTB_Change)
                    var selectedItem = _List_CTTB.FirstOrDefault(x => x.MaCTTB == _chiTietThietBi_Click_Row.MaCTTB);
                    if (selectedItem != null)
                    {
                        _List_CTTB_Change.Add(selectedItem);
                    }

                    // Xóa thiết bị khỏi danh sách chính (_list_MonHoc_BaiHoc_ChiTietTB)
                    _list_MonHoc_BaiHoc_ChiTietTB.RemoveAll(item => item.MaCTTB == _chiTietThietBi_Click_Row.MaCTTB);

                    // Xóa thiết bị khỏi danh sách lọc (_List_CTTB_Filter)
                    _List_CTTB_Filter.RemoveAll(item => item.MaCTTB == _chiTietThietBi_Click_Row.MaCTTB);

                    // Tải lại danh sách lên DataGridView
                    loadDGVCTTB_MonHoc_BaiHoc(_list_MonHoc_BaiHoc_ChiTietTB);
                    loadDGV_DSChiTietThietBi_Xoa(_List_CTTB_Filter);

                    // Đặt lại biến chi tiết thiết bị
                    _chiTietThietBi_Click_Row = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Đã xảy ra lỗi khi xóa thiết bị: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    btn_XoaTB.Enabled = true;
                }
            }
        }
        private void loadDGV_DSChiTietThietBi_Xoa(List<ChiTietThietBi_ThietBiDTO> List_CTTB)
        {
            dgv_DSChiTietThietBi.DataSource = null;
            if (List_CTTB != null)
            {
                dgv_DSChiTietThietBi.DataSource = List_CTTB;
                dgv_DSChiTietThietBi.Columns["MaCTTB"].HeaderText = "Mã CT thiết bị";
                dgv_DSChiTietThietBi.Columns["MaTB"].HeaderText = "Mã thiết bị";
                dgv_DSChiTietThietBi.Columns["TenTB"].HeaderText = "Tên thiết bị";
                dgv_DSChiTietThietBi.Columns["TinhTrang"].HeaderText = "Tình trạng";
                dgv_DSChiTietThietBi.Columns["TrangThai"].Visible = false;
                dgv_DSChiTietThietBi.Columns["NgayMua"].Visible = false;
            }
        }
        private void btnGuiPhieuMuon_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn gửi phiếu mượn không?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MuonThietBiDTO mtbDTO = new MuonThietBiDTO
                {
                    //MaNguoiDung = AccountInfo.MaNguoiDung,
                    MaNguoiDung = AccountInfo.MaNguoiDung,
                    MaTKB = _tkbChiTiet.MaTKB,
                    NgayMuon = DateTime.Now.Date,
                    TinhTrangTraTB = "Chưa trả"
                };

                try
                {
                    if (mtbBUS.Insert(mtbDTO))
                    {
                        MuonThietBiDTO item = mtbBUS.GetByMaND_MaTKB(AccountInfo.MaNguoiDung, _tkbChiTiet.MaTKB);
                        if (item != null)
                        {
                            foreach (var value in _list_MonHoc_BaiHoc_ChiTietTB)
                            {
                                ChiTietMuonThietBiDTO ctmtbDTO = new ChiTietMuonThietBiDTO
                                {
                                    MaMuon = item.MaMuon,
                                    MaCTTB = value.MaCTTB
                                };

                                ctmtbBUS.Insert(ctmtbDTO);
                                cttbBUS.Update_TrangThai(value.MaCTTB, 3);
                            }
                        }
                        MessageBox.Show("Gửi phiếu mượn thiết bị thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FormTask.OpenFormInPanel<Teaching_Schedule>(FormTask.Pannel_change);
                    }
                    else
                    {
                        MessageBox.Show("Gửi phiếu mượn thiết bị thất bại. Vui lòng kiểm tra lại thông tin!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Đã xảy ra lỗi trong quá trình gửi phiếu mượn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgv_DSChiTietThietBi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Get the selected row
                DataGridViewRow row = dgv_DSChiTietThietBi.Rows[e.RowIndex];

                //Lưu thông tin chi tiết thiết bị được click
                _chiTietThietBi_Click_Row = new ChiTietThietBi_ThietBiDTO
                {
                    MaCTTB = Convert.ToInt32(row.Cells["MaCTTB"].Value),
                    MaTB = Convert.ToInt32(row.Cells["MaTB"].Value),
                    TenTB = row.Cells["TenTB"].Value.ToString(),
                    TinhTrang = row.Cells["TinhTrang"].Value.ToString(),
                    TrangThai = Convert.ToInt32(row.Cells["TrangThai"].Value),
                    NgayMua = Convert.ToDateTime(row.Cells["NgayMua"].Value)
                };

                //Load data lên text box
                txtMaTB.Text = row.Cells["MaTB"].Value.ToString();
                txtTenTB.Text = row.Cells["TenTB"].Value.ToString();

                //Controls
                if (_is_dgv_DSTB_Clicked)
                {
                    btn_ThemTB.Enabled = false;
                    btn_XoaTB.Enabled = true;
                }
                else
                {
                    btn_ThemTB.Enabled= true;
                    btn_XoaTB.Enabled= false;
                }
            }
        }

        private void dgv_DSTB_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgv_DSTB.Rows[e.RowIndex];

                var maTB = Convert.ToInt32(row.Cells["MaTB"].Value);

                // Lọc danh sách ChiTietThietBi theo MaTB và chỉ lấy các MaCTTB đã tồn tại
                _List_CTTB_Filter = filtered_List_MonHoc_BaiHoc_ChiTietTB(_list_MonHoc_BaiHoc_ChiTietTB, maTB);

                loadDGV_DSChiTietThietBi_dgv_DSTB_Click(_List_CTTB_Filter);


                //Controls
                _is_dgv_DSTB_Clicked = true;
                btn_ThemTB.Enabled = false;
                btn_XoaTB.Enabled = false;
            }
        }

        private List<ChiTietThietBi_ThietBiDTO> filtered_List_MonHoc_BaiHoc_ChiTietTB(List<MonHoc_BaiHoc_ChiTietTB_DTO> _list_MonHoc_BaiHoc_ChiTietTB, int maTB)
        {
            var _MonHoc_BaiHoc_ChiTietTB_Dgv_Row_Click = _list_MonHoc_BaiHoc_ChiTietTB
                    .Where(mhcttb => mhcttb.MaTB == maTB)
                    .ToList();

            // Lấy danh sách MaCTTB từ _list_MonHoc_BaiHoc_ChiTietTB_Click
            var existingMaCTTB = _MonHoc_BaiHoc_ChiTietTB_Dgv_Row_Click
                .Select(x => x.MaCTTB)
                .ToHashSet(); // Sử dụng HashSet để tăng hiệu suất kiểm tra

            // Lọc danh sách ChiTietThietBi theo MaTB và chỉ lấy các MaCTTB đã tồn tại
            var filteredCTTB = _List_CTTB
                .Where(cttb => cttb.MaTB == maTB && existingMaCTTB.Contains(cttb.MaCTTB))
                .ToList();

            return filteredCTTB;
        } 

        private void loadDGV_DSChiTietThietBi_dgv_DSTB_Click(List<ChiTietThietBi_ThietBiDTO> filteredCTTB)
        {
            if (filteredCTTB != null)
            {
                dgv_DSChiTietThietBi.DataSource = filteredCTTB;
                dgv_DSChiTietThietBi.Columns["MaCTTB"].HeaderText = "Mã CT thiết bị";
                dgv_DSChiTietThietBi.Columns["MaTB"].HeaderText = "Mã thiết bị";
                dgv_DSChiTietThietBi.Columns["TenTB"].HeaderText = "Tên thiết bị";
                dgv_DSChiTietThietBi.Columns["TinhTrang"].HeaderText = "Tình trạng";
                dgv_DSChiTietThietBi.Columns["TrangThai"].Visible = false;
                dgv_DSChiTietThietBi.Columns["NgayMua"].Visible = false;
            }
        }
    }
}
