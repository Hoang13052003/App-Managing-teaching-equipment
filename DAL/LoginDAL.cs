using DTO;
using System;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Windows.Forms;
using System.Configuration;
using Microsoft.VisualBasic;
namespace DAL
{
    public class LoginDAL : DatabaseHelper
    {
        private NguoiDungDAL NDDAL = new NguoiDungDAL();
        private ThongTinCaNhanDAL TTCNDAL = new ThongTinCaNhanDAL();
        public NguoiDungDTO Login(string tenDangNhap, string matKhau)
        {
            NguoiDungDTO nguoiDung = null;
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;

            try
            {
                connection = GetConnection();
                command = new SqlCommand("SELECT * FROM NguoiDung WHERE TenDangNhap = @TenDangNhap", connection);
                command.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                connection.Open();

                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string storedHash = reader["MatKhau"].ToString();

                    // Mã hóa mật khẩu người dùng nhập vào và so sánh với mật khẩu lưu trữ
                    if (VerifyPassword(matKhau ,storedHash))
                    {
                        nguoiDung = new NguoiDungDTO
                        {
                            MaNguoiDung = reader["MaNguoiDung"].ToString(),
                            TenDangNhap = reader["TenDangNhap"].ToString(),
                            MatKhau = storedHash,
                            TrangThai = (bool)reader["TrangThai"]
                        };
                    }
                }
            }
            catch (System.Exception ex)
            {
                // Log lỗi thay vì hiển thị
                System.Console.WriteLine("Lỗi khi đăng nhập: " + ex.Message);
            }
            finally
            {
                // Đảm bảo rằng các tài nguyên được giải phóng
                reader?.Close();
                command?.Dispose();
                connection?.Close();
            }
            return nguoiDung;
        }

        public bool Register(NguoiDungDTO nguoiDung)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                // Kiểm tra xem tên đăng nhập đã tồn tại chưa
                if (NDDAL.CheckIfUsernameExists(nguoiDung.TenDangNhap))
                {
                    System.Console.WriteLine("Tên đăng nhập đã tồn tại.");
                    return false; // Tên đăng nhập đã tồn tại
                }

                // Mã hóa mật khẩu trực tiếp mà không sử dụng salt
                string hashedPassword = HashPassword(nguoiDung.MatKhau);

                connection = GetConnection();
                command = new SqlCommand(
                    "INSERT INTO NguoiDung (MaNguoiDung, TenDangNhap, MatKhau, TrangThai) VALUES (@MaNguoiDung, @TenDangNhap, @MatKhau, @TrangThai)",
                    connection);
                command.Parameters.AddWithValue("@MaNguoiDung", NDDAL.GenerateUniqueUserCode());
                command.Parameters.AddWithValue("@TenDangNhap", nguoiDung.TenDangNhap);
                command.Parameters.AddWithValue("@MatKhau", hashedPassword);
                command.Parameters.AddWithValue("@TrangThai", true); // Trạng thái mặc định là true (hoạt động)
                connection.Open();

                return command.ExecuteNonQuery() > 0;
            }
            catch (System.Exception ex)
            {
                // Log lỗi thay vì hiển thị
                System.Console.WriteLine("Lỗi khi đăng ký: " + ex.Message);
                return false;
            }
            finally
            {
                // Đảm bảo rằng các tài nguyên được giải phóng
                command?.Dispose();
                connection?.Close();
            }
        }


        public NguoiDungDTO RecoverPassword(string tenDangNhap)
        {
            // 1. Lấy thông tin người dùng từ cơ sở dữ liệu
            var nguoiDung = NDDAL.GetByTenDangNhap(tenDangNhap);

            if (nguoiDung == null)
            {
                MessageBox.Show("Không tồn tại người dùng này!", "Thông báo");
                return null;
            }
            // 2. Tạo code xác thực mới ngẫu nhiên
            string verificationCode = GenerateRandomCode(8);
            SendCodeEmail(TTCNDAL.GetByMaNguoiDung(nguoiDung.MaNguoiDung).Email, verificationCode);

            if (ConfirmVerificationCode(verificationCode))
            {
                return nguoiDung;
            }

            return null;
        }

        //private string GenerateRandomPassword(int length)
        //{
        //    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        //    Random random = new Random();
        //    char[] password = new char[length];

        //    for (int i = 0; i < length; i++)
        //    {
        //        password[i] = chars[random.Next(chars.Length)];
        //    }
        //    return new string(password);
        //}
        private string GenerateRandomCode(int length)
        {
            const string chars = "0123456789";
            Random random = new Random();
            char[] password = new char[length];

            for (int i = 0; i < length; i++)
            {
                password[i] = chars[random.Next(chars.Length)];
            }
            return new string(password);
        }
        // Phương thức gửi email
        public void SendCodeEmail(string toEmail, string newCode)
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
                    message.Subject = "Mã xác thực của bạn";
                    message.Body = $"Mã xác thực của bạn là: {newCode}";
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

        public void SendEmail(string toEmail, string newPassword)
        {
            try
            {
                // Lấy thông tin cấu hình từ App.config
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
                    message.Subject = "Mật khẩu mới của bạn";
                    message.Body = $"Mật khẩu mới của bạn là: {newPassword}";
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

        //Xác thực để lấy lại mật khẩu
        private bool ConfirmVerificationCode(string inputCode)
        {
            string enteredCode = Microsoft.VisualBasic.Interaction.InputBox("Nhập mã xác thực đã gửi qua email của bạn:", "Xác Thực Mã", "");

            if (enteredCode == inputCode)
            {
                MessageBox.Show("Xác thực thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                return true;
            }
            else
            {
                MessageBox.Show("Mã xác thực không đúng, vui lòng thử lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return System.BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            string hash = HashPassword(enteredPassword);
            return hash == storedHash;
        }

        public string CheckRoles(String maNguoiDung)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;

            try
            {
                connection = GetConnection();
                command = new SqlCommand("SELECT NND.TenNhom FROM NguoiDungNhomNguoiDung ND_NND JOIN NhomNguoiDung NND ON ND_NND.MaNhom = NND.MaNhom WHERE ND_NND.MaNguoiDung = @maNguoiDung", connection);
                command.Parameters.AddWithValue("@maNguoiDung", maNguoiDung);
                connection.Open();
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string role = reader["TenNhom"].ToString();
                    return role;
                }

            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Lỗi khi đăng nhập: " + ex.Message);
            }
            finally
            {
                // Đảm bảo rằng các tài nguyên được giải phóng
                reader?.Close();
                command?.Dispose();
                connection?.Close();
            }
            return null;
        }


    }
}
