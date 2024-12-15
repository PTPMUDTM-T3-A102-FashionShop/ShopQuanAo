using WebsiteBanQuanAo.Filters;
using WebsiteBanQuanAo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WebsiteBanQuanAo.Controllers
{
    [MyAuthenFilter]
    [UserAuthorization]
    public class CartController : Controller
    {
        private readonly ShopQuanAoEntities _dbContext = new ShopQuanAoEntities();

        private int GetCurrentUserId()
        {
            var authCookie = Request.Cookies["auth"];
            return authCookie != null
                ? _dbContext.NguoiDungs.Where(u => u.TenDangNhap == authCookie.Value).Select(u => u.NguoiDungID).FirstOrDefault()
                : 0;
        }

        private bool IsUserLoggedIn()
        {
            return Session["UserID"] != null;
        }

        public ActionResult Index()
        {
            if (!IsUserLoggedIn())
                return RedirectToAction("Login", "User");

            int userId = GetCurrentUserId();
            var cartItems = _dbContext.GioHangs.Where(g => g.NguoiDungID == userId && g.ChiTietSanPham.SoLuongTonKho > 0).ToList();

            if (!cartItems.Any())
            {
                ViewBag.SLSP = 0;
                ViewBag.Message = "Giỏ hàng của bạn trống hoặc sản phẩm trong giỏ đã hết hàng.";
                ViewBag.TotalPrice = 0;
                return View(cartItems);
            }

            ViewBag.SLSP = cartItems.Sum(item => item.SoLuong);
            ViewBag.TotalPrice = cartItems.Sum(item =>
                (item.ChiTietSanPham.Gia - (item.ChiTietSanPham.GiaDuocGiam ?? 0)) * item.SoLuong);

            if (TempData["ErrorMessage"] != null)
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();

            return View(cartItems);
        }

        public ActionResult Add(int? id, int? sizeID, int? colorID, string returnUrl)
        {
            if (!IsUserLoggedIn())
                return RedirectToAction("Login", "User");

            if (!id.HasValue || !sizeID.HasValue || !colorID.HasValue)
            {
                ModelState.AddModelError("", "Vui lòng chọn kích thước và màu sắc.");
                return RedirectToAction("Details", "Product", new { id });
            }

            int userId = GetCurrentUserId();
            var productDetail = _dbContext.ChiTietSanPhams.FirstOrDefault(p => p.SanPhamID == id && p.SizeID == sizeID && p.MauID == colorID);

            if (productDetail == null)
            {
                ModelState.AddModelError("", "Sản phẩm với kích thước và màu sắc này không tồn tại.");
                return RedirectToAction("Index", "Product");
            }

            var cartItem = _dbContext.GioHangs.FirstOrDefault(g => g.ChiTietSanPham.ChiTietID == productDetail.ChiTietID && g.NguoiDungID == userId);

            if (cartItem != null)
            {
                cartItem.SoLuong++;
            }
            else
            {
                _dbContext.GioHangs.Add(new GioHang
                {
                    SanPhamID = productDetail.ChiTietID,
                    SoLuong = 1,
                    NguoiDungID = userId
                });
            }

            _dbContext.SaveChanges();

            if (string.IsNullOrEmpty(returnUrl))
            {
                return RedirectToAction("Index", "Product");
            }
            else
            {
                return Redirect(returnUrl);
            }

        }

        public ActionResult UpdateQuantity(int quan, int proid)
        {
            if (!IsUserLoggedIn())
                return RedirectToAction("Login", "User");

            if (quan <= 0)
                return RedirectToAction("Index");

            int userId = GetCurrentUserId();
            var cartItem = _dbContext.GioHangs.FirstOrDefault(g => g.GioHangID == proid && g.NguoiDungID == userId);

            if (cartItem != null)
            {
                var product = _dbContext.ChiTietSanPhams.FirstOrDefault(p => p.ChiTietID == cartItem.SanPhamID);

                if (product != null && quan <= product.SoLuongTonKho)
                {
                    cartItem.SoLuong = quan;
                    _dbContext.SaveChanges();
                }
                else if (product != null)
                {
                    TempData["ErrorMessage"] = $"Số lượng yêu cầu vượt quá số lượng tồn kho (Tồn kho: {product.SoLuongTonKho}).";
                    TempData["ProductId"] = proid;
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteQuantity(int proid)
        {
            if (!IsUserLoggedIn())
                return RedirectToAction("Login", "User");

            int userId = GetCurrentUserId();
            var cartItem = _dbContext.GioHangs.FirstOrDefault(g => g.GioHangID == proid && g.NguoiDungID == userId);

            if (cartItem != null)
            {
                _dbContext.GioHangs.Remove(cartItem);
                _dbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
