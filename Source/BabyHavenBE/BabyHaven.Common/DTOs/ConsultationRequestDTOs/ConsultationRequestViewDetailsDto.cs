using BabyHaven.Common.DTOs.ChildrenDTOs;
using BabyHaven.Common.DTOs.GrowthRecordDTOs;
using BabyHaven.Common.Enum.ConsultationRequestEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.ConsultationRequestDTOs
{
    public class ConsultationRequestViewDetailsDto
    {
        // Basic information about the consultation request
        public string MemberName { get; set; } = string.Empty;

        public string ChildName { get; set; } = string.Empty;

        public DateTime RequestDate { get; set; }


        // Status and categorization of the request
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ConsultationRequestStatus Status { get; set; } = ConsultationRequestStatus.Pending;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ConsultationRequestUrgency Urgency { get; set; } = ConsultationRequestUrgency.Low;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ConsultationRequestCategory Category { get; set; } = ConsultationRequestCategory.Other;


        // Detailed information about the request
        public string Description { get; set; } = string.Empty;

        public string Attachments { get; set; }


        // Timestamps for tracking changes
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }


        // Child information
        public ChildViewDetailsDto? Child { get; set; }


        // Growth history of the child
        public List<GrowthRecordViewAllDto> RecentGrowthRecords { get; set; } = new List<GrowthRecordViewAllDto>();


        // Previous consultation requests related to the child
        public List<ConsultationRequestViewAllDto> PreviousConsultations { get; set; } = new List<ConsultationRequestViewAllDto>();
    }
}
