using Domain.Abstract;
using Domain.Users.ValueObjects;

namespace Domain.Users
{
    public class User(
        string firstname,
        string lastname,
        string username,
        UserRole role,
        PasswordValueObject password,
        PhoneNumberValueObject phoneNumber) : BaseEntity()
    {
        public string Firstname { get; set; } = firstname;
        public string Lastname { get; set; } = lastname;
        public string Username { get; set; } = username;
        public UserRole Role { get; set; } = role;
        public PasswordValueObject Password { get; set; } = password;
        public PhoneNumberValueObject PhoneNumber { get; set; } = phoneNumber;

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
