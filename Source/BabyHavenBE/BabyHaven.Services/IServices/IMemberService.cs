using BabyHaven.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.IServices
{
    public interface IMemberService
    {
        Task<IServiceResult> GetAll();
        Task<IServiceResult> GetById(Guid MemberId);
    }
}
