using System;
namespace Domain.Entities
{
    public class MailSettings
    {
        // Sender email address
        public string Mail { get; set; }

        // Display name
        public string DisplayName { get; set; }

        // Email password
        public string Password { get; set; }

        // Host provider
        public string Host { get; set; }

        // Port number
        public int Port { get; set; }
    }
}
