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
    public class MemberCreateDto
    {
        [Required(ErrorMessage = "UserId is required.")]
        public Guid UserId { get; set; }

        public int PackageId { get; set; }

        public string EmergencyContact { get; set; } = string.Empty;


        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MemberStatus Status { get; set; } = MemberStatus.Active;

        public DateTime JoinDate { get; set; }

        public DateTime? LeaveDate { get; set; }

        public string Notes { get; set; } = string.Empty;
    }
}
