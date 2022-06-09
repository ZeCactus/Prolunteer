using Microsoft.EntityFrameworkCore;
using Prolunteer.BusinessLogic.Base;
using Prolunteer.BusinessLogic.Implementation.Certification.Models;
using Prolunteer.BusinessLogic.Implementation.Certification.Validations;
using Prolunteer.Common.DTOs;
using Prolunteer.Common.Exceptions;
using Prolunteer.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prolunteer.BusinessLogic.Implementation.Certification
{
    public class CertificationService : BaseService
    {
        private readonly CertificationCreateModelValidator CertificationCreateModelValidator;
        private readonly CertificationRequestModelValidator CertificationRequestModelValidator;

        public CertificationService(ServiceDependencies dependencies)
            : base(dependencies)
        {
            this.CertificationCreateModelValidator = new CertificationCreateModelValidator(uow);
            this.CertificationRequestModelValidator = new CertificationRequestModelValidator(uow, CurrentUser.Id);
        }

        public PaginationDTO<CertificationViewModel> GetCertifications(int pageNumber, int pageSize, string filter)
        {
            var certifications = uow.Certifications.Get();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                certifications = certifications.Where(c => c.Name.Contains(filter));
            }
            var elements = certifications
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(c => Mapper.Map<CertificationViewModel>(c))
                .ToList();

            var count = certifications.Count();

            return new PaginationDTO<CertificationViewModel>(elements, count);
        }

        public bool RemoveCertification(int id)
        {
            return ExecuteInTransaction(uow =>
            {
                var entityToRemove = uow.Certifications.Get().Where(c => c.Id == id).FirstOrDefault();

                if(entityToRemove == null)
                {
                    return false;
                }

                Delete(entityToRemove);
                uow.SaveChanges();

                return true;
            });
        }

        public void AddCertification(CertificationCreateModel model)
        {
            ExecuteInTransaction(uow =>
            {
                CertificationCreateModelValidator.Validate(model).ThenThrow();

                var entityToAdd = Mapper.Map<Entities.Certification>(model);

                uow.Certifications.Insert(entityToAdd);
                uow.SaveChanges();
            });
        }

        public void RequestCertification(CertificationRequestModel model)
        {
            ExecuteInTransaction(uow =>
            {
                CertificationRequestModelValidator.Validate(model).ThenThrow();

                var entityToCreate = Mapper.Map<Entities.UserCertification>(model);

                uow.UserCertifications.Insert(entityToCreate);
                uow.SaveChanges();
            });
        }

        public List<ListItemModel<int, string>> GetCertificationsAsListItemModelList()
        {
            return uow.Certifications
                .Get()
                .Where(c => !c.IsDeleted)
                .Select(c => new ListItemModel<int, string>
                {
                    Value = c.Id,
                    Text = c.Name
                })
                .ToList();
        }

        public PaginationDTO<PendingCertificationViewModel> GetPendingCertifications(int pageNumber, int pageSize, string filter)
        {
            var certifications = uow.UserCertifications
                .Get()
                .Where(uc => !uc.Approved);
            if (!string.IsNullOrWhiteSpace(filter))
            {
                certifications = certifications.Where(c => c.Certification.Name.Contains(filter) || c.User.FirstName.Contains(filter) || c.User.LastName.Contains(filter));
            }
            var elements = certifications
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(uc => uc.Certification)
                .Include(ucd => ucd.User)
                .Select(ucd => Mapper.Map<PendingCertificationViewModel>(ucd))
                .ToList();

            var count = certifications.Count();

            return new PaginationDTO<PendingCertificationViewModel>(elements, count);
        }

        public PendingCertificationViewModel GetPendingCertification(Guid userId, int certificationId)
        {
            return uow.UserCertifications
                .Get()
                .Include(uc => uc.Certification)
                .Include(uc => uc.User)
                .Where(uc => uc.UserId == userId && uc.CertificationId == certificationId)
                .Select(uc => Mapper.Map<PendingCertificationViewModel>(uc))
                .FirstOrDefault();
        }

        public string GetPendingCertificationDocument(Guid userId, int certificationId)
        {
            var documentAsByteArray = uow.UserCertificationDocuments
                .Get()
                .Where(ucd => ucd.UserId == userId && ucd.CertificationId == certificationId)
                .Select(ucd => ucd.Document)
                .FirstOrDefault();
            return Convert.ToBase64String(documentAsByteArray);
        }

        public void ApproveCertificationRequest(Guid userId, int certificationId)
        {
            ExecuteInTransaction(uow =>
            {
                var entityToUpdate = uow.UserCertifications
                    .Get()
                    .Where(uc => uc.CertificationId == certificationId && uc.UserId == userId)
                    .SingleOrDefault();
                
                if(entityToUpdate == null)
                {
                    throw new NotFoundErrorException();
                }

                entityToUpdate.Approved = true;

                uow.UserCertifications.Update(entityToUpdate);
                uow.SaveChanges();

            });
        }

        public List<ListItemModel<int, string>> GetCertificationsAvailableForUserAsListItemModelList()
        {
            return uow.Certifications
                .Get()
                .Where(c => !c.IsDeleted)
                .Include(c => c.UserCertifications)
                .Where(c => !c.UserCertifications
                                .Any(uc => uc.UserId == CurrentUser.Id))
                .Select(c => new ListItemModel<int, string>
                {
                    Value = c.Id,
                    Text = c.Name
                })
                .ToList();
        }
    }
}
