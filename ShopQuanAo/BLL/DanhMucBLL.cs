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
            return danhMucDAL.DeleteDanhMuc(danhMucID);
        }

    }
}
