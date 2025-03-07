﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.GrowthRecordDTOs
{
    public class GrowthRecordToddlerDto
    {
        [Range(40, 80, ErrorMessage = "Chest Circumference must be between 40cm and 80cm.")]
        public float? ChestCircumference { get; set; }

        [MaxLength(50, ErrorMessage = "NutritionalStatus cannot exceed 50 characters.")]
        public string? NutritionalStatus { get; set; }

        [MaxLength(2000, ErrorMessage = "ImmunizationStatus cannot exceed 2000 characters.")]
        public string? ImmunizationStatus { get; set; }
    }
}
