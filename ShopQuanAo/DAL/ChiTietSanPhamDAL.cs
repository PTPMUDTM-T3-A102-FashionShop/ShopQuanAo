using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ChiTietSanPhamDAL
    {
        DoAnKetMon_UDTMDataContext doAnKetMon_UDTM = new DoAnKetMon_UDTMDataContext();

        public ChiTietSanPhamDAL() {  }
        public void DeleteBySanPhamID(int sanPhamID)
        {
            var records = doAnKetMon_UDTM.ChiTietSanPhams
                                    .Where(ctsp => ctsp.SanPhamID == sanPhamID)
                                    .ToList();
            foreach (var record in records)
            {
                doAnKetMon_UDTM.ChiTietSanPhams.DeleteOnSubmit(record);
            }
            doAnKetMon_UDTM.SubmitChanges();
        }
    }
}
