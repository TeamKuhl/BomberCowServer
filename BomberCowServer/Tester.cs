using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BomberLib;

namespace BomberCowServer
{
    class Tester
    {
        static void Main(string[] args)
        {
            Console.Title = "BomberCowServer";
            // create server
            Server server = new Server();
            //server.start(45454);

            while (true)
            {
                String input = Console.ReadLine();
                if (input == "stop")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Server is shutting down");
                    server.sendToAll("server_stop");
                    Environment.Exit(0);
                }
                if (input == "start")
                {
                    server.start(45454);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Server started");
                    Console.ResetColor();
                }
                if (input == "sendall")
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("Message:");
                    server.sendToAll("[SERVER] " +Console.ReadLine());
                    Console.ResetColor();
                }
            }
        }
    }
}
