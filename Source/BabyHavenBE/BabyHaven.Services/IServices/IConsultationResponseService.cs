using BabyHaven.Common.DTOs.ConsultationResponseDTOs;
using BabyHaven.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.IServices
{
    public interface IConsultationResponseService
    {
        Task<IServiceResult> GetAll();
        Task<IServiceResult> GetById(int ResponseId);
        Task<IServiceResult> DeleteById(int ResponseId);
    }
}
