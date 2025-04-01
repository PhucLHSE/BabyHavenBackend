using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common.DTOs.MilestoneDTOs;
using BabyHaven.Common.DTOs.MilestoneDTOS;
using BabyHaven.Repositories.Models;

namespace BabyHaven.Services.Mappers
{
    public static class MilestoneMapper
    {
        public static Milestone ToMilestone(this MilestoneCreateDto dto)
        {
            return new Milestone
            {
                MilestoneName = dto.MilestoneName,
                Description = dto.Description,
                Importance = dto.Importance,
                Category = dto.Category,
                MinAge = dto.IsPersonal ? 0 : dto.MinAge,
                MaxAge = dto.IsPersonal ? 0 : dto.MaxAge,
                IsPersonal = dto.IsPersonal,
            };
        }

        public static Milestone ToMilestone(this MilestoneUpdateDto dto, Milestone existingMilestone)
        {
            if (!string.IsNullOrEmpty(dto.MilestoneName))
            {
                existingMilestone.MilestoneName = dto.MilestoneName;
            }

            if (!string.IsNullOrEmpty(dto.Description))
            {
                existingMilestone.Description = dto.Description;
            }

            if (!string.IsNullOrEmpty(dto.Importance))
            {
                existingMilestone.Importance = dto.Importance;
            }

            if (!string.IsNullOrEmpty(dto.Category))
            {
                existingMilestone.Category = dto.Category;
            }

            if (dto.MinAge.HasValue)
            {
                existingMilestone.MinAge = dto.MinAge;
            }

            if (dto.MaxAge.HasValue)
            {
                existingMilestone.MaxAge = dto.MaxAge;
            }

            existingMilestone.IsPersonal = dto.IsPersonal;
            existingMilestone.UpdatedAt = DateTime.UtcNow;

            return existingMilestone;
        }

        public static MilestoneViewAllDto ToMilestoneViewAllDto(this Milestone milestone)
        {
            return new MilestoneViewAllDto
            {
                MilestoneName = milestone.MilestoneName,
                Description = milestone.Description,
                Importance = milestone.Importance,
                Category = milestone.Category,
                MinAge = milestone.MinAge,
                MaxAge = milestone.MaxAge,
                IsPersonal = milestone.IsPersonal
            };
        }

        public static MilestoneViewDetailsDto ToMilestoneViewDetailsDto(this Milestone milestone)
        {
            return new MilestoneViewDetailsDto
            {
                MilestoneId = milestone.MilestoneId,
                MilestoneName = milestone.MilestoneName,
                Description = milestone.Description,
                Importance = milestone.Importance,
                Category = milestone.Category,
                MinAge = milestone.MinAge,
                MaxAge = milestone.MaxAge,
                IsPersonal = milestone.IsPersonal,
                CreatedAt = milestone.CreatedAt,
                UpdatedAt = milestone.UpdatedAt
            };
        }
    }
}
