using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeFights.model;

namespace CodeFights.samples
{
    class Kickboxer: IFighter
    {
        private Area attack1 = Area.Jaw;
        private Area attack2 = Area.Nose;
        private Area defence = Area.Nose;
    
	    public Move MakeNextMove(Move opponentLastMove, int myLastScore, int oppLastScore) 
        {
		    if (opponentLastMove != null)
	            if (opponentLastMove.Defences.Contains(this.attack1))
	                this.attack1 = CreateRandomArea();
        
            this.attack2 = CreateRandomArea();
        
            return new Move()
                        .AddAttack(attack1)
                        .AddAttack(attack2)
                        .AddDefence(defence);
        }

        private Area CreateRandomArea() 
        {
            double random = new Random().NextDouble();
            if (random<0.3)
                return Area.Nose;

            if (random<0.7)
                return Area.Jaw;

            if (random<0.9)
                return Area.Groin; // oh yeah

            return Area.Belly;
        }	

    }
}
