using BabyHaven.Common.Enum.ConsultationRequestEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.ConsultationRequestDTOs
{
    public class ConsultationRequestViewAllDto
    {
        public int RequestId { get; set; }

        public Guid MemberId { get; set; }

        public string MemberName { get; set; } = string.Empty;

        public string ChildName { get; set; } = string.Empty;

        public int DoctorId { get; set; }

        public DateTime RequestDate { get; set; }

        public string Description { get; set; } = string.Empty ;


        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ConsultationRequestStatus Status { get; set; } = ConsultationRequestStatus.Pending;


        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ConsultationRequestUrgency Urgency { get; set; } = ConsultationRequestUrgency.Low;


        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ConsultationRequestCategory Category { get; set; } = ConsultationRequestCategory.Other;
    }
}
