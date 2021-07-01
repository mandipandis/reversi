using System;
using System.Collections.Generic;

namespace reversi
{
    class Program
    {
        static void Main(string[] args)
        {

            Board board = new Board();

            Console.Write("Welcome to Reversi!\nChoose 'x' or 'o': ");
            char coise = Console.ReadLine()[0];

            char tile = '-';
            //char otherTile = '-';
            bool goOn = true;
            while (goOn)
            {
                if (coise == 'x')
                {
                    Console.WriteLine("You are playing as 'x'!");
                    tile = 'x';
                    //otherTile = 'o';
                    goOn = false;
                }
                else if (coise == 'o')
                {
                    Console.WriteLine("You are playing as 'o'!");
                    tile = 'o';
                    //otherTile = 'x'; 
                    goOn = false;
                }
                else
                {
                    Console.Write("Unapproved value.\nChoose 'x' or 'o': ");
                    coise = Console.ReadLine()[0];
                }
            }

            Console.WriteLine("You start!");
            Console.WriteLine("Board: \n");
            board.print();

            int posX = 0;
            int posY = 0;
            goOn = true;
            while (goOn)
            {
                Console.Write("Select an X cordinate: ");
                posX = int.Parse(Console.ReadLine());
                posX -= 1;
                Console.Write("Select a Y cordinate: ");
                posY = int.Parse(Console.ReadLine());
                posY -= 1;
                //Kolla om innom directions
                var positions = board.flippedTiles(posX, posY, tile);
                if (positions.Count > 0)
                {
                    board.setTile(posX, posY, tile);
                    foreach (var pos in positions)
                    {
                        board.setTile(pos.x, pos.y, tile);
                    }
                    goOn = false;
                }
                else
                {
                    Console.WriteLine("Unapproved move, make a new one");
                }
            }                
            board.print();
        }
    }
}
