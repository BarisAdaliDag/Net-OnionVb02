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
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, BaseCommandResult>
    {
        private readonly IProductRepository _repository;

        public CreateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<BaseCommandResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Product product = new Product
                {
                    ProductName = request.ProductName,
                    UnitPrice = request.UnitPrice,
                    CategoryId = request.CategoryId,
                    CreatedDate = DateTime.Now,
                    Status = Domain.Enums.DataStatus.Inserted
                };

                await _repository.CreateAsync(product);

                return new BaseCommandResult
                {
                    IsSuccess = true,
                    Message = "Ürün başarıyla eklendi",
                    Id = product.Id
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