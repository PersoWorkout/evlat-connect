using Domain.Abstract;

namespace Domain.Auth
{
    public static class AuthErrors
    {
        public static readonly Error UsernameEmpty =
            new("Username.EMpty", "'Username' must not be empty.");

        public static readonly Error InvalidCredentials = 
            new("Credentials.Invalid", 
                "The credentials are not valid");

        public static readonly Error SessionExpired =
            new("Session.Expired",
                "Your session was expired. Please sign in to continue");

        public static readonly Error NotAllowed =
            new("Ressource.NotAllowed",
                "You are not allowed to access this ressource");
    }
}
