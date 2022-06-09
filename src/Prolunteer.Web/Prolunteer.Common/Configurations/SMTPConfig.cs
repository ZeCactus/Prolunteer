using System;

namespace Prolunteer.Common.Configurations
{
    public class SMTPConfig
    {
        public bool UseDefaultCredentials { get; set; }
        public bool EnableSsl { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
    }
}
