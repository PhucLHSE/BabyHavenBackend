﻿using BabyHaven.Common.DTOs.ConsultationRequestDTOs;
using BabyHaven.Common.DTOs.GrowthRecordDTOs;
using BabyHaven.Common.Enum.ConsultationResponseEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.ConsultationResponseDTOs
{
    public class ConsultationResponseViewDetailsDto
    {
        // Basic information about the consultation response
        public int RequestId { get; set; }

        public string DoctorName { get; set; } = string.Empty;

        public DateTime ResponseDate { get; set; }

        // Detailed information about the response
        public string Content { get; set; } = string.Empty;

        public List<string> Attachments { get; set; } = new List<string>();

        public bool? IsHelpful { get; set; }


        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ConsultationResponseStatus Status { get; set; } = ConsultationResponseStatus.Pending;

        // Timestamps for tracking changes
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        // Previous consultation responses related to the request
        public List<ConsultationResponseViewAllDto> PreviousConsultations { get; set; } = new List<ConsultationResponseViewAllDto>();
    }
}
