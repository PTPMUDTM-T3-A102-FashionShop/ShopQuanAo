using WebsiteBanQuanAo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteBanQuanAo.Controllers
{
    public class RateController : Controller
    {
        ShopQuanAoEntities db = new ShopQuanAoEntities();

        public ActionResult Index(int id)
        {
            var order = db.DonHangs.FirstOrDefault(x => x.DonHangID == id);
            if (order != null)
            {
                order.TinhTrangDonHang = "Hoàn Thành";
                db.SaveChanges();
            }

            ViewBag.iddonhang = id;

            if (order == null)
            {
                return HttpNotFound("Không tìm thấy đơn hàng.");
            }

            var orderDetails = db.ChiTietDonHangs
                                  .Where(x => x.DonHangID == id && (x.TinhTrangDanhGia == null || x.TinhTrangDanhGia == 0))
                                  .ToList();

            return View(orderDetails);
        }

        public ActionResult Rate(int id, int iddonhang)
        {
            var productDetail = db.ChiTietSanPhams.FirstOrDefault(x => x.ChiTietID == id);
            if (productDetail == null)
            {
                return HttpNotFound("Sản phẩm không tồn tại.");
            }

            ViewBag.iddonhang = iddonhang;
            return View(productDetail);
        }

        [HttpPost]
        public ActionResult Rate(int id, int danhGia, string noiDung, int iddonhang)
        {
            var orderDetail = db.ChiTietDonHangs
                                 .Where(x => x.SanPhamID == id && x.DonHangID == iddonhang)
                                 .FirstOrDefault();

            if (orderDetail != null)
            {
                orderDetail.TinhTrangDanhGia = 1;
                db.SaveChanges();
            }

            id = orderDetail?.ChiTietSanPham?.SanPham?.SanPhamID ?? 0;
            var product = db.SanPhams.FirstOrDefault(x => x.SanPhamID == id);
            if (product == null)
            {
                return HttpNotFound("Sản phẩm không tồn tại.");
            }

            int userId = GetCurrentUserId();

            var feedback = new PhanHoi
            {
                SanPhamID = id,
                NguoiDungID = userId,
                NoiDung = noiDung,
                DanhGia = danhGia,
                NgayPhanHoi = DateTime.Now
            };

            db.PhanHois.Add(feedback);

            var avgRating = db.PhanHois
                              .Where(x => x.SanPhamID == id)
                              .Average(x => (double?)x.DanhGia);

            if (product.SoSaoTB == null || product.SoSaoTB == 0)
            {
                product.SoSaoTB = danhGia;
            }
            else
            {
                product.SoSaoTB = (int)Math.Round(avgRating.Value);
            }

            db.SaveChanges();
            return RedirectToAction("Index", new { id = iddonhang });
        }

        private int GetCurrentUserId()
        {
            var authCookie = Request.Cookies["auth"];
            if (authCookie != null)
            {
                string userName = authCookie.Value;
                var user = db.NguoiDungs.FirstOrDefault(u => u.TenDangNhap == userName);
                return user?.NguoiDungID ?? 0;
            }
            return 0;
        }

        private ActionResult CheckUserLoggedIn()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            return null;
        }
    }
}
