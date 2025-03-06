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

        public async Task<IServiceResult> CreateGrowthRecordRequired(GrowthRecordRequiredDto dto)
        {
            try
            {
                if (dto == null)
                {
                    return new ServiceResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
                }

                var record = await _unitOfWork.GrowthRecordRepository.CreateAsync(dto.MapToGrowthRecordEntity());
                return new ServiceResult { Status = Const.SUCCESS_CREATE_CODE, Message = Const.SUCCESS_CREATE_MSG, Data = record };
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, ex.InnerException?.Message);
            }
        }

        public async Task<IServiceResult> CreateGrowthRecord(GrowthRecordCreateDto dto)
        {
            try
            {
                if (dto == null)
                {
                    return new ServiceResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
                }

                var child = await _unitOfWork.ChildrenRepository.GetChildByNameAndDateOfBirthAsync(dto.name, DateOnly.Parse(dto.DateOfBirth), dto.RecordedBy);

                if (child == null)
                {
                    return new ServiceResult(Const.FAIL_CREATE_CODE, "Child not found");
                }

                var record = await _unitOfWork.GrowthRecordRepository.CreateAsync(dto.MapToGrowthRecordEntity(child));
                return new ServiceResult { Status = Const.SUCCESS_CREATE_CODE, Message = Const.SUCCESS_CREATE_MSG, Data = record };
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, ex.InnerException.Message);
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

        public async Task<IServiceResult> UpdateGrowthRecord(GrowthRecordUpdateDto dto)
        {
            try
            {
                var growthRecord = await _unitOfWork.GrowthRecordRepository.GetGrowthRecordById(dto.RecordId, dto.ChildId);
                if (growthRecord == null)
                {
                    return new ServiceResult(Const.FAIL_UPDATE_CODE, "Record not found");
                }

                // Map the updated properties from the DTO to the entity
                growthRecord.MapToUpdatedGrowthRecord(dto);

                await _unitOfWork.GrowthRecordRepository.UpdateAsync(growthRecord);

                return new ServiceResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, growthRecord);
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, $"{Const.FAIL_UPDATE_MSG}: {ex.Message}");
            }
        }

        public async Task<IQueryable<GrowthRecordViewAllDto>> GetQueryable()
        {
            var growthRecords = await _unitOfWork.GrowthRecordRepository
                .GetAllAsync();

            return growthRecords
                .Select(record => record.MapToGrowthRecordViewAll())
                .AsQueryable();
        }
    }
}
