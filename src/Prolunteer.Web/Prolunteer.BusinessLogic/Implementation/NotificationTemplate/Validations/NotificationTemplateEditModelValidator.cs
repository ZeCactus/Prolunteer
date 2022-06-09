using FluentValidation;
using Prolunteer.BusinessLogic.Implementation.NotificationTemplate.Models;
using Prolunteer.DataAccess;
using System;
using System.Linq;

namespace Prolunteer.BusinessLogic.Implementation.NotificationTemplate.Validations
{
    public class NotificationTemplateEditModelValidator : AbstractValidator<NotificationTemplateEditModel>
    {
        public NotificationTemplateEditModelValidator(UnitOfWork uow)
        {
            RuleFor(ntem => ntem.Id)
                .NotEmpty().WithMessage("Don't delete the ID!")
                .Must(IdExists).WithMessage("Don't modify the ID!");

            RuleFor(ntem => ntem.Subject)
                .NotEmpty().WithMessage("Subject cannot be empty!")
                .MaximumLength(40).WithMessage("Subject mustm be at most 40 characters!");

            RuleFor(ntem => ntem.Template)
                .NotEmpty().WithMessage("The notification template cannot be empty!");

            bool IdExists(int id)
            {
                return uow.NotificationTemplates.Get().Any(nt => nt.Id == id);
            }
        }
    }
}
