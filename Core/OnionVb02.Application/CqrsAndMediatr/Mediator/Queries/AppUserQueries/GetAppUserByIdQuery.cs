using MediatR;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Results.AppUserResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionVb02.Application.CqrsAndMediatr.Mediator.Queries.AppUserQueries
{
    public class GetAppUserByIdQuery : IRequest<GetAppUserByIdQueryResult>
    {
        public int Id { get; set; }

        public GetAppUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
