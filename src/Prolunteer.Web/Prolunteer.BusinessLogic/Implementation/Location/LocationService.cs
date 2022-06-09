using Microsoft.EntityFrameworkCore;
using Prolunteer.BusinessLogic.Base;
using Prolunteer.BusinessLogic.Implementation.Location.Models;
using Prolunteer.BusinessLogic.Implementation.Location.Validations;
using Prolunteer.Common.DTOs;
using Prolunteer.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolunteer.BusinessLogic.Implementation.Location
{
    public class LocationService : BaseService
    {
        private readonly LocationCreateModelValidator LocationCreateModelValidator;
        public LocationService(ServiceDependencies dependencies)
            : base(dependencies)
        {
            this.LocationCreateModelValidator = new LocationCreateModelValidator(dependencies.UnitOfWork);
        }

        public PaginationDTO<LocationViewModel> GetLocations(int pageNumber, int pageSize, string filter)
        {
            var locations = uow.Locations.Get();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                locations = locations.Where(l => l.Name.Contains(filter));
            }

            var elements = locations
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(l => l.City)
                    .ThenInclude(c => c.County)
                .Select(l => Mapper.Map<Entities.Location, LocationViewModel>(l))
                .ToList();

            var count = locations.Count();

            return new PaginationDTO<LocationViewModel>(elements, count);
        }

        public void AddLocation(LocationCreateModel model)
        {
            ExecuteInTransaction(uow =>
            {
                LocationCreateModelValidator.Validate(model).ThenThrow();

                var entityToAdd = Mapper.Map<LocationCreateModel, Entities.Location>(model);

                uow.Locations.Insert(entityToAdd);
                uow.SaveChanges();
            });
        }

        public bool RemoveLocation(Guid id)
        {
            return ExecuteInTransaction(uow =>
            {
                var entityToRemove = uow.Locations.Get().FirstOrDefault(l => l.Id == id);

                if(entityToRemove == null)
                {
                    return false;
                }

                Delete(entityToRemove);
                uow.SaveChanges();

                return true;
            });
        }

        public List<ListItemModel<Guid, string>> GetLocationsAsListItemModelList()
        {
            return uow.Locations.Get()
                .Where(c => !c.IsDeleted)
                .Select(l => new ListItemModel<Guid, string>
                {
                    Value = l.Id,
                    Text = l.Name
                })
                .ToList();
        }

        public List<ListItemModel<Guid, string>> GetLocationsAsListItemModelList(Guid cityId)
        {
            return uow.Locations.Get()
                   .Where(l => l.CityId == cityId && !l.IsDeleted)
                   .Select(l => new ListItemModel<Guid, string>
                   {
                       Value = l.Id,
                       Text = l.Name
                   })
                   .ToList();
        }
    }
}
