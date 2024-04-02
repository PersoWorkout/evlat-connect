using Domain.Abstract;
using Domain.Users.ValueObjects;

namespace Domain.Users
{
    public class User : BaseEntity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public UserRole Role { get; set; }
        public PasswordValueObject Password { get; set; }
        public PhoneNumberValueObject PhoneNumber { get; set; }

        public void UpdateByUser(
            string? firstname = null,
            string? lastname = null,
            PasswordValueObject? password = null,
            PhoneNumberValueObject? phoneNumber = null)
        {
            if(!string.IsNullOrEmpty(firstname))
                Firstname = firstname;

            if (!string.IsNullOrEmpty(lastname))
                Lastname = lastname;

            if (password is not null)
                Password = password;

            if (phoneNumber is not null)
                PhoneNumber = phoneNumber;

            UpdatedAt = DateTime.Now;
        }

        public void UpdateByAdmin(
            string? firstname = null,
            string? lastname = null,
            PasswordValueObject? password = null,
            PhoneNumberValueObject? phoneNumber = null,
            string? username = null,
            UserRole? role = null)
        {
            if (!string.IsNullOrEmpty(username))
                Username = username;

            if (role.HasValue)
                Role = role.Value;

            UpdateByUser(
                firstname, 
                lastname, 
                password, 
                phoneNumber);
        }
    }
}
