﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common;
using BabyHaven.Common.DTOs.ChildMilestoneDTOs;
using BabyHaven.Repositories;
using BabyHaven.Repositories.Models;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Mappers;

namespace BabyHaven.Services.Services
{
    public class ChildMilestoneService : IChildMilestoneService
    {
        private readonly UnitOfWork _unitOfWork;

        public ChildMilestoneService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork 
                ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<IServiceResult> GetAll()
        {
            try
            {

                var childMilestones = await _unitOfWork.ChildMilestoneRepository
                    .GetAllAsync();

                var childMilestoneDtos = childMilestones.Select(childMilestone => childMilestone.ToChildMilestoneViewAllDto()).ToList();

                return new ServiceResult { Status = Const.SUCCESS_READ_CODE, 
                    Message = Const.SUCCESS_READ_MSG, 
                    Data = childMilestoneDtos };
            }
            catch (Exception ex)
            {

                return HandleException("retrieving child milestones", 
                    ex);
            }
        }

        public async Task<IQueryable<ChildMilestoneViewAllDto>> GetQueryable()
        {

            var childMilestones = await _unitOfWork.ChildMilestoneRepository
                .GetAllAsync();

            return childMilestones
                .Select(childMilestones => childMilestones.ToChildMilestoneViewAllDto())
                .AsQueryable();
        }

        public async Task<IServiceResult> GetById(Guid childId, int milestoneId)
        {
            try
            {

                var childMilestone = await _unitOfWork.ChildMilestoneRepository
                    .GetByIdAsync(childId, milestoneId);

                var dto = childMilestone.ToChildMilestoneViewDetailsDto();

                if (childMilestone == null)
                    return new ServiceResult { Status = Const.FAIL_READ_CODE, 
                        Message = "Child milestone not found." };

                return new ServiceResult { Status = Const.SUCCESS_READ_CODE, 
                    Message = Const.SUCCESS_READ_MSG, 
                    Data = dto };
            }
            catch (Exception ex)
            {

                return HandleException("retrieving the child milestone", 
                    ex);
            }
        }

        public async Task<IServiceResult> GetByChild(string name, string dob, Guid memberId)
        {
            try
            {
                var child = await _unitOfWork.ChildrenRepository
                    .GetChildByNameAndDateOfBirthAsync(name, DateOnly.Parse(dob), memberId);

                var childMilestones = await _unitOfWork.ChildMilestoneRepository
                    .GetByChild(child.ChildId);

                if (childMilestones == null)
                    return new ServiceResult
                    {
                        Status = Const.FAIL_READ_CODE,
                        Message = "Child milestone not found."
                    };

                return new ServiceResult
                {
                    Status = Const.SUCCESS_READ_CODE,
                    Message = Const.SUCCESS_READ_MSG,
                    Data = childMilestones
                };
            }
            catch (Exception ex)
            {

                return HandleException("retrieving the child milestone",
                    ex);
            }
        }

        public async Task<IServiceResult> Create(ChildMilestoneCreateDto dto)
        {
            try
            {

                if (dto == null)
                    return new ServiceResult { Status = Const.FAIL_CREATE_CODE, 
                        Message = Const.FAIL_CREATE_MSG };

                var child = await _unitOfWork.ChildrenRepository
                    .GetChildByNameAndDateOfBirthAsync(dto.ChildName, DateOnly.Parse(dto.DateOfBirth), dto.MemberId);

                if (child == null)
                {
                    return new ServiceResult
                    {
                        Status = Const.FAIL_CREATE_CODE,
                        Message = "Child not found"
                    };
                }

                var member = await _unitOfWork.MemberRepository.GetByIdAsync(dto.MemberId);

                if (member == null)
                    return new ServiceResult
                    {
                        Status = Const.FAIL_CREATE_CODE,
                        Message = "Member not found"
                    };

                var milestone = await _unitOfWork.MilestoneRepository.GetByIdAsync(dto.MilestoneId);

                if (milestone == null)
                {
                    return new ServiceResult
                    {
                        Status = Const.FAIL_READ_CODE,
                        Message = "Milestone not found"
                    };
                }

                await _unitOfWork.ChildMilestoneRepository
                    .CreateAsync(dto.ToChildMilestone(), child.ChildId, milestone.MilestoneId);

                return new ServiceResult { Status = Const.SUCCESS_CREATE_CODE, 
                    Message = Const.SUCCESS_CREATE_MSG, 
                    Data = dto };
            }
            catch (Exception ex)
            {

                return HandleException("creating the child milestone", 
                    ex);
            }
        }

        public async Task<IServiceResult> Update(int milestoneId, ChildMilestoneUpdateDto dto)
        {
            try
            {

                if (dto == null)
                    return new ServiceResult { Status = Const.FAIL_UPDATE_CODE, 
                        Message = Const.FAIL_UPDATE_MSG };

                var child = await _unitOfWork.ChildrenRepository
                    .GetChildByNameAndDateOfBirthAsync(dto.Name, DateOnly.Parse(dto.DateOfBirth), dto.MemberId);

                if (child == null)
                    return new ServiceResult
                    {
                        Status = Const.FAIL_UPDATE_CODE,
                        Message = "Child not found"
                    };

                var existingChildMilestone = await _unitOfWork.ChildMilestoneRepository
                    .GetByIdAsync(child.ChildId, milestoneId);

                if (existingChildMilestone == null)
                    return new ServiceResult { Status = Const.FAIL_READ_CODE, 
                        Message = "Child milestone not found." };

                await _unitOfWork.ChildMilestoneRepository
                    .UpdateAsync(dto.ToChildMilestone(existingChildMilestone));

                return new ServiceResult { Status = Const.SUCCESS_UPDATE_CODE, 
                    Message = Const.SUCCESS_UPDATE_MSG, };
            }
            catch (Exception ex)
            {

                return HandleException("updating the child milestone", 
                    ex);
            }
        }

        public async Task<IServiceResult> Delete(Guid childId, int milestoneId)
        {
            try
            {

                var childMilestone = await _unitOfWork.ChildMilestoneRepository
                    .GetByIdAsync(childId, milestoneId);

                if (childMilestone == null)
                    return new ServiceResult { Status = Const.FAIL_READ_CODE, 
                        Message = "Child milestone not found." };

                await _unitOfWork.ChildMilestoneRepository
                    .RemoveAsync(childMilestone);

                return new ServiceResult { Status = Const.SUCCESS_DELETE_CODE, 
                    Message = Const.SUCCESS_DELETE_MSG };
            }
            catch (Exception ex)
            {

                return HandleException("deleting the child milestone", 
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
