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
        private int maxWidth = 995; // Kích thước tối đa của thanh loading
        private Timer autoCloseTimer;
        public Loading()
        {
            InitializeComponent();
            InitializeAutoCloseTimer();
        }
        private void InitializeAutoCloseTimer()
        {
            autoCloseTimer = new Timer();
            autoCloseTimer.Interval = 9500; // 5000 milliseconds = 5 seconds
            autoCloseTimer.Tick += AutoCloseTimer_Tick;
        }
        private void AutoCloseTimer_Tick(object sender, EventArgs e)
        {
            autoCloseTimer.Stop(); // Dừng timer sau khi form tự tắt
            this.Close(); // Đóng form loading sau 5 giây
        }

        // Tạo hàm để bắt đầu hoạt ảnh
        private void timer1_Tick(object sender, EventArgs e)
        {
            panelSlide.Width += 10; // Mỗi lần tick, thanh tiến sẽ rộng thêm 10px
            if (panelSlide.Width > 995)
            {
                panelSlide.Width = 0;
            }
            // Kiểm tra nếu thanh đã đạt tới chiều rộng tối đa
            if (panelSlide.Width >= maxWidth)
            {
                // Dừng timer khi đạt đến chiều rộng tối đa
                timer1.Stop();
            }
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            timer1.Start();
            // Khởi động timer tự động đóng form sau 5 giây
            autoCloseTimer.Start();
        }
    }
}
