using AutoMapper;
using Prolunteer.BusinessLogic.Implementation.Account.Mappings.CustomLogic;
using Prolunteer.BusinessLogic.Implementation.Certification.Mappings.CustomLogic;
using Prolunteer.BusinessLogic.Implementation.Certification.Models;
using System;
using System.Collections.Generic;

namespace Prolunteer.BusinessLogic.Implementation.Certification.Mappings
{
    

    public class CertificationProfile : Profile
    {
        public CertificationProfile()
        {
            CreateMap<Entities.Certification, CertificationViewModel>();

            CreateMap<CertificationCreateModel, Entities.Certification>();

            CreateMap<CertificationRequestModel, Entities.UserCertification>()
                .ForMember(uc => uc.Approved, uc => uc.MapFrom(crm => false))
                .ForMember(uc => uc.UserId, uc => uc.MapFrom<UserIdentityResolver>())
                .ForMember(uc => uc.UserCertificationDocuments, uc => uc.MapFrom(crm => crm));

            CreateMap<CertificationRequestModel, Entities.UserCertificationDocument>()
                .ForMember(ucd => ucd.Id, ucd => ucd.MapFrom(iff => Guid.NewGuid()))
                .ForMember(ucd => ucd.Document, ucd => ucd.MapFrom<DocumentResolver>());

            CreateMap<CertificationRequestModel, ICollection<Entities.UserCertificationDocument>>()
                .ConvertUsing(new DocumentConverter());

            CreateMap<Entities.UserCertification, PendingCertificationViewModel>()
                .ForMember(pcvm => pcvm.CertificationName, pcvm => pcvm.MapFrom(ucd => ucd.Certification.Name))
                .ForMember(pcvm => pcvm.UserName, pcvm => pcvm.MapFrom(ucd => $"{ucd.User.FirstName} {ucd.User.LastName}"));
        }
    }
}
