using Chat;
using Chat.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            /*
            // Kliento panaudojimo pavyzdys
            ChatClient client = new ChatClient();
            client.Register("", "");
            client.SendMessage("", "");
            List<Message> messages = client.GetAllMessages(20);
            */

            string userName, userPass;

            Console.Write("Įveskite prisijungimo vardą: ");
            userName = Console.ReadLine().Trim();

            Console.Write("Įveskite prisijungimo slaptažodį: ");
            userPass = Console.ReadLine().Trim();

            if (true/* cia turi buti kodas kuris darytu prisijungima */)
            {
                ConsoleKeyInfo userMenuInput = new ConsoleKeyInfo();

                while (userMenuInput.Key != ConsoleKey.Escape)
                {
                    OutputMenu(userName);

                    if (userMenuInput.KeyChar == '1')
                    {
                        string messageRecipient, messageText;

                        InputMessage(out messageRecipient, out messageText);

                        if (true/* cia turi buti kodas kuris siustu zinute */)
                        {
                            OutputMessageSent();
                        }
                        else
                        {
                            OutputMessageNotSent();
                        }
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

                        messages = new string[] { "Žinutės nr. 1", "antra žinutė", "žinutė paskutinė" }; /* cia turi buti kodas kuris uzpildytu zinuciu sarasa */

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

            Console.WriteLine(string.Format("Prisijungęs vartotojas: {0}", loggedInUser));
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Galimi veiksmai (paspauskite atitinkamą klavišą):");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("1 - siųsti žinutę");
            Console.WriteLine("2 - gauti siuntėjų sąrašą");
            Console.WriteLine("3 - gauti siuntėjo žinutes");
            Console.WriteLine();
            Console.WriteLine("ESC - išeiti");
        }

        static void InputMessage(out string recipient, out string text)
        {
            Console.WriteLine();
            Console.WriteLine("------------------------------");

            Console.Write("Įveskite žinutes gavėjo vardą: ");
            recipient = Console.ReadLine().Trim();

            Console.Write("Įveskite žinutės tekstą: ");
            text = Console.ReadLine().Trim();
        }

        static void OutputMessageSent()
        {
            Console.WriteLine();
            Console.WriteLine("Žinutė nusiųsta!");
        }

        static void OutputMessageNotSent()
        {
            Console.WriteLine();
            Console.WriteLine("Žinutės nusiųsti nepavyko!");
        }

        static void OutputSenders(string[] senders)
        {
            Console.WriteLine();
            Console.WriteLine("------------------");
            Console.WriteLine("Žinučių siuntėjai:");

            for (int i = 0; i < senders.Length; i++)
            {
                Console.WriteLine(string.Format("{0}. {1}", (i + 1), senders[i]));
            }
        }

        static void InputSender(out string sender)
        {
            Console.WriteLine();
            Console.WriteLine("--------------------------------");

            Console.Write("Įveskite žinutės siuntėjo vardą: ");
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