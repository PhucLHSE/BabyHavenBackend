using BabyHaven.Common.DTOs.PackagePromotionDTOs;
using BabyHaven.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.IServices
{
    public interface IPackagePromotionService
    {
        Task<IServiceResult> GetAll();
        Task<IServiceResult> GetById(int packageId, Guid promotionId);
        Task<IServiceResult> Create(PackagePromotionCreateDto packagePromotionCreateDto);
    }
}
