using BabyHaven.Common.Enum.MemberEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.MemberDTOs
{
    public class MemberUpdateDto
    {
        [Required(ErrorMessage = "MemberId is required.")]
        public Guid MemberId { get; set; }

        [Required(ErrorMessage = "UserId is required.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "EmergencyContact is required.")]
        [MaxLength(255, ErrorMessage = "EmergencyContact cannot exceed 255 characters.")]
        public string EmergencyContact { get; set; } = string.Empty;

        [Required(ErrorMessage = "Status is required.")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MemberStatus Status { get; set; } = MemberStatus.Inactive;

        [Required(ErrorMessage = "JoinDate is required.")]
        public DateTime JoinDate { get; set; }

        public DateTime? LeaveDate { get; set; }

        [MaxLength(2000, ErrorMessage = "Notes cannot exceed 2000 characters.")]
        public string? Notes { get; set; } = string.Empty;
    }
}
