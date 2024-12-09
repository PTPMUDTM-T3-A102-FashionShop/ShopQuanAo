using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanQuanAo.Filters;
using WebsiteBanQuanAo.Models;
using WebsiteBanQuanAo.KNN;
namespace WebsiteBanQuanAo.Controllers
{
    [UserAuthorization]
    public class HomeController : Controller
    {
        ShopQuanAoEntities db = new ShopQuanAoEntities();
        public void LoadKM()
        {
            List<ChiTietKhuyenMai> lstctkm = db.ChiTietKhuyenMais.ToList();
            List<ChiTietSanPham> lstsp = db.ChiTietSanPhams.ToList();
            foreach (var a in lstctkm)
            {
                if (a.KhuyenMai.NgayKetThuc <= DateTime.Now && a.DaHetHan != true)
                {
                    a.DaHetHan = true;
                    foreach (var sp in lstsp)
                    {
                        if (a.SanPhamID == sp.SanPhamID)
                        {
                            sp.GiaDuocGiam -= (a.KhuyenMai.MucGiam * (decimal)0.01 * sp.Gia);
                        }
                    }
                }
            }
        }
        // GET: Home
        public ActionResult Index()
        {

            //PhanLoaiKNN kn = new PhanLoaiKNN();
            //kn.DocDuLieuHuanLuyen();
            //kn.DocDuLieuTuCSDL();
            LoadKM();
            var authCookie = Request.Cookies["auth"];
            string tenDangNhap = authCookie != null ? authCookie.Value : null;
            List<ChiTietSanPham> sanPhams = new List<ChiTietSanPham>();
            List<GioHang> cart = new List<GioHang>();
            int totalQuantity = 0;
            if (!string.IsNullOrEmpty(tenDangNhap))
            {
                NguoiDung kh = db.NguoiDungs.FirstOrDefault(u => u.TenDangNhap == tenDangNhap);
                if (kh != null)
                {
                    cart = db.GioHangs.Where(g => g.NguoiDungID == kh.NguoiDungID).ToList();
                    totalQuantity = cart.Sum(item => item.SoLuong);
                    sanPhams = LaySanPhamTheoPhanKhucVaSoThich(kh.PhanKhucKH, kh.SoThich, kh.GioiTinh);
                }
            }
            List<ChiTietSanPham> sps = new List<ChiTietSanPham>();
            if (sanPhams.Count == 0)
            {
                sps = db.ChiTietSanPhams.Where(x => x.SanPham.SoSaoTB >= 4).OrderByDescending(x => x.SanPham.SoLuongDaBan).Take(30).ToList();
                sanPhams = sps;
            }
            sanPhams = sanPhams
             .GroupBy(row => row.SanPham.SanPhamID)
             .Select(group => group.OrderBy(row => row.Gia).FirstOrDefault())
             .ToList();
            ViewBag.SLSP = totalQuantity;
            ViewBag.SanPhamLienQuan = sanPhams;
            List<ChiTietSanPham> sanPhamGiams = db.ChiTietSanPhams
               .Where(x => x.GiaDuocGiam != 0)
               .ToList();
            sanPhamGiams = sanPhamGiams
           .GroupBy(row => row.SanPham.SanPhamID)
           .Select(group => group.OrderBy(row => row.Gia).FirstOrDefault())
           .ToList();
            ViewBag.sanPhamGiams = sanPhamGiams;
            return View(sanPhams);
        }


        public List<ChiTietSanPham> LaySanPhamTheoPhanKhucVaSoThich(string phanKhucKH, string soThich, string gioiTinh)
        {
            // Lấy giá tối thiểu và tối đa dựa trên phân khúc
            int giaMin = LayGiaTuPhanKhuc(phanKhucKH, true);
            int giaMax = LayGiaTuPhanKhuc(phanKhucKH, false);

            // Lấy từ khóa độ tuổi từ phân khúc
            string tuKhoaDoTuoi = LayTuKhoaDoTuoi(phanKhucKH);

            var sanPhamsQuery = db.ChiTietSanPhams
                .Where(sp =>
                    sp.Gia >= giaMin &&
                    sp.Gia < giaMax &&
                    sp.SoLuongTonKho > 0); // Lọc theo mức chi tiêu và tồn kho

            // Lọc sản phẩm theo từ khóa độ tuổi (phân khúc)
            if (!string.IsNullOrEmpty(tuKhoaDoTuoi))
            {
                sanPhamsQuery = sanPhamsQuery
                    .Where(sp => sp.SanPham.MoTa.ToLower().Contains(tuKhoaDoTuoi.ToLower()));
            }

            // Trả về danh sách các sản phẩm lọc xong
            return sanPhamsQuery.Distinct().Take(9).ToList();
        }

        // Hàm lấy giá tối thiểu và tối đa dựa trên phân khúc
        private int LayGiaTuPhanKhuc(string phanKhucKH, bool isMin)
        {
            switch (phanKhucKH)
            {
                case "Người trẻ tiết kiệm":
                case "Người trung niên tiết kiệm":
                case "Người cao tuổi tiết kiệm":
                    return isMin ? 150000 : 400000;

                case "Người trẻ chi tiêu cân đối":
                case "Người trung niên chi tiêu cân đối":
                case "Người cao tuổi chi tiêu cân đối":
                    return isMin ? 500000 : 750000;

                case "Người trẻ chi tiêu mạnh tay":
                case "Người trung niên chi tiêu mạnh tay":
                case "Người cao tuổi chi tiêu mạnh tay":
                    return isMin ? 800000 : int.MaxValue;

                default:
                    return 0;
            }
        }
        private string LayTuKhoaDoTuoi(string phanKhucKH)
        {
            if (phanKhucKH.Contains("Người trẻ"))
                return "người trẻ";
            if (phanKhucKH.Contains("Người trung niên"))
                return "trung niên";
            if (phanKhucKH.Contains("Người cao tuổi"))
                return "cao tuổi";
            return string.Empty;
        }
    }
}
