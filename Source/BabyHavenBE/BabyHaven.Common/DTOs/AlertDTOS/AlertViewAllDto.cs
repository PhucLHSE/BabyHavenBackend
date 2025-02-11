using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.AlertDTOS
{
    public class AlertViewAllDto
    {
        public DateTime AlertDate { get; set; }
        public string Message { get; set; } = string.Empty;
        public string SeverityLevel { get; set; } = string.Empty;
    }
}
