using BabyHaven.Common.Enum.DoctorSpecializationEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.DoctorSpecializationDTOs
{
    public class DoctorSpecializationViewAllDto
    {
        //Doctor
        public string DoctorName { get; set; } = string.Empty;

        //Specialization
        public string SpecializationName { get; set; } = string.Empty;


        // DoctorSpecialization Status
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DoctorSpecializationStatus Status { get; set; } = DoctorSpecializationStatus.Inactive;
    }
}
