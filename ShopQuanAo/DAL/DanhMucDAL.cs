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

        public bool AddDanhMuc(DanhMuc newCategory)
        {
            try
            {
                qldm.DanhMucs.InsertOnSubmit(newCategory);
                qldm.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần thiết
                return false;
            }
        }
        public bool UpdateDanhMuc(int danhMucID, string newTenDanhMuc)
        {
            try
            {
                var category = qldm.DanhMucs.SingleOrDefault(dm => dm.DanhMucID == danhMucID);
                if (category != null)
                {
                    category.TenDanhMuc = newTenDanhMuc;
                    qldm.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần thiết
                return false;
            }
        }

        public bool DeleteDanhMuc(int danhMucID)
        {
            try
            {
                var category = qldm.DanhMucs.SingleOrDefault(dm => dm.DanhMucID == danhMucID);
                if (category != null)
                {
                    qldm.DanhMucs.DeleteOnSubmit(category);
                    qldm.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần thiết
                return false;
            }
        }

        public bool HasProducts(int danhMucID)
        {
            return qldm.SanPhams.Any(sp => sp.DanhMucID == danhMucID);
        }

    }
}
