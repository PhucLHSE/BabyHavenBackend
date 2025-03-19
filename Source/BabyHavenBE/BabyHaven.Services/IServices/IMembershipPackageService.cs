using BabyHaven.Common.DTOs.MembershipPackageDTOs;
using BabyHaven.Repositories.Models;
using BabyHaven.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.IServices
{
    public interface IMembershipPackageService
    {
        Task<IServiceResult> GetAll();

        Task<IQueryable<MembershipPackageViewAllDto>> GetQueryable();

        Task<IServiceResult> GetById(int PackageId);

        Task<IServiceResult> Create(MembershipPackageCreateDto membershipPackageDto);

        Task<IServiceResult> Update(MembershipPackageUpdateDto membershipPackageDto);

        Task<IServiceResult> DeleteById(int PackageId);
    }
}
