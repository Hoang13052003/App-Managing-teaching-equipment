﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using BUS;
using DTO;
using System.Configuration;

namespace GUI
{
    public partial class BaoDuong : Form
    {
        YeuCauThietBiBUS y = new YeuCauThietBiBUS();
        BaoDuongBUS b = new BaoDuongBUS(); 
        public BaoDuong()
        {
            InitializeComponent();
            this.Load += BaoDuong_Load;
            this.dgvYeuCau.CellClick += DgvYeuCau_CellClick;
            this.dgvBD.CellClick += DgvBD_CellClick;
            this.btnLamMoi.Click += BtnLamMoi_Click;
            this.btnSua.Click += BtnSua_Click;
            this.btnXoa.Click += BtnXoa_Click;
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Xác nhận xóa thông tin bảo dưỡng đang chọn?'",
                "Thông báo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                if (b.Delete(Convert.ToInt32(txtMaBD.Text)))
                {
                    MessageBox.Show("Đã xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LamMoi();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if(dgvBD.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvBD.SelectedRows[0];
                BaoDuongDTO baoDuong = new BaoDuongDTO();
                baoDuong.MaBD = Convert.ToInt32(selectedRow.Cells["MaBD"].Value);
                baoDuong.MaCTTB_NCC = Convert.ToInt32(selectedRow.Cells["MaCTTB_NCC"].Value);
                baoDuong.NgayBD = Convert.ToDateTime(ngayBD.Value);
                baoDuong.KetQua = txtKetQua.Text;
                baoDuong.ChiPhi = Convert.ToSingle(txtChiPhi.Text);

                if (b.Update(baoDuong))
                {
                    MessageBox.Show("Thông tin bảo dưỡng đã được thay đổi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LamMoi();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra, vui lòng thử lại sau!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn thông tin bảo dưỡng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
        }
        void LamMoi()
        {
            LoadBaoDuong();
            LoadYeuCau();
            LoadChiTietYC();
            txtMaBD.Text = string.Empty;
            txtTenTB.Text = string.Empty;
            txtKetQua.Text = string.Empty;
            txtChiPhi.Text = string.Empty;
            txtMaYC.Text = string.Empty;
            txtNguoiYeuCau.Text = string.Empty;
            ngayBD.Value = DateTime.Now;
            ngayYeuCau.Value = DateTime.Now;
            txtKetQuaSua.Text = string.Empty;
            txtChiPhiSua.Text = string.Empty;
        }

        private void DgvBD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                BingDingBaoDuongs(e.RowIndex);
            }
        }

        private void DgvYeuCau_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                BingDingYeuCaus(e.RowIndex);

                SearchChiTietYCTB(Convert.ToInt32(dgvYeuCau.Rows[e.RowIndex].Cells[0].Value));
            }
        }

        private void BaoDuong_Load(object sender, EventArgs e)
        {
            LoadChiTietYC();
            LoadBaoDuong();
            LoadYeuCau();
        }
        void LoadBaoDuong()
        {
            dgvBD.DataSource = b.GetAll();
            dgvBD.Columns["MaBD"].HeaderText = "Mã bảo dưỡng";
            dgvBD.Columns["TenTB"].HeaderText = "Tên thiết bị";
            dgvBD.Columns["TenPhong"].HeaderText = "Tên phòng";
            dgvBD.Columns["NgayBD"].HeaderText = "Ngày bảo dưỡng";
            dgvBD.Columns["KetQua"].HeaderText = "Kết quả";
            dgvBD.Columns["ChiPhi"].HeaderText = "Chi phí";
            dgvBD.Columns["MaCTTB_NCC"].Visible = false;
        }

        void BingDingBaoDuongs(int rowIndex)
        {
            DataGridViewRow row = dgvBD.Rows[rowIndex];
            txtMaBD.Text = row.Cells[0].Value.ToString();
            txtTenTB.Text = row.Cells[2].Value.ToString();
            txtPhong.Text = row.Cells[3].Value.ToString();
            ngayBD.Value = Convert.ToDateTime(row.Cells[4].Value);
            txtKetQua.Text = row.Cells[5].Value.ToString();
            txtChiPhi.Text = row.Cells[6].Value.ToString();
        }

        void LoadYeuCau() 
        {
            dgvYeuCau.DataSource = y.getAllYeuCauThietBi();
            dgvYeuCau.Columns["MaYC"].HeaderText = "Mã yêu cầu";
            dgvYeuCau.Columns["NgayYeuCau"].HeaderText = "Ngày yêu cầu";
            dgvYeuCau.Columns["TenNguoiDung"].HeaderText = "Người yêu cầu";
            dgvYeuCau.Columns["MaNguoiDung"].Visible = false;
        }

