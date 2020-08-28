using AutoMapper;
using CSharpFunctionalExtensions;
using Seabattle.Data.Contexts;
using Seabattle.Data.Models;
using Seabattle.Logic.Models;
using Seabattle.Logic.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;

namespace Seabattle.Logic.Services
{
    public class GameServise : IGameService
    {
        private readonly SeabattleContext _context;
        private readonly IMapper _mapper;

        public GameServise(SeabattleContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public Result<int> CreateGame(List<ShipDto> ships, string connectionId)
        {
            var model = new GameDto
            {
                ConnectionId = connectionId,
                UserArea = new AreaDto
                {
                    Ships = ships
                }
            };

            try
            {
                var dbModel = _mapper.Map<GameDb>(model);

                _context.Games.Add(dbModel);
                _context.SaveChanges();

                return Result.Success(dbModel.Id);
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure<int>(ex.Message);
            }
        }

        public Result<AnswerDto> MakeShot(ShotDto shot)
        {
            return Result.Success(new AnswerDto
            {
                InTarget = true,
                GameContinue = true,
                AreYouWinner = false
            });
        }

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                _context.Dispose();
                GC.SuppressFinalize(this);
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
