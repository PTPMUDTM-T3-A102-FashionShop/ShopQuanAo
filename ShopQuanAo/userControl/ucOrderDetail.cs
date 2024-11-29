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
    public partial class ucOrderDetail : UserControl
    {
        private Panel contentPanel;
        private NguoiDung nguoiDung;
        private DBConnection dBConnection;
        private int selectedDonHangID;
        public ucOrderDetail()
        {
            InitializeComponent();
            dBConnection = new DBConnection();

            // Đặt thuộc tính DropDownStyle cho ComboBox
            cbTinhTrangDonHang.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTinhTrangThanhToan.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void ucOrderDetail_Load(object sender, EventArgs e)
        {
            // Thiết lập các giá trị cho ComboBox khi form được tải
            cbTinhTrangDonHang.Items.AddRange(new string[] { "Đang Xử Lý", "Đã Xác Nhận", "Đang Vận Chuyển", "Hoàn Thành", "Đã Hủy" });
            cbTinhTrangThanhToan.Items.AddRange(new string[] { "Đã thanh toán", "Chưa thanh toán" });
            LoadDonHangData();
            dgvDonHang.ReadOnly = true; // Ngăn người dùng chỉnh sửa dữ liệu trực tiếp trên DataGridView
        }

        private void LoadDonHangData()
        {
            try
            {
                dBConnection.OpenConnection();
                string query = "SELECT * FROM DonHang";
                SqlDataAdapter adapter = new SqlDataAdapter(query, dBConnection.conn);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dgvDonHang.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dBConnection.CloseConnection();
            }
        }

        public void SetUserInfo(NguoiDung user)
        {
            nguoiDung = user;
            lblTenNguoiDung.Text = $"⭐ Xin chào, {nguoiDung.HoTen}";
        }
        public void SetContentPanel(Panel panel)
        {
            contentPanel = panel;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (contentPanel != null)
            {
                contentPanel.Controls.Clear();
                ucOrder ucOrder = new ucOrder();
                ucOrder.SetUserInfo(nguoiDung); // Truyền thông tin người dùng vào ucOrder
                ucOrder.SetContentPanel(contentPanel); // Đảm bảo đã thiết lập lại contentPanel cho ucOrder
                contentPanel.Controls.Add(ucOrder);
            }
            else
            {
                MessageBox.Show("contentPanel is not set.");
            }
        }

        private void lblTenNguoiDung_Click(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu trong các điều khiển
            if (cbTinhTrangDonHang.SelectedItem == null || string.IsNullOrEmpty(txtHinhThucThanhToan.Text) || cbTinhTrangThanhToan.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn một đơn hàng từ bảng để chỉnh sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hiển thị thông báo xác nhận
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn chỉnh sửa đơn hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    dBConnection.OpenConnection();
                    string query = @"
                        UPDATE DonHang
                        SET TinhTrangDonHang = @TinhTrangDonHang,
                            HinhThucThanhToan = @HinhThucThanhToan,
                            TinhTrangThanhToan = @TinhTrangThanhToan,
                            NgayThanhToan = GETDATE(),
                            NhanVienID = @NhanVienID
                        WHERE DonHangID = @DonHangID";

                    SqlCommand cmd = new SqlCommand(query, dBConnection.conn);
                    cmd.Parameters.AddWithValue("@TinhTrangDonHang", cbTinhTrangDonHang.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@HinhThucThanhToan", txtHinhThucThanhToan.Text);
                    cmd.Parameters.AddWithValue("@TinhTrangThanhToan", cbTinhTrangThanhToan.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@NhanVienID", nguoiDung.NguoiDungID);
                    cmd.Parameters.AddWithValue("@DonHangID", selectedDonHangID);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Chỉnh sửa đơn hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Tải lại dữ liệu sau khi chỉnh sửa
                    LoadDonHangData();

                    // Làm trống các điều khiển
                    cbTinhTrangDonHang.SelectedItem = null;
                    txtHinhThucThanhToan.Clear();
                    cbTinhTrangThanhToan.SelectedItem = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    dBConnection.CloseConnection();
                }
            }
        }

        private int GetSelectedDonHangID()
        {
            if (dgvDonHang.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvDonHang.SelectedRows[0];
                return Convert.ToInt32(selectedRow.Cells["DonHangID"].Value);
            }
            return -1;
        }

        private void dgvDonHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cbTinhTrangDonHang_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtHinhThucThanhToan_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbTinhTrangThanhToan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgvDonHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvDonHang.Rows.Count - 1) // Kiểm tra để tránh hàng cuối cùng trống
            {
                DataGridViewRow row = dgvDonHang.Rows[e.RowIndex];
                selectedDonHangID = Convert.ToInt32(row.Cells["DonHangID"].Value);
                cbTinhTrangDonHang.SelectedItem = row.Cells["TinhTrangDonHang"].Value.ToString();
                txtHinhThucThanhToan.Text = row.Cells["HinhThucThanhToan"].Value.ToString();
                cbTinhTrangThanhToan.SelectedItem = row.Cells["TinhTrangThanhToan"].Value.ToString();
            }
        }
    }
}
