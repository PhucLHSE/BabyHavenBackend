﻿using BabyHaven.Common.Enum.TransactionEnums;
using BabyHaven.Repositories.Base;
using BabyHaven.Repositories.DBContext;
using BabyHaven.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Repositories.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>
    {
        public TransactionRepository()
        {

        }

        public TransactionRepository(SWP391_ChildGrowthTrackingSystemContext context)
            => _context = context;

        public async Task<List<Transaction>> GetAllTransactionAsync()
        {
            var transactions = await _context.Transactions
                .Include(mm => mm.User)
                .Include(mm => mm.MemberMembership)
                    .ThenInclude(mm => mm.Package)  // Include Package from MemberMembership
                .ToListAsync();

            return transactions;
        }

        public async Task<Transaction?> GetByIdTransactionAsync(Guid transactionId)
        {
            return await _context.Transactions
                .Include(t => t.User)
                .Include(t => t.MemberMembership)
                    .ThenInclude(t => t.Package)   // Include Package from MemberMembership
                .FirstOrDefaultAsync(t => t.TransactionId == transactionId);
        }

        public async Task<Transaction?> GetByUserIdAndMemberMembershipId(Guid userId, Guid membershipId)
        {
            return await _context.Transactions
                .Include(t => t.User)
                .Include(t => t.MemberMembership)
                    .ThenInclude(t => t.Package)   // Include Package from MemberMembership
                .FirstOrDefaultAsync(t => t.UserId == userId && t.MemberMembershipId == membershipId && t.PaymentStatus.Equals(TransactionStatus.Pending.ToString()));
        }

        public async Task<Transaction?> GetByGatewayTransactionIdAsync(long gatewayTransactionId)
        {
            return await _context.Transactions
                .Include(t => t.User)
                .Include(t => t.MemberMembership)
                    .ThenInclude(t => t.Package)   // Include Package from MemberMembership
                .FirstOrDefaultAsync(t => t.GatewayTransactionId == gatewayTransactionId);
        }

        public async Task<List<Transaction>?> GetByUserId(Guid userId)
        {
            return await _context.Transactions
                .Include(p => p.MemberMembership)
                    .ThenInclude(p => p.Package)
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }
    }
}
