using BabyHaven.Common.DTOs.MemberMembershipDTOs;
using BabyHaven.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.IServices
{
    public interface IMemberMembershipService
    {
        Task<IServiceResult> GetAll();
        Task<IServiceResult> GetById(Guid MemberMembershipId);
        Task<IServiceResult> Create(MemberMembershipCreateDto memberMembershipDto);
        Task<IServiceResult> Update(MemberMembershipUpdateDto memberMembershipDto);
        Task<IServiceResult> DeleteById(Guid MemberMembershipId);
    }
}
