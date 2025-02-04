using BabyHaven.Repositories.Models;
using BabyHaven.Repositories;
using BabyHaven.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Repositories.Repositories;

namespace BabyHaven.Services.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly UserAccountRepository _repository;

        public UserAccountService(UnitOfWork unitOfWork, UserAccountRepository accountRepository)
        {
            _unitOfWork = unitOfWork;
            _repository = accountRepository;
        }

        public async Task<UserAccount> Authenticate(string email, string password)
        {
            return await _repository.GetUserAccount(email, password);
        }
    }
}
