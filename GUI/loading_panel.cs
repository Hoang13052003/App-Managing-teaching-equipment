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
    public partial class loading_panel : Form
    {
        private Timer closed;
        public loading_panel()
        {
            InitializeComponent();

            closed = new Timer();
            closed.Interval = 5500;
            closed.Tick += closed_Tick;
        }
        private void closed_Tick(object sender, EventArgs e)
        {
            closed.Stop();
            this.Close();
        }

        private void loading_panel_Load(object sender, EventArgs e)
        {
            //closed.Start();
        }
    }
}
