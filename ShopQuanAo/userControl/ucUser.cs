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
using FormShopQuanAo;

namespace userControl
{
    public partial class ucUser : UserControl
    {
        private int selectedUserId = -1;
        private DBConnection dbConnection;  
        public ucUser()
        {
            InitializeComponent();
            dbConnection = new DBConnection();
            dgvNguoiDung.CellClick += DgvNguoiDung_CellClick;

            dgvNguoiDung.ReadOnly = true; // Ngăn chặn chỉnh sửa trực tiếp
            // Đặt thuộc tính DropDownStyle cho ComboBox
            cbGioiTinh.DropDownStyle = ComboBoxStyle.DropDownList;
            cbKichHoat.DropDownStyle = ComboBoxStyle.DropDownList;
            cbNhomNguoiDung.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void DgvNguoiDung_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNguoiDung.Rows[e.RowIndex];
                txtTenDangNhap.Text = row.Cells["TenDangNhap"].Value.ToString();
                txtMatKhau.Text = row.Cells["MatKhau"].Value.ToString();
                txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtSDT.Text = row.Cells["SoDienThoai"].Value.ToString();
                txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();

                // Kiểm tra giá trị của ô NgaySinh
                if (row.Cells["NgaySinh"].Value != DBNull.Value)
                {
                    txtNgaySinh.Text = Convert.ToDateTime(row.Cells["NgaySinh"].Value).ToString("dd/MM/yyyy");
                }
                else
                {
                    txtNgaySinh.Text = string.Empty; // Hoặc đặt giá trị mặc định
                }
                cbNhomNguoiDung.SelectedValue = row.Cells["MaNhomNguoiDung"].Value;
                cbGioiTinh.SelectedItem = row.Cells["GioiTinh"].Value.ToString();
                cbKichHoat.SelectedItem = row.Cells["KichHoatDisplay"].Value.ToString();
            }
        }

        private void ucUser_Load(object sender, EventArgs e)
        {
            LoadNguoiDung();
            LoadNhomNguoiDung();
            cbGioiTinh.Items.AddRange(new string[] { "Nam", "Nữ" });
            cbKichHoat.Items.AddRange(new string[] { "Đã kích hoạt", "Chưa kích hoạt" });

            // Đặt giá trị mặc định cho các ComboBox và TextBox
            ClearInputFields();
        }
        private void ClearInputFields()
        {
            txtTenDangNhap.Text = string.Empty;
            txtMatKhau.Text = string.Empty;
            txtHoTen.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtSDT.Text = string.Empty;
            txtDiaChi.Text = string.Empty;
            txtNgaySinh.Text = string.Empty;
            cbNhomNguoiDung.SelectedIndex = -1;
            cbGioiTinh.SelectedIndex = -1;
            cbKichHoat.SelectedIndex = -1;
        }

        private void EnsureConnectionClosed()
        {
            if (dbConnection.conn.State == ConnectionState.Open)
            {
                dbConnection.conn.Close();
            }
        }

