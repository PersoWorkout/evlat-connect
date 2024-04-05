using Domain.Abstract;
using Domain.Subjects;
using MediatR;

namespace Application.Subjects.GetSubjects
{
    public class GetSubjectsQuery: IRequest<Result<IEnumerable<Subject>>>
    {
    }
}
