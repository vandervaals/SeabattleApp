using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Seabattle.Logic.Models;

namespace Seabattle.Logic.Services.Contracts
{
    public interface IGameService: IDisposable
    {
        Result<int> CreateGame(List<ShipDto> ships, string connectionId);
        Result<AnswerDto> MakeShot(ShotDto shot);
    }
}
