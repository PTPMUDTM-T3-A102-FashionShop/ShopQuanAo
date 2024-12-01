using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ChiTietSanPhamDAL
    {
        DoAnKetMon_UDTMDataContext doAnKetMon_UDTM = new DoAnKetMon_UDTMDataContext();
        SizeDAL sizeDAL = new SizeDAL();
        MauDAL mauDAL = new MauDAL();
        public ChiTietSanPhamDAL() {  }
        public void DeleteBySanPhamID(int sanPhamID)
        {
            var records = doAnKetMon_UDTM.ChiTietSanPhams
                                    .Where(ctsp => ctsp.SanPhamID == sanPhamID)
                                    .ToList();
            foreach (var record in records)
            {
                doAnKetMon_UDTM.ChiTietSanPhams.DeleteOnSubmit(record);
            }
            doAnKetMon_UDTM.SubmitChanges();
        }
        public List<ChiTietSanPham> getChiTietBySanPhamID(int sanPhamID)
        {
            return doAnKetMon_UDTM.ChiTietSanPhams.Where(ctsp=>ctsp.SanPhamID == sanPhamID).ToList();
        }
        public bool AddChiTietSanPham(ChiTietSanPham chiTietSanPham)
        {
            try
            {
                doAnKetMon_UDTM.ChiTietSanPhams.InsertOnSubmit(chiTietSanPham); // Thêm chi tiết sản phẩm vào DbSet
                doAnKetMon_UDTM.SubmitChanges(); // Lưu vào cơ sở dữ liệu
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm chi tiết sản phẩm vào CSDL", ex);
            }
        }
        public bool UpdateChiTietSanPham(ChiTietSanPham chiTietSanPham)
        {
            try
            {
                var record = doAnKetMon_UDTM.ChiTietSanPhams.SingleOrDefault(ctsp => ctsp.ChiTietID == chiTietSanPham.ChiTietID);
                if (record != null)
                {
                    record.Gia = chiTietSanPham.Gia;
                    record.SoLuongTonKho = chiTietSanPham.SoLuongTonKho;
                    record.MauID = chiTietSanPham.MauID;
                    record.SizeID = chiTietSanPham.SizeID;
                    record.HinhAnhUrl = chiTietSanPham.HinhAnhUrl;
                    doAnKetMon_UDTM.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi (nếu có)
                throw new Exception("Lỗi khi cập nhật chi tiết sản phẩm", ex);
            }
        }
        public bool DeleteChiTietSanPham(int ChiTietID)
        {
            try
            {
                var record = doAnKetMon_UDTM.ChiTietSanPhams.SingleOrDefault(ctsp => ctsp.ChiTietID == ChiTietID);
                if (record != null)
                {
                    doAnKetMon_UDTM.ChiTietSanPhams.DeleteOnSubmit(record); // Đánh dấu bản ghi để xóa
                    doAnKetMon_UDTM.SubmitChanges(); // Thực hiện xóa trong cơ sở dữ liệu
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa chi tiết sản phẩm trong CSDL", ex);
            }
        }

    }
}
