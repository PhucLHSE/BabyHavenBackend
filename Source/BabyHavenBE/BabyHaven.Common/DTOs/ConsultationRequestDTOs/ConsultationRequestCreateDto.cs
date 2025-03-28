﻿using BabyHaven.Common.Enum.ConsultationRequestEnums;
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
        //// Basic information about the consultation request
        //[Required(ErrorMessage = "MemberName is required.")]
        //[MaxLength(255, ErrorMessage = "MemberName cannot exceed 255 characters.")]
        //public string MemberName { get; set; } = string.Empty;

        //[Required(ErrorMessage = "ChildName is required.")]
        //[MaxLength(255, ErrorMessage = "ChildName cannot exceed 255 characters.")]
        //public string ChildName { get; set; } = string.Empty;

        [Required]
        public Guid MemberId { get; set; }

        [Required]
        public string ChildName { get; set; } = string.Empty;

        [Required]
        public string ChildBirth { get; set; } = string.Empty;

        public int DoctorId { get; set; }

        [Required(ErrorMessage = "RequestDate is required.")]
        //[FutureDate(ErrorMessage = "RequestDate must be today or in the future.")]
        public string RequestDate { get; set; } = string.Empty;

        // Status and categorization of the request
        [Required(ErrorMessage = "Status is required.")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ConsultationRequestStatus Status { get; set; } = ConsultationRequestStatus.Pending;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ConsultationRequestUrgency Urgency { get; set; } = ConsultationRequestUrgency.Low;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ConsultationRequestCategory Category { get; set; } = ConsultationRequestCategory.Other;


        // Detailed information about the request
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; } = string.Empty;
        public List<AttachmentDto> Attachments { get; set; } = new List<AttachmentDto>();
    }

    public class AttachmentDto
    {
        [Required(ErrorMessage = "FileName is required.")]
        public string FileName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; } = string.Empty;

        [Required(ErrorMessage = "MimeType is required.")]
        public string MimeType { get; set; } = string.Empty; //File type
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
