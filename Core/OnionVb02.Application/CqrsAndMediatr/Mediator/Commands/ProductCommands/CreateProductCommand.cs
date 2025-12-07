using MediatR;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Commands.BaseCoomands;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Results.BaseResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionVb02.Application.CqrsAndMediatr.Mediator.Commands.ProductCommands
{
    public class CreateProductCommand : BaseCommand<BaseCommandResult>
    {
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int? CategoryId { get; set; }
    }
}