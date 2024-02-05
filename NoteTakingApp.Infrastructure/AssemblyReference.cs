using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NoteTakingApp.Application.Abstractions.IServices;
using NoteTakingApp.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTakingApp.Infrastructure
{
    public static class AssemblyReference
    {
        public static IServiceCollection AddInfraStructureServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddSingleton<ITokenService>(new TokenService(configuration));
            services.AddScoped<IContextService, ContextService>();
            return services;
        }
    }
}
