using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB;
using System.Data.SqlClient;

namespace DAL
{
    public class SanPhamDAL
    {
        DoAnKetMon_UDTMDataContext doAnKetMon_UDTM = new DoAnKetMon_UDTMDataContext();
        NhaCungCapSanPhamDAL nhaCungCapSanPhamDAL = new NhaCungCapSanPhamDAL();
        ChiTietSanPhamDAL chiTietSanPhamDAL = new ChiTietSanPhamDAL();
        private DBConnection db = new DBConnection();
        public SanPhamDAL() { }
        public List<SanPham> GetAllSanPham()
        {
            return doAnKetMon_UDTM.SanPhams.Select(sp => sp).ToList<SanPham>();
        }
        public List<SanPham> GetSanPhamByDanhMucID(int danhMucID)
        {
            return doAnKetMon_UDTM.SanPhams
                                  .Where(sp => sp.DanhMucID == danhMucID)
                                  .ToList();
        }

        public List<SanPham> GetSanPhamByNhaCungCapID(int nhaCungCapID)
        {
            return doAnKetMon_UDTM.NhaCungCapSanPhams
                        .Where(nccsp => nccsp.NhaCungCapID == nhaCungCapID)
                        .Join(doAnKetMon_UDTM.SanPhams,
                              nccsp => nccsp.SanPhamID,
                              sp => sp.SanPhamID,
                              (nccsp, sp) => sp)
                        .ToList();
        }

        public bool AddSanPham(SanPham sanPham)
        {
            try
            {
                doAnKetMon_UDTM.SanPhams.InsertOnSubmit(sanPham);
                doAnKetMon_UDTM.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu cần
                return false;
            }
        }

        // Cập nhật sản phẩm
        public bool UpdateSanPham(SanPham sanPham)
        {
            try
            {
                var existingSanPham = doAnKetMon_UDTM.SanPhams
                                              .FirstOrDefault(sp => sp.SanPhamID == sanPham.SanPhamID);
                if (existingSanPham != null)
                {
                    existingSanPham.TenSanPham = sanPham.TenSanPham;
                    existingSanPham.MoTa = sanPham.MoTa;
                    existingSanPham.DanhMucID = sanPham.DanhMucID;
                    existingSanPham.KichHoat = sanPham.KichHoat;
                    doAnKetMon_UDTM.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu cần
                return false;
            }
        }

        // Xóa sản phẩm
        public void DeleteSanPham(int sanPhamID)
        {
            nhaCungCapSanPhamDAL.DeleteBySanPhamID(sanPhamID);

            // Xóa các bản ghi liên quan trong bảng ChiTietSanPham
            chiTietSanPhamDAL.DeleteBySanPhamID(sanPhamID);

            // Xóa sản phẩm chính
            var sanPham = doAnKetMon_UDTM.SanPhams.SingleOrDefault(sp => sp.SanPhamID == sanPhamID);
            if (sanPham != null)
            {
                doAnKetMon_UDTM.SanPhams.DeleteOnSubmit(sanPham);
                doAnKetMon_UDTM.SubmitChanges();
            }
        }

        //Code thuan`

        public List<SanPhamChiTietDTO> GetSanPhamChiTiet()
        {
            List<SanPhamChiTietDTO> list = new List<SanPhamChiTietDTO>();
            string query = @"
                SELECT sp.SanPhamID, sp.TenSanPham, sp.MoTa, sp.KichHoat, m.TenMau, s.TenSize, ct.Gia, ct.SoLuongTonKho
                FROM SanPham sp
                JOIN ChiTietSanPham ct ON sp.SanPhamID = ct.SanPhamID
                JOIN Mau m ON ct.MauID = m.MauID
                JOIN Size s ON ct.SizeID = s.SizeID;";

            SqlCommand cmd = new SqlCommand(query, db.conn);
            db.conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                SanPhamChiTietDTO spct = new SanPhamChiTietDTO
                {
                    SanPhamID = (int)reader["SanPhamID"],
                    TenSanPham = reader["TenSanPham"].ToString(),
                    MoTa = reader["MoTa"].ToString(),
                    KichHoat = (bool)reader["KichHoat"],
                    TenMau = reader["TenMau"].ToString(),
                    TenSize = reader["TenSize"].ToString(),
                    Gia = (decimal)reader["Gia"],
                    SoLuongTonKho = (int)reader["SoLuongTonKho"]
                };
                list.Add(spct);
            }
            db.conn.Close();
            return list;
        }

    }
}
