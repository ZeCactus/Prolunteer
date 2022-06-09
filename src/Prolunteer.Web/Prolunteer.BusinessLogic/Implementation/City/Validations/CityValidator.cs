using System;
using System.Linq;
using FluentValidation;
using Prolunteer.BusinessLogic.Implementation.City.Models;
using Prolunteer.DataAccess;

namespace Prolunteer.BusinessLogic.Implementation.City.Validations
{
    public class CityCreateModelValidator : AbstractValidator<CityCreateModel>
    {
        public CityCreateModelValidator(UnitOfWork uow)
        {
            RuleFor(ccm => ccm.CountyId)
                .NotEmpty().WithMessage("Camp obligatoriu!")
                .Must(CountyIdExists).WithMessage("Judet invalid!");
            RuleFor(ccm => ccm.Name)
                .NotEmpty().WithMessage("Camp obligatoriu!")
                .Must(CityNameIsUnique).WithMessage("Exista deja un oras cu acest nume");

            bool CountyIdExists(Guid id)
            {
                return uow.Counties.Get().Any(c => c.Id == id && !c.IsDeleted);
            }

            bool CityNameIsUnique(string name)
            {
                return !uow.Cities.Get().Any(c => c.Name == name);
            }
        }
    }
}
