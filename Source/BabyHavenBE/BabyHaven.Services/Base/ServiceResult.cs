using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyHaven.Services.Base
{
    public class ServiceResult : IServiceResult
    {
        public int Status { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }

        public ServiceResult()
        {
            Status = -1;
            Message = "Action fail";
        }

        public ServiceResult(int status, string message)
        {
            Status = status;
            Message = message;
        }

        public ServiceResult(int status, string message, object data)
        {
            Status = status;
            Message = message;
            Data = data;
        }
    }
}
