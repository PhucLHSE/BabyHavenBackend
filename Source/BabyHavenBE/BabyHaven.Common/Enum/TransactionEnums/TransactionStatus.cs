using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Common.Enum.TransactionEnums
{
    public enum TransactionStatus
    {
        // Waiting for processing
        Pending,

        // Transaction successfully completed
        Completed,

        // Transaction failed
        Failed,

        // Transaction was canceled
        Cancelled,

        // Amount refunded to the customer
        Refunded,

        // Transaction is being processed
        Processing,

        // Transaction is on hold or temporarily paused
        OnHold
    }
}
