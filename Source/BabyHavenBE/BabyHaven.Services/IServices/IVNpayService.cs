using BabyHaven.Common;
using BabyHaven.Services.Base;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BabyHaven.Services.IServices
{
    public interface IVNPayService
    {
        Task<IServiceResult> CreatePaymentUrl(Guid transactionId, string ipAddress);
        Task<IServiceResult> ValidateResponse(IQueryCollection parans);
    }
}
