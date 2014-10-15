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
            // set console title
            Console.Title = "BomberCowServer v0.0.0.0.0.0.0.0.00.03";

            // new game
            Game game = new Game();

            // start game
            game.start();

        }
    }
}
