using System;
using System.Text;
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
            };
        }

        public static AlertViewDetailsDto ToAlertViewDetailsDto(this Alert alert)
        {
            return new AlertViewDetailsDto
            {
                AlertId = alert.AlertId,
                GrowthRecordId = alert.GrowthRecordId,
                RecordDate = alert.GrowthRecord.CreatedAt,
                AlertDate = alert.AlertDate,
                DiseaseId = alert.DiseaseId,
                Message = alert.Message,
                IsRead = alert.IsRead,
                SeverityLevel = alert.SeverityLevel,
                IsAcknowledged = alert.IsAcknowledged,
            };
        }

        public static Alert ToAlertFromGrowthRecord(this Disease disease, GrowthRecord record, string? customMessage = null)
        {
            return new Alert
            {
                GrowthRecordId = record.RecordId,
                AlertDate = DateTime.UtcNow,
                DiseaseId = disease.DiseaseId,
                Message = customMessage ?? $"Alert: {disease.DiseaseName}\nDisease Type: {disease.DiseaseType}",
                IsRead = false,
                IsAcknowledged = false,
                SeverityLevel = disease.Severity
            };
        }

        public static string BuildDetailedMessage(Disease disease, GrowthRecord record, Child child, BmiPercentile bmiPercentileData, double averageBmi, bool isBmiDecreasing, bool isBmiIncreasing, bool isHeightStagnant, int recordCount)
        {
            var messageBuilder = new StringBuilder();

            // Tiêu đề alert
            messageBuilder.AppendLine($"Alert: {disease.DiseaseName}");

            // Ngày tạo alert
            messageBuilder.AppendLine($"Date: {DateTime.UtcNow:dd/MM/yyyy HH:mm:ss}");

            // Loại bệnh
            messageBuilder.AppendLine($"Disease Type: {disease.DiseaseType}");

            // Triệu chứng
            if (!string.IsNullOrEmpty(disease.Symptoms))
                messageBuilder.AppendLine($"Symptoms: {disease.Symptoms}");

            // Phương pháp điều trị
            if (!string.IsNullOrEmpty(disease.Treatment))
                messageBuilder.AppendLine($"Recommended Treatment: {disease.Treatment}");

            // Cách phòng ngừa
            if (!string.IsNullOrEmpty(disease.Prevention))
                messageBuilder.AppendLine($"Prevention Tips: {disease.Prevention}");

            // Mô tả bệnh
            if (!string.IsNullOrEmpty(disease.Description))
                messageBuilder.AppendLine($"Description: {disease.Description}");

            // Ghi chú
            if (!string.IsNullOrEmpty(disease.Notes))
                messageBuilder.AppendLine($"Notes: {disease.Notes}");

            // Phân tích xu hướng tăng trưởng
            string trendAnalysis = "";
            switch (disease.DiseaseName)
            {
                case "Severe Malnutrition":
                case "Mild Malnutrition":
                    trendAnalysis = isBmiDecreasing
                        ? $"BMI has been decreasing over the last {recordCount} records and is now {averageBmi:F1}, below the {(disease.DiseaseName == "Severe Malnutrition" ? "1st" : "5th")} percentile ({(disease.DiseaseName == "Severe Malnutrition" ? bmiPercentileData.P01 : bmiPercentileData.P01 + (bmiPercentileData.P50 - bmiPercentileData.P01) * 0.2):F1})."
                        : $"BMI is currently {averageBmi:F1}, below the {(disease.DiseaseName == "Severe Malnutrition" ? "1st" : "5th")} percentile ({(disease.DiseaseName == "Severe Malnutrition" ? bmiPercentileData.P01 : bmiPercentileData.P01 + (bmiPercentileData.P50 - bmiPercentileData.P01) * 0.2):F1}).";
                    break;

                case "Overweight":
                case "Obesity":
                    trendAnalysis = isBmiIncreasing
                        ? $"BMI has been increasing over the last {recordCount} records and is now {averageBmi:F1}, above the {(disease.DiseaseName == "Overweight" ? "75th" : "99th")} percentile ({(disease.DiseaseName == "Overweight" ? bmiPercentileData.P75 : bmiPercentileData.P99):F1})."
                        : $"BMI is currently {averageBmi:F1}, above the {(disease.DiseaseName == "Overweight" ? "75th" : "99th")} percentile ({(disease.DiseaseName == "Overweight" ? bmiPercentileData.P75 : bmiPercentileData.P99):F1}).";
                    break;

                case "Stunted Growth":
                    trendAnalysis = isHeightStagnant
                        ? $"Height has been stagnant over the last {recordCount} records and is now {record.Height:F1} cm, below the expected range ({(child.Gender == "Male" ? disease.LowerBoundMale : disease.LowerBoundFemale):F1} cm)."
                        : $"Height is currently {record.Height:F1} cm, below the expected range ({(child.Gender == "Male" ? disease.LowerBoundMale : disease.LowerBoundFemale):F1} cm).";
                    break;

                case "Anemia":
                    trendAnalysis = $"Ferritin level is currently {record.FerritinLevel:F1}, outside the normal range ({(child.Gender == "Male" ? disease.LowerBoundMale : disease.LowerBoundFemale):F1} - {(child.Gender == "Male" ? disease.UpperBoundMale : disease.UpperBoundFemale):F1}).";
                    break;

                case "Diabetes Type 1":
                    trendAnalysis = $"Blood sugar level is currently {record.BloodSugarLevel:F1}, outside the normal range ({(child.Gender == "Male" ? disease.LowerBoundMale : disease.LowerBoundFemale):F1} - {(child.Gender == "Male" ? disease.UpperBoundMale : disease.UpperBoundFemale):F1}).";
                    break;

                case "Asthma":
                    trendAnalysis = $"Oxygen saturation is currently {record.OxygenSaturation:F1}, outside the normal range ({(child.Gender == "Male" ? disease.LowerBoundMale : disease.LowerBoundFemale):F1} - {(child.Gender == "Male" ? disease.UpperBoundMale : disease.UpperBoundFemale):F1}).";
                    break;

                case "Rickets":
                    trendAnalysis = $"Growth hormone level is currently {record.GrowthHormoneLevel:F1}, outside the normal range ({(child.Gender == "Male" ? disease.LowerBoundMale : disease.LowerBoundFemale):F1} - {(child.Gender == "Male" ? disease.UpperBoundMale : disease.UpperBoundFemale):F1}).";
                    break;

                case "Hypertension":
                    trendAnalysis = $"Blood pressure is currently {record.BloodPressure:F1}, outside the normal range ({(child.Gender == "Male" ? disease.LowerBoundMale : disease.LowerBoundFemale):F1} - {(child.Gender == "Male" ? disease.UpperBoundMale : disease.UpperBoundFemale):F1}).";
                    break;

                case "Failure to Thrive":
                    trendAnalysis = $"Developmental milestones length is currently {record.DevelopmentalMilestones?.Length ?? 0}, outside the normal range ({(child.Gender == "Male" ? disease.LowerBoundMale : disease.LowerBoundFemale):F1} - {(child.Gender == "Male" ? disease.UpperBoundMale : disease.UpperBoundFemale):F1}).";
                    break;

                default:
                    trendAnalysis = "No specific trend analysis available.";
                    break;
            }

            messageBuilder.AppendLine($"Trend Analysis: {trendAnalysis}");

            return messageBuilder.ToString().Trim();
        }
    }
}