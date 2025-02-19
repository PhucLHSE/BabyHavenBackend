using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyHaven.Common.DTOs.ChildrenDTOs;
using BabyHaven.Repositories.Models;

namespace BabyHaven.Services.Mappers
{
    public static class ChildrenMapper
    {
        public static Child ToChild(this ChildCreateDto dto)
        {
            return new Child
            {
                Name = dto.Name,
                DateOfBirth = dto.DateOfBirth,
                MemberId = dto.MemberId,
                Gender = dto.Gender,
                BirthWeight = dto.BirthWeight,
                BirthHeight = dto.BirthHeight,
                BloodType = dto.BloodType,
                Allergies = dto.Allergies,
                Notes = dto.Notes,
                RelationshipToMember = dto.RelationshipToMember
            };
        }

        public static Child ToChild(this ChildUpdateDto dto, Child existingChild)
        {
            existingChild.Name = dto.Name;
            existingChild.DateOfBirth = dto.DateOfBirth;
            existingChild.Gender = dto.Gender;
            existingChild.BloodType = dto.BloodType;
            existingChild.Allergies = dto.Allergies;
            existingChild.Notes = dto.Notes;
            existingChild.RelationshipToMember = dto.RelationshipToMember;
            existingChild.UpdatedAt = DateTime.UtcNow;

            return existingChild;
        }

        public static ChildViewAllDto ToChildViewAllDto(this Child child)
        {
            return new ChildViewAllDto
            {
                Name = child.Name,
                DateOfBirth = child.DateOfBirth,
                Age = child.CalculateAge(),
                BirthHeight = child.BirthHeight,
                BirthWeight = child.BirthWeight,
                Gender = child.Gender,
                BloodType = child.BloodType,
                Allergies = child.Allergies,
                Notes = child.Notes,
                RelationshipToMember = child.RelationshipToMember
            };
        }

        public static ChildViewDetailsDto ToChildViewDetailsDto(this Child child)
        {
            return new ChildViewDetailsDto
            {
                ChildId = child.ChildId,
                Name = child.Name,
                DateOfBirth = child.DateOfBirth,
                Age = CalculateAge(child.DateOfBirth),
                Gender = child.Gender,
                BirthWeight = child.BirthWeight,
                BirthHeight = child.BirthHeight,
                BloodType = child.BloodType,
                Allergies = child.Allergies,
                Notes = child.Notes,
                RelationshipToMember = child.RelationshipToMember,
                MemberId = child.MemberId,
                CreatedAt = child.CreatedAt,
                UpdatedAt = child.UpdatedAt
            };
        }

        private static int CalculateAge(DateOnly dateOfBirth)
        {
            if (dateOfBirth == default)
            {
                return 0;
            }

            var today = DateOnly.FromDateTime(DateTime.Today);
            var age = today.Year - dateOfBirth.Year;

            if (today < dateOfBirth.AddYears(age))
            {
                age--;
            }

            return age;
        }
    }
}
