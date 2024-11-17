using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanQuanAo.Models;
namespace WebsiteBanQuanAo.Controllers
{
    public class CartController : Controller
    {
        DoAnKetMon_UDTMEntities2 db = new DoAnKetMon_UDTMEntities2();
        private int GetCurrentUserId()
        {
            var authCookie = Request.Cookies["auth"];
            if (authCookie != null)
            {
                string tenDangNhap = authCookie.Value;
                var user = db.NguoiDungs.FirstOrDefault(u => u.TenDangNhap == tenDangNhap);
                if (user != null)
                {
                    return user.NguoiDungID;
                }
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
        public ActionResult Index()
        {
            CheckUserLoggedIn();

            int userId = GetCurrentUserId();
            List<GioHang> cart = db.GioHangs.Where(g => g.NguoiDungID == userId).ToList();
            decimal totalPrice = 0;
            int totalQuantity = 0;
            if (cart == null || !cart.Any())
            {
                ViewBag.SLSP = 0;
            }
            if (cart.Any())
            {
                foreach (var item in cart)
                {
                    totalPrice += item.ChiTietSanPham.Gia * item.SoLuong;
                    totalQuantity += item.SoLuong;
                }
            }

            ViewBag.SLSP = totalQuantity;
            ViewBag.TotalPrice = totalPrice;
            return View(cart);
        }
        public ActionResult Add(int? id, int? sizeID, int? colorID, string returnUrl)
        {
            var checkLoginResult = CheckUserLoggedIn();
            if (checkLoginResult != null)
            {
                return checkLoginResult;
            }
            if (id.HasValue && sizeID.HasValue && colorID.HasValue)
            {
                var productDetail = db.ChiTietSanPhams
                    .FirstOrDefault(p => p.SanPhamID == id && p.SizeID == sizeID && p.MauID == colorID);
                if (productDetail == null)
                {
                    ModelState.AddModelError("", "Sản phẩm với kích thước và màu sắc này không tồn tại.");
                    return RedirectToAction("Index", "Product");
                }
                int userId = GetCurrentUserId();
                var cartItem = db.GioHangs.FirstOrDefault(row =>
                    row.ChiTietSanPham.ChiTietID == productDetail.ChiTietID &&
                    row.NguoiDungID == userId);

                if (cartItem != null)
                {
                    cartItem.SoLuong += 1;
                }
                else
                {
                    GioHang newCartItem = new GioHang
                    {
                        SanPhamID = productDetail.ChiTietID, 
                        SoLuong = 1,
                        NguoiDungID = userId
                    };
                    db.GioHangs.Add(newCartItem);
                }

                db.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("", "Vui lòng chọn kích thước và màu sắc.");
                return RedirectToAction("Details", "Product", new { id = id }); 
            }

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Product");
        }
        public ActionResult UpdateQuantity(int quan, int proid)
        {
            CheckUserLoggedIn();

            if (quan > 0)
            {
                int userId = GetCurrentUserId();
                GioHang cartItem = db.GioHangs.FirstOrDefault(row => row.GioHangID == proid && row.NguoiDungID == userId);

                if (cartItem != null)
                {
                    cartItem.SoLuong = quan;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult DeleteQuantity(int proid)
        {
            CheckUserLoggedIn();

            int userId = GetCurrentUserId();
            GioHang cartItem = db.GioHangs.FirstOrDefault(row => row.GioHangID == proid && row.NguoiDungID == userId);

            if (cartItem != null)
            {
                db.GioHangs.Remove(cartItem);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}