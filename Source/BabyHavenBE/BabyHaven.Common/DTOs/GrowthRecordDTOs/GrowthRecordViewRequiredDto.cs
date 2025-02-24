using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.GrowthRecordDTOs
{
    public class GrowthRecordViewRequiredDto
    {
        public double Weight { get; set; }
        public double Height { get; set; }
        public double? BMI => (Height > 0) ? Weight / ((Height / 100) * (Height / 100)) : null;
        public string? DevelopmentalMilestones { get; set; }
        public string? Notes { get; set; }
    }
}
