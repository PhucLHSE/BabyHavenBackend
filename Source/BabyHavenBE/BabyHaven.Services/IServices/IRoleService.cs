using BabyHaven.Common.DTOs.FeatureDTOs;
using BabyHaven.Common.DTOs.RoleDTOs;
using BabyHaven.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.IServices
{
    public interface IRoleService
    {
        Task<IServiceResult> GetAll();
        Task<IServiceResult> GetById(int RoleId);
        Task<IServiceResult> Create(RoleCreateDto roleDto);
        Task<IServiceResult> Update(RoleUpdateDto roleDto);
        Task<IServiceResult> DeleteById(int RoleId);
    }
}
