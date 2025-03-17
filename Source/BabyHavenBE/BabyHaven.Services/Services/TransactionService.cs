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

        public TransactionService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork
                ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<IServiceResult> GetAll()
        {

            var transactions = await _unitOfWork.TransactionRepository
            .GetAllTransactionAsync();

            if (transactions == null || !transactions.Any())
            {

                return new ServiceResult(Const.WARNING_NO_DATA_CODE, 
                    Const.WARNING_NO_DATA_MSG,
                    new List<TransactionViewAllDto>());
            }
            else
            {

                var transactionDtos = transactions
                    .Select(transactions => transactions.MapToTransactionViewAllDto())
                    .ToList();

                return new ServiceResult(Const.SUCCESS_READ_CODE, 
                    Const.SUCCESS_READ_MSG,
                    transactionDtos);
            }
        }

        public async Task<IQueryable<TransactionViewAllDto>> GetQueryable()
        {

            var transactions = await _unitOfWork.TransactionRepository
                .GetAllTransactionAsync();

            return transactions
                .Select(transactions => transactions.MapToTransactionViewAllDto())
                .AsQueryable();
        }

        public async Task<IServiceResult> GetById(Guid TransactionId)
        {

            var transaction = await _unitOfWork.TransactionRepository
                .GetByIdTransactionAsync(TransactionId);

            if (transaction == null)
            {

                return new ServiceResult(Const.WARNING_NO_DATA_CODE, 
                    Const.WARNING_NO_DATA_MSG,
                    new TransactionViewDetailsDto());
            }
            else
            {

                var transactionDto = transaction.MapToTransactionViewDetailsDto();

                return new ServiceResult(Const.SUCCESS_READ_CODE, 
                    Const.SUCCESS_READ_MSG,
                    transactionDto);
            }
        }

        public async Task<IServiceResult> GetByUserId(Guid userId)
        {

            var transaction = await _unitOfWork.TransactionRepository
                .GetByUserId(userId);

            if (transaction == null)
            {

                return new ServiceResult(Const.WARNING_NO_DATA_CODE, 
                    Const.WARNING_NO_DATA_MSG,
                    new TransactionViewDetailsDto());
            }
            else
            {

                var transactionDto = transaction
                    .Select(t => t.MapToTransactionViewAllDto())
                    .ToList();

                return new ServiceResult(Const.SUCCESS_READ_CODE, 
                    Const.SUCCESS_READ_MSG,
                    transactionDto);
            }
        }

        public async Task<IServiceResult> Create(TransactionCreateDto transactionDto)
        {
            try
            {

                // Map DTO to Entity
                var newTransaction = transactionDto.MapToTransactionCreateDto();

                // Save data to database
                var result = await _unitOfWork.TransactionRepository
                    .CreateAsync(newTransaction);

                if (result > 0)
                {

                    // Retrieve user details from UserAccountRepository
                    var user = await _unitOfWork.UserAccountRepository
                        .GetByIdAsync(newTransaction.UserId);

                    var memberMembership = await _unitOfWork.MemberMembershipRepository
                        .GetByIdMemberMembershipAsync(newTransaction.MemberMembershipId);

                    if(memberMembership == null)
                    {

                        return new ServiceResult(Const.FAIL_READ_CODE, 
                            "Membership plan not found");
                    }

                    // Assign retrieved user details to navigation properties
                    newTransaction.User = user;
                    newTransaction.MemberMembership = memberMembership;
                    newTransaction.Amount = memberMembership.Package.Price;
                    newTransaction.Description = memberMembership.Package.Description;

                    await _unitOfWork.TransactionRepository .UpdateAsync(newTransaction);

                    // Map the saved entity to a response DTO
                    var responseDto = newTransaction.MapToTransactionViewDetailsDto();

                    responseDto.FullName = user?.Name ?? "Unknown FullName User";
                    responseDto.PackageName = memberMembership?.Package.PackageName ?? "Unknown PackName";

                    return new ServiceResult(Const.SUCCESS_CREATE_CODE, 
                        Const.SUCCESS_CREATE_MSG,
                        responseDto);
                }
                else
                {

                    return new ServiceResult(Const.FAIL_CREATE_CODE, 
                        Const.FAIL_CREATE_MSG);
                }
            }
            catch (Exception ex)
            {

                return new ServiceResult(Const.ERROR_EXCEPTION, 
                    ex.ToString());
            }
        }

        public async Task<IServiceResult> DeleteById(Guid TransactionId)
        {
            try
            {

                var transaction = await _unitOfWork.TransactionRepository
                    .GetByIdTransactionAsync(TransactionId);

                if (transaction == null)
                {

                    return new ServiceResult(Const.WARNING_NO_DATA_CODE, 
                        Const.WARNING_NO_DATA_MSG,
                        new TransactionDeleteDto());
                }
                else
                {

                    var deleteTransactionDto = transaction.MapToTransactionDeleteDto();

                    var result = await _unitOfWork.TransactionRepository
                        .RemoveAsync(transaction);

                    if (result)
                    {

                        return new ServiceResult(Const.SUCCESS_DELETE_CODE, 
                            Const.SUCCESS_DELETE_MSG,
                            deleteTransactionDto);
                    }
                    else
                    {

                        return new ServiceResult(Const.FAIL_DELETE_CODE, 
                            Const.FAIL_DELETE_MSG,
                            deleteTransactionDto);
                    }
                }
            }
            catch (Exception ex)
            {

                return new ServiceResult(Const.ERROR_EXCEPTION, 
                    ex.ToString());
            }
        }

        public async Task<IServiceResult> GetByUserIdAndMemberMembership(Guid userId, Guid membershipId)
        {

            var transaction = await _unitOfWork.TransactionRepository
                .GetByUserIdAndMemberMembershipId(userId, membershipId);

            if (transaction == null)
            {

                return new ServiceResult(Const.WARNING_NO_DATA_CODE, 
                    Const.WARNING_NO_DATA_MSG,
                    new TransactionViewDetailsDto());
            }
            else
            {

                return new ServiceResult(Const.SUCCESS_READ_CODE, 
                    Const.SUCCESS_READ_MSG,
                    transaction.MapToTransactionViewDetailsDto());
            }
        }
    }
}
