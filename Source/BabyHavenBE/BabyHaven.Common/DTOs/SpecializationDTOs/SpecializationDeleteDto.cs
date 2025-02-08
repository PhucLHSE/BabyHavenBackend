using BabyHaven.Common.Enum.SpecializationEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.SpecializationDTOs
{
    public class SpecializationDeleteDto
    {
        public string SpecializationName { get; set; }

        public string Description { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SpecializationStatus Status { get; set; } = SpecializationStatus.Inactive;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
