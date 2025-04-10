﻿using BabyHaven.Common.Enum.SpecializationEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.SpecializationDTOs
{
    public class SpecializationViewAllDto
    {
        public int SpecializationId { get; set; }
        public string SpecializationName { get; set; } = string.Empty;


        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SpecializationStatus Status { get; set; } = SpecializationStatus.Inactive;
    }
}
