using FluentValidation;
using Prolunteer.BusinessLogic.Implementation.Location.Models;
using Prolunteer.DataAccess;
using System;
using System.Linq;

namespace Prolunteer.BusinessLogic.Implementation.Location.Validations
{
    public class LocationCreateModelValidator : AbstractValidator<LocationCreateModel>
    {
        public LocationCreateModelValidator(UnitOfWork uow)
        {
            RuleFor(lcm => lcm.CityId)
                .NotEmpty().WithMessage("Camp obligatoriu!")
                .Must(IsValidCityId).WithMessage("Orasul selectat nu exista!");

            RuleFor(lcm => lcm.Name)
                .NotEmpty().WithMessage("Camp obligatoriu!")
                .MaximumLength(20).WithMessage("Numele loctiei trebuie sa aiba cel mult 20 de caractere")
                .Must(NameNotAlreadyExists).WithMessage("Exista deja o locatie cu acest nume!");
            
            bool IsValidCityId(Guid id)
            {
                return uow.Cities.Get().Any(c => c.Id == id && !c.IsDeleted);
            }
            
            bool NameNotAlreadyExists(string name)
            {
                return !uow.Locations.Get().Any(l => l.Name == name);
            }
        }
    }
}
