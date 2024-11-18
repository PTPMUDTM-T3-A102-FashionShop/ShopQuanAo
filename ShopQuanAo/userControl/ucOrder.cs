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
    public partial class ucOrder : UserControl
    {
        private NguoiDung nguoiDung;
        public ucOrder()
        {
            InitializeComponent();
        }

        private void ucOrder_Load(object sender, EventArgs e)
        {

        }
        public void SetUserInfo(NguoiDung user)
        {
            nguoiDung = user;
            lblTenNguoiDung.Text = $"Xin chào {nguoiDung.HoTen}";
        }

        private void lblTenNguoiDung_Click(object sender, EventArgs e)
        {

        }
    }
}
