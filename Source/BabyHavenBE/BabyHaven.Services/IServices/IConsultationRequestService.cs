using BabyHaven.Common.DTOs.ConsultationRequestDTOs;
using BabyHaven.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.IServices
{
    public interface IConsultationRequestService
    {
        Task<IServiceResult> GetAll();

        Task<IQueryable<ConsultationRequestViewAllDto>> GetQueryable();

        Task<IServiceResult> GetById(int RequestId);

        Task<IServiceResult> Create(ConsultationRequestCreateDto consultationRequestDto);

        Task<IServiceResult> DeleteById(int RequestId);
    }
}
