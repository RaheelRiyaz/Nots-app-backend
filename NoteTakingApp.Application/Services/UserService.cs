using AutoMapper;
using NoteTakingApp.Application.Abstractions.IRepository;
using NoteTakingApp.Application.Abstractions.IServices;
using NoteTakingApp.Application.APIResponse;
using NoteTakingApp.Application.DTOS;
using NoteTakingApp.Application.Utilis;
using NoteTakingApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTakingApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly ITokenService tokenService;

        public UserService(IUserRepository userRepository, IMapper mapper, ITokenService tokenService)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.tokenService = tokenService;
        }

        public async Task<APIRESPONSE<LoginResponse>> Login(LoginRequest model)
        {
            var user = await userRepository.FirstOrDefaultAsync(_ => _.Email == model.Email);

            if (user is null)
                return APIRESPONSE<LoginResponse>.ErrorResponse("Invalid credentials");

            if (!AppEncryption.ComparePassword(user.Password, model.Password, user.Salt))
                return APIRESPONSE<LoginResponse>.ErrorResponse("Invalid credentials");

            var response = new LoginResponse(
               user.Id,
              tokenService.GnerateToken(user)
              );

            return APIRESPONSE<LoginResponse>.SuccessResponse(response,message: "Logged in successfully");
        }




        public async Task<APIRESPONSE<UserResponse>> Signup(UserRequest model)
        {
            var emailExists = await userRepository.IsExistsAsync(_ => _.Email == model.Email);

            if (emailExists) return APIRESPONSE<UserResponse>.ErrorResponse(message: "Email address already taken");

            var user = new User()
            {
                Email = model.Email,
                Salt = AppEncryption.GenerateSalt()
            };

            user.Password = AppEncryption.GenerateHashedPassword(model.Password, user.Salt);

            var res = await userRepository.AddAsync(user);

            if (res > 0) return APIRESPONSE<UserResponse>.SuccessResponse(message: "Signed up successfully", result: mapper.Map<UserResponse>(user));

            return APIRESPONSE<UserResponse>.ErrorResponse();
        }
    }
}
