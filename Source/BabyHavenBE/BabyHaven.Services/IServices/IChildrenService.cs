using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common.DTOs.ChildrenDTOs;
using BabyHaven.Services.Base;

namespace BabyHaven.Services.IServices
{
    public interface IChildrenService
    {
        Task<IServiceResult> CreateChild(ChildCreateDto dto);
        Task<IServiceResult> CreateChildForNow(ChildCreateForNowDto dto);
        Task<IServiceResult> UpdateChildById(ChildUpdateDto dto);
        Task<IServiceResult> DeleteChildById(Guid childId);
        Task<IServiceResult> GetChildById(Guid childId);
        Task<IServiceResult> GetAllChildren();
        Task<IServiceResult> GetChildrenByMemberId(Guid memberId);
        Task<IServiceResult> GetChildByNameDateOfBirthAndMemberId(string childName, string dateOfBirth, Guid memberId);
        Task<IServiceResult> PreDeleteById(Guid childId);
        Task<IServiceResult> RecoverById(Guid childId);
    }
}
