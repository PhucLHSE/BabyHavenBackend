using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common.DTOs.DiseaseDTOs;
using BabyHaven.Common.Enum.AlertEnums;
using BabyHaven.Repositories.Models;

namespace BabyHaven.Services.Mappers
{
    public static class DiseaseMapper
    {
        public static DiseaseViewAllDto MapToDiseaseViewAllDto(this Disease model)
        {
            return new DiseaseViewAllDto
            {
                DiseaseName = model.DiseaseName,
                Description = model.Description,
                DiseaseType = model.DiseaseType ?? "Other",
                LowerBoundFemale = model.LowerBoundFemale,
                LowerBoundMale = model.LowerBoundMale,
                UpperBoundFemale = model.UpperBoundFemale,
                UpperBoundMale = model.UpperBoundMale,
                MaxAge = model.MaxAge,
                MinAge = model.MinAge,
                Severity = Enum.TryParse(model.Severity, out SeverityLevelEnum severity) ? severity : SeverityLevelEnum.Mild,
                Symptoms = model.Symptoms,
                Treatment = model.Treatment,
                Prevention = model.Prevention,
                Notes = model.Notes
            };
        }

        public static DiseaseViewDetailsDto MapToDiseaseViewDetailDto(this Disease model)
        {
            return new DiseaseViewDetailsDto
            {
                DiseaseId = model.DiseaseId,
                DiseaseName = model.DiseaseName,
                Description = model.Description,
                DiseaseType = model.DiseaseType ?? "Other",
                IsActive = model.IsActive,
                LowerBoundFemale = model.LowerBoundFemale,
                LowerBoundMale = model.LowerBoundMale,
                UpperBoundFemale = model.UpperBoundFemale,
                UpperBoundMale = model.UpperBoundMale,
                MaxAge = model.MaxAge,
                MinAge = model.MinAge,
                Severity = Enum.TryParse(model.Severity, out SeverityLevelEnum severity) ? severity : SeverityLevelEnum.Mild,
                Symptoms = model.Symptoms,
                Treatment = model.Treatment,
                Prevention = model.Prevention,
                Notes = model.Notes,
                LastModified = model.LastModified,
                CreatedAt = model.CreatedAt
            };
        }

        public static Disease MapToDiseaseCreateEntity(this DiseaseCreateDto dto)
        {
            return new Disease
            {
                DiseaseName = dto.DiseaseName,
                Description = dto.Description,
                DiseaseType = dto.DiseaseType,
                IsActive = true,
                LowerBoundFemale = dto.LowerBoundFemale,
                LowerBoundMale = dto.LowerBoundMale,
                UpperBoundFemale = dto.UpperBoundFemale,
                UpperBoundMale = dto.UpperBoundMale,
                MaxAge = dto.MaxAge,
                MinAge = dto.MinAge,
                Severity = dto.Severity.ToString(),
                Symptoms = dto.Symptoms,
                Treatment = dto.Treatment,
                Prevention = dto.Prevention,
                Notes = dto.Notes,
            };
        }

        public static Disease MapToDiseaseUpdateEntity(this DiseaseUpdateDto dto)
        {
            return new Disease
            {
                DiseaseName = dto.DiseaseName,
                Description = dto.Description,
                DiseaseType = dto.DiseaseType,
                IsActive = dto.IsActive,
                LowerBoundFemale = dto.LowerBoundFemale,
                LowerBoundMale = dto.LowerBoundMale,
                UpperBoundFemale = dto.UpperBoundFemale,
                UpperBoundMale = dto.UpperBoundMale,
                MaxAge = dto.MaxAge,
                MinAge = dto.MinAge,
                Severity = dto.Severity.ToString(),
                Symptoms = dto.Symptoms,
                Treatment = dto.Treatment,
                Prevention = dto.Prevention,
                Notes = dto.Notes,
                LastModified = DateTime.UtcNow
            };
        }
    }
}
