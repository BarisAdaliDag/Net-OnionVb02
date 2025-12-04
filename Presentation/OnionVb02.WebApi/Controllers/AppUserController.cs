using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Commands.AppUserCommands;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Queries.AppUserQueries;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Results.AppUserResults;

namespace OnionVb02.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppUserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> AppUserList()
        {
            List<GetAppUserQueryResult> appUsers = await _mediator.Send(new GetAppUserQuery());
            return Ok(appUsers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppUser(int id)
        {
            GetAppUserByIdQueryResult value = await _mediator.Send(new GetAppUserByIdQuery(id));
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppUser(CreateAppUserCommand command)
        {
            await _mediator.Send(command);
            return Ok("Veri eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAppUser(UpdateAppUserCommand command)
        {
            await _mediator.Send(command);
            return Ok("Veri güncellendi");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAppUser(int id)
        {
            await _mediator.Send(new RemoveAppUserCommand(id));
            return Ok("Veri Silindi");
        }
    }
}