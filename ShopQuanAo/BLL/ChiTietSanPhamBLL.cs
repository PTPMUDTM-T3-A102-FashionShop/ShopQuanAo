using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ChiTietSanPhamBLL
    {
        ChiTietSanPhamDAL chiTietSanPhamDAL = new ChiTietSanPhamDAL();
        public ChiTietSanPhamBLL() {  }

        public List<ChiTietSanPham> getChiTietBySanPhamID(int sanPhamID)
        {
            return chiTietSanPhamDAL.getChiTietBySanPhamID(sanPhamID);
        }
        public bool AddChiTietSanPham(ChiTietSanPham chiTietSanPham)
        {
            try
            {
                return chiTietSanPhamDAL.AddChiTietSanPham(chiTietSanPham);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm chi tiết sản phẩm", ex);
            }
        }
        public bool UpdateChiTietSanPham(ChiTietSanPham chiTietSanPham)
        {
            try
            {
                return chiTietSanPhamDAL.UpdateChiTietSanPham(chiTietSanPham);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật chi tiết sản phẩm", ex);
            }
        }
        public bool DeleteChiTietSanPham(int ChiTietID)
        {
            try
            {
                return chiTietSanPhamDAL.DeleteChiTietSanPham(ChiTietID); // Gọi phương thức xóa trong DAL
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa chi tiết sản phẩm", ex);
            }
        }

    }
}
