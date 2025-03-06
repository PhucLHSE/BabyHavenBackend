using BabyHaven.Common.DTOs.UserAccountDTOs;
using BabyHaven.Repositories.Models;
using BabyHaven.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.IServices
{
    public interface IUserAccountService
    {
        Task<UserAccount> Authenticate(string email, string password);
        Task<IServiceResult> AuthenticateWithGoogle(LoginGoogleDto googleDto);
        Task<IServiceResult> GetAll();
        Task<IQueryable<UserAccountViewAllDto>> GetQueryable();
        Task<IServiceResult> GetById(Guid UserId);
        Task<IServiceResult> Update(UserAccountUpdateDto userDto);
        Task<IServiceResult> DeleteById(Guid UserId);
        Task<UserAccount?> GetByEmailAsync(string email);
        Task<bool> CreateAsync(UserAccount userAccount);
        Task<IServiceResult> Create(UserAccountCreateDto userDto);
    }
}
