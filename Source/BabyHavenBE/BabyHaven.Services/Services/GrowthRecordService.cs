﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common;
using BabyHaven.Common.DTOs.GrowthRecordDTOs;
using BabyHaven.Repositories.Repositories;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Mappers;

namespace BabyHaven.Services.Services
{
    public class GrowthRecordService : IGrowthRecordService
    {
        private readonly GrowthRecordRepository _growthRecordRepository;

        public GrowthRecordService(GrowthRecordRepository growthRecordRepository)
        {
            _growthRecordRepository = growthRecordRepository ?? throw new ArgumentNullException(nameof(growthRecordRepository));
        }

        public async Task<IServiceResult> CreateGrowthRecordChild(GrowthRecordChildDto dto)
        {
            try
            {
                var growthRecord = dto.MapToGrowthRecordEntity();

                await _growthRecordRepository.CreateAsync(growthRecord);

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
                var growthRecord = dto.MapToGrowthRecordEntity();

                await _growthRecordRepository.CreateAsync(growthRecord);

                return new ServiceResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, growthRecord);
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, $"{Const.FAIL_CREATE_MSG}: {ex.Message}");
            }
        }

        public async Task<IServiceResult> CreateGrowthRecordTeenager(GrowthRecordTeenagerDto dto)
        {
            try
            {
                var growthRecord = dto.MapToGrowthRecordEntity();

                await _growthRecordRepository.CreateAsync(growthRecord);

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

                await _growthRecordRepository.CreateAsync(growthRecord);

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
                var growthRecord = await _growthRecordRepository.GetByIdAsync(recordId);
                if (growthRecord == null)
                {
                    return new ServiceResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
                }

                await _growthRecordRepository.RemoveAsync(growthRecord);

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
                var growthRecords = await _growthRecordRepository.GetAllGrowthRecordsByChild(childId);
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
                var growthRecord = await _growthRecordRepository.GetGrowthRecordById(recordId, childId);
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
                var growthRecords = await _growthRecordRepository.GetRecordsByDateRangeAsync(childId, startDate, endDate);
                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, growthRecords);
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, $"{Const.FAIL_READ_MSG}: {ex.Message}");
            }
        }
    }
}
