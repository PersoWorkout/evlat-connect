using Domain.Abstract;
using Domain.Subjects;
using MediatR;

namespace Application.Subjects.GetSubjectById
{
    public class GetSubjectByIdQuery(Guid id): IRequest<Result<Subject>>
    {
        public Guid Id { get; set; } = id;
    }
}
