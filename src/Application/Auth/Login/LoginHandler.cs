using Domain.Abstract;
using Domain.Auth;
using MediatR;

namespace Application.Auth.Login
{
    public class LoginHandler(
        IAuthRepository repostory) : IRequestHandler<LoginCommand, Result<Session>>
    {
        private readonly IAuthRepository _repostory = repostory;

        public async Task<Result<Session>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _repostory.GetUserByUsername(
                request.Username);

            if (user is null)
                return Result<Session>.Failure(
                    AuthErrors.InvalidCredentials);

            var validation = await _repostory.VerifyCredentials(
                request.Username,
                request.Password);

            if (!validation)
                return Result<Session>.Failure(
                    AuthErrors.InvalidCredentials);

            var session = new Session(user.Id);
            await _repostory.Create(session);

            return Result<Session>.Success(session);
        }
    }
}
