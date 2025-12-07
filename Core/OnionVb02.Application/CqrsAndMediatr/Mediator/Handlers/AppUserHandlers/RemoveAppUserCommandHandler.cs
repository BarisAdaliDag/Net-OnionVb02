using MediatR;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Commands.AppUserCommands;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Results.BaseResults;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionVb02.Application.CqrsAndMediatr.Mediator.Handlers.AppUserHandlers
{
    public class RemoveAppUserCommandHandler : IRequestHandler<RemoveAppUserCommand, BaseCommandResult>
    {
        private readonly IAppUserRepository _repository;

        public RemoveAppUserCommandHandler(IAppUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<BaseCommandResult> Handle(RemoveAppUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                AppUser value = await _repository.GetByIdAsync(request.Id);

                if (value == null)
                {
                    return new BaseCommandResult
                    {
                        IsSuccess = false,
                        Message = "Kullanıcı bulunamadı"
                    };
                }

                await _repository.DeleteAsync(value);

                return new BaseCommandResult
                {
                    IsSuccess = true,
                    Message = "Kullanıcı başarıyla silindi",
                    Id = request.Id
                };
            }
            catch (Exception ex)
            {
                return new BaseCommandResult
                {
                    IsSuccess = false,
                    Message = $"Hata: {ex.Message}"
                };
            }
        }
    }
}
