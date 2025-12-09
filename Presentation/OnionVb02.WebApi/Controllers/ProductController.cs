using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Commands.ProductCommands;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Queries.ProductQueries;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Results.ProductResults;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Results.BaseResults;
using OnionVb02.Application.DTOClasses;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;
using OnionVb02.Domain.Enums;

namespace OnionVb02.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IProductAttributeRepository _attributeRepo;
        private readonly IProductAttributeValueRepository _attributeValueRepo;

        public ProductController(
            IMediator mediator,
            IProductAttributeRepository attributeRepo,
            IProductAttributeValueRepository attributeValueRepo)
        {
            _mediator = mediator;
            _attributeRepo = attributeRepo;
            _attributeValueRepo = attributeValueRepo;
        }

      

        [HttpGet]
        public async Task<IActionResult> ProductList()
        {
            List<GetProductQueryResult> products = await _mediator.Send(new GetProductQuery());
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            GetProductByIdQueryResult value = await _mediator.Send(new GetProductByIdQuery(id));
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommand command)
        {
            BaseCommandResult result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommand command)
        {
            BaseCommandResult result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            BaseCommandResult result = await _mediator.Send(new RemoveProductCommand(id));
            return Ok(result);
        }

     

        [HttpGet("{id}/with-attributes")]
        public async Task<IActionResult> GetProductWithAttributes(int id)
        {
          
            var product = await _mediator.Send(new GetProductByIdQuery(id));
            if (product == null)
                return NotFound();

      
            var productAttrs = await _attributeValueRepo.GetByProductIdAsync(id);


            var result = new ProductWithAttributesDto
            {
                Id = product.Id,
                ProductName = product.ProductName,
                UnitPrice = product.UnitPrice,
                CategoryId = product.CategoryId,
              
                Attributes = productAttrs.ToDictionary(
            x => x.ProductAttribute.AttributeName,
            x => x.Value
        ),
                AttributeValues = productAttrs.Select(x => new ProductAttributeValueDto
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    ProductAttributeId = x.ProductAttributeId,
                    Value = x.Value,
                    AttributeName = x.ProductAttribute.AttributeName,
                    CreatedDate = x.CreatedDate,
                    UpdatedDate = x.UpdatedDate,
                    Status = x.Status
                }).ToList()
            };

            return Ok(result);
        }

        [HttpPost("{productId}/attributes")]
        public async Task<IActionResult> AddAttributeToProduct(int productId, [FromBody] AddProductAttributeRequest request)
        {
            // Product var mı kontrol et
            var product = await _mediator.Send(new GetProductByIdQuery(productId));
            if (product == null)
                return NotFound("Product not found");

            // Attribute var mı kontrol et
            var attribute = await _attributeRepo.GetByNameAsync(request.AttributeName);
            if (attribute == null)
                return NotFound($"Attribute '{request.AttributeName}' not found");

            // Zaten var mı kontrol et
            var existing = await _attributeValueRepo.GetByProductAndAttributeAsync(productId, attribute.Id);
            if (existing != null && existing.Status != DataStatus.Deleted)
                return BadRequest("This attribute already exists for this product");

            // Ekle
            var attributeValue = new ProductAttributeValue
            {
                ProductId = productId,
                ProductAttributeId = attribute.Id,
                Value = request.Value,
                CreatedDate = DateTime.Now,
                Status = DataStatus.Inserted
            };

            await _attributeValueRepo.CreateAsync(attributeValue);
            return CreatedAtAction(nameof(GetProductWithAttributes), new { id = productId }, attributeValue);
        }

        [HttpPut("{productId}/attributes/{attributeName}")]
        public async Task<IActionResult> UpdateProductAttribute(
            int productId,
            string attributeName,
            [FromBody] UpdateProductAttributeValueRequest request)
        {
            var attribute = await _attributeRepo.GetByNameAsync(attributeName);
            if (attribute == null)
                return NotFound($"Attribute '{attributeName}' not found");

            var oldAttributeValue = await _attributeValueRepo.GetByProductAndAttributeAsync(productId, attribute.Id);
            if (oldAttributeValue == null)
                return NotFound("Attribute value not found for this product");

            var newAttributeValue = oldAttributeValue;
            newAttributeValue.Value = request.NewValue;
            newAttributeValue.UpdatedDate = DateTime.Now;
            newAttributeValue.Status = DataStatus.Updated;

            await _attributeValueRepo.UpdateAsync(oldAttributeValue, newAttributeValue);
            return NoContent();
        }

        [HttpDelete("{productId}/attributes/{attributeName}")]
        public async Task<IActionResult> DeleteProductAttribute(int productId, string attributeName)
        {
            var attribute = await _attributeRepo.GetByNameAsync(attributeName);
            if (attribute == null)
                return NotFound($"Attribute '{attributeName}' not found");

            var attributeValue = await _attributeValueRepo.GetByProductAndAttributeAsync(productId, attribute.Id);
            if (attributeValue == null)
                return NotFound("Attribute value not found for this product");

            await _attributeValueRepo.DeleteAsync(attributeValue);
            return NoContent();
        }

        [HttpGet("search-by-attribute")]
        public async Task<IActionResult> SearchByAttribute([FromQuery] string attributeName, [FromQuery] string value)
        {
            if (string.IsNullOrWhiteSpace(attributeName) || string.IsNullOrWhiteSpace(value))
                return BadRequest("AttributeName and Value are required");

            var attribute = await _attributeRepo.GetByNameAsync(attributeName);
            if (attribute == null)
                return NotFound($"Attribute '{attributeName}' not found");

            var allValues = await _attributeValueRepo.GetByAttributeIdAsync(attribute.Id);
            var matchingProducts = allValues
                .Where(x => x.Value == value)
                .Select(x => x.ProductId)
                .Distinct()
                .ToList();

            var results = new List<ProductWithAttributesDto>();
            foreach (var productId in matchingProducts)
            {
                var product = await _mediator.Send(new GetProductByIdQuery(productId));
                if (product != null)
                {
                    var productAttrs = await _attributeValueRepo.GetByProductIdAsync(productId);
                    results.Add(new ProductWithAttributesDto
                    {
                        Id = product.Id,
                        ProductName = product.ProductName,
                        UnitPrice = product.UnitPrice,
                        CategoryId = product.CategoryId,
                        Attributes = productAttrs.ToDictionary(
                            x => x.ProductAttribute.AttributeName,
                            x => x.Value
                        )
                    });
                }
            }

            return Ok(results);
        }
    }

    // Request Models
    public class AddProductAttributeRequest
    {
        public string AttributeName { get; set; }
        public string Value { get; set; }
    }

    public class UpdateProductAttributeValueRequest
    {
        public string NewValue { get; set; }
    }
}