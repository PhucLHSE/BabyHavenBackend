using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BabyHaven.Common;
using BabyHaven.Common.DTOs.AlertDTOS;
using BabyHaven.Repositories;
using BabyHaven.Repositories.Helper;
using BabyHaven.Repositories.Models;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Mappers;

namespace BabyHaven.Services.Services
{
    public class AlertService : IAlertService
    {
        private readonly UnitOfWork _unitOfWork;

        public AlertService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork
                ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<IServiceResult> GetAll()
        {
            try
            {
                var alerts = await _unitOfWork.AlertRepository.GetAllAsync();
                var alertDtos = alerts.Select(alert => alert.ToAlertViewAllDto()).ToList();

                return new ServiceResult
                {
                    Status = Const.SUCCESS_READ_CODE,
                    Message = Const.SUCCESS_READ_MSG,
                    Data = alertDtos
                };
            }
            catch (Exception ex)
            {
                return HandleException("retrieving alerts", ex);
            }
        }

        public async Task<IQueryable<AlertViewAllDto>> GetQueryable()
        {
            var alerts = await _unitOfWork.AlertRepository.GetAllAsync();
            return alerts.Select(alert => alert.ToAlertViewAllDto()).AsQueryable();
        }

        public async Task<IServiceResult> GetById(int alertId)
        {
            try
            {
                var alert = await _unitOfWork.AlertRepository.GetByIdAsync(alertId);
                if (alert == null)
                    return new ServiceResult { Status = Const.FAIL_READ_CODE, Message = "Alert not found." };

                return new ServiceResult
                {
                    Status = Const.SUCCESS_READ_CODE,
                    Message = Const.SUCCESS_READ_MSG,
                    Data = alert.ToAlertViewDetailsDto()
                };
            }
            catch (Exception ex)
            {
                return HandleException("retrieving the alert", ex);
            }
        }

        public async Task<IServiceResult> GetByChild(string name, string dob, Guid memberId)
        {
            try
            {
                var alerts = await _unitOfWork.AlertRepository.GetChildByNameAndDateOfBirthAsync(name, DateOnly.Parse(dob), memberId);
                if (alerts == null || !alerts.Any())
                    return new ServiceResult { Status = Const.FAIL_READ_CODE, Message = "Alert not found." };

                var alertDtos = alerts.Select(a => a.ToAlertViewDetailsDto()).ToList();

                return new ServiceResult
                {
                    Status = Const.SUCCESS_READ_CODE,
                    Message = Const.SUCCESS_READ_MSG,
                    Data = alertDtos
                };
            }
            catch (Exception ex)
            {
                return HandleException("retrieving the alert", ex);
            }
        }

        public async Task<IServiceResult> Create(AlertCreateDto dto)
        {
            try
            {
                if (dto == null)
                    return new ServiceResult { Status = Const.FAIL_CREATE_CODE, Message = Const.FAIL_CREATE_MSG };

                var alert = dto.ToAlert();
                await _unitOfWork.AlertRepository.CreateAsync(alert);

                return new ServiceResult
                {
                    Status = Const.SUCCESS_CREATE_CODE,
                    Message = Const.SUCCESS_CREATE_MSG,
                    Data = alert
                };
            }
            catch (Exception ex)
            {
                return HandleException("creating the alert", ex);
            }
        }

        public async Task<IServiceResult> Update(AlertUpdateDto dto)
        {
            try
            {
                if (dto == null)
                    return new ServiceResult { Status = Const.FAIL_UPDATE_CODE, Message = Const.FAIL_UPDATE_MSG };

                var alert = await _unitOfWork.AlertRepository.GetByIdAsync(dto.AlertId);
                if (alert == null)
                    return new ServiceResult { Status = Const.FAIL_READ_CODE, Message = "Alert not found." };

                alert = dto.ToAlert(alert);
                await _unitOfWork.AlertRepository.UpdateAsync(alert);

                return new ServiceResult
                {
                    Status = Const.SUCCESS_UPDATE_CODE,
                    Message = Const.SUCCESS_UPDATE_MSG,
                    Data = alert
                };
            }
            catch (Exception ex)
            {
                return HandleException("updating the alert", ex);
            }
        }

        public async Task<IServiceResult> Delete(int alertId)
        {
            try
            {
                var alert = await _unitOfWork.AlertRepository.GetByIdAsync(alertId);
                if (alert == null)
                    return new ServiceResult { Status = Const.FAIL_READ_CODE, Message = "Alert not found." };

                await _unitOfWork.AlertRepository.RemoveAsync(alert);

                return new ServiceResult
                {
                    Status = Const.SUCCESS_DELETE_CODE,
                    Message = Const.SUCCESS_DELETE_MSG
                };
            }
            catch (Exception ex)
            {
                return HandleException("deleting the alert", ex);
            }
        }

