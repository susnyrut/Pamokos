using System;
using CodeFights.model;
using System.IO;
using CodeFights.boilerplate.server;

namespace CodeFights.boilerplate
{
    class Human: IFighter
    {
	    private TextWriter consoleOut = Console.Out;
	    private TextReader consoleIn = Console.In;
		
	    public Move MakeNextMove(Move oppMove, int iScored, int oppScored)
        {
		    PrintInstructions();
		
		    while (true)
                try
                {
                    Move move = ParseInput(consoleIn.ReadLine().Trim());
                    return move;
                }
                catch (ArgumentException ipe)
                {
                    Console.Error.WriteLine("Human error: " + ipe.Message);
                }
                catch (OperationCanceledException oce)
                {
                    Console.Error.WriteLine("Bye");
                    Environment.Exit(0);
                }
	    }

	    private void PrintInstructions() 
        {
		    consoleOut.Write("Make your move by (A)ttacking and (B)locking (N)ose, (J)aw, (B)elly, (G)roin, (L)egs (for example, BN BB AN): ");
	    }
	
	    private Move ParseInput(string input) 
        {
		    input=input.Replace("\\W", "").ToLowerInvariant();

            if (input.StartsWith("q"))
                throw new OperationCanceledException("Exiting");
	 
		    Move move = Protocol.ParseMove(input);
		    if (move.Attacks.Count + move.Defences.Count > 3)
			    throw new ArgumentException("Can make max 3 things at a time!");
		
		    return move;
	    }

    }
}
