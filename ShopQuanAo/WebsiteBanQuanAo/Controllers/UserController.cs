using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanQuanAo.Models;
using BCrypt.Net;
using WebsiteBanQuanAo.Filters;

namespace WebsiteBanQuanAo.Controllers
{
    public class UserController : Controller
    {
        DoAnKetMon_UDTMEntities2 db = new DoAnKetMon_UDTMEntities2();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register([Bind(Include = "HoTen,TenDangNhap,MatKhau,Email,GioiTinh,")] NguoiDung newUser)
        {
            if (ModelState.IsValid)
            {
                if (db.NguoiDungs.Any(u => u.TenDangNhap == newUser.TenDangNhap))
                {
                    ModelState.AddModelError("TenDangNhap", "Tên đăng nhập đã tồn tại.");
                    return View(newUser);
                }
                NguoiDung myUser = new NguoiDung
                {
                    HoTen = newUser.HoTen,
                    TenDangNhap = newUser.TenDangNhap,
                    MatKhau = BCrypt.Net.BCrypt.HashPassword(newUser.MatKhau),
                    Email = newUser.Email,
                    GioiTinh = newUser.GioiTinh,
                    MaNhomNguoiDung = 1,
                    NgayTao = DateTime.Now,
                    KichHoat = true,
                };
                db.NguoiDungs.Add(myUser);
                db.SaveChanges();
                return RedirectToAction("Login", "User");
            }
            return View(newUser);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(NguoiDung loginUser)
        {
            if (ModelState.IsValid)
            {
                NguoiDung myUser = db.NguoiDungs.FirstOrDefault(u => u.TenDangNhap == loginUser.TenDangNhap);
                if (myUser != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(loginUser.MatKhau, myUser.MatKhau))
                    {
                        Session["UserID"] = myUser.NguoiDungID;
                        HttpCookie authCookie = new HttpCookie("auth", myUser.TenDangNhap)
                        {
                            Expires = DateTime.Now.AddDays(1),
                            Path = "/",
                            HttpOnly = true
                        };
                        HttpCookie roleCookie = new HttpCookie("role", myUser.NhomNguoiDung.TenNhomNguoiDung);
                        Response.Cookies.Add(authCookie);
                        Response.Cookies.Add(roleCookie);

                        return myUser.NhomNguoiDung.TenNhomNguoiDung == "Quản lý"
                            ? RedirectToAction("Index", "Home", new { area = "admin" })
                            : RedirectToAction("Index", "Product");
                    }
                }
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không hợp lệ.");
            }
            return View(loginUser);
        }

        public ActionResult Logout()
        {
            if (Request.Cookies["auth"] != null)
            {
                var cookie = new HttpCookie("auth")
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
                Response.Cookies.Add(cookie);
            }
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
