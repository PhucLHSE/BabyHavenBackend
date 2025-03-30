using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common.DTOs.ChildMilestoneDTOs;
using BabyHaven.Repositories.Models;

namespace BabyHaven.Services.Mappers
{
    public static class ChildMilestoneMapper
    {
        public static ChildMilestone ToChildMilestone(this ChildMilestoneCreateDto dto)
        {
            return new ChildMilestone
            {
                MilestoneId = dto.MilestoneId,
                Notes = dto.Notes,
                Guidelines = dto.Guidelines,
                Importance = dto.Importance,
                Category = dto.Category,
                AchievedDate = DateOnly.Parse(dto.AchievedDate),
                Status = dto.AchievedDate == null ? "In Progress" : "Completed",
            };
        }

        public static ChildMilestone ToChildMilestone(this ChildMilestoneUpdateDto dto, ChildMilestone existingChildMilestone)
        {
            existingChildMilestone.MilestoneId = dto.MilestoneId;
            existingChildMilestone.AchievedDate = dto.AchievedDate;
            existingChildMilestone.Status = dto.Status;
            existingChildMilestone.Notes = dto.Notes;
            existingChildMilestone.Guidelines = dto.Guidelines;
            existingChildMilestone.Importance = dto.Importance;
            existingChildMilestone.Category = dto.Category;
            existingChildMilestone.UpdatedAt = DateTime.UtcNow;

            return existingChildMilestone;
        }

        public static ChildMilestoneViewAllDto ToChildMilestoneViewAllDto(this ChildMilestone childMilestone)
        {
            return new ChildMilestoneViewAllDto
            {
                AchievedDate = childMilestone.AchievedDate,
                Status = childMilestone.Status,
                Notes = childMilestone.Notes,
                Guidelines = childMilestone.Guidelines,
                Importance = childMilestone.Importance,
                Category = childMilestone.Category
            };
        }

        public static ChildMilestoneViewDetailsDto ToChildMilestoneViewDetailsDto(this ChildMilestone childMilestone)
        {
            return new ChildMilestoneViewDetailsDto
            {
                ChildId = childMilestone.ChildId,
                MilestoneId = childMilestone.MilestoneId,
                AchievedDate = childMilestone.AchievedDate,
                Status = childMilestone.Status,
                Notes = childMilestone.Notes,
                Guidelines = childMilestone.Guidelines,
                Importance = childMilestone.Importance,
                Category = childMilestone.Category,
                CreatedAt = childMilestone.CreatedAt,
                UpdatedAt = childMilestone.UpdatedAt
            };
        }
    }
}
