﻿using BabyHaven.Common.DTOs.ChildrenDTOs;
using BabyHaven.Common.DTOs.ConsultationRequestDTOs;
using BabyHaven.Common.DTOs.GrowthRecordDTOs;
using BabyHaven.Common.Enum.ConsultationRequestEnums;
using BabyHaven.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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
                RequestId = model.RequestId,

                MemberId = model.MemberId,

                MemberName = model.Member?.User?.Name ?? "Unknown",

                ChildName = model.Child?.Name ?? "Unknown",

                DoctorId = model.DoctorId,

                RequestDate = model.RequestDate,

                IsActive = model.IsActive,

                Description = model.Description,

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

        // Mapper for ConsultationRequestViewDetailsDto
        public static ConsultationRequestViewDetailsDto MapToConsultationRequestViewDetailsDto(this ConsultationRequest model)
        {
            return new ConsultationRequestViewDetailsDto
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
                          : ConsultationRequestCategory.Other,

                // Description of the consultation request, defaulting to an empty string if null
                Description = model.Description ?? string.Empty,

                // Attachments of the consultation request, defaulting to an empty list if null
                Attachments = model.Attachments,

                // Audit Information
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,

                // Map Child details
                Child = model.Child?.ToChildViewDetailsDto() ?? new ChildViewDetailsDto(),

                //// Map recent growth records
                //RecentGrowthRecords = model.Child?.GrowthRecords?
                //    .OrderByDescending(gr => gr.UpdatedAt)
                //    .Take(5)
                //    .Select(gr => gr.MapToGrowthRecordViewAll())
                //    .ToList() ?? new List<GrowthRecordViewAllDto>(),

                //// Map previous consultation requests
                //PreviousConsultations = model.Child?.ConsultationRequests?
                //    .Where(cr => cr.RequestId != model.RequestId) // Exclude current request
                //    .OrderByDescending(cr => cr.RequestDate)
                //    .Take(5)
                //    .Select(cr => cr.MapToConsultationRequestViewAllDto())
                //    .ToList() ?? new List<ConsultationRequestViewAllDto>()
            };
        }

        // Mapper for ConsultationRequestCreateDto
        public static ConsultationRequest MapToConsultationRequest(this ConsultationRequestCreateDto dto, int doctorId, Child child)
        {
            var attachmentsList = dto.Attachments.Select(att => new
            {
                FileName = att.FileName,
                Content = att.Content,
                MimeType = att.MimeType
            }).ToList();

            return new ConsultationRequest
            {
                MemberId = child.MemberId,
                ChildId = child.ChildId,
                DoctorId = doctorId,

                RequestDate = DateTime.Parse(dto.RequestDate),
                Status = dto.Status.ToString(),
                Urgency = dto.Urgency.ToString(),
                Category = dto.Category.ToString(),
                Description = dto.Description,
                Attachments = JsonSerializer.Serialize(attachmentsList),

                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        // Mapper for ConsultationRequestDeleteDto
        public static ConsultationRequestDeleteDto MapToConsultationRequestDeleteDto(this ConsultationRequest model)
        {
            return new ConsultationRequestDeleteDto
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
                          : ConsultationRequestCategory.Other,

                // Description of the consultation request, defaulting to an empty string if null
                Description = model.Description ?? string.Empty,

                // Attachments of the consultation request, defaulting to an empty list if null
                Attachments = string.IsNullOrEmpty(model.Attachments)
                          ? new List<string>()
                          : JsonSerializer.Deserialize<List<string>>(model.Attachments)
                          ?? new List<string>(),

                // Audit Information
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,
            };
        }
    }
}
