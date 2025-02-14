using BabyHaven.Common.DTOs.MemberMembershipDTOs;
using BabyHaven.Common.DTOs.TransactionDTOs;
using BabyHaven.Common.Enum.TransactionEnums;
using BabyHaven.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                TransactionDate = model.TransactionDate,

                // Convert Status from string to enum
                PaymentStatus = Enum.TryParse<TransactionStatus>(model.PaymentStatus, true, out var status)
                          ? status
                          : TransactionStatus.Pending
            };
        }

        // Mapper TransactionViewDetailsDto
        public static TransactionViewDetailsDto MapToTransactionViewDetailsDto(this Transaction model)
        {
            return new TransactionViewDetailsDto
            {
                FullName = model.User?.Name ?? "Unknown",
                PackageName = model.MemberMembership?.Package?.PackageName ?? "Unknown",

                Amount = model.Amount,
                Currency = model.Currency,
                TransactionType = model.TransactionType,
                TransactionDate = model.TransactionDate,
                PaymentDate = model.PaymentDate,
                GatewayTransactionId = model.GatewayTransactionId,
                Description = model.Description,

                // Convert Status from string to enum
                PaymentStatus = Enum.TryParse<TransactionStatus>(model.PaymentStatus, true, out var status)
                          ? status
                          : TransactionStatus.Pending
            };
        }
    }
}
