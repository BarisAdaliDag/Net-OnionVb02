using OnionVb02.Application.DTOInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionVb02.Application.DTOClasses
{
    public class ProductAttributeDto : BaseDto, IDto
    {
        public string AttributeName { get; set; }
        public string AttributeType { get; set; }
        public string? Description { get; set; }
    }
}
