using AutoMapper;
using Prolunteer.BusinessLogic.Implementation.Event.Models;
using Prolunteer.Entities;
using Prolunteer.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prolunteer.BusinessLogic.Implementation.VolunteerPosition.Models;
using Prolunteer.BusinessLogic.Implementation.Account.Mappings.CustomLogic;

namespace Prolunteer.BusinessLogic.Implementation.Event.Mappings
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<EventCreateModel, Entities.Event>()
                .ForMember(e => e.Id, e => e.MapFrom(ecm => Guid.NewGuid()))
                .ForMember(e => e.OrganizerId, e => e.MapFrom<UserIdentityResolver>())
                .ForMember(e => e.Image, e => e.Ignore());

            CreateMap<Entities.Event, EventVM>()
                .ForMember(evm => evm.Organizer, evm => evm.MapFrom(e => $"{e.Organizer.FirstName} {e.Organizer.LastName}"))
                .ForMember(evm => evm.EventType, evm => evm.MapFrom(e => e.EventType.Name))
                .ForMember(evm => evm.Location, evm => evm.MapFrom(e => e.Location.Name))
                .ForMember(evm => evm.Image, evm => evm.MapFrom(e => e.Image.Length != 0 ? Convert.ToBase64String(e.Image) : ""))
                .ForMember(evm => evm.StartDate, evm => evm.MapFrom(e => e.StartDate.Date.ToString("dd/MM/yyyy")))
                .ForMember(evm => evm.EndDate, evm => evm.MapFrom(e => e.EndDate.Date.ToString("dd/MM/yyyy")));

            CreateMap<Entities.VolunteerPosition, VolunteerEventVM>()
                .ForMember(evm => evm.Organizer, evm => evm.MapFrom(vp => $"{vp.Event.Organizer.FirstName} {vp.Event.Organizer.LastName}"))
                .ForMember(evm => evm.EventType, evm => evm.MapFrom(vp => vp.Event.EventType.Name))
                .ForMember(evm => evm.Location, evm => evm.MapFrom(vp => vp.Event.Location.Name))
                .ForMember(evm => evm.StartDate, evm => evm.MapFrom(vp => vp.Event.StartDate.Date.ToString("dd/MM/yyyy")))
                .ForMember(evm => evm.EndDate, evm => evm.MapFrom(vp => vp.Event.EndDate.Date.ToString("dd/MM/yyyy")))
                .ForMember(evm => evm.PositionName, evm => evm.MapFrom(vp => vp.Name))
                .ForMember(evm => evm.Name, evm => evm.MapFrom(vp => vp.Event.Name))
                .ForMember(evm => evm.Description, evm => evm.MapFrom(vp => vp.Event.Name))
                .ForMember(evm => evm.Image, evm => evm.MapFrom(vp => Convert.ToBase64String(vp.Event.Image)));

            CreateMap<Entities.Event, EventDetailsVM>()
                .ForMember(edvm => edvm.Organizer, edvm => edvm.MapFrom(e => $"{e.Organizer.FirstName} {e.Organizer.LastName}"))
                .ForMember(edvm => edvm.EventType, edvm => edvm.MapFrom(e => e.EventType.Name))
                .ForMember(edvm => edvm.Location, edvm => edvm.MapFrom(e => e.Location.Name))
                .ForMember(edvm => edvm.City, edvm => edvm.MapFrom(e => e.Location.City.Name))
                .ForMember(edvm => edvm.County, edvm => edvm.MapFrom(e => e.Location.City.County.Name))
                .ForMember(edvm => edvm.Image, edvm => edvm.MapFrom(e => Convert.ToBase64String(e.Image)))
                .ForMember(edvm => edvm.VolunteerPositions,
                           edvm => edvm.MapFrom((e, edvm, i, context) => e.VolunteerPositions
                                                    .Select(vp => context.Mapper.Map<Entities.VolunteerPosition, VolunteerPositionViewModel>(vp))
                                                    .ToList()));
            CreateMap<Entities.Event, AvailableEventDetailsModel>()
                .ForMember(edvm => edvm.Organizer, edvm => edvm.MapFrom(e => $"{e.Organizer.FirstName} {e.Organizer.LastName}"))
                .ForMember(edvm => edvm.EventType, edvm => edvm.MapFrom(e => e.EventType.Name))
                .ForMember(edvm => edvm.Location, edvm => edvm.MapFrom(e => e.Location.Name))
                .ForMember(edvm => edvm.City, edvm => edvm.MapFrom(e => e.Location.City.Name))
                .ForMember(edvm => edvm.County, edvm => edvm.MapFrom(e => e.Location.City.County.Name))
                .ForMember(edvm => edvm.AvailablePositions, edvm => edvm.MapFrom(e => e.VolunteerPositions))
                .ForMember(edvm => edvm.Image, edvm => edvm.MapFrom(e => Convert.ToBase64String(e.Image)));
        }
    }
}
