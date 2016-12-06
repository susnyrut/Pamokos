using System;
using CodeFights.model;

namespace CodeFights.samples
{
    class Boxer : IFighter
    {
        private Area attack1 = Area.Nose;
        private Area attack2 = Area.Jaw;
        private Area defence = Area.Nose;

        private int myScoreTotal = 0;
        private int opponentScoreTotal = 0;

        public Move MakeNextMove(Move opponentLastMove, int myLastScore, int oppLastScore)
        {
            myScoreTotal += myLastScore;
            opponentScoreTotal += oppLastScore;

            Move move = new Move()
                            .AddAttack(attack1)
                            .AddAttack(attack2);

            if (opponentLastMove != null)
                if (opponentLastMove.Attacks.Contains(defence) == false)
                    defence = changeDefence(defence);

            if (myScoreTotal >= opponentScoreTotal)
                move.AddAttack(createRandomAttack()); // 3 attacks, 0 defence
            else
                move.AddDefence(defence);             // 2 attacks, 1 defence

            return move;
        }

        private Area changeDefence(Area oldDefence)
        {
            return (oldDefence == Area.Nose) ? Area.Jaw : Area.Nose;
        }

        private Area createRandomAttack()
        {
            return new Random().NextDouble() > 0.5d ? Area.Belly : Area.Jaw;
        }
    }
}