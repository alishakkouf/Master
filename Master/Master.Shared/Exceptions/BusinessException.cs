using System;

namespace Master.Shared.Exceptions
{

    [Serializable]
    public class BusinessException : Exception
    {
        /// <summary>
        /// Exception for business rules conflicts. 
        /// </summary>
        public BusinessException(string message)
            : base(message)
        {
        }
    }
}
