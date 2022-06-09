using AutoMapper;
using Prolunteer.Common.DTOs;
using Prolunteer.DataAccess;
using System;
using System.Transactions;
using Prolunteer.Entities;
using System.Linq;
using Prolunteer.Entities.Enums;
using Prolunteer.Notifications.Email;
using Prolunteer.Notifications.NotificationManager;

namespace Prolunteer.BusinessLogic.Base
{
    public class BaseService
    {
        protected readonly IMapper Mapper;
        protected readonly UnitOfWork uow;
        protected readonly CurrentUserDTO CurrentUser;
        protected readonly NotificationManager NotificationManager;

        public BaseService(ServiceDependencies serviceDependencies)
        {
            this.Mapper = serviceDependencies.Mapper;
            this.uow = serviceDependencies.UnitOfWork;
            this.CurrentUser = serviceDependencies.CurrentUser;
            this.NotificationManager = serviceDependencies.NotificationManager;
            
        }

        protected TResult ExecuteInTransaction<TResult>(Func<UnitOfWork, TResult> func)
        {
            if(func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }

            using (var transactionScope = new TransactionScope())
            {
                var result = func(uow);
                transactionScope.Complete();
                return result;
            }
        }

        protected void ExecuteInTransaction(Action<UnitOfWork> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            using (var transactionScope = new TransactionScope())
            {
                action(uow);

                transactionScope.Complete();
            }
        }

        protected void Delete(VolunteerPosition volunteerPosition)
        {
            volunteerPosition.IsDeleted = true;
            uow.VolunteerPositions.Update(volunteerPosition);
        }

        protected void Delete(Event eventEntity)
        {

            foreach(var volunteerPosition in eventEntity.VolunteerPositions)
            {
                if(!volunteerPosition.IsDeleted)
                {
                    Delete(volunteerPosition);
                }
            }

            eventEntity.IsDeleted = true;

            uow.Events.Update(eventEntity);
        }

        protected void Delete(EventType eventType)
        {
            eventType.IsDeleted = true;

            uow.EventTypes.Update(eventType);
        }

        protected void Delete(User user)
        {
            foreach(var eventEntity in user.Events)
            {
                if(!eventEntity.IsDeleted)
                {
                    Delete(eventEntity);
                }
            }

            user.IsDeleted = true;
            uow.Users.Update(user);
        }

        protected void Delete(Location location)
        {
            location.IsDeleted = true;

            uow.Locations.Update(location);
        }

        protected void Delete(City city)
        {
            foreach(var location in city.Locations)
            {
                if (!location.IsDeleted)
                {
                    Delete(location);
                }
            }

            city.IsDeleted = true;

            uow.Cities.Update(city);
        }

        protected void Delete(County county)
        {
            foreach(var city in county.Cities)
            {
                if (!city.IsDeleted)
                {
                    Delete(city);
                }
            }

            county.IsDeleted = true;

            uow.Counties.Update(county);
        }

        protected void Delete(Certification certification)
        {
            foreach(var requiredCertification in certification.RequiredCertifications)
            {
                Delete(requiredCertification.VolunteerPosition);
            }

            certification.IsDeleted = true;

            uow.Certifications.Update(certification);
        }
    }
}
