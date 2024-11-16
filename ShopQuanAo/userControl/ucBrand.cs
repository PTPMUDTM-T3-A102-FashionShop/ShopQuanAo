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
    public partial class ucBrand : UserControl
    {
        NhaCungCapBLL nhaCungCapBLL = new NhaCungCapBLL();
        private int selectedBrandId = -1;
        public ucBrand()
        {
            InitializeComponent();
        }

        private void ucBrand_Load(object sender, EventArgs e)
        {
            List<NhaCungCap> brandList = nhaCungCapBLL.getAllNhaCungCapBLL();
            dgvBrand.DataSource = brandList;

            dgvBrand.Columns["NhaCungCapID"].Visible = false;
            dgvBrand.Columns["TenNhaCungCap"].HeaderText = "Tên nhà cung cấp";
            dgvBrand.Columns["DiaChi"].HeaderText = "Địa chỉ";
            dgvBrand.Columns["SoDienThoai"].HeaderText = "Số điện thoại";
            dgvBrand.Columns["Email"].HeaderText = "Email";
            dgvBrand.Columns["MoTa"].Visible = false;
        }

        private void LoadBrand()
        {
            List<NhaCungCap> brandList = nhaCungCapBLL.getAllNhaCungCapBLL();
            dgvBrand.DataSource = brandList;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtEmail.Text) ||
                string.IsNullOrEmpty(txtSDT.Text) || string.IsNullOrEmpty(txtAddress.Text) ||
                string.IsNullOrEmpty(txtDes.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            string tenNhaCungCap = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string soDienThoai = txtSDT.Text.Trim();
            string diaChi = txtAddress.Text.Trim();
            string moTa = txtDes.Text.Trim();

            NhaCungCap newBrand = new NhaCungCap
            {
                TenNhaCungCap = tenNhaCungCap,
                Email = email,
                SoDienThoai = soDienThoai,
                DiaChi = diaChi,
                MoTa = moTa
            };

            bool isAdded = nhaCungCapBLL.AddNhaCungCap(newBrand);

            if (isAdded)
            {
                MessageBox.Show("Nhà cung cấp đã được thêm thành công.");
                LoadBrand();
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi thêm nhà cung cấp.");
            }
            clearTextboxes();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedBrandId == -1)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp cần xóa.");
                return;
            }

            bool hasProducts = nhaCungCapBLL.HasProductsInSanPham(selectedBrandId);
            if (hasProducts)
            {
                MessageBox.Show("Không thể xóa nhà cung cấp này vì còn sản phẩm thuộc nhà cung cấp này.");
                return;
            }

            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhà cung cấp này?", "Xác nhận", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                bool isDeleted = nhaCungCapBLL.DeleteNhaCungCap(selectedBrandId);

                if (isDeleted)
                {
                    MessageBox.Show("Nhà cung cấp đã được xóa.");
                    LoadBrand();
                }
                else
                {
                    MessageBox.Show("Đã xảy ra lỗi khi xóa nhà cung cấp.");
                }
            }
            clearTextboxes();

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedBrandId == -1)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp cần chỉnh sửa.");
                return;
            }

            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtEmail.Text) ||
                string.IsNullOrEmpty(txtSDT.Text) || string.IsNullOrEmpty(txtAddress.Text) ||
                string.IsNullOrEmpty(txtDes.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            string newTenNhaCungCap = txtName.Text.Trim();
            string newEmail = txtEmail.Text.Trim();
            string newSoDienThoai = txtSDT.Text.Trim();
            string newDiaChi = txtAddress.Text.Trim();
            string newMoTa = txtDes.Text.Trim();

            bool isUpdated = nhaCungCapBLL.UpdateNhaCungCap(selectedBrandId, newTenNhaCungCap, newEmail, newSoDienThoai, newDiaChi, newMoTa);

            if (isUpdated)
            {
                MessageBox.Show("Nhà cung cấp đã được cập nhật.");
                LoadBrand();
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi cập nhật nhà cung cấp.");
            }
            clearTextboxes();
        }

        private void dgvBrand_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dgvBrand.Rows[e.RowIndex];

                // Hiển thị thông tin nhà cung cấp vào các TextBox
                txtName.Text = selectedRow.Cells["TenNhaCungCap"].Value.ToString();
                txtEmail.Text = selectedRow.Cells["Email"].Value.ToString();
                txtSDT.Text = selectedRow.Cells["SoDienThoai"].Value.ToString();
                txtAddress.Text = selectedRow.Cells["DiaChi"].Value.ToString();
                txtDes.Text = selectedRow.Cells["MoTa"].Value.ToString();

                selectedBrandId = int.Parse(selectedRow.Cells["NhaCungCapID"].Value.ToString());

                NhaCungCapBLL nhaCungCapBLL = new NhaCungCapBLL();
                List<SanPham> productList = nhaCungCapBLL.GetSanPhamByNhaCungCapIDBLL(selectedBrandId);

                dgvSP.DataSource = productList;

                //dgvSP.Columns["SanPhamID"].Visible = false;
                //dgvSP.Columns["NhaCungCap"].Visible = false;
                //dgvSP.Columns["DanhMuc"].Visible = false;
                //dgvSP.Columns["DanhMucID"].Visible = false;
                //dgvSP.Columns["NhaCungCapID"].Visible = false;

                dgvSP.Columns["TenSanPham"].HeaderText = "Tên sản phẩm";
                dgvSP.Columns["MoTa"].HeaderText = "Mô tả";
                dgvSP.Columns["SoLuongDaBan"].HeaderText = "Số lượng đã bán";
            }
        }

        void clearTextboxes()
        {
            txtAddress.Text = string.Empty;
            txtDes.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtName.Text = string.Empty;
            txtSDT.Text = string.Empty;
        }
    }
}
