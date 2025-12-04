using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionVb02.Application.CqrsAndMediatr.Mediator.Commands.AppUserCommands
{
    //Todo : Ödev Command generic IRequest
    //Burada dikkat ettiyseniz Command'leriniz bir istekten sonra bir response döndürme amacında olmadıkları icinde generic olmayan IRequest tipinden miras almaktadır...Eger command'leriniz bir Result(response) döndürmek isteselerdi onlara ayrı class acılacaktı ve IRequest tipiniz de generic argüman olarak o class tipini alacaktı...
    public class CreateAppUserCommand : IRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
