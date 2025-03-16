using BabyHaven.Common.DTOs.FeatureDTOs;
using BabyHaven.Common;
using BabyHaven.Repositories;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common.DTOs.SpecializationDTOs;

namespace BabyHaven.Services.Services
{
    public class SpecializationService : ISpecializationService
    {
        private readonly UnitOfWork _unitOfWork;

        public SpecializationService()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IServiceResult> GetAll()
        {

            var specializations = await _unitOfWork.SpecializationRepository
                .GetAllAsync();

            if (specializations == null || !specializations.Any())
            {

                return new ServiceResult(Const.
                    WARNING_NO_DATA_CODE,
                    Const.
                    WARNING_NO_DATA_MSG,
                    new List<SpecializationViewAllDto>());
            }
            else
            {

                var specializationDtos = specializations
                    .Select(specializations => specializations.MapToSpecializationViewAllDto())
                    .ToList();

                return new ServiceResult(Const.
                    SUCCESS_READ_CODE,
                    Const.
                    SUCCESS_READ_MSG,
                    specializationDtos);
            }
        }

        public async Task<IQueryable<SpecializationViewAllDto>> GetQueryable()
        {

            var specializations = await _unitOfWork.SpecializationRepository
                .GetAllAsync();

            return specializations
                .Select(specializations => specializations.MapToSpecializationViewAllDto())
                .AsQueryable();
        }

        public async Task<IServiceResult> GetById(int SpecializationId)
        {

            var specialization = await _unitOfWork.SpecializationRepository
                .GetByIdAsync(SpecializationId);

            if (specialization == null)
            {

                return new ServiceResult(Const.
                    WARNING_NO_DATA_CODE,
                    Const.
                    WARNING_NO_DATA_MSG,
                    new SpecializationViewDetailsDto());
            }
            else
            {

                var specializationDto = specialization.MapToSpecializationViewDetailsDto();

                return new ServiceResult(Const.
                    SUCCESS_READ_CODE,
                    Const.
                    SUCCESS_READ_MSG,
                    specializationDto);
            }
        }

        public async Task<IServiceResult> Create(SpecializationCreateDto specializationDto)
        {
            try
            {

                // Check if the specialization exists in the database
                var specialization = await _unitOfWork.SpecializationRepository
                    .GetBySpecializationNameAsync(specializationDto.SpecializationName);

                if (specialization != null)
                {

                    return new ServiceResult(Const.
                        FAIL_CREATE_CODE,
                        "Specialization with the same name already exists.");
                }

                // Map DTO to Entity
                var newSpecialization = specializationDto.MapToSpecializationCreateDto();

                // Add creation and update time information
                newSpecialization.CreatedAt = DateTime.UtcNow;
                newSpecialization.UpdatedAt = DateTime.UtcNow;

                // Save data to database
                var result = await _unitOfWork.SpecializationRepository
                    .CreateAsync(newSpecialization);

                if (result > 0)
                {

                    return new ServiceResult(Const.
                        SUCCESS_CREATE_CODE,
                        Const.
                        SUCCESS_CREATE_MSG,
                        newSpecialization);
                }
                else
                {

                    return new ServiceResult(Const.
                        FAIL_CREATE_CODE,
                        Const.
                        FAIL_CREATE_MSG);
                }
            }
            catch (Exception ex)
            {

                return new ServiceResult(Const.
                    ERROR_EXCEPTION,
                    ex.ToString());
            }
        }

        public async Task<IServiceResult> Update(SpecializationUpdateDto specializationDto)
        {
            try
            {

                // Check if the Specialization exists in the database
                var specialization = await _unitOfWork.SpecializationRepository
                    .GetByIdAsync(specializationDto.SpecializationId);

                if (specialization == null)
                {

                    return new ServiceResult(Const.
                        FAIL_UPDATE_CODE,
                        "Specialization not found.");
                }

                //Map DTO to Entity
                specializationDto.MapToSpecializationUpdateDto(specialization);

                // Update time information
                specialization.UpdatedAt = DateTime.UtcNow;

                // Save data to database
                var result = await _unitOfWork.SpecializationRepository
                    .UpdateAsync(specialization);

                if (result > 0)
                {
                    return new ServiceResult(Const.
                        SUCCESS_UPDATE_CODE,
                        Const.
                        SUCCESS_UPDATE_MSG,
                        specialization);
                }
                else
                {
                    return new ServiceResult(Const.
                        FAIL_UPDATE_CODE,
                        Const.
                        FAIL_UPDATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.
                    ERROR_EXCEPTION,
                    ex.ToString());
            }
        }

        public async Task<IServiceResult> DeleteById(int SpecializationId)
        {
            try
            {

                var specialization = await _unitOfWork.SpecializationRepository
                    .GetByIdAsync(SpecializationId);

                if (specialization == null)
                {

                    return new ServiceResult(Const.
                        WARNING_NO_DATA_CODE,
                        Const.
                        WARNING_NO_DATA_MSG,
                        new SpecializationDeleteDto());
                }
                else
                {

                    var deleteSpecializationDto = specialization.MapToSpecializationDeleteDto();

                    var result = await _unitOfWork.SpecializationRepository
                        .RemoveAsync(specialization);

                    if (result)
                    {

                        return new ServiceResult(Const.
                            SUCCESS_DELETE_CODE,
                            Const.
                            SUCCESS_DELETE_MSG,
                            deleteSpecializationDto);
                    }
                    else
                    {

                        return new ServiceResult(Const.
                            FAIL_DELETE_CODE,
                            Const.
                            FAIL_DELETE_MSG,
                            deleteSpecializationDto);
                    }
                }
            }
            catch (Exception ex)
            {

                return new ServiceResult(Const.
                    ERROR_EXCEPTION,
                    ex.ToString());
            }
        }
    }
}
