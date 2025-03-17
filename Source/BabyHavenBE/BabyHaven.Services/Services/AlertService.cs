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
                if (latestRecord == null)
                {
                    return new ServiceResult(Const.FAIL_READ_CODE, "No growth records found for this child.");
                }

                var child = await _unitOfWork.ChildrenRepository.GetByIdAsync(childId);
                if (child?.Gender == null)
                {
                    return new ServiceResult(Const.FAIL_READ_CODE, "Child not found or gender not specified.");
                }

                var diseases = await _unitOfWork.DiseaseRepository.GetAllAsync();
                var alertsToCreate = diseases
                    .Where(disease => ShouldCreateAlert(latestRecord, disease, child))
                    .Select(disease => disease.ToAlertFromGrowthRecord(latestRecord))
                    .ToList();

                if (!alertsToCreate.Any())
                {
                    return new ServiceResult(Const.WARNING_NO_DATA_CODE, "No alerts created.");
                }

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


        private bool IsWeightAndHeightIssue(GrowthRecord record, Disease disease, Child child)
        {
            // Kiểm tra giá trị 0 hoặc âm
            if (record.Weight <= 0 || record.Height <= 0)
                return false;

            double bmi = record.Weight / (record.Height * record.Height) * 10000;
            bool isBmiLowForMale = (child.Gender == "Male" && bmi < disease.LowerBoundMale);
            bool isBmiLowForFemale = (child.Gender == "Female" && bmi < disease.LowerBoundFemale);

            return isBmiLowForMale || isBmiLowForFemale;
        }

        private bool IsFerritinLevelLow(GrowthRecord record, Disease disease, Child child)
        {
            bool isFerritinLowForMale = (child.Gender == "Male" && record.FerritinLevel.HasValue && record.FerritinLevel.Value < disease.LowerBoundMale);
            bool isFerritinLowForFemale = (child.Gender == "Female" && record.FerritinLevel.HasValue && record.FerritinLevel.Value < disease.LowerBoundFemale);

            return isFerritinLowForMale || isFerritinLowForFemale;
        }

        private bool IsTriglyceridesHigh(GrowthRecord record, Disease disease, Child child)
        {
            bool isTriglyceridesHighForMale = (child.Gender == "Male" && record.Triglycerides.HasValue && record.Triglycerides.Value > disease.UpperBoundMale);
            bool isTriglyceridesHighForFemale = (child.Gender == "Female" && record.Triglycerides.HasValue && record.Triglycerides.Value > disease.UpperBoundFemale);

            return isTriglyceridesHighForMale || isTriglyceridesHighForFemale;
        }

        private bool IsBloodSugarLevelHigh(GrowthRecord record, Disease disease, Child child)
        {
            bool isBloodSugarHighForMale = (child.Gender == "Male" && record.BloodSugarLevel.HasValue && record.BloodSugarLevel.Value > disease.UpperBoundMale);
            bool isBloodSugarHighForFemale = (child.Gender == "Female" && record.BloodSugarLevel.HasValue && record.BloodSugarLevel.Value > disease.UpperBoundFemale);

            return isBloodSugarHighForMale || isBloodSugarHighForFemale;
        }

        private bool IsBloodPressureIssue(GrowthRecord record, Disease disease, Child child)
        {
            bool isBloodPressureHighForMale = (child.Gender == "Male" && record.BloodPressure.HasValue && record.BloodPressure.Value > disease.UpperBoundMale);
            bool isBloodPressureHighForFemale = (child.Gender == "Female" && record.BloodPressure.HasValue && record.BloodPressure.Value > disease.UpperBoundFemale);

            return isBloodPressureHighForMale || isBloodPressureHighForFemale;
        }

        private bool IsMuscleMassLow(GrowthRecord record, Disease disease, Child child)
        {
            bool isMuscleMassLowForMale = (child.Gender == "Male" && record.MuscleMass.HasValue && record.MuscleMass.Value < disease.LowerBoundMale);
            bool isMuscleMassLowForFemale = (child.Gender == "Female" && record.MuscleMass.HasValue && record.MuscleMass.Value < disease.LowerBoundFemale);

            return isMuscleMassLowForMale || isMuscleMassLowForFemale;
        }


        private bool ShouldCreateAlert(GrowthRecord record, Disease disease, Child child)
        {
            if (IsWeightAndHeightIssue(record, disease, child))
                return true;

            if (IsFerritinLevelLow(record, disease, child))
                return true;

            if (IsTriglyceridesHigh(record, disease, child))
                return true;

            if (IsBloodSugarLevelHigh(record, disease, child))
                return true;

            if (IsBloodPressureIssue(record, disease, child))
                return true;

            if (IsMuscleMassLow(record, disease, child))
                return true;

            return false;
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
