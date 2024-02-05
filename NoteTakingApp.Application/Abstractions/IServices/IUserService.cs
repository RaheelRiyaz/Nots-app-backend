using NoteTakingApp.Application.APIResponse;
using NoteTakingApp.Application.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTakingApp.Application.Abstractions.IServices
{
    public interface IUserService
    {
        Task<APIRESPONSE<UserResponse>> Signup(UserRequest model);
        Task<APIRESPONSE<LoginResponse>> Login(LoginRequest model);
    }
}
