using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Common.DTOs.AlertDTOS
{
    public class AlertViewCheckingDto
    {
        [Required]
        public Guid MemberId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string DateOfBirth { get; set; }
    }
}
