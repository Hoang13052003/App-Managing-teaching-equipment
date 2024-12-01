using Microsoft.Win32;
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
    public partial class Loading : Form
    {
        //private int maxWidth = 995;
        //private int startWidth = 800;
        private Timer autoCloseTimer, start;
        private Login login = new Login();
        private bool _isLogin = false;
        public Loading()
        {
            InitializeComponent();
            InitializeAutoCloseTimer();
        }
        private void InitializeAutoCloseTimer()
        {
            autoCloseTimer = new Timer();
            autoCloseTimer.Interval = 7200;
            autoCloseTimer.Tick += AutoCloseTimer_Tick;

            start = new Timer();
            start.Interval = 5500;
            start.Tick += start_Tick;
        }
        private void AutoCloseTimer_Tick(object sender, EventArgs e)
        {
            this.Hide();
            timer1.Stop();
            autoCloseTimer.Stop();
        }
        private void start_Tick(object sender, EventArgs e)
        {
            if (_isLogin)
            {
                FormTask.OpenDashboard_Loading<Login>(this, false);
                start.Stop();
            }
            else
            {
                FormTask.OpenDashboard_Loading<Login>(this, true);
                start.Stop();
            }
        }
        // Tạo hàm để bắt đầu hoạt ảnh
        private void timer1_Tick(object sender, EventArgs e)
        {
            panelSlide.Width += 15;
            if (panelSlide.Width > 1000)
            {
                panelSlide.Width = 0;
            }
        }
        private (string UserName, string PassWord, bool IsLoggedIn) LoadLoginStateFromRegistry()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\MyApp");
            if (key != null)
            {
                string userName = key.GetValue("UserName")?.ToString();
                string passWord = key.GetValue("PassWord")?.ToString();
                bool isLoggedIn = Convert.ToBoolean(key.GetValue("IsLoggedIn"));
                key.Close();
                return (userName, passWord, isLoggedIn);
            }
            return (null, null, false);
        }
        private void Loading_Load(object sender, EventArgs e)
        {
            var (userName, password, isLoggedIn) = LoadLoginStateFromRegistry();
            _isLogin = isLoggedIn;

            timer1.Start();
            autoCloseTimer.Start();
            start.Start();
        }
    }
}
