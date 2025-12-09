using OnionVb02.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionVb02.Contract.RepositoryInterfaces
{
    public interface IProductAttributeValueRepository : IRepository<ProductAttributeValue>
    {
        Task<List<ProductAttributeValue>> GetByProductIdAsync(int productId);
        Task<List<ProductAttributeValue>> GetByAttributeIdAsync(int attributeId);
        Task<ProductAttributeValue?> GetByProductAndAttributeAsync(int productId, int attributeId);
    }
}
