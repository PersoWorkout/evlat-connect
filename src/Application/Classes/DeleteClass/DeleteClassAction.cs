using Domain.Abstract;
using Domain.Classes.Errors;
using MediatR;
using System.Net;

namespace Application.Classes.DeleteClass
{
    public class DeleteClassAction(ISender sender)
    {
        private readonly ISender _sender = sender;

        public async Task<Result<object>> Execute(string id)
        {
            if (!Guid.TryParse(id, out var classId))
                return Result<object>.Failure(
                    ClassErrors.ClassNotFound(id),
                    HttpStatusCode.NotFound);

            return await _sender.Send(new DeleteClassCommand(classId));
        }
    }
}
