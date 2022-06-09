using AutoMapper;
using Prolunteer.BusinessLogic.Implementation.Location.Models;
using System;

namespace Prolunteer.BusinessLogic.Implementation.Location.Mappings
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<Entities.Location, LocationViewModel>()
                .ForMember(lvm => lvm.City, lvm => lvm.MapFrom(l => l.City.Name))
                .ForMember(lvm => lvm.County, lvm => lvm.MapFrom(l => l.City.County.Name));
            CreateMap<LocationCreateModel, Entities.Location>()
                .ForMember(l => l.Id, l => l.MapFrom(lcm => Guid.NewGuid()));
        }
    }
}
