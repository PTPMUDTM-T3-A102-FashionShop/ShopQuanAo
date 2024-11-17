using WebsiteBanQuanAo.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanQuanAo.Models;
namespace WebsiteBanQuanAo.Controllers
{
    public class OrderController : Controller
    {
        private readonly DoAnKetMon_UDTMEntities2 db = new DoAnKetMon_UDTMEntities2();
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "User");
            }

            int userId = (int)Session["UserId"];
            List<DonHang> lst = db.DonHangs.Where(x => x.NguoiDungID == userId).ToList();
            return View(lst);
        }
        public ActionResult Details(int id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            var order = db.DonHangs
                          .Where(o => o.DonHangID == id)
                          .FirstOrDefault();

            if (order == null)
            {
                return HttpNotFound();
            }
            List<ChiTietDonHang> orderDetails = db.ChiTietDonHangs.Where(x => x.DonHangID == id).ToList();
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
            return RedirectToAction("Index");
        }


    }
}