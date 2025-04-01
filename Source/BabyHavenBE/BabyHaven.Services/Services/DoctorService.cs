
using BabyHaven.Common;
using BabyHaven.Common.DTOs.DoctorDTOs;
using BabyHaven.Common.DTOs.DoctorSpecializationDTOs;
using BabyHaven.Repositories;
using BabyHaven.Repositories.Models;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.Services
{
    public class DoctorService : IDoctorService
    {

        private UnitOfWork _unitOfWork;

        public DoctorService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IServiceResult> GetAll()
        {

            var doctors = await _unitOfWork.DoctorRepository
                .GetAllWithUsersAsync();

            if (doctors == null || !doctors.Any())
            {

                return new ServiceResult(Const.WARNING_NO_DATA_CODE, 
                    Const.WARNING_NO_DATA_MSG,
                    new List<DoctorViewAllDto>());
            }
            else
            {

                var doctorDtos = doctors
                    .Select(doctor => doctor.MapToDoctorViewAllDto())
                    .ToList();

                return new ServiceResult(Const.SUCCESS_READ_CODE, 
                    Const.SUCCESS_READ_MSG,
                    doctors);
            }
        }

        public async Task<IQueryable<DoctorViewAllDto>> GetQueryable()
        {

            var doctors = await _unitOfWork.DoctorRepository
                .GetAllDoctorAsync();

            return doctors
                .Select(doctors => doctors.MapToDoctorViewAllDto())
                .AsQueryable();
        }

        public async Task<IServiceResult> GetById(int DoctorId)
        {

            var doctor = await _unitOfWork.DoctorRepository
                .GetByIdWithUsersAsync(DoctorId);

            if (doctor == null)
            {

                return new ServiceResult(Const.WARNING_NO_DATA_CODE, 
                    Const.WARNING_NO_DATA_MSG,
                    new DoctorViewDetailsDto());
            }
            else
            {

                var doctorDto = doctor.MapToDoctorViewDetailsDto();

                return new ServiceResult(Const.SUCCESS_READ_CODE, 
                    Const.SUCCESS_READ_MSG,
                    doctorDto);
            }
        }

        public async Task<IServiceResult> GetByUserId(Guid userId)
        {

            var doctor = await _unitOfWork.DoctorRepository
                .GetByUserId(userId);

            if (doctor == null)
            {

                return new ServiceResult(Const.WARNING_NO_DATA_CODE,
                    Const.WARNING_NO_DATA_MSG,
                    new DoctorViewDetailsDto());
            }
            else
            {

                var doctorDto = doctor.MapToDoctorViewDetailsDto();

                return new ServiceResult(Const.SUCCESS_READ_CODE,
                    Const.SUCCESS_READ_MSG,
                    doctorDto);
            }
        }

        public async Task<IServiceResult> Update(DoctorUpdateDto doctorDto)
        {
            try
            {

                // Check if the package exists in the database
                var doctor = await _unitOfWork.DoctorRepository
                    .GetByIdAsync(doctorDto.DoctorId);

                if (doctor == null)
                {

                    return new ServiceResult(Const.FAIL_UPDATE_CODE, 
                        "Doctor not found.");
                }

                //Map DTO to Entity
                doctorDto.MapToDoctorUpdateDto(doctor);

                // Update time information
                doctor.UpdatedAt = DateTime.UtcNow;

                // Save data to database
                var result = await _unitOfWork.DoctorRepository
                    .UpdateAsync(doctor);

                if (result > 0)
                {

                    return new ServiceResult(Const.SUCCESS_UPDATE_CODE, 
                        Const.SUCCESS_UPDATE_MSG,
                        doctor);
                }
                else
                {

                    return new ServiceResult(Const.FAIL_UPDATE_CODE, 
                        Const.FAIL_UPDATE_MSG);
                }
            }
            catch (Exception ex)
            {

                return new ServiceResult(Const.ERROR_EXCEPTION, 
                    ex.ToString());
            }
        }

        public async Task<IServiceResult> DeleteById(int DoctorId)
        {
            try
            {

                // Retrieve the Doctor using the provided DoctorId 
                var doctor = await _unitOfWork.DoctorRepository
                    .GetByIdAsync(DoctorId);

                // Check if the PackageFeature exists
                if (doctor == null)
                {

                    return new ServiceResult(Const.WARNING_NO_DATA_CODE, 
                        Const.WARNING_NO_DATA_MSG,
                        new DoctorDeleteDto());
                }
                else
                {

                    // Map to DoctorDeleteDto for response
                    var deleteDoctorDto = doctor.MapToDoctorDeleteDto();

                    var result = await _unitOfWork.DoctorRepository
                        .RemoveAsync(doctor);

                    if (result)
                    {

                        return new ServiceResult(Const.SUCCESS_DELETE_CODE,
                            Const.SUCCESS_DELETE_MSG,
                            deleteDoctorDto);
                    }
                    else
                    {

                        return new ServiceResult(Const.FAIL_DELETE_CODE, 
                            Const.FAIL_DELETE_MSG,
                            deleteDoctorDto);
                    }
                }
            }
            catch (Exception ex)
            {

                return new ServiceResult(Const.ERROR_EXCEPTION, 
                    ex.ToString());
            }
        }
        public async Task<IServiceResult> Create(DoctorCreateDto doctorDto)
        {
            try
            {

                if (doctorDto == null)
                    return new ServiceResult { Status = Const.FAIL_CREATE_CODE, 
                        Message = Const.FAIL_CREATE_MSG };

                // Check for duplicate email
                var existingDoctorByEmail = await _unitOfWork.DoctorRepository
                    .GetByEmailAsync(doctorDto.Email);

                if (existingDoctorByEmail != null)
                {

                    return new ServiceResult { Status = Const.FAIL_CREATE_CODE, 
                        Message = "A doctor with this email already exists." };
                }

                // Retrieve specializations
                var specializations = await _unitOfWork.SpecializationRepository
                    .GetByIds(doctorDto.SpecializationIds);

                // Map DTO to Entity
                var newDoctor = doctorDto.MapToDoctor();

                // Add doctor to database
                await _unitOfWork.DoctorRepository
                    .CreateAsync(newDoctor);

                // Create DoctorSpecialization entries using the mapper
                foreach (var specialization in specializations)
                {

                    var doctorSpecialization = new DoctorSpecializationCreateDto()
                        .MapToDoctorSpecialization(newDoctor.DoctorId, specialization.SpecializationId);

                    await _unitOfWork.DoctorSpecializationRepository
                        .CreateAsync(doctorSpecialization);
                }

                // Return result
                return new ServiceResult { Status = Const.SUCCESS_CREATE_CODE, 
                    Message = Const.SUCCESS_CREATE_MSG,
                    Data = newDoctor.MapToDoctorViewDetailsDto() };
            }
            catch (Exception ex)
            {

                return new ServiceResult { Status = Const.ERROR_EXCEPTION, 
                    Message = $"An error occurred while creating the doctor: {ex.InnerException.Message}" };
            }
        }

    }
}

