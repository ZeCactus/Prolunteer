using Microsoft.EntityFrameworkCore;
using Prolunteer.BusinessLogic.Base;
using Prolunteer.BusinessLogic.Implementation.VolunteerPosition.Models;
using Prolunteer.BusinessLogic.Implementation.VolunteerPosition.Validations;
using Prolunteer.Common.Exceptions;
using Prolunteer.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolunteer.BusinessLogic.Implementation.VolunteerPosition
{
    public class VolunteerPositionService : BaseService
    {
        private readonly VolunteerPositionCreateModelValidator VolunteerPositionCreateModelValidator;
        public VolunteerPositionService(ServiceDependencies dependencies)
            : base(dependencies)
        {
            this.VolunteerPositionCreateModelValidator = new VolunteerPositionCreateModelValidator(dependencies.UnitOfWork);
        }

        public VolunteerPositionDetailsModel GetVolunteerPositionDetails(Guid id)
        {
            return uow.VolunteerPositions
                .Get()
                .Include(vp => vp.VolunteerParticipations)
                    .ThenInclude(vpa => vpa.User)
                .Where(vp => vp.Id == id)
                .Select(vp => Mapper.Map<VolunteerPositionDetailsModel>(vp))
                .FirstOrDefault();
        }

        public void AddVolunteerPosition(VolunteerPositionCreateModel model)
        {
            ExecuteInTransaction(uow =>
            {
                VolunteerPositionCreateModelValidator.Validate(model).ThenThrow();

                var TargetEvent = uow.Events.Get().Where(e => e.Id == model.EventId).FirstOrDefault();

                if (TargetEvent == null)
                {
                    throw new NotFoundErrorException();
                }

                if (TargetEvent.OrganizerId != CurrentUser.Id)
                {
                    throw new UnauthorizedAccessException();
                }

                var PositionToAdd = Mapper.Map<VolunteerPositionCreateModel, Entities.VolunteerPosition>(model);

                TargetEvent.VolunteerPositions.Add(PositionToAdd);

                uow.SaveChanges();
            });
        }

        public bool Enroll(Guid positionId)
        {
            return ExecuteInTransaction(uow =>
            {
                var positionQuery = uow.VolunteerPositions
                    .Get()
                    .Where(vp => vp.Id == positionId && !vp.IsDeleted)
                    .Where(vp => vp.VolunteerParticipations.Count() < vp.MaximumNrOfVolunteers
                                 && vp.RequiredCertifications
                                        .All(rc => rc.Certification
                                                        .UserCertifications
                                                        .Any(uc => uc.UserId == CurrentUser.Id && uc.Approved))
                    )
                    .Include(vp => vp.Event)
                        .ThenInclude(e => e.VolunteerPositions);
                var positionToEnrollInto = positionQuery.FirstOrDefault();
                if (positionToEnrollInto == null)
                {
                    return false;
                }

                positionToEnrollInto.VolunteerParticipations
                    .Add(new Entities.VolunteerParticipation
                    {
                        UserId = CurrentUser.Id
                    });

                uow.VolunteerPositions.Update(positionToEnrollInto);
                uow.SaveChanges();

                var eventFilled = positionToEnrollInto.Event
                    .VolunteerPositions
                    .All(vp => vp.VolunteerParticipations.Count() == vp.MaximumNrOfVolunteers);

                if (eventFilled)
                {
                    var filledEvent = positionQuery
                        .Include(vp => vp.Event.Organizer)
                        .Include(vp => vp.Event.Location)
                        .Select(vp => vp.Event)
                        .FirstOrDefault();

                    NotificationManager.SendEventFilledNotification(filledEvent);
                }
                else if(positionToEnrollInto.VolunteerParticipations.Count() == positionToEnrollInto.MaximumNrOfVolunteers)
                {
                    var filledPosition = positionQuery
                        .Include(vp => vp.Event.Organizer)
                        .Include(vp => vp.Event.Location)
                        .FirstOrDefault();

                    NotificationManager.SendPositionFilledNotification(filledPosition);
                }

                return true;
            });
        }

        public bool RemoveVolunteerPosition(Guid id)
        {
            var positionToRemove = uow.VolunteerPositions.Get()
                                      .Include(vp => vp.Event)
                                      .Where(vp => vp.Id == id)
                                      .FirstOrDefault();

            if(positionToRemove == null)
            {
                return false;
            }

            if(positionToRemove.Event.OrganizerId != CurrentUser.Id)
            {
                return false;
            }

            ExecuteInTransaction(uow =>
            {
                Delete(positionToRemove);
                uow.SaveChanges();
            });

            return true;
        }

    }
}
