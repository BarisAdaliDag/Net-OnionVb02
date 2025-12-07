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
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, BaseCommandResult>
    {
        private readonly IProductRepository _repository;

        public UpdateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<BaseCommandResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
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

                value.ProductName = request.ProductName;
                value.UnitPrice = request.UnitPrice;
                value.CategoryId = request.CategoryId;
                value.UpdatedDate = DateTime.Now;
                value.Status = Domain.Enums.DataStatus.Updated;
                await _repository.SaveChangesAsync();

                return new BaseCommandResult
                {
                    IsSuccess = true,
                    Message = "Ürün başarıyla güncellendi",
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
