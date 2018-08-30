using Chess.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models.Pieces
{
    public class Bishop : Piece
    {
        public override int Move(int[] positionXY, bool isMoving)
        {
            int distanceX = Math.Abs(XPosition - positionXY[0]);
            int distanceY = Math.Abs(YPosition - positionXY[1]);

            if ((positionXY[0] < 0 || positionXY[0] > 7) || (positionXY[1] < 0 || positionXY[1] > 7))
            {
                return 4;
            }

            if (distanceX != distanceY)
            {
                return 1;
            }

            int validationReturn = PieceInWay(this, positionXY, distanceX, distanceY, false);
            if (validationReturn == 0 && isMoving)
            {
                UpdateBoard(positionXY);
            }

            return validationReturn;
        }

        public static int PieceInWay(Object obj, int[] positionXY, int distanceX, int distanceY, bool isQueen)
        {
            Piece piece;

            if (isQueen)
            {
                piece = obj as Queen;
            } else
            {
                piece = obj as Bishop;
            }

            if (distanceX == 0 && distanceY == 0)
            {
                return 6;
            }

            bool IsXPositive = (piece.XPosition - positionXY[0] > 0);
            bool IsYPositive = (piece.YPosition - positionXY[1] > 0);

            int xIter = 0;
            int yIter = 0;
            for (int x = piece.XPosition; (IsXPositive) ? x >= positionXY[0] : x <= positionXY[0];)
            {
                xIter++;
                yIter = 0;
                for (int y = piece.YPosition; (IsYPositive) ? y >= positionXY[1] : y <= positionXY[1];)
                {
                    yIter++;
                    if (xIter != 1 && (xIter == yIter))
                    {
                        if (ChessController.Board.gameSpace[x, y].GetType() != typeof(EmptyPiece))
                        {
                            int[] testedPositon = { x, y };
                            if (piece.IsSameColor(testedPositon))
                            {
                                return 2;
                            }

                            if (ChessController.Board.gameSpace[x, y].GetType() == typeof(King))
                            {
                                return 5;
                            }

                            if (!((x == positionXY[0]) && (y == positionXY[1])))
                            {

                                return 3;
                            }
                        }
                    }


                    if (IsYPositive)
                        y--;
                    else
                        y++;
                }
                    if (IsXPositive)
                        x--;
                    else
                        x++;
            }

            return 0;
        }

        public override List<string> GetAvailableMoves(bool isQueen)
        {
            List<string> availibleMoves = new List<string>();

            //int xIter = 0;
            //int yIter = 0;
            //for (int x = XPosition; x < 8; x++)
            //{
            //    xIter++;
            //    for (int y = YPosition; y < 8; y++)
            //    {
            //        yIter++;
            //        if (xIter == yIter)
            //        {
            //            if ()
            //            {
            //                availibleMoves.Add($"{ Convert.ToString(Convert.ToChar(x + 97)) }{ y + 1}");

            //            }
            //        }
            //    }
            //}

            //xIter = 0;
            //yIter = 0;
            //for (int x = XPosition; x < 8; x++)
            //{
            //    xIter++;
            //    for (int y = YPosition; y >= 0; y--)
            //    {
            //        yIter++;
            //        if (xIter == yIter)
            //        {

            //        }
            //    }
            //}

            //xIter = 0;
            //yIter = 0;
            //for (int x = XPosition; x >= 0; x--)
            //{
            //    xIter++;
            //    for (int y = YPosition; y < 8; y++)
            //    {
            //        yIter++;
            //        if (xIter == yIter)
            //        {

            //        }
            //    }
            //}

            //xIter = 0;
            //yIter = 0;
            //for (int x = XPosition; x >= 0; x--)
            //{
            //    xIter++;
            //    for (int y = YPosition; y >= 0; y--)
            //    {
            //        yIter++;
            //        if (xIter == yIter)
            //        {

            //        }
            //    }
            //}

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (Math.Abs(x - XPosition) == Math.Abs(y - YPosition))
                    {
                        int checkMovement = Move(new int[] { x, y }, false);
                        if (checkMovement == 0 || checkMovement == 6)
                        {
                            availibleMoves.Add($"{ Convert.ToString(Convert.ToChar(x + 97)) }{ Math.Abs(y - 8) }");
                        }
                    }
                }
            }

            bool inCheck = ChessController.Board.IsKingInCheck(ChessController.IsWhite)[2] == 1;

            if (inCheck && !isQueen)
            {
                foreach (string move in movablePositions)
                {
                    Piece tempPiece = ChessController.Board.GetPieceAt(move);
                    int xPos = XPosition;
                    int yPos = YPosition;
                    Piece currentPieceClone = new Rook((char.IsLower(GetSymbol()) ? "White" : "Black"), XPosition, YPosition);

                    //BUG: Pawn promotes if can block or take to last row
                    Move(ChessController.ConvertToXY(move), true);

                    if (ChessController.Board.IsKingInCheck(ChessController.IsWhite)[2] == 1)
                    {
                        movablePositions.Remove(move);
                    }

                    ChessController.Board.gameSpace[xPos, yPos] = currentPieceClone;
                    ChessController.Board.gameSpace[ChessController.ConvertToXY(move)[0], ChessController.ConvertToXY(move)[1]] = tempPiece;
                }
            }

            return availibleMoves;
        }

        public Bishop(String color, int x, int y) : base(color.Equals("White") ? 'b' : 'B', x, y)
        {
        }
    }
}
