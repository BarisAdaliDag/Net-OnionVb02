using OnionVb02.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionVb02.Contract.RepositoryInterfaces
{
    public interface IProductAttributeRepository : IRepository<ProductAttribute>
    {
        Task<ProductAttribute?> GetByNameAsync(string attributeName);
        Task<List<ProductAttribute>> GetAllActiveAsync();
    }
}
