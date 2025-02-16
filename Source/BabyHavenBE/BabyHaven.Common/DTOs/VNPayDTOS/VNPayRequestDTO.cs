using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.VNPayDTOS
{
    public class VNPayRequestDTO
    {
        public int Amount { get; set; }
        public string OrderInfo { get; set; }
        public string OrderType { get; set; }
        public string BankCode { get; set; }
    }
}
