using OnionVb02.Application.CqrsAndMediatr.Mediator.Commands.BaseCoomands;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Results.BaseResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionVb02.Application.CqrsAndMediatr.Mediator.Commands.AppUserProfileCommands
{
    public class CreateAppUserProfileCommand : BaseCommand<BaseCommandResult>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AppUserId { get; set; }
    }
}
