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
    public class ProductAttributeValueRepository : BaseRepository<ProductAttributeValue>, IProductAttributeValueRepository
    {
        public ProductAttributeValueRepository(MyContext context) : base(context)
        {
        }

        public async Task<List<ProductAttributeValue>> GetByProductIdAsync(int productId)
        {
            return await _dbSet
                .Include(x => x.ProductAttribute)
                .Where(x => x.ProductId == productId && x.Status != DataStatus.Deleted)
                .ToListAsync();
        }

        public async Task<List<ProductAttributeValue>> GetByAttributeIdAsync(int attributeId)
        {
            return await _dbSet
                .Include(x => x.Product)
                .Where(x => x.ProductAttributeId == attributeId && x.Status != DataStatus.Deleted)
                .ToListAsync();
        }

        public async Task<ProductAttributeValue?> GetByProductAndAttributeAsync(int productId, int attributeId)
        {
            return await _dbSet
                .Include(x => x.ProductAttribute)
                .Where(x => x.ProductId == productId
                         && x.ProductAttributeId == attributeId
                         && x.Status != DataStatus.Deleted)
                .FirstOrDefaultAsync();
        }
    }
}