using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialRoom
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(200, 45);

            Console.SetCursorPosition(75, 25);

            Console.WriteLine(@"Statok:
                                                                            Életerő: 80
                                                                            Erő: 3
                                                                            Józanság: 70
                                                                            Védekezés: 6
                                                                            Energia: 2");

            Console.SetCursorPosition(72, 0);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(@"|
                                                                        |
                                                                        |
                                                                        |
                                                                        |
                                                                        |
                                                                        |
                                                                        |
                                                                        |
                                                                        |
                                                                        |
                                                                        |
                                                                        |
                                                                        |
                                                                        |
                                                                        |
                                                                        |
                                                                        |
                                                                        |
                                                                        |
                                                                        |
                                                                        |
                                                                        |
                                                                        |
                                                                        |
                                                                        |
                                                                        |
                                                                        |
                                                                        |
                                                                        |
                                                                        |
                                                                        |
                                                                        |
                                                                        |
                                                                        |
                                                                        |");
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(88, 0);

            Console.WriteLine("Eszköztár");

            Console.SetCursorPosition(75, 20);

            Console.WriteLine("? megvizsgál | + használ | - eldob");

            Console.SetCursorPosition(2, 13);

            Console.WriteLine(@"__
 (`/\
 `=\/\ __...--~~~~~-._   _.-~~~~~--...__
  `=\/\               \ /               \\
   `=\/                V                 \\
   //_\___--~~~~~~-._  |  _.-~~~~~~--...__\\
  //  ) (..----~~~~._\ | /_.~~~~----.....__\\
 ===( INK )==========\\|//====================
__ejm\___/________dwb`---`____________________________________________");


            Console.SetCursorPosition(0, 23);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(@"_________________________________________________________________________________________________________________");
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(1, 25);

            Console.WriteLine(@"A következő három ötlet jut eszedbe

 >> Egy poros pince ahol egy csontvár (nagyon veszélyes) ül a sarokban.
 >> Körbenézel a falak homokból vannak és egy szarkofág (? veszélyes)
    van előtted.
 >> A falak ki vannak matracozva és egy megkötözött ember (?) 
    van a szobába rajtad kívül.");

        }
    }
}
