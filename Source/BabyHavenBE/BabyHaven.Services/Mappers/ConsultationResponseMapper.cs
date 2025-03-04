using BabyHaven.Common.DTOs.ConsultationRequestDTOs;
using BabyHaven.Common.DTOs.ConsultationResponseDTOs;
using BabyHaven.Common.Enum.ConsultationResponseEnums;
using BabyHaven.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BabyHaven.Services.Mappers
{
    public static class ConsultationResponseMapper
    {
        // Mapper ConsultationResponseViewAllDto
        public static ConsultationResponseViewAllDto MapToConsultationResponseViewAllDto(this ConsultationResponse model)
        {
            return new ConsultationResponseViewAllDto
            {
                //DoctorName = model.Doctor?.User?.Name ?? "Unknown",
                RequestName = model.Request?.Description ?? "Unknown",

                ResponseDate = model.ResponseDate,
                Content = model.Content ?? string.Empty,
                IsHelpful = model.IsHelpful,
                // Convert Status from string to enum
                Status = Enum.TryParse<ConsultationResponseStatus>(model.Status, true, out var status)
                          ? status
                          : ConsultationResponseStatus.Pending,

              
            };
        }

        //Mapper for ConsultationResponseViewDetailsDto
        public static ConsultationResponseViewDetailsDto MapToConsultationResponseViewDetailsDto(this ConsultationResponse model)
        {
            return new ConsultationResponseViewDetailsDto
            {
                //DoctorName = model.Doctor?.User?.Name ?? "Unknown",
                RequestName = model.Request?.Description ?? "Unknown",

                ResponseDate = model.ResponseDate,
                Content = model.Content,
                IsHelpful = model.IsHelpful,
                // Convert Status from string to enum
                Status = Enum.TryParse<ConsultationResponseStatus>(model.Status, true, out var status)
                          ? status
                          : ConsultationResponseStatus.Pending,

                // Attachments of the consultation response, defaulting to an empty list if null
                Attachments = string.IsNullOrEmpty(model.Attachments)
                          ? new List<string>()
                          : JsonSerializer.Deserialize<List<string>>(model.Attachments)
                          ?? new List<string>(),

                // Audit Information
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,

                //// Map previous consultation requests
                //PreviousConsultations = model.Request?.ConsultationResponses?
                //    .Where(cr => cr.ResponseId != model.ResponseId) // Exclude current response
                //    .OrderByDescending(cr => cr.ResponseDate)
                //    .Take(5)
                //    .Select(cr => cr.MapToConsultationResponseViewAllDto())
                //    .ToList() ?? new List<ConsultationResponseViewAllDto>()
            };
        }

        // Mapper for ConsultationResponseCreateDto
        public static ConsultationResponse MapToConsultationResponse(this ConsultationResponseCreateDto dto, int doctorId, int requestId)
        {
            return new ConsultationResponse
            {
                //DoctorId = doctorId,
                RequestId = requestId,

                ResponseDate = dto.ResponseDate,
                Status = dto.Status.ToString(),
                Content = dto.Content.ToString(),
                IsHelpful = dto.IsHelpful,
                Attachments = dto.Attachments != null && dto.Attachments.Count > 0
                    ? JsonSerializer.Serialize(dto.Attachments)
                    : string.Empty,

                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        // Mapper for ConsultationResponseDeleteDto
        public static ConsultationResponseDeleteDto MapToConsultationResponseDeleteDto(this ConsultationResponse model)
        {
            return new ConsultationResponseDeleteDto
            {
                //DoctorName = model.Doctor?.User?.Name ?? "Unknown",
                RequestName = model.Request?.Description ?? "Unknown",

                ResponseDate = model.ResponseDate,

                // Convert Status from string to enum
                Status = Enum.TryParse<ConsultationResponseStatus>(model.Status, true, out var status)
                          ? status
                          : ConsultationResponseStatus.Pending,

                // Description of the consultation response, defaulting to an empty string if null
                Content = model.Content ?? string.Empty,

                // Attachments of the consultation response, defaulting to an empty list if null
                Attachments = string.IsNullOrEmpty(model.Attachments)
                          ? new List<string>()
                          : JsonSerializer.Deserialize<List<string>>(model.Attachments)
                          ?? new List<string>(),
                IsHelpful = model.IsHelpful,
                // Audit Information
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,
            };
        }
    }
}
