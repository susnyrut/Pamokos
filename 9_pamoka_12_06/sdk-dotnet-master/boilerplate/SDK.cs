using System;
using CodeFights.model;
using CodeFights.boilerplate;
using CodeFights.boilerplate.server;
using CodeFights.samples;

namespace CodeFights
{
    class SDK
    {
        public static string FIGHT_HUMAN_SWITCH = "--fight-me";
        public static string FIGHT_BOT_SWITCH = "--fight-bot";
        public static string RUN_ON_SERVER_SWITCH = "--fight-on-server";

        public static string USAGE_INSTRUCTIONS = FIGHT_HUMAN_SWITCH + "\t\truns your bot against you in interactive mode\n"
                                               + FIGHT_BOT_SWITCH + " boxer\truns your bot against a built-in boxer bot\n"
                                               + FIGHT_BOT_SWITCH + " kickboxer\truns your bot against a built-in kickboxer bot\n"
                                               + RUN_ON_SERVER_SWITCH + "\truns your bot in codefights engine environment";


        static void Main(string[] args)
        {
            if (IsFightHumanMode(args))
            {
                new Arena()
                    .RegisterFighter(new Human(), "You")
                    .RegisterFighter(new MyFighter(), "Your bot")
                    .StageFight();

                Console.ReadKey();
            }
            else if (IsFightBotMode(args))
            {
                new Arena()
                    .RegisterFighter(new MyFighter(), "Your bot")
                    .RegisterFighter(createBot(args), args[1])
                    .StageFight();

                Console.ReadKey();
            }
            else
            if (IsRunInServerMode(args))
                new ServerMode().Run(new MyFighter());
            else
                PrintUsageInstructions(args);

        }


        private static bool IsRunInServerMode(string[] args)
        {
            return args.Length == 1 && args[0].Equals(RUN_ON_SERVER_SWITCH, StringComparison.InvariantCultureIgnoreCase);
        }

        private static bool IsFightBotMode(string[] args)
        {
            return args.Length >= 2 && args[0].Equals(FIGHT_BOT_SWITCH, StringComparison.InvariantCultureIgnoreCase);
        }

        private static bool IsFightHumanMode(string[] args)
        {
            return args.Length == 1 && args[0].Equals(FIGHT_HUMAN_SWITCH, StringComparison.InvariantCultureIgnoreCase);
        }

        private static void PrintUsageInstructions(string[] args)
        {
		    if (args.Length>0)
            {
			    Console.Out.Write("unrecognized option(s): ");

			    foreach(string arg in args)
				    Console.Out.Write(arg+" ");

			    Console.Out.WriteLine();
		    }
		    Console.Out.WriteLine(USAGE_INSTRUCTIONS);
	    }

        private static IFighter createBot(string[] args)
        {
            if ("boxer".Equals(args[1], StringComparison.InvariantCultureIgnoreCase))
                return new Boxer();

            if ("kickboxer".Equals(args[1], StringComparison.InvariantCultureIgnoreCase))
                return new Kickboxer();

            throw new NotSupportedException("unrecognized built-in bot: " + args[1]);
        }
    }
}
