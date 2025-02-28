using BabyHaven.Common;
using BabyHaven.Repositories;
using BabyHaven.Services.Base;
using BabyHaven.Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common.DTOs.ConsultationRequestDTOs;
using BabyHaven.Services.IServices;
using BabyHaven.Common.DTOs.TransactionDTOs;

namespace BabyHaven.Services.Services
{
    public class ConsultationRequestService : IConsultationRequestService
    {
        private readonly UnitOfWork _unitOfWork;

        public ConsultationRequestService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IServiceResult> GetAll()
        {
            var consultationRequests = await _unitOfWork.ConsultationRequestRepository
                .GetAllConsultationRequestAsync();

            if (consultationRequests == null || !consultationRequests.Any())
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG,
                    new List<ConsultationRequestViewAllDto>());
            }
            else
            {
                var consultationRequestDtos = consultationRequests
                    .Select(consultationRequests => consultationRequests.MapToConsultationRequestViewAllDto())
                    .ToList();

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG,
                    consultationRequestDtos);
            }
        }

        public async Task<IServiceResult> GetById(int RequestId)
        {
            var consultationRequest = await _unitOfWork.ConsultationRequestRepository
                .GetByIdConsultationRequestAsync(RequestId);

            if (consultationRequest == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG,
                    new ConsultationRequestViewDetailsDto());
            }
            else
            {
                var consultationRequestDto = consultationRequest.MapToConsultationRequestViewDetailsDto();

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG,
                    consultationRequestDto);
            }
        }

        public async Task<IServiceResult> Create(ConsultationRequestCreateDto consultationRequestDto)
        {
            try
            {
                // Retrieve mappings: MemberName -> MemberId and ChildName -> ChildId
                var memberNameToIdMapping = await _unitOfWork.MemberRepository
                    .GetAllMemberNameToIdMappingAsync();

                var childNameToIdMapping = await _unitOfWork.ChildrenRepository
                    .GetAllChildNameToIdMappingAsync();

                // Check existence and retrieve MemberId
                if (!memberNameToIdMapping
                    .TryGetValue(consultationRequestDto.MemberName, out var memberId))
                {
                    return new ServiceResult(Const.FAIL_UPDATE_CODE,
                        $"MemberName '{consultationRequestDto.MemberName}' does not exist.");
                }

                // Check if the provided ChildName exists
                if (!childNameToIdMapping
                    .TryGetValue(consultationRequestDto.ChildName, out var childId))
                {
                    return new ServiceResult(Const.FAIL_UPDATE_CODE,
                        $"ChildName '{consultationRequestDto.ChildName}' does not exist.");
                }

                // Map the DTO to an entity object
                var newConsultationRequest = consultationRequestDto.MapToConsultationRequest(memberId, childId);

                // Save the new entity to the database
                var result = await _unitOfWork.ConsultationRequestRepository
                    .CreateAsync(newConsultationRequest);

                if (result > 0)
                {
                    var memberName = await _unitOfWork.MemberRepository
                        .GetByIdMemberAsync(newConsultationRequest.MemberId);

                    var childName = await _unitOfWork.ChildrenRepository
                        .GetByIdAsync(newConsultationRequest.ChildId);

                    // Assign retrieved details to navigation properties
                    newConsultationRequest.Member = memberName;
                    newConsultationRequest.Child = childName;

                    // Map the saved entity to a response DTO
                    var responseDto = newConsultationRequest.MapToConsultationRequestViewDetailsDto();

                    return new ServiceResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG,
                        responseDto);
                }
                else
                {
                    return new ServiceResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IServiceResult> DeleteById(int RequestId)
        {
            try
            {
                var consultationRequest = await _unitOfWork.ConsultationRequestRepository
                    .GetByIdConsultationRequestAsync(RequestId);

                if (consultationRequest == null)
                {
                    return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG,
                        new ConsultationRequestDeleteDto());
                }
                else
                {
                    var deleteConsultationRequestDto = consultationRequest.MapToConsultationRequestDeleteDto();

                    var result = await _unitOfWork.ConsultationRequestRepository
                        .RemoveAsync(consultationRequest);

                    if (result)
                    {
                        return new ServiceResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG,
                            deleteConsultationRequestDto);
                    }
                    else
                    {
                        return new ServiceResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG,
                            deleteConsultationRequestDto);
                    }
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }
    }
}
