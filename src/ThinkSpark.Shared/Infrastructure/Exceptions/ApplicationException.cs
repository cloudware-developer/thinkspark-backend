using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkSpark.Shared.Infrastructure.Exceptions
{
    public class ApplicationException : Exception
    {
        public ApplicationException()
        {
        }

        public ApplicationException(string message) : base(message)
        {
        }

        public ApplicationException(string message, Exception inner) : base(message, inner)
        {
        }

        public ApplicationException(List<ValidationFailure> Errors)
        {

        }

        public ApplicationException(string message, List<ValidationFailure> Errors)
        {
        }
    }
}
