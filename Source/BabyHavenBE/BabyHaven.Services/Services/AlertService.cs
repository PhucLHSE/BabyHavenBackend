using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common;
using BabyHaven.Common.DTOs.AlertDTOS;
using BabyHaven.Repositories;
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
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<IServiceResult> GetAll()
        {
            try
            {
                var alerts = await _unitOfWork.AlertRepository.GetAllAsync();
                var alertDtos = alerts.Select(alert => alert.ToAlertViewAllDto()).ToList();
                return new ServiceResult { Status = Const.SUCCESS_READ_CODE, Message = Const.SUCCESS_READ_MSG, Data = alertDtos };
            }
            catch (Exception ex)
            {
                return HandleException("retrieving alerts", ex);
            }
        }

        public async Task<IQueryable<AlertViewAllDto>> GetQueryable()
        {

            var alerts = await _unitOfWork.AlertRepository
                .GetAllAsync();

            return alerts
                .Select(alerts => alerts.ToAlertViewAllDto())
                .AsQueryable();
        }

        public async Task<IServiceResult> GetById(int alertId)
        {
            try
            {
                var alert = await _unitOfWork.AlertRepository.GetByIdAsync(alertId);
                if (alert == null)
                    return new ServiceResult { Status = Const.FAIL_READ_CODE, Message = "Alert not found." };

                return new ServiceResult { Status = Const.SUCCESS_READ_CODE, Message = Const.SUCCESS_READ_MSG, Data = alert.ToAlertViewDetailsDto() };
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
                if (dto is null)
                    return new ServiceResult { Status = Const.FAIL_CREATE_CODE, Message = Const.FAIL_CREATE_MSG };

                var alert = dto.ToAlert();
                await _unitOfWork.AlertRepository.CreateAsync(alert);

                return new ServiceResult { Status = Const.SUCCESS_CREATE_CODE, Message = Const.SUCCESS_CREATE_MSG, Data = alert };
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
                if (dto is null)
                    return new ServiceResult { Status = Const.FAIL_UPDATE_CODE, Message = Const.FAIL_UPDATE_MSG };

                var alert = await _unitOfWork.AlertRepository.GetByIdAsync(dto.AlertId);
                if (alert == null)
                    return new ServiceResult { Status = Const.FAIL_READ_CODE, Message = "Alert not found." };

                alert = dto.ToAlert(alert);
                await _unitOfWork.AlertRepository.UpdateAsync(alert);

                return new ServiceResult { Status = Const.SUCCESS_UPDATE_CODE, Message = Const.SUCCESS_UPDATE_MSG, Data = alert };
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

                return new ServiceResult { Status = Const.SUCCESS_DELETE_CODE, Message = Const.SUCCESS_DELETE_MSG };
            }
            catch (Exception ex)
            {
                return HandleException("deleting the alert", ex);
            }
        }

        public async Task<IServiceResult> CheckAndCreateAlert(Guid childId)
        {
            try
            {
                if (childId == Guid.Empty)
                {
                    return new ServiceResult(Const.FAIL_READ_CODE, "Invalid child ID.");
                }

                var latestRecord = await _unitOfWork.GrowthRecordRepository.GetLatestGrowthRecordByChildAsync(childId);
                var child = await _unitOfWork.ChildrenRepository.GetByIdAsync(childId);
                if (child == null)
                {
                    return new ServiceResult(Const.FAIL_READ_CODE, "Child not found.");
                }

                if (latestRecord == null)
                    return new ServiceResult(Const.FAIL_READ_CODE, "No growth records found for this child.");

                var diseases = await _unitOfWork.DiseaseRepository.GetAllAsync();

                var alerts = diseases
                    .Where(d => ShouldCreateAlert(latestRecord, d, child))
                    .ToList();

                if (!alerts.Any())
                    return new ServiceResult(Const.WARNING_NO_DATA_CODE, "No alerts created.");

                foreach (var alert in alerts)
                {
                    await _unitOfWork.AlertRepository.CreateAsync(alert.ToAlertFromGrowthRecord(latestRecord));
                }

                return new ServiceResult(Const.SUCCESS_CREATE_CODE, "Alerts created successfully.", alerts);
            }
            catch (Exception ex)
            {
                return HandleException("checking and creating alerts", ex);
            }
        }

        private bool IsWeightAndHeightIssue(GrowthRecord record, Disease disease, Child child)
        {
            double bmi = record.Weight / (record.Height * record.Height) * 10000;
            bool isBmiLowForMale = (child.Gender == "Male" && bmi < disease.LowerBoundMale); // Nếu BMI thấp hơn mức giới hạn
            bool isBmiLowForFemale = (child.Gender == "Female" && bmi < disease.LowerBoundFemale); // Nếu BMI thấp hơn mức giới hạn


            return isBmiLowForMale || isBmiLowForFemale;
        }

        private bool ShouldCreateAlert(GrowthRecord record, Disease disease, Child child)
        {
            bool isBmiIssue = IsWeightAndHeightIssue(record, disease, child);
            bool isFerritinLevelLow = record.FerritinLevel.HasValue && record.FerritinLevel.Value < disease.LowerBoundMale;
            bool isTriglyceridesHigh = record.Triglycerides.HasValue && record.Triglycerides.Value > disease.UpperBoundMale;
            bool isBloodSugarLevelHigh = record.BloodSugarLevel.HasValue && record.BloodSugarLevel.Value > disease.UpperBoundFemale;
            bool isBloodPressureIssue = record.BloodPressure.HasValue && record.BloodPressure.Value > 140;
            bool isMuscleMassLow = record.MuscleMass.HasValue && record.MuscleMass.Value < 10;
            bool isNutritionalStatusPoor = !string.IsNullOrEmpty(record.NutritionalStatus) && record.NutritionalStatus.Contains("Poor");

            return isBmiIssue || isFerritinLevelLow || isTriglyceridesHigh || isBloodSugarLevelHigh ||
                   isBloodPressureIssue || isMuscleMassLow || isNutritionalStatusPoor;
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
