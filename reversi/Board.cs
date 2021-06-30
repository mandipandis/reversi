using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reversi
{
    struct Position
    {
        public int x;
        public int y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
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

        public void setTile(int x, int y, char tile)
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
        private bool isOnBoard(int x, int y)
        {
            return x > -1 && x < 8 && y > -1 && y < 8;
        }
        public List<Position> flippedTiles(int x, int y, char tile)
        {
            //ska ge vilka rutor som ska flippas, men ska inte flippa
            //kollar om på board
            //kollar 8an runt

            //om finns granne som har annan färg- true

            List<Position> directions = new List<Position>();

            directions.Add(new Position(1, 1));
            directions.Add(new Position(1, 0));
            directions.Add(new Position(1, -1));
            directions.Add(new Position(-1, -1));
            directions.Add(new Position(-1, 0));
            directions.Add(new Position(-1, 1));
            directions.Add(new Position(0, -1));
            directions.Add(new Position(0, 1));

            char otherTile = '-';
            if (tile == 'x')
            {
                otherTile = 'o';
            }
            else if (tile == 'o')
            {
                otherTile = 'x';
            }

            List<Position> tilesToFlip = new List<Position>();

            foreach (var direction in directions)
            {
                int curX = x + direction.x;
                int curY = y + direction.y;

                if (isOnBoard(curX, curY))
                {
                    while (board[curX, curY] == otherTile)
                    {
                        //direction i den riktning ska öka ett steg,
                        curX += direction.x;
                        curY += direction.y;
                        
                        //kolla om det är annann eller samma som "tile" är
                        if (isOnBoard(curX, curY) && board[curX, curY] == tile)
                        {
                            while (true)
                            {
                                curX -= direction.x;
                                curY -= direction.y;
                                if (curX == x && curY == y)
                                {
                                    break;
                                }
                                else
                                {
                                    tilesToFlip.Add(new Position(curX, curY));
                                }
                            }
                        }
                    }     
                }
            }
            return tilesToFlip;
        }
    } 
}