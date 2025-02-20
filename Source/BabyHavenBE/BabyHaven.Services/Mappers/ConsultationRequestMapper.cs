using BabyHaven.Common.DTOs.ConsultationRequestDTOs;
using BabyHaven.Common.Enum.ConsultationRequestEnums;
using BabyHaven.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.Mappers
{
    public static class ConsultationRequestMapper
    {
        // Mapper TransactionViewAllDto
        public static ConsultationRequestViewAllDto MapToConsultationRequestViewAllDto(this ConsultationRequest model)
        {
            return new ConsultationRequestViewAllDto
            {
                MemberName = model.Member?.User?.Name ?? "Unknown",
                ChildName = model.Child?.Name ?? "Unknown",

                RequestDate = model.RequestDate,

                // Convert Status from string to enum
                Status = Enum.TryParse<ConsultationRequestStatus>(model.Status, true, out var status)
                          ? status
                          : ConsultationRequestStatus.Pending,

                // Convert Urgency from string to enum
                Urgency = Enum.TryParse<ConsultationRequestUrgency>(model.Urgency, true, out var urgency)
                          ? urgency
                          : ConsultationRequestUrgency.Low,

                // Convert Category from string to enum
                Category = Enum.TryParse<ConsultationRequestCategory>(model.Category, true, out var category)
                          ? category
                          : ConsultationRequestCategory.Other
            };
        }
    }
}
