using AutoMapper;
using Prolunteer.BusinessLogic.Implementation.Certification.Models;
using System;
using System.Collections.Generic;

namespace Prolunteer.BusinessLogic.Implementation.Certification.Mappings.CustomLogic
{
    public class DocumentConverter : ITypeConverter<CertificationRequestModel, ICollection<Entities.UserCertificationDocument>>
    {
        public ICollection<Entities.UserCertificationDocument> Convert(CertificationRequestModel model, ICollection<Entities.UserCertificationDocument> destination, ResolutionContext context)
        {
            var ucd = context.Mapper.Map<Entities.UserCertificationDocument>(model);
            var result = new List<Entities.UserCertificationDocument>();
            result.Add(ucd);
            return result;
        }
    }
}
