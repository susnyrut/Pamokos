using System;
using System.Collections.Generic;
using CodeFights.model;

using System.Linq;

namespace CodeFights.boilerplate
{
    class Arena
    {
        private Dictionary<string, IFighter> fighters = new Dictionary<string, IFighter>();
	
	    private Commentator commentator = new Commentator();
	
	    public Arena RegisterFighter(IFighter fighter, string name)
        {
		    fighters.Add(name, fighter);
		    return this;
	    }
	
	    public void StageFight() 
        {
		    if (fighters.Count != 2)
			    throw new ArgumentException("Must be 2 fighters!"); 
		
            string f1name = fighters.Keys.FirstOrDefault();
		    IFighter fighter1 = fighters[f1name];
            fighters.Remove(f1name);

		    string f2name = fighters.Keys.FirstOrDefault();
		    IFighter fighter2 = fighters[f2name];
            fighters.Remove(f2name);
		
		    commentator.SetFighterNames(f1name, f2name);
		
		    Move f1Move = null;
		    Move f2Move = null;
		
		    int score1=0;
		    int score2=0;
		
		    int f1Lifepoints, f2Lifepoints=f1Lifepoints=GameScoringRules.LIFEPOINTS;
		
		    while(f1Lifepoints > 0 && f2Lifepoints > 0)
            {
			    Move move1 = fighter1.MakeNextMove(f2Move, score1, score2);
			    if (GameScoringRules.IsMoveLegal(move1)==false)
				throw new ArgumentException(f1name+" made an illegal move: "+move1);

			    Move move2 = fighter2.MakeNextMove(f1Move, score2, score1);
			    if (GameScoringRules.IsMoveLegal(move2)==false)
				throw new ArgumentException(f2name+" made an illegal move: "+move2);
			
			    score1 = GameScoringRules.CalculateScore(move1.Attacks, move2.Defences);
			    score2 = GameScoringRules.CalculateScore(move2.Attacks, move1.Defences);
		
			    commentator.DescribeRound(move1, move2, score1, score2);
			
			    f1Lifepoints -=score2;
			    f2Lifepoints -=score1;
			
			    f1Move = move1;
			    f2Move = move2;
		    }
		
		    commentator.GameOver(f1Lifepoints, f2Lifepoints);
	    }

        public Arena SetCommentator(Commentator c)
        {
            this.commentator = c;
            return this;
        }

    }
}
