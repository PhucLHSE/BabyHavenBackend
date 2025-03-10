using BabyHaven.Common.DTOs.DoctorDTOs;
using BabyHaven.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.IServices
{
    public interface IDoctorService
    {
        Task<IServiceResult> GetAll();
        Task<IServiceResult> GetById(int DoctorId);
        Task<IServiceResult> Create(DoctorCreateDto doctorDto,Guid userId);

        Task<IServiceResult> Update(DoctorUpdateDto doctorUpdateDto);
        Task<IServiceResult> DeleteById(int DoctorId);
    }
}
