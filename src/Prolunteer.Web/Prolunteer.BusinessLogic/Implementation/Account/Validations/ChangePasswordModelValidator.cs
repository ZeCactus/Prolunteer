using FluentValidation;
using Prolunteer.BusinessLogic.Implementation.Account.Models;
using Prolunteer.BusinessLogic.Implementation.Account.Utilities;
using Prolunteer.DataAccess;
using System;

namespace Prolunteer.BusinessLogic.Implementation.Account.Validations
{
    public class ChangePasswordModelValidator : AbstractValidator<ChangePasswordModel>
    {
        public ChangePasswordModelValidator(string oldPasswordHash)
        {
            RuleFor(cpm => cpm.OldPassword)
                .Must(PasswordIsValid).WithMessage("Parola incorecta!");
            RuleFor(cpm => cpm.NewPassword)
                .Equal(cpm => cpm.NewPasswordRepeat)
                    .WithMessage("Parola noua este diferita in campul de confirmare!")
                    .When(cpm => string.IsNullOrWhiteSpace(cpm.NewPassword));

            bool PasswordIsValid(string password)
            {
                return PasswordUtilities.CheckPassword(password, oldPasswordHash);
            }
        }
    }
}
