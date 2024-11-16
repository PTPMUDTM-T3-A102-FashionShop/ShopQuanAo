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
    public partial class ucUser : UserControl
    {
        private int selectedUserId = -1;
        private NguoiDungBLL bl;
        private DBConnection dbConnection;
        public ucUser()
        {
            InitializeComponent();
            dbConnection = new DBConnection();
            bl = new NguoiDungBLL();
            dgvNguoiDung.CellClick += DgvNguoiDung_CellClick;

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

                if (row.Cells["TenDangNhap"].Value == DBNull.Value ||
                    row.Cells["MatKhau"].Value == DBNull.Value ||
                    row.Cells["HoTen"].Value == DBNull.Value ||
                    row.Cells["Email"].Value == DBNull.Value ||
                    row.Cells["SoDienThoai"].Value == DBNull.Value ||
                    row.Cells["DiaChi"].Value == DBNull.Value ||
                    row.Cells["NgaySinh"].Value == DBNull.Value ||
                    row.Cells["MaNhomNguoiDung"].Value == DBNull.Value ||
                    row.Cells["GioiTinh"].Value == DBNull.Value ||
                    row.Cells["KichHoat"].Value == DBNull.Value)
                {
                    ClearInputFields();
                }
                else
                {
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

                    // Kiểm tra giá trị của ô MaNhomNguoiDung
                    if (row.Cells["MaNhomNguoiDung"].Value != DBNull.Value)
                    {
                        int maNhomNguoiDung = Convert.ToInt32(row.Cells["MaNhomNguoiDung"].Value);
                        cbNhomNguoiDung.SelectedValue = maNhomNguoiDung;
                    }
                    else
                    {
                        cbNhomNguoiDung.SelectedIndex = -1; // Hoặc đặt giá trị mặc định
                    }

                    cbGioiTinh.SelectedItem = row.Cells["GioiTinh"].Value.ToString();
                    cbKichHoat.SelectedItem = (bool)row.Cells["KichHoat"].Value ? "Đã kích hoạt" : "Chưa kích hoạt";
                }
            }
        }

        private void ucUser_Load(object sender, EventArgs e)
        {
            LoadData();
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

        private void LoadData()
        {
            dgvNguoiDung.DataSource = bl.GetAllNguoiDung();
            dgvNguoiDung.ReadOnly = true; // Ngăn người dùng chỉnh sửa dữ liệu trực tiếp trên DataGridView
        }
        private void LoadNhomNguoiDung()
        {
            cbNhomNguoiDung.DataSource = bl.GetAllNhomNguoiDung();
            cbNhomNguoiDung.DisplayMember = "TenNhomNguoiDung";
            cbNhomNguoiDung.ValueMember = "MaNhomNguoiDung";
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
            if (!bl.CheckUnique(txtTenDangNhap.Text, txtEmail.Text))
            {
                MessageBox.Show("Tên đăng nhập hoặc Email đã tồn tại.");
                return;
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
            {// Thực hiện chèn dữ liệu vào cơ sở dữ liệu
                try
                {
                    DTO.NguoiDung nguoiDung = new DTO.NguoiDung
                    {
                        TenDangNhap = txtTenDangNhap.Text,
                        MatKhau = txtMatKhau.Text,
                        HoTen = txtHoTen.Text,
                        Email = txtEmail.Text,
                        SoDienThoai = txtSDT.Text,
                        DiaChi = txtDiaChi.Text,
                        NgaySinh = ngaySinh,
                        MaNhomNguoiDung = maNhomNguoiDung,
                        GioiTinh = gioiTinh,
                        KichHoat = kichHoat
                    };

                    bl.InsertNguoiDung(nguoiDung);
                    MessageBox.Show("Thêm người dùng thành công.");
                    LoadData(); // Tải lại dữ liệu sau khi thêm
                    ClearInputFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
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
                nguoiDungID = GetNguoiDungID(txtTenDangNhap.Text, txtEmail.Text);
                if (nguoiDungID == -1)
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

            // Kiểm tra tính duy nhất của TenDangNhap và Email (trừ trường hợp không thay đổi)
            if (!bl.CheckUnique(txtTenDangNhap.Text, txtEmail.Text, nguoiDungID))
            {
                MessageBox.Show("Tên đăng nhập hoặc Email đã tồn tại.");
                return;
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
                    DTO.NguoiDung nguoiDung = new DTO.NguoiDung
                    {
                        NguoiDungID = nguoiDungID,
                        TenDangNhap = txtTenDangNhap.Text,
                        MatKhau = txtMatKhau.Text,
                        HoTen = txtHoTen.Text,
                        Email = txtEmail.Text,
                        SoDienThoai = txtSDT.Text,
                        DiaChi = txtDiaChi.Text,
                        NgaySinh = ngaySinh,
                        MaNhomNguoiDung = maNhomNguoiDung,
                        GioiTinh = gioiTinh,
                        KichHoat = kichHoat
                    };

                    bl.UpdateNguoiDung(nguoiDung);
                    MessageBox.Show("Sửa thông tin người dùng thành công.");
                    LoadData(); // Tải lại dữ liệu sau khi sửa
                    ClearInputFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private int GetNguoiDungID(string tenDangNhap, string email)
        {
            int nguoiDungID = -1;
            try
            {
                dbConnection.conn.Open();
                string getIdQuery = "SELECT NguoiDungID FROM NguoiDung WHERE TenDangNhap = @TenDangNhap OR Email = @Email";
                SqlCommand getIdCmd = new SqlCommand(getIdQuery, dbConnection.conn);
                getIdCmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                getIdCmd.Parameters.AddWithValue("@Email", email);
                object resultID = getIdCmd.ExecuteScalar();
                if (resultID != null)
                {
                    nguoiDungID = (int)resultID;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                dbConnection.conn.Close();
            }
            return nguoiDungID;
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
                nguoiDungID = GetNguoiDungID(txtTenDangNhap.Text, txtEmail.Text);
                if (nguoiDungID == -1)
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

            // Hỏi người dùng xem có đồng ý xóa không
            DialogResult resultDialog = MessageBox.Show("Bạn có chắc chắn muốn xóa (vô hiệu hóa) người dùng này?", "Xác nhận", MessageBoxButtons.YesNo);
            if (resultDialog == DialogResult.Yes)
            {
                // Thực hiện cập nhật giá trị KichHoat thành 0 (Không kích hoạt)
                try
                {
                    bl.DisableNguoiDung(nguoiDungID);
                    MessageBox.Show("Người dùng đã được vô hiệu hóa.");
                    LoadData(); // Tải lại dữ liệu sau khi vô hiệu hóa
                    ClearInputFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
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
