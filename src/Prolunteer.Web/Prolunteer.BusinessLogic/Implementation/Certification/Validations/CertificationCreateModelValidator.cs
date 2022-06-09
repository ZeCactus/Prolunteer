using FluentValidation;
using Prolunteer.BusinessLogic.Implementation.Certification.Models;
using Prolunteer.DataAccess;
using System;
using System.Linq;

namespace Prolunteer.BusinessLogic.Implementation.Certification.Validations
{
    public class CertificationCreateModelValidator : AbstractValidator<CertificationCreateModel>
    {
        public CertificationCreateModelValidator(UnitOfWork uow)
        {
            RuleFor(ccm => ccm.Name)
                .NotEmpty().WithMessage("Camp obligatoriu!")
                .MaximumLength(20).WithMessage("Numele certificarii trebuie sa aiba cel mult 20 de caractere!")
                .Must(CertificationNameIsUnique).WithMessage("Exista deja o certificare cu acest nume!");
            RuleFor(ccm => ccm.Description)
                .MaximumLength(100).WithMessage("Descrierea certificarii trebuie sa aiba cel mult 100 de caractere!");


            bool CertificationNameIsUnique(string name)
            {
                return !uow.Certifications.Get().Any(c => c.Name == name);
            }
        }
    }
}
