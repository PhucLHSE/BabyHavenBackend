using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.TransactionDTOs.VNPayTransactionDTOS
{
    public class VNPayTransactionDto
    {
        public long PaymentId { get; set; }
        public bool IsSuccess { get; set; } 
        public string Description { get; set; } = string.Empty; 
        public DateTime Timestamp { get; set; } 
        public int VnpayTransactionId { get; set; } 
        public string PaymentMethod { get; set; } = string.Empty;

        public VNPayPaymentResponseDto PaymentResponse { get; set; } 
        public VNPayTransactionStatusDto TransactionStatus { get; set; } 
        public VNPayBankingInfoDto BankingInfo { get; set; }
    }
}
