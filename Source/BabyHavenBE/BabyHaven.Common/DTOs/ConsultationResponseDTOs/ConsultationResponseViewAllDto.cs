﻿using BabyHaven.Common.Enum.ConsultationResponseEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.ConsultationResponseDTOs
{
    public class ConsultationResponseViewAllDto
    {
        // Basic information about the consultation response
        public string RequestName { get; set; } = string.Empty;

        public string DoctorName { get; set; } = string.Empty ;

        public DateTime ResponseDate { get; set; }

        // Detailed information about the response
        public string Content { get; set; } = string.Empty;   

        public bool? IsHelpful { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ConsultationResponseStatus Status { get; set; } = ConsultationResponseStatus.Pending;
    }
}
