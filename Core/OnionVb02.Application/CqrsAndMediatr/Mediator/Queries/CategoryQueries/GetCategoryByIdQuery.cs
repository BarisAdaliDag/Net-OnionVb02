
using OnionVb02.Application.CqrsAndMediatr.Mediator.Queries.BaseQueries;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Results.CategoryResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionVb02.Application.CqrsAndMediatr.Mediator.Queries.CategoryQueries
{
    public class GetCategoryByIdQuery : BaseQuery<GetCategoryByIdQueryResult>
    {
        public int Id { get; set; }

        public GetCategoryByIdQuery(int id)
        {
            Id = id;
        }
    }
}

