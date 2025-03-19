using BabyHaven.Common.DTOs.TransactionDTOs;
using BabyHaven.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.IServices
{
    public interface ITransactionService
    {
        Task<IServiceResult> GetAll();

        Task<IQueryable<TransactionViewAllDto>> GetQueryable();

        Task<IServiceResult> GetById(Guid TransactionId);

        Task<IServiceResult> GetByUserId(Guid userId);

        Task<IServiceResult> GetByUserIdAndMemberMembership(Guid userId, Guid membershipId);

        Task<IServiceResult> Create(TransactionCreateDto transactionCreateDto);

        Task<IServiceResult> DeleteById(Guid TransactionId);
    }
}
