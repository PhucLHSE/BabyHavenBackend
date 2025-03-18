using BabyHaven.Common.DTOs.MemberMembershipDTOs;
using BabyHaven.Common.DTOs.TransactionDTOs;
using BabyHaven.Common.Enum.MemberMembershipEnums;
using BabyHaven.Common.Enum.TransactionEnums;
using BabyHaven.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using VNPAY.NET.Models;

namespace BabyHaven.Services.Mappers
{
    public static class TransactionMapper
    {
        // Mapper TransactionViewAllDto
        public static TransactionViewAllDto MapToTransactionViewAllDto(this Transaction model)
        {
            return new TransactionViewAllDto
            {
                FullName = model.User?.Name ?? "Unknown",
                PackageName = model.MemberMembership?.Package?.PackageName ?? "Unknown",

                Amount = model.Amount,
                Currency = model.Currency,
                TransactionType = model.TransactionType,
                PaymentMethod = model.PaymentMethod,
                TransactionDate = model.TransactionDate,

                // Convert Status from string to enum
                PaymentStatus = Enum.TryParse<Common.Enum.TransactionEnums.TransactionStatus>(model.PaymentStatus, true, out var status)
                          ? status
                          : Common.Enum.TransactionEnums.TransactionStatus.Pending
            };
        }

        // Mapper TransactionViewDetailsDto
        public static TransactionViewDetailsDto MapToTransactionViewDetailsDto(this Transaction model)
        {
            return new TransactionViewDetailsDto
            {
                TransactionId = model.TransactionId,
                FullName = model.User?.Name ?? "Unknown",
                PackageName = model.MemberMembership?.Package?.PackageName ?? "Unknown",

                Amount = model.Amount,
                Currency = model.Currency,
                TransactionType = model.TransactionType,
                PaymentMethod = model.PaymentMethod,
                TransactionDate = model.TransactionDate,
                PaymentDate = model.PaymentDate,
                GatewayTransactionId = model.GatewayTransactionId.ToString(),
                Description = model.Description,

                // Convert Status from string to enum
                PaymentStatus = Enum.TryParse<Common.Enum.TransactionEnums.TransactionStatus>(model.PaymentStatus, true, out var status)
                          ? status
                          : Common.Enum.TransactionEnums.TransactionStatus.Pending
            };
        }

        // Mapper TransactionCreateDto
        public static Transaction MapToTransactionCreateDto(this TransactionCreateDto dto)
        {
            return new Transaction
            {
                UserId = dto.UserId,
                MemberMembershipId = dto.MemberMembershipId,

                Currency = "",
                TransactionType = "",
                PaymentMethod = "",
                TransactionDate = DateTime.UtcNow,
                GatewayTransactionId = DateTime.UtcNow.Ticks,

                PaymentStatus = Common.Enum.TransactionEnums.TransactionStatus.Pending.ToString()
            };
        }

        // Mapper TransactionDeleteDto
        public static TransactionDeleteDto MapToTransactionDeleteDto(this Transaction model)
        {
            return new TransactionDeleteDto
            {
                FullName = model.User?.Name ?? "Unknown",
                PackageName = model.MemberMembership?.Package?.PackageName ?? "Unknown",

                Amount = model.Amount,
                Currency = model.Currency,
                TransactionType = model.TransactionType,
                PaymentMethod = model.PaymentMethod,
                TransactionDate = model.TransactionDate,
                PaymentDate = model.PaymentDate,
                GatewayTransactionId = model.GatewayTransactionId,
                Description = model.Description,

                // Convert Status from string to enum
                PaymentStatus = Enum.TryParse<Common.Enum.TransactionEnums.TransactionStatus>(model.PaymentStatus, true, out var status)
                          ? status
                          : Common.Enum.TransactionEnums.TransactionStatus.Pending
            };
        }
      
        public static void UpdateTransactionFromVNPayResponse(this Transaction transaction, PaymentResult vnpayResponse)
        {
            transaction.PaymentStatus = vnpayResponse.IsSuccess 
                ? Common.Enum.TransactionEnums.TransactionStatus.Completed.ToString() 
                : Common.Enum.TransactionEnums.TransactionStatus.Failed.ToString();

            transaction.PaymentDate = DateTime.Now;
            transaction.PaymentMethod = vnpayResponse.PaymentMethod ?? transaction.PaymentMethod;
            transaction.Currency = "VND";
            transaction.TransactionType = "VNpay";

            //Update MemberMembership
            transaction.MemberMembership.StartDate = DateTime.Now;
            transaction.MemberMembership.EndDate = DateTime.Now.AddMonths(transaction.MemberMembership.Package.DurationMonths);

            transaction.MemberMembership.Status = vnpayResponse.IsSuccess 
                ? MemberMembershipStatus.Active.ToString() 
                : MemberMembershipStatus.Inactive.ToString();
            transaction.MemberMembership.IsActive = vnpayResponse.IsSuccess;
            transaction.MemberMembership.UpdatedAt = DateTime.Now;
        }
    }
}
