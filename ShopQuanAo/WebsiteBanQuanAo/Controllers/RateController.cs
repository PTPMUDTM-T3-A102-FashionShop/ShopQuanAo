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
        DoAnKetMon_UDTMEntities2 db = new DoAnKetMon_UDTMEntities2();

        public ActionResult Index(int id)
        {
            DonHang dh = db.DonHangs.FirstOrDefault(x => x.DonHangID == id);
            dh.TinhTrangDonHang = "Hoàn Thành";
            db.SaveChanges();
            ViewBag.iddonhang = id;
            if (dh == null)
            {
                return HttpNotFound("Không tìm thấy đơn hàng.");
            }
            var ctdhList = db.ChiTietDonHangs.Where(x => x.DonHangID == id).ToList();
            ctdhList = ctdhList.Where(x => x.TinhTrangDanhGia == null || x.TinhTrangDanhGia == 0).ToList();
            return View(ctdhList); 
        }
        public ActionResult Rate(int id, int iddonhang)
        {
            ChiTietSanPham sp = db.ChiTietSanPhams.FirstOrDefault(x => x.ChiTietID == id);
            if (sp == null)
            {
                return HttpNotFound("Sản phẩm không tồn tại.");
            }
            ViewBag.iddonhang = iddonhang;
            return View(sp); 
        }

        [HttpPost]
        public ActionResult Rate(int id, int danhGia, string noiDung, int iddonhang)
        {
            ChiTietDonHang ctdh = db.ChiTietDonHangs
    .Where(x => x.SanPhamID == id && x.DonHangID == iddonhang) 
    .FirstOrDefault(); 
            ctdh.TinhTrangDanhGia = 1;
            db.SaveChanges();
            id = ctdh.ChiTietSanPham.SanPham.SanPhamID;
            SanPham sp = db.SanPhams.Where(x => x.SanPhamID == id).FirstOrDefault();
            if (sp == null)
            {
                return HttpNotFound("Sản phẩm không tồn tại.");
            }
            int nguoiDungID = GetCurrentUserId(); 
            PhanHoi ph = new PhanHoi
            {
                SanPhamID = id,
                NguoiDungID = nguoiDungID,
                NoiDung = noiDung,
                DanhGia = danhGia,
                NgayPhanHoi = DateTime.Now
            };
            db.PhanHois.Add(ph);
            var danhGiaTB = db.PhanHois
                               .Where(x => x.SanPhamID == id)
                               .Average(x => (double?)x.DanhGia);
            if (sp.SoSaoTB == null || sp.SoSaoTB == 0)
            {
                sp.SoSaoTB = danhGia;
            }
            else
            {
                sp.SoSaoTB = (int)Math.Round(danhGiaTB.Value);
            }


            db.SaveChanges();
            return RedirectToAction("index", new { id = iddonhang });
        }
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

    }
}
