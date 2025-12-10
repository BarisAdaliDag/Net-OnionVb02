using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Queries.ProductQueries;
using OnionVb02.Application.DTOClasses;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;
using OnionVb02.Domain.Enums;
using OnionVb02.WebApi.RequestModels.ProductAttribute;

namespace OnionVb02.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAttributeManagementController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IProductAttributeRepository _attributeRepo;
        private readonly IProductAttributeValueRepository _attributeValueRepo;

        public ProductAttributeManagementController(
            IMediator mediator,
            IProductAttributeRepository attributeRepo,
            IProductAttributeValueRepository attributeValueRepo)
        {
            _mediator = mediator;
            _attributeRepo = attributeRepo;
            _attributeValueRepo = attributeValueRepo;
        }

        /// <summary>
        /// Ürünü tüm özellikleriyle getir
        /// GET /api/ProductAttributeManagement/product/1
        /// </summary>
        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetProductWithAttributes(int productId)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(productId));
            if (product == null)
                return NotFound("Product not found");

            var productAttrs = await _attributeValueRepo.GetByProductIdAsync(productId);

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

        /// <summary>
        /// Ürüne özellik ekle
        /// POST /api/ProductAttributeManagement/product/1/add
        /// </summary>
        /// 

        [HttpPost("product/{productId}/add")]
        public async Task<IActionResult> AddAttributeToProduct(int productId, [FromBody] AddProductAttributeRequest request)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(productId));
            if (product == null)
                return NotFound("Product not found");

            var attribute = await _attributeRepo.GetByNameAsync(request.AttributeName);
            if (attribute == null)
                return NotFound($"Attribute '{request.AttributeName}' not found");

            var existing = await _attributeValueRepo.GetByProductAndAttributeAsync(productId, attribute.Id);
            if (existing != null && existing.Status != DataStatus.Deleted)
                return BadRequest("This attribute already exists for this product");

            var attributeValue = new ProductAttributeValue
            {
                ProductId = productId,
                ProductAttributeId = attribute.Id,
                Value = request.Value,
                CreatedDate = DateTime.Now,
                Status = DataStatus.Inserted
            };

            await _attributeValueRepo.CreateAsync(attributeValue);

            // DTO'ya çevir - ✅ DÜZELTME
            var dto = new ProductAttributeValueDto
            {
                Id = attributeValue.Id,
                ProductId = attributeValue.ProductId,
                ProductAttributeId = attributeValue.ProductAttributeId,
                Value = attributeValue.Value,
                AttributeName = request.AttributeName,
                CreatedDate = attributeValue.CreatedDate,
                Status = attributeValue.Status
            };

            return CreatedAtAction(
                nameof(GetProductWithAttributes),
                new { productId },
                new { message = "Attribute added successfully", attribute = dto }
            );
        }


        /// <summary>
        /// Ürünün özelliğini güncelle
        /// PUT /api/ProductAttributeManagement/product/1/update/Color
        /// </summary>
        [HttpPut("product/{productId}/update/{attributeName}")]
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

            return Ok(new { message = "Attribute updated successfully" });
        }

        /// <summary>
        /// Ürünün özelliğini sil
        /// DELETE /api/ProductAttributeManagement/product/1/delete/Color
        /// </summary>
        [HttpDelete("product/{productId}/delete/{attributeName}")]
        public async Task<IActionResult> DeleteProductAttribute(int productId, string attributeName)
        {
            var attribute = await _attributeRepo.GetByNameAsync(attributeName);
            if (attribute == null)
                return NotFound($"Attribute '{attributeName}' not found");

            var attributeValue = await _attributeValueRepo.GetByProductAndAttributeAsync(productId, attribute.Id);
            if (attributeValue == null)
                return NotFound("Attribute value not found for this product");

            await _attributeValueRepo.DeleteAsync(attributeValue);

            return Ok(new { message = "Attribute deleted successfully" });
        }

        /// <summary>
        /// Belirli özelliğe sahip ürünleri ara
        /// GET /api/ProductAttributeManagement/search?attributeName=Color&value=Red
        /// </summary>
        [HttpGet("search")]
        public async Task<IActionResult> SearchByAttribute(
            [FromQuery] string attributeName,
            [FromQuery] string value)
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

        /// <summary>
        /// Ürünün tüm özelliklerini listele
        /// GET /api/ProductAttributeManagement/product/1/list
        /// </summary>
        [HttpGet("product/{productId}/list")]
        public async Task<IActionResult> GetProductAttributes(int productId)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(productId));
            if (product == null)
                return NotFound("Product not found");

            var productAttrs = await _attributeValueRepo.GetByProductIdAsync(productId);

            var result = productAttrs.Select(x => new ProductAttributeValueDto
            {
                Id = x.Id,
                ProductId = x.ProductId,
                ProductAttributeId = x.ProductAttributeId,
                Value = x.Value,
                AttributeName = x.ProductAttribute.AttributeName,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                Status = x.Status
            }).ToList();

            return Ok(result);
        }
    }




}