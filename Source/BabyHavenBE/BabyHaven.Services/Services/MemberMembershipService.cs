using BabyHaven.Common;
using BabyHaven.Repositories;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common.DTOs.MemberMembershipDTOs;
using BabyHaven.Services.Mappers;

namespace BabyHaven.Services.Services
{
    public class MemberMembershipService : IMemberMembershipService
    {
        private readonly UnitOfWork _unitOfWork;

        public MemberMembershipService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IServiceResult> GetAll()
        {
            var memberMemberships = await _unitOfWork.MemberMembershipRepository
            .GetAllMemberMembershipAsync();

            if (memberMemberships == null || !memberMemberships.Any())
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG,
                    new List<MemberMembershipViewAllDto>());
            }
            else
            {
                var memberMembershipDtos = memberMemberships
                    .Select(memberMemberships => memberMemberships.MapToMemberMembershipViewAllDto())
                    .ToList();

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG,
                    memberMembershipDtos);
            }
        }

        public async Task<IServiceResult> GetById(Guid MemberMembershipId)
        {
            var memberMembership = await _unitOfWork.MemberMembershipRepository
                .GetByIdMemberMembershipAsync(MemberMembershipId);

            if (memberMembership == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG,
                    new MemberMembershipViewDetailsDto());
            }
            else
            {
                var memberMembershipDto = memberMembership.MapToMemberMembershipViewDetailsDto();

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG,
                    memberMembershipDto);
            }
        }
    }
}
