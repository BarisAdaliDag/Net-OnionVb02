using OnionVb02.Application.CqrsAndMediatr.Mediator.Commands.BaseCoomands;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Results.BaseResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionVb02.Application.CqrsAndMediatr.Mediator.Commands.OrderCommands
{
    public class RemoveOrderCommand : BaseCommand<BaseCommandResult>
    {
        public int Id { get; set; }

        public RemoveOrderCommand(int id)
        {
            Id = id;
        }
    }
}