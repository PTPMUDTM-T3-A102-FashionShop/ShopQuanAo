using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAL
{
    public class DanhMucDAL
    {
        DoAnKetMon_UDTMDataContext qldm = new DoAnKetMon_UDTMDataContext();
        public DanhMucDAL()
        { 
        
        }

        public List<DanhMuc> getAllDanhMucDAL()
        {

            return qldm.DanhMucs.Select(dm => dm).ToList<DanhMuc>();
        }
    }
}
