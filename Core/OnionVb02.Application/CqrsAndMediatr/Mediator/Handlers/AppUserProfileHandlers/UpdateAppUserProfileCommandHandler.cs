using MediatR;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Commands.AppUserProfileCommands;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Results.BaseResults;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionVb02.Application.CqrsAndMediatr.Mediator.Handlers.AppUserProfileHandlers
{
    public class UpdateAppUserProfileCommandHandler : IRequestHandler<UpdateAppUserProfileCommand, BaseCommandResult>
    {
        private readonly IAppUserProfileRepository _repository;

        public UpdateAppUserProfileCommandHandler(IAppUserProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task<BaseCommandResult> Handle(UpdateAppUserProfileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                AppUserProfile value = await _repository.GetByIdAsync(request.Id);

                if (value == null)
                {
                    return new BaseCommandResult
                    {
                        IsSuccess = false,
                        Message = "Kullanıcı profili bulunamadı"
                    };
                }

                value.FirstName = request.FirstName;
                value.LastName = request.LastName;
                value.UpdatedDate = DateTime.Now;
                value.Status = Domain.Enums.DataStatus.Updated;
                await _repository.SaveChangesAsync();

                return new BaseCommandResult
                {
                    IsSuccess = true,
                    Message = "Kullanıcı profili başarıyla güncellendi",
                    Id = value.Id
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