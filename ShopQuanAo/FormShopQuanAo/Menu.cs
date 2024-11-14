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
using Guna.UI2.WinForms;
using userControl;

namespace FormShopQuanAo
{
    public partial class Menu : Form
    {
        private NguoiDung currentUser;
        NguoiDungBLL nguoiDungBLL = new NguoiDungBLL();

        public Menu(NguoiDung user)
        {
            InitializeComponent();
            currentUser = user;
            this.Load += Menu_Load;
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            btnHome.Tag = "MH001";
            btnCate.Tag = "MH002";
            btnSP.Tag = "MH003";
            btnBrand.Tag = "MH004";
            btnOrder.Tag = "MH005";
            btnUser.Tag = "MH006";
            btnTK.Tag = "MH007";

            List<string> userPermissions = nguoiDungBLL.GetUserPermissions(currentUser.NguoiDungID);

            SetButtonVisibility(userPermissions);
        }
        private void SetButtonVisibility(List<string> userPermissions)
        {
            foreach (Control control in sideBar.Controls)
            {
                if (control is Guna2Button btn && btn.Name != "btnLogout")
                {
                    string screenCode = (string)btn.Tag;

                    if (userPermissions.Contains(screenCode))
                    {
                        btn.Visible = true;
                    }
                    else
                    {
                        btn.Visible = false;
                    }
                }
            }
        }



        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCate_Click(object sender, EventArgs e)
        {
            ucCategory ucCategory = new ucCategory();
            contentPanel.Controls.Add(ucCategory);
        }

    }
}
