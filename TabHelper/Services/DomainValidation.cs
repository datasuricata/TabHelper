using System;

namespace TabHelper.Services
{
    public class DomainValidation : Exception
    {
        public DomainValidation(string message) : base(message)
        {
            
        }

        public static void When(bool hasError, string message)
        {
            if (hasError)
            {
                throw new DomainValidation(message);
            }

        }
    }
}