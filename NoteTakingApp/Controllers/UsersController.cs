using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteTakingApp.Application.Abstractions.IServices;
using NoteTakingApp.Application.APIResponse;
using NoteTakingApp.Application.DTOS;

namespace NoteTakingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }



        [HttpPost("signup")]
        public async Task<APIRESPONSE<UserResponse>> Signup(UserRequest model)
        {
            return await userService.Signup(model);
        }



        [HttpPost("login")]
        public async Task<APIRESPONSE<LoginResponse>> Login(LoginRequest model)
        {
            return await userService.Login(model);
        }
    }
}
