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

        public async Task<IServiceResult> GetById(int DoctorSpecializationId)
        {
            var doctorSpecialization = await _unitOfWork.DoctorSpecializationRepository.GetByIdDoctorSpecializationAsync(DoctorSpecializationId);

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
                // Check if DoctorSpecialization exists
                var doctorSpecialization = await _unitOfWork.DoctorSpecializationRepository
                    .GetByIdAsync(doctorSpecializationUpdateDto.DoctorSpecializationId);

                if (doctorSpecialization == null)
                {
                    return new ServiceResult(Const.FAIL_UPDATE_CODE, "DoctorSpecialization not found.");
                }

                // Call the correct extension method
                doctorSpecialization.MapToUpdatedDoctorSpecialization(doctorSpecializationUpdateDto);

                // Update modification timestamp
                doctorSpecialization.UpdatedAt = DateTime.UtcNow;

                // Save changes to the database
                var result = await _unitOfWork.DoctorSpecializationRepository.UpdateAsync(doctorSpecialization);

                if (result > 0)
                {
                    return new ServiceResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, doctorSpecialization);
                }
                else
                {
                    return new ServiceResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }



        public async Task<IServiceResult> DeleteById(int DoctorSpecializationId)
        {
            try
            {
                // Retrieve the DoctorSpecialization using the provided SpecializationId and DoctorId
                var doctorSpecialization = await _unitOfWork.DoctorSpecializationRepository.GetByIdDoctorSpecializationAsync( DoctorSpecializationId);

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
