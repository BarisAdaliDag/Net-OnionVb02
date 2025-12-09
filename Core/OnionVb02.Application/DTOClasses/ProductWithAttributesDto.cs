using OnionVb02.Application.DTOInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionVb02.Application.DTOClasses
{
    public class ProductWithAttributesDto : BaseDto, IDto
    {
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public Dictionary<string, string> Attributes { get; set; } = new();
        public List<ProductAttributeValueDto> AttributeValues { get; set; } = new();
    }
}
