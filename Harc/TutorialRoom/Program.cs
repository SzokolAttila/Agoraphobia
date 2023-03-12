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
                                                                            Életerő: 60
                                                                            Erő: 4
                                                                            Józanság: 50
                                                                            Védekezés: 6
                                                                            Energia: 5");

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

            Console.SetCursorPosition(87, 1);

            Console.WriteLine("fa bot s:2");

            Console.SetCursorPosition(87, 2);

            Console.WriteLine("Kötött sapka v:2 *");

            Console.SetCursorPosition(75, 20);

            Console.WriteLine("? megvizsgál | + használ | - eldob");

            Console.SetCursorPosition(70,7);

            Console.WriteLine(@" 
                                    
                            \    /
                             \__/
                           _| .. |_
                          |   __   |
                          \__ vv __/
                          /_|    |_\
                          V_|\__/|_V
                    ");



            Console.SetCursorPosition(0, 23);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(@"_________________________________________________________________________________________________________________");
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(1, 25);

            Console.WriteLine(@"Vérnyúl Életerő: 20
Ellenség lépése: sebzés 6
Mi a következő lépésed?


 +
Mit használsz föl?
 1
Wow támadásod 8 sebzést mért az ellenfélre!");


        }
    }
}
