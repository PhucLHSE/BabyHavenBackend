using BabyHaven.Common.DTOs.FeatureDTOs;
using BabyHaven.Common.DTOs.SpecializationDTOs;
using BabyHaven.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.IServices
{
    public interface ISpecializationService
    {
        Task<IServiceResult> GetAll();

        Task<IQueryable<SpecializationViewAllDto>> GetQueryable();

        Task<IServiceResult> GetById(int SpecializationId);

        Task<IServiceResult> Create(SpecializationCreateDto specializationDto);

        Task<IServiceResult> Update(SpecializationUpdateDto specializationDto);

        Task<IServiceResult> DeleteById(int SpecializationId);
    }
}
