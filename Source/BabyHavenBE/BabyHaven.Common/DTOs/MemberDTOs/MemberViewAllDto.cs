﻿using BabyHaven.Common.Enum.MemberEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.MemberDTOs
{
    public class MemberViewAllDto
    {
        public Guid MemberId { get; set; }
        public Guid UserId { get; set; }
        public string MemberName { get; set; } = string.Empty;

        public string EmergencyContact { get; set; } = string.Empty;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MemberStatus Status { get; set; } = MemberStatus.Inactive;

        public DateTime JoinDate { get; set; }

        public string Notes { get; set; }
    }
}
