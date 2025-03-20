using BabyHaven.Common.DTOs.ConsultationRequestDTOs;
using BabyHaven.Common.DTOs.TransactionDTOs;
using BabyHaven.Common;
using BabyHaven.Repositories;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common.DTOs.ConsultationResponseDTOs;
using BabyHaven.Repositories.Models;
using BabyHaven.Common.Enum.ConsultationResponseEnums;

namespace BabyHaven.Services.Services
{
    public class ConsultationResponseService : IConsultationResponseService
    {
        private readonly UnitOfWork _unitOfWork;

        public ConsultationResponseService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IServiceResult> GetAll()
        {

            var consultationResponses = await _unitOfWork.ConsultationResponseRepository
                .GetAllConsultationResponseAsync();

            if (consultationResponses == null || !consultationResponses.Any())
            {

                return new ServiceResult(Const.WARNING_NO_DATA_CODE, 
                    Const.WARNING_NO_DATA_MSG,
                    new List<ConsultationResponseViewAllDto>());
            }
            else
            {

                var consultationResponseDtos = consultationResponses
                    .Select(consultationResponses => consultationResponses.MapToConsultationResponseViewAllDto())
                    .ToList();

                return new ServiceResult(Const.SUCCESS_READ_CODE,
                    Const.SUCCESS_READ_MSG,
                    consultationResponseDtos);
            }
        }

        public async Task<IQueryable<ConsultationResponseViewAllDto>> GetQueryable()
        {

            var consultationResponses = await _unitOfWork.ConsultationResponseRepository
                .GetAllConsultationResponseAsync();

            return consultationResponses
                .Select(consultationResponses => consultationResponses.MapToConsultationResponseViewAllDto())
                .AsQueryable();
        }

        public async Task<IServiceResult> GetById(int ResponseId)
        {

            var consultationResponse = await _unitOfWork.ConsultationResponseRepository
                .GetByIdConsultationResponseAsync(ResponseId);

            if (consultationResponse == null)
            {

                return new ServiceResult(Const.WARNING_NO_DATA_CODE, 
                    Const.WARNING_NO_DATA_MSG,
                    new ConsultationResponseViewDetailsDto());
            }
            else
            {

                var consultationResponseDto = consultationResponse.MapToConsultationResponseViewDetailsDto();

                return new ServiceResult(Const.SUCCESS_READ_CODE, 
                    Const.SUCCESS_READ_MSG,
                    consultationResponseDto);
            }
        }

        public async Task<IServiceResult> GetByMemberId(Guid memberId)
        {

            var consultationResponses = await _unitOfWork.ConsultationResponseRepository
                .GetByMemberIdConsultationResponseAsync(memberId);

            if (consultationResponses == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, 
                    Const.WARNING_NO_DATA_MSG,
                    new ConsultationResponseViewDetailsDto());
            }
            else
            {

                var consultationResponseDtos = consultationResponses
                    .Select(consultationResponses => consultationResponses.MapToConsultationResponseViewAllDto())
                    .ToList();

                return new ServiceResult(Const.SUCCESS_READ_CODE, 
                    Const.SUCCESS_READ_MSG,
                    consultationResponseDtos);
            }
        }

        public async Task<IServiceResult> Create(ConsultationResponseCreateDto dto)
        {
            try
            {

                if (dto == null)
                {
                    return new ServiceResult(Const.FAIL_CREATE_CODE, 
                        Const.FAIL_CREATE_MSG);
                }

                var consultationRequest = await _unitOfWork.ConsultationRequestRepository
                    .GetByIdConsultationRequestAsync(dto.RequestId);

                if (consultationRequest == null)
                {

                    return new ServiceResult(Const.FAIL_CREATE_CODE,
                        "Consultation Request not found");
                }

                var consultationResponse = await _unitOfWork.ConsultationResponseRepository
                    .CreateAsync(dto.MapToConsultationResponse());

                return new ServiceResult { Status = Const.SUCCESS_CREATE_CODE, 
                    Message = Const.SUCCESS_CREATE_MSG, 
                    Data = consultationResponse };
            }
            catch (Exception ex)
            {

                return new ServiceResult(Const.ERROR_EXCEPTION, 
                    ex.InnerException?.Message);
            }
        }

        public async Task<IServiceResult> DeleteById(int ResponseId)
        {
            try
            {

                var consultationResponse = await _unitOfWork.ConsultationResponseRepository
                    .GetByIdConsultationResponseAsync(ResponseId);

                if (consultationResponse == null)
                {

                    return new ServiceResult(Const.WARNING_NO_DATA_CODE, 
                        Const.WARNING_NO_DATA_MSG,
                        new ConsultationResponseDeleteDto());
                }
                else
                {

                    var deleteConsultationResponseDto = consultationResponse.MapToConsultationResponseDeleteDto();

                    var result = await _unitOfWork.ConsultationResponseRepository
                        .RemoveAsync(consultationResponse);

                    if (result)
                    {

                        return new ServiceResult(Const.SUCCESS_DELETE_CODE, 
                            Const.SUCCESS_DELETE_MSG,
                            deleteConsultationResponseDto);
                    }
                    else
                    {

                        return new ServiceResult(Const.FAIL_DELETE_CODE, 
                            Const.FAIL_DELETE_MSG,
                            deleteConsultationResponseDto);
                    }
                }
            }
            catch (Exception ex)
            {

                return new ServiceResult(Const.ERROR_EXCEPTION, 
                    ex.ToString());
            }
        }

        public async Task<IServiceResult> UpdateStatus(int requestId, string status)
        {
            try
            {
                var consultationResponse = await _unitOfWork.ConsultationResponseRepository
                                    .GetByRequestId(requestId);
                if (consultationResponse == null)
                {
                    return new ServiceResult(Const.WARNING_NO_DATA_CODE,
                        Const.WARNING_NO_DATA_MSG);
                }

                consultationResponse.Status = status;

                var result = await _unitOfWork.ConsultationResponseRepository
                    .UpdateAsync(consultationResponse);

                return new ServiceResult(Const.SUCCESS_UPDATE_CODE,
                    Const.SUCCESS_UPDATE_MSG,
                    consultationResponse);

            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION,
                    ex.InnerException.ToString());
            }
        }
    }
}
