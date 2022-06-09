using System;

namespace Prolunteer.BusinessLogic.Implementation.NotificationTemplate.Models
{
    public class NotificationTemplateEditModel
    {
        public int Id { get; set; }
        public string NotificationName { get; set; }
        public string Subject { get; set; }
        public string Template { get; set; }
    }
}
