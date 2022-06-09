
using Prolunteer.BusinessLogic.Base;
using Prolunteer.BusinessLogic.Implementation.NotificationTemplate.Models;
using Prolunteer.BusinessLogic.Implementation.NotificationTemplate.Validations;
using Prolunteer.Common.DTOs;
using Prolunteer.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prolunteer.BusinessLogic.Implementation.NotificationTemplate
{
    public class NotificationTemplateService : BaseService
    {
        private readonly NotificationTemplateEditModelValidator NotificationTemplateEditModelValidator;

        public NotificationTemplateService(ServiceDependencies dependencies)
            : base(dependencies) 
        {
            this.NotificationTemplateEditModelValidator = new NotificationTemplateEditModelValidator(dependencies.UnitOfWork);
        }

        public List<ListItemModel<int, string>> GetNotificationNamesAsListItemModelList()
        {
            return uow.NotificationTemplates
                .Get()
                .Select(nt => new ListItemModel<int, string>
                {
                    Value = nt.Id,
                    Text = nt.NotificationName
                })
                .ToList();
        }

        public NotificationTemplateEditModel GetNotificationTemplateForEdit(int id)
        {
            var entity = uow.NotificationTemplates
                .Get()
                .FirstOrDefault(nt => nt.Id == id);

            return Mapper.Map<NotificationTemplateEditModel>(entity);
        }

        public void EditNotificationTemplate(NotificationTemplateEditModel model)
        {
            NotificationTemplateEditModelValidator.Validate(model).ThenThrow();

            var entityToEdit = uow.NotificationTemplates
                .Get()
                .FirstOrDefault(nt => nt.Id == model.Id);

            entityToEdit.Template = model.Template;

            uow.SaveChanges();
        }
    }
}
