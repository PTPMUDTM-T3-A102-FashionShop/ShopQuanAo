using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanQuanAo.Models;
using WebsiteBanQuanAo.Filters;
using WebsiteBanQuanAo.KNN;

namespace WebsiteBanQuanAo.Controllers
{
    [UserAuthorization]
    public class ProductController : Controller
    {
        ShopQuanAoEntities db = new ShopQuanAoEntities();

        public ActionResult Index(int? danhmucid, string search = "", string SortColumn = "Price", string IconClass = "fa-sort-asc", int page = 1, int dotuoi = 0, string gioitinh = "", string sothich = "", decimal mucchitieu = 0, bool trangthaigoiy = false)
        {
            LoadKM();
            var authCookie = Request.Cookies["auth"];
            string tenDangNhap = authCookie?.Value;

            var lstsp = db.ChiTietSanPhams.Where(row =>
            row.SanPham.TenSanPham.Contains(search) &&
            row.SoLuongTonKho > 0 &&
            (row.KichHoat ?? false) // Nếu KichHoat là null, coi như là false
                ).ToList();


            if (danhmucid.HasValue)
            {
                lstsp = lstsp.Where(row => row.SanPham.DanhMucID == danhmucid).ToList();
            }

            lstsp = lstsp
                .GroupBy(row => row.SanPham.SanPhamID)
                .Select(group => group.OrderBy(row => row.Gia).FirstOrDefault())
                .ToList();

            ViewBag.trangthaigoiy = trangthaigoiy;

            List<DanhMuc> lstdm = db.DanhMucs.ToList();
            List<SanPham> lstsp2 = db.SanPhams.ToList();

            ViewBag.sp = lstsp2;
            ViewBag.dm = lstdm;
            ViewBag.search = search;
            ViewBag.SortColumn = SortColumn;
            ViewBag.IconClass = IconClass;

            if (SortColumn == "Price")
            {
                lstsp = IconClass == "asc" ? lstsp.OrderBy(row => row.Gia).ToList() : lstsp.OrderByDescending(row => row.Gia).ToList();
            }
            else if (SortColumn == "Name")
            {
                lstsp = IconClass == "asc" ? lstsp.OrderBy(row => row.SanPham.TenSanPham).ToList() : lstsp.OrderByDescending(row => row.SanPham.TenSanPham).ToList();
            }

            int NoOfRecordPerPage = 9;
            int NoOfPages = (int)Math.Ceiling(lstsp.Count() / (double)NoOfRecordPerPage);
            int NoOfRecordToSkip = (page - 1) * NoOfRecordPerPage;

            ViewBag.Page = page;
            ViewBag.NoOfPages = NoOfPages;
            lstsp = lstsp.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();

            if (!string.IsNullOrEmpty(tenDangNhap))
            {
                var user = db.NguoiDungs.FirstOrDefault(u => u.TenDangNhap == tenDangNhap);
                if (user != null)
                {
                    var cart = db.GioHangs.Where(g => g.NguoiDungID == user.NguoiDungID).ToList();
                    int totalQuantity = cart.Sum(item => item.SoLuong);
                    ViewBag.SLSP = totalQuantity;
                }
            }
            else
            {
                ViewBag.SLSP = 0;
            }

            ViewBag.dmid = danhmucid;
            return View(lstsp);
        }

        public ActionResult Details(int id)
        {
            LoadKM();
            var authCookie = Request.Cookies["auth"];
            string tenDangNhap = authCookie?.Value;

            var pro = db.ChiTietSanPhams.Where(x => x.SanPhamID == id).ToList();

            if (pro == null || !pro.Any())
            {
                return HttpNotFound("Không tìm thấy sản phẩm hoặc sản phẩm không khả dụng.");
            }

            if (!string.IsNullOrEmpty(tenDangNhap))
            {
                var user = db.NguoiDungs.FirstOrDefault(u => u.TenDangNhap == tenDangNhap);
                if (user != null)
                {
                    var cart = db.GioHangs.Where(g => g.NguoiDungID == user.NguoiDungID).ToList();
                    int totalQuantity = cart.Sum(item => item.SoLuong);
                    ViewBag.SLSP = totalQuantity;
                }
            }
            else
            {
                ViewBag.SLSP = 0;
            }

            ViewBag.sanpham = pro.OrderBy(x => x.Gia).FirstOrDefault();
            ViewBag.phanhoi = db.PhanHois.Where(x => x.SanPhamID == id).ToList();

            return View(pro);
        }

        [HttpPost]
        public ActionResult UpdateOptions(int? sizeID, int? mauID, int sanPhamID)
        {
            var gia = db.ChiTietSanPhams
                        .Where(x => x.SanPhamID == sanPhamID &&
                                    (sizeID == null || x.SizeID == sizeID) &&
                                    (mauID == null || x.MauID == mauID))
                        .Select(x => x.Gia)
                        .FirstOrDefault();

            var availableColors = db.ChiTietSanPhams
                                    .Where(x => x.SanPhamID == sanPhamID && (sizeID == null || x.SizeID == sizeID))
                                    .Select(x => x.Mau)
                                    .Distinct()
                                    .ToList();

            var availableSizes = db.ChiTietSanPhams
                                   .Where(x => x.SanPhamID == sanPhamID && (mauID == null || x.MauID == mauID))
                                   .Select(x => x.Size)
                                   .Distinct()
                                   .ToList();

            ViewBag.Gia = gia;
            ViewBag.AvailableColors = availableColors;
            ViewBag.AvailableSizes = availableSizes;

            return View("ChiTiet", db.ChiTietSanPhams.Where(x => x.SanPhamID == sanPhamID).ToList());
        }

        public void LoadKM()
        {
            var lstctkm = db.ChiTietKhuyenMais.ToList();
            var lstsp = db.ChiTietSanPhams.ToList();
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

        [HttpGet]
        public JsonResult GetStock(int SizeID, int colorID, int productID)
        {
            var chiTietSanPham = db.ChiTietSanPhams
                .FirstOrDefault(ct => ct.SizeID == SizeID && ct.MauID == colorID && ct.SanPhamID == productID);

            return Json(new
            {
                soLuongTonKho = chiTietSanPham?.SoLuongTonKho ?? 0
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetPrice(int SizeID, int colorID, int productID)
        {
            var chiTietSanPham = db.ChiTietSanPhams
                .FirstOrDefault(c => c.SizeID == SizeID && c.MauID == colorID && c.SanPhamID == productID);

            if (chiTietSanPham != null)
            {
                return Json(new
                {
                    gia = chiTietSanPham.Gia - chiTietSanPham.GiaDuocGiam,
                    giaduocgiam = chiTietSanPham.GiaDuocGiam,
                    chiTietSanPhamGia = chiTietSanPham.Gia
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                gia = 0,
                giaduocgiam = 0,
                chiTietSanPhamGia = 0
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
