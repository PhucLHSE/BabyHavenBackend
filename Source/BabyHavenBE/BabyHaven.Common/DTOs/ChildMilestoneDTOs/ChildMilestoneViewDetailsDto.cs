using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BabyHaven.Common.Enum.Converters;

namespace BabyHaven.Common.DTOs.ChildMilestoneDTOs
{
    public class ChildMilestoneViewDetailsDto
    {
        public Guid ChildId { get; set; }

        public int MilestoneId { get; set; }

        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly? AchievedDate { get; set; }

        public string Status { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        public string Guidelines { get; set; } = string.Empty;

        public string Importance { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
