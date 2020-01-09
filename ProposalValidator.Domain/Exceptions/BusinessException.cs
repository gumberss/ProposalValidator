using System;

namespace ProposalValidator.Domain.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(String message) : base(message) { }
    }
}
