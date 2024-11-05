using BUS;
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
    public partial class FormMuonThietBi : Form
    {
        private MuonThietBiBUS MuonThietBiBUS = new MuonThietBiBUS();
        public FormMuonThietBi()
        {
            InitializeComponent();
            this.Load += FormMuonThietBi_Load1;
            dgvDSPhieuMuon.CellClick += DgvDSPhieuMuon_CellClick;
            dgvDSCTPM.CellClick += DgvDSCTPM_CellClick;
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DgvDSCTPM_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvDSCTPM.Rows.Count)
            {
                DataGridViewRow row = dgvDSCTPM.Rows[e.RowIndex];
                // Lấy các giá trị từ dòng đã chọn và gán vào các TextBox
                txtMaTB.Text = row.Cells["MaTB"].Value?.ToString() ?? string.Empty;
                txtTenTB.Text = row.Cells["TenTB"].Value?.ToString() ?? string.Empty;
                txtNSX.Text = row.Cells["NSX"].Value?.ToString() ?? string.Empty;
                txtSoLuong.Text = row.Cells["SoLuong"].Value?.ToString() ?? string.Empty;
                txtTenNCC.Text = row.Cells["TenNCC"].Value?.ToString() ?? string.Empty;
            }
        }

        private void DgvDSPhieuMuon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvDSPhieuMuon.Rows.Count - 1) // Kiểm tra chỉ số hàng hợp lệ
            {
                // Lấy dòng đã chọn
                DataGridViewRow row = dgvDSPhieuMuon.Rows[e.RowIndex];

                //// Lấy MaMuon từ dòng đã chọn
                int maMuon = Convert.ToInt32(row.Cells["MaMuon"].Value);

                // Cập nhật các thông tin phiếu mượn lên giao diện
                txtMaMuon.Text = row.Cells["MaMuon"].Value?.ToString() ?? string.Empty;
                txtNguoiMuon.Text = row.Cells["MaNguoiDung"].Value?.ToString() ?? string.Empty;
                txtMaPhong.Text = row.Cells["MaPhong"].Value?.ToString() ?? string.Empty;

                // Lấy chi tiết phiếu mượn từ BUS (Business Logic Layer)
                var chiTietMuon = MuonThietBiBUS.GetCTPMByID(maMuon);

                if (chiTietMuon != null)
                {
                    txtMaCTPM_NCC.Text = chiTietMuon.MaCTTB_NCC.ToString() ?? string.Empty;

                    // Kiểm tra Ngày Mượn và Ngày Trả có null hay không
                    dtpNgayMuon.Text = chiTietMuon.NgayMuon?.ToString("dd/MM/yyyy") ?? string.Empty;
                    dtpNgayTra.Text = chiTietMuon.NgayTra?.ToString("dd/MM/yyyy") ?? string.Empty;

                    // Cập nhật trạng thái (Đã trả / Chưa trả)
                    cbbTrangThai.SelectedItem = chiTietMuon.TrangThai ? "Chưa trả" : "Đã trả";

                    // Sau khi lấy chi tiết phiếu mượn, tiếp tục lấy thông tin thiết bị mượn
                    LoadThietBiDetails(chiTietMuon.MaCTTB_NCC);
                }
                else
                {
                    // Xử lý khi không tìm thấy chi tiết phiếu mượn
                    MessageBox.Show("Không tìm thấy chi tiết phiếu mượn.");
                }
            }
        }

        private void LoadThietBiDetails(int maCTTB_NCC)
        {
            // Lấy danh sách các thiết bị mượn từ BUS
            DataTable thietBiDetailsTable = MuonThietBiBUS.GetThietBiDetails(maCTTB_NCC);

            // Kiểm tra nếu DataTable không rỗng
            if (thietBiDetailsTable != null && thietBiDetailsTable.Rows.Count > 0)
            {
                // Hiển thị danh sách thiết bị mượn lên DataGridView
                dgvDSCTPM.DataSource = thietBiDetailsTable;

                // Tuỳ chỉnh thêm nếu cần như: cập nhật tên cột, ẩn cột không cần thiết...
                if (dgvDSCTPM.Columns.Count > 0)
                {
                    if (dgvDSCTPM.Columns.Count > 0) dgvDSCTPM.Columns[0].HeaderText = "Mã Thiết Bị";
                    if (dgvDSCTPM.Columns.Count > 1) dgvDSCTPM.Columns[1].HeaderText = "Tên Thiết Bị";
                    if (dgvDSCTPM.Columns.Count > 2) dgvDSCTPM.Columns[2].HeaderText = "Nhà Sản Xuất";
                    if (dgvDSCTPM.Columns.Count > 3) dgvDSCTPM.Columns[3].HeaderText = "Số Lượng";
                    if (dgvDSCTPM.Columns.Count > 4) dgvDSCTPM.Columns[4].HeaderText = "Tên Nhà Cung Cấp";
                }

            }
            else
            {
                // Nếu không có dữ liệu
                MessageBox.Show("Không có thiết bị mượn nào.");
            }
        }



        private void LoadTrangThaiComboBox()
        {
            cbbTrangThai.Items.Clear();
            cbbTrangThai.Items.Add("Chưa trả");
            cbbTrangThai.Items.Add("Đã trả");
            cbbTrangThai.SelectedIndex = 0; // Chọn "Chưa trả" là mặc định
        }


        private void FormMuonThietBi_Load1(object sender, EventArgs e)
        {
            LoadDataToDGVDSPhieuMuon();
            LoadTrangThaiComboBox();
        }

        private void LoadDataToDGVDSPhieuMuon()
        {
            var listPM = MuonThietBiBUS.GetMuonThietBiWithDetails();
            dgvDSPhieuMuon.DataSource = listPM;
        }


    }
}
