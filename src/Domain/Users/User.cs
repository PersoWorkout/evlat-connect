using Domain.Abstract;
using Domain.Classes;
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

        public Guid? ClassId { get; set; }
        public Class? CLass { get; set; }
        public List<Class>? Classes { get; set; }

        public void Update(
            string? firstname = null,
            string? lastname = null,
            string? username = null,
            PasswordValueObject? password = null,
            PhoneNumberValueObject? phoneNumber = null,
            Guid? classId = null)
        {
            if(!string.IsNullOrEmpty(firstname))
                Firstname = firstname;

            if (!string.IsNullOrEmpty(lastname))
                Lastname = lastname;

            if (!string.IsNullOrEmpty(username))
                Username = username;

            if (password is not null)
                Password = password;

            if (phoneNumber is not null)
                PhoneNumber = phoneNumber;

            if(classId.HasValue)
                ClassId = classId.Value;

            UpdatedAt = DateTime.Now;
        }
    }
}
