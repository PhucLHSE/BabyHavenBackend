using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.ChildrenDTOs
{
    public class ChildCreateForNowDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Member ID is required.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        public string DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [RegularExpression("^(Male|Female)$", ErrorMessage = "Gender must be Male or Female.")]
        public string Gender { get; set; } = string.Empty;

        [Required(ErrorMessage = "Birth Weight is required.")]
        [Range(0.5, 100.0, ErrorMessage = "Birth Weight must be between 0.5 and 100.0 kg.")]
        public double BirthWeight { get; set; }

        [Required(ErrorMessage = "Birth Height is required.")]
        [Range(20.0, 300.0, ErrorMessage = "Birth Height must be between 20.0 and 300.0 cm.")]
        public double BirthHeight { get; set; }

        [StringLength(3, ErrorMessage = "Blood Type can't be longer than 3 characters.")]
        public string BloodType { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Allergies can't be longer than 500 characters.")]
        public string Allergies { get; set; } = string.Empty;

        [StringLength(2000, ErrorMessage = "Notes can't be longer than 2000 characters.")]
        public string Notes { get; set; } = string.Empty;
    }
}
