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
            Server server = new Server(true, false);
            //create log
            Log log = new Log(true, false);
            //server.start(45454);

            while (true)
            {
                String input = Console.ReadLine();
                if (input == "stop")
                {
                    log.warn("Server is shutting down");
                    server.sendToAll("server_stop");
                    Environment.Exit(0);
                }
                if (input == "start")
                {
                    if (server.start(45454))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Server started");
                        Console.ResetColor();
                    }
                    else log.error("Server konnte nicht gestartet werden");
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
