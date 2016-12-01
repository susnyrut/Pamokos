using System;
using CodeFights.model;
using System.IO;

namespace CodeFights.boilerplate.server
{
    class Protocol
    {
        	
	    public class ServerResponse
        {
		    public Move move;
		    public int score1;
		    public int score2;
	    }

	    public const string HANDSHAKE = "I-AM ready";

	    public const string REQUEST_HEADER = "";
	
	    public const string YOUR_SCORE = "YOUR-SCORE";
	    public const string OPPONENT_SCORE = "OPPONENT-SCORE";
	    public const string ENEMY_MOVE = "ENEMY-MOVE";
	    public const string MOVE_COMMENT = "COMMENT";
	
	    private TextWriter outStream;
	    private TextReader inStream;
	
	    public Protocol(TextReader inStream, TextWriter outStream) 
        {
		    this.outStream = outStream;
		    this.inStream = inStream;
	    }

	    public void Handshake() 
        {
		    outStream.WriteLine(HANDSHAKE);
	    }

	    public void SendRequest(Move move) 
        {
		    outStream.WriteLine(REQUEST_HEADER + SerializeMove(move));
	    }

	    public ServerResponse ReadResponse()
        {
		    return Parse(inStream.ReadLine());
	    }

	    public static string SerializeMove(Move move)
        {
		    string rez = "";
		
		    foreach(Area attack in move.Attacks)
			    rez+="a"+attack.ToString().Substring(0,1);
		
		    foreach(Area defence in move.Defences)
			    rez+="b"+defence.ToString().Substring(0,1);
		
		    if (move.Comment!=null && move.Comment.Trim()!=string.Empty)
			    rez+="c"+move.Comment.Trim();
		
		    return rez.ToLowerInvariant();
	    }
	
	    public static Move ParseMove(string input)
        {
		    Move rez = new Move();
		
		    int index=0;
            while (index < input.Length)
            {
                char type = input[index++];

                switch (type)
                {
                    case 'a': rez.AddAttack(GetArea(input, index++)); break;
                    case 'b': rez.AddDefence(GetArea(input, index++)); break;
                    case 'c': rez.SetComment(input.Substring(index)); index = input.Length + 1; break;
                    default:
                        throw new ArgumentException("Unrecognized input: " + type);
                }
            }
		    return rez;
	    }

	
	    protected static ServerResponse Parse(string line)
        {
		    ServerResponse result = new ServerResponse();
		
		    string[] words = line.Split(' ');
            int index=0;
            while (index<words.Length)
            {
                string firstKeyword = words[index++];
            
                if (index>=words.Length)
                    throw new ArgumentException("Insufficient params. Syntax is [YOUR-SCORE area] [OPPONENT-SCORE area] [ENEMY-MOVE move]");
            
                string nextKeyword = words[index++];
            
                if (YOUR_SCORE.Equals(firstKeyword))
                    int.TryParse(nextKeyword, out result.score1);
                else
                if (OPPONENT_SCORE.Equals(firstKeyword))
                    int.TryParse(nextKeyword, out result.score2);
                else
                if (ENEMY_MOVE.Equals(firstKeyword))
                    result.move = ParseMove(nextKeyword);
                else
                    throw new ArgumentException("invalid keyword " + firstKeyword + ". Syntax is [YOUR-SCORE area] [OPPONENT-SCORE area] [ENEMY-MOVE move]");
            }
		    return result;
	    }
	
        private static string MergeStrings(string[] words, int i) 
        {
            string rez="";
            for (int j=i; j<words.Length; j++)
                rez+=words[j]+" ";

            return rez.Trim();
        }

        private static Area GetArea(string line, int index)
        {
            if (index >= line.Length)
                throw new ArgumentException("Must also specify attack/defence area!");

            switch (line[index])
            {
                case 'n': return Area.Nose;
                case 'j': return Area.Jaw;
                case 'b': return Area.Belly;
                case 'g': return Area.Groin;
                case 'l': return Area.Legs;
                default: throw new ArgumentException("Unrecognized area: "+line[index]);
            }
        }
    }
}
