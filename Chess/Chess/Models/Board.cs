using Chess.Controllers;
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

        public int[] IsKingInCheck(bool isKingWhite, int[] pos)
        {
            int[] kingInCheckArray = new int[4];
            kingInCheckArray[2] = 0;
            int count = 0;

            foreach (Piece p in gameSpace)
            {
                if (char.IsLower(p.GetSymbol()) ^ isKingWhite)
                {
                    if (p.Move(pos) == 5)
                    {
                        kingInCheckArray[2] = 1;
                        kingInCheckArray[0] = p.XPosition;
                        kingInCheckArray[1] = p.YPosition;
                        count++;
                    }

                    if (p.Move(pos) == 6)
                    {
                        count = 1;
                    }
                }
            }

            kingInCheckArray[3] = count;

            return kingInCheckArray;
        }

        public int[] IsKingInCheck(bool isKingWhite)
        {
            return IsKingInCheck(isKingWhite, LocateKing(isKingWhite));
        }

        public bool IsKingInCheckmate(bool isKingWhite)
        {
            int[] kingInCheckArray = IsKingInCheck(isKingWhite);

            if (kingInCheckArray[2] == 1)
            {
                int[] kingPos = LocateKing(isKingWhite);

                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        int x = kingPos[0] + i;
                        int y = kingPos[1] + j;

                        if (x > -1 && x < 8 && y > -1 && y < 8)
                        {
                            if (IsKingInCheck(isKingWhite, new int[2] { x, y })[3] == 0)
                            {
                                if (gameSpace[x, y] is EmptyPiece || (char.IsLower(gameSpace[x, y].GetSymbol()) ^ isKingWhite))
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }

                if (kingInCheckArray[3] > 1)
                {
                    return true;
                }

                foreach (Piece p in gameSpace)
                {
                    if (!(p is EmptyPiece))
                    {
                        if (char.IsUpper(p.GetSymbol()) ^ isKingWhite)
                        {
                            if (p.Move(new int[2] { kingInCheckArray[0], kingInCheckArray[1] }) == 0)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public List<string> GetMovablePieces()
        {
            List<string> pieces = new List<string>();
            foreach (Piece p in gameSpace)
            {
                if (char.IsUpper(p.GetSymbol()) ^ ChessController.IsWhite)
                {
                    if (p.IsMovable())
                    {
                        string moveString = $"{ Convert.ToString(Convert.ToChar(p.XPosition + 97)) }{ p.YPosition + 1}";
                        pieces.Add(moveString);
                        Console.WriteLine($"{p.GetType().Name}:  {moveString}");
                    }
                }
            }

            return pieces;
        }

        public Piece GetPieceAt(string move)
        {
            int[] pos = ChessController.ConvertToXY(move);

            return gameSpace[pos[0], pos[1]];
        }
    }
}