        private void LoadNguoiDung()
        {
            try
            {
                EnsureConnectionClosed(); // Đảm bảo kết nối đã đóng
                dbConnection.conn.Open();
                string query = "SELECT TenDangNhap, MatKhau, HoTen, Email, SoDienThoai, DiaChi, NgaySinh, MaNhomNguoiDung, GioiTinh, KichHoat FROM NguoiDung";
                SqlDataAdapter da = new SqlDataAdapter(query, dbConnection.conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Thêm cột hiển thị KichHoat dưới dạng chuỗi
                dt.Columns.Add("KichHoatDisplay", typeof(string));
                foreach (DataRow row in dt.Rows)
                {
                    row["KichHoatDisplay"] = (bool)row["KichHoat"] ? "Đã kích hoạt" : "Chưa kích hoạt";
                }

                dgvNguoiDung.DataSource = dt;
                dgvNguoiDung.Columns["KichHoat"].Visible = false; // Ẩn cột KichHoat gốc
                dgvNguoiDung.Columns["KichHoatDisplay"].HeaderText = "Kích Hoạt"; // Đặt tên cho cột hiển thị
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void LoadNhomNguoiDung()
        {
            try
            {
                EnsureConnectionClosed(); // Đảm bảo kết nối đã đóng
                dbConnection.conn.Open();
                string query = "SELECT MaNhomNguoiDung, TenNhomNguoiDung FROM NhomNguoiDung";
                SqlDataAdapter da = new SqlDataAdapter(query, dbConnection.conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cbNhomNguoiDung.DataSource = dt;
                cbNhomNguoiDung.DisplayMember = "TenNhomNguoiDung";
                cbNhomNguoiDung.ValueMember = "MaNhomNguoiDung";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                dbConnection.conn.Close();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Kiểm tra các TextBox và ComboBox đã có dữ liệu hay chưa
            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text) ||
                string.IsNullOrWhiteSpace(txtMatKhau.Text) ||
                string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtSDT.Text) ||
                string.IsNullOrWhiteSpace(txtDiaChi.Text) ||
                string.IsNullOrWhiteSpace(txtNgaySinh.Text) ||
                cbNhomNguoiDung.SelectedIndex == -1 ||
                cbGioiTinh.SelectedIndex == -1 ||
                cbKichHoat.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            // Kiểm tra tính duy nhất của TenDangNhap và Email
            try
            {
                EnsureConnectionClosed(); // Đảm bảo kết nối đã đóng
                dbConnection.conn.Open();
                string checkQuery = "SELECT COUNT(*) FROM NguoiDung WHERE TenDangNhap = @TenDangNhap OR Email = @Email";
                SqlCommand checkCmd = new SqlCommand(checkQuery, dbConnection.conn);
                checkCmd.Parameters.AddWithValue("@TenDangNhap", txtTenDangNhap.Text);
                checkCmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                int count = (int)checkCmd.ExecuteScalar();
                if (count > 0)
                {
                    MessageBox.Show("Tên đăng nhập hoặc Email đã tồn tại.");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return;
            }
            finally
            {
                dbConnection.conn.Close();
            }

            // Kiểm tra định dạng số điện thoại
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtSDT.Text, @"^0\d+$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ. Số điện thoại phải bắt đầu bằng số '0' và chỉ chứa số.");
                return;
            }

            // Kiểm tra định dạng ngày sinh
            DateTime ngaySinh;
            if (!DateTime.TryParseExact(txtNgaySinh.Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out ngaySinh))
            {
                MessageBox.Show("Ngày sinh không hợp lệ. Vui lòng nhập theo định dạng dd/MM/yyyy.");
                return;
            }

            // Lấy giá trị MaNhomNguoiDung từ cbNhomNguoiDung
            int maNhomNguoiDung = (int)cbNhomNguoiDung.SelectedValue;

            // Lấy giá trị GioiTinh từ cbGioiTinh
            string gioiTinh = cbGioiTinh.SelectedItem.ToString();

            // Lấy giá trị KichHoat từ cbKichHoat
            bool kichHoat = cbKichHoat.SelectedItem.ToString() == "Đã kích hoạt";

            // Hỏi người dùng xem có đồng ý thêm không
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thêm người dùng này?", "Xác nhận", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                // Thực hiện chèn dữ liệu vào cơ sở dữ liệu
                try
                {
                    dbConnection.conn.Open();
                    string insertQuery = "INSERT INTO NguoiDung (TenDangNhap, MatKhau, HoTen, Email, SoDienThoai, DiaChi, NgaySinh, MaNhomNguoiDung, GioiTinh, KichHoat) " +
                                         "VALUES (@TenDangNhap, @MatKhau, @HoTen, @Email, @SoDienThoai, @DiaChi, @NgaySinh, @MaNhomNguoiDung, @GioiTinh, @KichHoat)";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, dbConnection.conn);
                    insertCmd.Parameters.AddWithValue("@TenDangNhap", txtTenDangNhap.Text);
                    insertCmd.Parameters.AddWithValue("@MatKhau", txtMatKhau.Text);
                    insertCmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text);
                    insertCmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    insertCmd.Parameters.AddWithValue("@SoDienThoai", txtSDT.Text);
                    insertCmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                    insertCmd.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                    insertCmd.Parameters.AddWithValue("@MaNhomNguoiDung", maNhomNguoiDung);
                    insertCmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                    insertCmd.Parameters.AddWithValue("@KichHoat", kichHoat);
                    insertCmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm người dùng thành công.");
                    LoadNguoiDung(); // Tải lại dữ liệu sau khi thêm
                    ClearInputFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    dbConnection.conn.Close();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Kiểm tra các TextBox và ComboBox đã có dữ liệu hay chưa
            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text) ||
                string.IsNullOrWhiteSpace(txtMatKhau.Text) ||
                string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtSDT.Text) ||
                string.IsNullOrWhiteSpace(txtDiaChi.Text) ||
                string.IsNullOrWhiteSpace(txtNgaySinh.Text) ||
                cbNhomNguoiDung.SelectedIndex == -1 ||
                cbGioiTinh.SelectedIndex == -1 ||
                cbKichHoat.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            int nguoiDungID = -1;

