using BabyHaven.Common;
using BabyHaven.Repositories;
using BabyHaven.Services.Base;
using BabyHaven.Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common.DTOs.TransactionDTOs;
using BabyHaven.Services.IServices;
namespace BabyHaven.Services.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly UnitOfWork _unitOfWork;

        public TransactionService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IServiceResult> GetAll()
        {
            var transactions = await _unitOfWork.TransactionRepository
            .GetAllTransactionAsync();

            if (transactions == null || !transactions.Any())
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG,
                    new List<TransactionViewAllDto>());
            }
            else
            {
                var transactionDtos = transactions
                    .Select(transactions => transactions.MapToTransactionViewAllDto())
                    .ToList();

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG,
                    transactionDtos);
            }
        }

        public async Task<IServiceResult> GetById(Guid TransactionId)
        {
            var transaction = await _unitOfWork.TransactionRepository
                .GetByIdTransactionAsync(TransactionId);

            if (transaction == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG,
                    new TransactionViewDetailsDto());
            }
            else
            {
                var transactionDto = transaction.MapToTransactionViewDetailsDto();

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG,
                    transactionDto);
            }
        }
    }
}
