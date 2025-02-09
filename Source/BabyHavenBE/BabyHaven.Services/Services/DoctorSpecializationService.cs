using BabyHaven.Common;
using BabyHaven.Common.DTOs.DoctorDTOs;
using BabyHaven.Common.DTOs.DoctorSpecializationDTOs;
using BabyHaven.Repositories;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.Services
{
    public class DoctorSpecializationService:IDoctorSpecializationService
    {
        private readonly UnitOfWork _unitOfWork;

        public DoctorSpecializationService()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IServiceResult> GetAll()
        {
            var doctorSpecializations = await _unitOfWork.DoctorSpecializationRepository.GetAllDoctorSpecializationAsync();

            if (doctorSpecializations == null || !doctorSpecializations.Any())
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG,
                    new List<DoctorSpecializationViewAllDto>());
            }
            else
            {
                var doctorSpecializationDtos = doctorSpecializations
                    .Select(doctorSpecialization => doctorSpecialization.MapToDoctorSpecializationViewAllDto())
                    .ToList();

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG,
                    doctorSpecializationDtos);
            }
        }

        public async Task<IServiceResult> GetById(int DoctorId, int SpecializationId)
        {
            var doctorSpecialization = await _unitOfWork.DoctorSpecializationRepository.GetByIdDoctorSpecializationAsync(DoctorId, SpecializationId);

            if (doctorSpecialization == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG,
                    new DoctorViewDetailsDto());
            }
            else
            {
                var doctorSpecializationDto = doctorSpecialization.MapToDoctorSpecializationViewDetailsDto();

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG,
                    doctorSpecializationDto);
            }
        }

        public async Task<IServiceResult> Create(DoctorSpecializationCreateDto doctorSpecializationCreateDto)
        {
            try
            {
                // Retrieve mappings: SpecializationName -> SpecializationId and DoctorName -> DoctorId
                var specializationNameToIdMapping = await _unitOfWork.SpecializationRepository.GetAllSpecializationNameToIdMappingAsync();
                var doctorNameToIdMapping = await _unitOfWork.DoctorRepository.GetAllDoctorNameToIdMappingAsync();

                // Check if the provided SpecializationName exists
                if (!specializationNameToIdMapping.ContainsKey(doctorSpecializationCreateDto.SpecializationName))
                {
                    return new ServiceResult(Const.FAIL_CREATE_CODE,
                        $"SpecializationName '{doctorSpecializationCreateDto.SpecializationName}' does not exist.");
                }

                // Check if the provided DoctorName exists
                if (!doctorNameToIdMapping.ContainsKey(doctorSpecializationCreateDto.DoctorName))
                {
                    return new ServiceResult(Const.FAIL_CREATE_CODE,
                        $"DoctorName '{doctorSpecializationCreateDto.DoctorName}' does not exist.");
                }

                // Get SpecializationId and DoctorId from PackageName and FeatureName
                var specializationId = specializationNameToIdMapping[doctorSpecializationCreateDto.SpecializationName];
                var doctorId = doctorNameToIdMapping[doctorSpecializationCreateDto.DoctorName];

                // Check if the DoctorSpecialization already exists in the database
                var existingDoctorSpecialization = await _unitOfWork.DoctorSpecializationRepository
                    .GetByIdDoctorSpecializationAsync(doctorId, specializationId);

                if (existingDoctorSpecialization != null && existingDoctorSpecialization.SpecializationId > 0)
                {
                    return new ServiceResult(Const.FAIL_CREATE_CODE,
                        "The specified DoctorSpecialization already exists.");
                }

                // Map the DTO to an entity object
                var newDoctorSpecialization = doctorSpecializationCreateDto.MapToDoctorSpecialization(specializationId, doctorId);

                // Add timestamp for creation
                newDoctorSpecialization.CreatedAt = DateTime.UtcNow;

                // Save the new entity to the database
                var result = await _unitOfWork.DoctorSpecializationRepository.CreateAsync(newDoctorSpecialization);

                if (result > 0)
                {
                    var responseDto = new DoctorSpecializationCreateDto
                    {
                        DoctorName = doctorSpecializationCreateDto.DoctorName,
                        SpecializationName = doctorSpecializationCreateDto.SpecializationName,
                        Status = doctorSpecializationCreateDto.Status
                    };

                    return new ServiceResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG,
                        responseDto);
                }
                else
                {
                    return new ServiceResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IServiceResult> Update(DoctorSpecializationUpdateDto doctorSpecializationUpdateDto)
        {
            try
            {
                // // Retrieve mappings: SpecializationName -> SpecializationId and DoctorName -> DoctorId
                var specializationNameToIdMapping = await _unitOfWork.SpecializationRepository.GetAllSpecializationNameToIdMappingAsync();
                var doctorNameToIdMapping = await _unitOfWork.DoctorRepository.GetAllDoctorNameToIdMappingAsync();

                // Check if the provided DoctorName exists
                if (!doctorNameToIdMapping.ContainsKey(doctorSpecializationUpdateDto.DoctorName))
                    return new ServiceResult(Const.FAIL_UPDATE_CODE,
                        $"DoctorName '{doctorSpecializationUpdateDto.DoctorName}' does not exist.");

                // Check if the provided SpecializationName exists
                if (!specializationNameToIdMapping.ContainsKey(doctorSpecializationUpdateDto.SpecializationName))
                    return new ServiceResult(Const.FAIL_UPDATE_CODE,
                        $"SpecializationName '{doctorSpecializationUpdateDto.SpecializationName}' does not exist.");

                // Get SpecializationId and DoctorId from SpecializationName and FeatureName
                int specializationId = specializationNameToIdMapping[doctorSpecializationUpdateDto.SpecializationName];
                int doctorId = doctorNameToIdMapping[doctorSpecializationUpdateDto.DoctorName];

                //  Check if the DoctorSpecialization already exists in the database
                var existingDoctorSpecialization = await _unitOfWork.DoctorSpecializationRepository.
                    GetByIdDoctorSpecializationAsync(specializationId, doctorId);

                if (existingDoctorSpecialization == null)
                    return new ServiceResult(Const.FAIL_UPDATE_CODE,
                        "The specified DoctorSpecialization does not exist.");

                // Map the update data
                existingDoctorSpecialization.MapToUpdatedDoctorSpecialization(doctorSpecializationUpdateDto);

                // Update time information
                existingDoctorSpecialization.UpdatedAt = DateTime.Now;

                // Save the new entity to the database
                var result = await _unitOfWork.DoctorSpecializationRepository.UpdateAsync(existingDoctorSpecialization);

                if (result > 0)
                {
                    return new ServiceResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG,
                        doctorSpecializationUpdateDto);
                }
                else
                {
                    return new ServiceResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG,
                        doctorSpecializationUpdateDto);
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IServiceResult> DeleteById(int SpecializationId, int DoctorId)
        {
            try
            {
                // Retrieve the DoctorSpecialization using the provided SpecializationId and DoctorId
                var doctorSpecialization = await _unitOfWork.DoctorSpecializationRepository.GetByIdDoctorSpecializationAsync(SpecializationId, DoctorId);

                // Check if the DoctorSpecialization exists
                if (doctorSpecialization == null)
                {
                    return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG,
                        new DoctorSpecializationDeleteDto());
                }
                else
                {
                    // Map to DoctorSpecializationDeleteDto for response
                    var deleteDoctorSpecializationDto = doctorSpecialization.MapToDoctorSpecializationDeleteDto();

                    var result = await _unitOfWork.DoctorSpecializationRepository.RemoveAsync(doctorSpecialization);

                    if (result)
                    {
                        return new ServiceResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG,
                            deleteDoctorSpecializationDto);
                    }
                    else
                    {
                        return new ServiceResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG,
                            deleteDoctorSpecializationDto);
                    }
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }
    }
}
