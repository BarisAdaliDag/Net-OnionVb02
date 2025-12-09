using OnionVb02.Application.DTOInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionVb02.Application.DTOClasses
{
    public class ProductAttributeValueDto : BaseDto, IDto
    {
        public int ProductId { get; set; }
        public int ProductAttributeId { get; set; }
        public string Value { get; set; }

        public string? AttributeName { get; set; }
        public string? ProductName { get; set; }
    }
}