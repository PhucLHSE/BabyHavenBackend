using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common.DTOs.GrowthRecordDTOs;
using BabyHaven.Repositories.Models;

namespace BabyHaven.Services.Mappers
{
    public static class GrowthRecordMapper
    {
        public static GrowthRecord MapToGrowthRecordEntity(this GrowthRecordCreateDto dto)
        {
            return new GrowthRecord
            {
                Weight = dto.Weight,
                Height = dto.Height,
                ChestCircumference = dto.ChestCircumference,
                MuscleMass = dto.MuscleMass,
                BloodSugarLevel = dto.BloodSugarLevel,
                Triglycerides = dto.Triglycerides,
                GrowthHormoneLevel = dto.GrowthHormoneLevel,
                MentalHealthStatus = dto.MentalHealthStatus,
                NeurologicalReflexes = dto.NeurologicalReflexes,
                DevelopmentalMilestones = dto.DevelopmentalMilestones,
                AttentionSpan = dto.AttentionSpan,
                Vision = dto.Vision,
                Hearing = dto.Hearing,
                PhysicalActivityLevel = dto.PhysicalActivityLevel,
                HeartRate = dto.HeartRate,
                BloodPressure = dto.BloodPressure,
                ImmunizationStatus = dto.ImmunizationStatus,
                NutritionalStatus = dto.NutritionalStatus,
                Notes = dto.Notes,
                BodyTemperature = dto.BodyTemperature,
                OxygenSaturation = dto.OxygenSaturation,
                SleepDuration = dto.SleepDuration,
                FerritinLevel = dto.FerritinLevel,
                HeadCircumference = dto.HeadCircumference
            };
        }
        // Required
        public static GrowthRecord MapToGrowthRecordEntity(this GrowthRecordRequiredDto dto)
        {
            return new GrowthRecord
            {
                ChildId = dto.ChildID,
                RecordedBy = dto.RecordedBy,
                Weight = dto.Weight,
                Height = dto.Height,
                DevelopmentalMilestones = dto.DevelopmentalMilestones,
                Notes = dto.Notes
            };
        }
        public static void MapToUpdatedGrowthRecord(this GrowthRecord growthRecord, GrowthRecordUpdateDto updateDto)
        {
            // Cập nhật Weight nếu có
            if (updateDto.Weight.HasValue)
                growthRecord.Weight = updateDto.Weight.Value;

            // Cập nhật Height nếu có
            if (updateDto.Height.HasValue)
                growthRecord.Height = updateDto.Height.Value;

            // Cập nhật HeadCircumference nếu có
            if (updateDto.HeadCircumference.HasValue)
                growthRecord.HeadCircumference = updateDto.HeadCircumference.Value;

            // Cập nhật MuscleMass nếu có
            if (updateDto.MuscleMass.HasValue)
                growthRecord.MuscleMass = updateDto.MuscleMass.Value;

            // Cập nhật NutritionalStatus nếu có
            if (!string.IsNullOrEmpty(updateDto.NutritionalStatus))
                growthRecord.NutritionalStatus = updateDto.NutritionalStatus;

            // Cập nhật FerritinLevel nếu có
            if (updateDto.FerritinLevel.HasValue)
                growthRecord.FerritinLevel = updateDto.FerritinLevel.Value;

            // Cập nhật Triglycerides nếu có
            if (updateDto.Triglycerides.HasValue)
                growthRecord.Triglycerides = updateDto.Triglycerides.Value;

            // Cập nhật BloodSugarLevel nếu có
            if (updateDto.BloodSugarLevel.HasValue)
                growthRecord.BloodSugarLevel = updateDto.BloodSugarLevel.Value;

            // Cập nhật PhysicalActivityLevel nếu có
            if (!string.IsNullOrEmpty(updateDto.PhysicalActivityLevel))
                growthRecord.PhysicalActivityLevel = updateDto.PhysicalActivityLevel;

            // Cập nhật HeartRate nếu có
            if (updateDto.HeartRate.HasValue)
                growthRecord.HeartRate = updateDto.HeartRate.Value;

            // Cập nhật BloodPressure nếu có
            if (updateDto.BloodPressure.HasValue)
                growthRecord.BloodPressure = updateDto.BloodPressure.Value;

            // Cập nhật BodyTemperature nếu có
            if (updateDto.BodyTemperature.HasValue)
                growthRecord.BodyTemperature = updateDto.BodyTemperature.Value;

            // Cập nhật OxygenSaturation nếu có
            if (updateDto.OxygenSaturation.HasValue)
                growthRecord.OxygenSaturation = updateDto.OxygenSaturation.Value;

            // Cập nhật SleepDuration nếu có
            if (updateDto.SleepDuration.HasValue)
                growthRecord.SleepDuration = updateDto.SleepDuration.Value;

            // Cập nhật Vision nếu có
            if (!string.IsNullOrEmpty(updateDto.Vision))
                growthRecord.Vision = updateDto.Vision;

            // Cập nhật Hearing nếu có
            if (!string.IsNullOrEmpty(updateDto.Hearing))
                growthRecord.Hearing = updateDto.Hearing;

            // Cập nhật ImmunizationStatus nếu có
            if (!string.IsNullOrEmpty(updateDto.ImmunizationStatus))
                growthRecord.ImmunizationStatus = updateDto.ImmunizationStatus;

            // Cập nhật MentalHealthStatus nếu có
            if (!string.IsNullOrEmpty(updateDto.MentalHealthStatus))
                growthRecord.MentalHealthStatus = updateDto.MentalHealthStatus;

            // Cập nhật GrowthHormoneLevel nếu có
            if (updateDto.GrowthHormoneLevel.HasValue)
                growthRecord.GrowthHormoneLevel = updateDto.GrowthHormoneLevel.Value;

            // Cập nhật AttentionSpan nếu có
            if (!string.IsNullOrEmpty(updateDto.AttentionSpan))
                growthRecord.AttentionSpan = updateDto.AttentionSpan;

            // Cập nhật NeurologicalReflexes nếu có
            if (!string.IsNullOrEmpty(updateDto.NeurologicalReflexes))
                growthRecord.NeurologicalReflexes = updateDto.NeurologicalReflexes;
        }

        //View All
        public static GrowthRecordViewAllDto MapToGrowthRecordViewAll(this GrowthRecord model)
        {
            return new GrowthRecordViewAllDto
            {
                Weight = model.Weight,
                Height = model.Height,
                ChestCircumference = model.ChestCircumference,
                MuscleMass = model.MuscleMass,
                BloodSugarLevel = model.BloodSugarLevel,
                Triglycerides = model.Triglycerides,
                GrowthHormoneLevel = model.GrowthHormoneLevel,
                MentalHealthStatus = model.MentalHealthStatus,
                NeurologicalReflexes = model.NeurologicalReflexes,
                DevelopmentalMilestones = model.DevelopmentalMilestones,
                AttentionSpan = model.AttentionSpan,
                Vision = model.Vision,
                Hearing = model.Hearing,
                PhysicalActivityLevel = model.PhysicalActivityLevel,
                HeartRate = model.HeartRate,
                BloodPressure = model.BloodPressure,
                ImmunizationStatus = model.ImmunizationStatus,
                NutritionalStatus = model.NutritionalStatus,
                Notes = model.Notes,
            };
        }
        
        public static GrowthRecordViewRequiredDto MapToViewRequired(this GrowthRecord model)
        {
            return new GrowthRecordViewRequiredDto
            {
                Height = model.Height,
                Weight = model.Weight,
                DevelopmentalMilestones = model.DevelopmentalMilestones,
                Notes = model.Notes
            };
        }
    }
}

