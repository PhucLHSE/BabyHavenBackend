using BabyHaven.Common.DTOs.FeatureDTOs;
using BabyHaven.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.IServices
{
    public interface IFeatureService
    {
        Task<IServiceResult> GetAll();
        Task<IServiceResult> GetById(int FeatureId);
        Task<IServiceResult> Create(FeatureCreateDto featureDto);
    }
}
