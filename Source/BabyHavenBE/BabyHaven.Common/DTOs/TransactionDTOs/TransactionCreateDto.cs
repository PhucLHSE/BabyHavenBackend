using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.TransactionDTOs
{
    public class TransactionCreateDto
    {
        [Required(ErrorMessage = "UserId is required.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "MemberMembershipId is required.")]
        public Guid MemberMembershipId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a positive value.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Currency is required.")]
        [MaxLength(50, ErrorMessage = "Currency cannot exceed 50 characters.")]
        public string Currency { get; set; } = string.Empty;

        [Required(ErrorMessage = "TransactionType is required.")]
        [MaxLength(100, ErrorMessage = "TransactionType cannot exceed 100 characters.")]
        public string TransactionType { get; set; } = string.Empty;

        [Required(ErrorMessage = "PaymentMethod is required.")]
        [MaxLength(50, ErrorMessage = "PaymentMethod cannot exceed 50 characters.")]
        public string PaymentMethod { get; set; } = string.Empty;

        [MaxLength(255, ErrorMessage = "Description cannot exceed 255 characters.")]
        public string? Description { get; set; } = string.Empty;
    }
}
