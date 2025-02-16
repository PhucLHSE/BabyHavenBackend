using BabyHaven.Common;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using VNPAY.NET.Enums;
using VNPAY.NET.Models;
using VNPAY.NET;
using Microsoft.AspNetCore.Http;
using BabyHaven.Repositories;
using BabyHaven.Services.Mappers;

namespace BabyHaven.Services.Services
{
    public class VNPayService : IVNPayService
    {
        private readonly IVnpay _vnPay;
        private readonly IConfiguration _configuration;
        private readonly UnitOfWork _unitOfWork;

        public VNPayService(IVnpay vnPay, IConfiguration configuration, UnitOfWork unitOfWork)
        {
            _vnPay = vnPay;
            _configuration = configuration;
            _unitOfWork ??= new UnitOfWork();
            // Khởi tạo cấu hình VNPAY từ appsettings.json
            _vnPay.Initialize(
                _configuration["Vnpay:TmnCode"],
                _configuration["Vnpay:HashSecret"],
                _configuration["Vnpay:BaseUrl"],
                _configuration["Vnpay:ReturnUrl"]
            );
        }

        public async Task<IServiceResult> CreatePaymentUrl(Guid transactionId, string ipAddress)
        {
            try
            {
                var transaction = await _unitOfWork.TransactionRepository.GetByIdAsync(transactionId);

                if (transaction == null)
                {
                    return new ServiceResult(Const.FAIL_READ_CODE, "Transaction not found");
                }

                var request = new PaymentRequest
                {
                    PaymentId = BitConverter.ToInt64(transactionId.ToByteArray(), 0),
                    Money = Convert.ToDouble(transaction.Amount),
                    Description = transaction.Description,
                    IpAddress = ipAddress,
                    BankCode = BankCode.ANY, // Cho phép chọn bất kỳ ngân hàng nào
                    CreatedDate = DateTime.UtcNow,
                    Currency = Currency.VND,
                    Language = DisplayLanguage.Vietnamese
                };

                // Tạo URL thanh toán
                var paymentUrl = _vnPay.GetPaymentUrl(request);

                return new ServiceResult(Const.SUCCESS_CREATE_CODE, "Create payment URL successfully!", paymentUrl);
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IServiceResult> ValidateResponse(IQueryCollection queryParams)
        {
            try
            {
                var paymentResult = _vnPay.GetPaymentResult(queryParams);
                var transactionId = Guid.Parse(paymentResult.PaymentId.ToString());

                var transaction = await _unitOfWork.TransactionRepository.GetByIdAsync(transactionId);
                if (transaction == null)
                {
                    return new ServiceResult(Const.FAIL_CREATE_CODE, "Không tìm thấy giao dịch.");
                }

                transaction.UpdateTransactionFromVNPayResponse(paymentResult);

                await _unitOfWork.TransactionRepository.UpdateAsync(transaction);

                return new ServiceResult(Const.SUCCESS_CREATE_CODE, "Cập nhật trạng thái giao dịch thành công!", transaction);
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }


    }
}
