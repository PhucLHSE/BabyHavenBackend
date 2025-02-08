using BabyHaven.Common.Enum.SpecializationEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.SpecializationDTOs
{
    public class SpecializationCreateDto
    {
        [Required(ErrorMessage = "SpecializationName is required.")]
        [MaxLength(255, ErrorMessage = "SpecializationName cannot exceed 255 characters.")]
        public string SpecializationName { get; set; } = string.Empty;


        [MaxLength(2000, ErrorMessage = "Description cannot exceed 2000 characters.")]
        public string Description { get; set; } = string.Empty ;

        [Required(ErrorMessage = "Status is required.")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SpecializationStatus Status { get; set; } = SpecializationStatus.Inactive;

    }
}
