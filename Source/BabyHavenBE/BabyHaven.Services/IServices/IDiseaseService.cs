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
        Task<IServiceResult> GetById(int DiseaseId);
        Task<IServiceResult> Save(DiseaseCreateDto diseaseCreateDto);
    }
}
