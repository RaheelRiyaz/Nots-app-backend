using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTakingApp.Application.DTOS
{
    public record UserRequest(string Email,string Password);

    public record UserResponse(Guid Id, string Email);
    public record LoginRequest(string Email, string Password);
    public record LoginResponse(Guid Id, string Token);
}
