using BabyHaven.Common;
using BabyHaven.Common.DTOs.FeatureDTOs;
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
    public class FeatureService : IFeatureService
    {
        private readonly UnitOfWork _unitOfWork;

        public FeatureService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IServiceResult> GetAll()
        {
            var features = await _unitOfWork.FeatureRepository.GetAllAsync();

            if (features == null && !features.Any())
            {
                return new ServiceResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, 
                    new List<FeatureViewAllDto>());
            }
            else
            {
                var featureDtos = features
                    .Select(features => features.MapToFeatureViewAllDto())
                    .ToList();

                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, 
                    featureDtos);
            }
        }
    }
}
