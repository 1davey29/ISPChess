﻿using Chess.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models.Pieces
{
    public class Pawn : Piece
    {
        private bool hasMoved = false;

        public int Move(int[] positionXY, bool isMoving, bool isChecking)
        {
            int distanceX = Math.Abs(XPosition - positionXY[0]);
            int distanceY = Math.Abs(YPosition - positionXY[1]);

            if (distanceX == 0 && distanceY == 0)
            {
                return 6;
            }

            if ((positionXY[0] < 0 || positionXY[0] > 7) || (positionXY[1] < 0 || positionXY[1] > 7))
            {
                return 4;
            }

            if (distanceX != 0 || ((hasMoved) ? distanceY > 1 : distanceY > 2))
            {
                if (distanceX != 1 || distanceY != 1)
                {
                    return 1;
                }
            }

            int value = 0;

            if (ChessController.Board.gameSpace[positionXY[0], positionXY[1]].GetType() != typeof(EmptyPiece))
            {
                if (distanceX == 1 && distanceY == 1)
                {
                    if (IsSameColor(positionXY))
                    {
                        return 2;
                    }
                    if (ChessController.Board.gameSpace[positionXY[0], positionXY[1]].GetType() == typeof(King))
                    {
                        value = 5;
                    }
                }
                else
                {
                    if (IsSameColor(positionXY))
                    {
                        return 2;
                    }
                    return 3;
                }
            }
            else if (distanceX == 1 && distanceY == 1)
            {
                return 1;
            }
            else if (distanceY == 2)
            {
                for (int y = (YPosition - positionXY[1] > 0) ?
                    positionXY[1] : distanceY; (YPosition - positionXY[1] > 0) ? y <= distanceY :
                    y >= positionXY[1];)
                {

                    if (ChessController.Board.gameSpace[positionXY[0], y].GetType() != typeof(EmptyPiece))
                    {
                        if (IsSameColor(positionXY))
                        {
                            return 2;
                        }
                        return 3;
                    }

                    if (YPosition - positionXY[1] > 0)
                        y++;
                    else
                        y--;
                }
            }

            if (isMoving)
            {
                UpdateBoard(positionXY);

                if (!isChecking)
                {
                    hasMoved = true;

                    if (YPosition == 7 || YPosition == 0)
                    {
                        Promote();
                    }
                }

            }

            //ChessController.Board.DisplayBoard();

            return value;
        }

        public override int Move(int[] positionXY, bool isAcutallyMoving)
        {
            return Move(positionXY, isAcutallyMoving, false);
        }

        public override List<string> GetAvailableMoves()
        {

            //TODO this is a lazy, inefficient method, clean up if possible
                //No reason for a pawn to check the ENTIRE board for moves

            List<string> availibleMoves = new List<string>();
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    int movementReturn = Move(new int[] { x, y }, false);
                    if (movementReturn == 0 || movementReturn == 6)
                    {
                        availibleMoves.Add($"{ Convert.ToString(Convert.ToChar(x + 97)) }{ Math.Abs(y - 8) }");
                    }
                }
            }

            bool inCheck = ChessController.Board.IsKingInCheck(ChessController.IsWhite)[2] == 1;

            if (inCheck)
            {
                List<string> remove = new List<string>();
                int xPos = XPosition;
                int yPos = YPosition;

                foreach (string move in availibleMoves)
                {
                    Piece tempPiece = ChessController.Board.GetPieceAt(move);
                    Piece currentPieceClone = new Pawn((char.IsLower(GetSymbol()) ? "White" : "Black"), xPos, yPos);

                    //BUG: Pawn promotes if can block or take to last row
                    XPosition = xPos;
                    YPosition = yPos;
                    Move(ChessController.ConvertToXY(move), true, true);

                    if (ChessController.Board.IsKingInCheck(ChessController.IsWhite)[2] == 1)
                    {
                        remove.Add(move);
                    }

                    ChessController.Board.gameSpace[xPos, yPos] = currentPieceClone;
                    ChessController.Board.gameSpace[ChessController.ConvertToXY(move)[0], ChessController.ConvertToXY(move)[1]] = tempPiece;
                }

                foreach (string move in remove)
                {
                    availibleMoves.Remove(move);
                }

                XPosition = xPos;
                YPosition = yPos;
            }

            if (!availibleMoves.Contains($"{ Convert.ToString(Convert.ToChar(XPosition + 97)) }{ Math.Abs(YPosition - 8) }"))
            {
                availibleMoves.Add($"{ Convert.ToString(Convert.ToChar(XPosition + 97)) }{ Math.Abs(YPosition - 8) }");
            }

            return availibleMoves;
        }

        public void Promote()
        {
            bool isPromoting = true;
            Console.WriteLine("Your Pawn has earned a promotion! Which piece do you want to promote to? \nPick one: Queen, Rook, Bishop, Knight");
            do
            {
                string selection = Console.ReadLine();
                switch (selection.ToLower())
                {
                    case "queen":
                        ChessController.Board.gameSpace[XPosition, YPosition] = new Queen((Char.IsLower(GetSymbol()) ? "White" : "Black"), XPosition, YPosition);
                        isPromoting = false;
                        break;
                    case "rook":
                        ChessController.Board.gameSpace[XPosition, YPosition] = new Rook((Char.IsLower(GetSymbol()) ? "White" : "Black"), XPosition, YPosition);
                        isPromoting = false;
                        break;
                    case "bishop":
                        ChessController.Board.gameSpace[XPosition, YPosition] = new Bishop((Char.IsLower(GetSymbol()) ? "White" : "Black"), XPosition, YPosition);
                        isPromoting = false;
                        break;
                    case "knight":
                        ChessController.Board.gameSpace[XPosition, YPosition] = new Knight((Char.IsLower(GetSymbol()) ? "White" : "Black"), XPosition, YPosition);
                        isPromoting = false;
                        break;
                    default:
                        Console.WriteLine("Invalid piece type!");
                        break;
                }
            } while (isPromoting);
        }

        public Pawn(String color, int x, int y) : base(color.Equals("White") ? 'p' : 'P', x, y)
        {
        }
    }
}
