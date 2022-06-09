using Microsoft.EntityFrameworkCore;
using Prolunteer.BusinessLogic.Base;
using Prolunteer.BusinessLogic.Implementation.City.Models;
using Prolunteer.BusinessLogic.Implementation.City.Validations;
using Prolunteer.Common.DTOs;
using Prolunteer.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prolunteer.BusinessLogic.Implementation.City
{
    public class CityService : BaseService
    {
        private readonly CityCreateModelValidator CityCreateModelValidator;
        public CityService(ServiceDependencies dependencies)
            : base(dependencies)
        {
            this.CityCreateModelValidator = new CityCreateModelValidator(dependencies.UnitOfWork);
        }

        public PaginationDTO<CityViewModel> GetCities(int pageNumber, int pageSize, string filter)
        {
            var cities = uow.Cities.Get();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                cities = cities.Where(c => c.Name.Contains(filter));
            }

            var elements = cities
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(c => c.County)
                .Select(c => Mapper.Map<Entities.City, CityViewModel>(c))
                .ToList();

            var count = cities.Count();

            return new PaginationDTO<CityViewModel>(elements, count);
        }

        public void AddCity(CityCreateModel model)
        {
            ExecuteInTransaction(uow =>
            {
                CityCreateModelValidator.Validate(model).ThenThrow();

                var entityToAdd = Mapper.Map<CityCreateModel, Entities.City>(model);

                uow.Cities.Insert(entityToAdd);
                uow.SaveChanges();
            });
        }

        public bool RemoveCity(Guid id)
        {
            return ExecuteInTransaction(uow =>
            {
                var entityToRemove = uow.Cities.Get().FirstOrDefault(c => c.Id == id);

                if (entityToRemove == null)
                {
                    return false;
                }

                Delete(entityToRemove);
                uow.SaveChanges();

                return true;
            });
        }

        public List<ListItemModel<Guid, string>> GetCitiesAsListItemModelList()
        {
            return uow.Cities.Get()
                   .Where(c => !c.IsDeleted)
                   .Select(c => new ListItemModel<Guid, string>
                   {
                       Value = c.Id,
                       Text = c.Name
                   })
                   .ToList();
        }

        public List<ListItemModel<Guid, string>> GetCitiesAsListItemModelList(Guid countyId)
        {
            return uow.Cities.Get()
                   .Where(c => c.CountyId == countyId && !c.IsDeleted)
                   .Select(c => new ListItemModel<Guid, string>
                   {
                       Value = c.Id,
                       Text = c.Name
                   })
                   .ToList();
        }
    }
}
