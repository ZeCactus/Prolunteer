using AutoMapper;
using Prolunteer.BusinessLogic.Implementation.NotificationTemplate.Models;
using Prolunteer.Entities;
using System;

namespace Prolunteer.BusinessLogic.Implementation.NotificationTemplate.Mappings
{
    public class NotificationTemplateProfile : Profile
    {
        public NotificationTemplateProfile()
        {
            CreateMap<Entities.NotificationTemplate, NotificationTemplateEditModel>();
        }
    }
}
