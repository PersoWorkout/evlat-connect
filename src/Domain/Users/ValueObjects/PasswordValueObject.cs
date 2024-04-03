using Domain.Abstract;
using Domain.Users.Errors;
using System.Text.RegularExpressions;

namespace Domain.Users.ValueObjects
{
    public partial class PasswordValueObject : ValueObject
    {
        private const string PasswordRegexPattern =
            @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[~`!@#\$%\^&\*\(\)_\-\+=\{\[\}\]\|\\:;'<,>\.\?\/]).{8,}$";

        [GeneratedRegex(PasswordRegexPattern, RegexOptions.None, 250)]
        private static partial Regex MyRegex();

        public string Value { get; init; }

        private PasswordValueObject(string value)
        {
            Value = value;
        }

        public static Result<PasswordValueObject> Create(string value)
        {
            if (string.IsNullOrEmpty(value))
                return Result<PasswordValueObject>.Failure(
                    PasswordErrors.Empty);

            if (!MyRegex().IsMatch(value))
                return Result<PasswordValueObject>.Failure(
                    PasswordErrors.Invalid);
                

            return Result<PasswordValueObject>
                .Success(new PasswordValueObject(value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
