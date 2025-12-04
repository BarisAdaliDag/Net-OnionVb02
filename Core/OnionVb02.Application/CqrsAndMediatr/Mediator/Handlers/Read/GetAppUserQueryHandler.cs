using MediatR;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Queries.AppUserQueries;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Results.AppUserResults;
using OnionVb02.Contract.RepositoryInterfaces;
using OnionVb02.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionVb02.Application.CqrsAndMediatr.Mediator.Handlers.Read
{
    public class GetAppUserQueryHandler : IRequestHandler<GetAppUserQuery, List<GetAppUserQueryResult>>
    {
        private readonly IAppUserRepository _repository;

        public GetAppUserQueryHandler(IAppUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetAppUserQueryResult>> Handle(GetAppUserQuery request, CancellationToken cancellationToken)
        {
            List<AppUser> values = await _repository.GetAllAsync();
            return values.Select(x => new GetAppUserQueryResult
            {
                Id = x.Id,
                Password = x.Password,
                UserName = x.UserName
            }).ToList();
        }
    }
}
