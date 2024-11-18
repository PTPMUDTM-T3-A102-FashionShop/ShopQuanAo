using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BLL
{
    public class MauBLL
    {
        MauDAL mauDAL = new MauDAL();
        public MauBLL() {  }

        public List<Mau> getAllMau()
        {
            return mauDAL.getAllMau();
        }
    }
}
