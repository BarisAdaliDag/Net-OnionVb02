using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionVb02.Application.CqrsAndMediatr.Mediator.Results.BaseResults
{
    public class BaseCommandResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int? Id { get; set; }
    }
}
