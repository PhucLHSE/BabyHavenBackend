using BabyHaven.Common.Enum.ConsultationRequestEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.ConsultationRequestDTOs
{
    public class ConsultationRequestCreateDto
    {
        // Basic information about the consultation request
        [Required(ErrorMessage = "MemberName is required.")]
        [MaxLength(255, ErrorMessage = "MemberName cannot exceed 255 characters.")]
        public string MemberName { get; set; } = string.Empty;

        [Required(ErrorMessage = "ChildName is required.")]
        [MaxLength(255, ErrorMessage = "ChildName cannot exceed 255 characters.")]
        public string ChildName { get; set; } = string.Empty;

        [Required(ErrorMessage = "RequestDate is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid date format.")]
        [FutureDate(ErrorMessage = "RequestDate must be today or in the future.")]
        public DateTime RequestDate { get; set; }


        // Status and categorization of the request
        [Required(ErrorMessage = "Status is required.")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ConsultationRequestStatus Status { get; set; } = ConsultationRequestStatus.Pending;

        [Required(ErrorMessage = "Urgency is required.")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ConsultationRequestUrgency Urgency { get; set; } = ConsultationRequestUrgency.Low;

        [Required(ErrorMessage = "Category is required.")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ConsultationRequestCategory Category { get; set; } = ConsultationRequestCategory.Other;


        // Detailed information about the request
        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(1000, ErrorMessage = "Description cannot exceed 2000 characters.")]
        public string Description { get; set; } = string.Empty;

        [MaxLength(5, ErrorMessage = "Cannot attach more than 5 files.")]
        public List<string> Attachments { get; set; } = new List<string>();
    }

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
