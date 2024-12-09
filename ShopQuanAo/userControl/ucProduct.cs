using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ScrollBar;

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
                SoSaoTB = 0,
                SoLuongDaBan = 0,
                DanhMucID = danhMucID,
                KichHoat = kichHoat
            };

            // Thêm sản phẩm vào cơ sở dữ liệu thông qua BLL
            SanPhamBLL sanPhamBLL = new SanPhamBLL();
            bool result = sanPhamBLL.AddSanPham(sanPham);
            int sanPhamIDMax = sanPhamBLL.GetMaxSanPhamID();
            label1.Text = sanPhamIDMax.ToString();
            if (result)
            {
                MessageBox.Show("Thêm sản phẩm thành công!");
                loadSanPham();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi thêm sản phẩm!");
            }

            if (cbbBrand.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp trước khi thêm!");
                return;
            }


            decimal gia;
            int soLuongTonKho;

            // Kiểm tra dữ liệu nhập vào
            if (string.IsNullOrEmpty(txtPrice.Text) || !decimal.TryParse(txtPrice.Text, out gia) || gia < 10000)
            {
                MessageBox.Show("Giá sản phẩm không hợp lệ hoặc thấp hơn 10,000!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(txtQuantity.Text) || !int.TryParse(txtQuantity.Text, out soLuongTonKho) || soLuongTonKho < 1)
            {
                MessageBox.Show("Số lượng tồn kho không hợp lệ hoặc ít hơn 1!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra ComboBox
            if ((int)cbbMau.SelectedValue == 0)
            {
                MessageBox.Show("Vui lòng chọn màu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if ((int)cbbSize.SelectedValue == 0)
            {
                MessageBox.Show("Vui lòng chọn kích thước!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Lấy giá trị từ các ComboBox
            int mauID = (int)cbbMau.SelectedValue;
            int sizeID = (int)cbbSize.SelectedValue;

            string hinhAnhUrl = pbImg.ImageLocation;

            // Kiểm tra nếu chưa tải ảnh
            if (string.IsNullOrEmpty(hinhAnhUrl))
            {
                MessageBox.Show("Vui lòng tải ảnh sản phẩm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tạo tên file cho hình ảnh
            string newFileName = Path.GetFileName(hinhAnhUrl);
            string destinationFolder = Path.Combine(Application.StartupPath, @"..\..\..\WebsiteBanQuanAo\img");
            string destinationFilePath = Path.Combine(destinationFolder, newFileName);

            // Kiểm tra nếu file đã tồn tại
            if (File.Exists(destinationFilePath))
            {
                MessageBox.Show("Tên hình ảnh đã tồn tại trong cơ sở dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra thư mục img có tồn tại không, nếu không thì tạo mới
            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }
            bool kichHoatDetail = rdbDetailOn.Checked;
            try
            {
                // Tạo đối tượng ChiTietSanPham
                ChiTietSanPham chiTietSanPham = new ChiTietSanPham
                {
                    //Kich hoat ??
                    SanPhamID = sanPhamIDMax,
                    MauID = mauID,
                    GiaDuocGiam = 0,
                    SizeID = sizeID,
                    Gia = gia,
                    KichHoat = kichHoatDetail,
                    SoLuongTonKho = soLuongTonKho,
                    HinhAnhUrl = newFileName
                };

                // Thêm vào cơ sở dữ liệu
                bool isSuccess = chiTietSanPhamBLL.AddChiTietSanPham(chiTietSanPham);

                if (isSuccess)
                {
                    // Sao chép file hình ảnh vào thư mục img
                    File.Copy(hinhAnhUrl, destinationFilePath);

                    MessageBox.Show("Thêm chi tiết sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Lỗi khi thêm chi tiết sản phẩm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra khi thêm chi tiết sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




            int nhaCungCapID = (int)cbbBrand.SelectedValue;

            bool result2 = nhaCungCapBLL.AddSupplierForProductBLL(sanPhamIDMax, nhaCungCapID);

            if (result2)
            {
                MessageBox.Show("Thêm nhà cung cấp thành công!");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSP.SelectedRows.Count > 0)
            {
                int sanPhamID = Convert.ToInt32(dgvSP.SelectedRows[0].Cells["SanPhamID"].Value);
                sanPhamBLL.DeleteSanPham(sanPhamID);
                
                MessageBox.Show("Sản phẩm đã được xóa thành công.");
                loadSanPham();
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
                    SoSaoTB = 0,
                    SoLuongDaBan = 0,
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
