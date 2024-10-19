using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanQuanAo.Models;
using System.Web.Helpers;
using BCrypt.Net;
namespace WebsiteBanQuanAo.Controllers
{
    public class UserController : Controller
    {
        DoAnKetMon_UDTMEntities db = new DoAnKetMon_UDTMEntities();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register([Bind(Include = "HoTen,TenDangNhap,MatKhau,Email,GioiTinh")] NguoiDung a)
        {
            if (a != null)
            {
                NguoiDung myUser = new NguoiDung
                {
                    TenDangNhap = a.TenDangNhap,
                    MatKhau = BCrypt.Net.BCrypt.HashPassword(a.MatKhau),
                    Email = a.Email,
                    HoTen = a.HoTen,
                    GioiTinh = a.GioiTinh,
                    VaiTro = "user",
                    NgayTao = DateTime.Now,
                    KichHoat = true,
                };
                db.NguoiDungs.Add(myUser);
                db.SaveChanges();
            }
            return RedirectToAction("Login", "User");
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(NguoiDung a)
        {
            if (a != null)
            {
                NguoiDung myUser = db.NguoiDungs.Where(u => u.TenDangNhap == a.TenDangNhap).FirstOrDefault();
                if (myUser != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(a.MatKhau, myUser.MatKhau) && myUser.VaiTro == "admin")
                    {
                        HttpCookie authCookie = new HttpCookie("auth", myUser.TenDangNhap);
                        HttpCookie roleCookie = new HttpCookie("role", myUser.VaiTro);
                        Response.Cookies.Add(authCookie);
                        Response.Cookies.Add(roleCookie);
                        return RedirectToAction("Index", "Home", new { area = "admin" });
                    }
                    else if (BCrypt.Net.BCrypt.Verify(a.MatKhau, myUser.MatKhau) && myUser.VaiTro == "user")
                    {
                        HttpCookie authCookie = new HttpCookie("auth", myUser.TenDangNhap)
                        {
                            Expires = DateTime.Now.AddDays(1),
                            Path = "/",
                            HttpOnly = true
                        };
                        HttpCookie roleCookie = new HttpCookie("role", myUser.VaiTro);
                        Response.Cookies.Add(authCookie);
                        Response.Cookies.Add(roleCookie);
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("Password", "Invalid username or password !!!");
            }
            return View();
        }
    }
}