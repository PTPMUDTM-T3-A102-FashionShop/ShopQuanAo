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
            buttonsLoad();
        }
        //SettingMenu
        #region
        public void buttonsLoad()
        {
            btnHome.GotFocus += BtnHome_GotFocus;
            btnHome.Leave += BtnHome_Leave;
            btnCate.GotFocus += BtnCate_GotFocus; ;
            btnCate.Leave += BtnCate_Leave; ;
            btnBrand.GotFocus += BtnBrand_GotFocus; ;
            btnBrand.Leave += BtnBrand_Leave; ;
            btnOrder.GotFocus += BtnOrder_GotFocus; ;
            btnOrder.Leave += BtnOrder_Leave; ;
            btnSP.GotFocus += BtnSP_GotFocus; ;
            btnSP.Leave += BtnSP_Leave; ;
            btnUser.GotFocus += BtnUser_GotFocus; ;
            btnUser.Leave += BtnUser_Leave; ;
            btnTK.GotFocus += BtnTK_GotFocus; ;
            btnTK.Leave += BtnTK_Leave; 
        }

        private void BtnTK_Leave(object sender, EventArgs e)
        {
            btnTK.Image = Properties.Resources.bana;
            btnTK.FillColor = Color.BlanchedAlmond;
            btnTK.ForeColor = Color.Black;
        }

        private void BtnTK_GotFocus(object sender, EventArgs e)
        {
            btnTK.Image = Properties.Resources.wana;
            btnTK.FillColor = Color.SandyBrown;
            btnTK.ForeColor = Color.White;
        }

        private void BtnUser_Leave(object sender, EventArgs e)
        {
            btnUser.Image = Properties.Resources.account;
            btnUser.FillColor = Color.BlanchedAlmond;
            btnUser.ForeColor = Color.Black;
        }

        private void BtnUser_GotFocus(object sender, EventArgs e)
        {
            btnUser.Image = Properties.Resources.wuser;
            btnUser.FillColor = Color.SandyBrown;
            btnUser.ForeColor = Color.White;
        }

        private void BtnSP_Leave(object sender, EventArgs e)
        {
            btnSP.Image = Properties.Resources.bpro;
            btnSP.FillColor = Color.BlanchedAlmond;
            btnSP.ForeColor = Color.Black;
        }

        private void BtnSP_GotFocus(object sender, EventArgs e)
        {
            btnSP.Image = Properties.Resources.wpro;
            btnSP.FillColor = Color.SandyBrown;
            btnSP.ForeColor = Color.White;
        }

        private void BtnOrder_Leave(object sender, EventArgs e)
        {
            btnOrder.Image = Properties.Resources.bo;
            btnOrder.FillColor = Color.BlanchedAlmond;
            btnOrder.ForeColor = Color.Black;
        }

        private void BtnOrder_GotFocus(object sender, EventArgs e)
        {
            btnOrder.Image = Properties.Resources.wo;
            btnOrder.FillColor = Color.SandyBrown;
            btnOrder.ForeColor = Color.White;
        }

        private void BtnBrand_Leave(object sender, EventArgs e)
        {
            btnBrand.Image = Properties.Resources.bbrand;
            btnBrand.FillColor = Color.BlanchedAlmond;
            btnBrand.ForeColor = Color.Black;
        }

        private void BtnBrand_GotFocus(object sender, EventArgs e)
        {
            btnBrand.Image = Properties.Resources.wbrand;
            btnBrand.FillColor = Color.SandyBrown;
            btnBrand.ForeColor = Color.White;
        }

        private void BtnCate_Leave(object sender, EventArgs e)
        {
            btnCate.Image = Properties.Resources.bcat;
            btnCate.FillColor = Color.BlanchedAlmond;
            btnCate.ForeColor = Color.Black;
        }

        private void BtnCate_GotFocus(object sender, EventArgs e)
        {
            btnCate.Image = Properties.Resources.wcat;
            btnCate.FillColor = Color.SandyBrown;
            btnCate.ForeColor = Color.White;
        }

        private void BtnHome_Leave(object sender, EventArgs e)
        {
            btnHome.Image = Properties.Resources.bhome;
            btnHome.FillColor = Color.BlanchedAlmond;
            btnHome.ForeColor = Color.Black;
        }

        private void BtnHome_GotFocus(object sender, EventArgs e)
        {
            btnHome.Image = Properties.Resources.whome;
            btnHome.FillColor = Color.SandyBrown;
            btnHome.ForeColor = Color.White;
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

            btnHome.Focus();

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
        #endregion
        private void btnCate_Click(object sender, EventArgs e)
        {
            contentPanel.Controls.Clear();
            ucCategory ucCategory = new ucCategory();
            contentPanel.Controls.Add(ucCategory);
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            contentPanel.Controls.Clear();
            ucUser ucUser = new ucUser();
            contentPanel.Controls.Add(ucUser);
        }

        private void btnBrand_Click(object sender, EventArgs e)
        {
            contentPanel.Controls.Clear();
            ucBrand ucBrand = new ucBrand();
            contentPanel.Controls.Add(ucBrand);
        }

        private void btnSP_Click(object sender, EventArgs e)
        {
            contentPanel.Controls.Clear();
            ucProduct ucProduct = new ucProduct();

            ucProduct.EditBrandRequested += UcProduct_EditBrandRequested1; ;
            ucProduct.EditDetailRequested += UcProduct_EditDetailRequested1; ;

            contentPanel.Controls.Add(ucProduct);
        }

        private void ShowProduct()
        {
            contentPanel.Controls.Clear();

            ucProduct ucProduct = new ucProduct();
            ucProduct.EditBrandRequested += UcProduct_EditBrandRequested1;
            ucProduct.EditDetailRequested += UcProduct_EditDetailRequested1;

            contentPanel.Controls.Add(ucProduct);
            ucProduct.Dock = DockStyle.Fill;
        }

        private void UcProduct_EditDetailRequested1(int sanPhamID)
        {
            contentPanel.Controls.Clear();

            ucDetail detailControl = new ucDetail(sanPhamID);

            //detailControl.BackToProductRequested += ShowProduct;

            contentPanel.Controls.Add(detailControl);
        }

        private void UcProduct_EditBrandRequested1(int sanPhamID)
        {
            contentPanel.Controls.Clear();

            ucEditBrand editBrandControl = new ucEditBrand(sanPhamID);

            editBrandControl.BackToProductRequested += ShowProduct;

            contentPanel.Controls.Add(editBrandControl);
        }

    }
}
