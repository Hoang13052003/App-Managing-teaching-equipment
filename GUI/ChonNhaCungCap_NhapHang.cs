using BUS;
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
    public partial class ChonNhaCungCap_NhapHang : Form
    {
        SupplierBUS sup = new SupplierBUS();
        YeuCauThietBiBUS y = new YeuCauThietBiBUS();
        public ChonNhaCungCap_NhapHang()
        {
            InitializeComponent();
            this.Load += ChonNhaCungCap_NhapHang_Load;
            this.cboNCC.SelectedIndexChanged += CboNCC_SelectedIndexChanged;
        }

        private void CboNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboNCC.SelectedValue != null && int.TryParse(cboNCC.SelectedValue.ToString(), out int maNCC))
            {
                dgvDSThietBi.DataSource = sup.SearchThietBi_NCC(maNCC);
                dgvDSThietBi.Columns["MaTB"].HeaderText = "Mã thiết bị";
                dgvDSThietBi.Columns["TenTB"].HeaderText = "Tên thiết bị";
                dgvDSThietBi.Columns["MaLoai"].Visible = false;
                dgvDSThietBi.Columns["NSX"].Visible = false;
                dgvDSThietBi.Columns["SoLuong"].Visible = false;
            }
        }

        private void ChonNhaCungCap_NhapHang_Load(object sender, EventArgs e)
        {
            LoadNCC();
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            int maNCC = Convert.ToInt32(cboNCC.SelectedValue.ToString());
            NhapThemThietBi frm = new NhapThemThietBi(maNCC);
            frm.ShowDialog();
        }
        void LoadNCC() 
        {
            cboNCC.DataSource = sup.getAll();
            cboNCC.DisplayMember = "TenNCC";
            cboNCC.ValueMember = "MaNCC";
        }
    }
}
