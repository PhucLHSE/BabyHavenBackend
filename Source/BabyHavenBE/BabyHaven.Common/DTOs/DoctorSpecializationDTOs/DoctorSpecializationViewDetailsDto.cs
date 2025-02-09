using BabyHaven.Common.Enum.DoctorSpecializationEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.DoctorSpecializationDTOs
{
    public class DoctorSpecializationViewDetailsDto
    {
        //Doctor details
        public string DoctorName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Degree { get; set; } = string.Empty;

        public string HospitalName { get; set; } = string.Empty;

        public string HospitalAddress { get; set; } = string.Empty;

        public string Biography { get; set; } = string.Empty;

        //Specialization details
        public string SpecializationName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        // DoctorSpecialization details
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        // DoctorSpecialization Status
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DoctorSpecializationStatus Status { get; set; } = DoctorSpecializationStatus.Inactive;
    }
}
