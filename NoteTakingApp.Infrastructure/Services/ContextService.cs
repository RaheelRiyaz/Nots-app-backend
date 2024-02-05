using Microsoft.AspNetCore.Http;
using NoteTakingApp.Application.Abstractions.IServices;
using NoteTakingApp.Infrastructure.AppClaims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NoteTakingApp.Infrastructure.Services
{
    public class ContextService : IContextService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public ContextService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<Guid> GetId()
        {
            var id = Guid.Empty;
            await Task.Run(() =>
            {
                var userId = httpContextAccessor?.HttpContext?.User.Claims.FirstOrDefault(_ => _.Type == AppClaim.Id)?.Value;
                id = userId is not null ? Guid.Parse(userId) : id;
            });

            return id;
        }
    }
}
