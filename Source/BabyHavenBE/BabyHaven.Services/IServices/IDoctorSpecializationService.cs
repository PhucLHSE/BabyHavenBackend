using BabyHaven.Common.DTOs.DoctorSpecializationDTOs;
using BabyHaven.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.IServices
{
    public interface IDoctorSpecializationService
    {
        Task<IServiceResult> GetAll();

        Task<IQueryable<DoctorSpecializationViewAllDto>> GetQueryable();

        Task<IServiceResult> GetById(int DoctorSpecializationId);

        Task<IServiceResult> Create(DoctorSpecializationCreateDto doctorSpecializationCreateDto);

        Task<IServiceResult> Update(DoctorSpecializationUpdateDto doctorSpecializationUpdateDto);

        Task<IServiceResult> DeleteById(int DoctorSpecializationId);
    }
}
