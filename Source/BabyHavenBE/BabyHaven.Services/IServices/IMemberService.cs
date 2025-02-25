using BabyHaven.Common.DTOs.MemberDTOs;
using BabyHaven.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.IServices
{
    public interface IMemberService
    {
        Task<IServiceResult> GetAll();
        Task<IServiceResult> GetById(Guid MemberId);
        Task<IServiceResult> GetByUserId(Guid userId);
        Task<IServiceResult> Update(MemberUpdateDto memberUpdateDto);
        Task<IServiceResult> DeleteById(Guid MemberId);
    }
}
