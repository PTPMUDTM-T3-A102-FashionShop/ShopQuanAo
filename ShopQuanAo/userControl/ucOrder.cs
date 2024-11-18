using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;
using DB;

namespace userControl
{
    public partial class ucOrder : UserControl
    {
        private NguoiDung nguoiDung;
        private SanPhamBLL sanPhamBLL = new SanPhamBLL();
        public ucOrder()
        {
            InitializeComponent();
            InitializeHoaDonGrid(); // Khởi tạo cột cho dgvHoaDon
        }

        private void InitializeHoaDonGrid()
        {
            // Xóa tất cả cột nếu có trước
            dgvHoaDon.Columns.Clear();

            // Thêm cột vào dgvHoaDon
            dgvHoaDon.Columns.Add("STT", "STT");
            dgvHoaDon.Columns.Add("MaSanPham", "Mã sản phẩm");
            dgvHoaDon.Columns.Add("TenSanPham", "Tên Sản Phẩm");
            dgvHoaDon.Columns.Add("Gia", "Giá");
            dgvHoaDon.Columns.Add("SoLuong", "Số Lượng");
            dgvHoaDon.Columns.Add("TongThanhTien", "Tổng Thành Tiền");
            dgvHoaDon.Columns.Add("TenMau", "Tên Màu");
            dgvHoaDon.Columns.Add("TenSize", "Tên Size");

            // Định dạng cột (tuỳ chọn)
            dgvHoaDon.Columns["Gia"].DefaultCellStyle.Format = "N0"; // Định dạng số
            dgvHoaDon.Columns["TongThanhTien"].DefaultCellStyle.Format = "N0"; // Định dạng số
        }

        private void ucOrder_Load(object sender, EventArgs e)
        {
            LoadSanPhamChiTiet();
            dgvSanPham.CellClick += DgvSanPham_CellClick;
        }

        private void DgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dgvSanPham.Rows[e.RowIndex];
                txtMaSanPham.Text = selectedRow.Cells["SanPhamID"].Value.ToString();
                txtTenSanPham.Text = selectedRow.Cells["MoTa"].Value.ToString();
                txtGia.Text = selectedRow.Cells["Gia"].Value.ToString();
                txtTenMau.Text = selectedRow.Cells["TenMau"].Value.ToString();
                txtTenSize.Text = selectedRow.Cells["TenSize"].Value.ToString();
                bool kichHoat = (bool)selectedRow.Cells["KichHoat"].Value;
                lblTrangThai.Text = kichHoat ? "Đã kích hoạt" : "Chưa kích hoạt";
            }
        }

        private void LoadSanPhamChiTiet()
        {
            List<SanPhamChiTietDTO> list = sanPhamBLL.GetSanPhamChiTiet();
            dgvSanPham.DataSource = list;
            dgvSanPham.ReadOnly = true;
        }

        private void dgvSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void SetUserInfo(NguoiDung user)
        {
            nguoiDung = user;
            lblTenNguoiDung.Text = $"⭐ Xin chào, {nguoiDung.HoTen}";
        }

        private void lblTenNguoiDung_Click(object sender, EventArgs e)
        {

        }

        private void txtTenSanPham_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtGia_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu các ô textbox cần thiết còn trống
            if (string.IsNullOrWhiteSpace(txtTenSanPham.Text) ||
                string.IsNullOrWhiteSpace(txtGia.Text) ||
                string.IsNullOrWhiteSpace(txtTenMau.Text) ||
                string.IsNullOrWhiteSpace(txtTenSize.Text))
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm từ bảng Sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra số lượng nhập
            if (string.IsNullOrWhiteSpace(txtSoLuong.Text) || !int.TryParse(txtSoLuong.Text, out int soLuong) || soLuong < 1)
            {
                MessageBox.Show("Vui lòng nhập số lượng hợp lệ (lớn hơn hoặc bằng 1)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra trạng thái sản phẩm
            if (lblTrangThai.Text == "Chưa kích hoạt")
            {
                MessageBox.Show("Sản phẩm này tạm thời không còn bán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra tồn kho
            DataGridViewRow matchedRow = null;
            foreach (DataGridViewRow row in dgvSanPham.Rows)
            {
                if (row.Cells["MoTa"].Value?.ToString() == txtTenSanPham.Text &&
                    row.Cells["Gia"].Value?.ToString() == txtGia.Text &&
                    row.Cells["TenMau"].Value?.ToString() == txtTenMau.Text &&
                    row.Cells["TenSize"].Value?.ToString() == txtTenSize.Text)
                {
                    matchedRow = row;
                    break;
                }
            }

            if (matchedRow == null)
            {
                MessageBox.Show("Không tìm thấy sản phẩm trong bảng Sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int soLuongTonKho = Convert.ToInt32(matchedRow.Cells["SoLuongTonKho"].Value);
            if (soLuong > soLuongTonKho)
            {
                MessageBox.Show($"Số lượng nhập vượt quá tồn kho! Hiện có {soLuongTonKho} sản phẩm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra sản phẩm đã thêm hay chưa
            foreach (DataGridViewRow row in dgvHoaDon.Rows)
            {
                if (row.Cells["TenSanPham"].Value?.ToString() == txtTenSanPham.Text &&
                    row.Cells["TenMau"].Value?.ToString() == txtTenMau.Text &&
                    row.Cells["TenSize"].Value?.ToString() == txtTenSize.Text)
                {
                    MessageBox.Show("Sản phẩm đã được thêm vào hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Thêm sản phẩm vào dgvHoaDon
            int stt = dgvHoaDon.Rows.Count;
            decimal masanPham = Convert.ToInt32(txtMaSanPham.Text);
            decimal gia = Convert.ToDecimal(txtGia.Text);
            decimal tongThanhTien = gia * soLuong;

            dgvHoaDon.Rows.Add(stt, masanPham, txtTenSanPham.Text, gia, soLuong, tongThanhTien, txtTenMau.Text, txtTenSize.Text);

            // Cập nhật tổng thành tiền
            decimal tongTien = 0;
            foreach (DataGridViewRow row in dgvHoaDon.Rows)
            {
                tongTien += Convert.ToDecimal(row.Cells["TongThanhTien"].Value);
            }

            lblTongThanhTien.Text = $"Tổng thành tiền: {tongTien:##,###} đ";
            MessageBox.Show("Thêm sản phẩm vào hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            txtTenSanPham.Clear();
            txtGia.Clear();
            txtSoLuong.Clear();
            txtTenSize.Clear();
            txtTenMau.Clear();
            txtMaSanPham.Clear();
        }

        private void btnThemDonHang_Click(object sender, EventArgs e)
        {

        }

        private void dgvHoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblTongThanhTien_Click(object sender, EventArgs e)
        {

        }

        private void txtTenMau_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTenSize_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Chắc chắn làm mới bảng hóa đơn bán hàng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                dgvHoaDon.Rows.Clear();
                lblTongThanhTien.Text = "Tổng thành tiền: 0";
            }
        }

        private void txtMaSanPham_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
