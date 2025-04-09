using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BabyHaven.Common.Enum.Converters;

namespace BabyHaven.Common.DTOs.ChildrenDTOs
{
    public class ChildCreateDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Member ID is required.")]
        public Guid MemberId { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        public string DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [RegularExpression("^(Male|Female)$", ErrorMessage = "Gender must be Male or Female.")]
        public string Gender { get; set; } = string.Empty;

        [Range(0, 10.0, ErrorMessage = "Birth Weight must be between 0 and 10.0 kg.")]
        public double BirthWeight { get; set; }

        [Range(0, 60.0, ErrorMessage = "Birth Height must be between 0 and 10.0 cm.")]
        public double BirthHeight { get; set; }

        [StringLength(2000, ErrorMessage = "Notes can't be longer than 2000 characters.")]
        public string Notes { get; set; } = string.Empty;
    }
}
