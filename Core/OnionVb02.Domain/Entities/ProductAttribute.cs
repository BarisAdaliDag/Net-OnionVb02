using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionVb02.Domain.Entities
{
    public class ProductAttribute : BaseEntity
    {
        public string AttributeName { get; set; }
        public string AttributeType { get; set; }
        public string? Description { get; set; }

        // Relational Properties
        public virtual ICollection<ProductAttributeValue> ProductAttributeValues { get; set; }
    }
}
