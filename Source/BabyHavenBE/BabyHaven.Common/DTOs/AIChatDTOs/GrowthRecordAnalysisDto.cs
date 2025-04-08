using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.AIChatDTOs
{
    public class GrowthRecordAnalysisDto
    {
        public string ChildName { get; set; }
        public float Weight { get; set; }

        public float Height { get; set; }

        public float? BMI => Height > 0 ? Weight / (Height / 100 * (Height / 100)) : null;

        public float? ChestCircumference { get; set; }
        public float? MuscleMass { get; set; }
        public float? BloodSugarLevel { get; set; }
        public float? Triglycerides { get; set; }
        public string? NutritionalStatus { get; set; }
        public int Age { get; set; }
    }
}
