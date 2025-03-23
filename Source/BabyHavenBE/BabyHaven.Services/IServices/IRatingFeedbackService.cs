using BabyHaven.Common.DTOs.ConsultationResponseDTOs;
using BabyHaven.Common.DTOs.RatingFeedbackDTOs;
using BabyHaven.Repositories.Models;
using BabyHaven.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.IServices
{
    public interface IRatingFeedbackService
    {
        Task<IServiceResult> GetAll();
        Task<IServiceResult> GetByid(int ratingId);
        Task<IServiceResult> Create(RatingFeedbackCreateDto dto);
        Task<IQueryable<RatingFeedbackViewAllDto>> GetQueryable();
    }
}
