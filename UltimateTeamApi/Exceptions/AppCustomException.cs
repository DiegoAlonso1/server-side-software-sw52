using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace UltimateTeamApi.Exceptions
{
    public class AppCustomException : Exception
    {
        public AppCustomException() : base()
        {
        }

        public AppCustomException(string message) : base(message)
        {
        }

        public AppCustomException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
