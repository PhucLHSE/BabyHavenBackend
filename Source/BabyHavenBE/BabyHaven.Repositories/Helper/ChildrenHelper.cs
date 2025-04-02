using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Repositories.Helper
{
    public static class ChildrenHelper
    {
        public static int CalculateAge(DateTime dateOfBirth)
        {
            DateTime today = DateTime.Today;

            // Calculate the difference in years and months
            int years = today.Year - dateOfBirth.Year;
            int months = today.Month - dateOfBirth.Month;

            // Adjust if the birthday hasn't occurred this year
            if (dateOfBirth.Date > today.AddYears(-years))
            {
                years--;
                months += 12; // Add 12 months to account for the year subtraction
            }

            // If the day of the month hasn't been reached yet, subtract one month
            if (today.Day < dateOfBirth.Day)
            {
                months--;
            }

            // If months is negative after adjustment, correct it
            if (months < 0)
            {
                years--;
                months += 12;
            }

            // Total months = (years * 12) + months
            int totalMonths = (years * 12) + months;

            // Ensure totalMonths is not negative (e.g., if dateOfBirth is in the future)
            if (totalMonths < 0)
            {
                totalMonths = 0;
            }

            // Cap the age at 228 months (the maximum in the dataset)
            if (totalMonths > 228)
            {
                totalMonths = 228;
            }

            return totalMonths;
        }
    }
}