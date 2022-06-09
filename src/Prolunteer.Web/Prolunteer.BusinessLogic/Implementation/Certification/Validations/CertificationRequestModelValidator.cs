using FluentValidation;
using Prolunteer.BusinessLogic.Implementation.Certification.Models;
using Prolunteer.DataAccess;
using System;
using System.Linq;

namespace Prolunteer.BusinessLogic.Implementation.Certification.Validations
{
    public class CertificationRequestModelValidator : AbstractValidator<CertificationRequestModel>
    {
        private readonly int MaximumFileSize = 1024;
        public CertificationRequestModelValidator(UnitOfWork uow, Guid userId)
        {
            RuleFor(crm => crm.CertificationId)
                .NotEmpty().WithMessage("Campul este obligatoriu!")
                .Must(CertificationIdExists).WithMessage("Certificarea selectata nu exista!")
                .Must(UserDoesNotHaveCertification).WithMessage("Detineti deja certificarea selectata!");

            RuleFor(crm => crm.Document)
                .Must(doc => doc.Length > 0).WithMessage("Fisierul incarcat nu poate fi citit!")
                .Must(doc => doc.Length < MaximumFileSize * 1000).WithMessage($"Documentul incarcat trebuie sa fie mai mic de {MaximumFileSize}KB!");

            bool CertificationIdExists(int id)
            {
                return uow.Certifications.Get().Any(c => c.Id == id && !c.IsDeleted);
            }

            bool UserDoesNotHaveCertification(int id)
            {
                return !uow.UserCertifications.Get().Any(uc => uc.UserId == userId && uc.CertificationId == id);
            }

        }
    }
}
