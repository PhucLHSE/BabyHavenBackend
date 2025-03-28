using BabyHaven.Common.DTOs.DoctorDTOs;
using BabyHaven.Common.Enum.DoctorEnums;
using BabyHaven.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BabyHaven.Services.Mappers
{
    public static class DoctorMapper
    {
        // Mapper DoctorViewAllDto
        public static DoctorViewAllDto MapToDoctorViewAllDto(this Doctor model)
        {
            return new DoctorViewAllDto
            {
                UserName = model.User?.Username ?? string.Empty,
                Name = model.Name,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Degree = model.Degree,
                HospitalName = model.HospitalName,
                HospitalAddress = model.HospitalAddress,
                Biography = model.Biography,
                DateOfBirth = model.User?.DateOfBirth,

                // Convert Status from string to enum
                Status = Enum.TryParse<DoctorStatus>(model.Status, true, out var status)
                          ? status
                          : DoctorStatus.Inactive
            };
        }

        // Mapper DoctorViewDetailsDto
        public static DoctorViewDetailsDto MapToDoctorViewDetailsDto(this Doctor model)
        {
            return new DoctorViewDetailsDto
            {
                DoctorId = model.DoctorId,
                UserName = model.User?.Username ?? string.Empty,
                Name = model.Name,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Degree = model.Degree,
                HospitalName = model.HospitalName,
                HospitalAddress = model.HospitalAddress,
                Biography = model.Biography,
                DateOfBirth = model.User?.DateOfBirth,

                //Convert Status from string to enum
                Status = Enum.TryParse<DoctorStatus>(model.Status, true, out var status)
                          ? status
                          : DoctorStatus.Inactive,

                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt
            };
        }

        //Mapper DoctorCreateDto
        public static Doctor MapToDoctor(this DoctorCreateDto dto,Guid userId)
        {
            return new Doctor
            {
                Name = dto.Name,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Degree = dto.Degree,
                HospitalName = dto.HospitalName,
                HospitalAddress = dto.HospitalAddress,
                Biography = dto.Biography,
                Status = DoctorStatus.Active.ToString(),
            };
        }

        //Mapper DoctorUpdateDto
        public static void MapToDoctorUpdateDto(this DoctorUpdateDto updateDto, Doctor doctor)
        {
            if (!string.IsNullOrWhiteSpace(updateDto.Name))
                doctor.Name = updateDto.Name;

            if (!string.IsNullOrWhiteSpace(updateDto.Email))
                doctor.Email = updateDto.Email;

            if (!string.IsNullOrWhiteSpace(updateDto.PhoneNumber))
                doctor.PhoneNumber = updateDto.PhoneNumber;

            if (!string.IsNullOrWhiteSpace(updateDto.Degree))
                doctor.Degree = updateDto.Degree;

            if (!string.IsNullOrWhiteSpace(updateDto.HospitalName))
                doctor.HospitalName = updateDto.HospitalName;

            if (!string.IsNullOrWhiteSpace(updateDto.HospitalAddress))
                doctor.HospitalAddress = updateDto.HospitalAddress;

            if (!string.IsNullOrWhiteSpace(updateDto.Biography))
                doctor.Biography = updateDto.Biography;

            if (updateDto.Status.HasValue)
                doctor.Status = updateDto.Status.ToString();
            doctor.UpdatedAt = DateTime.UtcNow;
        }

        // Mapper DoctorDeleteDto
        public static DoctorDeleteDto MapToDoctorDeleteDto(this Doctor model)
        {
            return new DoctorDeleteDto
            {
                UserName = model.User?.Username ?? string.Empty,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Name = model.Name,
                Degree = model.Degree,
                HospitalName = model.HospitalName,
                HospitalAddress = model.HospitalAddress,
                Biography = model.Biography,


                //Convert Status from string to enum
                Status = Enum.TryParse<DoctorStatus>(model.Status, true, out var status)
                          ? status
                          : DoctorStatus.Inactive,

                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt
            };
        }
    }
}
