using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FormShopQuanAo;

namespace userControl
{
    public partial class ucLogin : UserControl
    {
        public DBConnection DbConnection;
        public ucLogin()
        {
            InitializeComponent();
            DbConnection = new DBConnection();

            txtPassword.PasswordChar = '*';
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Kiểm tra nếu các ô text rỗng
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Câu lệnh SQL kiểm tra tài khoản và mật khẩu
            string query = "SELECT COUNT(*) FROM NguoiDung WHERE TenDangNhap = @username AND MatKhau = @password AND KichHoat = 1";

            try
            {
                DbConnection.conn.Open();
                DbConnection.cmd.CommandText = query;
                DbConnection.cmd.Parameters.Clear();
                DbConnection.cmd.Parameters.AddWithValue("@username", username);
                DbConnection.cmd.Parameters.AddWithValue("@password", password);

                int count = (int)DbConnection.cmd.ExecuteScalar();

                // Nếu có tài khoản hợp lệ
                if (count > 0)
                {
                    // Đóng form hiện tại và mở Form1
                    //FormShopQuanAo.Form1 form1 = new FormShopQuanAo.Form1();
                    //form1.Show();
                    //this.ParentForm.Hide(); // Đóng form hiện tại
                }
                else
                {
                    // Thông báo lỗi nếu đăng nhập không thành công
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                DbConnection.conn.Close();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát không?", "Thoát ứng dụng", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit(); // Thoát ứng dụng
            }
        }

        private void cbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowPassword.Checked)
            {
                // Nếu checkbox được tick, hiển thị mật khẩu
                txtPassword.PasswordChar = '\0';  // Mật khẩu sẽ hiển thị dưới dạng chữ
            }
            else
            {
                // Nếu checkbox không được tick, ẩn mật khẩu
                txtPassword.PasswordChar = '*';  // Mật khẩu sẽ hiển thị dưới dạng dấu *
            }
        }
    }
}
