using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.TransactionDTOs.VNPayTransactionDTOS
{
    public class VNPayPaymentResponseDto
    {
        public int Code { get; set; } 
        public string Description { get; set; } = string.Empty;
    }
}
