﻿using BabyHaven.Common.DTOs.MemberMembershipDTOs;
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

        Task<IQueryable<MemberMembershipViewAllDto>> GetQueryable();

        Task<IServiceResult> GetById(Guid memberMembershipId);

        Task<IServiceResult> GetByMemberId(Guid memberId);

        Task<IServiceResult> Create(MemberMembershipCreateDto memberMembershipDto);

        Task<IServiceResult> Update(MemberMembershipUpdateDto memberMembershipDto);

        Task<IServiceResult> DeleteById(Guid memberMembershipId);

        Task<IServiceResult> PreDeleteById(Guid memberMembershipId);
    }
}
