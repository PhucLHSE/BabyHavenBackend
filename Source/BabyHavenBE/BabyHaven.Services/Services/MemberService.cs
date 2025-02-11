using BabyHaven.Common;
using BabyHaven.Repositories;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common.DTOs.MemberDTOs;
using BabyHaven.Services.Mappers;

namespace BabyHaven.Services.Services
{
    public class MemberService : IMemberService
    {
        private readonly UnitOfWork _unitOfWork;

        public MemberService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IServiceResult> GetAll()
        {
            var members = await _unitOfWork.MemberRepository
                .GetAllMemberAsync();

            if (members == null || !members.Any())
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG,
                    new List<MemberViewAllDto>());
            }
            else
            {
                var memberDtos = members
                    .Select(promotions => promotions.MapToMemberViewAllDto())
                    .ToList();

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG,
                    memberDtos);
            }
        }
    }
}
