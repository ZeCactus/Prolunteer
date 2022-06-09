using AutoMapper;
using Prolunteer.BusinessLogic.Implementation.VolunteerPosition.Models;
using Prolunteer.Common.DTOs;
using Prolunteer.DataAccess;
using System;
using System.Linq;

namespace Prolunteer.BusinessLogic.Implementation.VolunteerPosition.Mappings.CustomLogic
{
    public class AvailabilityResolver : IValueResolver<Entities.VolunteerPosition, AvailablePositionModel, bool>
    {
        private readonly UnitOfWork uow;
        private readonly CurrentUserDTO CurrentUser;
        public AvailabilityResolver(UnitOfWork uow, CurrentUserDTO currentUser)
        {
            this.uow = uow;
            this.CurrentUser = currentUser;
        }
        public bool Resolve(Entities.VolunteerPosition source, AvailablePositionModel destination, bool result, ResolutionContext context)
        {
            return uow.VolunteerPositions
                .Get()
                .Where(vp => vp.Id == source.Id && !vp.IsDeleted)
                .Any(vp => vp.VolunteerParticipations.Count() < vp.MaximumNrOfVolunteers && vp.RequiredCertifications
                                        .All(rc => rc.Certification
                                                    .UserCertifications
                                                    .Any(uc => uc.UserId == CurrentUser.Id && uc.Approved)
                                        )
                );
        }
    }
}
