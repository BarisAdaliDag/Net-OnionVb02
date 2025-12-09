using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionVb02.Domain.Entities
{
    public class ProductAttributeValue : BaseEntity
    {
        public int ProductId { get; set; }
        public int ProductAttributeId { get; set; }
        public string Value { get; set; }

        // Relational Properties
        public virtual Product Product { get; set; }
        public virtual ProductAttribute ProductAttribute { get; set; }
    }
}