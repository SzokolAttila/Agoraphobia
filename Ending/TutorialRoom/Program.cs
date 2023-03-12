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
                                                                            Életerő: 56
                                                                            Erő: 10
                                                                            Józanság: 100
                                                                            Védekezés: 12
                                                                            Energia:5");

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

            Console.WriteLine("Fa bot s:2");

            Console.SetCursorPosition(87, 2);

            Console.WriteLine("Excalibur s:10");

            Console.SetCursorPosition(87, 3);

            Console.WriteLine("Mario sapkája v:7 *");

            Console.SetCursorPosition(75, 20);

            Console.WriteLine("? megvizsgál | + használ | - eldob");

            Console.SetCursorPosition(25, 1);






            Console.SetCursorPosition(2, 13);

            Console.WriteLine(@"
                    /\_____/\
                   /  o   o  \
                  ( ==  ^  == )
                   )         (
                  (           )
                 ( (  )   (  ) )
                (__(__)___(__)__)
");


            Console.SetCursorPosition(0, 23);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(@"_________________________________________________________________________________________________________________");
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(1, 25);

            Console.WriteLine(@"A macska újra megjelenik előtted és hozzád szól:
Gratulálok John sikerült elég inspirációt szerezned,
most ideje felébredned.

 >> Így lesz vége?

 Ezt látod:
 >> Új játék
 >> Kilépés");

        }
    }
}
