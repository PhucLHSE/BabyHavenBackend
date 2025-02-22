using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.GrowthRecordDTOs
{
    public class GrowthRecordBabyDto
    {
        [Range(30, 60, ErrorMessage = "Head Circumference must be between 30cm and 60cm.")]
        public float? HeadCircumference { get; set; }

        [MaxLength(2000, ErrorMessage = "ImmunizationStatus cannot exceed 2000 characters.")]
        public string? ImmunizationStatus { get; set; }
    }
}
