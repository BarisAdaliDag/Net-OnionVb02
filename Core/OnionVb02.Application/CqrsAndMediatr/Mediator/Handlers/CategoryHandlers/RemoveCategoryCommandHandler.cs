using MediatR;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Commands.CategoryCommands;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Results.BaseResults;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionVb02.Application.CqrsAndMediatr.Mediator.Handlers.CategoryHandlers
{
    public class RemoveCategoryCommandHandler : IRequestHandler<RemoveCategoryCommand, BaseCommandResult>
    {
        private readonly ICategoryRepository _repository;

        public RemoveCategoryCommandHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<BaseCommandResult> Handle(RemoveCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Category value = await _repository.GetByIdAsync(request.Id);

                if (value == null)
                {
                    return new BaseCommandResult
                    {
                        IsSuccess = false,
                        Message = "Kategori bulunamadı"
                    };
                }

                await _repository.DeleteAsync(value);

                return new BaseCommandResult
                {
                    IsSuccess = true,
                    Message = "Kategori başarıyla silindi",
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