        void BingDingYeuCaus(int rowIndex)
        {
            DataGridViewRow row = dgvYeuCau.Rows[rowIndex];
            txtMaYC.Text = row.Cells[0].Value.ToString();
            txtNguoiYeuCau.Text = row.Cells[2].Value.ToString();
            ngayYeuCau.Value = Convert.ToDateTime(row.Cells[3].Value);
        }

        void SearchChiTietYCTB(int pMaYC)
        {
            dgvChiTietYC.DataSource = y.searchChiTietYeuCauSuaThietBi(pMaYC);
            dgvChiTietYC.Columns["MaYC"].HeaderText = "Mã yêu cầu";
            dgvChiTietYC.Columns["TenTB"].HeaderText = "Tên thiết bị";
            dgvChiTietYC.Columns["TenPhong"].HeaderText = "Phòng";
            dgvChiTietYC.Columns["LoaiYeuCau"].HeaderText = "Loại yêu cầu";
            dgvChiTietYC.Columns["GhiChu"].HeaderText = "Ghi chú";
            dgvChiTietYC.Columns["TrangThai"].HeaderText = "Trạng thái";
            dgvChiTietYC.Columns["MaCTTB_NCC"].Visible = false;
        }

        private bool isEventRegistered = false;
        void LoadChiTietYC()
        {
            dgvChiTietYC.DataSource = y.getAllChiTietYeuCauSuaThietBi();

            dgvChiTietYC.Columns["MaYC"].HeaderText = "Mã yêu cầu";
            dgvChiTietYC.Columns["TenTB"].HeaderText = "Tên thiết bị";
            dgvChiTietYC.Columns["TenPhong"].HeaderText = "Phòng";
            dgvChiTietYC.Columns["LoaiYeuCau"].HeaderText = "Loại yêu cầu";
            dgvChiTietYC.Columns["GhiChu"].HeaderText = "Ghi chú";
            dgvChiTietYC.Columns["TrangThai"].HeaderText = "Trạng thái";
            dgvChiTietYC.Columns["MaCTTB_NCC"].Visible = false;

            // Chỉ thêm cột button nếu chưa có
            if (!dgvChiTietYC.Columns.Contains("btnProcess"))
            {
                DataGridViewButtonColumn btnProcessColumn = new DataGridViewButtonColumn();
                btnProcessColumn.HeaderText = "Xử lý";
                btnProcessColumn.Name = "btnProcess";
                btnProcessColumn.Text = "✓ / ×";
                btnProcessColumn.UseColumnTextForButtonValue = false;
                dgvChiTietYC.Columns.Add(btnProcessColumn);
            }

            // Đăng ký sự kiện chỉ một lần
            if (!isEventRegistered)
            {
                dgvChiTietYC.CellFormatting += DgvChiTietYC_CellFormatting;
                dgvChiTietYC.CellPainting += DgvChiTietYC_CellPainting;
                dgvChiTietYC.CellContentClick += DgvChiTietYC_CellContentClick;
                isEventRegistered = true;
            }
        }


