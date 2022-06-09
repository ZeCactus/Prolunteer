using AutoMapper;
using Prolunteer.DataAccess;
using Prolunteer.Common.DTOs;
using Prolunteer.Notifications.Email;
using Prolunteer.Notifications.NotificationManager;

namespace Prolunteer.BusinessLogic.Base
{
    public class ServiceDependencies
    {
        public IMapper Mapper { get; set; }
        public UnitOfWork UnitOfWork { get; set; }
        public CurrentUserDTO CurrentUser { get; set; }
        public NotificationManager NotificationManager { get; set; }

        public ServiceDependencies(IMapper mapper, UnitOfWork unitofwork, CurrentUserDTO currentuser, NotificationManager notificationManager)
        {
            this.Mapper = mapper;
            this.UnitOfWork = unitofwork;
            this.CurrentUser = currentuser;
            this.NotificationManager = notificationManager;
        }
    }
}
