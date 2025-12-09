using Microsoft.EntityFrameworkCore;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;
using OnionVb02.Domain.Enums;
using OnionVb02.Persistence.ContextClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionVb02.Persistence.RepositoryConcretes
{
    public class ProductAttributeRepository : BaseRepository<ProductAttribute>, IProductAttributeRepository
    {
        public ProductAttributeRepository(MyContext context) : base(context)
        {
        }

        public async Task<ProductAttribute?> GetByNameAsync(string attributeName)
        {
            return await _dbSet
                .Where(x => x.AttributeName == attributeName && x.Status != DataStatus.Deleted)
                .FirstOrDefaultAsync();
        }

        public async Task<List<ProductAttribute>> GetAllActiveAsync()
        {
            return await _dbSet
                .Where(x => x.Status != DataStatus.Deleted)
                .OrderBy(x => x.AttributeName)
                .ToListAsync();
        }
    }
}