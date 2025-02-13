using BabyHaven.Common.Enum.TransactionEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.TransactionDTOs
{
    public class TransactionViewAllDto
    {
        public string FullName { get; set; } = string.Empty;

        public string PackageName { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public string Currency { get; set; } = string.Empty;

        public string TransactionType { get; set; } = string.Empty;

        public DateTime TransactionDate { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TransactionStatus PaymentStatus { get; set; } = TransactionStatus.Pending;
    }
}
