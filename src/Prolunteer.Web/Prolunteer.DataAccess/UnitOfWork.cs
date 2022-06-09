using Prolunteer.Common;
using Prolunteer.DataAccess.EntityFramework;
using Prolunteer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolunteer.DataAccess
{
    public class UnitOfWork
    {
        private readonly ProlunteerContext Context;

        public UnitOfWork(ProlunteerContext context)
        {
            this.Context = context;
        }

        private IRepository<Certification> certifications;
        public IRepository<Certification> Certifications => certifications ?? (certifications = new BaseRepository<Certification>(Context));

        private IRepository<City> cities;
        public IRepository<City> Cities => cities ?? (cities = new BaseRepository<City>(Context));

        private IRepository<County> counties;
        public IRepository<County> Counties => counties ?? (counties = new BaseRepository<County>(Context));

        private IRepository<Location> locations;
        public IRepository<Location> Locations => locations ?? (locations = new BaseRepository<Location>(Context));
        
        private IRepository<Event> events;
        public IRepository<Event> Events => events ?? (events = new BaseRepository<Event>(Context));

        private IRepository<EventType> eventTypes;
        public IRepository<EventType> EventTypes => eventTypes ?? (eventTypes = new BaseRepository<EventType>(Context));

        private IRepository<RequiredCertification> requiredCertifications;
        public IRepository<RequiredCertification> RequiredCertifications => requiredCertifications ?? (requiredCertifications = new BaseRepository<RequiredCertification>(Context));

        private IRepository<Role> roles;
        public IRepository<Role> Roles => roles ?? (roles = new BaseRepository<Role>(Context));

        private IRepository<User> users;
        public IRepository<User> Users => users ?? (users = new BaseRepository<User>(Context));

        private IRepository<UserCertification> userCertifications;
        public IRepository<UserCertification> UserCertifications => userCertifications ?? (userCertifications = new BaseRepository<UserCertification>(Context));

        private IRepository<UserCertificationDocument> userCertificationDocuments;
        public IRepository<UserCertificationDocument> UserCertificationDocuments => userCertificationDocuments ?? (userCertificationDocuments = new BaseRepository<UserCertificationDocument>(Context));

        private IRepository<UserRole> userRoles;
        public IRepository<UserRole> UserRoles => userRoles ?? (userRoles = new BaseRepository<UserRole>(Context));

        private IRepository<VolunteerParticipation> volunteerParticipations;
        public IRepository<VolunteerParticipation> VolunteerParticipations => volunteerParticipations ?? (volunteerParticipations = new BaseRepository<VolunteerParticipation>(Context));

        private IRepository<VolunteerPosition> volunteerPositions;
        public IRepository<VolunteerPosition> VolunteerPositions => volunteerPositions ?? (volunteerPositions = new BaseRepository<VolunteerPosition>(Context));

        private IRepository<NotificationTemplate> notificationTemplates;
        public IRepository<NotificationTemplate> NotificationTemplates => notificationTemplates ?? (notificationTemplates = new BaseRepository<NotificationTemplate>(Context));

        public void ClearTracking()
        {
            Context.ChangeTracker.Clear();
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }

}
