using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
namespace BLL
{
    public class DanhMucBLL
    {
        DanhMucDAL danhMucDAL = new DanhMucDAL();
        SanPhamDAL sanPhamDAL = new SanPhamDAL();

        public DanhMucBLL()
        { 
        
        }

        public List<DanhMuc> getAllDanhMucBLL() {
            return danhMucDAL.getAllDanhMucDAL();
        }
        public bool AddDanhMuc(DanhMuc newCategory)
        {
            return danhMucDAL.AddDanhMuc(newCategory);
        }

        public bool UpdateDanhMuc(int danhMucID, string newTenDanhMuc)
        {
            return danhMucDAL.UpdateDanhMuc(danhMucID, newTenDanhMuc);
        }

        public bool DeleteDanhMuc(int danhMucID)
        {
            if (danhMucDAL.HasProducts(danhMucID))
            {
                return false;
            }

            return danhMucDAL.DeleteDanhMuc(danhMucID);
        }

        public List<SanPham> GetSanPhamByDanhMucIDBLL(int danhMucID)
        {
            return sanPhamDAL.GetSanPhamByDanhMucID(danhMucID);
        }

    }
}
