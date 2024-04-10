using Domain.Abstract;
using Domain.Competences;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Competences.GetCompetences
{
    public class GetCompetencesQuery: 
        IRequest<Result<IEnumerable<Competence>>>
    {
    }
}
