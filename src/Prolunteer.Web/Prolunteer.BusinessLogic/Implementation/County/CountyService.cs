using Prolunteer.BusinessLogic.Base;
using Prolunteer.BusinessLogic.Implementation.County.Models;
using Prolunteer.BusinessLogic.Implementation.County.Validations;
using Prolunteer.Common.DTOs;
using Prolunteer.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolunteer.BusinessLogic.Implementation.County
{
    public class CountyService : BaseService
    {
        private readonly CountyCreateModelValidator CountyCreateModelValidator;
        public CountyService(ServiceDependencies dependencies)
            : base(dependencies)
        {
            CountyCreateModelValidator = new CountyCreateModelValidator(dependencies.UnitOfWork);
        }

        public PaginationDTO<CountyViewModel> GetCounties(int pageNumber, int pageSize, string filter)
        {
            var counties = uow.Counties.Get();
            if(!string.IsNullOrWhiteSpace(filter))
            {
                counties = counties.Where(c => c.Name.Contains(filter));
            }
            var elements = counties
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(c => Mapper.Map<Entities.County, CountyViewModel>(c))
                .ToList();
            var count = counties.Count();

            return new PaginationDTO<CountyViewModel>(elements, count);
        }

        public void AddCounty(CountyCreateModel model)
        {
            ExecuteInTransaction(uow =>
            {
                CountyCreateModelValidator.Validate(model).ThenThrow();

                var newCounty = Mapper.Map<CountyCreateModel, Entities.County>(model);

                uow.Counties.Insert(newCounty);
                uow.SaveChanges();
            });
        }

        public bool RemoveCounty(Guid id)
        {
            return ExecuteInTransaction(uow =>
            {
                var entityToRemove = uow.Counties.Get().FirstOrDefault(c => c.Id == id);

                if (entityToRemove == null)
                {
                    return false;
                }

                Delete(entityToRemove);
                uow.SaveChanges();
                return true;
            });
        }

        public List<ListItemModel<Guid, string>> GetCountiesAsListItemModelList()
        {
            return uow.Counties.Get()
                .Where(c => !c.IsDeleted)
                .Select(c => new ListItemModel<Guid, string>
                {
                    Value = c.Id,
                    Text = c.Name
                })
                .ToList();
        }
    }
}