        public async Task<IServiceResult> CheckAndCreateAlert(string name, string dob, Guid memberId)
        {
            try
            {
                // Lấy thông tin trẻ
                var child = await _unitOfWork.ChildrenRepository
                    .GetChildByNameAndDateOfBirthAsync(name, DateOnly.Parse(dob), memberId);

                if (child == null)
                {
                    return new ServiceResult(Const.FAIL_READ_CODE, "Invalid child.");
                }

                // Tính tuổi của trẻ
                int age = ChildrenHelper.CalculateAge(child.DateOfBirth.ToDateTime(TimeOnly.MinValue));
                if (age < 0)
                {
                    return new ServiceResult(Const.FAIL_READ_CODE, "Invalid age.");
                }

                if (child?.Gender == null)
                {
                    return new ServiceResult(Const.FAIL_READ_CODE, "Child not found or gender not specified.");
                }

                // Lấy 3 bản ghi tăng trưởng gần nhất
                var recentRecords = await _unitOfWork.GrowthRecordRepository
                    .GetRecentGrowthRecordsByChildAsync(child.ChildId, 3);

                if (recentRecords == null || !recentRecords.Any())
                {
                    return new ServiceResult(Const.FAIL_READ_CODE, "No growth records found for this child.");
                }

                // Lấy dữ liệu chuẩn từ WHO dựa trên tuổi và giới tính (cho các bệnh liên quan đến BMI)
                var bmiPercentileData = await _unitOfWork.BmiPercentileRepository
                    .GetByAgeAndGender(age, child.Gender);

                if (bmiPercentileData == null)
                {
                    return new ServiceResult(Const.FAIL_READ_CODE, "Failed to retrieve BMI percentile data.");
                }

                // Lấy danh sách các bệnh từ DiseaseRepository
                var diseases = await _unitOfWork.DiseaseRepository.GetAllAsync();
                if (diseases == null || !diseases.Any())
                {
                    return new ServiceResult(Const.FAIL_READ_CODE, "No diseases found in the database.");
                }

                // Phân tích xu hướng và tạo alert
                var alertsToCreate = new List<Alert>();

                // Tính BMI trung bình và xu hướng để sử dụng trong thông điệp
                var bmiValues = recentRecords
                    .Select(r => r.Weight / (r.Height * r.Height) * 10000)
                    .ToList();
                bool isBmiDecreasing = IsDecreasingTrend(bmiValues);
                bool isBmiIncreasing = IsIncreasingTrend(bmiValues);
                double averageBmi = bmiValues.Average();

                // Tính xu hướng chiều cao
                var heights = recentRecords.Select(r => r.Height).ToList();
                bool isHeightStagnant = IsStagnantTrend(heights);

                foreach (var disease in diseases)
                {
                    if (ShouldCreateAlert(recentRecords, disease, child, bmiPercentileData))
                    {
                        // Tạo thông điệp chi tiết bằng AlertMapper
                        var latestRecord = recentRecords.OrderByDescending(r => r.CreatedAt).First();
                        string customMessage = AlertMapper.BuildDetailedMessage(
                            disease,
                            latestRecord,
                            child,
                            bmiPercentileData,
                            averageBmi,
                            isBmiDecreasing,
                            isBmiIncreasing,
                            isHeightStagnant,
                            recentRecords.Count
                        );
                        var alert = disease.ToAlertFromGrowthRecord(latestRecord, customMessage);
                        alertsToCreate.Add(alert);
                    }
                }

                // Kiểm tra tần suất alert để tránh trùng lặp
                var existingAlerts = await _unitOfWork.AlertRepository
                    .GetRecentAlertsByChildAsync(child.ChildId, TimeSpan.FromDays(7));

                alertsToCreate = alertsToCreate
                    .Where(alert => !existingAlerts.Any(ea => ea.DiseaseId == alert.DiseaseId))
                    .ToList();

                if (!alertsToCreate.Any())
                {
                    return new ServiceResult(Const.WARNING_NO_DATA_CODE, "No new alerts created.");
                }

                // Lưu các alert mới
                foreach (var alert in alertsToCreate)
                {
                    await _unitOfWork.AlertRepository.CreateAsync(alert);
                }

                return new ServiceResult(Const.SUCCESS_CREATE_CODE, "Alerts created successfully.", alertsToCreate);
            }
            catch (Exception ex)
            {
                return HandleException("checking and creating alerts", ex);
            }
        }

