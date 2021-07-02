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

    struct MoveScore
    {
        public int x;
        public int y;
        public double value;

        public MoveScore(int x, int y, double value)
        {
            this.x = x;
            this.y = y;
            this.value = value;
        }
    }

    class Board
    {
        char[,] board = new char[8, 8];
        public const char black = 'x';
        public const char white = 'o';
        const char empty = '-';

        public Board()
        {
            init();
        }

        public void init()
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    board[x, y] = empty;
                }
            }

            setTile(3, 3, white);
            setTile(4, 3, black);
            setTile(4, 4, white);
            setTile(3, 4, black);
        }

        public void print()
        {
            Console.Write("\n ");
            for (int x = 0; x < 8; x++)
            {
                Console.Write(" " + (x + 1));
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

            Console.WriteLine("Antal {0}: {1}", white, tileCount(white));
            Console.WriteLine("Antal {0}: {1}", black, tileCount(black));
        }

        public void setTile(int x, int y, char tile)
        {
            board[x, y] = tile;
        }

        public void setTiles(List<Position> positions, char tile)
        {
            foreach (var pos in positions)
            {
                setTile(pos.x, pos.y, tile);
            }
        }

        public int tileCount(char tile)
        {
            int tiles = 0;
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (board[x, y] == tile)
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

        private bool isFree(int x, int y)
        {
            return isOnBoard(x, y) && board[x, y] == empty;
        }

        public List<Position> flippedTiles(int x, int y, char tile)
        {
            List<Position> directions = new List<Position>();
            List<Position> tilesToFlip = new List<Position>();

            directions.Add(new Position(1, 1));
            directions.Add(new Position(1, 0));
            directions.Add(new Position(1, -1));
            directions.Add(new Position(-1, -1));
            directions.Add(new Position(-1, 0));
            directions.Add(new Position(-1, 1));
            directions.Add(new Position(0, -1));
            directions.Add(new Position(0, 1));

            char otherTile = tile == black ? white : black;

            foreach (var direction in directions)
            {
                int curX = x + direction.x;
                int curY = y + direction.y;

                while (isOnBoard(curX, curY) && board[curX, curY] == otherTile)
                {
                    curX += direction.x;
                    curY += direction.y;

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
            if (tilesToFlip.Count > 0)
            {
                tilesToFlip.Add(new Position(x, y));
            }
            return tilesToFlip;
        }

        public Position? bestMove(char tile)
        {
            List<MoveScore> moves = new List<MoveScore>();

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (isFree(x, y))
                    {
                        var tiles = flippedTiles(x, y, tile);
                        if (tiles.Count > 0)
                        {
                            var copy = clone();
                            copy.setTiles(tiles, tile);
                            var score = copy.value(tile);
                            moves.Add(new MoveScore(x, y, score));
                        }
                    }
                }
            }

            if (moves.Count > 0)
            {
                var best = moves[0];
                foreach (var move in moves)
                {
                    if (best.value < move.value)
                    {
                        best = move;
                    }
                }
                return new Position(best.x, best.y);
            }
            return null;
        }

        public double value(char tile)
        {
            char otherTile = tile == black ? white : black;
            return tileCount(tile) * 1.5 - tileCount(otherTile);
        }

        public Board clone()
        {
            Board b = new Board();

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    b.board[x, y] = board[x, y];
                }
            }
            return b;
        }
    }
}