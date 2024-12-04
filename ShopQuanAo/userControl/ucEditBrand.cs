using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace userControl
{
    public partial class ucEditBrand : UserControl
    {
        NhaCungCapBLL nhaCungCapBLL = new NhaCungCapBLL();
        private int sanPhamID;
        public event Action BackToProductRequested;
        public ucEditBrand(int sanPhamID)
        {
            InitializeComponent();
            this.sanPhamID = sanPhamID;
        }

        void loadBrand()
        {
            List<NhaCungCap> brands = nhaCungCapBLL.getAllNhaCungCapBLL();

            NhaCungCap defaultBrand = new NhaCungCap
            {
                NhaCungCapID = 0,
                TenNhaCungCap = "<Vui lòng chọn nhà cung cấp>"
            };

            brands.Insert(0, defaultBrand);

            // Gán danh sách vào ComboBox
            cbbBrand.DataSource = brands;
            cbbBrand.DisplayMember = "TenNhaCungCap";
            cbbBrand.ValueMember = "NhaCungCapID";

            // Đặt mục mặc định
            cbbBrand.SelectedIndex = 0;
        }

        private void LoadBrandsForProduct()
        {
            List<NhaCungCap> suppliers = nhaCungCapBLL.GetSuppliersByProductIDBLL(sanPhamID);
            dgvBrand.DataSource = suppliers;
            dgvBrand.Columns["NhaCungCapID"].Visible = false;
            dgvBrand.Columns["TenNhaCungCap"].HeaderText = "Nhà cung cấp";
        }

        private void ucEditBrand_Load(object sender, EventArgs e)
        {
            loadBrand();
            LoadBrandsForProduct();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cbbBrand.SelectedIndex == 0) 
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp trước khi thêm!");
                return;
            }

            int nhaCungCapID = (int)cbbBrand.SelectedValue;

            bool result = nhaCungCapBLL.AddSupplierForProductBLL(sanPhamID, nhaCungCapID);

            if (result)
            {
                MessageBox.Show("Thêm nhà cung cấp thành công!");
                LoadBrandsForProduct();
            }
            else
            {
                MessageBox.Show("Nhà cung cấp đã tồn tại cho sản phẩm này!");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvBrand.SelectedRows.Count > 0)
            {
                int nhaCungCapID = Convert.ToInt32(dgvBrand.SelectedRows[0].Cells["NhaCungCapID"].Value);

                bool result = nhaCungCapBLL.RemoveSupplierFromProductBLL(sanPhamID, nhaCungCapID);

                if (result)
                {
                    MessageBox.Show("Xóa nhà cung cấp thành công!");
                    LoadBrandsForProduct();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi xóa nhà cung cấp!");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhà cung cấp để xóa!");
            }
        }

        private void dgvBrand_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int nhaCungCapID = Convert.ToInt32(dgvBrand.Rows[e.RowIndex].Cells["NhaCungCapID"].Value);
                cbbBrand.SelectedValue = nhaCungCapID;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            BackToProductRequested?.Invoke();
        }
    }
}
