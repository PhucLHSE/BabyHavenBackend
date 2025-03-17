using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common;
using BabyHaven.Common.DTOs.MilestoneDTOS;
using BabyHaven.Repositories;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Mappers;

namespace BabyHaven.Services.Services
{
    public class MilestoneService : IMilestoneService
    {
        private readonly UnitOfWork _unitOfWork;

        public MilestoneService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork 
                ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<IServiceResult> GetAll()
        {
            try
            {

                var milestones = await _unitOfWork.MilestoneRepository
                    .GetAllAsync();

                return new ServiceResult { Status = Const.SUCCESS_READ_CODE, 
                    Message = Const.SUCCESS_READ_MSG, 
                    Data = milestones };
            }
            catch (Exception ex)
            {

                return HandleException("retrieving milestones", 
                    ex);
            }
        }

        public async Task<IQueryable<MilestoneViewAllDto>> GetQueryable()
        {

            var milestones = await _unitOfWork.MilestoneRepository
                .GetAllAsync();

            return milestones
                .Select(milestones => milestones.ToMilestoneViewAllDto())
                .AsQueryable();
        }

        public async Task<IServiceResult> GetById(int milestoneId)
        {
            try
            {

                var milestone = await _unitOfWork.MilestoneRepository
                    .GetByIdAsync(milestoneId);

                if (milestone == null)
                    return new ServiceResult { Status = Const.FAIL_READ_CODE, 
                        Message = "Milestone not found." };

                return new ServiceResult { Status = Const.SUCCESS_READ_CODE, 
                    Message = Const.SUCCESS_READ_MSG, 
                    Data = milestone };
            }
            catch (Exception ex)
            {

                return HandleException("retrieving the milestone", 
                    ex);
            }
        }

        public async Task<IServiceResult> Create(MilestoneCreateDto dto)
        {
            try
            {
                if (dto == null)
                    return new ServiceResult { Status = Const.FAIL_CREATE_CODE, 
                        Message = Const.FAIL_CREATE_MSG };

                var milestone = dto.ToMilestone();

                await _unitOfWork.MilestoneRepository
                    .CreateAsync(milestone);

                return new ServiceResult { Status = Const.SUCCESS_CREATE_CODE, 
                    Message = Const.SUCCESS_CREATE_MSG, 
                    Data = milestone };
            }
            catch (Exception ex)
            {

                return HandleException("creating the milestone", 
                    ex);
            }
        }

        public async Task<IServiceResult> Update(MilestoneUpdateDto dto)
        {
            try
            {
                if (dto == null)
                    return new ServiceResult { Status = Const.FAIL_UPDATE_CODE, 
                        Message = Const.FAIL_UPDATE_MSG };

                var existingMilestone = await _unitOfWork.MilestoneRepository
                    .GetByIdAsync(dto.MilestoneId);

                if (existingMilestone == null)
                    return new ServiceResult { Status = Const.FAIL_READ_CODE, 
                        Message = "Milestone not found." };

                await _unitOfWork.MilestoneRepository
                    .UpdateAsync(dto.ToMilestone(existingMilestone));

                return new ServiceResult { Status = Const.SUCCESS_UPDATE_CODE, 
                    Message = Const.SUCCESS_UPDATE_MSG, 
                    Data = dto };
            }
            catch (Exception ex)
            {

                return HandleException("updating the milestone", 
                    ex);
            }
        }

        public async Task<IServiceResult> Delete(int milestoneId)
        {
            try
            {

                var milestone = await _unitOfWork.MilestoneRepository
                    .GetByIdAsync(milestoneId);

                if (milestone == null)
                    return new ServiceResult { Status = Const.FAIL_READ_CODE, 
                        Message = "Milestone not found." };

                await _unitOfWork.MilestoneRepository
                    .RemoveAsync(milestone);

                return new ServiceResult { Status = Const.SUCCESS_DELETE_CODE, 
                    Message = Const.SUCCESS_DELETE_MSG };
            }
            catch (Exception ex)
            {

                return HandleException("deleting the milestone", 
                    ex);
            }
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
