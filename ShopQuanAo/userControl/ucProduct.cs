using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;

namespace userControl
{
    public partial class ucProduct : UserControl
    {
        DanhMucBLL danhMucBLL = new DanhMucBLL();
        NhaCungCapBLL nhaCungCapBLL = new NhaCungCapBLL();
        SanPhamBLL sanPhamBLL = new SanPhamBLL();
        MauBLL mauBLL = new MauBLL();
        SizeBLL sizeBLL = new SizeBLL();
        ChiTietSanPhamBLL chiTietSanPhamBLL = new ChiTietSanPhamBLL();
        public event Action<int> EditBrandRequested;
        public event Action<int> EditDetailRequested;
        public ucProduct()
        {
            InitializeComponent();
        }
        //loadForm
        #region
        void loadDanhMuc()
        {
            List<DanhMuc> danhmucs = danhMucBLL.getAllDanhMucBLL();
            cbbDanhMuc.DataSource = danhmucs;
            cbbDanhMuc.DisplayMember = "TenDanhMuc";
            cbbDanhMuc.ValueMember = "DanhMucID";
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
        void loadSanPham()
        {
            List<SanPham> sanphams = sanPhamBLL.GetAllSanPham();
            dgvSP.DataSource = sanphams;
            dgvSP.Columns["SanPhamID"].Visible = false;
            dgvSP.Columns["DanhMuc"].Visible = false;
            dgvSP.Columns["DanhMucID"].Visible = false;
            dgvSP.Columns["TenSanPham"].HeaderText = "Tên sản phẩm";
            dgvSP.Columns["MoTa"].HeaderText = "Mô tả";
            dgvSP.Columns["SoLuongDaBan"].HeaderText = "Số lượng đã bán";
        }
        void loadMau()
        {
            List<Mau> maus = mauBLL.getAllMau();

            Mau defaultMau = new Mau
            {
                MauID = 0,
                TenMau = "<Vui lòng chọn màu>"
            };

            maus.Insert(0, defaultMau);
            cbbMau.DataSource = maus;
            cbbMau.DisplayMember = "TenMau";
            cbbMau.ValueMember = "MauID";
            cbbMau.SelectedIndex = 0;
        }

        void loadSize()
        {
            List<DTO.Size> sizes = sizeBLL.getAllSize();

            DTO.Size defaultSize = new DTO.Size
            {
                SizeID = 0,
                TenSize = "<Vui lòng chọn kích thước>"
            };

            sizes.Insert(0, defaultSize);
            cbbSize.DataSource = sizes;
            cbbSize.DisplayMember = "TenSize";
            cbbSize.ValueMember = "SizeID";
            cbbSize.SelectedIndex = 0;
        }
        private void ucProduct_Load(object sender, EventArgs e)
        {
            loadDanhMuc();
            loadSanPham();
            loadBrand();
            loadMau();
            loadSize();
        }
        #endregion
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string tenSanPham = txtName.Text;
            string moTa = txtDes.Text;
            int danhMucID = (int)cbbDanhMuc.SelectedValue;
            bool kichHoat = rdbOn.Checked;

            // Tạo đối tượng SanPham
            SanPham sanPham = new SanPham
            {
                TenSanPham = tenSanPham,
                MoTa = moTa,
                DanhMucID = danhMucID,
                KichHoat = kichHoat
            };

            // Thêm sản phẩm vào cơ sở dữ liệu thông qua BLL
            SanPhamBLL sanPhamBLL = new SanPhamBLL();
            bool result = sanPhamBLL.AddSanPham(sanPham);

            if (result)
            {
                MessageBox.Show("Thêm sản phẩm thành công!");
                loadSanPham();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi thêm sản phẩm!");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSP.SelectedRows.Count > 0)
            {
                int sanPhamID = Convert.ToInt32(dgvSP.SelectedRows[0].Cells["SanPhamID"].Value);
                sanPhamBLL.DeleteSanPham(sanPhamID);
                
                MessageBox.Show("Sản phẩm đã được xóa thành công.");
                loadDanhMuc();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để xóa.");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvSP.SelectedRows.Count > 0)
            {
                int sanPhamID = int.Parse(dgvSP.SelectedRows[0].Cells["SanPhamID"].Value.ToString());

                // Lấy thông tin từ các điều khiển
                string tenSanPham = txtName.Text;
                string moTa = txtDes.Text;
                int danhMucID = (int)cbbDanhMuc.SelectedValue;
                bool kichHoat = rdbOn.Checked;

                // Tạo đối tượng SanPham
                SanPham sanPham = new SanPham
                {
                    SanPhamID = sanPhamID,
                    TenSanPham = tenSanPham,
                    MoTa = moTa,
                    DanhMucID = danhMucID,
                    KichHoat = kichHoat
                };

                // Cập nhật sản phẩm vào cơ sở dữ liệu thông qua BLL
                SanPhamBLL sanPhamBLL = new SanPhamBLL();
                bool result = sanPhamBLL.UpdateSanPham(sanPham);

                if (result)
                {
                    MessageBox.Show("Cập nhật sản phẩm thành công!");
                    loadSanPham();  // Cập nhật lại DataGridView sau khi cập nhật
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi cập nhật sản phẩm!");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần chỉnh sửa!");
            }
        }

        private void dgvSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSP.Rows[e.RowIndex];

                txtName.Text = row.Cells["TenSanPham"].Value?.ToString();
                txtDes.Text = row.Cells["MoTa"].Value?.ToString();
                cbbDanhMuc.SelectedValue = row.Cells["DanhMucID"].Value;

                bool kichHoat = bool.Parse(row.Cells["KichHoat"].Value?.ToString());
                rdbOn.Checked = kichHoat;
                rdbOff.Checked = !kichHoat;
            }
        }

        private void btnEditBrand_Click(object sender, EventArgs e)
        {
            if (dgvSP.SelectedRows.Count > 0)
            {
                int sanPhamID = Convert.ToInt32(dgvSP.SelectedRows[0].Cells["SanPhamID"].Value);
                EditBrandRequested?.Invoke(sanPhamID);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm để chỉnh sửa thương hiệu!");
            }
        }

        private void btnEditDetail_Click(object sender, EventArgs e)
        {
            if (dgvSP.SelectedRows.Count > 0)
            {
                int sanPhamID = Convert.ToInt32(dgvSP.SelectedRows[0].Cells["SanPhamID"].Value);
                EditDetailRequested?.Invoke(sanPhamID); // Gửi mã sản phẩm
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm để xem chi tiết!");
            }

        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files (*.jpg;*.png)|*.jpg;*.png";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;

                if (filePath.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                    filePath.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                {
                    pbImg.ImageLocation = filePath;
                }
                else
                {
                    MessageBox.Show("Chỉ cho phép chọn các file .jpg hoặc .png.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
