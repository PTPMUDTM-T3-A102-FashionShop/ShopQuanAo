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
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;

namespace userControl
{
    public partial class ucOrder : UserControl
    {
        private NguoiDung nguoiDung;
        private SanPhamBLL sanPhamBLL = new SanPhamBLL();
        private DBConnection dbConnection = new DBConnection();
        private Panel contentPanel;

        private Application excel;
        private Workbook workbook;
        private Worksheet worksheet;

        public ucOrder()
        {
            InitializeComponent();
            InitializeHoaDonGrid(); // Khởi tạo cột cho dgvHoaDon
        }

        public void SetContentPanel(Panel panel)
        {
            contentPanel = panel;
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
            dgvHoaDon.ReadOnly = true;
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
            // Kiểm tra xem có ít nhất một dòng hợp lệ trong dgvHoaDon
            bool hasData = false;
            foreach (DataGridViewRow row in dgvHoaDon.Rows)
            {
                if (row.IsNewRow) continue; // Bỏ qua hàng mới (hàng trống cho nhập liệu)

                // Kiểm tra nếu có ít nhất một dòng có dữ liệu hợp lệ
                if (row.Cells["MaSanPham"].Value != null &&
                    row.Cells["SoLuong"].Value != null &&
                    row.Cells["Gia"].Value != null &&
                    Convert.ToInt32(row.Cells["SoLuong"].Value) > 0 &&
                    Convert.ToDecimal(row.Cells["Gia"].Value) > 0)
                {
                    hasData = true;
                    break; // Dừng vòng lặp nếu tìm thấy dòng hợp lệ
                }
            }

            if (!hasData)
            {
                MessageBox.Show("Không có dữ liệu hợp lệ trong bảng hóa đơn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Lấy thông tin người dùng và tổng tiền từ giao diện
            int nguoiDungID = nguoiDung.NguoiDungID;
            decimal tongTien = decimal.Parse(lblTongThanhTien.Text.Replace("Tổng thành tiền: ", "").Replace(" đ", "").Replace(",", ""));

            DonHangDTO donHang = new DonHangDTO
            {
                NguoiDungID = nguoiDungID,
                TongTien = tongTien,
                TinhTrangDonHang = "Hoàn Thành",
                HinhThucThanhToan = "Tiền mặt",
                TinhTrangThanhToan = "Đã thanh toán"
            };

            List<ChiTietDonHangDTO> chiTietDonHangs = new List<ChiTietDonHangDTO>();

            foreach (DataGridViewRow row in dgvHoaDon.Rows)
            {
                if (row.IsNewRow) continue;

                int maSanPham = Convert.ToInt32(row.Cells["MaSanPham"].Value);
                string tenMau = row.Cells["TenMau"].Value.ToString();
                string tenSize = row.Cells["TenSize"].Value.ToString();
                int soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);
                decimal gia = Convert.ToDecimal(row.Cells["Gia"].Value);

                // Lấy ChiTietID từ bảng ChiTietSanPham
                int chiTietID = GetChiTietID(maSanPham, tenMau, tenSize);

                ChiTietDonHangDTO chiTiet = new ChiTietDonHangDTO
                {
                    SanPhamID = chiTietID,
                    SoLuong = soLuong,
                    DonGia = gia
                };

                chiTietDonHangs.Add(chiTiet);
            }

            DonHangBLL donHangBLL = new DonHangBLL();
            donHangBLL.ThemDonHang(donHang, chiTietDonHangs);

            MessageBox.Show("Thêm đơn hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ExportToExcel(dgvHoaDon, lblTongThanhTien, nguoiDung.HoTen);
            LamMoi();
            LoadSanPhamChiTiet();
        }

        public void ExportToExcel(DataGridView dgvHoaDon, System.Windows.Forms.Label lblTongThanhTien, string nguoiLap)
        {
            excel = new Application { Visible = false };
            workbook = excel.Workbooks.Add(Type.Missing);
            worksheet = (Worksheet)workbook.ActiveSheet;

            try
            {
                SetupWorksheet();
                SetHeaderInfo(nguoiLap);
                CreateTableHeader();
                FillTableData(dgvHoaDon);
                AddFooter(dgvHoaDon.Rows.Count, lblTongThanhTien.Text);
                FormatWorksheet();

                string fileName = SaveExcelFile();
                MessageBox.Show($"Đã xuất hóa đơn thành công: {fileName}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CleanupExcelResources();
            }
        }

        private void SetupWorksheet()
        {
            worksheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
            worksheet.PageSetup.FitToPagesWide = 1;
            worksheet.PageSetup.FitToPagesTall = false;
        }

        private void SetHeaderInfo(string nguoiLap)
        {
            Range headerRange = worksheet.Range["A1:E1"];
            headerRange.Merge();
            headerRange.Value = "HÓA ĐƠN THANH TOÁN SHOP THE ZONE";
            headerRange.Font.Size = 16;
            headerRange.Font.Bold = true;
            headerRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;

            worksheet.Cells[2, 2] = "Địa chỉ: Lê Trọng Tấn, Tân Phú, TP.HCM";
            worksheet.Cells[3, 2] = "Số điện thoại: +09 1234 7986";
            worksheet.Cells[4, 2] = $"Ngày và giờ: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
            worksheet.Cells[5, 2] = $"Người lập: {nguoiLap}";

            Range infoRange = worksheet.Range["A2:E5"];
            infoRange.Font.Size = 12;
            infoRange.HorizontalAlignment = XlHAlign.xlHAlignLeft;
        }

        private void CreateTableHeader()
        {
            string[] headers = { "STT", "Tên Sản Phẩm", "Số Lượng", "Giá", "Tổng Thành Tiền" };
            Range headerRange = worksheet.Range["A6:E6"];
            headerRange.Value = headers;
            headerRange.Font.Bold = true;
            headerRange.Interior.Color = XlRgbColor.rgbLightGray;
            headerRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            headerRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
            headerRange.Borders.LineStyle = XlLineStyle.xlContinuous;
        }

        private void FillTableData(DataGridView dgvHoaDon)
        {
            int row = 7;
            foreach (DataGridViewRow dgvRow in dgvHoaDon.Rows)
            {
                if (dgvRow.IsNewRow) continue;
                WriteRowData(row++, dgvRow);
            }
        }

        private void WriteRowData(int row, DataGridViewRow dgvRow)
        {
            worksheet.Cells[row, 1] = dgvRow.Cells["STT"].Value?.ToString();
            worksheet.Cells[row, 2] = dgvRow.Cells["TenSanPham"].Value?.ToString();
            worksheet.Cells[row, 3] = dgvRow.Cells["SoLuong"].Value?.ToString();

            if (decimal.TryParse(dgvRow.Cells["Gia"].Value?.ToString(), out var gia))
                worksheet.Cells[row, 4] = gia;

            if (decimal.TryParse(dgvRow.Cells["TongThanhTien"].Value?.ToString(), out var tongTien))
                worksheet.Cells[row, 5] = tongTien;

            Range dataRange = worksheet.Range[$"A{row}:E{row}"];
            dataRange.Borders.LineStyle = XlLineStyle.xlContinuous;
            dataRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
        }

        private void AddFooter(int rowCount, string totalAmount)
        {
            int lastRow = rowCount + 7;

            Range totalRange = worksheet.Range[$"A{lastRow}:E{lastRow}"];
            totalRange.Merge();
            totalRange.Value = totalAmount;
            totalRange.Font.Bold = true;
            totalRange.HorizontalAlignment = XlHAlign.xlHAlignRight;

            Range thankYouRange = worksheet.Range[$"A{lastRow + 1}:E{lastRow + 1}"];
            thankYouRange.Merge();
            thankYouRange.Value = "Cảm ơn - Hẹn gặp lại Quý Khách";
            thankYouRange.Font.Italic = true;
            thankYouRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
        }

        private void FormatWorksheet()
        {
            worksheet.Columns.AutoFit();
            worksheet.Rows.AutoFit();

            Range usedRange = worksheet.UsedRange;
            usedRange.Borders.LineStyle = XlLineStyle.xlContinuous;
            usedRange.Columns[4].NumberFormat = "#,##0";
            usedRange.Columns[5].NumberFormat = "#,##0";
        }
        private string SaveExcelFile()
        {

            // Thay đổi đường dẫn nè
            string folderPath = @"C:\Users\gialo\Desktop\";
            string fileName = $"HoaDon_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
            string fullPath = System.IO.Path.Combine(folderPath, fileName);

            workbook.SaveAs(fullPath);
            workbook.Close();
            return fullPath;
        }

        private void CleanupExcelResources()
        {
            if (worksheet != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
            if (workbook != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            if (excel != null)
            {
                excel.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private int GetChiTietID(int maSanPham, string tenMau, string tenSize)
        {
            // Thực hiện truy vấn để lấy ChiTietID từ bảng ChiTietSanPham
            DBConnection dbConnection = new DBConnection();
            dbConnection.OpenConnection();

            try
            {
                string query = @"
            SELECT ct.ChiTietID
            FROM ChiTietSanPham ct
            JOIN Mau m ON ct.MauID = m.MauID
            JOIN Size s ON ct.SizeID = s.SizeID
            WHERE ct.SanPhamID = @SanPhamID
              AND m.TenMau = @TenMau
              AND s.TenSize = @TenSize";

                SqlCommand cmd = new SqlCommand(query, dbConnection.conn);
                cmd.Parameters.AddWithValue("@SanPhamID", maSanPham);
                cmd.Parameters.AddWithValue("@TenMau", tenMau);
                cmd.Parameters.AddWithValue("@TenSize", tenSize);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            finally
            {
                dbConnection.CloseConnection();
            }
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
                LamMoi();
            }
        }

        private void LamMoi() {
            dgvHoaDon.Rows.Clear();
            lblTongThanhTien.Text = "Tổng thành tiền: 0";
        }

        private void txtMaSanPham_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnChiTietDonHang_Click(object sender, EventArgs e)
        {
            contentPanel.Controls.Clear();
            ucOrderDetail ucOrderDetail = new ucOrderDetail();
            ucOrderDetail.SetUserInfo(nguoiDung);  // Truyền thông tin người dùng vào ucOrderDetail
            ucOrderDetail.SetContentPanel(contentPanel);  // Truyền contentPanel vào ucOrderDetail
            contentPanel.Controls.Add(ucOrderDetail);
        }
    }
}
