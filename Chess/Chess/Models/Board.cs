using Chess.Enums;
using Chess.Models.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public class Board
    {
        public Piece[,] gameSpace { get; set; }

        public Board(LaunchState launchState = LaunchState.Empty)
        {
            gameSpace = new Piece[8, 8];

            switch (launchState)
            {
                case LaunchState.Empty:

                    for (int x = 0; x < 8; x++)
                    {
                        for (int y = 0; y < 8; y++)
                        {
                            gameSpace[x, y] = new EmptyPiece(x, y);
                        }
                    }

                    break;

                case LaunchState.NoPawns:

                    for (int x = 0; x < 8; x++)
                    {
                        if(x == 0)
                        {
                            for (int y = 0; y < 8; y++)
                            {
                                if (y == 0 | y == 7)
                                {
                                    gameSpace[x, y] = new Rook("White", x, y);
                                }
                                else if (y == 1 | y == 6)
                                {
                                    gameSpace[x, y] = new Knight("White", x, y);
                                }
                                else if (y == 2 | y == 5)
                                {
                                    gameSpace[x, y] = new Bishop("White", x, y);
                                }
                                else if (y == 3)
                                {
                                    gameSpace[x, y] = new Queen("White", x, y);
                                }
                                else if (y == 4)
                                {
                                    gameSpace[x, y] = new King("White", x, y);
                                }
                            }
                        }
                        else if (x == 7)
                        {
                            for (int y = 0; y < 8; y++)
                            {
                                if (y == 0 | y == 7)
                                {
                                    gameSpace[x, y] = new Rook("Black", x, y);
                                }
                                else if (y == 1 | y == 6)
                                {
                                    gameSpace[x, y] = new Knight("Black", x, y);
                                }
                                else if (y == 2 | y == 5)
                                {
                                    gameSpace[x, y] = new Bishop("Black", x, y);
                                }
                                else if (y == 3)
                                {
                                    gameSpace[x, y] = new Queen("Black", x, y);
                                }
                                else if (y == 4)
                                {
                                    gameSpace[x, y] = new King("Black", x, y);
                                }
                            }
                        }
                        else
                        {
                            for (int y = 0; y < 8; y++)
                            {
                                gameSpace[x, y] = new EmptyPiece(x, y);
                            }
                        }
                    }

                    break;

                case LaunchState.FullStart:

                    for (int x = 0; x < 8; x++)
                    {
                        if (x == 0)
                        {
                            for (int y = 0; y < 8; y++)
                            {
                                if (y == 0 | y == 7)
                                {
                                    gameSpace[x, y] = new Rook("White", x, y);
                                }
                                else if (y == 1 | y == 6)
                                {
                                    gameSpace[x, y] = new Knight("White", x, y);
                                }
                                else if (y == 2 | y == 5)
                                {
                                    gameSpace[x, y] = new Bishop("White", x, y);
                                }
                                else if (y == 3)
                                {
                                    gameSpace[x, y] = new Queen("White", x, y);
                                }
                                else if (y == 4)
                                {
                                    gameSpace[x, y] = new King("White", x, y);
                                }
                            }
                        }
                        else if (x == 1)
                        {
                            for (int y = 0; y < 8; y++)
                            {
                                gameSpace[x, y] = new Pawn("White", x, y);
                            }
                        }
                        else if (x == 6)
                        {
                            for (int y = 0; y < 8; y++)
                            {
                                gameSpace[x, y] = new Pawn("Black", x, y);
                            }
                        }
                        else if (x == 7)
                        {
                            for (int y = 0; y < 8; y++)
                            {
                                if (y == 0 | y == 7)
                                {
                                    gameSpace[x, y] = new Rook("Black", x, y);
                                }
                                else if (y == 1 | y == 6)
                                {
                                    gameSpace[x, y] = new Knight("Black", x, y);
                                }
                                else if (y == 2 | y == 5)
                                {
                                    gameSpace[x, y] = new Bishop("Black", x, y);
                                }
                                else if (y == 3)
                                {
                                    gameSpace[x, y] = new Queen("Black", x, y);
                                }
                                else if (y == 4)
                                {
                                    gameSpace[x, y] = new King("Black", x, y);
                                }
                            }
                        }
                        else
                        {
                            for (int y = 0; y < 8; y++)
                            {
                                gameSpace[x, y] = new EmptyPiece(x, y);
                            }
                        }
                    }

                    break;

                default:

                    break;
            }
        }

        public void DisplayBoard()
        {
            int iterationCounter = 0;

            foreach (Piece p in gameSpace)
            {
                iterationCounter++;
                Console.Write($"{p.GetSymbol()} ");

                if (iterationCounter % 8 == 0)
                {
                    Console.WriteLine();
                }
            }
        }
    }
}
