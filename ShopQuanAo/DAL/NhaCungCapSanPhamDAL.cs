using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class NhaCungCapSanPhamDAL
    {
        DoAnKetMon_UDTMDataContext doAnKetMon_UDTM = new DoAnKetMon_UDTMDataContext();

        public NhaCungCapSanPhamDAL() { }
        public void DeleteBySanPhamID(int sanPhamID)
        {
            var records = doAnKetMon_UDTM.NhaCungCapSanPhams
                                    .Where(nccsp => nccsp.SanPhamID == sanPhamID)
                                    .ToList();
            foreach (var record in records)
            {
                doAnKetMon_UDTM.NhaCungCapSanPhams.DeleteOnSubmit(record);
            }
            doAnKetMon_UDTM.SubmitChanges();
        }
    }
}
