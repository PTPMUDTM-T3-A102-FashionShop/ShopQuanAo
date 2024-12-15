using WebsiteBanQuanAo.Filters;
using WebsiteBanQuanAo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WebsiteBanQuanAo.Controllers
{
    [UserAuthorization]
    public class AddressController : Controller
    {
        private readonly ShopQuanAoEntities _dbContext = new ShopQuanAoEntities();

        public ActionResult Index()
        {
            int userId = GetAuthenticatedUserId();
            var userAddresses = FetchUserAddresses(userId);
            return View(userAddresses);
        }

        private List<ThongTinGiaoHang> FetchUserAddresses(int userId)
        {
            return _dbContext.ThongTinGiaoHangs.Where(addr => addr.NguoiDungID == userId).ToList();
        }

        public ActionResult AddShippingAddress()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddShippingAddress(ThongTinGiaoHang newAddress)
        {
            if (!ModelState.IsValid)
            {
                var currentAddresses = FetchUserAddresses(GetAuthenticatedUserId());
                return View("Index", currentAddresses);
            }

            int userId = GetAuthenticatedUserId();
            newAddress.NguoiDungID = userId;

            if (newAddress.DiaChiMacDinh)
            {
                UnsetDefaultAddress(userId, newAddress.DiaChiID);
            }

            _dbContext.ThongTinGiaoHangs.Add(newAddress);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult EditShippingAddress(int id)
        {
            var address = _dbContext.ThongTinGiaoHangs.Find(id);
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

            var existingAddress = _dbContext.ThongTinGiaoHangs.Find(updatedAddress.DiaChiID);
            if (existingAddress != null)
            {
                existingAddress.TenNguoiNhan = updatedAddress.TenNguoiNhan;
                existingAddress.SoDienThoai = updatedAddress.SoDienThoai;
                existingAddress.DiaChiGiaoHang = updatedAddress.DiaChiGiaoHang;

                if (updatedAddress.DiaChiMacDinh)
                {
                    UnsetDefaultAddress(existingAddress.NguoiDungID, existingAddress.DiaChiID);
                }

                existingAddress.DiaChiMacDinh = updatedAddress.DiaChiMacDinh;
                _dbContext.SaveChanges();
            }

            var updatedAddressList = FetchUserAddresses(existingAddress.NguoiDungID);
            return View("Index", updatedAddressList);
        }

        [HttpPost]
        public ActionResult UnsetDefaultAddress(int userId, int addressId)
        {
            var otherAddresses = _dbContext.ThongTinGiaoHangs
                .Where(addr => addr.NguoiDungID == userId && addr.DiaChiID != addressId)
                .ToList();

            foreach (var addr in otherAddresses)
            {
                addr.DiaChiMacDinh = false;
            }

            var selectedAddress = _dbContext.ThongTinGiaoHangs.Find(addressId);
            if (selectedAddress != null)
            {
                selectedAddress.DiaChiMacDinh = true;
            }

            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        private void SetDefaultToFirstAddress(int userId)
        {
            var firstAvailableAddress = _dbContext.ThongTinGiaoHangs
                .Where(addr => addr.NguoiDungID == userId && !addr.DiaChiMacDinh)
                .FirstOrDefault();

            if (firstAvailableAddress != null)
            {
                firstAvailableAddress.DiaChiMacDinh = true;
                _dbContext.SaveChanges();
            }
        }

        [HttpPost]
        public ActionResult DeleteShippingAddress(int id)
        {
            var addressToDelete = _dbContext.ThongTinGiaoHangs.Find(id);
            if (addressToDelete != null)
            {
                _dbContext.ThongTinGiaoHangs.Remove(addressToDelete);
                _dbContext.SaveChanges();

                if (addressToDelete.DiaChiMacDinh)
                {
                    SetDefaultToFirstAddress(addressToDelete.NguoiDungID);
                }
            }

            return RedirectToAction("Index");
        }

        private int GetAuthenticatedUserId()
        {
            var authCookie = Request.Cookies["auth"];
            if (authCookie != null)
            {
                string username = authCookie.Value;
                var user = _dbContext.NguoiDungs.FirstOrDefault(u => u.TenDangNhap == username);
                if (user != null)
                {
                    return user.NguoiDungID;
                }
            }

            return 0;
        }
    }
}
