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
            char choise = Console.ReadLine()[0];

            char tile = '-';
            bool goOn = true;
            while (goOn)
            {
                if (choise == Board.black || choise == Board.white)
                {
                    tile = choise;
                    goOn = false;
                }
                else
                {
                    Console.Write("Unapproved value.\nChoose 'x' or 'o': ");
                    choise = Console.ReadLine()[0];
                }
            }

            Console.WriteLine("You are playing as {0}!", tile);
            Console.WriteLine("You start!");
            board.print();

            char otherTile = tile == Board.black ? Board.white : Board.black;
            int posX = 0;
            int posY = 0;
            goOn = true;
            bool stay = true;
            while (goOn)
            {
                while (stay)
                {
                    Console.Write("\nSelect an X cordinate: ");
                    posX = int.Parse(Console.ReadLine());
                    posX -= 1;
                    Console.Write("Select a Y cordinate: ");
                    posY = int.Parse(Console.ReadLine());
                    posY -= 1;
                    var positions = board.flippedTiles(posX, posY, tile);
                    if (positions.Count > 0)
                    {
                        board.setTiles(positions, tile);
                        stay = false;
                    }
                    else
                    {
                        Console.WriteLine("Unapproved move, make a new one");
                    }
                }
               
                board.print();

                var move = board.bestMove(otherTile);
                if(move.HasValue)
                {
                    var flipped = board.flippedTiles(move.Value.x, move.Value.y, otherTile);
                    board.setTiles(flipped, otherTile);
                    Console.WriteLine("Computer moved to X: {0} and Y: {1}", move.Value.x + 1, move.Value.y + 1);
                    stay = true;
                }
                else
                {
                    var tiles = board.tileCount(tile);
                    var otherTiles = board.tileCount(otherTile);
                    if(tiles > otherTiles)
                    {
                        Console.WriteLine("You won!");
                        goOn = false;
                    }
                    else if(otherTiles > tiles)
                    {
                        Console.WriteLine("You lost!");
                        goOn = false;
                    }
                }
                board.print();
            }
        }
    }
}