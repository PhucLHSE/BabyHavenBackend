using BabyHaven.Common.DTOs.PackageFeatureDTOs;
using BabyHaven.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.IServices
{
    public interface IPackageFeatureService
    {
        Task<IServiceResult> GetAll();
        Task<IServiceResult> GetById(int packageId, int featureId);
        Task<IServiceResult> Create(PackageFeatureCreateDto packageFeatureCreateDto);
        Task<IServiceResult> Update(PackageFeatureUpdateDto packageFeatureUpdateDto);
    }
}
