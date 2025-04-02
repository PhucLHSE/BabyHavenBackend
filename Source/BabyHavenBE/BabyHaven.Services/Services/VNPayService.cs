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
using BabyHaven.Common.DTOs.VNPayDTOS;
using BabyHaven.Common.Enum.MemberMembershipEnums;

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

        public async Task<IServiceResult> CreatePaymentUrl(long gatewayTransactionId, string ipAddress)
        {
            try
            {
                var transaction = await _unitOfWork.TransactionRepository.GetByGatewayTransactionIdAsync(gatewayTransactionId);

                if (transaction == null)
                {
                    return new ServiceResult(Const.FAIL_READ_CODE, "Transaction not found");
                }

                var membership = await _unitOfWork.MemberMembershipRepository.GetByIdMemberMembershipAsync(transaction.MemberMembershipId);

                if (membership == null)
                {
                    return new ServiceResult(Const.FAIL_READ_CODE, "Membership not found");
                }

                var request = new PaymentRequest
                {
                    PaymentId = transaction.GatewayTransactionId,
                    Money = Convert.ToDouble(membership.Package.Price),
                    Description = membership.Package.Description,
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

        public async Task<IServiceResult> CreatePaymentUrl(long gatewayTransactionId, Guid memberMembershipId, string ipAddress)
        {
            try
            {
                var transaction = await _unitOfWork.TransactionRepository.GetByGatewayTransactionIdAsync(gatewayTransactionId);
                var membership = await _unitOfWork.MemberMembershipRepository.GetByIdMemberMembershipAsync(memberMembershipId);


                if (transaction == null)
                {
                    return new ServiceResult(Const.FAIL_READ_CODE, "Transaction not found");
                }

                if (membership == null)
                {
                    return new ServiceResult(Const.FAIL_READ_CODE, "Membership plan not found");
                }

                var request = new PaymentRequest
                {
                    PaymentId = transaction.GatewayTransactionId,
                    Money = Convert.ToDouble(membership.Package.Price),
                    Description = membership.Package.Description,
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
                var gatewayTransactionId = paymentResult.PaymentId;

                var transaction = await _unitOfWork.TransactionRepository.GetByGatewayTransactionIdAsync(gatewayTransactionId);
                if (transaction == null)
                {
                    return new ServiceResult(Const.FAIL_CREATE_CODE, "Transaction not found!");
                }

                if (transaction.MemberMembership == null)
                {
                    return new ServiceResult(Const.FAIL_CREATE_CODE, "Membership not found!");
                }


                if (paymentResult.IsSuccess is true)
                {
                    var existingMemberships = await _unitOfWork.MemberMembershipRepository
                        .GetAllOldByMemberIdAsync(transaction.MemberMembership.MemberId);
                    foreach (var membership in existingMemberships)
                    {
                        membership.Status = MemberMembershipStatus.Suspended.ToString();
                        await _unitOfWork.MemberMembershipRepository.UpdateAsync(membership);
                    }
                }

                transaction.UpdateTransactionFromVNPayResponse(paymentResult);
                await _unitOfWork.MemberMembershipRepository.UpdateAsync(transaction.MemberMembership);
                await _unitOfWork.TransactionRepository.UpdateAsync(transaction);

                return new ServiceResult(Const.SUCCESS_CREATE_CODE, "Payment status: " + transaction.PaymentStatus, transaction);
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
    }
}
