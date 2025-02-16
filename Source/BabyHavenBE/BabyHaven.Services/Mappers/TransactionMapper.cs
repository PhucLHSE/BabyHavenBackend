using BabyHaven.Common.DTOs.MemberMembershipDTOs;
using BabyHaven.Common.DTOs.TransactionDTOs;
using BabyHaven.Common.Enum.TransactionEnums;
using BabyHaven.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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
                PaymentMethod = model.PaymentMethod,
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

        // Mapper TransactionCreateDto
        public static Transaction MapToTransactionCreateDto(this TransactionCreateDto dto)
        {
            return new Transaction
            {
                UserId = dto.UserId,
                MemberMembershipId = dto.MemberMembershipId,

                Amount = dto.Amount,
                Currency = dto.Currency,
                TransactionType = dto.TransactionType,
                PaymentMethod = dto.PaymentMethod,
                TransactionDate = DateTime.UtcNow,
                Description = dto.Description,

                PaymentStatus = TransactionStatus.Pending.ToString()
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
                PaymentStatus = Enum.TryParse<TransactionStatus>(model.PaymentStatus, true, out var status)
                          ? status
                          : TransactionStatus.Pending
            };
        }
    }
}
