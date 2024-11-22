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

        public void AddMau(Mau mau)
        {
            doAnKetMon_UDTM.Maus.InsertOnSubmit(mau);
            doAnKetMon_UDTM.SubmitChanges();
        }

        public void DeleteMau(int mauId)
        {
            var mauToDelete = doAnKetMon_UDTM.Maus.SingleOrDefault(m => m.MauID == mauId);
            if (mauToDelete != null)
            {
                doAnKetMon_UDTM.Maus.DeleteOnSubmit(mauToDelete);
                doAnKetMon_UDTM.SubmitChanges();
            }
        }

        public void UpdateMau(Mau updatedMau)
        {
            var existingMau = doAnKetMon_UDTM.Maus.SingleOrDefault(m => m.MauID == updatedMau.MauID);
            if (existingMau != null)
            {
                existingMau.TenMau = updatedMau.TenMau;
                doAnKetMon_UDTM.SubmitChanges();
            }
        }

    }
}
