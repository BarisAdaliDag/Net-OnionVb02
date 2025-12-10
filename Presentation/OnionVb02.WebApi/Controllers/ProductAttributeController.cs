using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnionVb02.Application.DTOClasses;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;
using OnionVb02.Domain.Enums;
using OnionVb02.WebApi.RequestModels.ProductAttribute;

namespace OnionVb02.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAttributeController : ControllerBase
    {
        private readonly IProductAttributeRepository _attributeRepo;
        private readonly IMapper _mapper;

        public ProductAttributeController(
            IProductAttributeRepository attributeRepo,
            IMapper mapper)
        {
            _attributeRepo = attributeRepo;
            _mapper = mapper;
        }

     
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var attributes = await _attributeRepo.GetAllActiveAsync();
            var dtos = _mapper.Map<List<ProductAttributeDto>>(attributes);
            return Ok(dtos);
        }

      
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var attribute = await _attributeRepo.GetByIdAsync(id);
            if (attribute == null || attribute.Status == DataStatus.Deleted)
                return NotFound();

            var dto = _mapper.Map<ProductAttributeDto>(attribute);
            return Ok(dto);
        }

        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductAttributeRequest request)
        {
            var existing = await _attributeRepo.GetByNameAsync(request.AttributeName);
            if (existing != null)
                return BadRequest($"Attribute '{request.AttributeName}' already exists");

            var attribute = new ProductAttribute
            {
                AttributeName = request.AttributeName,
                AttributeType = request.AttributeType,
                Description = request.Description,
                CreatedDate = DateTime.Now,
                Status = DataStatus.Inserted
            };

            await _attributeRepo.CreateAsync(attribute);
            var dto = _mapper.Map<ProductAttributeDto>(attribute);

            return CreatedAtAction(nameof(GetById), new { id = attribute.Id }, dto);
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductAttributeRequest request)
        {
            var oldAttribute = await _attributeRepo.GetByIdAsync(id);
            if (oldAttribute == null || oldAttribute.Status == DataStatus.Deleted)
                return NotFound();

            var newAttribute = oldAttribute;
            newAttribute.Description = request.Description;
            newAttribute.UpdatedDate = DateTime.Now;
            newAttribute.Status = DataStatus.Updated;

            await _attributeRepo.UpdateAsync(oldAttribute, newAttribute);
            return NoContent();
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var attribute = await _attributeRepo.GetByIdAsync(id);
            if (attribute == null)
                return NotFound();

            await _attributeRepo.DeleteAsync(attribute);
            return NoContent();
        }
    }

   

  
}