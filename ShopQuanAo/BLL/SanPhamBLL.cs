using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace BLL
{
    public class SanPhamBLL
    {
        SanPhamDAL sanPhamDAL = new SanPhamDAL();
        public SanPhamBLL() { }

        // Lấy tất cả sản phẩm
        public List<SanPham> GetAllSanPham()
        {
            return sanPhamDAL.GetAllSanPham();
        }

        // Thêm sản phẩm mới
        public bool AddSanPham(SanPham sanPham)
        {
            return sanPhamDAL.AddSanPham(sanPham);
        }

        // Cập nhật sản phẩm
        public bool UpdateSanPham(SanPham sanPham)
        {
            return sanPhamDAL.UpdateSanPham(sanPham);
        }

        // Xóa sản phẩm
        public void DeleteSanPham(int sanPhamID)
        {
            sanPhamDAL.DeleteSanPham(sanPhamID);
        }

        // Lấy sản phẩm theo DanhMucID
        public List<SanPham> GetSanPhamByDanhMucID(int danhMucID)
        {
            return sanPhamDAL.GetSanPhamByDanhMucID(danhMucID);
        }

        // Code thuan`
        public List<SanPhamChiTietDTO> GetSanPhamChiTiet()
        {
            return sanPhamDAL.GetSanPhamChiTiet();
        }
    }
}