        private bool ShouldCreateAlert(List<GrowthRecord> recentRecords, Disease disease, Child child, BmiPercentile bmiPercentileData)
        {
            // Tính BMI cho từng bản ghi
            var bmiValues = recentRecords
                .Select(r => r.Weight / (r.Height * r.Height) * 10000)
                .ToList();

            // Kiểm tra xu hướng BMI
            bool isBmiDecreasing = IsDecreasingTrend(bmiValues);
            bool isBmiIncreasing = IsIncreasingTrend(bmiValues);

            // Tính BMI trung bình
            double averageBmi = bmiValues.Average();

            // So sánh với dữ liệu chuẩn từ WHO (BmiPercentile) hoặc ngưỡng từ Disease
            switch (disease.DiseaseName)
            {
                case "Severe Malnutrition":
                    // Kiểm tra nếu BMI dưới percentile 1st và có xu hướng giảm
                    return averageBmi < bmiPercentileData.P01 && isBmiDecreasing;

                case "Mild Malnutrition":
                    // Kiểm tra nếu BMI dưới percentile 5th và có xu hướng giảm
                    // Vì không có P05, ước lượng P05 nằm giữa P01 và P50
                    double estimatedP05 = bmiPercentileData.P01 + (bmiPercentileData.P50 - bmiPercentileData.P01) * 0.2;
                    return averageBmi < estimatedP05 && isBmiDecreasing;

                case "Overweight":
                    // Kiểm tra nếu BMI trên percentile 75th và có xu hướng tăng
                    return averageBmi > bmiPercentileData.P75 && isBmiIncreasing;

                case "Obesity":
                    // Kiểm tra nếu BMI trên percentile 99th và có xu hướng tăng
                    return averageBmi > bmiPercentileData.P99 && isBmiIncreasing;

                case "Stunted Growth":
                    // Kiểm tra xu hướng chiều cao
                    var heights = recentRecords.Select(r => r.Height).ToList();
                    bool isHeightStagnant = IsStagnantTrend(heights);
                    return isHeightStagnant && IsValueInRange(heights.Last(), disease, child);

                case "Anemia":
                    // Kiểm tra FerritinLevel
                    var ferritinLevels = recentRecords.Select(r => r.FerritinLevel).ToList();
                    return IsValueInRange(ferritinLevels.Last(), disease, child);

                case "Diabetes Type 1":
                    // Kiểm tra BloodSugarLevel
                    var bloodSugarLevels = recentRecords.Select(r => r.BloodSugarLevel).ToList();
                    return IsValueInRange(bloodSugarLevels.Last(), disease, child);

                case "Asthma":
                    // Kiểm tra OxygenSaturation
                    var oxygenLevels = recentRecords.Select(r => r.OxygenSaturation).ToList();
                    return IsValueInRange(oxygenLevels.Last(), disease, child);

                case "Rickets":
                    // Kiểm tra GrowthHormoneLevel
                    var growthHormoneLevels = recentRecords.Select(r => r.GrowthHormoneLevel).ToList();
                    return IsValueInRange(growthHormoneLevels.Last(), disease, child);

                case "Hypertension":
                    // Kiểm tra BloodPressure
                    var bloodPressures = recentRecords.Select(r => r.BloodPressure).ToList();
                    return IsValueInRange(bloodPressures.Last(), disease, child);

                case "Failure to Thrive":
                    // Kiểm tra DevelopmentalMilestones
                    return IsDevelopmentalMilestonesInRange(recentRecords.Last(), disease, child);

                default:
                    return false;
            }
        }

        //private bool IsBmiInRange(GrowthRecord growthRecord, Disease disease, Child child)
        //{
        //    if (growthRecord.Weight <= 0 || growthRecord.Height <= 0) return false;

        //    double bmi = growthRecord.Weight / (growthRecord.Height * growthRecord.Height) * 10000;
        //    return IsValueInRange(bmi, disease, child);
        //}

        private bool IsValueInRange(double? value, Disease disease, Child child)
        {
            if (!value.HasValue || value.Value < 0) return false;

            return child.Gender == "Male"
                ? value.Value > disease.LowerBoundMale && value.Value < disease.UpperBoundMale
                : value.Value > disease.LowerBoundFemale && value.Value < disease.UpperBoundFemale;
        }

        private bool IsDevelopmentalMilestonesInRange(GrowthRecord growthRecord, Disease disease, Child child)
        {
            if (string.IsNullOrEmpty(growthRecord.DevelopmentalMilestones)) return false;

            int length = growthRecord.DevelopmentalMilestones.Length;

            return child.Gender == "Male"
                ? length > disease.LowerBoundMale && length < disease.UpperBoundMale
                : length > disease.LowerBoundFemale && length < disease.UpperBoundFemale;
        }

        private bool IsDecreasingTrend(List<double> values)
        {
            if (values.Count < 2) return false;
            for (int i = 1; i < values.Count; i++)
            {
                if (values[i] >= values[i - 1]) return false; // Nếu có giá trị tăng hoặc không đổi, không phải xu hướng giảm
            }
            return true;
        }

        private bool IsIncreasingTrend(List<double> values)
        {
            if (values.Count < 2) return false;
            for (int i = 1; i < values.Count; i++)
            {
                if (values[i] <= values[i - 1]) return false; // Nếu có giá trị giảm hoặc không đổi, không phải xu hướng tăng
            }
            return true;
        }

        private bool IsStagnantTrend(List<double> values)
        {
            if (values.Count < 2) return false;
            double averageChange = (values.Last() - values.First()) / (values.Count - 1);
            return Math.Abs(averageChange) < 0.5; // Giả định thay đổi trung bình nhỏ hơn 0.5 cm là "stagnant"
        }

        private ServiceResult HandleException(string action, Exception ex)
        {
            return new ServiceResult
            {
                Status = Const.ERROR_EXCEPTION,
                Message = $"An error occurred while {action}: {ex.InnerException?.Message}"
            };
        }
    }
}