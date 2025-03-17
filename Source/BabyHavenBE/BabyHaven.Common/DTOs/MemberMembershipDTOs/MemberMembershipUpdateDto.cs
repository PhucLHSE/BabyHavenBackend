using BabyHaven.Common.Enum.MemberMembershipEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.MemberMembershipDTOs
{
    public class MemberMembershipUpdateDto
    {
        [Required(ErrorMessage = "MemberMembershipId is required.")]
        public Guid MemberMembershipId { get; set; }

        public string MemberName { get; set; } = string.Empty;

        public string PackageName { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MemberMembershipStatus Status { get; set; } = MemberMembershipStatus.Inactive;

        [Required(ErrorMessage = "IsActive is required.")]
        public bool IsActive { get; set; }

        [MaxLength(2000, ErrorMessage = "Description cannot exceed 2000 characters.")]
        public string Description { get; set; } = string.Empty;
    }
}
