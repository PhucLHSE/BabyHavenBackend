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
    }
}
