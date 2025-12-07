using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionVb02.Application.CqrsAndMediatr.Mediator.Commands.BaseCoomands
{
    public abstract class BaseCommand<TResponse> : IRequest<TResponse>
    {
    }
}
