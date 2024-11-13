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
    }
}
