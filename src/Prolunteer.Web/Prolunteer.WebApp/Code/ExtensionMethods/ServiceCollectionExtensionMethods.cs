using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Prolunteer.BusinessLogic.Base;
using Prolunteer.BusinessLogic.Implementation.Account;
using Prolunteer.BusinessLogic.Implementation.Account.Mappings.CustomLogic;
using Prolunteer.BusinessLogic.Implementation.Certification;
using Prolunteer.BusinessLogic.Implementation.Certification.Mappings.CustomLogic;
using Prolunteer.BusinessLogic.Implementation.City;
using Prolunteer.BusinessLogic.Implementation.County;
using Prolunteer.BusinessLogic.Implementation.Event;
using Prolunteer.BusinessLogic.Implementation.EventType;
using Prolunteer.BusinessLogic.Implementation.Location;
using Prolunteer.BusinessLogic.Implementation.NotificationTemplate;
using Prolunteer.BusinessLogic.Implementation.Seeding;
using Prolunteer.BusinessLogic.Implementation.VolunteerPosition;
using Prolunteer.BusinessLogic.Implementation.VolunteerPosition.Mappings.CustomLogic;
using Prolunteer.Common.DTOs;
using Prolunteer.DataAccess;
using Prolunteer.WebApp.Code.Base;
using System;
using System.Linq;

namespace Prolunteer.WebApp.Code.ExtensionMethods
{
    public static class ServiceCollectionExtensionMethods
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddScoped<ControllerDependencies>();
            return services;
        }

        public static IServiceCollection AddProlunteerBusinessLogic(this IServiceCollection services)
        {
            services.AddScoped<ServiceDependencies>();
            services.AddScoped<UserAccountService>();
            services.AddScoped<EventService>();
            services.AddScoped<CityService>();
            services.AddScoped<VolunteerPositionService>();
            services.AddScoped<LocationService>();
            services.AddScoped<CountyService>();
            services.AddScoped<EventTypeService>();
            services.AddScoped<CertificationService>();
            services.AddScoped<SeedingService>();
            services.AddScoped<NotificationTemplateService>();

            return services;
        }

        public static IServiceCollection AddProlunteerCustomAutoMapperLogic(this IServiceCollection services)
        {
            services.AddScoped<DocumentResolver>();
            services.AddScoped<DocumentConverter>();
            services.AddScoped<UserIdentityResolver>();
            services.AddScoped<AvailabilityResolver>();

            return services;
        }

        
        public static IServiceCollection AddProlunteerCurrentUser(this IServiceCollection services)
        {
            services.AddScoped(s =>
            {
                var accessor = s.GetService<IHttpContextAccessor>();
                var httpContext = accessor.HttpContext;
                var claims = httpContext.User.Claims;

                var userIdClaim = claims?.FirstOrDefault(c => c.Type == "Id")?.Value;
                var isParsingSuccessful = Guid.TryParse(userIdClaim, out Guid id);

                if (!isParsingSuccessful)
                {
                    return new CurrentUserDTO
                    {
                        IsAuthenticated = false
                    };
                }
                var uow = s.GetService<UnitOfWork>();
                var user = uow.Users.Get()
                    .Include(u => u.UserRoles)
                    .Include(u => u.UserCertifications)
                    .Where(u => u.Id == id)
                    .FirstOrDefault();
                if(user == null)
                {
                    return new CurrentUserDTO
                    {
                        IsAuthenticated = false
                    };
                }
                return new CurrentUserDTO
                {
                    Id = id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    IsAuthenticated = true,
                    Roles = user.UserRoles.Select(ur => ur.RoleId).ToList(),
                    Certifications = user.UserCertifications.Select(uc => uc.CertificationId).ToList()
                };
            });
            
            return services;
        }
    }
}
