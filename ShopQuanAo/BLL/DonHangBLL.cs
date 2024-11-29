using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data;
namespace BLL
{
    public class DonHangBLL
    {
        private DonHangDAL donHangDAL;

        public DonHangBLL()
        {
            donHangDAL = new DonHangDAL();
        }

        public void ThemDonHang(DonHangDTO donHang, List<ChiTietDonHangDTO> chiTietDonHangs)
        {
            int donHangID = donHangDAL.InsertDonHang(donHang);

            foreach (var chiTiet in chiTietDonHangs)
            {
                chiTiet.DonHangID = donHangID;
                donHangDAL.InsertChiTietDonHang(chiTiet);
                donHangDAL.UpdateSoLuongTonKho(chiTiet.SanPhamID, chiTiet.SoLuong);
            }
        }
    }
}
