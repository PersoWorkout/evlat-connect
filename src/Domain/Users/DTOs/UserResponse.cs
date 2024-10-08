﻿namespace Domain.Users.DTOs
{
    public class UserResponse
    {
        public required string Id { get; set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public required string Username { get; set; }
        public required string Role { get; set; }
        public required string PhoneNumber { get; set; }
    }
}
