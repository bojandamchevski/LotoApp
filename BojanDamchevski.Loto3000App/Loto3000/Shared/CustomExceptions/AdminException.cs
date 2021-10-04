using System;

namespace Shared.CustomExceptions
{
    public class AdminException : Exception
    {
        public AdminException(string message) : base(message)
        {

        }
    }
}