            // Lấy NguoiDungID dựa vào TenDangNhap hoặc Email
            try
            {
                EnsureConnectionClosed(); // Đảm bảo kết nối đã đóng
                dbConnection.conn.Open();
                string getIdQuery = "SELECT NguoiDungID FROM NguoiDung WHERE TenDangNhap = @TenDangNhap OR Email = @Email";
                SqlCommand getIdCmd = new SqlCommand(getIdQuery, dbConnection.conn);
                getIdCmd.Parameters.AddWithValue("@TenDangNhap", txtTenDangNhap.Text);
                getIdCmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                object resultID = getIdCmd.ExecuteScalar();
                if (resultID != null)
                {
                    nguoiDungID = (int)resultID;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy người dùng.");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return;
            }
            finally
            {
                dbConnection.conn.Close();
            }

            // Kiểm tra tính duy nhất của TenDangNhap và Email (trừ trường hợp không thay đổi)
            try
            {
                EnsureConnectionClosed(); // Đảm bảo kết nối đã đóng
                dbConnection.conn.Open();
                string checkQuery = "SELECT COUNT(*) FROM NguoiDung WHERE (TenDangNhap = @TenDangNhap OR Email = @Email) AND NguoiDungID != @NguoiDungID";
                SqlCommand checkCmd = new SqlCommand(checkQuery, dbConnection.conn);
                checkCmd.Parameters.AddWithValue("@TenDangNhap", txtTenDangNhap.Text);
                checkCmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                checkCmd.Parameters.AddWithValue("@NguoiDungID", nguoiDungID);
                int count = (int)checkCmd.ExecuteScalar();
                if (count > 0)
                {
                    MessageBox.Show("Tên đăng nhập hoặc Email đã tồn tại.");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return;
            }
            finally
            {
                dbConnection.conn.Close();
            }

            // Kiểm tra định dạng số điện thoại
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtSDT.Text, @"^0\d+$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ. Số điện thoại phải bắt đầu bằng số '0' và chỉ chứa số.");
                return;
            }

            // Kiểm tra định dạng ngày sinh
            DateTime ngaySinh;
            if (!DateTime.TryParseExact(txtNgaySinh.Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out ngaySinh))
            {
                MessageBox.Show("Ngày sinh không hợp lệ. Vui lòng nhập theo định dạng dd/MM/yyyy.");
                return;
            }

            // Lấy giá trị MaNhomNguoiDung từ cbNhomNguoiDung
            int maNhomNguoiDung = (int)cbNhomNguoiDung.SelectedValue;

            // Lấy giá trị GioiTinh từ cbGioiTinh
            string gioiTinh = cbGioiTinh.SelectedItem.ToString();

            // Lấy giá trị KichHoat từ cbKichHoat
            bool kichHoat = cbKichHoat.SelectedItem.ToString() == "Đã kích hoạt";

