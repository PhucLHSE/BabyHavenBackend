﻿using BabyHaven.Common.Enum.ConsultationRequestEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.ConsultationRequestDTOs
{
    public class ConsultationRequestDeleteDto
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

        public List<string> Attachments { get; set; } = new List<string>();


        // Timestamps for tracking changes
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
