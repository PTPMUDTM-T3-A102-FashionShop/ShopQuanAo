using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanQuanAo.Models;

namespace WebsiteBanQuanAo.Controllers
{
    public class HomeController : Controller
    {
        DoAnKetMon_UDTMEntities db = new DoAnKetMon_UDTMEntities();
        public ActionResult Index()
        {
            var authCookie = Request.Cookies["auth"];
            string tenDangNhap = authCookie != null ? authCookie.Value : null;
            List<SanPham> sanPhams = new List<SanPham>();
            List<GioHang> cart = new List<GioHang>();
            int totalQuantity = 0;
            if (!string.IsNullOrEmpty(tenDangNhap))
            {
                NguoiDung kh = db.NguoiDungs.FirstOrDefault(u => u.TenDangNhap == tenDangNhap);
                if (kh != null)
                {
                    cart = db.GioHangs.Where(g => g.NguoiDungID == kh.NguoiDungID).ToList();
                    totalQuantity = cart.Sum(item => item.SoLuong);
                }
            }
            if (sanPhams.Count == 0)
            {
                sanPhams = db.SanPhams.OrderBy(sp => sp.Gia).Take(10).ToList();
            }
            ViewBag.SLSP = totalQuantity;
            ViewBag.SanPhamLienQuan = sanPhams;
            return View(sanPhams);
        }


        
        
    }
}