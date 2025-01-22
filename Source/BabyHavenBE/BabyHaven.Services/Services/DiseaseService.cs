using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common;
using BabyHaven.Common.DTOs.DiseaseDTOs;
using BabyHaven.Common.DTOs.MembershipPackageDTOs;
using BabyHaven.Repositories;
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
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new MembershipPackageViewDetailsDto());
            }
            else
            {
                var diseaseDto = disease.MapToDiseaseViewDetailDto();

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, diseaseDto);
            }
        }

        public async Task<IServiceResult> Save(DiseaseCreateDto diseaseCreateDto)
        {
            try
            {
                int result = -1;

                // Map DTO to Entity
                var diseaseDto = diseaseCreateDto.MapToDiseaseCreateDto();

                // Check if the package exists in the database
                var diseaseTmp = await _unitOfWork.DiseaseRepository.GetByIdAsync(diseaseDto.DiseaseId);

                if (diseaseTmp != null)
                {
                    // Update current fields directly
                    

                    result = await _unitOfWork.DiseaseRepository.UpdateAsync(diseaseTmp);

                    if (result > 0)
                    {
                        return new ServiceResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, diseaseTmp);
                    }
                    else
                    {
                        return new ServiceResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                    }
                }
                else
                {
                    result = await _unitOfWork.DiseaseRepository.CreateAsync(diseaseDto);

                    if (result > 0)
                    {
                        return new ServiceResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, diseaseDto);
                    }
                    else
                    {
                        return new ServiceResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, diseaseDto);
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
