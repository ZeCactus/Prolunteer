using AutoMapper;
using Prolunteer.BusinessLogic.Implementation.County.Models;
using System;

namespace Prolunteer.BusinessLogic.Implementation.County.Mappings
{
    class CountyProfile : Profile
    {
        public CountyProfile()
        {
            CreateMap<CountyCreateModel, Entities.County>()
                .ForMember(c => c.Id, c => c.MapFrom(ccm => Guid.NewGuid()));
            CreateMap<Entities.County, CountyViewModel>();
        }
    }
}
