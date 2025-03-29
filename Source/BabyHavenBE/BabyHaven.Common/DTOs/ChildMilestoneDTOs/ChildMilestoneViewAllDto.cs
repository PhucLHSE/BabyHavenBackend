using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BabyHaven.Common.Enum.Converters;

namespace BabyHaven.Common.DTOs.ChildMilestoneDTOs
{
    public class ChildMilestoneViewAllDto
    {
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly? AchievedDate { get; set; }
        public string ChildName { get; set; }
        public string DateOfBirth { get; set; }
        public Guid MemberId { get; set; }

        public string Status { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        public string Guidelines { get; set; } = string.Empty;

        public string Importance { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;
    }
}
