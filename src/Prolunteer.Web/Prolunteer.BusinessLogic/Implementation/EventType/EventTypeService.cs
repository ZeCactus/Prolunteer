using Prolunteer.BusinessLogic.Base;
using Prolunteer.BusinessLogic.Implementation.EventType.Models;
using Prolunteer.BusinessLogic.Implementation.EventType.Validations;
using Prolunteer.Common.DTOs;
using Prolunteer.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prolunteer.BusinessLogic.Implementation.EventType
{
    public class EventTypeService : BaseService
    {
        private readonly EventTypeCreateModelValidator EventTypeCreateModelValidator;
        public EventTypeService(ServiceDependencies dependencies)
            : base(dependencies)
        {
            this.EventTypeCreateModelValidator = new EventTypeCreateModelValidator(dependencies.UnitOfWork);
        }

        public PaginationDTO<EventTypeViewModel> GetEventTypes(int pageNumber, int pageSize, string filter)
        {
            var eventTypes = uow.EventTypes.Get();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                eventTypes = eventTypes.Where(et => et.Name.Contains(filter));
            }

            var elements = eventTypes
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(et => Mapper.Map<Entities.EventType, EventTypeViewModel>(et))
                .ToList();

            var count = eventTypes.Count();

            return new PaginationDTO<EventTypeViewModel>(elements, count);
        }

        public void AddEventType(EventTypeCreateModel model)
        {
            ExecuteInTransaction(uow =>
            {
                EventTypeCreateModelValidator.Validate(model).ThenThrow();

                var entityToAdd = Mapper.Map<Entities.EventType>(model);

                uow.EventTypes.Insert(entityToAdd);
                uow.SaveChanges();
            });
        }

        public bool RemoveEventType(int id)
        {
            return ExecuteInTransaction(uow =>
            {
                var entityToRemove = uow.EventTypes.Get().FirstOrDefault(et => et.Id == id);

                if (entityToRemove == null)
                {
                    return false;
                }

                Delete(entityToRemove);
                uow.SaveChanges();

                return true;
            });
        }

        public List<ListItemModel<int, string>> GetEventTypesAsListItemModelList()
        {
            return uow.EventTypes.Get()
                .Where(c => !c.IsDeleted)
                .Select(et => new ListItemModel<int, string>
                {
                    Value = et.Id,
                    Text = et.Name
                })
                .ToList();
        }
    }
}
