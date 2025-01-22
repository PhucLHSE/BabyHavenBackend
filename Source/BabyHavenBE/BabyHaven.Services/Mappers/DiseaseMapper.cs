using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common.DTOs.DiseaseDTOs;
using BabyHaven.Repositories.Models;

namespace BabyHaven.Services.Mappers
{
    public static class DiseaseMapper
    {
        public static DiseaseViewAllDto MapToDiseaseViewAllDto(this Disease model)
        {
            return new DiseaseViewAllDto
            {
                DiseaseId = model.DiseaseId,
                DiseaseName = model.DiseaseName,
                Description = model.Description,
                DiseaseType = model.DiseaseType,
                IsActive = model.IsActive,
                LowerBoundFemale = model.LowerBoundFemale,
                LowerBoundMale = model.LowerBoundMale,
                UpperBoundFemale = model.UpperBoundFemale,
                UpperBoundMale = model.UpperBoundMale,
                MaxAge = model.MaxAge,
                MinAge = model.MinAge,
                Severity = model.Severity,
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
                DiseaseType = model.DiseaseType,
                IsActive = model.IsActive,
                LowerBoundFemale = model.LowerBoundFemale,
                LowerBoundMale = model.LowerBoundMale,
                UpperBoundFemale = model.UpperBoundFemale,
                UpperBoundMale = model.UpperBoundMale,
                MaxAge = model.MaxAge,
                MinAge = model.MinAge,
                Severity = model.Severity,
                Symptoms = model.Symptoms,
                Treatment = model.Treatment,
                Prevention = model.Prevention,
                Notes = model.Notes,
                LastModified = model.LastModified,
                CreatedAt = model.CreatedAt
            };
        }

        public static Disease MapToMembershipPackageCreateDto(this DiseaseCreateDto model)
        {
            return new Disease
            {
                DiseaseName = model.DiseaseName,
                Description = model.Description,
                DiseaseType = model.DiseaseType,
                IsActive = model.IsActive,
                LowerBoundFemale = model.LowerBoundFemale,
                LowerBoundMale = model.LowerBoundMale,
                UpperBoundFemale = model.UpperBoundFemale,
                UpperBoundMale = model.UpperBoundMale,
                MaxAge = model.MaxAge,
                MinAge = model.MinAge,
                Severity = model.Severity,
                Symptoms = model.Symptoms,
                Treatment = model.Treatment,
                Prevention = model.Prevention,
                Notes = model.Notes
            };
        }
    }
}
