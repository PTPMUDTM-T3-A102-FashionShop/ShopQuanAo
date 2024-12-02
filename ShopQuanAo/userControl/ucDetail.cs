using BLL;
using DTO;
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

namespace userControl
{
    public partial class ucDetail : UserControl
    {
        private int sanPhamID;
        MauBLL mauBLL = new MauBLL();
        SizeBLL sizeBLL = new SizeBLL();
        ChiTietSanPhamBLL chiTietSanPhamBLL = new ChiTietSanPhamBLL();
        public event Action BackToProductRequested;
        public ucDetail(int sanPhamID)
        {
            InitializeComponent();
            this.sanPhamID = sanPhamID;
        }
        #region
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

        void loadChiTiet()
        {
            List<ChiTietSanPham> details = chiTietSanPhamBLL.getChiTietBySanPhamID(this.sanPhamID);
            dgvDetail.DataSource = details;
            dgvDetail.Columns["ChiTietID"].Visible = false;
            dgvDetail.Columns["SanPhamID"].Visible = false;
            dgvDetail.Columns["SanPham"].Visible = false;
            dgvDetail.Columns["MauID"].Visible = false;
            dgvDetail.Columns["SizeID"].Visible = false;
            dgvDetail.Columns["Mau"].Visible = false;
            dgvDetail.Columns["Size"].Visible = false;

            dgvDetail.Columns["Gia"].HeaderText = "Giá";
            dgvDetail.Columns["SoLuongTonKho"].HeaderText = "Số lượng tồn";
            dgvDetail.Columns["HinhAnhUrl"].HeaderText = "Hình ảnh";
        }

        private void ucDetail_Load(object sender, EventArgs e)
        {
            loadMau();
            loadSize();
            loadChiTiet();
        }
        private void ClearFields()
        {
            txtPrice.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            pbImg.Image = null;
        }
        #endregion

        private void btnAdd_Click(object sender, EventArgs e)
        {
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

            try
            {
                // Tạo đối tượng ChiTietSanPham
                ChiTietSanPham chiTietSanPham = new ChiTietSanPham
                {
                    SanPhamID = sanPhamID,
                    MauID = mauID,
                    SizeID = sizeID,
                    Gia = gia,
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
                    loadChiTiet(); // Tải lại danh sách chi tiết sản phẩm
                    ClearFields(); // Làm trống các trường
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
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDetail.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một chi tiết sản phẩm để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy thông tin chi tiết sản phẩm từ DataGridView
            int ChiTietID = (int)dgvDetail.CurrentRow.Cells["ChiTietID"].Value; // ID của chi tiết sản phẩm
            string hinhAnhUrl = dgvDetail.CurrentRow.Cells["HinhAnhUrl"].Value.ToString(); // Tên file ảnh của chi tiết

            // Hỏi xác nhận người dùng trước khi xóa
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa chi tiết sản phẩm này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                return;
            }

            // Xóa chi tiết sản phẩm khỏi cơ sở dữ liệu
            try
            {
                bool isDeleted = chiTietSanPhamBLL.DeleteChiTietSanPham(ChiTietID); // Gọi phương thức xóa trong BLL

                if (isDeleted)
                {
                    // Xóa ảnh khỏi thư mục img
                    string destinationFolder = Path.Combine(Application.StartupPath, @"..\..\..\WebsiteBanQuanAo\img");
                    string imagePath = Path.Combine(destinationFolder, hinhAnhUrl);

                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath); // Xóa file ảnh
                    }
                    ClearFields();
                    MessageBox.Show("Xóa chi tiết sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadChiTiet(); // Tải lại danh sách chi tiết sản phẩm
                }
                else
                {
                    MessageBox.Show("Lỗi khi xóa chi tiết sản phẩm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra khi xóa chi tiết sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Lấy giá trị từ các trường nhập liệu
            decimal gia;
            int soLuongTonKho;
            int ChiTietID = (int)dgvDetail.CurrentRow.Cells["ChiTietID"].Value; // Lấy ID chi tiết sản phẩm đã chọn
            string currentImageUrl = dgvDetail.CurrentRow.Cells["HinhAnhUrl"].Value.ToString(); // Lấy tên ảnh cũ từ DataGridView

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

            // Kiểm tra nếu có ảnh mới
            if (!string.IsNullOrEmpty(hinhAnhUrl) && Path.GetFileName(hinhAnhUrl) != currentImageUrl)
            {
                string newFileName = Path.GetFileName(hinhAnhUrl);
                string destinationFolder = Path.Combine(Application.StartupPath, @"..\..\..\WebsiteBanQuanAo\img");
                string destinationFilePath = Path.Combine(destinationFolder, newFileName);

                if (File.Exists(destinationFilePath))
                {
                    MessageBox.Show("Tên hình ảnh đã tồn tại trong cơ sở dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!Directory.Exists(destinationFolder))
                {
                    Directory.CreateDirectory(destinationFolder);
                }

                // Xóa ảnh cũ
                string oldImagePath = Path.Combine(destinationFolder, currentImageUrl);
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }

                // Sao chép ảnh mới
                File.Copy(hinhAnhUrl, destinationFilePath);
                currentImageUrl = newFileName; // Cập nhật tên ảnh
            }

            // Cập nhật thông tin chi tiết sản phẩm
            ChiTietSanPham chiTietSanPham = new ChiTietSanPham
            {
                ChiTietID = ChiTietID,
                SanPhamID = sanPhamID,
                MauID = mauID,
                SizeID = sizeID,
                Gia = gia,
                SoLuongTonKho = soLuongTonKho,
                HinhAnhUrl = currentImageUrl
            };

            // Gọi phương thức cập nhật trong BLL
            bool isSuccess = chiTietSanPhamBLL.UpdateChiTietSanPham(chiTietSanPham);

            if (isSuccess)
            {
                MessageBox.Show("Cập nhật chi tiết sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadChiTiet(); // Tải lại danh sách
                ClearFields(); // Làm trống các trường
            }
            else
            {
                MessageBox.Show("Lỗi khi cập nhật chi tiết sản phẩm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void dgvDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dgvDetail.Rows[e.RowIndex];

                txtPrice.Text = selectedRow.Cells["Gia"].Value.ToString();
                txtQuantity.Text = selectedRow.Cells["SoLuongTonKho"].Value.ToString();
                cbbMau.SelectedValue = selectedRow.Cells["MauID"].Value;
                cbbSize.SelectedValue = selectedRow.Cells["SizeID"].Value;

                string hinhAnhUrl = selectedRow.Cells["HinhAnhUrl"].Value.ToString();

                // Tạo đường dẫn tương đối tới thư mục img trong WebsiteBanQuanAo
                // Quay lại 3 cấp và đến thư mục img trong WebsiteBanQuanAo
                string imagePath = Path.Combine(Application.StartupPath, @"..\..\..\WebsiteBanQuanAo\img", hinhAnhUrl);

                // Kiểm tra nếu hình ảnh tồn tại
                if (File.Exists(imagePath))
                {
                    pbImg.ImageLocation = imagePath; // Hiển thị hình ảnh lên PictureBox
                }
                else
                {
                    pbImg.Image = null; // Nếu không có hình ảnh, bỏ trống PictureBox
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            BackToProductRequested?.Invoke();
        }
    }
}
