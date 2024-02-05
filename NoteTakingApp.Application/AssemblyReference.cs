using Microsoft.Extensions.DependencyInjection;
using NoteTakingApp.Application.Abstractions.IServices;
using NoteTakingApp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NoteTakingApp.Application
{
    public static class AssemblyReference
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,string webRootPath)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INotesService, NotesService>();
            services.AddSingleton<IStorageService>(new StorageService(webRootPath));
            return services;
        }   
    }
}
