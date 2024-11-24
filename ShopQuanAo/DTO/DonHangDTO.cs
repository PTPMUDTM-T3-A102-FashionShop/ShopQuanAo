using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DonHangDTO
    {
        public int DonHangID { get; set; }
        public int NguoiDungID { get; set; }
        public decimal TongTien { get; set; }
        public string TinhTrangDonHang { get; set; }
        public DateTime NgayDatHang { get; set; }
        public string HinhThucThanhToan { get; set; }
        public string TinhTrangThanhToan { get; set; }
        public DateTime NgayThanhToan { get; set; }
    }
}
