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

    }
}
