using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Common.Enum.ConsultationRequestEnums
{
    public enum ConsultationRequestStatus
    {
        // Waiting for processing
        Pending,

        // Approved by doctor/admin
        Approved,

        // Rejected by doctor/admin
        Rejected,

        // Consultation completed
        Completed
    }
}
