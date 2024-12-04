using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using userControl;

namespace FormShopQuanAo
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            ucLogin loginControl = ucLogin1;
            loginControl.LoginSuccess += LoginControl_LoginSuccess;
        }

        private void LoginControl_LoginSuccess(object sender, EventArgs e)
        {
            var user = (sender as ucLogin).User;

            this.Hide();
            Menu menuForm = new Menu(user);
            menuForm.Show();
        }
    }
}
