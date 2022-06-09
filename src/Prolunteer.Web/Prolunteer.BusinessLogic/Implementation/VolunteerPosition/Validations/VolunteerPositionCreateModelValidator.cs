using FluentValidation;
using Prolunteer.BusinessLogic.Implementation.VolunteerPosition.Models;
using Prolunteer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolunteer.BusinessLogic.Implementation.VolunteerPosition.Validations
{
    public class VolunteerPositionCreateModelValidator : AbstractValidator<VolunteerPositionCreateModel>
    {
        public VolunteerPositionCreateModelValidator(UnitOfWork uow)
        {
            RuleFor(vpcm => vpcm.EventId)
                .NotEmpty().WithMessage("Id-ul evenimentului lipseste!")
                .Must(EventIdExists).WithMessage("Id-ul evenimentului este invalid!");
            RuleFor(vpcm => vpcm.Name)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(vpcm => vpcm.RequiredCertifications)
                .Must(AllRequiredCertificationsExist).WithMessage("Introduceti doar certificari valide!");
            RuleFor(vpcm => vpcm.MaximumNrOfVolunteers)
                .GreaterThan(0).WithMessage("Introduceti un numar de voluntari mai mare ca 0!");

            bool AllRequiredCertificationsExist(List<int> requiredCertifications)
            {
                return !requiredCertifications
                       .Except(uow.Certifications
                                  .Get()
                                  .Select(c => c.Id)
                                  .ToList())
                       .Any();
            }

            bool EventIdExists(Guid eventId)
            {
                return uow.Events.Get().Any(e => e.Id == eventId && !e.IsDeleted);
            }
        }
    }
}
