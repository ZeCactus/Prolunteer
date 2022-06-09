using AutoMapper;
using Prolunteer.BusinessLogic.Implementation.City.Models;
using System;

namespace Prolunteer.BusinessLogic.Implementation.City.Mappings
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<CityCreateModel, Entities.City>()
                .ForMember(c => c.Id, c => c.MapFrom(ccm => Guid.NewGuid()));
            CreateMap<Entities.City, CityViewModel>()
                .ForMember(cvm => cvm.County, cvm => cvm.MapFrom(c => c.County.Name));
        }
    }
}