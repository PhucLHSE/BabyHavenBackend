using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common;
using BabyHaven.Repositories;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;

namespace BabyHaven.Services.Services
{
    public class BmiPercentileService : IBmiPercentileService
    {
        private readonly UnitOfWork _unitOfWork;

        public BmiPercentileService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork
                ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<IServiceResult> GetAll()
        {
            var BmiData = await _unitOfWork.BmiPercentileRepository.GetAllAsync();

            if (BmiData == null)
            {
                return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG);
            }

            return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, BmiData);
        }

        public async Task<IServiceResult> GetByAgeAndGender(int age, string gender)
        {
            var BmiData = await _unitOfWork.BmiPercentileRepository.GetByAgeAndGender(age, gender);

            if (BmiData == null)
            {
                return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG);
            }

            return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, BmiData);
        }
    }
}
