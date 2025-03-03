using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common;
using BabyHaven.Common.DTOs.ChildrenDTOs;
using BabyHaven.Common.Enum.ChildrenEnums;
using BabyHaven.Repositories;
using BabyHaven.Repositories.Models;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Mappers;

namespace BabyHaven.Services.Services
{
    public class ChildrenService : IChildrenService
    {
        private readonly UnitOfWork _unitOfWork;
        public ChildrenService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        // when the baby is born 
        public async Task<IServiceResult> CreateChild(ChildCreateDto dto)
        {
            try
            {
                if (dto == null)
                    return new ServiceResult { Status = Const.FAIL_CREATE_CODE, Message = Const.FAIL_CREATE_MSG };

                var child = dto.ToChild();

                await _unitOfWork.ChildrenRepository.CreateAsync(child);

                return new ServiceResult { Status = Const.SUCCESS_CREATE_CODE, Message = Const.SUCCESS_CREATE_MSG};
            }
            catch (Exception ex)
            {
                return new ServiceResult { Status = Const.ERROR_EXCEPTION, Message = $"An error occurred while creating the child: {ex.InnerException?.Message}" };
            }
        }

        //when the baby is developing now
        public async Task<IServiceResult> CreateChildForNow(ChildCreateForNowDto dto)
        {
            try
            {
                if (dto == null)
                    return new ServiceResult { Status = Const.FAIL_CREATE_CODE, Message = Const.FAIL_CREATE_MSG };

                var member = await _unitOfWork.MemberRepository.GetMemberByUserId(dto.UserId);

                if (member == null)
                    return new ServiceResult { Status = Const.FAIL_CREATE_CODE, Message = Const.FAIL_CREATE_MSG };

                var child = dto.ToChild(member.MemberId);

                await _unitOfWork.ChildrenRepository.CreateAsync(child);

                return new ServiceResult { Status = Const.SUCCESS_CREATE_CODE, Message = Const.SUCCESS_CREATE_MSG };
            }
            catch (Exception ex)
            {
                return new ServiceResult { Status = Const.ERROR_EXCEPTION, Message = $"An error occurred while creating the child: {ex.InnerException?.Message}" };
            }
        }

        public async Task<IServiceResult> DeleteChildById(Guid childId)
        {
            try
            {
                var child = await _unitOfWork.ChildrenRepository.GetByIdAsync(childId);
                if (child == null)
                    return new ServiceResult { Status = Const.FAIL_READ_CODE, Message = "Child not found." };

                await _unitOfWork.ChildrenRepository.RemoveAsync(child);

                return new ServiceResult { Status = Const.SUCCESS_DELETE_CODE, Message = Const.SUCCESS_DELETE_MSG };
            }
            catch (Exception ex)
            {
                return new ServiceResult { Status = Const.ERROR_EXCEPTION, Message = $"An error occurred while deleting the child: {ex.Message}" };
            }
        }

        public async Task<IServiceResult> GetAllChildren()
        {
            try
            {
                var children = await _unitOfWork.ChildrenRepository.GetAllAsync();
                var childrenDtos = children
                    .Select(children => children.ToChildViewAllDto())
                    .ToList();
                return new ServiceResult { Status = Const.SUCCESS_READ_CODE, Message = Const.SUCCESS_READ_MSG, Data = childrenDtos };
            }
            catch (Exception ex)
            {
                return new ServiceResult { Status = Const.ERROR_EXCEPTION, Message = $"An error occurred while retrieving children: {ex.Message}" };
            }
        }

        public async Task<IServiceResult> GetChildById(Guid childId)
        {
            try
            {
                var child = await _unitOfWork.ChildrenRepository.GetByIdAsync(childId);
                if (child == null)
                    return new ServiceResult { Status = Const.FAIL_READ_CODE, Message = "Child not found." };

                return new ServiceResult { Status = Const.SUCCESS_READ_CODE, Message = Const.SUCCESS_READ_MSG, Data = child.ToChildViewAllDto() };
            }
            catch (Exception ex)
            {
                return new ServiceResult { Status = Const.ERROR_EXCEPTION, Message = $"An error occurred while retrieving the child: {ex.Message}" };
            }
        }

