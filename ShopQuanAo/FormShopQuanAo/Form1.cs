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

namespace FormShopQuanAo
{
    public partial class Form1 : Form
    {
        DanhMucBLL danhMucBLL = new DanhMucBLL();

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = danhMucBLL.getAllDanhMucBLL();
        }

    }
}
