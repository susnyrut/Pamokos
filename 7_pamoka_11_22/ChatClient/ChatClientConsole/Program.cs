using Chat;
using Chat.Model;
using System;
using System.Collections.Generic;

namespace ChatClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            /*ChatClient client = new ChatClient();

            client.Register("", "");
            client.SendMessage("", "");
            List<Message> messages = client.GetAllMessages(20);*/

            string userName, userPass;

            Console.Write("Iveskite prisijungimo varda: ");
            userName = Console.ReadLine().Trim();

            Console.Write("Iveskite prisijungimo slaptazodi: ");
            userPass = Console.ReadLine().Trim();

            if (true/* cia turi buti kodas kuris patikrintu ar prisijungimas pavyko */)
            {
                ConsoleKeyInfo userMenuInput = new ConsoleKeyInfo();

                while (userMenuInput.Key != ConsoleKey.Escape)
                {
                    OutputMenu(userName);

                    if (userMenuInput.KeyChar == '1')
                    {
                        string messageRecipient, messageText;

                        InputMessage(out messageRecipient, out messageText);

                        /* cia turi buti kodas kuris siustu zinute */

                        OutputMessageSent();
                    }
                    else if (userMenuInput.KeyChar == '2')
                    {
                        string[] senders = new string[] { "Jonas", "Petras", "Bronius" }; /* cia turi buti kodas kuris uzpildytu siunteju sarasa */

                        OutputSenders(senders);
                    }
                    else if (userMenuInput.KeyChar == '3')
                    {
                        string sender;
                        string[] messages;

                        InputSender(out sender);

                        messages = new string[] { "zinutes nr. 1", "antra zinute", "zinute paskutine" }; /* cia turi buti kodas kuris uzpildytu zinuciu sarasa */

                        OutputMessages(messages);
                    }

                    userMenuInput = Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("-----------------------");
                Console.WriteLine("Prisijungimas nepavyko!");
                Console.ReadKey();
            }
        }

        static void OutputMenu(string loggedInUser)
        {
            Console.Clear();

            Console.WriteLine(string.Format("Prisijunges vartotojas: {0}", loggedInUser));
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Galimi veiksmai (paspauskite atitinkama klavisa):");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("1 - siusti zinute");
            Console.WriteLine("2 - gauti siunteju sarasa");
            Console.WriteLine("3 - gauti siuntejo zinutes");
            Console.WriteLine();
            Console.WriteLine("ESC - iseiti");
        }

        static void InputMessage(out string recipient, out string text)
        {
            Console.WriteLine();
            Console.WriteLine("------------------------------");

            Console.Write("Iveskite zinutes gavejo varda: ");
            recipient = Console.ReadLine().Trim();

            Console.Write("Iveskite zinutes teskta: ");
            text = Console.ReadLine().Trim();
        }

        static void OutputMessageSent()
        {
            Console.WriteLine();
            Console.WriteLine("Zinute nusiusta!");
        }

        static void OutputSenders(string[] senders)
        {
            Console.WriteLine();
            Console.WriteLine("------------------");
            Console.WriteLine("Zinuciu siuntejai:");

            for (int i = 0; i < senders.Length; i++)
            {
                Console.WriteLine(string.Format("{0}. {1}", (i + 1), senders[i]));
            }
        }

        static void InputSender(out string sender)
        {
            Console.WriteLine();
            Console.WriteLine("--------------------------------");

            Console.Write("Iveskite zinutes siuntejo varda: ");
            sender = Console.ReadLine().Trim();
        }

        static void OutputMessages(string[] messages)
        {
            for (int i = 0; i < messages.Length; i++)
            {
                Console.WriteLine(string.Format("{0}. {1}", (i + 1), messages[i]));
            }
        }
    }
}