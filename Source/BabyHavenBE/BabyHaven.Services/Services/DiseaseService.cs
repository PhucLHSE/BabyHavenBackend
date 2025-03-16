using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common;
using BabyHaven.Common.DTOs.DiseaseDTOs;
using BabyHaven.Common.DTOs.MembershipPackageDTOs;
using BabyHaven.Repositories;
using BabyHaven.Repositories.Models;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Mappers;

namespace BabyHaven.Services.Services
{
    public class DiseaseService : IDiseaseService
    {
        private readonly UnitOfWork _unitOfWork;
        public DiseaseService() {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IServiceResult> Create(DiseaseCreateDto diseaseCreateDto)
        {
            try
            {
                var disease = diseaseCreateDto.MapToDiseaseCreateEntity(); // Map DTO to entity
                var result = await _unitOfWork.DiseaseRepository.CreateAsync(disease);

                if (result > 0)
                {
                    return new ServiceResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, disease);
                }
                else
                {
                    return new ServiceResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IServiceResult> DeleteById(int DiseaseId)
        {
            var disease = await _unitOfWork.DiseaseRepository.GetByIdAsync(DiseaseId);

            if (disease == null || !disease.IsActive)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            else
            {
                var result = await _unitOfWork.DiseaseRepository.RemoveAsync(disease);
                if (result == true)
                {
                    return new ServiceResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
                }
                else
                {
                    return new ServiceResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
                }
            }
        }

        public async Task<IServiceResult> GetAll()
        {
            var diseases = await _unitOfWork.DiseaseRepository.GetAllAsync();

            if (diseases == null || !diseases.Any())
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<DiseaseViewAllDto>());
            }
            else
            {
                var diseaseDtos = diseases
                    .Select(disease => disease.MapToDiseaseViewAllDto())
                    .ToList();
                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, diseaseDtos);
            }
        }

        public async Task<IServiceResult> GetById(int DiseaseId)
        {
            var disease = await _unitOfWork.DiseaseRepository.GetByIdAsync(DiseaseId);

            if (disease == null)
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new DiseaseViewAllDto());
            }
            else
            {
                var diseaseDto = disease.MapToDiseaseViewDetailDto();

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, diseaseDto);
            }
        }

        public async Task<IServiceResult> PreDeleteById(int diseaseId)
        {
            try
            {
                // Check if the disease exists in the database
                var disease = await _unitOfWork.DiseaseRepository.GetByIdAsync(diseaseId);
                if (disease == null)
                {
                    // Return a warning if the disease does not exist
                    return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }

                // Mark the disease as "inactive" (soft delete)
                disease.IsActive = false;

                // Save the updated status to the database
                var result = await _unitOfWork.DiseaseRepository.UpdateAsync(disease);
                if (result > 0)
                {
                    // Return success response along with the updated disease object
                    return new ServiceResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, disease);
                }
                else
                {
                    // Return failure response if the update was not successful
                    return new ServiceResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
                }
            }
            catch (Exception ex)
            {
                // Handle any exception and return an error response
                return new ServiceResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IServiceResult> RecoverById(int diseaseId)
        {
            try
            {
                // Check if the disease exists in the database
                var disease = await _unitOfWork.DiseaseRepository.GetByIdAsync(diseaseId);
                if (disease == null)
                {
                    // Return a warning if the disease does not exist
                    return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }

                // Mark the disease as "active" (recovery)
                disease.IsActive = true;

                // Save the updated status to the database
                var result = await _unitOfWork.DiseaseRepository.UpdateAsync(disease);
                if (result > 0)
                {
                    // Return success response along with the updated disease object
                    return new ServiceResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, disease);
                }
                else
                {
                    // Return failure response if the update was not successful
                    return new ServiceResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                }
            }
            catch (Exception ex)
            {
                // Handle any exception and return an error response
                return new ServiceResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IServiceResult> UpdateById(int DiseaseId, DiseaseUpdateDto diseaseUpdateDto)
        {
            try
            {
                var disease = await _unitOfWork.DiseaseRepository.GetByIdAsync(DiseaseId);

                if (disease == null)
                {
                    return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }

                disease.IsActive = false;

                var result = await _unitOfWork.DiseaseRepository.UpdateAsync(disease);
                if (result > 0)
                {
                    return new ServiceResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, disease);
                }
                else
                {
                    return new ServiceResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IQueryable<DiseaseViewAllDto>> GetQueryable()
        {

            var diseases = await _unitOfWork.DiseaseRepository
                .GetAllAsync();

            return diseases
                .Select(diseases => diseases.MapToDiseaseViewAllDto())
                .AsQueryable();
        }
    }
}
