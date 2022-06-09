using AutoMapper;
using Prolunteer.BusinessLogic.Implementation.EventType.Models;
using System;

namespace Prolunteer.BusinessLogic.Implementation.EventType.Mappings
{
    public class EventTypeProfile : Profile
    {
        public EventTypeProfile()
        {
            CreateMap<EventTypeCreateModel, Entities.EventType>()
                .ForMember(et => et.Id, et => et.Ignore());
            CreateMap<Entities.EventType, EventTypeViewModel>();
        }
    }
}
