using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.ChildrenDTOs
{
    public class ChildUpdateDto
    {
        [Required(ErrorMessage = "Child's Id is required.")]
        public Guid ChildId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date of Birth is required.")]
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [RegularExpression("^(Male|Female|Other)$", ErrorMessage = "Gender must be Male, Female, or Other.")]
        public string Gender { get; set; } = string.Empty;

        [StringLength(3, ErrorMessage = "Blood Type can't be longer than 3 characters.")]
        public string BloodType { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Allergies can't be longer than 500 characters.")]
        public string Allergies { get; set; } = string.Empty;

        [StringLength(2000, ErrorMessage = "Notes can't be longer than 2000 characters.")]
        public string Notes { get; set; } = string.Empty;

        [Required(ErrorMessage = "Relationship to Member is required.")]
        [StringLength(100, ErrorMessage = "Relationship to Member can't be longer than 100 characters.")]
        public string RelationshipToMember { get; set; } = string.Empty;

        public DateTime UpdatedAt { get; set; }
    }
}
