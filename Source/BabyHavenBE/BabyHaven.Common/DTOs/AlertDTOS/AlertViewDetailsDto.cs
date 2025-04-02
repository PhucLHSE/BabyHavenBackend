using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.AlertDTOS
{
    public class AlertViewDetailsDto
    {
        public int AlertId { get; set; }
        public int GrowthRecordId { get; set; }
        public DateTime AlertDate { get; set; }
        public int DiseaseId { get; set; }
        public string DiseaseName { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public string SeverityLevel { get; set; } = string.Empty;
        public bool IsAcknowledged { get; set; }
        public DateTime? RecordDate { get; set; }
    }
}
