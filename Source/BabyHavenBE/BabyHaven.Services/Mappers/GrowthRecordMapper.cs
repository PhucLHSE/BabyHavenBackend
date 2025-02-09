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
        // Infant
        public static GrowthRecord MapToGrowthRecordEntity(this GrowthRecordInfantDto dto, List<Alert>? alerts)
        {
            return new GrowthRecord
            {
                ChildId = dto.ChildID,
                RecordedBy = dto.RecordedBy,
                Weight = dto.Weight,
                Height = dto.Height,
                HeadCircumference = dto.HeadCircumference,
                ImmunizationStatus = dto.ImmunizationStatus,
                Notes = dto.Notes,
                OxygenSaturation = dto.OxygenSaturation,  // Thêm OxygenSaturation vào GrowthRecord
                Alerts = alerts
            };
        }

        // Toddler
        public static GrowthRecord MapToGrowthRecordEntity(this GrowthRecordToddlerDto dto)
        {
            return new GrowthRecord
            {
                ChildId = dto.ChildID,
                RecordedBy = dto.RecordedBy,
                Weight = dto.Weight,
                Height = dto.Height,
                ChestCircumference = dto.ChestCircumference,
                NutritionalStatus = dto.NutritionalStatus,
                ImmunizationStatus = dto.ImmunizationStatus,
                Notes = dto.Notes,
            };
        }

        // Child
        public static GrowthRecord MapToGrowthRecordEntity(this GrowthRecordChildDto dto)
        {
            return new GrowthRecord
            {
                ChildId = dto.ChildID,
                RecordedBy = dto.RecordedBy,
                Weight = dto.Weight,
                Height = dto.Height,
                ChestCircumference = dto.ChestCircumference,
                MuscleMass = dto.MuscleMass,
                PhysicalActivityLevel = dto.PhysicalActivityLevel,
                HeartRate = dto.HeartRate,
                BloodPressure = dto.BloodPressure,
                Vision = dto.Vision,
                Hearing = dto.Hearing,
                Notes = dto.Notes,
            };
        }

        // Teenager
        public static GrowthRecord MapToGrowthRecordEntity(this GrowthRecordTeenagerDto dto)
        {
            return new GrowthRecord
            {
                ChildId = dto.ChildId,
                RecordedBy = dto.RecordedBy,
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
                Notes = dto.Notes,
            };
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
    }
}

