using System;
using System.IO;
using CodeFights.model;

namespace CodeFights.boilerplate.server
{
    class ServerMode
    {
	    private TextReader inStream = Console.In;
	    private TextWriter outStream = Console.Out;

	    public void Run(IFighter fighter)
        {
		    Protocol protocol = new Protocol(inStream, outStream);
		    protocol.Handshake();
		
		    Protocol.ServerResponse resp = new Protocol.ServerResponse();
		
		    while(true)
            {
			    Move move = fighter.MakeNextMove(resp.move, resp.score1, resp.score2);
			    protocol.SendRequest(move);
			    resp = protocol.ReadResponse();
		    }	
	    }	
    }
}
