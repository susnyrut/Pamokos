using CodeFights.model;

namespace CodeFights.samples
{
    class SimpleGuy : IFighter
    {
        public Move MakeNextMove(Move opponentLastMove, int myLastScore, int oppLastScore)
        {
            var move = new Move()
                            .AddAttack(Area.Nose)
                            .AddAttack(Area.Nose)
                            .AddDefence(Area.Nose);

            return move;
        }
    }
}
