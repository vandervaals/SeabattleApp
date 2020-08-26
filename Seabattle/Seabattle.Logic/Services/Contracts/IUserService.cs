using CSharpFunctionalExtensions;
using Seabattle.Logic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Seabattle.Logic.Services.Contracts
{
    public interface IUserService
    {
        Task<Result> Register(NewUserDto model);

        Task<Result<IReadOnlyCollection<UserDto>>> GetAllUsers();

        Task<Maybe<UserDto>> GetUserAsync(string username, string password);

        Maybe<UserDto> GetUser(string username, string password);
    }
}
