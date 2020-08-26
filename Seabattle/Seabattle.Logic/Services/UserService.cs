using CSharpFunctionalExtensions;
using Seabattle.Logic.Models;
using Seabattle.Logic.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using AutoMapper;
using Seabattle.Logic.Extensions;

namespace Seabattle.Logic.Services
{
    public class UserService : IUserService
    {
        private UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            this._mapper = mapper;
        }

        public async Task<Result> Register(NewUserDto model)
        {
            // validation username existing
            var user = new IdentityUser
            {
                Email = model.Email,
                UserName = model.UserName,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            //var result2 = await _userManager.AddToRoleAsync(user.Id, "user");

            //var token = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);

            //await _userManager.SendEmailAsync(user.Id, "Confirm your email", $"click on https://localhost:44444/api/user/email/confirm?userId={user.Id}&token={token}");

            return Result.Success(result.ToFunctionalResult());
        }

        public async Task<Result<IReadOnlyCollection<UserDto>>> GetAllUsers()
        {
            throw new NotImplementedException();
            //bad practice! don't use it in production. only as sample
            //var users = await _userManager.Users.ProjectToListAsync<UserDto>(_mapper.ConfigurationProvider);
            //return Result.Success((IReadOnlyCollection<UserDto>)users.AsReadOnly());
        }

        public async Task<Maybe<UserDto>> GetUserAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return null;

            var isValid = await _userManager.CheckPasswordAsync(user, password);
            return isValid ? _mapper.Map<UserDto>(user) : null;
        }

        public Maybe<UserDto> GetUser(string username, string password)
        {
            var user = _userManager.FindByName(username);
            if (user == null) return null;

            var isValid = _userManager.CheckPassword(user, password);
            return isValid ? _mapper.Map<UserDto>(user) : null;
        }
    }
}
