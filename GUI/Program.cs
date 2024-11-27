using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Drawing;

namespace GUI
{
    internal static class AccountInfo
    {
        public static string MaNguoiDung { get; set; }
        public static string TenNguoiDung { get; set; }

        public static void SetAccountInfo(NguoiDungDTO user)
        {
            MaNguoiDung = user.MaNguoiDung;
        }
        public static void Clear()
        {
            MaNguoiDung = null;
        }
    }

    public interface IParamForm
    {
        void SetParams(object param);
    }

    public static class FormTask
    {
        private static Panel pannel_change;

        public static Panel Pannel_change { get => pannel_change; set => pannel_change = value; }

        public static void OpenForm<T>(Form currentForm) where T : Form, new()
        {
            // Đóng form hiện tại
            currentForm.Hide(); // Ẩn form hiện tại

            // Tạo một thể hiện mới của form cần mở
            T newForm = new T();

            newForm.FormClosed += (s, args) => currentForm.Show(); // Khi form mới đóng, hiển thị lại form hiện tại
            newForm.Show(); // Hiển thị form mới
        }

        public static void OpenForm<T>(NguoiDungDTO nguoiDung) where T : Form
        {
            // Tạo một thể hiện mới của form cần mở
            T newForm = (T)Activator.CreateInstance(typeof(T), nguoiDung);

            newForm.Show(); // Hiển thị form mới
        }

        public static void OpenFormInPanel<T>(Panel panel) where T : Form, new()
        {
            // Tạo một thể hiện mới của form cần mở
            T newForm = new T();
            newForm.TopLevel = false; // Thiết lập form không phải là form cấp cao
            newForm.FormBorderStyle = FormBorderStyle.None; // Không hiển thị viền
            panel.Controls.Clear(); // Xóa tất cả điều khiển trong panel
            panel.Controls.Add(newForm); // Thêm form mới vào panel

            newForm.Dock = DockStyle.Fill; // Đảm bảo form lấp đầy panel
            newForm.Show(); // Hiển thị form mới
        }
        public static void OpenFormInPanel(Panel panel, Form newForm)
        {
            // Tạo một thể hiện mới của form cần mở
            newForm.TopLevel = false;
            newForm.FormBorderStyle = FormBorderStyle.None;

            //panel.Controls.Clear();
            panel.Controls.Add(newForm);

            //newForm.Dock = DockStyle.Fill;
            newForm.Show();
        }
        public static void OpenFormInPanel<T>(Panel panel, ThoiKhoaBieuDTO tkb) where T : Form
        {
            T newForm = (T)Activator.CreateInstance(typeof(T), tkb);
            newForm.TopLevel = false; // Thiết lập form không phải là form cấp cao
            newForm.FormBorderStyle = FormBorderStyle.None; // Không hiển thị viền

            panel.Controls.Clear(); // Xóa tất cả điều khiển trong panel
            panel.Controls.Add(newForm); // Thêm form mới vào panel

            newForm.Dock = DockStyle.Fill; // Đảm bảo form lấp đầy panel
            newForm.Show(); // Hiển thị form mới
        }
        public static void OpenFormInPanel<T>(Panel panel, ThoiKhoaBieuChiTietDTO tkb) where T : Form
        {
            T newForm = (T)Activator.CreateInstance(typeof(T), tkb);
            newForm.TopLevel = false; // Thiết lập form không phải là form cấp cao
            newForm.FormBorderStyle = FormBorderStyle.None; // Không hiển thị viền

            panel.Controls.Clear(); // Xóa tất cả điều khiển trong panel
            panel.Controls.Add(newForm); // Thêm form mới vào panel

            newForm.Dock = DockStyle.Fill; // Đảm bảo form lấp đầy panel
            newForm.Show(); // Hiển thị form mới
        }

        public static void OpenFormInPanelWithParams<T>(Panel panel, params object[] parameters) where T : Form
        {
            // Tạo một thể hiện mới của form cần mở với các tham số
            T newForm = (T)Activator.CreateInstance(typeof(T), parameters);
            newForm.TopLevel = false;
            newForm.FormBorderStyle = FormBorderStyle.None;

            //panel.Controls.Clear();
            panel.Controls.Add(newForm);

            //newForm.Dock = DockStyle.Fill;
            newForm.Show();
        }
    }

    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dateTime, DayOfWeek startOfWeek)
        {
            int diff = dateTime.DayOfWeek - startOfWeek;
            if (diff < 0) diff += 7;
            return dateTime.AddDays(-1 * diff).Date;
        }
    }

    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Dashboard_User());

        }
    }
}
