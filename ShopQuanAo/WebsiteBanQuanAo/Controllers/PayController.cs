using WebsiteBanQuanAo.Filters;
using WebsiteBanQuanAo.Models;
using WebsiteBanQuanAo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WebsiteBanQuanAo.Controllers
{
    [UserAuthorization]
    public class PayController : Controller
    {
        ShopQuanAoEntities db = new ShopQuanAoEntities();
        private readonly IVnPayServers _vnPayservice;

        public PayController(IVnPayServers vnPayServers)
        {
            _vnPayservice = vnPayServers;
        }

        public ActionResult Index()
        {
            CheckUserLoggedIn();

            int userId = GetCurrentUserId();
            List<GioHang> cart = db.GioHangs.Where(g => g.NguoiDungID == userId).ToList();
            decimal totalPrice = 0;
            int totalQuantity = 0;
            var selectedAddress = GetDefaultOrFirstShippingAddress(userId);

            ViewBag.HasShippingAddress = selectedAddress != null;
            ViewBag.AddAddressLink = Url.Action("Index", "Address");

            if (selectedAddress != null)
            {
                ViewBag.DiaChiGiaoHang = selectedAddress;
            }

            if (cart.Any())
            {
                foreach (var item in cart)
                {
                    if (item.SoLuong <= item.ChiTietSanPham.SoLuongTonKho)
                    {
                        totalPrice += item.ChiTietSanPham.Gia * item.SoLuong;
                        totalQuantity += item.SoLuong;
                    }
                    else
                    {
                        TempData["ErrorMessage"] = $"Số lượng sản phẩm không hợp lệ, sản phẩm: {item.ChiTietSanPham.SanPham.TenSanPham} Size: {item.ChiTietSanPham.Size.TenSize} Màu: {item.ChiTietSanPham.Mau.TenMau} Chỉ còn: {item.ChiTietSanPham.SoLuongTonKho} sản phẩm !!!";
                        return RedirectToAction("Index", "Cart");
                    }
                }
            }
            else
            {
                TempData["ktracart"] = "Vui lòng thêm sản phẩm vào giỏ hàng";
                return RedirectToAction("Index", "Cart");
            }

            Session["ktracart"] = null;
            ViewBag.SLSP = totalQuantity;
            ViewBag.TotalPrice = totalPrice;

            return View(cart);
        }

        [HttpPost]
        public ActionResult Index(string paymentMethod)
        {
            if (paymentMethod == "vnpay")
            {
                int userId = GetCurrentUserId();
                NguoiDung user = db.NguoiDungs.FirstOrDefault(g => g.NguoiDungID == userId);

                var cart = db.GioHangs.Where(g => g.NguoiDungID == userId).ToList();
                if (cart == null || !cart.Any())
                {
                    return RedirectToAction("Index");
                }

                var selectedAddress = GetDefaultOrFirstShippingAddress(userId);
                if (selectedAddress == null)
                {
                    ModelState.AddModelError("DiaChiGiaoHang", "Vui lòng chọn địa chỉ giao hàng.");
                    return RedirectToAction("Index");
                }

                decimal totalPrice = cart.Sum(item => (item.ChiTietSanPham.Gia - (item.ChiTietSanPham.GiaDuocGiam ?? 0)) * item.SoLuong);
                TempData["PaymentMethod"] = paymentMethod;
                int amountToSend = Convert.ToInt32(totalPrice);

                string fullName = user != null ? user.HoTen : "Khách hàng không xác định";
                string description = $"Thanh toán cho giỏ hàng của bạn, tổng giá trị: {totalPrice:C}";

                int lastOrderId = db.DonHangs.OrderByDescending(d => d.DonHangID).Select(d => d.DonHangID).FirstOrDefault();
                int newOrderId = lastOrderId + 1;

                var vnPayModel = new VnPaymentRequestModel
                {
                    Amount = amountToSend,
                    CreatedDate = DateTime.Now,
                    Description = description,
                    FullName = fullName,
                    OrderId = newOrderId.ToString()
                };

                string paymentUrl = _vnPayservice.CreatePaymentUrl(HttpContext, vnPayModel);
                return Redirect(paymentUrl);
            }
            else
            {
                return RedirectToAction("CapNhatDonHang", new { paymentMethod = paymentMethod });
            }
        }

        public ActionResult CapNhatDonHang(string paymentMethod)
        {
            int userId = GetCurrentUserId();
            NguoiDung user = db.NguoiDungs.FirstOrDefault(g => g.NguoiDungID == userId);

            var cart = db.GioHangs.Where(g => g.NguoiDungID == userId).ToList();
            if (cart == null || !cart.Any())
            {
                TempData["ErrorMessage"] = "Giỏ hàng của bạn trống!";
                return RedirectToAction("Index");
            }

            var selectedAddress = GetDefaultOrFirstShippingAddress(userId);
            if (selectedAddress == null)
            {
                ModelState.AddModelError("DiaChiGiaoHang", "Vui lòng chọn địa chỉ giao hàng.");
                return RedirectToAction("Index");
            }

            decimal totalPrice = cart.Sum(item => (item.ChiTietSanPham.Gia - (item.ChiTietSanPham.GiaDuocGiam ?? 0)) * item.SoLuong);
            var newOrder = new DonHang
            {
                NguoiDungID = userId,
                DiaChiID = selectedAddress.DiaChiID,
                TongTien = totalPrice,
                TinhTrangDonHang = "Đang xử lý",
                HinhThucThanhToan = paymentMethod,
                TinhTrangThanhToan = paymentMethod == "vnpay" ? "Đã thanh toán" : "Chưa thanh toán",
                NgayThanhToan = paymentMethod == "vnpay" ? DateTime.Now : (DateTime?)null,
                NgayDatHang = DateTime.Now
            };

            db.DonHangs.Add(newOrder);
            db.SaveChanges();

            foreach (var item in cart)
            {
                var orderDetail = new ChiTietDonHang
                {
                    DonHangID = newOrder.DonHangID,
                    SanPhamID = item.SanPhamID,
                    SoLuong = item.SoLuong,
                    DonGia = item.ChiTietSanPham.Gia - (item.ChiTietSanPham.GiaDuocGiam ?? 0)
                };
                db.ChiTietDonHangs.Add(orderDetail);

                var product = db.ChiTietSanPhams.Find(item.SanPhamID);
                product.SoLuongTonKho -= item.SoLuong;
                product.SanPham.SoLuongDaBan += item.SoLuong;
            }
            db.SaveChanges();

            try
            {
                var mail = new System.Net.Mail.MailMessage
                {
                    From = new System.Net.Mail.MailAddress("huythang1306@gmail.com"),
                    Subject = "Xác nhận đơn hàng",
                    Body = GenerateOrderDetailsEmail(cart, newOrder.DonHangID, totalPrice)
                };
                mail.To.Add(user.Email);

                var smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new System.Net.NetworkCredential("huythang1306@gmail.com", "tkis metb fjre ailo"),
                    EnableSsl = true
                };
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Không thể gửi email: " + ex.Message);
            }

            db.GioHangs.RemoveRange(cart);
            db.SaveChanges();

            TempData["SuccessMessage"] = "Đơn hàng của bạn đã được tạo thành công!";
            return RedirectToAction("success");
        }

        private string GenerateOrderDetailsEmail(List<GioHang> cart, int orderId, decimal totalPrice)
        {
            var orderDetails = $"Cảm ơn bạn đã đặt hàng! Mã đơn hàng của bạn là: {orderId}.\n";
            orderDetails += "Dưới đây là thông tin chi tiết đơn hàng của bạn:\n\n";

            foreach (var item in cart)
            {
                orderDetails += $"- Sản phẩm: {item.ChiTietSanPham.SanPham.TenSanPham}\n";
                orderDetails += $"  Size: {item.ChiTietSanPham.Size.TenSize}\n";
                orderDetails += $"  Màu: {item.ChiTietSanPham.Mau.TenMau}\n";
                orderDetails += $"  Số lượng: {item.SoLuong}\n";
                orderDetails += $"  Đơn giá: {item.ChiTietSanPham.Gia - (item.ChiTietSanPham.GiaDuocGiam ?? 0):C}\n";
                orderDetails += $"  Thành tiền: {(item.ChiTietSanPham.Gia - (item.ChiTietSanPham.GiaDuocGiam ?? 0)) * item.SoLuong:C}\n\n";
            }

            orderDetails += $"Tổng tiền: {totalPrice:C}\n";
            orderDetails += "Cảm ơn bạn đã mua sắm tại cửa hàng chúng tôi!";
            return orderDetails;
        }

        public ActionResult fail() => View();

        public ActionResult success() => View();

        public ActionResult PaymentCallBack()
        {
            var hashSecret = System.Configuration.ConfigurationManager.AppSettings["VnPay:HashSecret"];
            var response = _vnPayservice.PaymentExecute(Request.QueryString, hashSecret);

            if (response == null)
            {
                TempData["Message"] = "Lỗi thanh toán VN Pay: Phản hồi không hợp lệ hoặc chữ ký không hợp lệ.";
                return RedirectToAction("fail");
            }
            else if (response.VnPayResponseCode != "00")
            {
                TempData["Message"] = $"Lỗi thanh toán VN Pay: {response.VnPayResponseCode}";
                return RedirectToAction("fail");
            }
            else
            {
                string paymentMethod = TempData["PaymentMethod"] as string;
                return RedirectToAction("CapNhatDonHang", new { paymentMethod = paymentMethod });
            }
        }

        private List<ThongTinGiaoHang> GetShippingAddresses(int userId) => db.ThongTinGiaoHangs.Where(addr => addr.NguoiDungID == userId).ToList();

        public ThongTinGiaoHang GetDefaultOrFirstShippingAddress(int userId)
        {
            var addresses = GetShippingAddresses(userId);
            return addresses.FirstOrDefault(addr => addr.DiaChiMacDinh) ?? addresses.FirstOrDefault();
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
