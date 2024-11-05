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
    public partial class Home_User : Form
    {
        public Home_User()
        {
            InitializeComponent();
        }

        private void pannel_Lich_Day_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void lb_Xem_Ho_So_Chi_Tiet_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void lb_Xem_Danh_Sach_Thong_Bao_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void pannel_Lich_Day_Click(object sender, EventArgs e)
        {
            FormTask.OpenFormInPanel<Teaching_Schedule>(FormTask.Pannel_change);
        }
    }
}
