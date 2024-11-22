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

        public void AddSize(Size size)
        {
            doAnKetMon_UDTM.Sizes.InsertOnSubmit(size);
            doAnKetMon_UDTM.SubmitChanges();
        }

        public void DeleteSize(int sizeId)
        {
            var sizeToDelete = doAnKetMon_UDTM.Sizes.SingleOrDefault(s => s.SizeID == sizeId);
            if (sizeToDelete != null)
            {
                doAnKetMon_UDTM.Sizes.DeleteOnSubmit(sizeToDelete);
                doAnKetMon_UDTM.SubmitChanges();
            }
        }

        public void UpdateSize(Size updatedSize)
        {
            var existingSize = doAnKetMon_UDTM.Sizes.SingleOrDefault(s => s.SizeID == updatedSize.SizeID);
            if (existingSize != null)
            {
                existingSize.TenSize = updatedSize.TenSize;
                doAnKetMon_UDTM.SubmitChanges();
            }
        }

    }
}
