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

namespace BabyHaven.Services.Services
{
    public class ConsultationResponseService: IConsultationResponseService
    {
        private readonly UnitOfWork _unitOfWork;

        public ConsultationResponseService()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IServiceResult> GetAll()
        {
            var consultationResponses = await _unitOfWork.ConsultationResponseRepository.GetAllConsultationResponseAsync();

            if (consultationResponses == null || !consultationResponses.Any())
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG,
                    new List<ConsultationRequestViewAllDto>());
            }
            else
            {
                var consultationResponseDtos = consultationResponses
                    .Select(consultationResponses => consultationResponses.MapToConsultationResponseViewAllDto())
                    .ToList();

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG,
                    consultationResponseDtos);
            }
        }

        public async Task<IServiceResult> GetById(int ResponseId)
        {
            var consultationResponse = await _unitOfWork.ConsultationResponseRepository
                .GetByIdConsultationResponseAsync(ResponseId);

            if (consultationResponse == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG,
                    new ConsultationResponseViewDetailsDto());
            }
            else
            {
                var consultationResponseDto = consultationResponse.MapToConsultationResponseViewDetailsDto();

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG,
                    consultationResponseDto);
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
                    return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG,
                        new ConsultationResponseDeleteDto());
                }
                else
                {
                    var deleteConsultationResponseDto = consultationResponse.MapToConsultationResponseDeleteDto();

                    var result = await _unitOfWork.ConsultationResponseRepository
                        .RemoveAsync(consultationResponse);

                    if (result)
                    {
                        return new ServiceResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG,
                            deleteConsultationResponseDto);
                    }
                    else
                    {
                        return new ServiceResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG,
                            deleteConsultationResponseDto);
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
