using WebsiteBanQuanAo.Filters;
using WebsiteBanQuanAo.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteBanQuanAo.Controllers
{
    public class HomeController : Controller
    {
        DoAnKetMon_UDTMEntities2 db = new DoAnKetMon_UDTMEntities2();

        public ActionResult Index()
        {
            var authCookie = Request.Cookies["auth"];
            string tenDangNhap = authCookie != null ? authCookie.Value : null;
            List<ChiTietSanPham> sanPhams = new List<ChiTietSanPham>();
            List<GioHang> cart = new List<GioHang>();
            int totalQuantity = 0;
            if (sanPhams.Count == 0)
            {
                sanPhams = db.ChiTietSanPhams.OrderBy(sp => sp.Gia).Take(10).ToList();
            }
            sanPhams = sanPhams
             .GroupBy(row => row.SanPham.SanPhamID)
             .Select(group => group.OrderBy(row => row.Gia).FirstOrDefault())
             .ToList();
            ViewBag.SLSP = totalQuantity;
            ViewBag.SanPhamLienQuan = sanPhams;
            return View(sanPhams);
        }
    }
}
