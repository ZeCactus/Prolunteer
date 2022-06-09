using System;
using System.Collections.Generic;

namespace Prolunteer.Notifications.Email
{
    public class Email
    {
        public Email()
        {
            this.Recipients = new List<string>();
        }
        public List<string> Recipients { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
