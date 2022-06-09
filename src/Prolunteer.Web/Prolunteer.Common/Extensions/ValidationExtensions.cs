using System;
using System.Collections.Generic;
using FluentValidation.Results;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prolunteer.Common.Exceptions;

namespace Prolunteer.Common.Extensions
{
    public static class ValidationExtensions
    {
        public static void ThenThrow(this ValidationResult result)
        {
            if (!result.IsValid)
            {
                throw new ValidationErrorException(result);
            }
        }
    }
}
