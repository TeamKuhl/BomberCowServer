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

            //create log
            Log log = new Log(true, true);

            // create server
            Server server = new Server(log);


            while (true)
            {
                String input = Console.ReadLine();
                if (input == "stop")
                {
                    server.stop();
                    //log.close();
                    //server.sendToAll("server_stop");
                    //Environment.Exit(0);
                }
                if (input == "start")
                {
                    server.start(45454);
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
