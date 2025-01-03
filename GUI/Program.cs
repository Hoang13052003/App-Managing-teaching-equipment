﻿using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Drawing;
using System.Threading;
using Timer = System.Windows.Forms.Timer;

namespace GUI
{
    internal static class AccountInfo
    {
        public static string MaNguoiDung { get; set; }
        public static string TenDangNhap { get; set; }

        public static void SetAccountInfo(NguoiDungDTO user)
        {
            MaNguoiDung = user.MaNguoiDung;
            TenDangNhap = user.TenDangNhap;
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
        public static Label LbNameForm { get; set; }

        public static void OpenForm<T>(Form currentForm) where T : Form, new()
        {
            // Đóng form hiện tại
            currentForm.Close(); // Đóng hoàn toàn form hiện tại

            // Tạo một thể hiện mới của form cần mở
            T newForm = new T();
            newForm.Show(); // Hiển thị form mới
        }
        public static void OpenForm(Form newForm)
        {
            // Tạo một thể hiện mới của form cần mở
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
            T newForm = new T();
            newForm.TopLevel = false;
            newForm.FormBorderStyle = FormBorderStyle.None;
            panel.Controls.Clear();
            panel.Controls.Add(newForm);

            newForm.Dock = DockStyle.Fill;
            newForm.Show();
        }
        public static async Task OpenFormInPanelAsync<T>(Panel panel, Form loadingForm) where T : Form, new()
        {
            // Hiển thị form loading trước
            loadingForm.TopLevel = false;
            loadingForm.FormBorderStyle = FormBorderStyle.None;
            loadingForm.Dock = DockStyle.Fill;
            panel.Controls.Add(loadingForm);
            loadingForm.Show();
            loadingForm.BringToFront();

            // Giả lập tác vụ "nặng" (thay bằng công việc thực tế nếu có)
            await Task.Delay(100); // Hoặc thay bằng tác vụ thực tế bạn muốn chạy

            // Tạo form mới
            T newForm = new T();
            newForm.TopLevel = false;
            newForm.FormBorderStyle = FormBorderStyle.None;
            newForm.Dock = DockStyle.Fill;
            newForm.Opacity = 0;  // Để form mới bắt đầu ở trạng thái trong suốt

            // Hiển thị form mới
            panel.Controls.Clear(); // Dọn dẹp các control cũ trong panel
            panel.Controls.Add(newForm); // Thêm form mới vào panel
            newForm.Show();

            // Dần dần tăng độ mờ của form mới (Hiệu ứng chuyển động mượt mà)
            for (double opacity = 0; opacity <= 1; opacity += 0.05)
            {
                newForm.Opacity = opacity;
                await Task.Delay(30); // Điều chỉnh độ mượt bằng thời gian delay
            }

            // Đóng form loading sau khi form mới mở xong
            loadingForm.Close();
        }




        public static void OpenFormInPanel(Panel panel, Form newForm)
        {
            // Tạo một thể hiện mới của form cần mở
            newForm.TopLevel = false;
            newForm.FormBorderStyle = FormBorderStyle.None;
            //newForm.Dock = DockStyle.Fill;

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

        public static async void OpenDashboard<T>(Form currentForm) where T : Form, new()
        {

            // Tạo form mới
            T newForm = new T();
            newForm.Opacity = 0;
            newForm.Hide();
            
            await FadeOutAsync(currentForm);

            await Task.Delay(500);

            newForm.Show();
            // Hiển thị form mới với hiệu ứng fade in
            await FadeInAsync(newForm);
        }

        public static async void OpenDashboard_Loading<T>(Form currentForm, bool check) where T : Form, new()
        {
            await FadeOutAsync_Loading(currentForm);

            // Tạo form mới
            T newForm = new T();
            newForm.Opacity = 0;
            newForm.Show();

            // Chờ 10 giây trước khi hiện form mới
            await Task.Delay(500);

            if (check)
            {
                await FadeInAsync(newForm);
            }
            else
            {
                newForm.Hide();
            }
            // Hiển thị form mới với hiệu ứng fade in
        }

        private static async Task FadeOutAsync(Form form)
        {
            for (double opacity = form.Opacity; opacity > 0; opacity -= 0.1)
            {
                form.Opacity = opacity;
                await Task.Delay(25); // Khoảng thời gian giữa các lần giảm độ mờ
            }
            form.Close();
        }
        private static async Task FadeOutAsync_Loading(Form form)
        {
            for (double opacity = form.Opacity; opacity >= 0; opacity -= 0.1)
            {
                form.Opacity = opacity;
                await Task.Delay(25); // Khoảng thời gian giữa các lần giảm độ mờ
            }
            form.Hide();
        }
        private static async Task FadeInAsync(Form form)
        {
            for (double opacity = form.Opacity; opacity < 1.1; opacity += 0.1)
            {
                form.Opacity = opacity;
                await Task.Delay(25);
            }
        }
        //private static async Task FadeInAsync_Loading(Form form)
        //{
        //    form.Hide();
        //    //for (double opacity = form.Opacity; opacity < 1.1; opacity += 0.1)
        //    //{
        //    //    form.Opacity = opacity;
        //    //    await Task.Delay(25);
        //    //}
        //}

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
            Application.Run(new Loading());

        }
    }
}
