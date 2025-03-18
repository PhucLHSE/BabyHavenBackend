using BabyHaven.Common;
using BabyHaven.Common.DTOs.VNPayDTOS;
using BabyHaven.Repositories.Models;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using VNPAY.NET;
using VNPAY.NET.Enums;
using VNPAY.NET.Models;
using VNPAY.NET.Utilities;

namespace BabyHaven.API.Controllers
{
    [ApiController]
    [Route("api/vnpay")]
    public class VNPayController : ControllerBase
    {
        private readonly IVNPayService _vnPayService;
        private readonly IJwtTokenService _jwtTokenService;

        public VNPayController(IVNPayService vnPayService, IJwtTokenService jwtTokenService)
        {
            _vnPayService = vnPayService;
            _jwtTokenService = jwtTokenService;
        }

        [HttpGet("create-payment")]
        public async Task<IActionResult> CreatePaymentUrl(long gatewayTransactionId)
        {
            string ipAddress = NetworkHelper.GetIpAddress(HttpContext);
            var result = await _vnPayService.CreatePaymentUrl(gatewayTransactionId, ipAddress);
            return StatusCode(result.Status, new { message = result.Message, data = result.Data });
        }

        [HttpGet("payment-confirm")]
        public async Task<IActionResult> PaymentConfirm()
        {
            try
            {
                var result = await _vnPayService.ValidateResponse(Request.Query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("PaymentConfirm Error: " + ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }


        [HttpGet("IpnAction")]
        public IActionResult IpnAction()
        {
            if (Request.QueryString.HasValue)
            {
                try
                {

                    var paymentResult = _vnPayService.ValidateResponse(Request.Query).Result;

                    if (paymentResult.Status == Const.SUCCESS_CREATE_CODE)
                    {
                        // Nếu thanh toán thành công, cập nhật trạng thái đơn hàng trong CSDL
                        // Ví dụ: Đánh dấu đơn hàng đã thanh toán
                        return Ok("Thanh toán thành công!");
                    }

                    // Nếu thanh toán thất bại, có thể hủy đơn hàng hoặc xử lý tùy theo yêu cầu
                    return BadRequest("Thanh toán thất bại!");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return NotFound("Không tìm thấy thông tin thanh toán.");
        }
    }
}
