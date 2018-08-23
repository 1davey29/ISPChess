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

                    for (int y = 0; y < 8; y++)
                    {
                        for (int x = 0; x < 8; x++)
                        {
                            gameSpace[x, y] = new EmptyPiece(x, y);
                        }
                    }

                    break;

                case LaunchState.NoPawns:

                    for (int y = 0; y < 8; y++)
                    {
                        if(y == 7)
                        {
                            for (int x = 0; x < 8; x++)
                            {
                                if (x == 0 | x == 7)
                                {
                                    gameSpace[x, y] = new Rook("White", x, y);
                                }
                                else if (x == 1 | x == 6)
                                {
                                    gameSpace[x, y] = new Knight("White", x, y);
                                }
                                else if (x == 2 | x == 5)
                                {
                                    gameSpace[x, y] = new Bishop("White", x, y);
                                }
                                else if (x == 3)
                                {
                                    gameSpace[x, y] = new Queen("White", x, y);
                                }
                                else if (x == 4)
                                {
                                    gameSpace[x, y] = new King("White", x, y);
                                }
                            }
                        }
                        else if (y == 0)
                        {
                            for (int x = 0; x < 8; x++)
                            {
                                if (x == 0 | x == 7)
                                {
                                    gameSpace[x, y] = new Rook("Black", x, y);
                                }
                                else if (x == 1 | x == 6)
                                {
                                    gameSpace[x, y] = new Knight("Black", x, y);
                                }
                                else if (x == 2 | x == 5)
                                {
                                    gameSpace[x, y] = new Bishop("Black", x, y);
                                }
                                else if (x == 3)
                                {
                                    gameSpace[x, y] = new Queen("Black", x, y);
                                }
                                else if (x == 4)
                                {
                                    gameSpace[x, y] = new King("Black", x, y);
                                }
                            }
                        }
                        else
                        {
                            for (int x = 0; x < 8; x++)
                            {
                                gameSpace[x, y] = new EmptyPiece(x, y);
                            }
                        }
                    }

                    break;

                case LaunchState.FullStart:

                    for (int y = 0; y < 8; y++)
                    {
                        if (y == 7)
                        {
                            for (int x = 0; x < 8; x++)
                            {
                                if (x == 0 | x == 7)
                                {
                                    gameSpace[x, y] = new Rook("White", x, y);
                                }
                                else if (x == 1 | x == 6)
                                {
                                    gameSpace[x, y] = new Knight("White", x, y);
                                }
                                else if (x == 2 | x == 5)
                                {
                                    gameSpace[x, y] = new Bishop("White", x, y);
                                }
                                else if (x == 3)
                                {
                                    gameSpace[x, y] = new Queen("White", x, y);
                                }
                                else if (x == 4)
                                {
                                    gameSpace[x, y] = new King("White", x, y);
                                }
                            }
                        }
                        else if (y == 6)
                        {
                            for (int x = 0; x < 8; x++)
                            {
                                gameSpace[x, y] = new Pawn("White", x, y);
                            }
                        }
                        else if (y == 1)
                        {
                            for (int x = 0; x < 8; x++)
                            {
                                gameSpace[x, y] = new Pawn("Black", x, y);
                            }
                        }
                        else if (y == 0)
                        {
                            for (int x = 0; x < 8; x++)
                            {
                                if (x == 0 | x == 7)
                                {
                                    gameSpace[x, y] = new Rook("Black", x, y);
                                }
                                else if (x == 1 | x == 6)
                                {
                                    gameSpace[x, y] = new Knight("Black", x, y);
                                }
                                else if (x == 2 | x == 5)
                                {
                                    gameSpace[x, y] = new Bishop("Black", x, y);
                                }
                                else if (x == 3)
                                {
                                    gameSpace[x, y] = new Queen("Black", x, y);
                                }
                                else if (x == 4)
                                {
                                    gameSpace[x, y] = new King("Black", x, y);
                                }
                            }
                        }
                        else
                        {
                            for (int x = 0; x < 8; x++)
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
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    Console.Write($"{gameSpace[x, y].GetSymbol()} ");
                }
                Console.WriteLine();
            }
        }

        public int[] LocateKing(bool isWhite)
        {
            int[] kingPositionXY = new int[2];
            foreach (Piece p in gameSpace)
            {
                if (p is King && (isWhite ? Char.IsLower(p.GetSymbol()) : Char.IsUpper(p.GetSymbol())))
                {
                    kingPositionXY[0] = p.XPosition;
                    kingPositionXY[1] = p.YPosition;
                }
            }
            return kingPositionXY;
        }
    }
}
