using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SanPhamChiTietDTO
    {
        public int SanPhamID { get; set; }
        public string TenSanPham { get; set; }
        public string MoTa { get; set; }
        public bool KichHoat { get; set; }
        public decimal Gia { get; set; }
        public int SoLuongTonKho { get; set; }
        public string TenMau { get; set; }
        public string TenSize { get; set; }
    }
}
