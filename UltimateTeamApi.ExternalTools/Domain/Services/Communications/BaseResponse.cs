using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTeamApi.ExternalTools.Domain.Services.Communications
{
    public abstract class BaseResponse<T>
    {
        public bool Success { get; protected set; }

        public string Message { get; set; }

        public T Resource { get; set; }

        public BaseResponse(T resource)
        {
            Resource = resource;
            Success = true;
            Message = string.Empty;
        }

        public BaseResponse(string message)
        {
            Success = false;
            Message = message;
        }
    }
}
