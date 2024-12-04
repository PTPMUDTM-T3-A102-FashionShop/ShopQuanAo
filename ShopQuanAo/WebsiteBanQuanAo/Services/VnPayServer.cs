using WebsiteBanQuanAo.Helper;
using WebsiteBanQuanAo.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebsiteBanQuanAo.Services
{
    public class VnPayServer : IVnPayServers
    {
        private readonly NameValueCollection _someConfigValue;

        public static object LogManager { get; private set; }
        public VnPayServer()
        {
            _someConfigValue = ConfigurationManager.AppSettings;
        }
        public string CreatePaymentUrl(HttpContextBase context, VnPaymentRequestModel model)
        {
            var tick = DateTime.Now.Ticks.ToString();
            var vnpay = new VnPayLibrary();
            vnpay.AddRequestData("vnp_Version", _someConfigValue["VnPay:Version"]);
            vnpay.AddRequestData("vnp_Command", _someConfigValue["VnPay:Command"]);
            vnpay.AddRequestData("vnp_TmnCode", _someConfigValue["VnPay:TmnCode"]);
            vnpay.AddRequestData("vnp_Amount", (model.Amount * 100).ToString());
            vnpay.AddRequestData("vnp_CreateDate", model.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", _someConfigValue["VnPay:CurrCode"]);
            vnpay.AddRequestData("vnp_IpAddr", GetIpAddress(context)); // Sử dụng HttpContextBase

            vnpay.AddRequestData("vnp_Locale", _someConfigValue["VnPay:Locale"]);
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toán cho đơn hàng:" + model.OrderId);
            vnpay.AddRequestData("vnp_OrderType", "other");
            vnpay.AddRequestData("vnp_ReturnUrl", _someConfigValue["VnPay:PaymentCallBack"]);
            vnpay.AddRequestData("vnp_TxnRef", tick);
            var paymentUrl = vnpay.CreateRequestUrl(_someConfigValue["VnPay:BaseUrl"], _someConfigValue["VnPay:HashSecret"]);
            return paymentUrl;
        }
        private string GetIpAddress(HttpContextBase context)
        {
            var ipAddress = string.Empty;

            try
            {
                ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(ipAddress))
                {
                    ipAddress = context.Request.ServerVariables["REMOTE_ADDR"];
                }
                if (!string.IsNullOrEmpty(ipAddress) && ipAddress.Contains(","))
                {
                    ipAddress = ipAddress.Split(',').First();
                }
                return ipAddress;
            }
            catch (Exception ex)
            {
                return "Invalid IP: " + ex.Message;
            }
        }
        public VnPaymentResponseModel PaymentExecute(NameValueCollection queryString, string hashSecret)
        {
            var vnpay = new VnPayLibrary();
            foreach (string key in queryString.AllKeys)
            {
                var value = queryString[key];
                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(key, value.ToString());
                }
            }
            var vnp_orderId = (vnpay.GetResponseData("vnp_TxnRef"));
            var vnp_TransactionId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
            var vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            var vnp_OrderInfo = vnpay.GetResponseData("vnp_OrderInfo");
            var vnp_SecureHash = queryString["vnp_SecureHash"];
            bool isSignatureValid = vnpay.ValidateSignature(vnp_SecureHash, _someConfigValue["VnPay:HashSecret"]);
            if (isSignatureValid)
            {

                return new VnPaymentResponseModel
                {
                    Success = false
                };
            }
            return new VnPaymentResponseModel
            {
                Success = true,
                PaymentMethod = "VnPay",
                OrderDescription = vnp_OrderInfo,
                OrderId = vnp_orderId.ToString(),
                TransactionId = vnp_TransactionId.ToString(),
                Token = vnp_SecureHash,
                VnPayResponseCode = vnp_ResponseCode.ToString(),
            };
        }
    }
}