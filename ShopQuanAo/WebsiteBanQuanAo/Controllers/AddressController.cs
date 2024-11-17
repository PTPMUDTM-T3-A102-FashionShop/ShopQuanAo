using WebsiteBanQuanAo.Filters;
using WebsiteBanQuanAo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WebsiteBanQuanAo.Controllers
{
    public class AddressController : Controller
    {
        private readonly DoAnKetMon_UDTMEntities2 db = new DoAnKetMon_UDTMEntities2();

        public ActionResult Index()
        {
            int userId = GetCurrentUserId();
            var addresses = GetShippingAddresses(userId);
            return View(addresses);
        }
        private List<ThongTinGiaoHang> GetShippingAddresses(int userId)
        {
            return db.ThongTinGiaoHangs.Where(addr => addr.NguoiDungID == userId).ToList();
        }
        public ActionResult AddShippingAddress()
        {
            return View(); 
        }
        [HttpPost]
        public ActionResult AddShippingAddress(ThongTinGiaoHang address)
        {
            if (!ModelState.IsValid)
            {
                var addresses = GetShippingAddresses(GetCurrentUserId());
                return View("Index", addresses); 
            }

            int userId = GetCurrentUserId();
            address.NguoiDungID = userId;
            if (address.DiaChiMacDinh)
            {
                SetDefaultShippingAddress(userId, address.DiaChiID);
            }
            db.ThongTinGiaoHangs.Add(address);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult EditShippingAddress(int id)
        {
            var address = db.ThongTinGiaoHangs.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address); 
        }

        [HttpPost]
        public ActionResult EditShippingAddress(ThongTinGiaoHang updatedAddress)
        {
            if (!ModelState.IsValid)
            {
                return View(updatedAddress);
            }

            var existingAddress = db.ThongTinGiaoHangs.Find(updatedAddress.DiaChiID);
            if (existingAddress != null)
            {
                existingAddress.TenNguoiNhan = updatedAddress.TenNguoiNhan;
                existingAddress.SoDienThoai = updatedAddress.SoDienThoai;
                existingAddress.DiaChiGiaoHang = updatedAddress.DiaChiGiaoHang;
                if (updatedAddress.DiaChiMacDinh)
                {
                    SetDefaultShippingAddress(existingAddress.NguoiDungID, existingAddress.DiaChiID);
                }

                existingAddress.DiaChiMacDinh = updatedAddress.DiaChiMacDinh; 
                db.SaveChanges();
            }
            var addressesAfterEdit = GetShippingAddresses(existingAddress.NguoiDungID);
            return View("Index", addressesAfterEdit);
        }
        [HttpPost]
        public ActionResult SetDefaultShippingAddress(int userId, int diaChiId)
        {
            var otherAddresses = db.ThongTinGiaoHangs
                .Where(addr => addr.NguoiDungID == userId && addr.DiaChiID != diaChiId)
                .ToList();

            foreach (var addr in otherAddresses)
            {
                addr.DiaChiMacDinh = false; 
            }
            var addressToSetDefault = db.ThongTinGiaoHangs.Find(diaChiId);
            if (addressToSetDefault != null)
            {
                addressToSetDefault.DiaChiMacDinh = true; 
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        private void SetNewDefaultAddress(int userId)
        {
            var remainingAddress = db.ThongTinGiaoHangs
                .Where(addr => addr.NguoiDungID == userId && !addr.DiaChiMacDinh)
                .FirstOrDefault();

            if (remainingAddress != null)
            {
                remainingAddress.DiaChiMacDinh = true;
                db.SaveChanges();
            }
        }
        [HttpPost]
        public ActionResult DeleteShippingAddress(int id)
        {
            var address = db.ThongTinGiaoHangs.Find(id);
            if (address != null)
            {
                db.ThongTinGiaoHangs.Remove(address);
                db.SaveChanges();
                if (address.DiaChiMacDinh)
                {
                    SetNewDefaultAddress(address.NguoiDungID);
                }
            }
            return RedirectToAction("Index");
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
    }
}
