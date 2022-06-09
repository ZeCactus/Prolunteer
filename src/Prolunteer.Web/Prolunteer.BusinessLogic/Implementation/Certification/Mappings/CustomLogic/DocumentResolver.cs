using AutoMapper;
using Prolunteer.BusinessLogic.Implementation.Certification.Models;
using System.IO;

namespace Prolunteer.BusinessLogic.Implementation.Certification.Mappings.CustomLogic
{
    public class DocumentResolver : IValueResolver<CertificationRequestModel, Entities.UserCertificationDocument, byte[]>
    {
        public byte[] Resolve(CertificationRequestModel model, Entities.UserCertificationDocument destination, byte[] bytes, ResolutionContext contest)
        {
            using (var stream = new MemoryStream())
            {
                model.Document.CopyTo(stream);
                var result = stream.ToArray();
                return result;
            }
        }
    }
}
