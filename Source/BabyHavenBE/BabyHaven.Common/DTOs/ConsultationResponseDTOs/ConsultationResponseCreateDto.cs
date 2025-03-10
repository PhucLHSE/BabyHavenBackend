using BabyHaven.Common.Enum.ConsultationResponseEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.ConsultationResponseDTOs
{
    public class ConsultationResponseCreateDto
    {
        // Basic information about the consultation response
        [Required(ErrorMessage = "RequestId is required.")]
        //[MaxLength(255, ErrorMessage = "RequestName cannot exceed 255 characters.")]
        public int RequestId { get; set; }

        [Required(ErrorMessage = "DoctorName is required.")]
        [MaxLength(255, ErrorMessage = "DoctorName cannot exceed 255 characters.")]
        public string DoctorName { get; set; } = string.Empty;

        [Required(ErrorMessage = "ResponseDate is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid date format.")]
        [FutureDate(ErrorMessage = "ResponseDate must be today or in the future.")]
        public DateTime ResponseDate { get; set; }

        // Detailed information about the response
        [Required(ErrorMessage = "Content is required.")]
        [MaxLength(2000, ErrorMessage = "Content cannot exceed 2000 characters.")]
        public string Content { get; set; } = string.Empty;

        [MaxLength(5, ErrorMessage = "Cannot attach more than 5 files.")]
        public List<string> Attachments { get; set; } = new List<string>();

        public bool? IsHelpful { get; set; }

        public ConsultationResponseStatus Status { get; set; } = ConsultationResponseStatus.Pending;
        // Custom validation attribute for future date
        public class FutureDateAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value is DateTime dateValue && dateValue < DateTime.UtcNow.Date)
                {
                    return new ValidationResult(ErrorMessage ?? "Date must be in the future.");
                }
                return ValidationResult.Success;
            }
        }
    }
}
