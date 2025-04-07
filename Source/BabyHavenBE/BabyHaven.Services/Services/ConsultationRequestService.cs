using BabyHaven.Common;
using BabyHaven.Repositories;
using BabyHaven.Services.Base;
using BabyHaven.Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common.DTOs.ConsultationRequestDTOs;
using BabyHaven.Services.IServices;
using BabyHaven.Common.DTOs.TransactionDTOs;
using Azure.Core;

namespace BabyHaven.Services.Services
{
    public class ConsultationRequestService : IConsultationRequestService
    {
        private readonly UnitOfWork _unitOfWork;

        public ConsultationRequestService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork 
                ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<IServiceResult> GetAll()
        {

            var consultationRequests = await _unitOfWork.ConsultationRequestRepository
                .GetAllConsultationRequestAsync();

            if (consultationRequests == null || !consultationRequests.Any())
            {

                return new ServiceResult(Const.WARNING_NO_DATA_CODE, 
                    Const.WARNING_NO_DATA_MSG,
                    new List<ConsultationRequestViewAllDto>());
            }
            else
            {

                var consultationRequestDtos = consultationRequests
                    .Select(consultationRequests => consultationRequests.MapToConsultationRequestViewAllDto())
                    .ToList();

                return new ServiceResult(Const.SUCCESS_READ_CODE, 
                    Const.SUCCESS_READ_MSG,
                    consultationRequestDtos);
            }
        }

        public async Task<IQueryable<ConsultationRequestViewAllDto>> GetQueryable()
        {

            var consultationRequests = await _unitOfWork.ConsultationRequestRepository
                .GetAllConsultationRequestAsync();

            return consultationRequests
                .Select(consultationRequests => consultationRequests.MapToConsultationRequestViewAllDto())
                .AsQueryable();
        }

        public async Task<IServiceResult> GetById(int RequestId)
        {

            var consultationRequest = await _unitOfWork.ConsultationRequestRepository
                .GetByIdConsultationRequestAsync(RequestId);

            if (consultationRequest == null)
            {

                return new ServiceResult(Const.WARNING_NO_DATA_CODE, 
                    Const.WARNING_NO_DATA_MSG,
                    new ConsultationRequestViewDetailsDto());
            }
            else
            {

                var consultationRequestDto = consultationRequest.MapToConsultationRequestViewDetailsDto();

                return new ServiceResult(Const.SUCCESS_READ_CODE,
                    Const.SUCCESS_READ_MSG,
                    consultationRequestDto);
            }
        }

        public async Task<IServiceResult> Create(ConsultationRequestCreateDto consultationRequestDto)
        {
            try
            {

                // Retrieve mappings: MemberName -> MemberId and ChildName -> ChildId
                var child = await _unitOfWork.ChildrenRepository.GetChildByNameAndDateOfBirthAsync(consultationRequestDto.ChildName, DateOnly.Parse(consultationRequestDto.ChildBirth), consultationRequestDto.MemberId);

                var doctor = await _unitOfWork.DoctorRepository.GetByIdAsync(consultationRequestDto.DoctorId);

                if (child == null)
                {

                    return new ServiceResult(Const.FAIL_CREATE_CODE, 
                        "Child not found");
                }

                if (doctor == null)
                {

                    return new ServiceResult(Const.FAIL_CREATE_CODE, 
                        "Doctor not found");
                }

                // Map the DTO to an entity object
                var newConsultationRequest = consultationRequestDto.MapToConsultationRequest(doctor.DoctorId, child);

                // Save the new entity to the database
                var result = await _unitOfWork.ConsultationRequestRepository
                    .CreateAsync(newConsultationRequest);

                if (result > 0)
                { 

                    return new ServiceResult(Const.SUCCESS_CREATE_CODE, 
                        Const.SUCCESS_CREATE_MSG);
                }
                else
                {

                    return new ServiceResult(Const.FAIL_CREATE_CODE, 
                        Const.FAIL_CREATE_MSG);
                }
            }
            catch (Exception ex)
            {

                return new ServiceResult(Const.ERROR_EXCEPTION, 
                    ex.ToString());
            }
        }

        public async Task<IServiceResult> UpdateStatus(int requestId, string status)
        {
            try
            {
                var consultationRequest = await _unitOfWork.ConsultationRequestRepository
                                    .GetByRequestId(requestId);
                if (consultationRequest == null)
                {
                    return new ServiceResult(Const.WARNING_NO_DATA_CODE,
                        Const.WARNING_NO_DATA_MSG);
                }

                if (status.Equals("Completed"))
                {
                    var pendingRequests = await _unitOfWork.ConsultationRequestRepository
                        .GetAllConsultationRequestByMemberId(consultationRequest.MemberId, consultationRequest.ChildId);

                    foreach (var request in pendingRequests)
                    {
                        request.Status = "Completed";
                        await _unitOfWork.ConsultationRequestRepository.UpdateAsync(request);
                        
                    }
                    return new ServiceResult(Const.SUCCESS_UPDATE_CODE,
                            Const.SUCCESS_UPDATE_MSG);
                }

                consultationRequest.Status = status;

                var result = await _unitOfWork.ConsultationRequestRepository
                    .UpdateAsync(consultationRequest);

                return new ServiceResult(Const.SUCCESS_UPDATE_CODE,
                    Const.SUCCESS_UPDATE_MSG,
                    consultationRequest);

            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION,
                    ex.InnerException.ToString());
            }
        }

        public async Task<IServiceResult> DeleteById(int RequestId)
        {
            try
            {

                var consultationRequest = await _unitOfWork.ConsultationRequestRepository
                    .GetByIdConsultationRequestAsync(RequestId);

                if (consultationRequest == null)
                {

                    return new ServiceResult(Const.WARNING_NO_DATA_CODE, 
                        Const.WARNING_NO_DATA_MSG,
                        new ConsultationRequestDeleteDto());
                }
                else
                {

                    var deleteConsultationRequestDto = consultationRequest.MapToConsultationRequestDeleteDto();

                    var result = await _unitOfWork.ConsultationRequestRepository
                        .RemoveAsync(consultationRequest);

                    if (result)
                    {

                        return new ServiceResult(Const.SUCCESS_DELETE_CODE, 
                            Const.SUCCESS_DELETE_MSG,
                            deleteConsultationRequestDto);
                    }
                    else
                    {

                        return new ServiceResult(Const.FAIL_DELETE_CODE, 
                            Const.FAIL_DELETE_MSG,
                            deleteConsultationRequestDto);
                    }
                }
            }
            catch (Exception ex)
            {

                return new ServiceResult(Const.ERROR_EXCEPTION, 
                    ex.ToString());
            }
        }
    }
}
