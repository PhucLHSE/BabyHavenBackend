using BabyHaven.Common.Enum.SpecializationEnums;
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
        public string SpecializationName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SpecializationStatus Status { get; set; } = SpecializationStatus.Inactive;

    }
}
