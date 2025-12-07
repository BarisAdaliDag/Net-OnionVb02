using OnionVb02.Application.CqrsAndMediatr.Mediator.Queries.BaseQueries;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Results.ProductResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionVb02.Application.CqrsAndMediatr.Mediator.Queries.ProductQueries
{
    public class GetProductQuery : BaseQuery<List<GetProductQueryResult>>
    {
    }
}