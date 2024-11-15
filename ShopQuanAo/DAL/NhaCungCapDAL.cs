using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class NhaCungCapDAL
    {
        DoAnKetMon_UDTMDataContext doAnKetMon_UDTM = new DoAnKetMon_UDTMDataContext();
        public NhaCungCapDAL() { }

        public List<NhaCungCap> getAllNhaCungCapDAL()
        {
            return doAnKetMon_UDTM.NhaCungCaps.ToList();
        }

        public bool AddNhaCungCap(NhaCungCap newBrand)
        {
            try
            {
                doAnKetMon_UDTM.NhaCungCaps.InsertOnSubmit(newBrand);
                doAnKetMon_UDTM.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateNhaCungCap(int brandID, string newTenNhaCungCap, string newEmail, string newSoDienThoai, string newDiaChi, string newMoTa)
        {
            try
            {
                var brand = doAnKetMon_UDTM.NhaCungCaps.SingleOrDefault(ncc => ncc.NhaCungCapID == brandID);
                if (brand != null)
                {
                    brand.TenNhaCungCap = newTenNhaCungCap;
                    brand.Email = newEmail;
                    brand.SoDienThoai = newSoDienThoai;
                    brand.DiaChi = newDiaChi;
                    brand.MoTa = newMoTa;
                    doAnKetMon_UDTM.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool DeleteNhaCungCap(int brandID)
        {
            try
            {
                var brand = doAnKetMon_UDTM.NhaCungCaps.SingleOrDefault(ncc => ncc.NhaCungCapID == brandID);
                if (brand != null)
                {
                    doAnKetMon_UDTM.NhaCungCaps.DeleteOnSubmit(brand);
                    doAnKetMon_UDTM.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool HasProductsInSanPham(int brandID)
        {
            try
            {
                var productsCount = doAnKetMon_UDTM.SanPhams.Count(sp => sp.NhaCungCapID == brandID);
                return productsCount > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
