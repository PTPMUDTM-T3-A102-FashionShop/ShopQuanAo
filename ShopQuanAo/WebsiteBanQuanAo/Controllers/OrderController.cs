using WebsiteBanQuanAo.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanQuanAo.Models;

namespace WebsiteBanQuanAo.Controllers
{
    [MyAuthenFilter]
    [UserAuthorization]
    public class OrderController : Controller
    {
        private readonly ShopQuanAoEntities db = new ShopQuanAoEntities();

        public ActionResult Index(int page = 1, string sortOrder = "desc")
        {
            var userId = GetUserIdIfLoggedIn();
            if (userId == 0) return RedirectToAction("Login", "User");

            var orders = db.DonHangs.Where(o => o.NguoiDungID == userId).ToList();
            var cart = db.GioHangs.Where(g => g.NguoiDungID == userId).ToList();
            var totalQuantity = cart.Sum(item => item.SoLuong);
            var pageSize = 5;
            var totalPages = (int)Math.Ceiling((double)orders.Count / pageSize);
            orders = SortOrders(orders, sortOrder);
            orders = PaginateOrders(orders, page, pageSize);

            ViewBag.SLSP = totalQuantity;
            ViewBag.Page = page;
            ViewBag.NoOfPages = totalPages;
            ViewBag.SortOrder = sortOrder;

            return View(orders);
        }

        public ActionResult Details(int id)
        {
            var userId = GetUserIdIfLoggedIn();
            if (userId == 0) return RedirectToAction("Login", "User");

            var cart = db.GioHangs.Where(g => g.NguoiDungID == userId).ToList();
            var totalQuantity = cart.Sum(item => item.SoLuong);

            var order = db.DonHangs.FirstOrDefault(o => o.DonHangID == id && o.NguoiDungID == userId);
            if (order == null) return HttpNotFound();

            var orderDetails = db.ChiTietDonHangs.Where(x => x.DonHangID == id).ToList();

            ViewBag.SLSP = totalQuantity;
            ViewBag.Order = order;
            return View(orderDetails);
        }

        public ActionResult Cancel(int id)
        {
            var order = db.DonHangs.Find(id);
            if (order != null && order.TinhTrangDonHang != "Đã Xác Nhận")
            {
                order.TinhTrangDonHang = "Đã Hủy";
                db.SaveChanges();
            }

            var orderDetails = db.ChiTietDonHangs.Where(x => x.DonHangID == id).ToList();
            foreach (var detail in orderDetails)
            {
                detail.ChiTietSanPham.SoLuongTonKho += detail.SoLuong;
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private int GetUserIdIfLoggedIn()
        {
            if (Session["UserID"] == null) return 0;

            var authCookie = Request.Cookies["auth"];
            if (authCookie == null) return 0;

            var user = db.NguoiDungs.FirstOrDefault(u => u.TenDangNhap == authCookie.Value);
            return user?.NguoiDungID ?? 0;
        }

        private List<DonHang> SortOrders(List<DonHang> orders, string sortOrder)
        {
            return sortOrder == "asc"
                ? orders.OrderBy(o => o.NgayDatHang).ToList()
                : orders.OrderByDescending(o => o.NgayDatHang).ToList();
        }

        private List<DonHang> PaginateOrders(List<DonHang> orders, int page, int pageSize)
        {
            return orders.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
