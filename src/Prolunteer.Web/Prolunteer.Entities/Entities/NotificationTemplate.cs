using Prolunteer.Common;
using System;

namespace Prolunteer.Entities
{
    public class NotificationTemplate : IEntity
    {
        public int Id { get; set; }
        public string NotificationName { get; set; }
        public string Subject { get; set; }
        public string Template { get; set; }
    }
}
