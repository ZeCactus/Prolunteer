using FluentValidation;
using Prolunteer.BusinessLogic.Implementation.Account.Models;
using Prolunteer.DataAccess;
using Prolunteer.Entities;
using Prolunteer.Entities.Enums;
using System.Linq;

namespace Prolunteer.BusinessLogic.Implementation.Account.Validations
{
    class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator(UnitOfWork uow)
        {
            RuleFor(rm => rm.EMail)
                .NotEmpty().WithMessage("This field is required!")
                .Must(NotAlreadyExist).WithMessage("An account using this email address already exists!")
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible).WithMessage("The email address you entered is not valid!");
            RuleFor(rm => rm.FirstName)
                .NotEmpty().WithMessage("This field is required!");
            RuleFor(rm => rm.LastName)
                .NotEmpty().WithMessage("This field is required!");
            RuleFor(rm => rm.BirthDay)
                .NotEmpty().WithMessage("This field is required!");
            RuleFor(rm => rm.Password)
                .NotEmpty().WithMessage("This field is required!");
            RuleFor(rm => rm.Role)
                .NotEmpty().WithMessage("This field is required!")
                .Must(UserSelectable).WithMessage("The option you chose is invalid!");

            bool NotAlreadyExist(string email)
            {
                return !uow.Users.Get().Any(u => u.EMail == email);
            }
            bool UserSelectable(int id)
            {
                return uow.Roles.Get()
                    .Where(r => r.Id != (int)RoleTypes.Admin)
                    .Any(r => r.Id == id);
            }
        }
    }
}