        private void DgvChiTietYC_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvChiTietYC.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                int trangThai = (int)e.Value;
                switch (trangThai)
                {
                    case 0:
                        e.Value = "Đang xử lý";
                        break;
                    case 1:
                        e.Value = "Hoàn thành";
                        break;
                    case 2:
                        e.Value = "Từ chối";
                        break;
                    default:
                        e.Value = "Không xác định";
                        break;
                }
                e.FormattingApplied = true;
            }
        }

        private void DgvChiTietYC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvChiTietYC.Columns["btnProcess"].Index && e.RowIndex >= 0)
            {
                var cellBounds = dgvChiTietYC.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                var clickLocation = dgvChiTietYC.PointToClient(Cursor.Position);

                var btn1Bounds = new Rectangle(cellBounds.Left + 5, cellBounds.Top + 5, cellBounds.Width / 2 - 10, cellBounds.Height - 10);
                var btn2Bounds = new Rectangle(cellBounds.Left + cellBounds.Width / 2 + 5, cellBounds.Top + 5, cellBounds.Width / 2 - 10, cellBounds.Height - 10);

                // Kiểm tra vị trí nhấn nút
                if (btn1Bounds.Contains(clickLocation)) // Nút "Hoàn thành"
                {
                    if (txtChiPhiSua.Text == string.Empty || txtKetQuaSua.Text == string.Empty)
                    {
                        MessageBox.Show("Cần cho biết kết quả và chi phí sửa chữa trước khi xác nhận!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    int maYC = Convert.ToInt32(dgvChiTietYC.Rows[e.RowIndex].Cells["MaYC"].Value);
                    int maCTTB_NCC = Convert.ToInt32(dgvChiTietYC.Rows[e.RowIndex].Cells["MaCTTB_NCC"].Value);
                    float chiPhi = Convert.ToSingle(txtChiPhiSua.Text);



                    YeuCauThietBiDTO yctb = y.getAllYeuCauThietBi().FirstOrDefault(item => item.MaYC == maYC);

                    bool success = y.UpdataTrangThaiCTYCTB(maYC, maCTTB_NCC, yctb.MaNguoiDung, 1, txtKetQuaSua.Text, chiPhi);



                    if (success)
                    {
                        MessageBox.Show("Đã hoàn thành cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        if (yctb != null)
                        {
                            BaoDuongDTO bd = b.GetByID(maCTTB_NCC, yctb.MaNguoiDung);
                            ThongTinCaNhanDTO item = new ThongTinCaNhanBUS().GetByMaNguoiDung(yctb.MaNguoiDung);
                            SendCodeEmail(item.Email, bd);
                        }

                        LamMoi();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thất bại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (btn2Bounds.Contains(clickLocation)) // Nút "Từ chối"
                {
                    int maYC = Convert.ToInt32(dgvChiTietYC.Rows[e.RowIndex].Cells["MaYC"].Value); 
                    int maCTTB_NCC = Convert.ToInt32(dgvChiTietYC.Rows[e.RowIndex].Cells["MaCTTB_NCC"].Value);
                    YeuCauThietBiDTO yctb = y.getAllYeuCauThietBi().FirstOrDefault(item => item.MaYC == maYC);
                    bool success = y.UpdataTrangThaiCTYCTB(maYC, maCTTB_NCC, yctb.MaNguoiDung, 2, "", 0);

                    if (success)
                    {
                        MessageBox.Show("Đã hoàn thành cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LamMoi();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thất bại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }
        private void SendCodeEmail(string toEmail, BaoDuongDTO item)
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

                using (var message = new MailMessage())
                {
                    message.To.Add(toEmail);
                    message.Subject = "Yêu cầu sửa chữa thiết bị của bạn đã được thực hiện:";
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
                                                .email-content {{
                                                    margin-bottom: 20px;
                                                }}
                                                .email-footer {{
                                                    margin-top: 20px;
                                                    font-size: 14px;
                                                    color: #555;
                                                    border-top: 1px solid #ddd;
                                                    padding-top: 10px;
                                                }}
                                                .highlight {{
                                                    font-weight: bold;
                                                    color: #d9534f;
                                                }}
                                            </style>
                                        </head>
                                        <body>
                                            <div class='email-container'>
                                                <div class='email-header'>
                                                    Yêu cầu sửa chữa thiết bị đã được thực hiện
                                                </div>
                                                <div class='email-content'>
                                                    <p>Thiết bị: <span class='highlight'>{item.TenTB.ToString()}</span> tại phòng: <span class='highlight'>{item.TenPhong.ToString()}</span> đã được bảo dưỡng.</p>
                                                    <p><b>Thông tin hỏng:</b></p>
                                                    <p>- {item.KetQua.ToString()}</p>
                                                    <p><b>Tổng chi phí sửa chữa:</b> <span class='highlight'>{item.ChiPhi.ToString()} VND</span></p>
                                                </div>
                                                <div class='email-footer'>
                                                    <p>Cảm ơn bạn đã sử dụng dịch vụ của chúng tôi.</p>
                                                </div>
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

        private void DgvChiTietYC_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == dgvChiTietYC.Columns["btnProcess"].Index && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var btn1Bounds = new Rectangle(e.CellBounds.Left + 5, e.CellBounds.Top + 5, e.CellBounds.Width / 2 - 10, e.CellBounds.Height - 10);
                var btn2Bounds = new Rectangle(e.CellBounds.Left + e.CellBounds.Width / 2 + 5, e.CellBounds.Top + 5, e.CellBounds.Width / 2 - 10, e.CellBounds.Height - 10);

                Color completeButtonColor = Color.Green;  
                Color rejectButtonColor = Color.Red;

                using (Brush brush = new SolidBrush(completeButtonColor))
                {
                    e.Graphics.FillRectangle(brush, btn1Bounds);
                }
                TextRenderer.DrawText(e.Graphics, "✓", e.CellStyle.Font, btn1Bounds, Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                using (Brush brush = new SolidBrush(rejectButtonColor))
                {
                    e.Graphics.FillRectangle(brush, btn2Bounds);
                }
                TextRenderer.DrawText(e.Graphics, "×", e.CellStyle.Font, btn2Bounds, Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                e.Handled = true;
            }
        }

        
    }
}
