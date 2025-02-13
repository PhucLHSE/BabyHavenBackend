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
                TransactionDate = model.TransactionDate,
                TransactionType = model.TransactionType,

                // Convert Status from string to enum
                PaymentStatus = Enum.TryParse<TransactionStatus>(model.PaymentStatus, true, out var status)
                          ? status
                          : TransactionStatus.Pending,
            };
        }
    }
}
