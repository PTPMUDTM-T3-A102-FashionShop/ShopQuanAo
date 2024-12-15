using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebsiteBanQuanAo.KNN;
using WebsiteBanQuanAo.Filters;
using WebsiteBanQuanAo.Models;

namespace WebsiteBanQuanAo.Controllers
{
    [UserAuthorization]
    public class HomeController : Controller
    {
        private readonly ShopQuanAoEntities _db = new ShopQuanAoEntities();

        private void CapNhatKhuyenMai()
        {
            var danhSachKhuyenMai = _db.ChiTietKhuyenMais.ToList();
            var danhSachSanPham = _db.ChiTietSanPhams.ToList();

            foreach (var chiTiet in danhSachKhuyenMai)
            {
                if (chiTiet.KhuyenMai.NgayKetThuc <= DateTime.Now && chiTiet.DaHetHan != true)
                {
                    chiTiet.DaHetHan = true;

                    foreach (var sanPham in danhSachSanPham.Where(sp => sp.SanPhamID == chiTiet.SanPhamID))
                    {
                        sanPham.GiaDuocGiam -= chiTiet.KhuyenMai.MucGiam * 0.01m * sanPham.Gia;
                    }
                }
            }
        }

        public ActionResult Index()
        {
            List<NguoiDung> ktra = _db.NguoiDungs.Where(x => x.PhanKhucKH == "Người trẻ tiết kiệm").ToList();
            if(ktra.Count == 0)
            {
                var knn = new KNNClassifier();
                knn.LoadTrainingDataFromFile();
                knn.LoadTrainingDataFromDatabase();
            }    
           

            CapNhatKhuyenMai();

            var authCookie = Request.Cookies["auth"];
            var tenDangNhap = authCookie?.Value;

            var danhSachSanPham = new List<ChiTietSanPham>();
            var gioHang = new List<GioHang>();
            var tongSoLuong = 0;

            if (!string.IsNullOrEmpty(tenDangNhap))
            {
                var nguoiDung = _db.NguoiDungs.FirstOrDefault(nd => nd.TenDangNhap == tenDangNhap);

                if (nguoiDung != null)
                {
                    gioHang = _db.GioHangs.Where(gh => gh.NguoiDungID == nguoiDung.NguoiDungID).ToList();
                    tongSoLuong = gioHang.Sum(gh => gh.SoLuong);
                    danhSachSanPham = LaySanPhamPhuHop(nguoiDung.PhanKhucKH, nguoiDung.SoThich, nguoiDung.GioiTinh);
                }
            }

            if (!danhSachSanPham.Any())
            {
                danhSachSanPham = _db.ChiTietSanPhams
                    .Where(sp => sp.SanPham.SoSaoTB >= 4)
                    .OrderByDescending(sp => sp.SanPham.SoLuongDaBan)
                    .Take(30)
                    .ToList();
            }

            danhSachSanPham = danhSachSanPham
                .GroupBy(sp => sp.SanPham.SanPhamID)
                .Select(gr => gr.OrderBy(sp => sp.Gia).First())
                .ToList();

            ViewBag.SLSP = tongSoLuong;
            ViewBag.SanPhamLienQuan = danhSachSanPham;

            var sanPhamGiamGia = _db.ChiTietSanPhams
                .Where(sp => sp.GiaDuocGiam > 0)
                .GroupBy(sp => sp.SanPham.SanPhamID)
                .Select(gr => gr.OrderBy(sp => sp.Gia).FirstOrDefault())
                .ToList();


            ViewBag.sanPhamGiams = sanPhamGiamGia;

            return View(danhSachSanPham);
        }

        private List<ChiTietSanPham> LaySanPhamPhuHop(string phanKhucKH, string soThich, string gioiTinh)
        {
            var (giaMin, giaMax) = XacDinhKhoangGia(phanKhucKH);
            var tuKhoaDoTuoi = XacDinhTuKhoaTuoi(phanKhucKH);

            var querySanPham = _db.ChiTietSanPhams
                .Where(sp => sp.Gia >= giaMin && sp.Gia < giaMax && sp.SoLuongTonKho > 0);

            if (!string.IsNullOrEmpty(tuKhoaDoTuoi))
            {
                querySanPham = querySanPham
                    .Where(sp => sp.SanPham.MoTa.ToLower().Contains(tuKhoaDoTuoi.ToLower()));
            }

            return querySanPham.Distinct().Take(9).ToList();
        }

        private (int, int) XacDinhKhoangGia(string phanKhucKH)
        {
            switch (phanKhucKH)
            {
                case "Người trẻ tiết kiệm":
                case "Người trung niên tiết kiệm":
                case "Người cao tuổi tiết kiệm":
                    return (150000, 400000);

                case "Người trẻ chi tiêu cân đối":
                case "Người trung niên chi tiêu cân đối":
                case "Người cao tuổi chi tiêu cân đối":
                    return (500000, 750000);

                case "Người trẻ chi tiêu mạnh tay":
                case "Người trung niên chi tiêu mạnh tay":
                case "Người cao tuổi chi tiêu mạnh tay":
                    return (800000, int.MaxValue);

                default:
                    return (0, 0);
            }
        }


        private string XacDinhTuKhoaTuoi(string phanKhucKH)
        {
            if (phanKhucKH.Contains("Người trẻ")) return "người trẻ";
            if (phanKhucKH.Contains("Người trung niên")) return "trung niên";
            if (phanKhucKH.Contains("Người cao tuổi")) return "cao tuổi";
            return string.Empty;
        }
    }
}
