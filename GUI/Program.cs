using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace GUI
{
    internal static class AccountInfo
    {
        public static string MaNguoiDung { get; set; }

        public static void SetAccountInfo(NguoiDungDTO user)
        {
            MaNguoiDung = user.MaNguoiDung;
        }

        public static void Clear()
        {
            MaNguoiDung = null;
        }
    }
    public static class FormTask
    {
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
            Application.Run(new FormMuonThietBi());
        }
    }
}
