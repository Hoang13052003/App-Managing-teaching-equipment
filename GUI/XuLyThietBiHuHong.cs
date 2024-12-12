using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GUI
{
    public partial class XuLyThietBiHuHong : Form
    {
        BienBanXuLyBUS b = new BienBanXuLyBUS();
        public XuLyThietBiHuHong()
        {
            InitializeComponent();
            this.Load += XuLyThietBiHuHong_Load;
            this.dgvDSBienBan.CellClick += DgvDSBienBan_CellClick;
            this.btnLamMoi.Click += BtnLamMoi_Click;
            //this.btnSearch.Click += BtnSearch_Click;
            this.dgvChiTietBB.CellClick += DgvChiTietBB_CellClick;
            this.btnCapNhat.Click += BtnCapNhat_Click;
        }

        private void BtnCapNhat_Click(object sender, EventArgs e)
        {
            // Kiểm tra thông tin đầu vào
            if (txtMaBB.Text == string.Empty || txtHoTen.Text == string.Empty || cboVaiTro.SelectedIndex <= 0
                || txtThoiGian.Text == string.Empty || txtThoiGianXuLy.Text == string.Empty
                || txtChiPhi.Text == string.Empty || cboTinhTrang.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cboTinhTrang.SelectedIndex != 1)
            {
                MessageBox.Show("Vui lòng chọn tình trạng đã xử lý!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Kiểm tra định dạng thời gian
            DateTime thoiGian;
            if (string.IsNullOrWhiteSpace(txtThoiGian.Text) || !DateTime.TryParseExact(txtThoiGian.Text, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out thoiGian))
            {
                MessageBox.Show("Thời gian không hợp lệ hoặc không đúng định dạng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo đối tượng BienBanXuLyDTO
            BienBanXuLyDTO bienBan = new BienBanXuLyDTO()
            {
                MaBB = Convert.ToInt32(txtMaBB.Text),
                TenNguoiLamHong = txtHoTen.Text,
                VaiTro = cboVaiTro.Text,
                ThoiGianLamHong = thoiGian,
                ThoiGianXuLy = DateTime.Now,
                ChiPhiSuaChua = Convert.ToSingle(txtChiPhi.Text),
                TinhTrang = cboTinhTrang.SelectedIndex,
            };

            List<ChiTietBienBanDTO> chiTietList = b.GetAllChiTietBB();

            if (chiTietList != null && chiTietList.Count > 0)
            {
                bool isUpdated = b.Update(bienBan, chiTietList);
                if (isUpdated)
                {
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    string emailNguoiNhan = "anhyeuem13052003@gmail.com"; // Email người làm hỏng
                    var _list_chiTietBienBan = b.SearchChiTietBB(bienBan.MaBB);
                    
                    SendCodeEmail(emailNguoiNhan, _list_chiTietBienBan, bienBan);

                    MessageBox.Show("Biên bản đã được lập và email yêu cầu bồi thường đã được gửi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    LamMoi();
                }
                else
                {
                    MessageBox.Show("Cập nhật không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Không có chi tiết để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void SendCodeEmail(string toEmail, List<ChiTietBienBanDTO> list, BienBanXuLyDTO itemBB)
        {
            try
            {
                string fromEmail = ConfigurationManager.AppSettings["EmailSender"];
                string emailPassword = ConfigurationManager.AppSettings["EmailPassword"];
                string smtpHost = ConfigurationManager.AppSettings["SmtpServer"];
                int smtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);

                if (string.IsNullOrEmpty(fromEmail) || string.IsNullOrEmpty(emailPassword) || string.IsNullOrEmpty(smtpHost))
                {
                    MessageBox.Show("Thông tin cấu hình email không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string tableRows = "";
                for (int i = 0; i < list.Count; i++)
                {
                    tableRows += $@"
                <tr>
                    <td style='text-align:center;'>{i + 1}</td>
                    <td>{list[i].MaCTTB_NCC}</td>
                    <td>{list[i].TenTB}</td>
                    <td style='text-align:center;'>{list[i].MoTaChiTiet}</td>
                </tr>";
                }

                using (var message = new MailMessage())
                {
                    message.To.Add(toEmail);
                    message.Subject = "Yêu cầu bồi thường thiết bị bị hỏng";

                    // Nội dung email
                    message.Body = $@"
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
                            Thông báo yêu cầu bồi thường
                        </div>
                        <div class='email-content'>
                            <p>Thông tin người làm hỏng: <span class='highlight'>{itemBB.TenNguoiLamHong} - Vai trò: {itemBB.VaiTro}</span></p>
                            <p>Tổng chi phí bồi thường: <span class='highlight'>{itemBB.ChiPhiSuaChua} VNĐ</span></p>
                        </div>
                        <div class='email-header'>
                            Danh sách thiết bị hư hỏng
                        </div>
                        <table>
                            <thead>
                                <tr>
                                    <th>STT</th>
                                    <th>Mã CTTB_NCC</th>
                                    <th>Tên thiết bị</th>
                                    <th>Mô tả hư hỏng</th>
                                </tr>
                            </thead>
                            <tbody>
                                {tableRows}
                            </tbody>
                        </table>
                        <p>Vui lòng liên hệ phòng quản lý để hoàn tất thủ tục bồi thường.</p>
                    </div>
                </body>
                </html>
            ";

                    message.From = new MailAddress(fromEmail);
                    message.IsBodyHtml = true;

                    using (var smtp = new SmtpClient(smtpHost, smtpPort))
                    {
                        smtp.Credentials = new NetworkCredential(fromEmail, emailPassword);
                        smtp.EnableSsl = true;
                        smtp.Send(message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi gửi email: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvChiTietBB_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                LoadImage(e.RowIndex, dgvChiTietBB);
                txtMoTa.Text = dgvChiTietBB.Rows[e.RowIndex].Cells["MoTaChiTiet"].Value.ToString();
            }
        }
        void LoadImage(int rowIndex, DataGridView dgv)
        {
            pictureBox.Image = null;
            string imageName = dgv.Rows[rowIndex].Cells["HinhAnh"].Value.ToString();

            string projectDirectory = Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName).FullName;

            string projectImagePath = Path.Combine(projectDirectory, "Image", "thietBiHuHong");

            string combinedImagePath = Path.Combine(projectImagePath, imageName);

            if (File.Exists(combinedImagePath))
            {
                pictureBox.Image = System.Drawing.Image.FromFile(combinedImagePath);
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                //pictureBox.Width = 290;
                //pictureBox.Height = 220;

                pictureBox.Anchor = AnchorStyles.None;
                pictureBox.Margin = new Padding(10);
            }
            else
            {
                MessageBox.Show("Không tìm thấy ảnh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //private void BtnSearch_Click(object sender, EventArgs e)
        //{
        //    if (txtSearch.Text != string.Empty)
        //    {
        //        searchBienBan(txtSearch.Text);
        //    }
        //}

        void searchBienBan(string keyword)
        {
            dgvDSBienBan.CellFormatting -= DgvDSBienBan_CellFormatting;

            dgvDSBienBan.DataSource = b.SearchBienBan(keyword);
            dgvDSBienBan.Columns["MaBB"].HeaderText = "Mã biên bản";
            dgvDSBienBan.Columns["TenNguoiLamHong"].HeaderText = "Họ tên";
            dgvDSBienBan.Columns["VaiTro"].HeaderText = "Vai trò";
            dgvDSBienBan.Columns["ThoiGianLamHong"].HeaderText = "Thời gian";
            dgvDSBienBan.Columns["ThoiGianXuLy"].HeaderText = "Thời gian xử lý";
            dgvDSBienBan.Columns["MoTaChiTiet"].HeaderText = "Mô tả";
            dgvDSBienBan.Columns["ChiPhiSuaChua"].HeaderText = "Chi phí";
            dgvDSBienBan.Columns["TinhTrang"].HeaderText = "Tình trạng";

            dgvDSBienBan.CellFormatting += DgvDSBienBan_CellFormatting;
        }
        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
        }
        void LamMoi()
        {
            dgvDSBienBan.CellFormatting -= DgvDSBienBan_CellFormatting;
            cboTinhTrang.Items.Clear();
            cboVaiTro.Items.Clear();

            LoadCboTinhTrang();
            LoadCboVaiTro();
            LoadDSBienBan();
            LoadChiTietBB();
            //txtSearch.Text = txtHoTen.Text = txtMaBB.Text = txtChiPhi.Text = txtMoTa.Text = txtThoiGian.Text = string.Empty;
            pictureBox.Image = null;
        }
        private void DgvDSBienBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                pictureBox.Image = null;
                BingDings(e.RowIndex);
                searchChiTietBB(Convert.ToInt32(dgvDSBienBan.Rows[e.RowIndex].Cells["MaBB"].Value));
            }
        }

        private void XuLyThietBiHuHong_Load(object sender, EventArgs e)
        {
            LoadDSBienBan();
            LoadChiTietBB();
            LoadCboTinhTrang();
            LoadCboVaiTro();
        }
        void BingDings(int rowIndex)
        {
            DataGridViewRow row = dgvDSBienBan.Rows[rowIndex];

            txtMaBB.Text = row.Cells["MaBB"].Value?.ToString() ?? string.Empty;
            txtHoTen.Text = row.Cells["TenNguoiLamHong"].Value?.ToString() ?? string.Empty;
            cboVaiTro.Text = row.Cells["VaiTro"].Value?.ToString() ?? string.Empty;

            if (row.Cells["ThoiGianLamHong"].Value != null && DateTime.TryParse(row.Cells["ThoiGianLamHong"].Value.ToString(), out DateTime thoiGian))
            {
                txtThoiGian.Text = thoiGian.ToString("dd/MM/yyyy HH:mm");
            }
            else
            {
                txtThoiGian.Text = "00/00/0000 00:00";
            }

            if (row.Cells["ThoiGianXuLy"].Value != null && DateTime.TryParse(row.Cells["ThoiGianXuLy"].Value.ToString(), out DateTime thoiGianXuLy))
            {
                txtThoiGianXuLy.Text = thoiGianXuLy.ToString("dd/MM/yyyy HH:mm");
            }
            else
            {
                txtThoiGianXuLy.Text = "00/00/0000 00:00";
            }

            txtChiPhi.Text = row.Cells["ChiPhiSuaChua"].Value?.ToString() ?? string.Empty;
            cboTinhTrang.Text = Convert.ToInt32(row.Cells["TinhTrang"].Value) == 1 ? "Đã xử lý" : "Chưa xử lý";
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
        void LoadCboTinhTrang()
        {
            cboTinhTrang.Items.Add(new { Text = "Chưa xử lý", Value = 0 });
            cboTinhTrang.Items.Add(new { Text = "Đã xử lý", Value = 1 });
            cboTinhTrang.DisplayMember = "Text";
            cboTinhTrang.ValueMember = "Value";
            cboTinhTrang.SelectedIndex = 0;
        }
        void searchChiTietBB(int pMaBB)
        {
            dgvChiTietBB.DataSource = b.SearchChiTietBB(pMaBB);
            dgvChiTietBB.Columns["MaBB"].HeaderText = "Mã biên bản";
            dgvChiTietBB.Columns["MaCTTB_NCC"].HeaderText = "Mã thiết bị";
            dgvChiTietBB.Columns["TenTB"].HeaderText = "Tên thiết bị";
            dgvChiTietBB.Columns["HinhAnh"].HeaderText = "Hình ảnh";
        }
        void LoadChiTietBB()
        {
            dgvChiTietBB.DataSource = b.GetAllChiTietBB();
            dgvChiTietBB.Columns["MaBB"].HeaderText = "Mã biên bản";
            dgvChiTietBB.Columns["MaCTTB_NCC"].HeaderText = "Mã thiết bị";
            dgvChiTietBB.Columns["TenTB"].HeaderText = "Tên thiết bị";
            dgvChiTietBB.Columns["HinhAnh"].HeaderText = "Hình ảnh";
            dgvChiTietBB.Columns["MoTaChiTiet"].HeaderText = "Mô tả chi tiết";
        }
        void LoadDSBienBan()
        {
            dgvDSBienBan.DataSource = b.GetAll();
            dgvDSBienBan.Columns["MaBB"].HeaderText = "Mã biên bản";
            dgvDSBienBan.Columns["TenNguoiLamHong"].HeaderText = "Họ tên";
            dgvDSBienBan.Columns["VaiTro"].HeaderText = "Vai trò";
            dgvDSBienBan.Columns["ThoiGianLamHong"].HeaderText = "Thời gian";
            dgvDSBienBan.Columns["ThoiGianXuLy"].HeaderText = "Thời gian xử lý";
            dgvDSBienBan.Columns["ChiPhiSuaChua"].HeaderText = "Chi phí";
            dgvDSBienBan.Columns["TinhTrang"].HeaderText = "Tình trạng";

            dgvDSBienBan.CellFormatting += DgvDSBienBan_CellFormatting;
        }

        private void DgvDSBienBan_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvDSBienBan.Columns[e.ColumnIndex].Name == "TinhTrang" && e.Value != null)
            {
                int trangThai = (int)e.Value;
                switch (trangThai)
                {
                    case 0:
                        e.Value = "Chưa xử lý";
                        break;
                    case 1:
                        e.Value = "Đã xử lý";
                        break;
                    default:
                        e.Value = "Không xác định";
                        break;
                }
                e.FormattingApplied = true;
            }
        }
    }
}
