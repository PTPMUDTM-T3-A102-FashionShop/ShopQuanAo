using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SizeBLL
    {
        SizeDAL sizeDAL = new SizeDAL();
        public SizeBLL() { }

        public List<Size> getAllSize()
        {
            return sizeDAL.getAllSize();
        }

        public void AddSize(Size size)
        {
            sizeDAL.AddSize(size);
        }

        public void DeleteSize(int sizeId)
        {
            sizeDAL.DeleteSize(sizeId);
        }

        public void UpdateSize(Size updatedSize)
        {
            sizeDAL.UpdateSize(updatedSize);
        }

    }
}
