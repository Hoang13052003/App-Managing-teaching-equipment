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

        //Danh sách thiết bị theo môn học bài học
        private List<MonHoc_BaiHoc_ChiTietTB_DTO> _list_MonHoc_BaiHoc_ChiTietTB = new List<MonHoc_BaiHoc_ChiTietTB_DTO>();

        //Danh sách lưu danh sách thiết bị mượn
        private List<ChiTietThietBi_ThietBiDTO> _List_CTTB_Muon = new List<ChiTietThietBi_ThietBiDTO>();

        //Danh sách tất cả các thiết bị
        private List<ChiTietThietBi_ThietBiDTO> _List_CTTB = new List<ChiTietThietBi_ThietBiDTO>();

        //Danh sách tất cả các thiết bị có thao tác Add, Remove
        private List<ChiTietThietBi_ThietBiDTO> _List_CTTB_Change = new List<ChiTietThietBi_ThietBiDTO>();

        //Danh sách tất cả các thiết bị khi dgv_DSTB_Click
        private List<ChiTietThietBi_ThietBiDTO> _List_CTTB_Muon_Filter;


        //Controls_Form
        public formThietBiDayHoc_MonHoc_YeuCauMuonThemThietBi()
        {
            InitializeComponent();
        }
        public formThietBiDayHoc_MonHoc_YeuCauMuonThemThietBi(ThoiKhoaBieuChiTietDTO tkbDTO)
        {
            InitializeComponent();
            _tkbDTO = tkbDTO;
            //AccountInfo.MaNguoiDung = "ND00000001";
        }

        private void formThietBiDayHoc_MonHoc_YeuCauMuonThemThietBi_Load(object sender, EventArgs e)
        {
            if (mtbBUS.GetByMaND_MaTKB(AccountInfo.MaNguoiDung, _tkbDTO.MaTKB) != null)
            {
                btn_ThemTB.Enabled = false;
                btn_XoaTB.Enabled = false;
                btnGuiPhieuMuon.Enabled = false;
                MessageBox.Show("Chú ý, Bạn đã gửi phiếu mượn thiết bị của môn học này!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btn_BaoCaoThietBiHuHong.Enabled = true;

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



                _List_CTTB_Muon = fiter_List_CTTB_Muon(_list_MonHoc_BaiHoc_ChiTietTB, _List_CTTB);


                //Controls
                btn_ThemTB.Enabled = false;
                btn_XoaTB.Enabled = false;
                btn_BaoCaoThietBiHuHong.Enabled = false;

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

        private List<ChiTietThietBi_ThietBiDTO> fiter_List_CTTB_Muon(List<MonHoc_BaiHoc_ChiTietTB_DTO> _list_MonHoc_BaiHoc_ChiTietTB, List<ChiTietThietBi_ThietBiDTO> _List_CTTB)
        {
            List<ChiTietThietBi_ThietBiDTO> filteredList = new List<ChiTietThietBi_ThietBiDTO>();

            foreach (var item in _list_MonHoc_BaiHoc_ChiTietTB)
            {
                // Lọc các thiết bị có mã trùng khớp với MaTB và lấy đúng số lượng
                var matchedItems = _List_CTTB
                    .Where(x => x.MaTB == item.MaTB)
                    .Take(item.SoLuong)
                    .ToList();

                // Thêm các thiết bị phù hợp vào danh sách kết quả
                filteredList.AddRange(matchedItems);
            }

            return filteredList;
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
            dgv_DSTB.DataSource = null;
            // Gán dữ liệu cho DataGridView từ BUS
            dgv_DSTB.DataSource = _list_MonHoc_BaiHoc_ChiTietTB;
            dgv_DSTB.Columns["MaTB"].HeaderText = "Mã thiết bị";
            dgv_DSTB.Columns["TenTB"].HeaderText = "Tên thiết bị";
            dgv_DSTB.Columns["SoLuong"].HeaderText = "Số lượng";
            dgv_DSTB.Columns["MaMH"].Visible = false;
            dgv_DSTB.Columns["MaBH"].Visible = false;
            dgv_DSTB.Columns["MaLoai"].Visible = false;
        }

        //public List<MonHoc_BaiHoc_ThietBi_DTO> GetUniqueThietBiList(List<MonHoc_BaiHoc_ChiTietTB_DTO> _list_MonHoc_BaiHoc_ChiTietTB)
        //{
        //    // Sử dụng LINQ để nhóm theo MaTB và tính số lượng
        //    var groupedResult = _list_MonHoc_BaiHoc_ChiTietTB
        //        .GroupBy(tb => new { tb.MaTB, tb.TenTB }) // Nhóm theo MaTB và TenTB
        //        .Select(group => new MonHoc_BaiHoc_ThietBi_DTO
        //        {
        //            MaTB = group.Key.MaTB,                 // Lấy MaTB
        //            TenTB = group.Key.TenTB,               // Lấy TenTB
        //            SoLuong = group.Count()         // Lấy MaCTTB bất kỳ từ nhóm
        //        })
        //        .ToList();

        //    return groupedResult;
        //}

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
                //Controls
                _is_dgv_DSTB_Clicked = false;
                btn_ThemTB.Enabled = false;
                btn_XoaTB.Enabled = false;

                var List_CTTB = _List_CTTB.Where(x => x.MaTB == maTB).ToList();

                loadDGV_DSChiTietThietBi(List_CTTB);
            }
        }
        private void loadDGV_DSChiTietThietBi(List<ChiTietThietBi_ThietBiDTO> List_CTTB)
        {
            if(List_CTTB != null)
            {
                //List_CTTB = List_CTTB.Where(cttb => !_list_MonHoc_BaiHoc_ChiTietTB.Any(mhcttb => mhcttb.MaTB == cttb.MaTB)).ToList();
                var filteredCTTB = List_CTTB.Where(cttb => !_List_CTTB_Muon.Any(mhcttb => mhcttb.MaCTTB == cttb.MaCTTB)).ToList();
                filteredCTTB = filteredCTTB.AsParallel()
                .Where(cttb => (string.Equals(cttb.TinhTrang, "Mới") || string.Equals(cttb.TinhTrang, "Cũ"))
                               && cttb.TrangThai == 1)
                .ToList();
                if (filteredCTTB != null)
                {
                    dgv_DSChiTietThietBi.DataSource = null;

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


                bool isExist = _list_MonHoc_BaiHoc_ChiTietTB.Any(item => item.MaTB == _chiTietThietBi_Click_Row.MaTB);

                if (isExist)
                {
                    _List_CTTB_Muon.Add(_chiTietThietBi_Click_Row);

                    _list_MonHoc_BaiHoc_ChiTietTB.Where(x => x.MaTB == _chiTietThietBi_Click_Row.MaTB)
                        .ToList()
                            .ForEach(x => x.SoLuong++);

                }
                else
                {
                    _List_CTTB_Muon.Add(_chiTietThietBi_Click_Row);


                    _list_MonHoc_BaiHoc_ChiTietTB.Add(new MonHoc_BaiHoc_ChiTietTB_DTO
                    {
                        MaMH = _tkbChiTiet.MaMon,
                        MaBH = _tkbChiTiet.MaBaiHoc,
                        MaTB = _chiTietThietBi_Click_Row.MaTB,
                        SoLuong = 1
                    });

                }

                loadDGVCTTB_MonHoc_BaiHoc(_list_MonHoc_BaiHoc_ChiTietTB);
                loadDGV_DSChiTietThietBi(_List_CTTB);

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

                    // Xóa thiết bị khỏi danh sách chính (_list_MonHoc_BaiHoc_ChiTietTB)
                    _List_CTTB_Muon.RemoveAll(item => item.MaCTTB == _chiTietThietBi_Click_Row.MaCTTB);

                    _list_MonHoc_BaiHoc_ChiTietTB.Where(x => x.MaTB == _chiTietThietBi_Click_Row.MaTB)
                       .ToList()
                           .ForEach(x => x.SoLuong--);


                    loadDGVCTTB_MonHoc_BaiHoc(_list_MonHoc_BaiHoc_ChiTietTB);
                    loadDGV_DSChiTietThietBi(_List_CTTB);

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
                            foreach (var value in _List_CTTB_Muon)
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
                _List_CTTB_Muon_Filter = _List_CTTB_Muon.Where(x => x.MaTB == maTB).ToList();   

                loadDGV_DSChiTietThietBi_dgv_DSTB_Click(_List_CTTB_Muon_Filter);


                //Controls
                _is_dgv_DSTB_Clicked = true;
                btn_ThemTB.Enabled = false;
                btn_XoaTB.Enabled = false;
            }
        }

        //private List<ChiTietThietBi_ThietBiDTO> filtered_List_MonHoc_BaiHoc_ChiTietTB(List<MonHoc_BaiHoc_ChiTietTB_DTO> _list_MonHoc_BaiHoc_ChiTietTB, int maTB)
        //{
        //    var _MonHoc_BaiHoc_ChiTietTB_Dgv_Row_Click = _list_MonHoc_BaiHoc_ChiTietTB
        //            .Where(mhcttb => mhcttb.MaTB == maTB)
        //            .ToList();

        //    // Lấy danh sách MaCTTB từ _list_MonHoc_BaiHoc_ChiTietTB_Click
        //    var existingMaCTTB = _MonHoc_BaiHoc_ChiTietTB_Dgv_Row_Click
        //        .Select(x => x.MaCTTB)
        //        .ToHashSet(); // Sử dụng HashSet để tăng hiệu suất kiểm tra

        //    // Lọc danh sách ChiTietThietBi theo MaTB và chỉ lấy các MaCTTB đã tồn tại
        //    var filteredCTTB = _List_CTTB
        //        .Where(cttb => cttb.MaTB == maTB && existingMaCTTB.Contains(cttb.MaCTTB))
        //        .ToList();

        //    return filteredCTTB;
        //} 

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

        private void btn_BaoCaoThietBiHuHong_Click(object sender, EventArgs e)
        {
            Form newForm = new BaoCaoThietBiHuHong_TKB
            {
                MaTKB = _tkbDTO.MaTKB
            };
            newForm.ShowDialog();
        }
    }
}
