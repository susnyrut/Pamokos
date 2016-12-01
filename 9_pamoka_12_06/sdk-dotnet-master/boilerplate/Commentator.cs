using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeFights.model;
using System.IO;

namespace CodeFights.boilerplate
{
    class Commentator
    {

        private string fighter1 = "Fighter1";
        private string fighter2 = "Fighter2";

        private TextWriter outStream = Console.Out;

        private int lp1 = GameScoringRules.LIFEPOINTS;
        private int lp2 = GameScoringRules.LIFEPOINTS;

        public void SetFighterNames(string fighter1name, string fighter2name)
        {
            fighter1 = fighter1name;
            fighter2 = fighter2name;
        }

        public void DescribeRound(Move move1, Move move2, int score1, int score2)
        {
            DescribeMove(fighter1, move1, score1, move2);
            DescribeMove(fighter2, move2, score2, move1);

            lp1 -= score2;
            lp2 -= score1;

            outStream.WriteLine(fighter1 + " vs " + fighter2 + ": " + lp1 + " to " + lp2);
            outStream.WriteLine();
        }


        public void GameOver(int f1Lifepoints, int f2Lifepoints)
        {
            outStream.WriteLine("FIGHT OVER");
            if (f1Lifepoints > f2Lifepoints)
                outStream.WriteLine("THE WINNER IS " + fighter1);
            else
            if (f2Lifepoints > f1Lifepoints)
                outStream.WriteLine("THE WINNER IS " + fighter2);
            else
                outStream.WriteLine("IT'S A DRAW!!!");
        }

        private void DescribeMove(string fighterName, Move move, int score, Move counterMove)
        {
            outStream.WriteLine(fighterName
                                    + DescribeAttacks(move, counterMove, score)
                                    + DescribeDefences(move));
        }

        private static string DescribeAttacks(Move move, Move counterMove, int score)
        {
            if (move.Attacks.Count <= 0)
                return " did NOT attack at all ";

            string rez = " attacked ";
            foreach (Area attack in move.Attacks)
            {
                rez += attack.ToString().ToUpper();
                if (counterMove.Defences.Contains(attack))
                    rez += "(-), ";
                else
                    rez += "(+), ";
            }
            return rez += " scoring " + score;
        }

        private static string DescribeDefences(Move move)
        {
            if (move.Defences.Count <= 0)
                return "  and was NOT defending at all.";

            string rez = " while defending ";
            foreach (Area defence in move.Defences)
                rez += defence.ToString().ToUpper() + ", ";

            return rez;
        }

    }
}
