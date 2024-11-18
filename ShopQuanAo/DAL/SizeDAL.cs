using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SizeDAL
    {
        DoAnKetMon_UDTMDataContext doAnKetMon_UDTM = new DoAnKetMon_UDTMDataContext();

        public SizeDAL() { }

        public List<Size> getAllSize()
        {
            return doAnKetMon_UDTM.Sizes.Select(size => size).ToList<Size>();
        }
    }
}
