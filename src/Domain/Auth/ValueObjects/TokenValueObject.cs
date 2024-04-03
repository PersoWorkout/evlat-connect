using Domain.Abstract;
using System.Security.Cryptography;

namespace Domain.Auth.ValueObjects
{
    public class TokenValueObject : ValueObject
    {
        public string Value { get; init; }

        public TokenValueObject()
        {
            byte[] tokenData = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(tokenData);
            }

            Value = BitConverter.ToString(tokenData).Replace("-", "").ToLower();
        }

        public TokenValueObject(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
