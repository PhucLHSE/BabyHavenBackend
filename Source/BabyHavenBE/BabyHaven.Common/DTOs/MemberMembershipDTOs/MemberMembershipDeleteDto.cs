using BabyHaven.Common.Enum.MemberMembershipEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.MemberMembershipDTOs
{
    public class MemberMembershipDeleteDto
    {
        public string MemberName { get; set; } = string.Empty;

        public string PackageName { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MemberMembershipStatus Status { get; set; } = MemberMembershipStatus.Inactive;

        public bool IsActive { get; set; }

        public string Description { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
