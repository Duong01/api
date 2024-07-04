using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using api_2hands.Util;
/*
namespace api_2hands.Controllers
{
    public class VNPayController : ApiController
    {
        public string url = "http://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
        public string returnUrl = "https://localhost:8080/vnpayAPI/PaymentConfirm"; // Update your port here
        public string tmnCode = ""; // Fill in your TMN code
        public string hashSecret = ""; // Fill in your hash secret

        public IHttpActionResult Index()
        {
            return Ok();
        }

        [HttpPost]
        [Route("/VNPayAPI/Payment/{amount}/{infor}/{orderinfor}")]
        public IHttpActionResult Payment(string amount, string infor, string orderinfor)
        {
            string hostName = Dns.GetHostName();
            string clientIPAddress = Dns.GetHostAddresses(hostName).GetValue(0).ToString();
            PayLib pay = new PayLib();

            pay.AddRequestData("vnp_Version", "2.1.0");
            pay.AddRequestData("vnp_Command", "pay");
            pay.AddRequestData("vnp_TmnCode", tmnCode);
            pay.AddRequestData("vnp_Amount", amount);
            pay.AddRequestData("vnp_BankCode", "");
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", "VND");
            pay.AddRequestData("vnp_IpAddr", clientIPAddress);
            pay.AddRequestData("vnp_Locale", "vn");
            pay.AddRequestData("vnp_OrderInfo", infor);
            pay.AddRequestData("vnp_OrderType", "other");
            pay.AddRequestData("vnp_ReturnUrl", returnUrl);
            pay.AddRequestData("vnp_TxnRef", orderinfor);

            string paymentUrl = pay.CreateRequestUrl(url, hashSecret);
            return Redirect(paymentUrl);
        }

        [HttpPost]
        [Route("/VNpayAPI/paymentconfirm")]
        public IHttpActionResult PaymentConfirm()
        {
            if (Request.RequestUri.Query.Length > 0)
            {
                var queryString = HttpUtility.ParseQueryString(Request.RequestUri.Query);
                string vnp_SecureHash = queryString["vnp_SecureHash"];

                // Remove the secure hash parameter from the query string before validating signature
                string queryStringWithoutHash = Request.RequestUri.Query.Replace($"&vnp_SecureHash={vnp_SecureHash}", "");

                bool checkSignature = ValidateSignature(queryStringWithoutHash, vnp_SecureHash, hashSecret);

                if (checkSignature && tmnCode == queryString["vnp_TmnCode"])
                {
                    string vnp_ResponseCode = queryString["vnp_ResponseCode"];
                    if (vnp_ResponseCode == "00")
                    {
                        // Payment success
                        return Redirect("SUCCESS_LINK");
                    }
                    else
                    {
                        // Payment failed with error code: vnp_ResponseCode
                        return Redirect("FAILURE_LINK");
                    }
                }
                else
                {
                    // Invalid signature
                    return Redirect("INVALID_SIGNATURE_LINK");
                }
            }
            // Invalid request
            return Redirect("INVALID_REQUEST_LINK");
        }

        public bool ValidateSignature(string queryStringWithoutHash, string inputHash, string secretKey)
        {
            string myChecksum = PayLib.HmacSHA512(secretKey, queryStringWithoutHash);
            return myChecksum.Equals(inputHash, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}*/
