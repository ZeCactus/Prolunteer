using FluentValidation;
using Prolunteer.BusinessLogic.Implementation.Event.Models;
using Prolunteer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolunteer.BusinessLogic.Implementation.Event.Validations
{
    class EventCreateModelValidator : AbstractValidator<EventCreateModel>
    {
        public EventCreateModelValidator(UnitOfWork uow)
        {
            RuleFor(ecm => ecm.LocationId)
                .NotEmpty().WithMessage("Camp obliatoriu!")
                .Must(LocationIdExists).WithMessage("Selectati o locatie valida!");
            RuleFor(ecm => ecm.EventTypeId)
                    .NotEmpty().WithMessage("Camp Obligatoriu!")
                    .Must(EventTypeIdExists).WithMessage("Selectati un tip de eveniment valid!");
            RuleFor(ecm => ecm.Name)
                .NotEmpty().WithMessage("Camp Obligatoriu!");
            RuleFor(ecm => ecm.StartDate)
                    .NotEmpty().WithMessage("Camp Obligatoriu!")
                    .GreaterThan(DateTime.Now).WithMessage("Evenimentul nu poate incepe in trecut!");
            RuleFor(ecm => ecm.EndDate)
                    .NotEmpty().WithMessage("Camp Obligatoriu!")
                    .GreaterThanOrEqualTo(ecm => ecm.StartDate).WithMessage("Evenimentul nu se poate termina inainte de a incepe!");
            bool LocationIdExists(Guid LocationId)
            {
                return uow.Locations.Get().Any(l => l.Id == LocationId && !l.IsDeleted);
            }
            bool EventTypeIdExists(int EventTypeId)
            {
                return uow.EventTypes.Get().Any(et => et.Id == EventTypeId && !et.IsDeleted);
            }
        }
    }
}