        public async Task<IServiceResult> GetChildByNameDateOfBirthAndMemberId(string childName, string dateOfBirth, Guid memberId)
        {
            try
            {
                var child = await _unitOfWork.ChildrenRepository.GetChildByNameAndDateOfBirthAsync(childName, DateOnly.Parse(dateOfBirth), memberId);
                if (child == null)
                    return new ServiceResult { Status = Const.FAIL_READ_CODE, Message = "Child not found." };

                return new ServiceResult { Status = Const.SUCCESS_READ_CODE, Message = Const.SUCCESS_READ_MSG, Data = child.ToChildViewAllDto() };
            }
            catch (Exception ex)
            {
                return new ServiceResult { Status = Const.ERROR_EXCEPTION, Message = $"An error occurred while retrieving the child: {ex.Message}" };
            }
        }

        public async Task<IServiceResult> GetChildrenByMemberId(Guid memberId)
        {
            try
            {
                var children = await _unitOfWork.ChildrenRepository.GetChildrenByMemberIdAsync(memberId);
                var childrenDtos = children
                    .Select(child => child.ToChildViewAllDto())
                    .ToList();
                return new ServiceResult { Status = Const.SUCCESS_READ_CODE, Message = Const.SUCCESS_READ_MSG, Data = childrenDtos };
            }
            catch (Exception ex)
            {
                return new ServiceResult { Status = Const.ERROR_EXCEPTION, Message = $"An error occurred while retrieving children: {ex.Message}" };
            }
        }

        public async Task<IServiceResult> PreDeleteById(Guid childId)
        {
            try
            {
                var child = await _unitOfWork.ChildrenRepository.GetByIdAsync(childId);
                if (child == null)
                    return new ServiceResult { Status = Const.FAIL_READ_CODE, Message = "Child not found." };

                child.Status = ChildrenStatus.Inactive.ToString();
                await _unitOfWork.ChildrenRepository.UpdateAsync(child);

                return new ServiceResult { Status = Const.SUCCESS_UPDATE_CODE, Message = "Child marked for deletion." };
            }
            catch (Exception ex)
            {
                return new ServiceResult { Status = Const.ERROR_EXCEPTION, Message = $"An error occurred while marking the child for deletion: {ex.Message}" };
            }
        }

        public async Task<IServiceResult> RecoverById(Guid childId)
        {
            try
            {
                var child = await _unitOfWork.ChildrenRepository.GetByIdAsync(childId);
                if (child == null)
                    return new ServiceResult { Status = Const.FAIL_READ_CODE, Message = "Child not found." };

                child.Status = ChildrenStatus.Active.ToString();
                await _unitOfWork.ChildrenRepository.UpdateAsync(child);

                return new ServiceResult { Status = Const.SUCCESS_UPDATE_CODE, Message = "Child recovered successfully." };
            }
            catch (Exception ex)
            {
                return new ServiceResult { Status = Const.ERROR_EXCEPTION, Message = $"An error occurred while recovering the child: {ex.Message}" };
            }
        }

        public async Task<IServiceResult> UpdateChildById(ChildUpdateDto dto)
        {
            try
            {
                if (dto == null)
                    return new ServiceResult { Status = Const.FAIL_UPDATE_CODE, Message = Const.FAIL_UPDATE_MSG };

                var child = await _unitOfWork.ChildrenRepository.GetByIdAsync(dto.ChildId);
                if (child == null)
                    return new ServiceResult { Status = Const.FAIL_READ_CODE, Message = "Child not found." };

                child = dto.ToChild(child);

                await _unitOfWork.ChildrenRepository.UpdateAsync(child);

                return new ServiceResult { Status = Const.SUCCESS_UPDATE_CODE, Message = Const.SUCCESS_UPDATE_MSG, Data = child.ToChildViewAllDto() };
            }
            catch (Exception ex)
            {
                return new ServiceResult { Status = Const.ERROR_EXCEPTION, Message = $"An error occurred while updating the child: {ex.Message}" };
            }
        }
    }
}
