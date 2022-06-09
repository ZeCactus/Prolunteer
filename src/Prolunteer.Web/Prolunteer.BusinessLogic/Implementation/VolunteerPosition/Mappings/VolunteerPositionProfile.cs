using AutoMapper;
using Prolunteer.BusinessLogic.Implementation.VolunteerPosition.Mappings.CustomLogic;
using Prolunteer.BusinessLogic.Implementation.VolunteerPosition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolunteer.BusinessLogic.Implementation.VolunteerPosition.Mappings
{
    public class VolunteerPositionProfile : Profile
    {
        public VolunteerPositionProfile()
        {
            CreateMap<VolunteerPositionCreateModel, Entities.VolunteerPosition>()
                .ForMember(vp => vp.Id, vp => vp.MapFrom(vpcm => Guid.NewGuid()))
                .ForMember(vp => vp.RequiredCertifications,
                           vp => vp.MapFrom(vpcm => vpcm.RequiredCertifications
                                                        .Select(rqc => new Entities.RequiredCertification 
                                                                            {
                                                                                CertificationId = rqc 
                                                                            })
                                                        .ToHashSet()));
            CreateMap<Entities.VolunteerPosition, VolunteerPositionViewModel>()
                .ForMember(vpvm => vpvm.EnrolledVolunteers,
                           vpvm => vpvm.MapFrom(vp => vp.VolunteerParticipations.Count()));
            CreateMap<Entities.VolunteerPosition, VolunteerPositionDetailsModel>()
                .ForMember(vpdm => vpdm.Volunteers,
                           vpdm => vpdm.MapFrom(vp => vp.VolunteerParticipations
                                                        .Select(vpa => vpa.User)
                                                        .ToList()
                                               )
                           );
            CreateMap<Entities.VolunteerPosition, AvailablePositionModel>()
                .ForMember(apm => apm.IsAvailable, apm => apm.MapFrom<AvailabilityResolver>())
                .ForMember(vpvm => vpvm.EnrolledVolunteers,
                           vpvm => vpvm.MapFrom(vp => vp.VolunteerParticipations.Count())); ;
                
        }
    }
}
