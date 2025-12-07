using OnionVb02.Application.CqrsAndMediatr.Mediator.Queries.BaseQueries;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Results.OrderResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionVb02.Application.CqrsAndMediatr.Mediator.Queries.OrderQueries
{
    public class GetOrderByIdQuery : BaseQuery<GetOrderByIdQueryResult>
    {
        public int Id { get; set; }

        public GetOrderByIdQuery(int id)
        {
            Id = id;
        }
    }
}