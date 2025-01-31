using BabyHaven.Common.Enum.RoleEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.RoleDTOs
{
    public class RoleCreateDto
    {
        [Required(ErrorMessage = "RoleName is required.")]
        [MaxLength(255, ErrorMessage = "RoleName cannot exceed 255 characters.")]
        public string RoleName { get; set; }= string.Empty;


        [MaxLength(2000, ErrorMessage = "Description cannot exceed 2000 characters.")]
        public string Description { get; set; } = string.Empty;


        [Required(ErrorMessage = "Status is required.")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RoleStatus Status { get; set; } = RoleStatus.Inactive;

    }
}
