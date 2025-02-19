using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNPAY.NET.Enums;

namespace BabyHaven.Common.DTOs.VNPayDTOS
{
    public class VNPayRequestDTO
    {
        public string PaymentId { get; set; }

        public string Description { get; set; }

        public double Money { get; set; }

        public string IpAddress { get; set; }

        public BankCode BankCode { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;


        public Currency Currency { get; set; }

        public DisplayLanguage Language { get; set; }
    }
}
