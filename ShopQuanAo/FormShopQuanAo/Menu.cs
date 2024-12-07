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
        Boolean isCollapse;

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
            btnDanhMuc.GotFocus += BtnDanhMuc_GotFocus;
            btnDanhMuc.Leave += BtnDanhMuc_Leave;
        }

        private void BtnDanhMuc_Leave(object sender, EventArgs e)
        {
            btnDanhMuc.Image = Properties.Resources.bcat;
            btnDanhMuc.FillColor = Color.BlanchedAlmond;
            btnDanhMuc.ForeColor = Color.Black;
        }

        private void BtnDanhMuc_GotFocus(object sender, EventArgs e)
        {
            btnDanhMuc.Image = Properties.Resources.wcat;
            btnDanhMuc.FillColor = Color.SandyBrown;
            btnDanhMuc.ForeColor = Color.White;
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
            btnBrand.Image = Properties.Resources.bbrand;
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

            btnHome.PerformClick();

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
            timer1.Start();
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

            detailControl.BackToProductRequested += ShowProduct;

            contentPanel.Controls.Add(detailControl);
        }

        private void UcProduct_EditBrandRequested1(int sanPhamID)
        {
            contentPanel.Controls.Clear();

            ucEditBrand editBrandControl = new ucEditBrand(sanPhamID);

            editBrandControl.BackToProductRequested += ShowProduct;

            contentPanel.Controls.Add(editBrandControl);
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            contentPanel.Controls.Clear();
            ucOrder ucOrder = new ucOrder();
            ucOrder.SetUserInfo(currentUser);
            ucOrder.SetContentPanel(contentPanel); // Truyền contentPanel vào ucOrder
            contentPanel.Controls.Add(ucOrder);
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            contentPanel.Controls.Clear();
            ucHome ucHome = new ucHome();
            contentPanel.Controls.Add(ucHome);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?", "Đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Form parentForm = this.FindForm();

                if (parentForm != null)
                {
                    Login loginForm = new Login();

                    if (loginForm != null)
                    {
                        loginForm.Show();
                    }

                    parentForm.Close();
                }
            }
        }

        private void btnDanhMuc_Click(object sender, EventArgs e)
        {
            contentPanel.Controls.Clear();
            ucCategory ucCategory = new ucCategory();
            contentPanel.Controls.Add(ucCategory);
        }

        private void btnSizeColor_Click(object sender, EventArgs e)
        {
            contentPanel.Controls.Clear();
            ucSizeColor ucSizeColor = new ucSizeColor();
            contentPanel.Controls.Add(ucSizeColor);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isCollapse)
            {
                btnCate.Image = Properties.Resources.baup;
                btnCate.HoverState.Image = Properties.Resources.waup;
                dropdownPanel.Height += 8;
                if(dropdownPanel.Size == dropdownPanel.MaximumSize)
                {
                    timer1.Stop();
                    isCollapse = false;
                }
            }
            else
            {
                btnCate.Image = Properties.Resources.badown;
                btnCate.HoverState.Image = Properties.Resources.wadown;
                dropdownPanel.Height -= 8;
                if (dropdownPanel.Size == dropdownPanel.MinimumSize)
                {
                    timer1.Stop();
                    isCollapse = true;
                }
            }
        }

        private void btnTK_Click(object sender, EventArgs e)
        {
            contentPanel.Controls.Clear();
            ucAnalytic ucAnalytic= new ucAnalytic();
            contentPanel.Controls.Add(ucAnalytic);
        }
    }
}
