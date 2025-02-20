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
    }
}
