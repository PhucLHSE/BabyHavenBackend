using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
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

        public async Task<IServiceResult> GetByChild(string name, string dob, Guid memberId)
        {
            try
            {
                var alert = await _unitOfWork.AlertRepository.GetByChild(name, dob, memberId);
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

        public async Task<IServiceResult> CheckAndCreateAlert(string name, string dob, Guid memberId)
        {
            try
            {

                var child = await _unitOfWork.ChildrenRepository.GetChildByNameAndDateOfBirthAsync(name, DateOnly.Parse(dob), memberId);

                if (child == null)
                {
                    return new ServiceResult(Const.FAIL_READ_CODE, "Invalid child.");
                }

                var latestRecord = await _unitOfWork.GrowthRecordRepository.GetLatestGrowthRecordByChildAsync(child.ChildId);
                if (latestRecord == null)
                {
                    return new ServiceResult(Const.FAIL_READ_CODE, "No growth records found for this child.");
                }

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

        private bool ShouldCreateAlert(GrowthRecord growthRecord, Disease disease, Child child)
        {
            int age = ChildrenHelper.CalculateAge(child.DateOfBirth.ToDateTime(TimeOnly.MinValue));
            if (age < 0) return false;

            return disease.DiseaseName switch
            {
                "Severe Malnutrition" => IsBmiInRange(growthRecord, disease, child),
                "Mild Malnutrition" => IsBmiInRange(growthRecord, disease, child),
                "Anemia" => IsValueInRange(growthRecord.FerritinLevel, disease, child),
                "Diabetes Type 1" => IsValueInRange(growthRecord.BloodSugarLevel, disease, child),
                "Stunted Growth" => IsValueInRange(growthRecord.Height, disease, child),
                "Asthma" => IsValueInRange(growthRecord.OxygenSaturation, disease, child),
                "Rickets" => IsValueInRange(growthRecord.GrowthHormoneLevel, disease, child),
                "Overweight" => IsBmiInRange(growthRecord, disease, child),
                "Obesity" => IsBmiInRange(growthRecord, disease, child),
                "Hypertension" => IsValueInRange(growthRecord.BloodPressure, disease, child),
                "Failure to Thrive" => IsDevelopmentalMilestonesInRange(growthRecord, disease, child),
                _ => false
            };
        }

        private bool IsBmiInRange(GrowthRecord growthRecord, Disease disease, Child child)
        {
            if (growthRecord.Weight <= 0 || growthRecord.Height <= 0) return false;
            double bmi = growthRecord.Weight / (growthRecord.Height * growthRecord.Height) * 10000;
            return IsValueInRange(bmi, disease, child);
        }

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
