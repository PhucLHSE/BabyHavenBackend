using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common.Enum.AlertEnums;

namespace BabyHaven.Common.DTOs.AlertDTOS
{
    public class AlertCreateDto
    {
        [Required]
        public int GrowthRecordId { get; set; }

        [Required]
        public DateTime AlertDate { get; set; }

        [Required]
        public int DiseaseId { get; set; }

        [Required]
        [StringLength(500)]
        public string Message { get; set; } = string.Empty;

        [Required]
        public bool IsRead { get; set; }

        [Required]
        [EnumDataType(typeof(SeverityLevelEnum))]
        public SeverityLevelEnum SeverityLevel { get; set; }

        [Required]
        public bool IsAcknowledged { get; set; }
    }
}
