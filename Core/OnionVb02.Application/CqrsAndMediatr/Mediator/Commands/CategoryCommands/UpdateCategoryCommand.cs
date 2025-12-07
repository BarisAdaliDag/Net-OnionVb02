using OnionVb02.Application.CqrsAndMediatr.Mediator.Commands.BaseCoomands;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Results.BaseResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionVb02.Application.CqrsAndMediatr.Mediator.Commands.CategoryCommands
{
    public class UpdateCategoryCommand : BaseCommand<BaseCommandResult>
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}