using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MauDAL
    {
        DoAnKetMon_UDTMDataContext doAnKetMon_UDTM = new DoAnKetMon_UDTMDataContext();

        public MauDAL() { }

        public List<Mau> getAllMau()
        {
            return doAnKetMon_UDTM.Maus.Select(mau => mau).ToList<Mau>();
        }
    }
}
