using FluentValidation;
using Prolunteer.BusinessLogic.Implementation.EventType.Models;
using Prolunteer.DataAccess;
using System;
using System.Linq;

namespace Prolunteer.BusinessLogic.Implementation.EventType.Validations
{
    public class EventTypeCreateModelValidator : AbstractValidator<EventTypeCreateModel>
    {
        public EventTypeCreateModelValidator(UnitOfWork uow)
        {
            RuleFor(ecm => ecm.Name)
                .NotEmpty().WithMessage("Camp obligatoriu!")
                .MaximumLength(20).WithMessage("Numele trebuie sa aiba cel mult 20 de caractere!")
                .Must(NameIsUnique).WithMessage("Exista deja un tip de eveniment cu acest nume!");
            RuleFor(ecm => ecm.Description)
                .MaximumLength(100).WithMessage("Descrierea trebuie sa aiba cel mult 100 de caractere!");

            bool NameIsUnique(string name)
            {
                return !uow.EventTypes.Get().Any(et => et.Name == name);
            }
        }
    }
}
