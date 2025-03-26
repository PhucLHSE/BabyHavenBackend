using BabyHaven.Common.DTOs.DoctorSpecializationDTOs;
using BabyHaven.Common.Enum.DoctorSpecializationEnums;
using BabyHaven.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.Mappers
{
    public static class DoctorSpecializationMapper
    {
        // Mapper DoctorSpecializationViewAllDto
        public static DoctorSpecializationViewAllDto MapToDoctorSpecializationViewAllDto(this DoctorSpecialization model)
        {
            return new DoctorSpecializationViewAllDto
            {
                // MembershipPackage
                DoctorName = model.Doctor?.Name ?? string.Empty,

                // Feature
                SpecializationName = model.Specialization?.SpecializationName ?? string.Empty,

                // Convert Status from string to enum
                Status = Enum.TryParse<DoctorSpecializationStatus>(model.Status, true, out var status)
                          ? status
                          : DoctorSpecializationStatus.Inactive
            };
        }

        // Mapper DoctorSpecializationViewDetailsDto
        public static DoctorSpecializationViewDetailsDto MapToDoctorSpecializationViewDetailsDto(this DoctorSpecialization model)
        {
            return new DoctorSpecializationViewDetailsDto
            {
                // Doctor details
                DoctorName = model.Doctor?.Name ?? string.Empty,
                Email = model.Doctor?.Email ?? string.Empty,
                PhoneNumber = model.Doctor?.PhoneNumber ?? string.Empty,
                Degree = model.Doctor?.Degree ?? string.Empty,
                HospitalName = model.Doctor?.HospitalName ?? string.Empty,
                HospitalAddress = model.Doctor?.HospitalAddress ?? string.Empty,
                Biography = model.Doctor?.PhoneNumber ?? string.Empty,

                // Specialization details
                SpecializationName = model.Specialization?.SpecializationName ?? string.Empty,
                Description = model.Specialization?.Description ?? string.Empty,

                // DoctorSpecialization details
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,

                // Convert Status from string to enum
                Status = Enum.TryParse<DoctorSpecializationStatus>(model.Status, true, out var status)
                          ? status
                          : DoctorSpecializationStatus.Inactive
            };
        }

        //Mapper DoctorSpecializationCreateDto
        public static DoctorSpecialization MapToDoctorSpecialization(this DoctorSpecializationCreateDto dto, int doctorId, int specializationId)
        {
            return new DoctorSpecialization
            {
                DoctorId = doctorId,
                SpecializationId = specializationId,
                Status = DoctorSpecializationStatus.Active.ToString(),
                CreatedAt = DateTime.UtcNow
            };
        }

        //Mapper DoctorSpecializationUpdateDto
        public static void MapToUpdatedDoctorSpecialization(this DoctorSpecialization doctorSpecialization, DoctorSpecializationUpdateDto updateDto)
        {
            if (updateDto.Status.HasValue)
                doctorSpecialization.Status = updateDto.Status.ToString();
        }

        //Mapper DoctorSpecializationDeleteDto
        public static DoctorSpecializationDeleteDto MapToDoctorSpecializationDeleteDto(this DoctorSpecialization model)
        {
            return new DoctorSpecializationDeleteDto
            {
                //Doctor details
                DoctorName = model.Doctor?.Name ?? string.Empty,
                Email = model.Doctor?.Email ?? string.Empty,
                PhoneNumber = model.Doctor?.PhoneNumber ?? string.Empty,
                Degree = model.Doctor?.Degree ?? string.Empty,
                HospitalName = model.Doctor?.HospitalName ?? string.Empty,
                HospitalAddress = model.Doctor?.HospitalAddress ?? string.Empty,
                Biography = model.Doctor?.Biography ?? string.Empty,

                //Specialization details
                SpecializationName = model.Specialization?.SpecializationName ?? string.Empty,
                Description = model.Specialization?.Description ?? string.Empty,

                // PackageFeature details
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,

                // PackageFeature status
                Status = Enum.TryParse<DoctorSpecializationStatus>(model.Status, true, out var status)
                          ? status
                          : DoctorSpecializationStatus.Inactive
            };
        }
    }
}
