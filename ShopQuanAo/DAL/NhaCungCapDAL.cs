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
                var productsCount = doAnKetMon_UDTM.NhaCungCapSanPhams.Count(spnc => spnc.NhaCungCapID == brandID);
                return productsCount > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<NhaCungCap> GetSuppliersByProductID(int sanPhamID)
        {
            return (from ncc in doAnKetMon_UDTM.NhaCungCaps
                    join nccsp in doAnKetMon_UDTM.NhaCungCapSanPhams
                    on ncc.NhaCungCapID equals nccsp.NhaCungCapID
                    where nccsp.SanPhamID == sanPhamID
                    select ncc).ToList();
        }

        public bool AddSupplierForProduct(int sanPhamID, int nhaCungCapID)
        {
            try
            {
                // Kiểm tra xem đã tồn tại hay chưa
                var exists = doAnKetMon_UDTM.NhaCungCapSanPhams
                                            .Any(nccsp => nccsp.SanPhamID == sanPhamID && nccsp.NhaCungCapID == nhaCungCapID);

                if (exists) return false;

                // Thêm mới
                var newRelation = new NhaCungCapSanPham
                {
                    SanPhamID = sanPhamID,
                    NhaCungCapID = nhaCungCapID
                };

                doAnKetMon_UDTM.NhaCungCapSanPhams.InsertOnSubmit(newRelation);
                doAnKetMon_UDTM.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveSupplierFromProduct(int sanPhamID, int nhaCungCapID)
        {
            try
            {
                var relation = doAnKetMon_UDTM.NhaCungCapSanPhams
                                              .SingleOrDefault(nccsp => nccsp.SanPhamID == sanPhamID && nccsp.NhaCungCapID == nhaCungCapID);

                if (relation != null)
                {
                    doAnKetMon_UDTM.NhaCungCapSanPhams.DeleteOnSubmit(relation);
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

    }
}
