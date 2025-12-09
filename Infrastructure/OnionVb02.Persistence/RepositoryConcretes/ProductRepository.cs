using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;
using OnionVb02.Persistence.ContextClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionVb02.Persistence.RepositoryConcretes
{
    public class ProductRepository(MyContext context) : BaseRepository<Product>(context),IProductRepository
    {


    }
}
