﻿using BabyHaven.Common.Enum.RoleEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.RoleDTOs
{
    public class RoleViewAllDto
    {
        public string RoleName { get; set; } = string.Empty;


        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RoleStatus Status { get; set; } = RoleStatus.Inactive;
    }
}
