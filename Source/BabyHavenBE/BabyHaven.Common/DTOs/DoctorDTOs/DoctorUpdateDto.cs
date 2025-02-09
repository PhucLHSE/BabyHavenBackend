using BabyHaven.Common.Enum.DoctorEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.DoctorDTOs
{
    public class DoctorUpdateDto
    {
        [Required(ErrorMessage = "DoctorId is required.")]
        public int DoctorId { get; set; }

        [Required(ErrorMessage = "UserName is required.")]
        [MaxLength(255, ErrorMessage = "UserName cannot exceed 255 characters.")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(255, ErrorMessage = "Name cannot exceed 255 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(255, ErrorMessage = "Email cannot exceed 255 characters.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "PhoneNumber is required.")]
        [MaxLength(20, ErrorMessage = "PhoneNumber cannot exceed 20 characters.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Degree is required.")]
        [MaxLength(255, ErrorMessage = "Degree cannot exceed 255 characters.")]
        public string Degree { get; set; } = string.Empty;

        [Required(ErrorMessage = "HospitalName is required.")]
        [MaxLength(255, ErrorMessage = "HospitalName cannot exceed 255 characters.")]
        public string HospitalName { get; set; } = string.Empty;

        [Required(ErrorMessage = "HospitalAddress is required.")]
        [MaxLength(255, ErrorMessage = "HospitalAddress cannot exceed 255 characters.")]
        public string HospitalAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "Biography is required.")]
        [MaxLength(2000, ErrorMessage = "Biography cannot exceed 2000 characters.")]
        public string Biography { get; set; } = string.Empty;

        [Required(ErrorMessage = "Status is required.")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DoctorStatus? Status { get; set; } = DoctorStatus.Inactive;
    }
}
