using MediatR;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Commands.ProductCommands;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Results.BaseResults;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionVb02.Application.CqrsAndMediatr.Mediator.Handlers.ProductHandlers
{
    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommand, BaseCommandResult>
    {
        private readonly IProductRepository _repository;

        public RemoveProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<BaseCommandResult> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Product value = await _repository.GetByIdAsync(request.Id);

                if (value == null)
                {
                    return new BaseCommandResult
                    {
                        IsSuccess = false,
                        Message = "Ürün bulunamadı"
                    };
                }

                await _repository.DeleteAsync(value);

                return new BaseCommandResult
                {
                    IsSuccess = true,
                    Message = "Ürün başarıyla silindi",
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
