﻿namespace MagaEmailApi.Models
{
    public class UserDetails
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public string Email { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }
    }

}
