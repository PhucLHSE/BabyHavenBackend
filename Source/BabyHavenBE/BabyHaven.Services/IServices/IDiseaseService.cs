using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common.DTOs.DiseaseDTOs;
using BabyHaven.Common.DTOs.MembershipPackageDTOs;
using BabyHaven.Services.Base;

namespace BabyHaven.Services.IServices
{
    public interface IDiseaseService
    {
        Task<IServiceResult> GetAll();
        Task<IQueryable<DiseaseViewAllDto>> GetQueryable();
        Task<IServiceResult> GetById(int DiseaseId);
        Task<IServiceResult> Create(DiseaseCreateDto diseaseCreateDto);
        Task<IServiceResult> DeleteById(int DiseaseId);
        Task<IServiceResult> UpdateById(int DiseaseId, DiseaseUpdateDto diseaseUpdateDto);
        Task<IServiceResult> PreDeleteById(int DiseaseId);
        Task<IServiceResult> RecoverById(int DiseaseId);
    }
}