            // Hỏi người dùng xem có đồng ý sửa không
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn sửa thông tin người dùng này?", "Xác nhận", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                // Thực hiện cập nhật dữ liệu vào cơ sở dữ liệu
                try
                {
                    EnsureConnectionClosed(); // Đảm bảo kết nối đã đóng
                    dbConnection.conn.Open();
                    string updateQuery = "UPDATE NguoiDung SET TenDangNhap = @TenDangNhap, MatKhau = @MatKhau, HoTen = @HoTen, Email = @Email, SoDienThoai = @SoDienThoai, DiaChi = @DiaChi, NgaySinh = @NgaySinh, MaNhomNguoiDung = @MaNhomNguoiDung, GioiTinh = @GioiTinh, KichHoat = @KichHoat WHERE NguoiDungID = @NguoiDungID";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, dbConnection.conn);
                    updateCmd.Parameters.AddWithValue("@TenDangNhap", txtTenDangNhap.Text);
                    updateCmd.Parameters.AddWithValue("@MatKhau", txtMatKhau.Text);
                    updateCmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text);
                    updateCmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    updateCmd.Parameters.AddWithValue("@SoDienThoai", txtSDT.Text);
                    updateCmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                    updateCmd.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                    updateCmd.Parameters.AddWithValue("@MaNhomNguoiDung", maNhomNguoiDung);
                    updateCmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                    updateCmd.Parameters.AddWithValue("@KichHoat", kichHoat);
                    updateCmd.Parameters.AddWithValue("@NguoiDungID", nguoiDungID);
                    updateCmd.ExecuteNonQuery();
                    MessageBox.Show("Sửa thông tin người dùng thành công.");
                    LoadNguoiDung(); // Tải lại dữ liệu sau khi sửa
                    ClearInputFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    dbConnection.conn.Close();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Kiểm tra các TextBox đã có dữ liệu hay chưa
            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Vui lòng chọn một người dùng từ bảng để xóa.");
                return;
            }

            int nguoiDungID = -1;

            // Lấy NguoiDungID dựa vào TenDangNhap hoặc Email
            try
            {
                EnsureConnectionClosed(); // Đảm bảo kết nối đã đóng
                dbConnection.conn.Open();
                string getIdQuery = "SELECT NguoiDungID FROM NguoiDung WHERE TenDangNhap = @TenDangNhap OR Email = @Email";
                SqlCommand getIdCmd = new SqlCommand(getIdQuery, dbConnection.conn);
                getIdCmd.Parameters.AddWithValue("@TenDangNhap", txtTenDangNhap.Text);
                getIdCmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                object result = getIdCmd.ExecuteScalar();
                if (result != null)
                {
                    nguoiDungID = (int)result;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy người dùng.");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return;
            }
            finally
            {
                dbConnection.conn.Close();
            }

            // Hỏi người dùng xem có đồng ý xóa không
            DialogResult resultDialog = MessageBox.Show("Bạn có chắc chắn muốn xóa (vô hiệu hóa) người dùng này?", "Xác nhận", MessageBoxButtons.YesNo);
            if (resultDialog == DialogResult.Yes)
            {
                // Thực hiện cập nhật giá trị KichHoat thành 0 (Không kích hoạt)
                try
                {
                    EnsureConnectionClosed(); // Đảm bảo kết nối đã đóng
                    dbConnection.conn.Open();
                    string updateQuery = "UPDATE NguoiDung SET KichHoat = 0 WHERE NguoiDungID = @NguoiDungID";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, dbConnection.conn);
                    updateCmd.Parameters.AddWithValue("@NguoiDungID", nguoiDungID);
                    updateCmd.ExecuteNonQuery();
                    MessageBox.Show("Người dùng đã được vô hiệu hóa.");
                    LoadNguoiDung(); // Tải lại dữ liệu sau khi vô hiệu hóa
                    ClearInputFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    dbConnection.conn.Close();
                }
            }
        }
        private void dgvNguoiDung_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtTenDangNhap_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMatKhau_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtHoTen_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSDT_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDiaChi_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNgaySinh_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTenNguoiDung_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbGioiTinh_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbKichHoat_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtNhomNguoiDung_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbNhomNguoiDung_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
