using BabyHaven.Common.Enum.DoctorEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.DoctorDTOs
{
    public class DoctorViewAllDto
    {
        public string UserName { get; set; } = string.Empty;


        public string Name { get; set; } = string.Empty;


        public string Email { get; set; } = string.Empty;


        public string PhoneNumber { get; set; } = string.Empty;


        public string Degree { get; set; } = string.Empty;


        public string HospitalName { get; set; } = string.Empty;


        public string HospitalAddress { get; set; } = string.Empty;


        public string Biography { get; set; } = string.Empty;


        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DoctorStatus Status { get; set; } = DoctorStatus.Inactive;
    }
}
