using Domain.Abstract;
using Domain.Subjects;
using MediatR;
using System.Net;

namespace Application.Subjects.DeleteSubject
{
    public class DeleteSubjectAction(ISender sender)
    {
        private readonly ISender _sender = sender;

        public async Task<Result<object>> Execute(string id)
        {
            if (!Guid.TryParse(id, out var subjectId))
                return Result<object>.Failure(
                    SubjectErrors.NotFound(id),
                    HttpStatusCode.NotFound);

            return await _sender.Send(
                new DeleteSubjectCommand(subjectId));
        }
    }
}
