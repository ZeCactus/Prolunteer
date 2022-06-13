using Microsoft.EntityFrameworkCore;
using Prolunteer.BusinessLogic.Base;
using Prolunteer.BusinessLogic.Implementation.Event.Models;
using Prolunteer.BusinessLogic.Implementation.Event.Validations;
using Prolunteer.Common.Extensions;
using Prolunteer.Entities;
using Prolunteer.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolunteer.BusinessLogic.Implementation.Event
{
    public class EventService : BaseService
    {
        private readonly EventCreateModelValidator EventCreateModelValidator;
        
        public EventService(ServiceDependencies serviceDependencies)
            :base(serviceDependencies)
        {
            this.EventCreateModelValidator = new EventCreateModelValidator(serviceDependencies.UnitOfWork);
        }

        public PaginationDTO<EventVM> GetEvents(int pageNumber, int pageSize, string filter)
        {
            var events = uow.Events.Get()
                .Where(e => !e.IsDeleted && e.OrganizerId == CurrentUser.Id);

            if (!string.IsNullOrWhiteSpace(filter))
            {
                events = events.Where(e => e.Name.Contains(filter) || e.Description.Contains(filter) || e.Location.Name.Contains(filter));
            }

            var elements = events
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(e => e.Organizer)
                .Include(e => e.EventType)
                .Select(e => Mapper.Map<Entities.Event, EventVM>(e))
                .ToList();

            var count = events.Count();

            return new PaginationDTO<EventVM>(elements, count);
        }

        public PaginationDTO<VolunteerEventVM> GetEnrolledEvents(int pageNumber, int pageSize, string filter)
        {
            var enrolledPositions = uow.VolunteerParticipations.Get()
                .Where(vp => vp.UserId == CurrentUser.Id && !vp.VolunteerPosition.Event.IsDeleted)
                .Include(vp => vp.VolunteerPosition.Event.Organizer)
                .Include(vp => vp.VolunteerPosition.Event.Location)
                .Include(vp => vp.VolunteerPosition.Event.EventType)
                .Select(vp => vp.VolunteerPosition);

            if (!string.IsNullOrWhiteSpace(filter))
            {
                enrolledPositions = enrolledPositions.Where(vp => vp.Event.Name.Contains(filter) || vp.Event.Description.Contains(filter) || vp.Event.Location.Name.Contains(filter));
            }

            var elements = enrolledPositions
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(vp => Mapper.Map<Entities.VolunteerPosition, VolunteerEventVM>(vp))
                .ToList();

            var count = enrolledPositions.Count();

            return new PaginationDTO<VolunteerEventVM>(elements, count);
        }
        public List<EventVM> GetEvents(Guid OrganizerId)
        {
            return uow.Events.Get()
                            .Where(e => e.OrganizerId == OrganizerId && e.StartDate.Date >= DateTime.Now.Date && !e.IsDeleted)
                            .Include(e => e.Organizer)
                            .Include(e => e.EventType)
                            .Include(e => e.Location)
                            .Select(e => Mapper.Map<Entities.Event, EventVM>(e))
                            .ToList();
        }
        public void CreateEvent(EventCreateModel model)
        {
            ExecuteInTransaction(uow =>
            {
                EventCreateModelValidator.Validate(model).ThenThrow();
                var newEvent = Mapper.Map<EventCreateModel, Entities.Event>(model);
                uow.Events.Insert(newEvent);
                uow.SaveChanges();
            });
        }

        public bool RemoveEvent(Guid id)
        {
            var eventToRemove = uow.Events.Get()
                .Where(e => e.Id == id)
                .Include(e => e.VolunteerPositions)
                    .ThenInclude(e => e.VolunteerParticipations)
                .Include(e => e.Organizer)
                .Include(e => e.EventType)
                .Include(e => e.Location)
                .FirstOrDefault();

            if (eventToRemove == null)
            {
                return false;
            }

            if(eventToRemove.OrganizerId != CurrentUser.Id)
            {
                return false;
            }

            ExecuteInTransaction(uow =>
            {
                Delete(eventToRemove);
                uow.SaveChanges();
            });

            return true;
        }

        public PaginationDTO<EventVM> GetAvailableEvents(int pageNumber, int pageSize, string filter)
        {
            var events = uow.Events
                .Get()
                .Where(e => e.StartDate.Date >= DateTime.Now.AddYears(-1).Date && !e.IsDeleted);

            if (!string.IsNullOrWhiteSpace(filter))
            {
                events = events.Where(e => e.Name.Contains(filter)
                                        || e.Description.Contains(filter)
                                        || e.Organizer.FirstName.Contains(filter) || e.Organizer.LastName.Contains(filter)
                                        || e.Location.Name.Contains(filter));
            }

            events = events
                .Where(e => !uow.VolunteerParticipations
                            .Get()
                            .Where(vp => vp.UserId == CurrentUser.Id)
                            .Select(vp => vp.VolunteerPosition.Event)
                            .Any(ie => ie.StartDate <= e.EndDate && ie.EndDate >= e.StartDate)
                )
                .Where(e => e.VolunteerPositions
                            .Any(vp => vp.VolunteerParticipations.Count() < vp.MaximumNrOfVolunteers && vp.RequiredCertifications
                                        .All(rc => rc.Certification
                                                    .UserCertifications
                                                    .Any(uc => uc.UserId == CurrentUser.Id && uc.Approved)
                                        )
                            )
                );
            var test = events.ToQueryString();
            var elements = events
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(e => e.Organizer)
                .Include(e => e.EventType)
                .Include(e => e.Location)
                .Select(e => Mapper.Map<EventVM>(e))
                .ToList();

            var count = events.Count();

            return new PaginationDTO<EventVM>(elements, count);

        }

        public Entities.Event GetAvailableEvent()
        {
            var events = uow.Events
                .Get()
                .Where(e => e.StartDate.Date >= DateTime.Now.Date && !e.IsDeleted);

            return events
                .Where(e => !uow.VolunteerParticipations
                            .Get()
                            .Where(vp => vp.UserId == CurrentUser.Id)
                            .Select(vp => vp.VolunteerPosition.Event)
                            .Any(ie => ie.StartDate <= e.EndDate && ie.EndDate >= e.StartDate)
                )
                .Where(e => e.VolunteerPositions
                            .Any(vp => vp.VolunteerParticipations.Count() < vp.MaximumNrOfVolunteers && vp.RequiredCertifications
                                        .All(rc => rc.Certification
                                                    .UserCertifications
                                                    .Any(uc => uc.UserId == CurrentUser.Id && uc.Approved)
                                        )
                            )
                )
                .OrderBy(e => Guid.NewGuid())
                .FirstOrDefault();
        }

        public AvailableEventDetailsModel GetAvailableEventDetails(Guid eventId)
        {
            var eventToMap = uow.Events
                .Get()
                .Where(e => e.Id == eventId && !e.IsDeleted)
                .Include(e => e.VolunteerPositions)
                    .ThenInclude(vp => vp.RequiredCertifications)
                        .ThenInclude(rc => rc.Certification)
                .Include(e => e.VolunteerPositions)
                    .ThenInclude(vp => vp.VolunteerParticipations)
                .Include(e => e.Location)
                    .ThenInclude(l => l.City)
                        .ThenInclude(c => c.County)
                .Include(e => e.EventType)
                .Include(e => e.Organizer)
                .FirstOrDefault();
            return Mapper.Map<AvailableEventDetailsModel>(eventToMap);
        }

        public Entities.VolunteerPosition GetAvailablePosition(Guid eventId, Guid userId)
        {
            var eventToMap = uow.Events
                .Get()
                .Where(e => e.Id == eventId && !e.IsDeleted)
                .Include(e => e.VolunteerPositions)
                    .ThenInclude(vp => vp.RequiredCertifications)
                .FirstOrDefault();

            return uow.VolunteerPositions
                .Get()
                .Where(vp => vp.EventId == eventToMap.Id && !vp.IsDeleted)
                .Include(vp => vp.RequiredCertifications)
                .Include(vp => vp.VolunteerParticipations)
                .Where(vp => vp.VolunteerParticipations.Count() < vp.MaximumNrOfVolunteers && vp.RequiredCertifications
                                        .All(rc => rc.Certification
                                                    .UserCertifications
                                                    .Any(uc => uc.UserId == userId && uc.Approved)
                                        )
                )
                .OrderBy(vp => Guid.NewGuid())
                .FirstOrDefault();
        }

        public EventDetailsVM GetEventDetails(Guid id)
        {
            return uow.Events.Get()
                            .Include(e => e.Organizer)
                            .Include(e => e.VolunteerPositions)
                                .ThenInclude(vp => vp.VolunteerParticipations)
                            .Include(e => e.EventType)
                            .Include(e => e.Location)
                                .ThenInclude(l => l.City)
                                    .ThenInclude(c => c.County)
                            .Where(e => e.Id == id)
                            .Select(e => Mapper.Map<Entities.Event, EventDetailsVM>(e))
                            .FirstOrDefault();
        }
    }
}
