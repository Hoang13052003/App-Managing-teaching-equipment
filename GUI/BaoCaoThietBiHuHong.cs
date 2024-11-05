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

namespace GUI
{
    public partial class BaoCaoThietBiHuHong : Form
    {
        private Dictionary<int, List<string>> deviceImages = new Dictionary<int, List<string>>();
        YeuCauThietBiBUS y = new YeuCauThietBiBUS();
        BienBanXuLyBUS b = new BienBanXuLyBUS();
        public BaoCaoThietBiHuHong()
        {
            InitializeComponent();
            this.btnThemAnh.Click += BtnThemAnh_Click;
            this.Load += BaoCaoThietBiHuHong_Load;
            this.btnSearch.Click += BtnSearch_Click;
            this.btnThem.Click += BtnThem_Click;
            this.btnXoa.Click += BtnXoa_Click;
            this.btnLamMoi.Click += BtnLamMoi_Click;
            this.cboLoaiTB.SelectedIndexChanged += CboLoaiTB_SelectedIndexChanged;
            this.cboThietBi.SelectedIndexChanged += CboThietBi_SelectedIndexChanged;
            this.btnGui.Click += BtnGui_Click;
            this.btnReset.Click += BtnReset_Click;
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void BtnGui_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Xác nhận gửi báo cáo?",
                "Thông báo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                // Kiểm tra điều kiện cho dgvThietBiHong
                if (dgvThietBiHong.Rows.Count == 0 ||
                (dgvThietBiHong.Rows.Count == 1 && dgvThietBiHong.Rows[0].Cells["MaCTTB_NCC"].Value == null))
                {
                    MessageBox.Show("Danh sách thiết bị hỏng không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Kiểm tra các trường thông tin
                if (string.IsNullOrWhiteSpace(txtHoTen.Text))
                {
                    MessageBox.Show("Họ tên không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtHoTen.Focus();
                    return;
                }

                if (cboVaiTro.SelectedIndex <= 0)
                {
                    MessageBox.Show("Vui lòng chọn vai trò.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtMoTa.Text))
                {
                    MessageBox.Show("Mô tả không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMoTa.Focus();
                    return;
                }

                DateTime thoiGian;
                if (string.IsNullOrWhiteSpace(txtThoiGian.Text) || !DateTime.TryParseExact(txtThoiGian.Text, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out thoiGian))
                {
                    MessageBox.Show("Thời gian không hợp lệ hoặc không đúng định dạng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                BienBanXuLyDTO bienBan = new BienBanXuLyDTO
                {
                    TenNguoiLamHong = txtHoTen.Text,
                    VaiTro = cboVaiTro.Text,
                    ThoiGianLamHong = thoiGian,
                    ThoiGianXuLy = null,
                    MoTaChiTiet = txtMoTa.Text,
                    ChiPhiSuaChua = 0,
                    TinhTrang = 0
                };
                List<ChiTietBienBanDTO> chiTietList = new List<ChiTietBienBanDTO>();

                string commonImageFolder = @"D:\DOAN_TOTNGHIEP\Code\ImageThietBiHuHong"; // Đường dẫn thư mục chung
                if (!System.IO.Directory.Exists(commonImageFolder))
                {
                    System.IO.Directory.CreateDirectory(commonImageFolder);
                }

                int validImageCount = 0;

                foreach (DataGridViewRow row in dgvThietBiHong.Rows)
                {
                    if (row.Cells["MaCTTB_NCC"].Value != null && row.Cells["MaCTTB_NCC"].Value != DBNull.Value)
                    {
                        int maCTTB_NCC = (int)row.Cells["MaCTTB_NCC"].Value;
                        string hinhAnh = "";

                        if (deviceImages.ContainsKey(maCTTB_NCC) && deviceImages[maCTTB_NCC].Count > 0)
                        {
                            string originalImagePath = deviceImages[maCTTB_NCC][0];
                            string imageName = $"img_{maCTTB_NCC}_{DateTime.Now:yyyyMMddHHmmss}.jpg";
                            string newImagePath = System.IO.Path.Combine(commonImageFolder, imageName);

                            try
                            {
                                System.IO.File.Copy(originalImagePath, newImagePath, true); // Sao chép ảnh vào thư mục chung
                                hinhAnh = imageName; // Lưu chỉ tên ảnh
                                validImageCount++;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Không thể lưu ảnh cho thiết bị {maCTTB_NCC}: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Thiết bị {maCTTB_NCC} không có hình ảnh.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        ChiTietBienBanDTO chiTiet = new ChiTietBienBanDTO
                        {
                            MaCTTB_NCC = maCTTB_NCC,
                            HinhAnh = hinhAnh // Lưu tên ảnh thay vì đường dẫn đầy đủ
                        };

                        chiTietList.Add(chiTiet);
                    }
                }

                if (validImageCount != chiTietList.Count)
                {
                    MessageBox.Show("Số lượng hình ảnh không khớp với số lượng thiết bị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (b.Insert(bienBan, chiTietList))
                {
                    MessageBox.Show("Gửi báo cáo thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LamMoi();
                    Reset();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi gửi báo cáo.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
        }
        void LamMoi()
        {
            LoadCboLoaiThietBi();
            LoadThietBi();
            LoadDgvDSChiTietThietBi();
        }
        void Reset()
        {
            txtHoTen.Text = string.Empty;
            txtMoTa.Text = string.Empty;
            cboVaiTro.Items.Clear();
            LoadCboVaiTro();
            DateTime now = DateTime.Now;
            txtThoiGian.Text = now.ToString("dd/MM/yyyy HH:mm");
            foreach (Control control in flowLayoutPanelHinhAnh.Controls.OfType<PictureBox>().ToList())
            {
                flowLayoutPanelHinhAnh.Controls.Remove(control);
                control.Dispose(); 
            }

            dgvThietBiHong.Rows.Clear();

            deviceImages.Clear();
        }

        private void CboThietBi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboThietBi.SelectedValue != null && int.TryParse(cboThietBi.SelectedValue.ToString(), out int maThietBi))
            {
                SearchDgvDSChiTietThietBi(maThietBi);
            }
        }

        private void CboLoaiTB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLoaiTB.SelectedValue != null && int.TryParse(cboLoaiTB.SelectedValue.ToString(), out int maLoai))
            {
                cboThietBi.DataSource = y.SearchThietBi(maLoai);
                cboThietBi.DisplayMember = "TenTB";
                cboThietBi.ValueMember = "MaTB";
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgvThietBiHong.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvThietBiHong.SelectedRows)
                {
                    if (row.Cells["MaCTTB_NCC"].Value != null)
                    {
                        int selectedDeviceId = (int)row.Cells["MaCTTB_NCC"].Value;

                        // Xóa ảnh khỏi deviceImages nếu tồn tại
                        if (deviceImages.ContainsKey(selectedDeviceId))
                        {
                            deviceImages.Remove(selectedDeviceId);
                        }

                        // Xóa PictureBox khỏi flowLayoutPanelHinhAnh
                        var pictureBoxToRemove = flowLayoutPanelHinhAnh.Controls
                            .OfType<PictureBox>()
                            .Where(p => p.Tag != null && (int)p.Tag == selectedDeviceId)
                            .ToList();

                        foreach (var picBox in pictureBoxToRemove)
                        {
                            flowLayoutPanelHinhAnh.Controls.Remove(picBox);
                            picBox.Dispose();
                        }
                    }

                    // Xóa dòng trong DataGridView
                    dgvThietBiHong.Rows.Remove(row);
                }
                dgvThietBiHong.ClearSelection();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một thiết bị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            if(dgvThietBiHong.Rows.Count > 6)
            {
                MessageBox.Show("Mỗi lần báo cáo chỉ tối đa 6 thiết bị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dgvThietBi.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvThietBi.SelectedRows[0];

                var trangThaiCell = selectedRow.Cells["TinhTrang"].Value;
                if (trangThaiCell != null && trangThaiCell.ToString() == "Hỏng")
                {
                    MessageBox.Show("Thiết bị đã được báo cáo hư hỏng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dgvThietBiHong.Rows.Count == 0)
                {
                    DataGridViewRow newRow = (DataGridViewRow)selectedRow.Clone();
                    foreach (DataGridViewCell cell in selectedRow.Cells)
                    {
                        newRow.Cells[cell.ColumnIndex].Value = cell.Value;
                    }
                    dgvThietBiHong.Rows.Add(newRow);
                }
                else
                {
                    bool exists = false;

                    foreach (DataGridViewRow row in dgvThietBiHong.Rows)
                    {
                        var cellValue1 = row.Cells["MaCTTB_NCC"].Value;
                        var cellValue2 = selectedRow.Cells["MaCTTB_NCC"].Value;

                        if (cellValue1 == null || cellValue2 == null)
                            continue;

                        if (cellValue1.ToString() == cellValue2.ToString())
                        {
                            exists = true;
                            break;
                        }
                    }

                    if (!exists)
                    {
                        DataGridViewRow newRow = (DataGridViewRow)selectedRow.Clone();
                        foreach (DataGridViewCell cell in selectedRow.Cells)
                        {
                            newRow.Cells[cell.ColumnIndex].Value = cell.Value;
                        }
                        dgvThietBiHong.Rows.Add(newRow);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một thiết bị để thêm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == string.Empty) return;
            dgvThietBi.DataSource = y.SearchKeyChiTietThietBi(txtSearch.Text);
        }

        private void BaoCaoThietBiHuHong_Load(object sender, EventArgs e)
        {
            LoadCboLoaiThietBi();
            LoadThietBi();
            LoadThietBiHong();
            LoadDgvDSChiTietThietBi();
            LoadCboVaiTro();
            DateTime now = DateTime.Now;
            txtThoiGian.Text = now.ToString("dd/MM/yyyy HH:mm");
        }

        void LoadCboVaiTro()
        {
            cboVaiTro.Items.Add(new { Text = "Chọn vai trò", Value = 0 }); 
            cboVaiTro.Items.Add(new { Text = "Giáo viên", Value = 1 });
            cboVaiTro.Items.Add(new { Text = "Học sinh", Value = 2 });
            cboVaiTro.Items.Add(new { Text = "Bộ phận kỹ thuật", Value = 3 }); 
            cboVaiTro.Items.Add(new { Text = "Bộ phận quản lý", Value = 4 });
            cboVaiTro.Items.Add(new { Text = "Khách", Value = 4 });

            cboVaiTro.DisplayMember = "Text";
            cboVaiTro.ValueMember = "Value";

            cboVaiTro.SelectedIndex = 0;
        }

        void LoadCboLoaiThietBi()
        {
            cboLoaiTB.DataSource = y.getAllLoaiTB();
            cboLoaiTB.DisplayMember = "TenLoai";
            cboLoaiTB.ValueMember = "MaLoai";
        }
        void LoadThietBi()
        {
            cboThietBi.DataSource = y.getAllThietBi();
            cboThietBi.DisplayMember = "TenTB";
            cboThietBi.ValueMember = "MaTB";
        }
        void SearchDgvDSChiTietThietBi(int pMaTB)
        {
            dgvThietBi.DataSource = y.SearchChiTietThietBi(pMaTB);
            dgvThietBi.Columns["MaCTTB_NCC"].HeaderText = "Mã chi tiết";
            dgvThietBi.Columns["TenTB"].HeaderText = "Tên thiết bị";
            dgvThietBi.Columns["TinhTrang"].HeaderText = "Tình trạng";
            dgvThietBi.Columns["TrangThai"].HeaderText = "Trạng thái";
            dgvThietBi.Columns["NgayMua"].Visible = false;
        }

        void LoadDgvDSChiTietThietBi()
        {
            dgvThietBi.DataSource = y.getAllChiTietThietBi();
            dgvThietBi.Columns["MaCTTB_NCC"].HeaderText = "Mã chi tiết";
            dgvThietBi.Columns["TenTB"].HeaderText = "Tên thiết bị";
            dgvThietBi.Columns["TinhTrang"].HeaderText = "Tình trạng";
            dgvThietBi.Columns["TrangThai"].HeaderText = "Trạng thái";
            dgvThietBi.Columns["NgayMua"].Visible = false;
        }

        void LoadThietBiHong()
        {
            dgvThietBiHong.Columns.Add("MaCTTB_NCC", "Mã chi tiết");
            dgvThietBiHong.Columns.Add("TenTB", "Tên thiết bị");
            dgvThietBiHong.Columns.Add("TinhTrang", "Tình trạng");
            dgvThietBiHong.Columns.Add("TrangThai", "Trạng thái");
            dgvThietBiHong.Columns.Add("NgayMua", "Ngày mua");
            dgvThietBiHong.Columns["NgayMua"].Visible = false;
        }

        private void BtnThemAnh_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu không có dòng nào được chọn
            if (dgvThietBiHong.SelectedRows.Count == 0 || dgvThietBiHong.SelectedRows[0].Cells["MaCTTB_NCC"].Value == null)
            {
                MessageBox.Show("Vui lòng chọn một thiết bị hư hỏng trước khi thêm hình ảnh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;
                int selectedDeviceId = (int)dgvThietBiHong.SelectedRows[0].Cells["MaCTTB_NCC"].Value;

                // Kiểm tra nếu thiết bị đã có ảnh
                if (deviceImages.ContainsKey(selectedDeviceId) && deviceImages[selectedDeviceId].Count > 0)
                {
                    // Cập nhật đường dẫn ảnh trong danh sách
                    deviceImages[selectedDeviceId][0] = selectedFilePath;

                    // Cập nhật ảnh trong PictureBox đã có sẵn trong FlowLayoutPanel
                    PictureBox existingPicBox = flowLayoutPanelHinhAnh.Controls
                        .OfType<PictureBox>()
                        .FirstOrDefault(p => p.Tag != null && (int)p.Tag == selectedDeviceId);

                    if (existingPicBox != null)
                    {
                        existingPicBox.Image = Image.FromFile(selectedFilePath);
                    }
                }
                else
                {
                    // Nếu thiết bị chưa có ảnh, thêm đường dẫn vào danh sách và tạo PictureBox mới
                    if (!deviceImages.ContainsKey(selectedDeviceId))
                    {
                        deviceImages[selectedDeviceId] = new List<string>();
                    }
                    deviceImages[selectedDeviceId].Add(selectedFilePath);

                    PictureBox picBox = new PictureBox();
                    picBox.Image = Image.FromFile(selectedFilePath);
                    picBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    picBox.Width = 100;
                    picBox.Height = 100;
                    picBox.Tag = selectedDeviceId; // Gắn Tag để dễ dàng nhận diện PictureBox cho thiết bị này

                    flowLayoutPanelHinhAnh.Controls.Add(picBox);
                }
            }
        }

    }
}
