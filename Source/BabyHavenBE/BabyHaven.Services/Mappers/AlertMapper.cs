using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common.DTOs.AlertDTOS;
using BabyHaven.Repositories.Models;

namespace BabyHaven.Services.Mappers
{
    public static class AlertMapper
    {
        public static Alert ToAlert(this AlertCreateDto dto)
        {
            return new Alert
            {
                GrowthRecordId = dto.GrowthRecordId,
                AlertDate = dto.AlertDate,
                DiseaseId = dto.DiseaseId,
                Message = dto.Message,
                IsRead = dto.IsRead,
                SeverityLevel = dto.SeverityLevel.ToString(),
                IsAcknowledged = dto.IsAcknowledged
            };
        }

        public static Alert ToAlert(this AlertUpdateDto dto, Alert existingAlert)
        {
            existingAlert.GrowthRecordId = dto.GrowthRecordId;
            existingAlert.AlertDate = dto.AlertDate;
            existingAlert.DiseaseId = dto.DiseaseId;
            existingAlert.Message = dto.Message;
            existingAlert.IsRead = dto.IsRead;
            existingAlert.SeverityLevel = dto.SeverityLevel.ToString();
            existingAlert.IsAcknowledged = dto.IsAcknowledged;

            return existingAlert;
        }

        public static AlertViewAllDto ToAlertViewAllDto(this Alert alert)
        {
            return new AlertViewAllDto
            {
                AlertDate = alert.AlertDate,
                Message = alert.Message,
                SeverityLevel = alert.SeverityLevel,
                Prevention = alert.Disease?.Prevention,
                TreatMent = alert.Disease?.Treatment,
                DiseaseName = alert.Disease?.DiseaseName,
                GrowthRecordDate = alert.GrowthRecord?.CreatedAt
            };
        }
        public static AlertViewDetailsDto ToAlertViewDetailsDto(this Alert alert)
        {
            return new AlertViewDetailsDto
            {
                AlertId = alert.AlertId,
                GrowthRecordId = alert.GrowthRecordId,
                AlertDate = alert.AlertDate,
                DiseaseId = alert.DiseaseId,
                Message = alert.Message,
                IsRead = alert.IsRead,
                SeverityLevel = alert.SeverityLevel,
                IsAcknowledged = alert.IsAcknowledged,
                Prevention = alert.Disease?.Prevention,
                TreatMent = alert.Disease?.Treatment,
                DiseaseName = alert.Disease?.DiseaseName,
                GrowthRecordDate = alert.GrowthRecord?.CreatedAt
            };
        }

        public static Alert ToAlertFromGrowthRecord(GrowthRecord record, Disease disease, string? customMessage = null)
        {
            string defaultMessage = $"Alert: {disease.DiseaseName}";

            // Thêm treatment nếu có
            if (!string.IsNullOrEmpty(disease.Treatment))
                defaultMessage += $" Recommended treatment: {disease.Treatment}.";

            // Thêm prevention nếu có
            if (!string.IsNullOrEmpty(disease.Prevention))
                defaultMessage += $" Prevention tips: {disease.Prevention}.";
            return new Alert
            {
                GrowthRecordId = record.RecordId,
                AlertDate = DateTime.UtcNow,
                DiseaseId = disease.DiseaseId,
                Message = customMessage ?? defaultMessage,
                IsRead = false,
                IsAcknowledged = false,
                SeverityLevel = disease.Severity,
                Disease = disease,
                GrowthRecord = record
            };
        }
    }
}
