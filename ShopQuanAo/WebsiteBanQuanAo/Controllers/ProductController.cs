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
        // GET: Product
        ShopQuanAoEntities db = new ShopQuanAoEntities();
        public ActionResult Index(int? danhmucid, string search = "", string SortColumn = "Price", string IconClass = "fa-sort-asc", int page = 1, int dotuoi = 0, string gioitinh = "", string sothich = "", decimal mucchitieu = 0, bool trangthaigoiy = false)
        {
            LoadKM();
            var authCookie = Request.Cookies["auth"];
            string tenDangNhap = authCookie != null ? authCookie.Value : null;
            List<ChiTietSanPham> lstsp = db.ChiTietSanPhams.ToList();
            if (danhmucid != null)
            {
                lstsp = db.ChiTietSanPhams
                    .Where(row =>
                        row.SanPham.TenSanPham.Contains(search) && // Tên sản phẩm khớp với tìm kiếm
                        row.SoLuongTonKho > 0 &&                   // Sản phẩm còn tồn kho
                        row.KichHoat == true &&                    // Sản phẩm đã kích hoạt
                        row.SanPham.DanhMucID == danhmucid         // Nằm trong danh mục có ID == danhmucid
                    )
                    .GroupBy(row => row.SanPham.SanPhamID)
                    .Select(group => group.OrderBy(row => row.Gia).FirstOrDefault())
                    .ToList();
            }
            else
            {
                lstsp = db.ChiTietSanPhams
            .Where(row => row.SanPham.TenSanPham.Contains(search) && row.SoLuongTonKho > 0 && row.KichHoat == true)
            .GroupBy(row => row.SanPham.SanPhamID)
            .Select(group => group.OrderBy(row => row.Gia).FirstOrDefault())
            .ToList();
            }

            
                ViewBag.trangthaigoiy = false;
            
            //Kết thúc
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
            // Phân trang
            int NoOfRecordPerPage = 9;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(lstsp.Count) / Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordToSkip = (page - 1) * NoOfRecordPerPage;
            ViewBag.Page = page;
            ViewBag.NoOfPages = NoOfPages;
            lstsp = lstsp.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();
            if (!string.IsNullOrEmpty(tenDangNhap))
            {
                NguoiDung user = db.NguoiDungs.FirstOrDefault(u => u.TenDangNhap == tenDangNhap);
                if (user != null)
                {
                    List<GioHang> cart = db.GioHangs.Where(g => g.NguoiDungID == user.NguoiDungID).ToList();
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
            // Lấy cookie xác thực
            var authCookie = Request.Cookies["auth"];
            string tenDangNhap = authCookie != null ? authCookie.Value : null;

            // Lấy chi tiết sản phẩm (đảm bảo chỉ lấy sản phẩm đang hoạt động hoặc còn hàng)
            List<ChiTietSanPham> pro = db.ChiTietSanPhams.Where(x => x.SanPhamID == id).ToList();

            // Kiểm tra sản phẩm có tồn tại không
            if (pro == null || !pro.Any())
            {
                return HttpNotFound("Không tìm thấy sản phẩm hoặc sản phẩm không khả dụng.");
            }

            // Lấy thông tin người dùng nếu đã đăng nhập
            if (!string.IsNullOrEmpty(tenDangNhap))
            {
                NguoiDung user = db.NguoiDungs.FirstOrDefault(u => u.TenDangNhap == tenDangNhap);
                if (user != null)
                {
                    // Lấy giỏ hàng
                    List<GioHang> cart = db.GioHangs.Where(g => g.NguoiDungID == user.NguoiDungID).ToList();
                    int totalQuantity = cart.Sum(item => item.SoLuong);
                    ViewBag.SLSP = totalQuantity;
                }
            }
            else
            {
                ViewBag.SLSP = 0; // Không có sản phẩm trong giỏ
            }

            // Lấy sản phẩm chính và phản hồi liên quan
            ViewBag.sanpham = pro.OrderBy(x => x.Gia).FirstOrDefault(); // Sản phẩm giá thấp nhất
            ViewBag.phanhoi = db.PhanHois.Where(x => x.SanPhamID == id).ToList();

            // Trả về View với danh sách sản phẩm
            return View(pro);
        }

        [HttpPost]
        public ActionResult UpdateOptions(int? sizeID, int? mauID, int sanPhamID)
        {
            var db = new ShopQuanAoEntities();

            // Tìm giá theo Size và Màu
            var gia = db.ChiTietSanPhams
                        .Where(x => x.SanPhamID == sanPhamID &&
                                    (sizeID == null || x.SizeID == sizeID) &&
                                    (mauID == null || x.MauID == mauID))
                        .Select(x => x.Gia)
                        .FirstOrDefault();

            // Tìm các màu tương ứng với Size đã chọn
            var availableColors = db.ChiTietSanPhams
                                    .Where(x => x.SanPhamID == sanPhamID && (sizeID == null || x.SizeID == sizeID))
                                    .Select(x => x.Mau)
                                    .Distinct()
                                    .ToList();

            // Tìm các size tương ứng với Màu đã chọn
            var availableSizes = db.ChiTietSanPhams
                                   .Where(x => x.SanPhamID == sanPhamID && (mauID == null || x.MauID == mauID))
                                   .Select(x => x.Size)
                                   .Distinct()
                                   .ToList();

            // Truyền giá và các tùy chọn vào ViewBag
            ViewBag.Gia = gia;
            ViewBag.AvailableColors = availableColors;
            ViewBag.AvailableSizes = availableSizes;

            // Render lại view với các giá trị được cập nhật
            return View("ChiTiet", db.ChiTietSanPhams.Where(x => x.SanPhamID == sanPhamID).ToList());
        }








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
        [HttpGet]
        public JsonResult GetStock(int SizeID, int colorID, int productID)
        {
            // Lấy số lượng tồn kho từ database
            var chiTietSanPham = db.ChiTietSanPhams
                .FirstOrDefault(ct => ct.SizeID == SizeID && ct.MauID == colorID && ct.SanPhamID == productID);

            // Trả về số lượng tồn kho hoặc 0 nếu không tìm thấy
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
                    gia = chiTietSanPham.Gia - chiTietSanPham.GiaDuocGiam, // Giá sau khi giảm
                    giaduocgiam = chiTietSanPham.GiaDuocGiam,
                    chiTietSanPhamGia = chiTietSanPham.Gia // Giá gốc của sản phẩm
                }, JsonRequestBehavior.AllowGet);
            }

            // Trả về giá trị mặc định nếu không tìm thấy
            return Json(new
            {
                gia = 0,
                giaduocgiam = 0,
                chiTietSanPhamGia = 0
            }, JsonRequestBehavior.AllowGet);
        }





    }
}