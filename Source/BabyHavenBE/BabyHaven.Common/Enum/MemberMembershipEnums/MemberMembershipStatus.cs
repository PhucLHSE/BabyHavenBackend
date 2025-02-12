using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Common.Enum.MemberMembershipEnums
{
    public enum MemberMembershipStatus
    {
        // The member is actively using the membership package
        Active,

        // The member has registered but has not activated or is not using the membership
        Inactive,

        // The membership package has expired
        Expired,

        // The member has canceled the membership before its expiration date
        Cancelled,

        // The membership has been suspended due to rule violations or other reasons
        Suspended,

        // The membership is pending processing (awaiting payment, approval, etc.)
        Pending,

        // The member has successfully renewed the membership package
        Renewed
    }
}
