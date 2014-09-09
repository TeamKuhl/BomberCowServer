using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomberCowServer
{
    class Tester
    {
        static void Main(string[] args)
        {
            // create server
            Server server = new Server();
            server.start(45454);
            Console.WriteLine("Server started");
        }
    }
}
