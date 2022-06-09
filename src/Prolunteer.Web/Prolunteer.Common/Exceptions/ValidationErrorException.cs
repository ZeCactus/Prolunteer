using System;
using System.Collections.Generic;
using FluentValidation.Results;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolunteer.Common.Exceptions
{
    public class ValidationErrorException : Exception
    {
        public readonly ValidationResult ValidationResult;

        public ValidationErrorException(ValidationResult result)
        {
            ValidationResult = result;
        }
    }
}
