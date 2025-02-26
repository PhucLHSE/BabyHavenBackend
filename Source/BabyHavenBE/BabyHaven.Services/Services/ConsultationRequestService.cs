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
            var consultationRequests = await _unitOfWork.ConsultationRequestRepository.GetAllConsultationRequestAsync();

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
            var consultantRequest = await _unitOfWork.ConsultationRequestRepository
                .GetConsultationRequestByIdAsync(RequestId);

            if (consultantRequest == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG,
                    new TransactionViewDetailsDto());
            }
            else
            {
                var consultantRequestDto = consultantRequest.MapToConsultationRequestViewDetailsDto();

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG,
                    consultantRequestDto);
            }
        }
    }
}
