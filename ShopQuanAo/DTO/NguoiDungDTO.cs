using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NguoiDungDTO
    {
        public int NguoiDungID { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public DateTime NgaySinh { get; set; }
        public int MaNhomNguoiDung { get; set; }
        public string GioiTinh { get; set; }
        public bool KichHoat { get; set; }
    }
}
