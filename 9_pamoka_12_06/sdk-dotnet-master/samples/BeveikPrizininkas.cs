using CodeFights.model;

namespace CodeFights.samples
{
    class BeveikPrizininkas : IFighter
    {
        public Move MakeNextMove(Move opponentsLastMove, int myLastScore, int opponentsLastScore)
        {
            var move = new Move();
            if (opponentsLastMove != null)
            {
                Area[] legalAreas = {Area.Nose, Area.Jaw, Area.Belly, Area.Groin, Area.Legs};
                foreach (Area area in legalAreas)
                {
                    if (!opponentsLastMove.Defences.Contains(area) && move.Attacks.Count < 2)
                    {
                        move.AddAttack(area);
                    }
                    if (opponentsLastMove.Attacks.Contains(area) && move.Defences.Count < 1)
                    {
                        move.AddDefence(area);
                    }
                }
            }
            else
            {
                move
                    .AddAttack(Area.Jaw)
                    .AddAttack(Area.Jaw)
                    .AddDefence(Area.Nose);
            }
            return move;
        }
    }
}