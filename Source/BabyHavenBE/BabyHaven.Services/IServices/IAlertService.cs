using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common.DTOs.AlertDTOS;
using BabyHaven.Services.Base;

namespace BabyHaven.Services.IServices
{
    public interface IAlertService
    {
        Task<IServiceResult> GetAll();
        Task<IServiceResult> GetById(int alertId);
        Task<IServiceResult> GetByChild(string name, string dob, Guid memberId);
        Task<IServiceResult> Create(AlertCreateDto dto);
        Task<IServiceResult> Update(AlertUpdateDto dto);
        Task<IServiceResult> Delete(int alertId);
        Task<IServiceResult> CheckAndCreateAlert(string name, string dob, Guid id);
    }
}
