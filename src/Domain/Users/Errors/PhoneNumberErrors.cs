using Domain.Abstract;

namespace Domain.Users.Errors
{
    public static class PhoneNumberErrors
    {
        public static readonly Error Empty = new("PhoneNumber.Empty", "'Phone Number' must be not empty");
        public static readonly Error Invalid = new("PhoneNumber.Invalid", "'Phone Number' is not a valid phone number");
    }
}
