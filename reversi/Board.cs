using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reversi
{
    class Board
    {
        char[,] board = new char[8, 8];

        public Board()
        {
            init();
        }

        public void init()
        {
            
            for(int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    board[x, y] = '-';
                }
            }

            setTile(3, 3, 'o');
            setTile(4, 3, 'x');
            setTile(4, 4, 'o');
            setTile(3, 4, 'x');
        }

        public void print()
        {
            Console.Write(" ");
            for (int x = 0; x < 8; x++)
            {
                Console.Write(" " + (x+1));
            }
            Console.Write('\n');
            for (int y = 0; y < 8; y++)
            {
                Console.Write(y + 1);
                for (int x = 0; x < 8; x++)
                {
                    Console.Write(" " + board[x, y]);
                }
                Console.Write('\n');
            }

            Console.WriteLine("Antal 'o': " + tileCount('o'));            
            Console.WriteLine("Antal 'x': " + tileCount('x'));
        }

        private void setTile(int x, int y, char tile)
        {
            board[x, y] = tile;
        }

        private int tileCount(char tile)
        {
            int tiles = 0;

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if(board[x, y] == tile)
                    {
                        tiles++;
                    }
                }
            }
            return tiles;
        }
    }
}