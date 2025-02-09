using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common;
using BabyHaven.Common.DTOs.GrowthRecordDTOs;
using BabyHaven.Repositories;
using BabyHaven.Repositories.Models;
using BabyHaven.Repositories.Repositories;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Mappers;

namespace BabyHaven.Services.Services
{
    public class GrowthRecordService : IGrowthRecordService
    {
        private readonly UnitOfWork _unitOfWork;

        public GrowthRecordService(UnitOfWork unitOfWor)
        {
            _unitOfWork = unitOfWor ?? throw new ArgumentNullException(nameof(unitOfWor));
        }

        public async Task<IServiceResult> CreateGrowthRecordChild(GrowthRecordChildDto dto)
        {
            try
            {
                var growthRecord = dto.MapToGrowthRecordEntity();

                await _unitOfWork.GrowthRecordRepository.CreateAsync(growthRecord);

                return new ServiceResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, growthRecord);
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, $"{Const.FAIL_CREATE_MSG}: {ex.Message}");
            }
        }

        public async Task<IServiceResult> CreateGrowthRecordInfant(GrowthRecordInfantDto dto)
        {
            try
            {
                // Tạo GrowthRecord từ DTO (không gắn Alerts ngay ở đây)
                var growthRecord = dto.MapToGrowthRecordEntity(new List<Alert>());

                // Lưu GrowthRecord vào cơ sở dữ liệu
                await _unitOfWork.GrowthRecordRepository.CreateAsync(growthRecord);

                // Kiểm tra và tạo Alert sau khi tạo thành công GrowthRecord
                var alertsResult = await CheckAndCreateAlert(dto.ChildID);

                // Kiểm tra nếu có alert, gắn chúng vào GrowthRecord
                if (alertsResult?.Data != null)
                {
                    growthRecord.Alerts = alertsResult.Data as List<Alert>;
                    await _unitOfWork.GrowthRecordRepository.UpdateAsync(growthRecord); // Cập nhật lại GrowthRecord với Alerts
                }

                // Trả kết quả thành công
                return new ServiceResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, growthRecord.MapToGrowthRecordViewAll());
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về thông báo lỗi
                return new ServiceResult(Const.ERROR_EXCEPTION, $"{Const.FAIL_CREATE_MSG}: {ex.InnerException?.ToString()}");
            }
        }


        public async Task<IServiceResult> CreateGrowthRecordTeenager(GrowthRecordTeenagerDto dto)
        {
            try
            {
                var growthRecord = dto.MapToGrowthRecordEntity();

                await _unitOfWork.GrowthRecordRepository.CreateAsync(growthRecord);

                return new ServiceResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, growthRecord);
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, $"{Const.FAIL_CREATE_MSG}: {ex.Message}");
            }
        }

        public async Task<IServiceResult> CreateGrowthRecordToddler(GrowthRecordToddlerDto dto)
        {
            try
            {
                var growthRecord = dto.MapToGrowthRecordEntity();


                await _unitOfWork.GrowthRecordRepository.CreateAsync(growthRecord);

                return new ServiceResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, growthRecord);
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, $"{Const.FAIL_CREATE_MSG}: {ex.Message}");
            }
        }

        public async Task<IServiceResult> DeleteGrowthRecord(int recordId)
        {
            try
            {
                var growthRecord = await _unitOfWork.GrowthRecordRepository.GetByIdAsync(recordId);
                if (growthRecord == null)
                {
                    return new ServiceResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
                }

                await _unitOfWork.GrowthRecordRepository.RemoveAsync(growthRecord);

                return new ServiceResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, $"{Const.FAIL_DELETE_MSG}: {ex.Message}");
            }
        }

        public async Task<IServiceResult> GetAllGrowthRecordsByChild(Guid childId)
        {
            try
            {
                var growthRecords = await _unitOfWork.GrowthRecordRepository.GetAllGrowthRecordsByChild(childId);
                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, growthRecords);
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, $"{Const.FAIL_READ_MSG}: {ex.Message}");
            }
        }

        public async Task<IServiceResult> GetGrowthRecordById(int recordId, Guid childId)
        {
            try
            {
                var growthRecord = await _unitOfWork.GrowthRecordRepository.GetGrowthRecordById(recordId, childId);
                if (growthRecord == null)
                {
                    return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG);
                }

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, growthRecord);
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, $"{Const.FAIL_READ_MSG}: {ex.Message}");
            }
        }

        public async Task<IServiceResult> GetRecordsByDateRangeAsync(Guid childId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var growthRecords = await _unitOfWork.GrowthRecordRepository.GetRecordsByDateRangeAsync(childId, startDate, endDate);
                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, growthRecords);
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, $"{Const.FAIL_READ_MSG}: {ex.Message}");
            }
        }

        public async Task<IServiceResult> CheckAndCreateAlert(Guid childId)
        {
            try
            {
                var latestRecord = await _unitOfWork.GrowthRecordRepository.GetLatestGrowthRecordByChildAsync(childId);
                var child = await _unitOfWork.ChildrenRepository.GetByIdAsync(childId);

                if (latestRecord == null)
                    return new ServiceResult(Const.FAIL_READ_CODE, "No growth records found for this child.");

                var diseases = await _unitOfWork.DiseaseRepository.GetAllAsync();

                var alerts = diseases
                    .Where(d => ShouldCreateAlert(latestRecord, d, child)) // Kiểm tra điều kiện tạo alert
                    .Select(d => AlertMapper.ToAlertFromGrowthRecord(latestRecord, d)) // Tạo alert
                    .ToList();

                if (!alerts.Any())
                    return new ServiceResult(Const.WARNING_NO_DATA_CODE, "No alerts created.");

                // Dùng vòng lặp để tạo từng alert
                foreach (var alert in alerts)
                {
                    await _unitOfWork.AlertRepository.CreateAsync(alert); // Lưu alert vào cơ sở dữ liệu
                }

                return new ServiceResult(Const.SUCCESS_CREATE_CODE, "Alerts created successfully.", alerts);
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, $"An error occurred while checking and creating alerts: {ex.InnerException?.Message}");
            }
        }


        private bool IsWeightAndHeightIssue(GrowthRecord record, Disease disease, Child child)
        {
            double bmi = record.Weight / (record.Height * record.Height);
            // So sánh BMI với các ngưỡng từ bệnh (có thể tùy chỉnh)
            bool isBmiLowForMale = (child.Gender == "Male" && bmi < disease.LowerBoundMale);
            bool isBmiLowForFemale = (child.Gender == "Female" && bmi < disease.LowerBoundFemale);

            return isBmiLowForMale || isBmiLowForFemale;
        }

        private bool ShouldCreateAlert(GrowthRecord record, Disease disease, Child child)
        {
            // Kiểm tra BMI
            bool isBmiIssue = IsWeightAndHeightIssue(record, disease, child);

            // Kiểm tra các chỉ số khác như FerritinLevel, Triglycerides, và BloodSugarLevel
            bool isFerritinLevelLow = record.FerritinLevel.HasValue && record.FerritinLevel.Value < disease.LowerBoundMale;
            bool isTriglyceridesHigh = record.Triglycerides.HasValue && record.Triglycerides.Value > disease.UpperBoundMale;
            bool isBloodSugarLevelHigh = record.BloodSugarLevel.HasValue && record.BloodSugarLevel.Value > disease.UpperBoundFemale;

            // Kiểm tra các chỉ số khác như BloodPressure, MuscleMass
            bool isBloodPressureIssue = record.BloodPressure.HasValue && record.BloodPressure.Value > 140; // Ví dụ: huyết áp cao
            bool isMuscleMassLow = record.MuscleMass.HasValue && record.MuscleMass.Value < 10; // Ví dụ: khối lượng cơ thấp

            // Kiểm tra chỉ số dinh dưỡng
            bool isNutritionalStatusPoor = !string.IsNullOrEmpty(record.NutritionalStatus) && record.NutritionalStatus.Contains("Poor");

            // Nếu bất kỳ điều kiện nào thỏa mãn, trả về true để tạo Alert
            return isBmiIssue || isFerritinLevelLow || isTriglyceridesHigh || isBloodSugarLevelHigh ||
                   isBloodPressureIssue || isMuscleMassLow || isNutritionalStatusPoor;
        }

    }
}
