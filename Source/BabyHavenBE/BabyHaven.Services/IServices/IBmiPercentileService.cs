using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common.DTOs.AlertDTOS;
using BabyHaven.Services.Base;

namespace BabyHaven.Services.IServices
{
    public interface IBmiPercentileService
    {
        Task<IServiceResult> GetAll();

        Task<IServiceResult> GetByAgeAndGender(int age, string gender);
    }
}
