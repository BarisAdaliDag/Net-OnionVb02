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
    public class CreateAppUserProfileCommandHandler : IRequestHandler<CreateAppUserProfileCommand, BaseCommandResult>
    {
        private readonly IAppUserProfileRepository _repository;
        private readonly IAppUserRepository _appUserRepository;

        public CreateAppUserProfileCommandHandler(IAppUserProfileRepository repository, IAppUserRepository appUserRepository)
        {
            _repository = repository;
            _appUserRepository = appUserRepository;

        }
        public async Task<BaseCommandResult> Handle(CreateAppUserProfileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                
                AppUser appUser = await _appUserRepository.GetByIdAsync(request.AppUserId);

                if (appUser == null)
                {
                    return new BaseCommandResult
                    {
                        IsSuccess = false,
                        Message = "Belirtilen kullanıcı bulunamadı"
                    };
                }

               
                AppUserProfile appUserProfile = new AppUserProfile
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    AppUserId = request.AppUserId, 
                    CreatedDate = DateTime.Now,
                    Status = Domain.Enums.DataStatus.Inserted
                };

                await _repository.CreateAsync(appUserProfile);

                return new BaseCommandResult
                {
                    IsSuccess = true,
                    Message = "Kullanıcı profili başarıyla eklendi",
                    Id = appUserProfile.Id
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