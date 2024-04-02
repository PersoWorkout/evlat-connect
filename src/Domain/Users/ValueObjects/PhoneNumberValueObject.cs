using Domain.Abstract;
using Domain.Users.Errors;
using System.Text.RegularExpressions;

namespace Domain.Users.ValueObjects
{
    public partial class PhoneNumberValueObject : ValueObject
    {
        private const string PhoneNumberRegexPattern =
            @"^(?:\+?33|0)[1-9](?:\d{2}){4}$";

        [GeneratedRegex(PhoneNumberRegexPattern, RegexOptions.None)]
        private static partial Regex MyRegex();

        public string Value { get; set; }

        private PhoneNumberValueObject(string value)
        {
            Value = value;
        }

        public static Result<PhoneNumberValueObject> Create(string value)
        {
            if (string.IsNullOrEmpty(value))
                return Result<PhoneNumberValueObject>.Failure(
                    PhoneNumberErrors.Empty);

            if(!MyRegex().IsMatch(value))
                return Result<PhoneNumberValueObject>.Failure(
                    PhoneNumberErrors.Invalid);

            return Result<PhoneNumberValueObject>.Success(
                new PhoneNumberValueObject(value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}