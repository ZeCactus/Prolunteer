using FluentValidation;
using Prolunteer.BusinessLogic.Implementation.County.Models;
using Prolunteer.DataAccess;
using System.Linq;

namespace Prolunteer.BusinessLogic.Implementation.County.Validations
{
    public class CountyCreateModelValidator : AbstractValidator<CountyCreateModel>
    {
        public CountyCreateModelValidator(UnitOfWork uow)
        {
            RuleFor(cm => cm.Name)
                .NotEmpty().WithMessage("Camp obligatoriu!")
                .MaximumLength(20).WithMessage("Lungimea maxima a numelui este de 20 de caractere")
                .Must(IsUnique).WithMessage("Exista deja un judet cu acest nume!");
            bool IsUnique(string name)
            {
                return !uow.Counties.Get().Any(c => c.Name == name);
            }
        }
    }
}
