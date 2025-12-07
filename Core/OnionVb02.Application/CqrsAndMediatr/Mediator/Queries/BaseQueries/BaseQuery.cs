using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionVb02.Application.CqrsAndMediatr.Mediator.Queries.BaseQueries
{
    public abstract class BaseQuery<TResponse> : IRequest<TResponse>
    {
    }
}