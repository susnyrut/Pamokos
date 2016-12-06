using System;
using CodeFights.model;

namespace CodeFights.samples
{
    class RandomGuy : IFighter
    {
        private readonly Random _random = new Random();

        public Move MakeNextMove(Move opponentLastMove, int myLastScore, int oppLastScore)
        {
            var move = new Move();
            var areas = new[] {Area.Belly, Area.Nose, Area.Groin, Area.Jaw, Area.Legs};

            for (var i = 0; i < 3; i++)
            {
                var area = areas[_random.Next(5)];

                if (_random.NextDouble() < 0.4)
                {
                    move.AddDefence(area);
                }
                else
                {
                    move.AddAttack(area);
                }
            }

            return move;
        }
    }
}
