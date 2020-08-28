using Seabattle.Logic.Models;
using System;
using System.Collections.Generic;

namespace Seabattle.Logic.ComputerLogic.ShipGeneration
{
    public class ShipGenerator
    {
        private int[][] _matrix;
        private Random _random = new Random();

        public List<ShipDto> GenerateShips()
        {
            var result = new List<ShipDto>();
            LocatedShips(result);
            return result;
        }

        private void LocatedShips(List<ShipDto> ships)
        {
            var decks = 4;
            var shipData = new ShipData();
            _matrix = CreateMatrix();

            for (var i = 1; i <= 4; i++)
            {
                for (var j = 0; j < i; j++)
                {
                    shipData = GetCoordinatesDecks(decks);
                    ships.Add(CreateShip(shipData));
                }
                decks--;
            }
        }

        private ShipData GetCoordinatesDecks(int decks)
        {
            var kx = GetRandom(1);
            var ky = kx == 0 ? 1 : 0;
            var x = kx == 0 ? GetRandom(9) : GetRandom(10 - decks);
            var y = kx == 0 ? GetRandom(10 - decks) : GetRandom(9);

            if (!CheckLocationShip(x, y, kx, ky, decks))
            {
                return GetCoordinatesDecks(decks);
            }

            return new ShipData
            {
                X = x,
                Y = y,
                KX = kx,
                KY = ky,
                Desk = decks
            };
        }

        private bool CheckLocationShip(int x, int y, int kx, int ky, int decks)  
        {
            var toX = 0;
            var toY = 0;

            var fromX = x == 0 ? x : x - 1;
            if (x + kx * decks == 10 && kx == 1)
            {
                toX = x + kx * decks;
            }
            if (x + kx * decks < 10 && kx == 1)
            {
                toX = x + kx * decks + 1;
            }
            if (x == 9 && kx == 0)
            {
                toX = x + 1;
            }
            if (x < 9 && kx == 0)
            {
                toX = x + 2;
            }

            var fromY = y == 0 ? y : y - 1;
            if (y + ky * decks == 10 && ky == 1)
            {
                toY = y + ky * decks;
            }
            if (y + ky * decks < 10 && ky == 1)
            {
                toY = y + ky * decks + 1;
            }
            if (y == 9 && ky == 0)
            {
                toY = y + 1;
            }
            if (y < 9 && ky == 0)
            {
                toY = y + 2;
            }

            for (var i = fromX; i < toX; i++)
            {
                for (var j = fromY; j < toY; j++)
                {
                    if (_matrix[i][j] == 1)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private ShipDto CreateShip(ShipData shipData)
        {
            var k = 0;
            var ship = new ShipDto
            {
                Cells = new List<CellDto>()
            };

            while (k < shipData.Desk)
            {
                _matrix[shipData.X + k * shipData.KX][shipData.Y + k * shipData.KY] = 1;
                ship.Cells.Add(new CellDto
                {
                    X = shipData.X + k * shipData.KX,
                    Y = shipData.Y + k * shipData.KY,
                    IsAlive = true
                });

                k++;
            }

            ship.IsHorizontal = shipData.KX == 1;

            return ship;
        }

        private int[][] CreateMatrix()
        {
            var arr = new int[10][];
            for (int i = 0; i < 10; i++)
            {
                arr[i] = new int[10];
                for (var j = 0; j < 10; j++)
                {
                    arr[i][j] = 0;
                }
            }
            return arr;
        }

        private int GetRandom(int number)
        {
            return (int)Math.Floor(_random.NextDouble() * (number + 1));
        }
    }
}
