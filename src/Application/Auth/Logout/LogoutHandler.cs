using Domain.Abstract;
using Domain.Auth;
using MediatR;
using System.Net;

namespace Application.Auth.Logout
{
    public class LogoutHandler(IAuthRepository repository) : IRequestHandler<LogoutCommand, Result<object>>
    {
        private readonly IAuthRepository _repository = repository;

        public async Task<Result<object>> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var session = await _repository.GetByToken(request.Token);
            if (session is null)
                return Result<object>.Failure(
                    AuthErrors.SessionExpired,
                    HttpStatusCode.Unauthorized);

            await _repository.Delete(session);

            return Result<object>.Success(session);
        }
    }
}
