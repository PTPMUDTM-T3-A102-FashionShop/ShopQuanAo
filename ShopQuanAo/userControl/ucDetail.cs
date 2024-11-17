using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace userControl
{
    public partial class ucDetail : UserControl
    {
        private int sanPhamID;
        public ucDetail(int sanPhamID)
        {
            InitializeComponent();
            this.sanPhamID = sanPhamID;
        }
    }
}
