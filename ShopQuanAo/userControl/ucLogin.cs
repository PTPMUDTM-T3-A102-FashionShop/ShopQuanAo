using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using BLL;

namespace userControl
{
    public partial class ucLogin : UserControl
    {
        public event EventHandler LoginSuccess;
        public NguoiDung User { get; private set; }
        NguoiDungBLL nguoiDungBLL = new NguoiDungBLL();
        public ucLogin()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '*';
            txtUsername.KeyDown += TxtFields_KeyDown;
            txtPassword.KeyDown += TxtFields_KeyDown;
        }
        private void TxtFields_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string message;
            NguoiDung user = nguoiDungBLL.ValidateUser(username, password, out message);

            if (user == null)
            {
                MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Đăng nhập thành công
            User = user;
            txtPassword.Text = string.Empty;
            txtUsername.Text = string.Empty;
            txtUsername.Focus();
            LoginSuccess?.Invoke(this, EventArgs.Empty);
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
