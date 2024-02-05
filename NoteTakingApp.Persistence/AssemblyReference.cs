using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NoteTakingApp.Application.Abstractions.IRepository;
using NoteTakingApp.Persistence.Data;
using NoteTakingApp.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTakingApp.Persistence
{
    public static class AssemblyReference
    {
        public static IServiceCollection AddPersisitenceServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<NoteTakingAppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(nameof(NoteTakingAppDbContext)));
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<INotesRepository, NotesRepository>();
            return services;
        }
    }
}
