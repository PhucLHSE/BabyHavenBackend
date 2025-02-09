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
                return new ServiceResult { Status = Const.ERROR_EXCEPTION, Message = $"An error occurred while retrieving alerts: {ex.InnerException?.ToString()}" };
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
                return new ServiceResult { Status = Const.ERROR_EXCEPTION, Message = $"An error occurred while retrieving the alert: {ex.Message}" };
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

                return new ServiceResult { Status = Const.SUCCESS_CREATE_CODE, Message = Const.SUCCESS_CREATE_MSG, Data = alert };
            }
            catch (Exception ex)
            {
                return new ServiceResult { Status = Const.ERROR_EXCEPTION, Message = $"An error occurred while creating the alert: {ex.Message}" };
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

                return new ServiceResult { Status = Const.SUCCESS_UPDATE_CODE, Message = Const.SUCCESS_UPDATE_MSG, Data = alert };
            }
            catch (Exception ex)
            {
                return new ServiceResult { Status = Const.ERROR_EXCEPTION, Message = $"An error occurred while updating the alert: {ex.Message}" };
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
                return new ServiceResult { Status = Const.ERROR_EXCEPTION, Message = $"An error occurred while deleting the alert: {ex.Message}" };
            }
        }
    }
}
