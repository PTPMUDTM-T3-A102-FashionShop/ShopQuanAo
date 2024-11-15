using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SanPhamDAL
    {
        DoAnKetMon_UDTMDataContext doAnKetMon_UDTM = new DoAnKetMon_UDTMDataContext();
        public SanPhamDAL() { }

        public List<SanPham> GetSanPhamByDanhMucID(int danhMucID)
        {
            return doAnKetMon_UDTM.SanPhams
                                  .Where(sp => sp.DanhMucID == danhMucID && sp.KichHoat == true)
                                  .ToList();
        }

        public List<SanPham> GetSanPhamByNhaCungCapID(int danhMucID)
        {
            return doAnKetMon_UDTM.SanPhams
                                  .Where(sp => sp.NhaCungCapID == danhMucID && sp.KichHoat == true)
                                  .ToList();
        }
    }
}
