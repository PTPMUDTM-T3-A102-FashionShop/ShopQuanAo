using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class NhaCungCapBLL
    {
        NhaCungCapDAL nhaCungCapDAL = new NhaCungCapDAL();
        SanPhamDAL sanPhamDAL = new SanPhamDAL();
        public NhaCungCapBLL() { }

        public List<NhaCungCap> getAllNhaCungCapBLL()
        {
            return nhaCungCapDAL.getAllNhaCungCapDAL();
        }

        public bool AddNhaCungCap(NhaCungCap newBrand)
        {
            return nhaCungCapDAL.AddNhaCungCap(newBrand);
        }

        public bool UpdateNhaCungCap(int brandID, string newTenNhaCungCap, string newEmail, string newSoDienThoai, string newDiaChi, string newMoTa)
        {
            return nhaCungCapDAL.UpdateNhaCungCap(brandID, newTenNhaCungCap, newEmail, newSoDienThoai, newDiaChi, newMoTa);
        }

        public bool DeleteNhaCungCap(int brandID)
        {
            return nhaCungCapDAL.DeleteNhaCungCap(brandID);
        }
        public bool HasProductsInSanPham(int brandID)
        {
            return nhaCungCapDAL.HasProductsInSanPham(brandID);
        }
        public List<SanPham> GetSanPhamByNhaCungCapIDBLL(int nhaCungCapID)
        {
            return sanPhamDAL.GetSanPhamByNhaCungCapID(nhaCungCapID);
        }

        public List<NhaCungCap> GetSuppliersByProductIDBLL(int sanPhamID)
        {
            return nhaCungCapDAL.GetSuppliersByProductID(sanPhamID);
        }

        public bool AddSupplierForProductBLL(int sanPhamID, int nhaCungCapID)
        {
            return nhaCungCapDAL.AddSupplierForProduct(sanPhamID, nhaCungCapID);
        }

        public bool RemoveSupplierFromProductBLL(int sanPhamID, int nhaCungCapID)
        {
            return nhaCungCapDAL.RemoveSupplierFromProduct(sanPhamID, nhaCungCapID);
        }


    }
}
