using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.TransactionDTOs.VNPayTransactionDTOS
{
    public class VNPayBankingInfoDto
    {
        public string BankCode { get; set; } = string.Empty;
        public string BankTransactionId { get; set; } = string.Empty;
    }
}
