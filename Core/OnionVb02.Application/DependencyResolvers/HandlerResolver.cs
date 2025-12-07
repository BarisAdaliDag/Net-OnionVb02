using Microsoft.Extensions.DependencyInjection;
using OnionVb02.Application.CqrsAndMediatr.Mediator.Handlers.CategoryHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionVb02.Application.DependencyResolvers
{
  
    public static class HandlerResolver
    {
        public static void AddHandlerService(this IServiceCollection services)
        {
            //services.AddScoped<GetCategoryQueryHandler>();
            //services.AddScoped<GetCategoryByIdQueryHandler>();
            //services.AddScoped<CreateCategoryCommandHandler>();
            //services.AddScoped<UpdateCategoryCommandHandler>();
            //services.AddScoped<RemoveCategoryCommandHandler>();

            services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(CqrsAndMediatr.Mediator.Handlers.CategoryHandlers.GetCategoryQueryHandler).Assembly));
        }
    }
}
